public class PurchaseItemResultData
{
	public global::System.Collections.Generic.List<IllumixItemInstance> items;

	public PurchaseItemResultData(global::PlayFab.ClientModels.PurchaseItemResult result)
	{
		items = new global::System.Collections.Generic.List<IllumixItemInstance>();
		if (result.Items.Count < 1)
		{
			return;
		}
		foreach (global::PlayFab.ClientModels.ItemInstance item in result.Items)
		{
			items.Add(new IllumixItemInstance(item));
		}
	}
}
