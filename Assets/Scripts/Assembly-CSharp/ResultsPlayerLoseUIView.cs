public class ResultsPlayerLoseUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private ResultSequencer playerLoseSequence;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject returnButton;

	[global::UnityEngine.SerializeField]
	private NumberChanger streakNumberChanger;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI lostRemnantText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI currentRemnantText;

	[global::UnityEngine.SerializeField]
	private NumberChanger remnantNumberChanger;

	[global::UnityEngine.SerializeField]
	private bool shouldFinalRemnantAnimate;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI animatronicName;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI deathExplanationText;

	private MasterDomain _masterDomain;

	private ResultsDataModel _results;

	private BaseResultStep.ConditionalSettings _conditionalSettings;

	private string _animatronicAudioId;

	public void StartPlayerLoseSequence(AudioPlayer audioPlayer)
	{
		GetDataSources();
		if (ValidateDataSources())
		{
			SetDynamicData();
		}
		else
		{
			global::UnityEngine.Debug.LogError("ResultsPlayerLoseUIView StartPlayerLoseSequence - Missing necessary data to fully display losses. Using fallback data");
		}
		if (_masterDomain != null)
		{
			playerLoseSequence.StartSequence(_masterDomain.eventExposer, _masterDomain.GameAssetManagementDomain, audioPlayer, _animatronicAudioId, _conditionalSettings);
		}
	}

	public void ShowReturnButton()
	{
		returnButton.SetActive(value: true);
	}

	private void GetDataSources()
	{
		_masterDomain = MasterDomain.GetDomain();
		if (_masterDomain != null)
		{
			_results = _masterDomain.GameUIDomain.GameUIData.rewardDataModel;
		}
	}

	private bool ValidateDataSources()
	{
		string text;
		if (_masterDomain != null)
		{
			if (_results != null && _results.encounterResult != null && _results.rewardDataV3 != null)
			{
				return true;
			}
			text = "ResultsDataModel or one of its children are null";
		}
		else
		{
			text = "MasterDomain is null";
		}
		global::UnityEngine.Debug.LogError("ResultsPlayerLoseUIView ValidateDataSources - " + text);
		return false;
	}

	private void SetDynamicData()
	{
		BaseResultStep.ConditionalSettings conditionalSettings = new BaseResultStep.ConditionalSettings();
		conditionalSettings.StartingStreak = _results.encounterResult.OldCurrentStreak;
		conditionalSettings.AllowAds = false;
		_conditionalSettings = conditionalSettings;
		_animatronicAudioId = _results.encounterResult.AnimatronicAudioId;
		streakNumberChanger.InitValues(_results.encounterResult.OldCurrentStreak, _results.encounterResult.NewCurrentStreak);
		lostRemnantText.text = $"-{_results.rewardDataV3.essenceOnLoss}";
		currentRemnantText.text = $"{_results.encounterResult.CurrentRemnant}";
		int num = global::UnityEngine.Mathf.Max(_results.encounterResult.CurrentRemnant - _results.rewardDataV3.essenceOnLoss, 0);
		remnantNumberChanger.InitValues(shouldFinalRemnantAnimate ? _results.encounterResult.CurrentRemnant : num, num);
		animatronicName.SetText(LocalizationDomain.Instance.Localization.GetLocalizedString(_masterDomain.ItemDefinitionDomain.ItemDefinitions.GetPlushSuitById(_results.encounterResult.PlushSuitId).AnimatronicName, _masterDomain.ItemDefinitionDomain.ItemDefinitions.GetPlushSuitById(_results.encounterResult.PlushSuitId).AnimatronicName));
		deathExplanationText.SetText(LocalizationDomain.Instance.Localization.GetLocalizedString(_results.encounterResult.DeathText, _results.encounterResult.DeathText));
	}
}
