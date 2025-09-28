public class TimerDisplay : global::UnityEngine.MonoBehaviour
{
	public enum TimerDisplayState
	{
		Active = 0,
		Inactive = 1,
		Hidden = 2,
		Flashing = 3
	}

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Settings")]
	[global::UnityEngine.Tooltip("Hide text or will continue flashing red")]
	public bool HideWhenZero;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Tooltip("In Seconds")]
	public float TimeToStartFlashing;

	[global::UnityEngine.SerializeField]
	public float FlashInterval;

	[global::UnityEngine.SerializeField]
	public bool FlashWhenComplete = true;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Hookups")]
	public global::UnityEngine.GameObject HideTimerObject;

	[global::UnityEngine.SerializeField]
	public global::TMPro.TextMeshProUGUI DisplayTimeText;

	[global::UnityEngine.HideInInspector]
	public long EndTime;

	[global::UnityEngine.HideInInspector]
	public TimerDisplay.TimerDisplayState CurrentState;

	private EventExposer _eventExposer;

	private void Awake()
	{
		_eventExposer = MasterDomain.GetDomain().eventExposer;
	}

	private void OnDestroy()
	{
		_ = _eventExposer;
		_eventExposer = null;
	}

	public TimerDisplay()
	{
		TimeToStartFlashing = 3.190147E+38f;
		FlashInterval = 0.5f;
		FlashWhenComplete = true;
	}
}
