public class WorkshopStageView : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform workshopStageTransform;

	public global::UnityEngine.Transform cameraTransform;

	public global::UnityEngine.Vector3 workshopCameraPos;

	public global::UnityEngine.Vector3 workshopModifyCameraPos;

	private MasterDomain _masterDomain;

	private WorkshopStage _workshopStage;

	private WorkshopSlotDataModel _workshopSlotDataModel;

	private int _lastSelectedSlotIndex;

	private string _lastSelectedPlushSuitId;

	private global::UnityEngine.Vector3 _desiredCamPos;

	private global::System.Collections.Generic.List<string> _requestedSpawnIds;

	public global::UnityEngine.GameObject checkActiveModifyCanvas;

	public void SetWorkshopCamPos()
	{
		_desiredCamPos = workshopCameraPos;
	}

	public void SetWorkshopModifyCamPos()
	{
		_desiredCamPos = workshopModifyCameraPos;
	}

	public void ResetCameraRotation()
	{
		global::UnityEngine.Debug.Log("reset cam position");
		if (!(cameraTransform == null))
		{
			cameraTransform.localRotation = global::UnityEngine.Quaternion.identity;
			_workshopStage.ResetAniamtronicRotation();
		}
	}

	private void Start()
	{
		_requestedSpawnIds = new global::System.Collections.Generic.List<string>();
		_workshopSlotDataModel = _masterDomain.GameUIDomain.GameUIData.workshopSlotDataModel;
		if (_masterDomain != null && _workshopStage == null)
		{
			_workshopStage = new WorkshopStage(_masterDomain.eventExposer, _masterDomain.Animatronic3DDomain, _masterDomain.ItemDefinitionDomain, workshopStageTransform, SpawnComplete, _workshopSlotDataModel);
		}
	}

	private void OnEnable()
	{
		_masterDomain = MasterDomain.GetDomain();
		_masterDomain.eventExposer.add_GestureTouchEvent(GestureTouchEventHandler);
		_lastSelectedSlotIndex = 0;
		_desiredCamPos = workshopCameraPos;
		_lastSelectedPlushSuitId = string.Empty;
		if (_workshopStage == null)
		{
			_workshopStage = new WorkshopStage(_masterDomain.eventExposer, _masterDomain.Animatronic3DDomain, _masterDomain.ItemDefinitionDomain, workshopStageTransform, SpawnComplete, _workshopSlotDataModel);
		}
	}

	private void OnDisable()
	{
		_masterDomain.eventExposer.remove_GestureTouchEvent(GestureTouchEventHandler);
		_workshopStage.ClearStage();
	}

	private void OnDestroy()
	{
		_workshopStage.Teardown();
		_workshopStage = null;
	}

	private void Update()
	{
		TryToSpawnWorkshopAnimatronic();
		UpdateCameraPosition();
		UpdateIsWaitingForSpawn();
	}

	private void UpdateIsWaitingForSpawn()
	{
		if (_workshopSlotDataModel != null)
		{
			_workshopSlotDataModel.StageIsWaitingForSpawn = _requestedSpawnIds.Count > 0;
		}
	}

	private void TryToSpawnWorkshopAnimatronic()
	{
		if (_workshopSlotDataModel != null && _workshopSlotDataModel.GetSelectedSlotData() != null && (_workshopSlotDataModel.GetSelectedSlotDataIndex() != _lastSelectedSlotIndex || !(_workshopSlotDataModel.GetSelectedSlotData().endoskeleton.plushSuit == _lastSelectedPlushSuitId)))
		{
			SpawnWorkshopAnimatronic();
			ResetCameraRotation();
		}
	}

	private void UpdateCameraPosition()
	{
		cameraTransform.localPosition = _desiredCamPos;
	}

	private void SpawnWorkshopAnimatronic()
	{
		if (_workshopSlotDataModel != null && _workshopSlotDataModel.GetSelectedSlotData() != null)
		{
			_lastSelectedSlotIndex = _workshopSlotDataModel.GetSelectedSlotDataIndex();
			_lastSelectedPlushSuitId = _workshopSlotDataModel.GetSelectedSlotData().endoskeleton.plushSuit;
			_requestedSpawnIds.Add(_workshopSlotDataModel.GetSelectedSlotData().endoskeleton.plushSuit);
			WorkshopEntry workshopEntry = new WorkshopEntry(_workshopSlotDataModel.GetSelectedSlotData().workshopEntry);
			workshopEntry.endoskeleton = _workshopSlotDataModel.GetSelectedSlotData().endoskeleton;
			_workshopStage.RequestAnimatronicSpawn(workshopEntry);
		}
	}

	private void SpawnComplete(string animatronicId)
	{
		_requestedSpawnIds.Remove(animatronicId);
	}

	private void RotateCamera(global::System.Collections.Generic.List<global::UnityEngine.Vector2> prevTouches, global::System.Collections.Generic.List<global::UnityEngine.Vector2> currTouches)
	{
		if (prevTouches.Count != 0 && currTouches.Count == 1 && currTouches.Count == prevTouches.Count)
		{
			float num = 180f / ((float)global::UnityEngine.Screen.width * 0.5f);
			if (!(cameraTransform == null))
			{
				cameraTransform.Rotate(new global::UnityEngine.Vector3
				{
					x = 0f,
					y = num * (currTouches[0].x - prevTouches[0].x),
					z = 0f
				}, global::UnityEngine.Space.Self);
				_workshopStage.RotateAnimatronic(prevTouches[0], currTouches[0]);
			}
		}
	}

	private void GestureTouchEventHandler(global::System.Collections.Generic.List<global::UnityEngine.Vector2> prevTouchPosition, global::System.Collections.Generic.List<global::UnityEngine.Vector2> currTouchPosition)
	{
		if (checkActiveModifyCanvas.activeInHierarchy)
		{
			RotateCamera(prevTouchPosition, currTouchPosition);
		}
	}
}
