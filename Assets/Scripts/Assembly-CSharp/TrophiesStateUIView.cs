public class TrophiesStateUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Editor Hookups")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform trophyPrefabParent;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI trophyNameText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI trophyDescriptionText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject checkActiveTrophyCanvas;

	[global::UnityEngine.Header("Selection")]
	[global::UnityEngine.SerializeField]
	private TrophyCell _trophyCellPrefab;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform _trophyCellParent;

	private global::UnityEngine.GameObject spawnedPrefab;

	private GameAssetManagementDomain gameAssetManagementDomain;

	private Localization loc;

	private TrophySelectionHandler _trophySelectionHandler;

	private float rotateSpeed;

	private GenericCreationRequest _currentRequest;

	private void Awake()
	{
		MasterDomain domain = MasterDomain.GetDomain();
		gameAssetManagementDomain = domain.GameAssetManagementDomain;
		loc = domain.LocalizationDomain.Localization;
		domain.eventExposer.add_TrophyChanged(SpawnTrophy);
		_trophySelectionHandler = new TrophySelectionHandler(new TrophySelectionHandlerLoadData
		{
			cellPrefab = _trophyCellPrefab,
			cellParent = _trophyCellParent,
			assetCache = gameAssetManagementDomain.AssetCacheAccess,
			itemDefinitions = domain.ItemDefinitionDomain.ItemDefinitions,
			inventory = domain.WorkshopDomain.Inventory
		});
		rotateSpeed = 180f / ((float)global::UnityEngine.Screen.width * 0.5f);
	}

	public void SpawnTrophy(TrophyData data)
	{
		if (spawnedPrefab != null)
		{
			global::UnityEngine.Object.Destroy(spawnedPrefab);
		}
		trophyPrefabParent.localRotation = global::UnityEngine.Quaternion.Euler(0f, -90f, 0f);
		trophyNameText.text = loc.GetLocalizedString(data.TrophyName, data.TrophyName);
		trophyDescriptionText.text = loc.GetLocalizedString(data.TrophyDescription, data.TrophyDescription);
		if (_currentRequest != null)
		{
			_currentRequest.CancelRequest();
		}
		_currentRequest = new GenericCreationRequest(data.Bundle, data.Asset);
		_currentRequest.add_OnRequestComplete(PrefabSpawned);
		gameAssetManagementDomain.CreateSafeObject(_currentRequest);
	}

	private void PrefabSpawned(GenericCreationRequest req)
	{
		if (!(req.SpawnedObject == null) && !req.IsCancelled())
		{
			spawnedPrefab = req.SpawnedObject;
			spawnedPrefab.transform.SetParent(trophyPrefabParent, worldPositionStays: true);
			spawnedPrefab.transform.localPosition = global::UnityEngine.Vector3.zero;
			spawnedPrefab.transform.localRotation = global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(spawnedPrefab.transform.localRotation.eulerAngles.x, spawnedPrefab.transform.localRotation.eulerAngles.y - 90f, spawnedPrefab.transform.localRotation.eulerAngles.z));
			spawnedPrefab.layer = 8;
		}
	}

	private void OnDestroy()
	{
		_trophySelectionHandler.OnDestroy();
		MasterDomain.GetDomain().eventExposer.remove_TrophyChanged(SpawnTrophy);
	}

	private void OnEnable()
	{
		MasterDomain.GetDomain().eventExposer.add_GestureTouchEvent(GestureTouchEventHandler);
	}

	private void OnDisable()
	{
		MasterDomain.GetDomain().eventExposer.remove_GestureTouchEvent(GestureTouchEventHandler);
	}

	private void GestureTouchEventHandler(global::System.Collections.Generic.List<global::UnityEngine.Vector2> prevTouchPosition, global::System.Collections.Generic.List<global::UnityEngine.Vector2> currTouchPosition)
	{
		if (checkActiveTrophyCanvas.activeInHierarchy)
		{
			RotateCamera(prevTouchPosition, currTouchPosition);
		}
	}

	private void RotateCamera(global::System.Collections.Generic.List<global::UnityEngine.Vector2> prevTouches, global::System.Collections.Generic.List<global::UnityEngine.Vector2> currTouches)
	{
		if (prevTouches.Count == 0)
		{
			global::UnityEngine.Debug.Log("No prev");
		}
		else if (currTouches.Count == 1 && currTouches.Count == prevTouches.Count)
		{
			_ = 180f / ((float)global::UnityEngine.Screen.width * 0.5f);
			RotateTrophyFacing(prevTouches[0], currTouches[0]);
		}
	}

	public void RotateTrophyFacing(global::UnityEngine.Vector3 mPrevPos, global::UnityEngine.Vector3 mCurrPos)
	{
		if (!(spawnedPrefab == null))
		{
			trophyPrefabParent.transform.Rotate(0f, 0f - (mCurrPos.x - mPrevPos.x) * rotateSpeed, 0f, global::UnityEngine.Space.Self);
		}
	}
}
