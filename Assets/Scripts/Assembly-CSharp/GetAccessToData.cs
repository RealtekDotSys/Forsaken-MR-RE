public class GetAccessToData
{
	public bool IsReady;

	private global::System.Action<STATIC_DATA.Root> awaitingStaticData;

	private global::System.Action<CONFIG_DATA.Root> awaitingConfigData;

	private global::System.Action<LOC_DATA.Root> awaitingLOCData;

	private global::System.Action<AUDIO_DATA.Root> awaitingAudioData;

	private global::System.Action<LOOT_STRUCTURE_DATA.Root> awaitingLootStructureData;

	private global::System.Action<LOOT_PACKAGE_DATA.Root> awaitingLootPackageData;

	private global::System.Action<LOOT_TABLE_DATA.Root> awaitingLootTableData;

	private global::System.Action<LOOT_ITEM_DATA.Root> awaitingLootItemData;

	private global::System.Action<CRATE_INFO_DATA.Root> awaitingCrateInfoData;

	private global::System.Action<STORE_DATA.Root> awaitingStoreData;

	private global::System.Action<STORESECTIONS_DATA.Root> awaitingStoreSectionsData;

	private global::System.Action<MODS_DATA.Root> awaitingModsData;

	private global::System.Action<MODCATEGORIES_DATA.Root> awaitingModCategoryData;

	private bool MasterDataHasBeenDeserialized;

	public GetAccessToData()
	{
		global::UnityEngine.Debug.Log("ConstantVariables Instance null? " + (ConstantVariables.Instance == null));
		global::UnityEngine.Debug.Log("ConstantVariables MasterDataDownloader null? " + (ConstantVariables.Instance.MasterDataDownloader == null));
		MasterDataHasBeenDeserialized = ConstantVariables.Instance.MasterDataDownloader.IsDeserialized;
		if (!MasterDataHasBeenDeserialized)
		{
			ConstantVariables.Instance.MasterDataDownloader.add_OnMasterDataDeserialized(DeserializingDone);
		}
	}

	private void DeserializingDone()
	{
		MasterDataHasBeenDeserialized = true;
		ConstantVariables.Instance.MasterDataDownloader.remove_OnMasterDataDeserialized(DeserializingDone);
		if (awaitingStaticData != null)
		{
			awaitingStaticData((STATIC_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(STATIC_DATA)));
		}
		if (awaitingConfigData != null)
		{
			awaitingConfigData((CONFIG_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(CONFIG_DATA)));
		}
		if (awaitingLOCData != null)
		{
			awaitingLOCData((LOC_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(LOC_DATA)));
		}
		if (awaitingAudioData != null)
		{
			awaitingAudioData((AUDIO_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(AUDIO_DATA)));
		}
		if (awaitingLootStructureData != null)
		{
			awaitingLootStructureData((LOOT_STRUCTURE_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(LOOT_STRUCTURE_DATA)));
		}
		if (awaitingLootPackageData != null)
		{
			awaitingLootPackageData((LOOT_PACKAGE_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(LOOT_PACKAGE_DATA)));
		}
		if (awaitingLootTableData != null)
		{
			awaitingLootTableData((LOOT_TABLE_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(LOOT_TABLE_DATA)));
		}
		if (awaitingLootItemData != null)
		{
			awaitingLootItemData((LOOT_ITEM_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(LOOT_ITEM_DATA)));
		}
		if (awaitingCrateInfoData != null)
		{
			awaitingCrateInfoData((CRATE_INFO_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(CRATE_INFO_DATA)));
		}
		if (awaitingStoreData != null)
		{
			awaitingStoreData((STORE_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(STORE_DATA)));
		}
		if (awaitingStoreSectionsData != null)
		{
			awaitingStoreSectionsData((STORESECTIONS_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(STORESECTIONS_DATA)));
		}
		if (awaitingModsData != null)
		{
			awaitingModsData((MODS_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(MODS_DATA)));
		}
		if (awaitingModCategoryData != null)
		{
			awaitingModCategoryData((MODCATEGORIES_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(MODCATEGORIES_DATA)));
		}
		awaitingStaticData = null;
		awaitingConfigData = null;
		awaitingLOCData = null;
		awaitingAudioData = null;
		awaitingLootStructureData = null;
		awaitingLootPackageData = null;
		awaitingLootTableData = null;
		awaitingLootItemData = null;
		awaitingCrateInfoData = null;
		awaitingStoreData = null;
		awaitingStoreSectionsData = null;
		awaitingModsData = null;
		awaitingModCategoryData = null;
	}

	public void GetCPUDataAsync(global::System.Action<CPU_DATA.Root> returnCPUDataCallback)
	{
		returnCPUDataCallback((CPU_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(CPU_DATA)));
	}

	public void GetPlushSuitDataAsync(global::System.Action<PLUSHSUIT_DATA.Root> returnPlushSuitDataCallback)
	{
		returnPlushSuitDataCallback((PLUSHSUIT_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(PLUSHSUIT_DATA)));
	}

	public void GetAttackDataAsync(global::System.Action<ATTACK_DATA.Root> returnAttackDataCallback)
	{
		returnAttackDataCallback((ATTACK_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(ATTACK_DATA)));
	}

	public void GetStaticDataAsync(global::System.Action<STATIC_DATA.Root> returnStaticDataCallback)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnStaticDataCallback((STATIC_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(STATIC_DATA)));
		}
		else
		{
			awaitingStaticData = (global::System.Action<STATIC_DATA.Root>)global::System.Delegate.Combine(awaitingStaticData, returnStaticDataCallback);
		}
	}

	public void GetConfigDataEntryAsync(global::System.Action<CONFIG_DATA.Root> returnConfigDataEntryCallback)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnConfigDataEntryCallback((CONFIG_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(CONFIG_DATA)));
		}
		else
		{
			awaitingConfigData = (global::System.Action<CONFIG_DATA.Root>)global::System.Delegate.Combine(awaitingConfigData, returnConfigDataEntryCallback);
		}
	}

	public void GetLocDataAsync(global::System.Action<LOC_DATA.Root> returnLocDataCallback)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnLocDataCallback((LOC_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(LOC_DATA)));
		}
		else
		{
			awaitingLOCData = (global::System.Action<LOC_DATA.Root>)global::System.Delegate.Combine(awaitingLOCData, returnLocDataCallback);
		}
	}

	public void GetAudioDataAsync(global::System.Action<AUDIO_DATA.Root> returnAudioDataCallback)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnAudioDataCallback((AUDIO_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(AUDIO_DATA)));
		}
		else
		{
			awaitingAudioData = (global::System.Action<AUDIO_DATA.Root>)global::System.Delegate.Combine(awaitingAudioData, returnAudioDataCallback);
		}
	}

	public void GetLootStructureDataAsync(global::System.Action<LOOT_STRUCTURE_DATA.Root> returnLootStructureData)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnLootStructureData((LOOT_STRUCTURE_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(LOOT_STRUCTURE_DATA)));
		}
		else
		{
			awaitingLootStructureData = (global::System.Action<LOOT_STRUCTURE_DATA.Root>)global::System.Delegate.Combine(awaitingLootStructureData, returnLootStructureData);
		}
	}

	public void GetLootPackageDataAsync(global::System.Action<LOOT_PACKAGE_DATA.Root> returnLootPackageData)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnLootPackageData((LOOT_PACKAGE_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(LOOT_PACKAGE_DATA)));
		}
		else
		{
			awaitingLootPackageData = (global::System.Action<LOOT_PACKAGE_DATA.Root>)global::System.Delegate.Combine(awaitingLootPackageData, returnLootPackageData);
		}
	}

	public void GetLootTableDataAsync(global::System.Action<LOOT_TABLE_DATA.Root> returnLootTableData)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnLootTableData((LOOT_TABLE_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(LOOT_TABLE_DATA)));
		}
		else
		{
			awaitingLootTableData = (global::System.Action<LOOT_TABLE_DATA.Root>)global::System.Delegate.Combine(awaitingLootTableData, returnLootTableData);
		}
	}

	public void GetLootItemDataAsync(global::System.Action<LOOT_ITEM_DATA.Root> returnLootItemData)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnLootItemData((LOOT_ITEM_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(LOOT_ITEM_DATA)));
		}
		else
		{
			awaitingLootItemData = (global::System.Action<LOOT_ITEM_DATA.Root>)global::System.Delegate.Combine(awaitingLootItemData, returnLootItemData);
		}
	}

	public void GetCrateInfoAsync(global::System.Action<CRATE_INFO_DATA.Root> returnCrateInfoData)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnCrateInfoData((CRATE_INFO_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(CRATE_INFO_DATA)));
		}
		else
		{
			awaitingCrateInfoData = (global::System.Action<CRATE_INFO_DATA.Root>)global::System.Delegate.Combine(awaitingCrateInfoData, returnCrateInfoData);
		}
	}

	public void GetStoreDataAsync(global::System.Action<STORE_DATA.Root> returnStoreDataCallback)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnStoreDataCallback((STORE_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(STORE_DATA)));
		}
		else
		{
			awaitingStoreData = (global::System.Action<STORE_DATA.Root>)global::System.Delegate.Combine(awaitingStoreData, returnStoreDataCallback);
		}
	}

	public void GetStoreSectionDataAsync(global::System.Action<STORESECTIONS_DATA.Root> returnStoreSectionDataCallback)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnStoreSectionDataCallback((STORESECTIONS_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(STORESECTIONS_DATA)));
		}
		else
		{
			awaitingStoreSectionsData = (global::System.Action<STORESECTIONS_DATA.Root>)global::System.Delegate.Combine(awaitingStoreSectionsData, returnStoreSectionDataCallback);
		}
	}

	public void GetTrophyDataAsync(global::System.Action<TROPHY_DATA.Root> returnTrophyDataCallback)
	{
		returnTrophyDataCallback((TROPHY_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(TROPHY_DATA)));
	}

	public void GetProfileAvatarDataAsync(global::System.Action<PROFILE_AVATAR_DATA.Root> returnProfileAvatarData)
	{
		returnProfileAvatarData((PROFILE_AVATAR_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(PROFILE_AVATAR_DATA)));
	}

	public void GetModsDataAsync(global::System.Action<MODS_DATA.Root> returnModsDataCallback)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnModsDataCallback((MODS_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(MODS_DATA)));
		}
		else
		{
			awaitingModsData = (global::System.Action<MODS_DATA.Root>)global::System.Delegate.Combine(awaitingModsData, returnModsDataCallback);
		}
	}

	public void GetModCategoryDataAsync(global::System.Action<MODCATEGORIES_DATA.Root> returnModCategoriesDataCallback)
	{
		if (MasterDataHasBeenDeserialized)
		{
			returnModCategoriesDataCallback((MODCATEGORIES_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(MODCATEGORIES_DATA)));
		}
		else
		{
			awaitingModCategoryData = (global::System.Action<MODCATEGORIES_DATA.Root>)global::System.Delegate.Combine(awaitingModCategoryData, returnModCategoriesDataCallback);
		}
	}

	public void GetSubEntityDataAsync(global::System.Action<SUB_ENTITY_DATA.Root> returnSubEntityDataCallback)
	{
		returnSubEntityDataCallback((SUB_ENTITY_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(SUB_ENTITY_DATA)));
	}

	public void GetScavengingAttackDataAsync(global::System.Action<SCAVENGING_ATTACK_DATA.Root> returnScavengingAttackDataCallback)
	{
		returnScavengingAttackDataCallback((SCAVENGING_ATTACK_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(SCAVENGING_ATTACK_DATA)));
	}

	public void GetScavengingDataAsync(global::System.Action<SCAVENGING_DATA.Root> returnScavengingDataCallback)
	{
		returnScavengingDataCallback((SCAVENGING_DATA.Root)ConstantVariables.Instance.MasterDataDownloader.GetMasterDataDeserialized(typeof(SCAVENGING_DATA)));
	}
}
