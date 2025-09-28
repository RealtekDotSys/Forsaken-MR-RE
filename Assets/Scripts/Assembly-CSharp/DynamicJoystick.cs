public class DynamicJoystick : Joystick
{
	[global::UnityEngine.SerializeField]
	private float moveThreshold = 1f;

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

	protected override void Start()
	{
		MoveThreshold = moveThreshold;
		base.Start();
		background.gameObject.SetActive(value: false);
	}

	public override void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
		background.gameObject.SetActive(value: true);
		base.OnPointerDown(eventData);
	}

	public override void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		background.gameObject.SetActive(value: false);
		base.OnPointerUp(eventData);
	}

	protected override void HandleInput(float magnitude, global::UnityEngine.Vector2 normalised, global::UnityEngine.Vector2 radius, global::UnityEngine.Camera cam)
	{
		if (magnitude > moveThreshold)
		{
			global::UnityEngine.Vector2 vector = normalised * (magnitude - moveThreshold) * radius;
			background.anchoredPosition += vector;
		}
		base.HandleInput(magnitude, normalised, radius, cam);
	}
}
