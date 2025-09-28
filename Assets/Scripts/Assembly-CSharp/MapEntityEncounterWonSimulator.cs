public static class MapEntityEncounterWonSimulator
{
	public static void FinishEncounter(global::System.Action onComplete)
	{
		LootRewardDisplayData lootRewardDisplayData = new LootRewardDisplayData();
		lootRewardDisplayData.crateInfo = MasterDomain.GetDomain().LootDomain.LootContainer.GetCrateInfoForId("RewardInfo_Bronze");
		lootRewardDisplayData.rewards = new global::System.Collections.Generic.List<LootRewardEntry>();
		LootRewardEntry lootRewardEntry = new LootRewardEntry();
		lootRewardEntry.LootItem = MasterDomain.GetDomain().LootDomain.LootContainer.GetLootItemForId("Loot_Parts");
		lootRewardEntry.NumToGive = 15;
		lootRewardDisplayData.rewards.Add(lootRewardEntry);
		LootRewardEntry lootRewardEntry2 = new LootRewardEntry();
		lootRewardEntry2.LootItem = MasterDomain.GetDomain().LootDomain.LootContainer.GetLootItemForId("Loot_Remnant");
		lootRewardEntry2.NumToGive = 28;
		lootRewardDisplayData.rewards.Add(lootRewardEntry2);
		LootRewardEntry lootRewardEntry3 = new LootRewardEntry();
		lootRewardEntry3.LootItem = MasterDomain.GetDomain().LootDomain.LootContainer.GetLootItemForId("FreddyFazbear_PlushSuit");
		lootRewardEntry3.NumToGive = 1;
		lootRewardDisplayData.rewards.Add(lootRewardEntry3);
		LootRewardEntry lootRewardEntry4 = new LootRewardEntry();
		lootRewardEntry4.LootItem = MasterDomain.GetDomain().LootDomain.LootContainer.GetLootItemForId("Loot_XP");
		lootRewardEntry4.NumToGive = 17;
		lootRewardDisplayData.rewards.Add(lootRewardEntry4);
		global::UnityEngine.Debug.Log("Sharing processed loot data");
		MasterDomain.GetDomain().eventExposer.OnLootRewardProcessed(lootRewardDisplayData);
		CoroutineHelper.StartCoroutine(WaitAndCallback(onComplete));
	}

	public static global::System.Collections.IEnumerator WaitAndCallback(global::System.Action completeCallback)
	{
		yield return new global::UnityEngine.WaitForSeconds(3f);
		completeCallback();
		yield return null;
	}
}
