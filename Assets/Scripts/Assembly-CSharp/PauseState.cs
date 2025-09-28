public class PauseState
{
	public readonly PauseData Config;

	public global::UnityEngine.Vector3 SavedPosition;

	public global::UnityEngine.Vector3 SavedForward;

	public readonly SimpleTimer HaywireTimer;

	public readonly SimpleTimer SlashTimer;

	public void Reset()
	{
		SavedPosition = global::UnityEngine.Vector3.zero;
		SavedForward = global::UnityEngine.Vector3.forward;
		HaywireTimer.Reset();
		SlashTimer.Reset();
	}

	public PauseState(PauseData config)
	{
		HaywireTimer = new SimpleTimer();
		SlashTimer = new SimpleTimer();
		Config = config;
	}
}
