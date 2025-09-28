public class WorkshopStateUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Editor Hookups")]
	private SendSelectDialog sendSelectDialog;

	[global::UnityEngine.SerializeField]
	private WorkshopStateUIActions workshopStateUIActions;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Select Buttons")]
	private global::UnityEngine.Transform buttonsParent;

	[global::UnityEngine.SerializeField]
	private WorkshopAnimatronicButton workshopAnimatronicButtonPrefab;

	[global::UnityEngine.SerializeField]
	private WorkshopAnimatronicButton_Locked workshopAnimatronicButton_LockedPrefab;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("CTA Buttons")]
	private global::UnityEngine.UI.Button modifyButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button deployButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button wearTearButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject conditionWordDisplay;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI deployButtonText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI availableText;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Top Display")]
	private global::TMPro.TextMeshProUGUI animatronicNameDisplay;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI wearTearPercentText;

	private MasterDomain _masterDomain;

	private WorkshopSlotDataModel _workshopSlotDataModel;

	private WorkshopSlotButtonHandler _workshopSlotButtonHandler;

	private WorkshopPageDisplayHandler _workshopPageDisplayHandler;

	private void AddSubcriptions()
	{
		global::UnityEngine.Debug.LogError("adding subbies");
		_masterDomain.MasterDataDomain.GetAccessToData.GetConfigDataEntryAsync(OnConfigDataLoaded);
	}

	private void OnConfigDataLoaded(CONFIG_DATA.Root configDataEntry)
	{
		global::UnityEngine.Debug.LogError("making handler data");
		global::UnityEngine.Debug.LogError("master domain null now?" + (_masterDomain == null));
		CleanUpWorkshopSlotButtonHandler();
		_workshopSlotButtonHandler = new WorkshopSlotButtonHandler(new WorkshopSlotButtonHandlerData
		{
			masterDomain = _masterDomain,
			eventExposer = _masterDomain.eventExposer,
			workshopSlotDataModel = _masterDomain.GameUIDomain.GameUIData.workshopSlotDataModel,
			prefab = workshopAnimatronicButtonPrefab,
			prefabLocked = workshopAnimatronicButton_LockedPrefab,
			parentTransform = buttonsParent,
			UISelectLockedButton = SelectLockedWarehouseButton,
			itemDefinitions = _masterDomain.ItemDefinitionDomain.ItemDefinitions
		});
		_workshopSlotButtonHandler.GenerateWorkshopAnimatronicButtons(_workshopSlotDataModel.WorkshopSlotDatas);
	}

	private void CleanUpWorkshopSlotButtonHandler()
	{
		if (_workshopSlotButtonHandler != null)
		{
			_workshopSlotButtonHandler.OnDestroy();
		}
		_workshopSlotButtonHandler = null;
	}

	public void ReenableSendSelect()
	{
		sendSelectDialog.ReenableSendSelect();
	}

	public void SelectLockedWarehouseButton(IWorkshopSlotButton workshopAnimatronicButton)
	{
	}

	public void SetupSendSelectDialog(global::System.Action<string> SendSelectedSlotToUserId)
	{
		sendSelectDialog.SetupSendSelectDialog(_masterDomain, SendSelectedSlotToUserId, _masterDomain.PlayerAvatarDomain.AvatarIconHandler);
	}

	private void Awake()
	{
		_masterDomain = MasterDomain.GetDomain();
		global::UnityEngine.Debug.LogError("master domain null?" + (_masterDomain == null));
		_workshopSlotDataModel = _masterDomain.GameUIDomain.GameUIData.workshopSlotDataModel;
		_workshopSlotDataModel.RefreshIcons();
		WorkshopPageHandlerData data = new WorkshopPageHandlerData
		{
			eventExposer = _masterDomain.eventExposer,
			conditionWordDisplay = conditionWordDisplay,
			workshopSlotDataModel = _masterDomain.GameUIDomain.GameUIData.workshopSlotDataModel,
			itemDefinitions = _masterDomain.ItemDefinitionDomain.ItemDefinitions,
			wearTearPercentText = wearTearPercentText,
			animatronicNameDisplay = animatronicNameDisplay,
			availableText = availableText,
			deployButtonText = deployButtonText,
			modifyButton = modifyButton,
			wearTearButton = wearTearButton,
			deployButton = deployButton
		};
		_workshopPageDisplayHandler = new WorkshopPageDisplayHandler(data);
		AddSubcriptions();
	}

	public void OverrideWorkshopSlotDataState(WorkshopSlotData workshopSlotData, WorkshopEntry.Status status)
	{
		_workshopSlotButtonHandler.OverrideWorkshopSlotDataState(workshopSlotData, status);
	}

	private void Update()
	{
		_workshopPageDisplayHandler.Update();
	}

	private void OnDestroy()
	{
		_workshopPageDisplayHandler.OnDestroy();
		CleanUpWorkshopSlotButtonHandler();
	}
}
