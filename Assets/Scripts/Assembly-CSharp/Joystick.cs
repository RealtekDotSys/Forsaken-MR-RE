public class Joystick : global::UnityEngine.MonoBehaviour, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IPointerUpHandler
{
	[global::UnityEngine.SerializeField]
	private float handleRange = 1f;

	[global::UnityEngine.SerializeField]
	private float deadZone;

	[global::UnityEngine.SerializeField]
	private AxisOptions axisOptions;

	[global::UnityEngine.SerializeField]
	private bool snapX;

	[global::UnityEngine.SerializeField]
	private bool snapY;

	[global::UnityEngine.SerializeField]
	protected global::UnityEngine.RectTransform background;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.RectTransform handle;

	private global::UnityEngine.RectTransform baseRect;

	private global::UnityEngine.Canvas canvas;

	private global::UnityEngine.Camera cam;

	private global::UnityEngine.Vector2 input = global::UnityEngine.Vector2.zero;

	public float Horizontal
	{
		get
		{
			if (!snapX)
			{
				return input.x;
			}
			return SnapFloat(input.x, AxisOptions.Horizontal);
		}
	}

	public float Vertical
	{
		get
		{
			if (!snapY)
			{
				return input.y;
			}
			return SnapFloat(input.y, AxisOptions.Vertical);
		}
	}

	public global::UnityEngine.Vector2 Direction => new global::UnityEngine.Vector2(Horizontal, Vertical);

	public float HandleRange
	{
		get
		{
			return handleRange;
		}
		set
		{
			handleRange = global::UnityEngine.Mathf.Abs(value);
		}
	}

	public float DeadZone
	{
		get
		{
			return deadZone;
		}
		set
		{
			deadZone = global::UnityEngine.Mathf.Abs(value);
		}
	}

	public AxisOptions AxisOptions
	{
		get
		{
			return AxisOptions;
		}
		set
		{
			axisOptions = value;
		}
	}

	public bool SnapX
	{
		get
		{
			return snapX;
		}
		set
		{
			snapX = value;
		}
	}

	public bool SnapY
	{
		get
		{
			return snapY;
		}
		set
		{
			snapY = value;
		}
	}

	protected virtual void Start()
	{
		HandleRange = handleRange;
		DeadZone = deadZone;
		baseRect = GetComponent<global::UnityEngine.RectTransform>();
		canvas = GetComponentInParent<global::UnityEngine.Canvas>();
		if (canvas == null)
		{
			global::UnityEngine.Debug.LogError("The Joystick is not placed inside a canvas");
		}
		global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(0.5f, 0.5f);
		background.pivot = vector;
		handle.anchorMin = vector;
		handle.anchorMax = vector;
		handle.pivot = vector;
		handle.anchoredPosition = global::UnityEngine.Vector2.zero;
	}

	public virtual void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		OnDrag(eventData);
	}

	public void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		cam = null;
		if (canvas.renderMode == global::UnityEngine.RenderMode.ScreenSpaceCamera)
		{
			cam = canvas.worldCamera;
		}
		global::UnityEngine.Vector2 vector = global::UnityEngine.RectTransformUtility.WorldToScreenPoint(cam, background.position);
		global::UnityEngine.Vector2 vector2 = background.sizeDelta / 2f;
		input = (eventData.position - vector) / (vector2 * canvas.scaleFactor);
		FormatInput();
		HandleInput(input.magnitude, input.normalized, vector2, cam);
		handle.anchoredPosition = input * vector2 * handleRange;
	}

	protected virtual void HandleInput(float magnitude, global::UnityEngine.Vector2 normalised, global::UnityEngine.Vector2 radius, global::UnityEngine.Camera cam)
	{
		if (magnitude > deadZone)
		{
			if (magnitude > 1f)
			{
				input = normalised;
			}
		}
		else
		{
			input = global::UnityEngine.Vector2.zero;
		}
	}

	private void FormatInput()
	{
		if (axisOptions == AxisOptions.Horizontal)
		{
			input = new global::UnityEngine.Vector2(input.x, 0f);
		}
		else if (axisOptions == AxisOptions.Vertical)
		{
			input = new global::UnityEngine.Vector2(0f, input.y);
		}
	}

	private float SnapFloat(float value, AxisOptions snapAxis)
	{
		if (value == 0f)
		{
			return value;
		}
		if (axisOptions == AxisOptions.Both)
		{
			float num = global::UnityEngine.Vector2.Angle(input, global::UnityEngine.Vector2.up);
			switch (snapAxis)
			{
			case AxisOptions.Horizontal:
				if (num < 22.5f || num > 157.5f)
				{
					return 0f;
				}
				return (value > 0f) ? 1 : (-1);
			case AxisOptions.Vertical:
				if (num > 67.5f && num < 112.5f)
				{
					return 0f;
				}
				return (value > 0f) ? 1 : (-1);
			default:
				return value;
			}
		}
		if (value > 0f)
		{
			return 1f;
		}
		if (value < 0f)
		{
			return -1f;
		}
		return 0f;
	}

	public virtual void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		input = global::UnityEngine.Vector2.zero;
		handle.anchoredPosition = global::UnityEngine.Vector2.zero;
	}

	protected global::UnityEngine.Vector2 ScreenPointToAnchoredPosition(global::UnityEngine.Vector2 screenPosition)
	{
		global::UnityEngine.Vector2 localPoint = global::UnityEngine.Vector2.zero;
		if (global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
		{
			global::UnityEngine.Vector2 vector = baseRect.pivot * baseRect.sizeDelta;
			return localPoint - background.anchorMax * baseRect.sizeDelta + vector;
		}
		return global::UnityEngine.Vector2.zero;
	}
}
