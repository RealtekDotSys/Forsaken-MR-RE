public class RewardDispatcher
{
	private EventExposer _masterEventExposer;

	private ServerDomain _serverDomain;

	private bool _shouldRequestRewards;

	public RewardDispatcher(EventExposer masterEventExposer, ServerDomain serverDomain)
	{
		_shouldRequestRewards = true;
		_masterEventExposer = masterEventExposer;
		_serverDomain = serverDomain;
	}

	public void Teardown()
	{
		_masterEventExposer = null;
		_serverDomain = null;
	}

	public void RequestRewards(AnimatronicEntity entity, bool encounterDefeated, global::System.Action onComplete, bool didUseJammer, bool leftEncounter)
	{
		if (!_shouldRequestRewards && !entity.entityId.ToLower().Contains("shadowbonnie"))
		{
			ExecuteFTUEEncounterRewardFlow(entity.endoskeletonData.cpu, encounterDefeated, onComplete);
			return;
		}
		if (encounterDefeated)
		{
			ReportWin(entity, onComplete, _serverDomain);
			return;
		}
		if (didUseJammer)
		{
			ReportJammer(entity, _serverDomain);
			onComplete?.Invoke();
			return;
		}
		if (leftEncounter)
		{
			ReportLeft(entity, _serverDomain);
			onComplete?.Invoke();
			return;
		}
		ReportLoss(entity, _serverDomain);
		if (onComplete != null)
		{
			CoroutineHelper.StartCoroutine(WaitAndCallComplete(onComplete));
		}
	}

	public global::System.Collections.IEnumerator WaitAndCallComplete(global::System.Action callback)
	{
		yield return new global::UnityEngine.WaitForSeconds(0.3f);
		callback();
		yield return null;
	}

	public static void ReportWin(AnimatronicEntity entity, global::System.Action onComplete, ServerDomain serverDomain)
	{
		global::UnityEngine.Debug.LogError("Reporting Won via RewardDispatcher");
		_ = entity.animatronicConfigData.CpuData;
		_ = entity.animatronicConfigData.PlushSuitData;
		serverDomain.mapEntityEncounterWonRequester.FinishEncounter(entity.entityId, onComplete);
	}

	public static void ReportLoss(AnimatronicEntity entity, ServerDomain serverDomain)
	{
		global::UnityEngine.Debug.LogError("Reporting Loss via RewardDispatcher");
		_ = entity.animatronicConfigData.CpuData;
		_ = entity.animatronicConfigData.PlushSuitData;
		serverDomain.mapEntityEncounterLostRequester.FinishEncounter(entity.entityId);
	}

	public static void ReportLeft(AnimatronicEntity entity, ServerDomain serverDomain)
	{
		global::UnityEngine.Debug.LogError("Reporting Left via RewardDispatcher");
		serverDomain.mapEntityEncounterLeftRequester.LeaveEncounter(entity.entityId);
	}

	public static void ReportJammer(AnimatronicEntity entity, ServerDomain serverDomain)
	{
		global::UnityEngine.Debug.LogError("Reporting Jammed via RewardDispatcher");
		serverDomain.mapEntityJamRequester.JamEntity(entity.entityId);
	}

	private void ExecuteFTUEEncounterRewardFlow(string cpuId, bool encounterDefeated, global::System.Action onComplete)
	{
		new global::System.Collections.Generic.List<LootRewardEntry>();
		if (cpuId.ToLower() != "bareendo_ftue" && cpuId.ToLower() != "freddyfazbear_basic")
		{
			global::UnityEngine.Debug.LogError("RewardDispatcher ExecuteFTUEEncounterRewardFlow - Invalid cpuId '" + cpuId + "'");
			return;
		}
		new LootRewardDisplayData();
		onComplete?.Invoke();
	}
}
