public class StreakRewardDisplay : global::UnityEngine.MonoBehaviour, IUISequence
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject newStreakParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject maxStreakParent;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI newStreakNum;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI maxStreakNum;

	[global::UnityEngine.SerializeField]
	private string audioBeginEventName;

	[global::UnityEngine.SerializeField]
	private string audioEndEventName;

	private UISequenceData uiSequenceData;

	private RewardSequenceStatus status;

	private readonly StatusType[] statusTypes;

	void IUISequence.StartSequence(UISequenceData data)
	{
		uiSequenceData = data;
		status = new RewardSequenceStatus(base.gameObject);
		SetDisplayData();
		base.gameObject.SetActive(value: true);
		if (uiSequenceData != null)
		{
			uiSequenceData.AudioPlayer.RaiseGameEventForModeWithOverrideByName(audioBeginEventName, uiSequenceData.AnimatronicAudioId, AudioMode.Camera);
		}
	}

	private void SetDisplayData()
	{
		MasterDomain domain = MasterDomain.GetDomain();
		if (domain.GameUIDomain.GameUIData.rewardDataModel != null)
		{
			int num;
			global::TMPro.TextMeshProUGUI textMeshProUGUI;
			if (domain.GameUIDomain.GameUIData.rewardDataModel.encounterResult.OldBestStreak < domain.GameUIDomain.GameUIData.rewardDataModel.encounterResult.NewBestStreak)
			{
				newStreakParent.SetActive(value: true);
				num = domain.GameUIDomain.GameUIData.rewardDataModel.encounterResult.NewBestStreak;
				textMeshProUGUI = newStreakNum;
			}
			else
			{
				maxStreakParent.SetActive(value: true);
				num = domain.GameUIDomain.GameUIData.rewardDataModel.encounterResult.NewCurrentStreak;
				textMeshProUGUI = maxStreakNum;
			}
			string text = num.ToString();
			if (textMeshProUGUI != null)
			{
				textMeshProUGUI.text = text;
			}
		}
	}

	void IUISequence.UpdateSequence()
	{
	}

	bool IUISequence.IsSequenceDone()
	{
		if (status != null)
		{
			return status.AllComplete(statusTypes);
		}
		return false;
	}

	void IUISequence.TeardownSequence()
	{
		base.gameObject.SetActive(value: false);
		if (uiSequenceData != null)
		{
			uiSequenceData.AudioPlayer.RaiseGameEventForModeWithOverrideByName(audioEndEventName, uiSequenceData.AnimatronicAudioId, AudioMode.Camera);
		}
	}

	public StreakRewardDisplay()
	{
		statusTypes = new StatusType[3]
		{
			StatusType.anim,
			StatusType.timer,
			StatusType.audio
		};
	}
}
