public class VariableJoystick : Joystick
{
	[global::UnityEngine.SerializeField]
	private float moveThreshold = 1f;

	[global::UnityEngine.SerializeField]
	private JoystickType joystickType;

	private global::UnityEngine.Vector2 fixedPosition = global::UnityEngine.Vector2.zero;

	public float MoveThreshold
	{
		get
		{
			return moveThreshold;
		}
		set
		{
			moveThreshold = global::UnityEngine.Mathf.Abs(value);
		}
	}

	public void SetMode(JoystickType joystickType)
	{
		this.joystickType = joystickType;
		if (joystickType == JoystickType.Fixed)
		{
			background.anchoredPosition = fixedPosition;
			background.gameObject.SetActive(value: true);
		}
		else
		{
			background.gameObject.SetActive(value: false);
		}
	}

	protected override void Start()
	{
		base.Start();
		fixedPosition = background.anchoredPosition;
		SetMode(joystickType);
	}

	public override void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		if (joystickType != JoystickType.Fixed)
		{
			background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
			background.gameObject.SetActive(value: true);
		}
		base.OnPointerDown(eventData);
	}

	public override void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		if (joystickType != JoystickType.Fixed)
		{
			background.gameObject.SetActive(value: false);
		}
		base.OnPointerUp(eventData);
	}

	protected override void HandleInput(float magnitude, global::UnityEngine.Vector2 normalised, global::UnityEngine.Vector2 radius, global::UnityEngine.Camera cam)
	{
		if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
		{
			global::UnityEngine.Vector2 vector = normalised * (magnitude - moveThreshold) * radius;
			background.anchoredPosition += vector;
		}
		base.HandleInput(magnitude, normalised, radius, cam);
	}
}
