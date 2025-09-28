public class IllumixItemInstance
{
	public string itemId;

	public int unitPrice;

	public string itemClass;

	public IllumixItemInstance(global::PlayFab.ClientModels.ItemInstance itemInstance)
	{
		itemId = itemInstance.ItemId;
		unitPrice = (int)itemInstance.UnitPrice;
		itemClass = itemInstance.ItemClass;
	}
}
