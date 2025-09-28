public class RewardDataV3
{
	public global::System.Collections.Generic.List<RewardItem> modRewardList;

	public global::System.Collections.Generic.List<RewardItem> currencyRewardList;

	public global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<RewardItem>> animatronicRewardTable;

	public global::System.Collections.Generic.List<RewardItem> cpuRewardList;

	public int essenceOnLoss;

	public RewardDataV3()
	{
		modRewardList = new global::System.Collections.Generic.List<RewardItem>();
		currencyRewardList = new global::System.Collections.Generic.List<RewardItem>();
		animatronicRewardTable = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<RewardItem>>();
		cpuRewardList = new global::System.Collections.Generic.List<RewardItem>();
	}

	public RewardDataV3(RewardDataV3 rewardDataV3)
	{
		modRewardList = new global::System.Collections.Generic.List<RewardItem>();
		currencyRewardList = new global::System.Collections.Generic.List<RewardItem>();
		animatronicRewardTable = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<RewardItem>>();
		cpuRewardList = new global::System.Collections.Generic.List<RewardItem>();
		foreach (RewardItem modReward in rewardDataV3.modRewardList)
		{
			modRewardList.Add(modReward);
		}
		foreach (RewardItem currencyReward in rewardDataV3.currencyRewardList)
		{
			currencyRewardList.Add(currencyReward);
		}
		foreach (RewardItem cpuReward in rewardDataV3.cpuRewardList)
		{
			cpuRewardList.Add(cpuReward);
		}
		foreach (string key in rewardDataV3.animatronicRewardTable.Keys)
		{
			animatronicRewardTable.Add(key, rewardDataV3.animatronicRewardTable[key]);
		}
		essenceOnLoss = rewardDataV3.essenceOnLoss;
	}
}
