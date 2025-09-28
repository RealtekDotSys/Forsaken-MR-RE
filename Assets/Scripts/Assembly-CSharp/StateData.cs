[global::System.Serializable]
public class StateData
{
	[global::System.Serializable]
	public enum AnimatronicState
	{
		NoState = -1,
		FarAway = 0,
		StalkingPlayer = 1,
		ApproachingPlayer = 2,
		NearPlayer = 3,
		SameRoom = 4,
		BackToWorkshop = 5,
		Destroy = 6,
		OnDeck = 7,
		WaitingForMovementNodes = 8,
		MoveToStalkingRange = 9,
		Scavenging = 10,
		Recall = 11,
		SentAway = 12,
		Blink = 13,
		Travel = 14,
		ScavengeAppointment = 15,
		COUNT = 16
	}

	private static StateData.AnimatronicState[] beforeAttackStates;

	private static StateData.AnimatronicState[] afterStalkingStates;

	public StateData.AnimatronicState animatronicState { get; set; }

	public bool expressDelivery { get; set; }

	public static bool IsInState(StateData.AnimatronicState sourceState, StateData.AnimatronicState stateToCheck)
	{
		return sourceState == stateToCheck;
	}

	public static bool IsInStates(StateData.AnimatronicState sourceState, StateData.AnimatronicState[] statesToCheck)
	{
		for (int i = 0; i < statesToCheck.Length; i++)
		{
			if (statesToCheck[i] == sourceState)
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsBeforeAttack(StateData.AnimatronicState sourceState)
	{
		return IsInStates(sourceState, beforeAttackStates);
	}

	public static bool IsAfterStalking(StateData.AnimatronicState sourceState)
	{
		return IsInStates(sourceState, afterStalkingStates);
	}

	public StateData()
	{
		animatronicState = StateData.AnimatronicState.FarAway;
		expressDelivery = false;
	}

	public StateData(StateData stateData)
	{
		animatronicState = stateData.animatronicState;
		expressDelivery = stateData.expressDelivery;
	}

	public StateData(StateData.AnimatronicState animatronicState, bool expressDelivery)
	{
		this.animatronicState = animatronicState;
		this.expressDelivery = expressDelivery;
	}

	public override string ToString()
	{
		return animatronicState.ToString();
	}

	static StateData()
	{
		beforeAttackStates = new StateData.AnimatronicState[3]
		{
			StateData.AnimatronicState.StalkingPlayer,
			StateData.AnimatronicState.ApproachingPlayer,
			StateData.AnimatronicState.NearPlayer
		};
		afterStalkingStates = new StateData.AnimatronicState[2]
		{
			StateData.AnimatronicState.ApproachingPlayer,
			StateData.AnimatronicState.NearPlayer
		};
	}
}
