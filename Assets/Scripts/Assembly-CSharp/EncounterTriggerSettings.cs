public class EncounterTriggerSettings
{
	private string _damagedAction;

	private string _slashedAction;

	private string _shutdownAction;

	private string _preShutdownAction;

	public EncounterTriggerSettings(ATTACK_DATA.EncounterTriggerSettings rawSettings)
	{
		if (rawSettings.DamagedAction != null)
		{
			_damagedAction = rawSettings.DamagedAction;
		}
		if (rawSettings.SlashedAction != null)
		{
			_slashedAction = rawSettings.SlashedAction;
		}
		if (rawSettings.ShutdownAction != null)
		{
			global::UnityEngine.Debug.Log("adding shutdownaction " + rawSettings.ShutdownAction);
			_shutdownAction = rawSettings.ShutdownAction;
		}
		if (rawSettings.PreShutdownAction != null)
		{
			global::UnityEngine.Debug.Log("adding preshutdownaction " + rawSettings.PreShutdownAction);
			_preShutdownAction = rawSettings.PreShutdownAction;
			global::UnityEngine.Debug.Log(_preShutdownAction);
		}
	}

	private string GetTriggerActionForPhase(EncounterTrigger trigger)
	{
		string text = "Unset";
		switch (trigger)
		{
		case EncounterTrigger.PreShutdown:
			global::UnityEngine.Debug.Log("Returning PreShutdown action " + _preShutdownAction);
			text = _preShutdownAction;
			break;
		case EncounterTrigger.Shutdown:
			global::UnityEngine.Debug.Log("Returning Shutdown action " + _preShutdownAction);
			text = _shutdownAction;
			break;
		case EncounterTrigger.Damaged:
			text = _damagedAction;
			break;
		case EncounterTrigger.Slashed:
			text = _slashedAction;
			break;
		default:
			text = "None";
			break;
		}
		global::UnityEngine.Debug.Log("Returning action (" + text + ")");
		return text;
	}

	public int GetNextIndexForPhase(EncounterTrigger trigger, int currentIndex)
	{
		if (GetTriggerActionForPhase(trigger) == "ActivateNextAnimatronic")
		{
			return currentIndex + 1;
		}
		if (GetTriggerActionForPhase(trigger) == "ActivatePreviousAnimatronic")
		{
			return currentIndex - 1;
		}
		global::UnityEngine.Debug.Log("cannot get new index, trigger action is " + GetTriggerActionForPhase(trigger) + " trigger is " + trigger);
		return currentIndex;
	}
}
