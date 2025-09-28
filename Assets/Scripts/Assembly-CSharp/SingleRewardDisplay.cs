public class SingleRewardDisplay : global::UnityEngine.MonoBehaviour, IUISequence
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image rewardIcon;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI rewardTitle;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI rewardNumber;

	[global::UnityEngine.SerializeField]
	private string audioBeginEventName;

	[global::UnityEngine.SerializeField]
	private string audioEndEventName;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> stars;

	private MasterDomain masterDomain;

	private UISequenceData uiSequenceData;

	private RewardSequenceStatus status;

	private readonly StatusType[] statusTypes;

	private global::System.Collections.Generic.Queue<ItemUIDescription> rewardQueue;

	private RewardV3Translator rewardV3Translator;

	private bool singleRewardSequenceComplete;

	private bool showingSubSequence;

	private bool receivedResults;

	private void EventExposer_RewardsReceived(RewardData obj)
	{
		masterDomain.eventExposer.remove_RewardsReceived(EventExposer_RewardsReceived);
		BuildRewardQueue(masterDomain.GameUIDomain.GameUIData.rewardDataModel.rewardDataV3);
	}

	private void AddListToQueue(global::System.Collections.Generic.Queue<ItemUIDescription> baseQueue, global::System.Collections.Generic.List<ItemUIDescription> incomingList)
	{
		foreach (ItemUIDescription incoming in incomingList)
		{
			baseQueue.Enqueue(incoming);
		}
	}

	private void BuildRewardQueue(RewardDataV3 rewardDataV3)
	{
		EncounterResult encounterResult = masterDomain.GameUIDomain.GameUIData.rewardDataModel.encounterResult;
		if (rewardV3Translator != null)
		{
			AddListToQueue(incomingList: encounterResult.PlayerDidWin ? rewardV3Translator.GetAllWinRewardDescriptions(rewardDataV3) : rewardV3Translator.GetEssenceOnLossCellDescriptions(rewardDataV3), baseQueue: rewardQueue);
			receivedResults = true;
		}
	}

	private void UpdateDisplayFromItemUIDefinition(ItemUIDescription itemUiDescription)
	{
		rewardIcon.sprite = itemUiDescription.sprite;
		rewardTitle.text = itemUiDescription.displayName;
		rewardNumber.text = itemUiDescription.number.ToString();
		foreach (global::UnityEngine.GameObject star in stars)
		{
			star.SetActive(stars.IndexOf(star) < itemUiDescription.magnitude);
		}
	}

	private void ShowSubSequence()
	{
		UpdateDisplayFromItemUIDefinition(rewardQueue.Dequeue());
		base.gameObject.SetActive(value: true);
		showingSubSequence = true;
		if (uiSequenceData != null)
		{
			uiSequenceData.AudioPlayer.RaiseGameEventForModeWithOverrideByName(audioBeginEventName, uiSequenceData.AnimatronicAudioId, AudioMode.Camera);
		}
	}

	private void CloseOutSubSequence()
	{
		status.ResetClick();
		status.RestartTimer();
		base.gameObject.SetActive(value: false);
		showingSubSequence = false;
		if (uiSequenceData != null)
		{
			uiSequenceData.AudioPlayer.RaiseGameEventForModeWithOverrideByName(audioEndEventName, uiSequenceData.AnimatronicAudioId, AudioMode.Camera);
		}
	}

	void IUISequence.StartSequence(UISequenceData data)
	{
		uiSequenceData = data;
		masterDomain = MasterDomain.GetDomain();
		singleRewardSequenceComplete = false;
		showingSubSequence = false;
		status = new RewardSequenceStatus(base.gameObject);
		rewardQueue = new global::System.Collections.Generic.Queue<ItemUIDescription>();
		rewardV3Translator = new RewardV3Translator(masterDomain);
		if (!masterDomain.GameUIDomain.GameUIData.rewardDataModel.resultDirty)
		{
			masterDomain.eventExposer.add_RewardsReceived(EventExposer_RewardsReceived);
		}
		else
		{
			BuildRewardQueue(masterDomain.GameUIDomain.GameUIData.rewardDataModel.rewardDataV3);
		}
	}

	void IUISequence.UpdateSequence()
	{
		if (receivedResults)
		{
			if (!showingSubSequence && rewardQueue.Count <= 0)
			{
				singleRewardSequenceComplete = true;
			}
			if (!singleRewardSequenceComplete)
			{
				EvaluateNextSequence();
			}
		}
	}

	private bool EvaluateNextSequence()
	{
		if (showingSubSequence)
		{
			if (!status.AllComplete(statusTypes))
			{
				return false;
			}
			CloseOutSubSequence();
			return false;
		}
		ShowSubSequence();
		return false;
	}

	bool IUISequence.IsSequenceDone()
	{
		return singleRewardSequenceComplete;
	}

	void IUISequence.TeardownSequence()
	{
		if (rewardQueue != null)
		{
			rewardQueue.Clear();
		}
		rewardQueue = null;
		CloseOutSubSequence();
	}

	public SingleRewardDisplay()
	{
		statusTypes = new StatusType[3]
		{
			StatusType.anim,
			StatusType.timer,
			StatusType.click
		};
	}
}
