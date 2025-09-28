public class LootItemCellDisplay : IxItemCellDisplay<LootRewardEntry>
{
	private global::TMPro.TextMeshProUGUI _itemName;

	private global::TMPro.TextMeshProUGUI _itemDescription;

	private global::UnityEngine.UI.Image _itemIcon;

	protected override void PopulateComponents()
	{
		global::System.Type[] onlyCacheTypes = new global::System.Type[2]
		{
			typeof(global::TMPro.TextMeshProUGUI),
			typeof(global::UnityEngine.UI.Image)
		};
		_components.CacheComponents(_root, onlyCacheTypes);
		_itemName = _components.TryGetComponent<global::TMPro.TextMeshProUGUI>("ItemCellNameLabel");
		_itemDescription = _components.TryGetComponent<global::TMPro.TextMeshProUGUI>("ItemCellDescLabel");
		_itemIcon = _components.TryGetComponent<global::UnityEngine.UI.Image>("ItemImage");
	}

	public override void UpdateData()
	{
		MasterDomain.GetDomain().GameAssetManagementDomain.IconLookupAccess.GetInterfaceAsync(UpdateDatab__4_0);
	}

	private void UpdateDatab__4_0(IconLookup lookup)
	{
		global::UnityEngine.Debug.Log("getting item " + _dataItem.LootItem.Item + " and lookup is null? - " + (lookup == null));
		MasterDomain.GetDomain().ItemDefinitionDomain.ItemDefinitions.GetDisplayDataForRewardData(_dataItem, lookup, UpdateDatab__4_1);
	}

	private void UpdateDatab__4_1(LootDisplayData displayData)
	{
		_itemIcon.overrideSprite = displayData.DisplayIcon;
		_itemName.text = displayData.DisplayName;
		_itemDescription.text = displayData.DisplayQuantity.ToString();
	}
}
