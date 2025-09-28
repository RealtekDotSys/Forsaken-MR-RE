public class ResultsPlayerWinUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private ResultSequencer playerWinSequence;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button returnButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject shortcutButton;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI oldStreakNumberText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI newStreakNumberText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI bestStreakNumberText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI animatronicName;

	private MasterDomain _masterDomain;

	private ResultsDataModel _results;

	private BaseResultStep.ConditionalSettings _conditionalSettings;

	private string _animatronicAudioId;

	private global::System.Action _onDismiss;

	public void Show(string buttonText, global::System.Action onDismiss)
	{
		global::TMPro.TextMeshProUGUI[] componentsInChildren = returnButton.GetComponentsInChildren<global::TMPro.TextMeshProUGUI>(includeInactive: true);
		if (componentsInChildren != null && componentsInChildren.Length != 0 && componentsInChildren[0] != null)
		{
			componentsInChildren[0].text = buttonText;
		}
		_onDismiss = onDismiss;
		returnButton.onClick.RemoveAllListeners();
		returnButton.onClick.AddListener(OnSequenceComplete);
		base.gameObject.SetActive(value: true);
	}

	public void StartPlayerWinSequence(AudioPlayer audioPlayer, bool allowShortcut, bool allowAds)
	{
		if (shortcutButton != null)
		{
			shortcutButton.SetActive(allowShortcut);
		}
		GetDataSources();
		if (ValidateDataSources())
		{
			SetDynamicData(allowAds);
		}
		else
		{
			global::UnityEngine.Debug.LogError("ResultsPlayerWinUIView StartPlayerWinSequence -Missing necessary data to fully display rewards. Using fallback data");
		}
		if (_onDismiss != null)
		{
			playerWinSequence.OnSequenceComplete = OnSequenceComplete;
		}
		if (_masterDomain != null)
		{
			global::UnityEngine.Debug.Log("BEGINNING PLAYER WIN RESULTS SEQUENCER");
			playerWinSequence.StartSequence(_masterDomain.eventExposer, _masterDomain.GameAssetManagementDomain, audioPlayer, _animatronicAudioId, _conditionalSettings);
		}
	}

	public void ShowReturnButton()
	{
		returnButton.gameObject.SetActive(value: true);
	}

	private void GetDataSources()
	{
		_masterDomain = MasterDomain.GetDomain();
		if (_masterDomain != null)
		{
			_results = _masterDomain.GameUIDomain.GameUIData.rewardDataModel;
			if (_results != null)
			{
				_ = _results.encounterResult;
			}
		}
	}

	private bool ValidateDataSources()
	{
		if (_masterDomain == null)
		{
			global::UnityEngine.Debug.LogError("ResultsPlayerWinUIView ValidateDataSources - MasterDomain is null");
			return false;
		}
		if (_results == null || _results.encounterResult == null)
		{
			global::UnityEngine.Debug.LogError("ResultsPlayerWinUIView ValidateDataSources - ResultsDataModel or one of its children are null");
			return false;
		}
		if ((_results == null || _results.rewardDataV3 == null) && !_masterDomain.LootDomain.LootRewardsManager.HasCachedLootRewards())
		{
			global::UnityEngine.Debug.LogError("ResultsPlayerWinUIView ValidateDataSources - ResultsDataModel or one of its children are null");
			return false;
		}
		return true;
	}

	private void SetDynamicData(bool allowAds)
	{
		RewardV3Translator rewardV3Translator = new RewardV3Translator(_masterDomain);
		global::System.Collections.Generic.List<ItemUIDescription> list = new global::System.Collections.Generic.List<ItemUIDescription>();
		if (_results.rewardDataV3 != null)
		{
			list = rewardV3Translator.GetAllWinRewardDescriptions(_results.rewardDataV3);
		}
		new global::System.Collections.Generic.Dictionary<string, int>();
		EncounterResult encounterResult = _results.encounterResult;
		EncounterResult encounterResult2 = _results.encounterResult;
		_conditionalSettings = new BaseResultStep.ConditionalSettings();
		_conditionalSettings.IsNewBestStreak = encounterResult.NewBestStreak > encounterResult2.OldBestStreak;
		_conditionalSettings.NumRewards = list.Count;
		_conditionalSettings.AllowAds = allowAds;
		_conditionalSettings.NumRewardCrates = _masterDomain.LootDomain.LootRewardsManager.GetNumCachedLootRewards();
		_animatronicAudioId = _results.encounterResult.AnimatronicAudioId;
		oldStreakNumberText.text = _results.encounterResult.OldCurrentStreak.ToString();
		newStreakNumberText.text = _results.encounterResult.NewCurrentStreak.ToString();
		bestStreakNumberText.text = _results.encounterResult.NewBestStreak.ToString();
		animatronicName.SetText(_masterDomain.ItemDefinitionDomain.ItemDefinitions.GetPlushSuitById(_results.encounterResult.PlushSuitId).AnimatronicName);
		_ = _conditionalSettings.IsNewBestStreak;
	}

	private void SetFallbackData()
	{
	}

	private void OnSequenceComplete()
	{
		if (_onDismiss != null)
		{
			_onDismiss();
		}
	}
}
