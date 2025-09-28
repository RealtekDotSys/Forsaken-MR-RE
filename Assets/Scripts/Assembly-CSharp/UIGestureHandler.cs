public class UIGestureHandler : global::UnityEngine.MonoBehaviour, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IBeginDragHandler
{
	public enum Mode
	{
		None = 0,
		Drag = 1,
		Pinch = 2,
		Rotate = 3
	}

	public enum Touches
	{
		One = 1,
		Two = 2
	}

	private const float DEFAULT_MOVE_SPEED = 0.25f;

	private const float DEFAULT_SCALE_SPEED = 0.5f;

	private const float DEFAULT_MIN_ROTATE_GESTUREANGLE = 6f;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<UIGestureHandler.Mode> _modes;

	[global::UnityEngine.SerializeField]
	private float _moveSpeed = 0.25f;

	[global::UnityEngine.SerializeField]
	private float _scaleSpeed;

	[global::UnityEngine.SerializeField]
	private float _minRotateGestureAngle = 6f;

	private MasterDomain _masterDomain;

	private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector2> _prevTouchesList;

	private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector2> _currTouchesList;

	private UIGestureHandler.Mode _currTouchMode;

	public global::System.Collections.Generic.List<UIGestureHandler.Mode> Modes => _modes;

	public float MoveSpeed => _moveSpeed;

	public float ScaleSpeed => _scaleSpeed;

	private void Start()
	{
		_masterDomain = MasterDomain.GetDomain();
		_masterDomain.eventExposer.add_GestureResetEvent(GestureResetEventHandler);
	}

	private void OnDestroy()
	{
		_masterDomain.eventExposer.remove_GestureResetEvent(GestureResetEventHandler);
	}

	private void Update()
	{
		if (global::UnityEngine.Input.touchCount < 1)
		{
			_currTouchMode = UIGestureHandler.Mode.None;
		}
	}

	public void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();
		_prevTouchesList.Clear();
		global::UnityEngine.Touch[] touches = global::UnityEngine.Input.touches;
		foreach (global::UnityEngine.Touch touch in touches)
		{
			_prevTouchesList.Add(touch.position);
		}
	}

	public void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		global::System.Collections.Generic.List<global::UnityEngine.Vector2> list = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();
		global::UnityEngine.Touch[] touches = global::UnityEngine.Input.touches;
		foreach (global::UnityEngine.Touch touch in touches)
		{
			list.Add(touch.position);
		}
		if (list.Count == 1)
		{
			_currTouchesList.Clear();
			_currTouchesList.Add(list[0]);
			_masterDomain.eventExposer.OnGestureTouchEvent(_prevTouchesList, _currTouchesList);
			_prevTouchesList.Clear();
			_prevTouchesList.Add(_currTouchesList[0]);
		}
		else if (list.Count == 0)
		{
			_currTouchesList.Clear();
			_currTouchesList.Add(global::UnityEngine.Input.mousePosition);
			_masterDomain.eventExposer.OnGestureTouchEvent(_prevTouchesList, _currTouchesList);
			_prevTouchesList.Clear();
			_prevTouchesList.Add(_currTouchesList[0]);
		}
		else if (list.Count == 2)
		{
			_currTouchesList.Clear();
			_currTouchesList.Add(list[0]);
			_currTouchesList.Add(list[1]);
			_masterDomain.eventExposer.OnGestureTouchEvent(_prevTouchesList, _currTouchesList);
			_prevTouchesList.Clear();
			_prevTouchesList.Add(_currTouchesList[0]);
			_prevTouchesList.Add(_currTouchesList[1]);
		}
	}

	private void ResetTouchMode()
	{
		_currTouchMode = UIGestureHandler.Mode.None;
	}

	private void ClearTouches()
	{
		_currTouchesList.Clear();
		_prevTouchesList.Clear();
	}

	private void GestureResetEventHandler()
	{
		_currTouchMode = UIGestureHandler.Mode.None;
		ClearTouches();
	}

	public UIGestureHandler()
	{
		_modes = new global::System.Collections.Generic.List<UIGestureHandler.Mode>();
		_moveSpeed = 1f;
		_scaleSpeed = 0.5f;
		_minRotateGestureAngle = 6f;
		_prevTouchesList = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();
		_currTouchesList = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();
	}
}
