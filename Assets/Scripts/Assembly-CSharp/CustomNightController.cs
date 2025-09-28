public class CustomNightController : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Custom Night UI View")]
	[global::UnityEngine.SerializeField]
	private CustomNightUIView uiView;

	[global::UnityEngine.Header("RoomPositions")]
	public global::System.Collections.Generic.List<global::UnityEngine.Transform> roomTransforms;

	private global::System.Collections.Generic.List<CustomNightAnimatronicAI> animatronics = new global::System.Collections.Generic.List<CustomNightAnimatronicAI>();

	private SimpleTimer opportunityTimer = new SimpleTimer();

	private SimpleTimer hourTimer = new SimpleTimer();

	private int hour;

	public bool LeftDoorClosed;

	public bool RightDoorClosed;

	public CustomNightAnimatronic.CustomNightRooms currentCamera = CustomNightAnimatronic.CustomNightRooms.Stage;

	public bool camUp;

	private bool nightOver;

	private void Start()
	{
		opportunityTimer.Reset();
		hourTimer.Reset();
	}

	public void Begin(global::System.Collections.Generic.List<CustomNightAnimatronic> animDatas)
	{
		foreach (CustomNightAnimatronic animData in animDatas)
		{
			CustomNightAnimatronicAI item = new CustomNightAnimatronicAI(animData, this);
			animatronics.Add(item);
		}
		hour = 0;
		opportunityTimer.StartTimer(5f);
		hourTimer.StartTimer(60f);
		nightOver = false;
	}

	private void Update()
	{
		if (nightOver)
		{
			opportunityTimer.Reset();
			hourTimer.Reset();
			return;
		}
		if (opportunityTimer.Started && opportunityTimer.IsExpired())
		{
			opportunityTimer.Reset();
			UpdateOpportunities();
			opportunityTimer.StartTimer(5f);
		}
		if (!hourTimer.Started || !hourTimer.IsExpired())
		{
			return;
		}
		hourTimer.Reset();
		hour++;
		if (hour == 6)
		{
			FinishNight(didDie: false);
			return;
		}
		if (hour == 3)
		{
			UpdateAnimatronicsThreeAM();
		}
		hourTimer.StartTimer(60f);
	}

	private void UpdateOpportunities()
	{
		foreach (CustomNightAnimatronicAI animatronic in animatronics)
		{
			animatronic.MovementOpportunity();
		}
	}

	private void UpdateAnimatronicsThreeAM()
	{
		foreach (CustomNightAnimatronicAI animatronic in animatronics)
		{
			animatronic.ThreeAM();
		}
	}

	public void FinishNight(bool didDie)
	{
		opportunityTimer.Reset();
		hourTimer.Reset();
		nightOver = true;
	}

	public void FlipCam()
	{
		camUp = !camUp;
	}
}
