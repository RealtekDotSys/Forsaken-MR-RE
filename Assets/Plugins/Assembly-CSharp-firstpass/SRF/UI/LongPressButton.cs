namespace SRF.UI
{
	[global::UnityEngine.AddComponentMenu("SRF/UI/Long Press Button")]
	public class LongPressButton : global::UnityEngine.UI.Button
	{
		private bool _handled;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Button.ButtonClickedEvent _onLongPress = new global::UnityEngine.UI.Button.ButtonClickedEvent();

		private bool _pressed;

		private float _pressedTime;

		public float LongPressDuration = 0.9f;

		public global::UnityEngine.UI.Button.ButtonClickedEvent onLongPress
		{
			get
			{
				return _onLongPress;
			}
			set
			{
				_onLongPress = value;
			}
		}

		public override void OnPointerExit(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			base.OnPointerExit(eventData);
			_pressed = false;
		}

		public override void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			if (eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left)
			{
				_pressed = true;
				_handled = false;
				_pressedTime = global::UnityEngine.Time.realtimeSinceStartup;
			}
		}

		public override void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!_handled)
			{
				base.OnPointerUp(eventData);
			}
			_pressed = false;
		}

		public override void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!_handled)
			{
				base.OnPointerClick(eventData);
			}
		}

		private void Update()
		{
			if (_pressed && global::UnityEngine.Time.realtimeSinceStartup - _pressedTime >= LongPressDuration)
			{
				_pressed = false;
				_handled = true;
				onLongPress.Invoke();
			}
		}
	}
}
