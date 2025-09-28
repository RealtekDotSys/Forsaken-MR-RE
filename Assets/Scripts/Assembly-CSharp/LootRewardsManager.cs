public class LootRewardsManager
{
	private EventExposer _eventExposer;

	private bool _autoShow;

	private global::System.Collections.Generic.List<LootRewardDisplayData> _cachedLootRewardDisplayData;

	public bool AutoShow
	{
		set
		{
			_autoShow = value;
			if (_autoShow)
			{
				ShowCachedLootRewards(null);
			}
		}
	}

	public event global::System.Action OnLootRewardReceived;

	public bool HasCachedLootRewards()
	{
		return _cachedLootRewardDisplayData.Count > 0;
	}

	public int GetNumCachedLootRewards()
	{
		if (_cachedLootRewardDisplayData != null)
		{
			return _cachedLootRewardDisplayData.Count;
		}
		return 0;
	}

	public LootRewardsManager(EventExposer eventExposer)
	{
		_autoShow = false;
		_eventExposer = eventExposer;
		_cachedLootRewardDisplayData = new global::System.Collections.Generic.List<LootRewardDisplayData>();
		_eventExposer.add_LootRewardProcessed(OnLootRewardProcessed);
		_eventExposer.add_GameDisplayChange(OnGameDisplayChange);
	}

	public void Teardown()
	{
		_eventExposer.remove_LootRewardProcessed(OnLootRewardProcessed);
		_eventExposer.remove_GameDisplayChange(OnGameDisplayChange);
		_eventExposer = null;
		_cachedLootRewardDisplayData.Clear();
	}

	private void OnLootRewardProcessed(LootRewardDisplayData lootRewardDisplayData)
	{
		if (lootRewardDisplayData.rewards.Count == 0)
		{
			global::UnityEngine.Debug.LogError("No rewards in processed reward data..?");
			return;
		}
		lootRewardDisplayData.MergeByType("Parts");
		if (_autoShow)
		{
			global::UnityEngine.Debug.Log("auto showin");
			ShowLootRewards(lootRewardDisplayData);
		}
		else
		{
			global::UnityEngine.Debug.Log("adding loot rewards!");
			_cachedLootRewardDisplayData.Add(lootRewardDisplayData);
		}
	}

	public void ShowCachedLootRewards(global::System.Action onDismiss)
	{
		global::UnityEngine.Debug.Log("showing cached lot rewards");
		foreach (LootRewardDisplayData cachedLootRewardDisplayDatum in _cachedLootRewardDisplayData)
		{
			cachedLootRewardDisplayDatum.onDismissCallback = onDismiss;
			ShowLootRewards(cachedLootRewardDisplayDatum);
		}
		_cachedLootRewardDisplayData.Clear();
	}

	private void ShowLootRewards(LootRewardDisplayData lootRewardDisplayData)
	{
		_eventExposer.OnLootRewardDisplayRequestReceived(lootRewardDisplayData);
		this.OnLootRewardReceived?.Invoke();
	}

	private void OnGameDisplayChange(GameDisplayData gameDisplayData)
	{
		if (gameDisplayData.currentDisplay == GameDisplayData.DisplayType.camera || gameDisplayData.currentDisplay == GameDisplayData.DisplayType.scavengingui)
		{
			return;
		}
		bool flag = gameDisplayData.currentDisplay != GameDisplayData.DisplayType.results;
		if (flag != _autoShow)
		{
			_autoShow = flag;
			if (_autoShow)
			{
				ShowCachedLootRewards(null);
			}
		}
	}
}
