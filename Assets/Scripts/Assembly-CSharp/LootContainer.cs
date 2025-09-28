public class LootContainer
{
	private global::System.Collections.Generic.Dictionary<string, LootStructureData> _lootStructures;

	private global::System.Collections.Generic.Dictionary<string, LootPackageData> _lootPackages;

	private global::System.Collections.Generic.Dictionary<string, LootTableData> _lootTables;

	private global::System.Collections.Generic.Dictionary<string, LootItemData> _lootItems;

	private global::System.Collections.Generic.Dictionary<string, CrateInfoData> _crateInfos;

	public LootContainer()
	{
		_lootStructures = new global::System.Collections.Generic.Dictionary<string, LootStructureData>();
		_lootPackages = new global::System.Collections.Generic.Dictionary<string, LootPackageData>();
		_lootTables = new global::System.Collections.Generic.Dictionary<string, LootTableData>();
		_lootItems = new global::System.Collections.Generic.Dictionary<string, LootItemData>();
		_crateInfos = new global::System.Collections.Generic.Dictionary<string, CrateInfoData>();
	}

	public void LoadLootStructureDataFromMasterData(LOOT_STRUCTURE_DATA.Root data)
	{
		foreach (LOOT_STRUCTURE_DATA.Entry entry in data.Entries)
		{
			LootStructureData value = new LootStructureData(entry);
			_lootStructures.Add(entry.Logical, value);
		}
	}

	public void LoadLootPackageDataFromMasterData(LOOT_PACKAGE_DATA.Root data)
	{
		foreach (LOOT_PACKAGE_DATA.Entry entry in data.Entries)
		{
			LootPackageData value = new LootPackageData(entry);
			_lootPackages.Add(entry.Logical, value);
		}
	}

	public void LoadLootTableDataFromMasterData(LOOT_TABLE_DATA.Root data)
	{
		foreach (LOOT_TABLE_DATA.Entry entry in data.Entries)
		{
			LootTableData value = new LootTableData(entry);
			_lootTables.Add(entry.Logical, value);
		}
	}

	public void LoadLootItemDataFromMasterData(LOOT_ITEM_DATA.Root data)
	{
		foreach (LOOT_ITEM_DATA.Entry entry in data.Entries)
		{
			LootItemData value = new LootItemData(entry);
			_lootItems.Add(entry.Item, value);
		}
	}

	public void LoadCrateInfoDataFromMasterData(CRATE_INFO_DATA.Root data)
	{
		foreach (CRATE_INFO_DATA.Entry entry in data.Entries)
		{
			CrateInfoData value = new CrateInfoData(entry);
			_crateInfos.Add(entry.Logical, value);
		}
	}

	public LootStructureData GetLootStructureForId(string id)
	{
		if (_lootStructures.TryGetValue(id, out var value))
		{
			return value;
		}
		global::UnityEngine.Debug.LogError("LootContainer GetLootStructureForId - Requested loot structure '" + id + "' is not found in the LOOT_STRUCTURE_DATA sheet");
		return null;
	}

	public LootPackageData GetLootPackageForId(string id)
	{
		if (_lootPackages.TryGetValue(id, out var value))
		{
			return value;
		}
		global::UnityEngine.Debug.LogError("LootContainer GetLootItemForId - Requested loot package '" + id + "' is not found in the LOOT_PACKAGE_DATA sheet");
		return null;
	}

	public LootTableData GetLootTableForId(string id)
	{
		if (_lootTables.TryGetValue(id, out var value))
		{
			return value;
		}
		global::UnityEngine.Debug.LogError("LootContainer GetLootItemForId - Requested loot table '" + id + "' is not found in the LOOT_TABLE_DATA sheet");
		return null;
	}

	public LootItemData GetLootItemForId(string id)
	{
		if (_lootItems.TryGetValue(id, out var value))
		{
			return value;
		}
		global::UnityEngine.Debug.LogError("LootContainer GetLootItemForId - Requested loot item '" + id + "' is not found in the LOOT_ITEM_DATA sheet");
		return null;
	}

	public CrateInfoData GetCrateInfoForId(string id)
	{
		if (_crateInfos.ContainsKey(id))
		{
			return _crateInfos[id];
		}
		return null;
	}
}
