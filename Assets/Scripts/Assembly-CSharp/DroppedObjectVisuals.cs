public class DroppedObjectVisuals
{
	private AssetCache _assetCache;

	private IDroppedObjectLoader _loader;

	private global::UnityEngine.Transform _parent;

	private global::UnityEngine.Camera _camera;

	private DropsObjectsData _settings;

	private global::System.Action _readyCallback;

	private global::UnityEngine.Transform _root;

	private global::System.Collections.Generic.List<DroppedObject> _preloadedDroppedObjects;

	private global::System.Collections.Generic.List<int> _remainingIndexList;

	private global::System.Collections.Generic.List<DroppedObject> _activeDroppedObjects;

	private const string DroppedObjectRootName = "DroppedObjectsRoot";

	private const string DroppedObjectTagName = "DroppedObject";

	private const string SpawnTriggerName = "Spawn";

	private const string DespawnTriggerName = "Despawn";

	public global::System.Collections.Generic.List<DroppedObject> ActiveObjects => _activeDroppedObjects;

	public DroppedObjectVisuals(AssetCache assetCache, global::UnityEngine.Transform parent, global::UnityEngine.Camera camera)
	{
		_assetCache = assetCache;
		_parent = parent;
		_camera = camera;
		_activeDroppedObjects = new global::System.Collections.Generic.List<DroppedObject>();
	}

	public void Teardown()
	{
		_assetCache = null;
		_activeDroppedObjects = null;
		_parent = null;
		_camera = null;
	}

	public void PreloadDroppableObjects(DropsObjectsData settings, PlushSuitData plushSuitData, global::System.Action readyCallback)
	{
		_settings = settings;
		_readyCallback = readyCallback;
		if (settings.SpawnType != DropsObjectsMechanic.SpawnType.FromPlushSuit)
		{
			if (settings.SpawnType == DropsObjectsMechanic.SpawnType.FromBundle)
			{
				PreloadFromBundle();
			}
		}
		else
		{
			PreloadFromPlushSuit(plushSuitData);
		}
	}

	public void DestroyDroppableObjects()
	{
		if (_root != null)
		{
			global::UnityEngine.Object.Destroy(_root.gameObject);
			_root = null;
		}
		_remainingIndexList = null;
		_activeDroppedObjects.Clear();
		if (_loader != null)
		{
			_loader.Unload();
		}
		_loader = null;
		_settings = null;
		_readyCallback = null;
	}

	public DroppedObject SpawnDroppedObject(global::UnityEngine.Vector3 pos)
	{
		if (_root == null)
		{
			return null;
		}
		if (_preloadedDroppedObjects.Count < 1)
		{
			return null;
		}
		DroppedObject droppedObject = _preloadedDroppedObjects[PickIndexToSpawn()];
		if (droppedObject == null)
		{
			return null;
		}
		droppedObject.SpawnTime = global::UnityEngine.Time.time;
		if (pos != global::UnityEngine.Vector3.zero)
		{
			droppedObject.transform.position = pos;
			droppedObject.transform.LookAt(new global::UnityEngine.Vector3(0f, droppedObject.transform.position.y, 0f));
		}
		else
		{
			droppedObject.transform.position = CalculateWorldPositionFromCameraTransform(_camera.transform);
		}
		droppedObject.GetComponentInChildren<global::UnityEngine.Collider>(includeInactive: true).enabled = true;
		if (droppedObject.animator != null)
		{
			droppedObject.animator.SetTrigger("Spawn");
		}
		droppedObject.modelRoot.gameObject.SetActive(value: true);
		_activeDroppedObjects.Add(droppedObject);
		return droppedObject;
	}

	public void DespawnDroppedObjects()
	{
		if (_root == null)
		{
			return;
		}
		foreach (DroppedObject activeDroppedObject in _activeDroppedObjects)
		{
			DespawnDroppedObject(activeDroppedObject);
		}
	}

	public void DespawnDroppedObject(DroppedObject activeDroppedObject)
	{
		if (!(_root == null) && !(activeDroppedObject == null))
		{
			if (activeDroppedObject.animator != null)
			{
				activeDroppedObject.animator.SetTrigger("Despawn");
			}
			else
			{
				activeDroppedObject.gameObject.SetActive(value: false);
			}
			CoroutineHelper.StartCoroutine(WaitAndRemoveFromList(activeDroppedObject));
		}
	}

	private global::System.Collections.IEnumerator WaitAndRemoveFromList(DroppedObject activeDroppedObject)
	{
		yield return new global::UnityEngine.WaitForEndOfFrame();
		if (_activeDroppedObjects.Contains(activeDroppedObject))
		{
			_activeDroppedObjects.Remove(activeDroppedObject);
		}
		yield return null;
	}

	public DroppedObject TestTouchVsDroppedObjects(global::UnityEngine.Vector2 position)
	{
		if (!_camera.gameObject.activeSelf)
		{
			return null;
		}
		global::UnityEngine.Ray ray = _camera.ScreenPointToRay(position);
		global::UnityEngine.Debug.Log("Casting");
		if (!global::UnityEngine.Physics.Raycast(ray, out var hitInfo, 1000f))
		{
			return null;
		}
		global::UnityEngine.Debug.Log("HIT: " + hitInfo.transform.name);
		MoveableEnvironmentObject component = hitInfo.transform.parent.GetComponent<MoveableEnvironmentObject>();
		if (component != null)
		{
			global::UnityEngine.Debug.Log("moving moveable object");
			component.Tapped();
			return null;
		}
		DroppedObject component2 = hitInfo.transform.parent.parent.GetComponent<DroppedObject>();
		if (component2 == null)
		{
			global::UnityEngine.Debug.LogError("No DroppedObject script on parent parent");
			return null;
		}
		component2.GetComponentInChildren<global::UnityEngine.Collider>(includeInactive: true).enabled = false;
		return component2;
	}

	private void NotifyReady()
	{
		_readyCallback?.Invoke();
		_readyCallback = null;
	}

	private void PreloadFromBundle()
	{
		CreateRoot();
		if (!(_root == null) && _settings != null)
		{
			_loader = new DroppedObjectFromAssetBundleLoader(_assetCache, _settings.TemplateBundleName, _settings.TemplateAssetName, _settings.ObjectBundleName, _settings.ObjectAssetNames);
			_loader.Load(_root, LoadComplete);
		}
	}

	private void PreloadFromPlushSuit(PlushSuitData plushSuitData)
	{
		CreateRoot();
		if (!(_root == null) && _settings != null)
		{
			_loader = new DroppedObjectFromAssetBundleLoader(_assetCache, _settings.TemplateBundleName, _settings.TemplateAssetName, plushSuitData.MangleEncounterBundleName, plushSuitData.MangleEncounterAssetNames);
			_loader.Load(_root, LoadComplete);
		}
	}

	private void LoadComplete(global::System.Collections.Generic.List<DroppedObject> droppedObjects)
	{
		_preloadedDroppedObjects = droppedObjects;
		BuildRemainingIndexList();
		NotifyReady();
	}

	private void BuildRemainingIndexList()
	{
		if (_remainingIndexList == null)
		{
			_remainingIndexList = new global::System.Collections.Generic.List<int>(_preloadedDroppedObjects.Count);
		}
		foreach (DroppedObject preloadedDroppedObject in _preloadedDroppedObjects)
		{
			_remainingIndexList.Add(_preloadedDroppedObjects.IndexOf(preloadedDroppedObject));
		}
	}

	private void CreateRoot()
	{
		global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("DroppedObjectsRoot");
		if (!(gameObject == null))
		{
			_root = gameObject.transform;
			_root.parent = _parent;
		}
	}

	private int PickIndexToSpawn()
	{
		int index = global::UnityEngine.Random.Range(0, _remainingIndexList.Count);
		if (_remainingIndexList.Count != 0)
		{
			int result = _remainingIndexList[index];
			_remainingIndexList.RemoveAt(index);
			return result;
		}
		BuildRemainingIndexList();
		int result2 = _remainingIndexList[index];
		_remainingIndexList.RemoveAt(index);
		return result2;
	}

	private global::UnityEngine.Vector3 CalculateWorldPositionFromCameraTransform(global::UnityEngine.Transform camera)
	{
		global::UnityEngine.Vector3 vector = global::UnityEngine.Vector3.ProjectOnPlane(camera.forward, global::UnityEngine.Vector3.up);
		global::UnityEngine.Quaternion quaternion = global::UnityEngine.Quaternion.AngleAxis(global::UnityEngine.Random.Range(_settings.AllowedAngle.Min, _settings.AllowedAngle.Max), global::UnityEngine.Vector3.up);
		global::UnityEngine.Vector3 result = camera.position + quaternion * vector * global::UnityEngine.Random.Range(_settings.AllowedDistance.Min, _settings.AllowedDistance.Max);
		result.y = global::UnityEngine.Random.Range(_settings.FallbackHeightOffset.Min, _settings.FallbackHeightOffset.Max) + result.y;
		return result;
	}
}
