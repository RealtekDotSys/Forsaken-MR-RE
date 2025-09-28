public class DroppedObjectFromAssetBundleLoader : IDroppedObjectLoader
{
	private AssetCache _assetCache;

	private string _templateBundleName;

	private string _templateAssetName;

	private string _objectBundleName;

	private string[] _objectAssetNames;

	private DroppedObject _templatePrefab;

	private global::UnityEngine.Transform _parent;

	private global::System.Collections.Generic.List<DroppedObject> _droppedObjects;

	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> _loadedDroppedObjectModels;

	private global::System.Action<global::System.Collections.Generic.List<DroppedObject>> _onComplete;

	private bool _hasBeenUsed;

	private bool _isLoading;

	private bool _unloadRequested;

	private int _failureCount;

	public DroppedObjectFromAssetBundleLoader(AssetCache assetCache, string templateBundleName, string templateAssetName, string objectBundleName, string[] objectAssetNames)
	{
		_assetCache = assetCache;
		_templateBundleName = templateBundleName;
		_templateAssetName = templateAssetName;
		_objectBundleName = objectBundleName;
		_objectAssetNames = objectAssetNames;
	}

	public void Load(global::UnityEngine.Transform parent, global::System.Action<global::System.Collections.Generic.List<DroppedObject>> onComplete)
	{
		if (_hasBeenUsed)
		{
			global::UnityEngine.Debug.LogError("DroppedObjectFromAssetBundleLoader Load - Load cannot be called more than once");
			return;
		}
		_hasBeenUsed = true;
		if (_objectAssetNames.Length != 0)
		{
			_parent = parent;
			_droppedObjects = new global::System.Collections.Generic.List<DroppedObject>();
			_loadedDroppedObjectModels = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();
			_onComplete = onComplete;
			_isLoading = true;
			_assetCache.LoadAsset<global::UnityEngine.GameObject>(_templateBundleName, _templateAssetName, TemplatePrefabLoaded, delegate
			{
				AssetLoadFailure(_templateBundleName, _templateAssetName);
			});
		}
		else
		{
			onComplete?.Invoke(new global::System.Collections.Generic.List<DroppedObject>());
		}
	}

	public void Unload()
	{
		_onComplete = null;
		if (_isLoading)
		{
			_unloadRequested = true;
		}
		else
		{
			UnloadAssets();
		}
	}

	private void TemplatePrefabLoaded(global::UnityEngine.GameObject gameObject)
	{
		if (gameObject != null)
		{
			_templatePrefab = gameObject.GetComponent<DroppedObject>();
		}
		if (_templatePrefab == null)
		{
			AssetLoadFailure(_templateBundleName, _templateAssetName);
		}
		else
		{
			CreateDroppedObjects();
		}
	}

	private void CreateDroppedObjects()
	{
		GameAssetManagementDomain gameAssetManagementDomain = MasterDomain.GetDomain().GameAssetManagementDomain;
		string[] objectAssetNames = _objectAssetNames;
		string assetName;
		for (int i = 0; i < objectAssetNames.Length; i++)
		{
			GenericCreationRequest genericCreationRequest = new GenericCreationRequest(asset: assetName = objectAssetNames[i], bundle: _objectBundleName);
			genericCreationRequest.add_OnRequestComplete(PrefabSpawned);
			gameAssetManagementDomain.CreateSafeObject(genericCreationRequest);
		}
		void PrefabSpawned(GenericCreationRequest req)
		{
			if (req.SpawnedObject == null)
			{
				AssetLoadFailure(_objectBundleName, assetName);
			}
			else
			{
				CreateDroppedObject(req.SpawnedObject);
			}
		}
	}

	private bool CreateDroppedObject(global::UnityEngine.GameObject droppedObjectInstance)
	{
		droppedObjectInstance.name = droppedObjectInstance.name.Replace("(Clone)", "");
		DroppedObject droppedObject = global::UnityEngine.Object.Instantiate(_templatePrefab, _parent);
		if (droppedObject != null && droppedObject.modelRoot != null)
		{
			droppedObjectInstance.transform.SetParent(droppedObject.modelRoot, worldPositionStays: false);
			if (droppedObjectInstance != null)
			{
				droppedObject.gameObject.AddComponent<DroppedObjectMaterialController>().Setup(droppedObject);
				droppedObject.modelRoot.gameObject.SetActive(value: false);
				if (droppedObject.animator != null)
				{
					droppedObject.animator.enabled = true;
					droppedObject.animator.Rebind();
				}
				_droppedObjects.Add(droppedObject);
				_loadedDroppedObjectModels.Add(droppedObjectInstance);
				TryToCompleteLoading();
				return true;
			}
			global::UnityEngine.Object.Destroy(droppedObject);
		}
		return false;
	}

	private void AssetLoadFailure(string bundleName, string assetName)
	{
		global::UnityEngine.Debug.LogError("DroppedObjectFromAssetBundleLoader AssetLoadFailure - Failed to load " + assetName + " from " + bundleName);
		_failureCount++;
		TryToCompleteLoading();
	}

	private void TryToCompleteLoading()
	{
		if (_droppedObjects.Count + _failureCount == _objectAssetNames.Length)
		{
			_onComplete?.Invoke(_droppedObjects);
			_parent = null;
			_onComplete = null;
			_isLoading = false;
			if (_unloadRequested)
			{
				UnloadAssets();
			}
		}
	}

	private void UnloadAssets()
	{
		if (_loadedDroppedObjectModels == null)
		{
			global::UnityEngine.Debug.LogError("Unload already fuckin requested! Time out lil bro!");
			return;
		}
		foreach (global::UnityEngine.GameObject loadedDroppedObjectModel in _loadedDroppedObjectModels)
		{
			_assetCache.ReleaseInstance(loadedDroppedObjectModel);
		}
		_loadedDroppedObjectModels.Clear();
		_loadedDroppedObjectModels = null;
		_objectBundleName = null;
		_objectAssetNames = null;
		_assetCache.ReleaseAsset(_templatePrefab.gameObject);
		_templatePrefab = null;
		_templateBundleName = null;
		_templateAssetName = null;
		_droppedObjects.Clear();
		_droppedObjects = null;
		_assetCache = null;
	}
}
