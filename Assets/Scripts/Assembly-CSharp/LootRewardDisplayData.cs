public class LootRewardDisplayData
{
	public CrateInfoData crateInfo;

	public global::System.Collections.Generic.List<LootRewardEntry> rewards;

	public global::System.Action onDismissCallback;

	public bool levelUp;

	public void MergeByType(string lootType)
	{
		if (rewards == null || rewards.Count < 1)
		{
			return;
		}
		int num = 0;
		foreach (LootRewardEntry reward in rewards)
		{
			if (reward.LootItem.Type == lootType)
			{
				num++;
			}
		}
		int num2 = 0;
		foreach (LootRewardEntry reward2 in rewards)
		{
			if (reward2.LootItem.Type == lootType)
			{
				if (num2 != num - 1)
				{
					rewards.Remove(reward2);
					num2++;
				}
				else
				{
					reward2.NumToGive += num2;
				}
			}
		}
	}
}
