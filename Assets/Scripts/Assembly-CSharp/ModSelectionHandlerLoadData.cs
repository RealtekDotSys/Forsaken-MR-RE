public struct ModSelectionHandlerLoadData
{
	public ModCell cellPrefab;

	public global::UnityEngine.Transform plushSuitCellParent;

	public global::System.Action<ModCell> SellDialog;

	public global::System.Action InvalidModCategoryDialog;

	public global::UnityEngine.GameObject noModsLabel;

	public global::TMPro.TextMeshProUGUI modCountText;

	public global::TMPro.TextMeshProUGUI modsTotalCountText;

	public WorkshopSlotDataModel workshopSlotDataModel;

	public GameUIMasterDataConnector dataConnector;

	public ModInventory modInventory;

	public SellModsRequester sellModsRequester;

	public EventExposer eventExposer;

	public GameAssetManagementDomain gameAssetManagementDomain;

	public ServerGameUIDataModel ServerGameUiDataModel;
}
