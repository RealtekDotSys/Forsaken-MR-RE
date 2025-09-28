public class CrateRewardResponseHandler : EventResponseHandler
{
	private global::System.Func<global::System.Collections.Generic.List<ServerData>, global::System.Collections.Generic.List<LootRewardEntry>> ProcessRewards;

	private global::System.Action<LootRewardDisplayData> LootRewardProcessed;

	public void Setup(global::System.Action<LootRewardDisplayData> callback)
	{
		LootRewardProcessed = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerData("CrateRewards") != null)
		{
			HandleResponse(data.GetServerData("CrateRewards"));
		}
	}

	private void HandleResponse(ServerData data)
	{
		LootRewardDisplayData lootRewardDisplayData = new LootRewardDisplayData();
		if (!string.IsNullOrEmpty(data.GetString("CrateInfo")))
		{
			lootRewardDisplayData.crateInfo = MasterDomain.GetDomain().LootDomain.LootContainer.GetCrateInfoForId(data.GetString("CrateInfo"));
		}
		else
		{
			lootRewardDisplayData.crateInfo = MasterDomain.GetDomain().LootDomain.LootContainer.GetCrateInfoForId("RewardInfo_Bronze");
		}
		global::System.Collections.Generic.List<ServerData> serverDataList = data.GetServerDataList("RewardList");
		global::UnityEngine.Debug.LogError("Reward list count: " + serverDataList.Count);
		lootRewardDisplayData.rewards = ProcessLootRewards(serverDataList);
		LootRewardProcessed(lootRewardDisplayData);
	}

	private global::System.Collections.Generic.List<LootRewardEntry> ProcessLootRewards(global::System.Collections.Generic.List<ServerData> data)
	{
		global::System.Collections.Generic.List<LootRewardEntry> list = new global::System.Collections.Generic.List<LootRewardEntry>();
		foreach (ServerData datum in data)
		{
			LootRewardEntry lootRewardEntry = new LootRewardEntry();
			ServerData serverData = datum.GetServerData("LootItem");
			LootItemData lootItemData = new LootItemData(serverData.GetString("Logical"), serverData.GetString("Type"), serverData.GetString("Subtype"));
			global::UnityEngine.Debug.Log(lootItemData.Logical + " - " + lootItemData.Type);
			lootRewardEntry.LootItem = lootItemData;
			lootRewardEntry.NumToGive = datum.GetInt("NumToGive").Value;
			list.Add(lootRewardEntry);
		}
		return list;
	}
}
