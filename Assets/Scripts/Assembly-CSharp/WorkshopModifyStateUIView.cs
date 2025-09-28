public class WorkshopModifyStateUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Hook ups")]
	private DialogHandler_WorkshopModify _dialogHandler_WorkshopModify;

	[global::UnityEngine.SerializeField]
	private WorkshopModifyStateUIActions _workshopModifyStateUIActions;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Top Bar")]
	private global::TMPro.TextMeshProUGUI _topBarAnimatronicNameText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _topBarAnimatronicStatusText;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Assemble Tabs")]
	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> _assembleTabs;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Plush Suits")]
	private SlotAssembleButton _plushAssemblyButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _plushSuitSelectorContainer;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Cpus")]
	private CPUCell _cpuCellPrefab;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform _cpuCellParent;

	[global::UnityEngine.SerializeField]
	private SlotAssembleButton _cpuAssemblyButton;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Essence")]
	private global::UnityEngine.UI.Button _essenceAssemblyButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Slider _essenceSlider;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _essenceSlotTotalDisplayText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _essenceTotalText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _sliderValueText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _essenceCurrentText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button _addButton;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Mods")]
	private ModCell _modCellPrefab;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _noModsLabel;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _modsAvailableCountText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _modsTotalCountText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _modSlotNumberDisplayText;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<SlotAssembleButton> _modAssembleButtons;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform _modCellParent;

	private WorkshopModifyPageDisplayHandler _workshopModifyPageDisplayHandler;

	private AssembleButtonsManager _assembleButtonsManager;

	private WorkshopSlotDataSaveHandler _workshopSlotDataSaveHandler;

	private PlushSuitSelector _plushSuitSelector;

	private EssenceSliderHandler _essenceSliderHandler;

	private CPUSelectionHandler _cpuSelectionHandler;

	private ModSelectionHandler _modSelectionHandler;

	private EssenceSliderData _essenceSliderData;

	private ModifyTabHandler _tabHandler;

	private bool resetrequested;

	public void SellMod(GatherModsForEquipping.ModContext modContext)
	{
		_modSelectionHandler.SellModAndUpdateLocalData(modContext);
	}

	public void SetEssenceValueFromSlider()
	{
		_essenceSliderHandler.SetEssenceValueFromSliderValue();
	}

	private void Awake()
	{
		resetrequested = false;
		MasterDomain domain = MasterDomain.GetDomain();
		_workshopSlotDataSaveHandler = new WorkshopSlotDataSaveHandler(new WorkshopSlotDataSaveHandlerLoadData
		{
			workshopSlotDataModel = domain.GameUIDomain.GameUIData.workshopSlotDataModel,
			setEndoskeletonConfigRequester = domain.ServerDomain.endoskeletonConfigRequester
		});
		_tabHandler = new ModifyTabHandler(new ModifyTabHandlerLoadData
		{
			AssembleTabs = _assembleTabs,
			EventExposer = domain.eventExposer
		});
		_workshopModifyPageDisplayHandler = new WorkshopModifyPageDisplayHandler(new WorkshopModifyPageDisplayHandlerLoadData
		{
			ItemDefinitions = domain.ItemDefinitionDomain.ItemDefinitions,
			WorkshopSlotDataModel = domain.GameUIDomain.GameUIData.workshopSlotDataModel,
			TopBarAnimatronicNameText = _topBarAnimatronicNameText,
			ModSlotNumberDisplayText = _modSlotNumberDisplayText,
			TopBarAnimatronicStatusText = _topBarAnimatronicStatusText,
			EventExposer = domain.eventExposer
		});
		_plushSuitSelector = new PlushSuitSelector(_plushSuitSelectorContainer, new WorkshopPlushSuitSelectionHandler(domain.GameUIDomain.GameUIData.workshopSlotDataModel));
		_cpuSelectionHandler = new CPUSelectionHandler(new CPUSelectionHandlerLoadData
		{
			cpuCellPrefab = _cpuCellPrefab,
			cpuCellParent = _cpuCellParent,
			gameAssetManagementDomain = domain.GameAssetManagementDomain,
			itemDefinitions = domain.ItemDefinitionDomain.ItemDefinitions,
			eventExposer = domain.eventExposer,
			workshopSlotDataModel = domain.GameUIDomain.GameUIData.workshopSlotDataModel,
			inventory = domain.WorkshopDomain.Inventory
		});
		_modSelectionHandler = new ModSelectionHandler(new ModSelectionHandlerLoadData
		{
			cellPrefab = _modCellPrefab,
			plushSuitCellParent = _modCellParent,
			SellDialog = _workshopModifyStateUIActions.DisplaySellMod,
			InvalidModCategoryDialog = _dialogHandler_WorkshopModify.ShowInvalidModCategory,
			noModsLabel = _noModsLabel,
			modCountText = _modsAvailableCountText,
			modsTotalCountText = _modsTotalCountText,
			workshopSlotDataModel = domain.GameUIDomain.GameUIData.workshopSlotDataModel,
			dataConnector = domain.GameUIDomain.GameUIData.gameUIMasterDataConnector,
			modInventory = domain.WorkshopDomain.Inventory.ModInventory,
			sellModsRequester = domain.ServerDomain.sellModsRequester,
			eventExposer = domain.eventExposer,
			gameAssetManagementDomain = domain.GameAssetManagementDomain,
			ServerGameUiDataModel = domain.GameUIDomain.GameUIData.serverGameUIDataModel
		});
		_essenceSliderData = new EssenceSliderData();
		_essenceSliderData.addButton = _addButton;
		_essenceSliderData.essenceSlider = _essenceSlider;
		_essenceSliderData.sliderValueText = _sliderValueText;
		_essenceSliderData.essenceTotalText = _essenceTotalText;
		_essenceSliderData.essenceCurrentText = _essenceCurrentText;
		_essenceSliderData.eventExposer = domain.eventExposer;
		_essenceSliderData.currencyContainer = domain.WorkshopDomain.Inventory.CurrencyContainer;
		_essenceSliderData.workshopSlotDataModel = domain.GameUIDomain.GameUIData.workshopSlotDataModel;
		_essenceSliderData.setEndoskeletonConfigRequester = domain.ServerDomain.endoskeletonConfigRequester;
		_essenceSliderHandler = new EssenceSliderHandler(_essenceSliderData);
		_assembleButtonsManager = new AssembleButtonsManager(new AssembleButtonsManagerLoadData
		{
			WorkshopSlotDataModel = domain.GameUIDomain.GameUIData.workshopSlotDataModel,
			EssenceSlotTotalDisplayText = _essenceSlotTotalDisplayText,
			GameAssetManagementDomain = domain.GameAssetManagementDomain,
			ItemDefinitions = domain.ItemDefinitionDomain.ItemDefinitions,
			ModAssembleButtons = _modAssembleButtons,
			CpuAssemblyButton = _cpuAssemblyButton,
			PlushAssemblyButton = _plushAssemblyButton,
			EssenceAssemblyButton = _essenceAssemblyButton,
			EventExposer = domain.eventExposer,
			ServerGameUiDataModel = domain.GameUIDomain.GameUIData.serverGameUIDataModel
		});
	}

	private void Start()
	{
		MasterDomain.GetDomain();
		resetrequested = true;
	}

	private void Update()
	{
		_workshopModifyPageDisplayHandler.Update();
		_essenceSliderHandler.Update();
		_assembleButtonsManager.Update();
		MasterDomain domain = MasterDomain.GetDomain();
		if (domain.GameUIDomain.WorkshopStage != null && resetrequested)
		{
			domain.GameUIDomain.WorkshopStage.SetWorkshopModifyCamPos();
			global::UnityEngine.Debug.Log("called reset camera");
			domain.GameUIDomain.WorkshopStage.ResetCameraRotation();
			resetrequested = false;
		}
	}

	private void OnDestroy()
	{
		_workshopSlotDataSaveHandler.OnDisable();
		if (MasterDomain.GetDomain().GameUIDomain != null)
		{
			MasterDomain.GetDomain().GameUIDomain.WorkshopStage.SetWorkshopCamPos();
			MasterDomain.GetDomain().GameUIDomain.WorkshopStage.ResetCameraRotation();
		}
		_modSelectionHandler.TearDown();
		_cpuSelectionHandler.OnDestroy();
		_essenceSliderHandler.OnDestroy();
		_workshopModifyPageDisplayHandler.OnDestroy();
		_tabHandler.OnDestroy();
	}

	private void OnEnable()
	{
		_plushSuitSelector.Setup(OnNewPlushSuitSelected);
	}

	private void OnNewPlushSuitSelected(string plushSuitId)
	{
		if (MasterDomain.GetDomain().GameUIDomain.GameUIData.workshopSlotDataModel == null)
		{
			return;
		}
		WorkshopSlotDataModel workshopSlotDataModel = MasterDomain.GetDomain().GameUIDomain.GameUIData.workshopSlotDataModel;
		if (workshopSlotDataModel.GetSelectedSlotData() != null)
		{
			workshopSlotDataModel.GetSelectedSlotData().endoskeleton.plushSuit = plushSuitId;
			new WorkshopEntry(workshopSlotDataModel.GetSelectedSlotData().workshopEntry);
			workshopSlotDataModel.GetSelectedSlotData().UpdateIcon();
			workshopSlotDataModel.GetSelectedSlotData().UpdateIsDirty();
			if (!workshopSlotDataModel.GetSelectedSlotData().ValidatePlushtrapCpu(workshopSlotDataModel.GetSelectedSlotData().endoskeleton.cpu, plushSuitId))
			{
				workshopSlotDataModel.GetSelectedSlotData().endoskeleton.cpu = "BareEndo";
			}
		}
	}

	private void OnDisable()
	{
	}

	public void TabSwitched()
	{
		resetrequested = true;
		_workshopSlotDataSaveHandler.OnDisable();
	}

	public void SwitchedToTab()
	{
		_plushSuitSelector.Setup(OnNewPlushSuitSelected);
	}
}
