namespace SRF.UI
{
	[global::UnityEngine.AddComponentMenu("SRF/UI/SRNumberSpinner")]
	public class SRNumberSpinner : global::UnityEngine.UI.InputField
	{
		private double _currentValue;

		private double _dragStartAmount;

		private double _dragStep;

		public float DragSensitivity = 0.01f;

		public double MaxValue = double.MaxValue;

		public double MinValue = double.MinValue;

		protected override void Awake()
		{
			base.Awake();
			if (base.contentType != global::UnityEngine.UI.InputField.ContentType.IntegerNumber && base.contentType != global::UnityEngine.UI.InputField.ContentType.DecimalNumber)
			{
				global::UnityEngine.Debug.LogError("[SRNumberSpinner] contentType must be integer or decimal. Defaulting to integer");
				base.contentType = global::UnityEngine.UI.InputField.ContentType.DecimalNumber;
			}
		}

		public override void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (base.interactable && !eventData.dragging)
			{
				global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
				base.OnPointerClick(eventData);
				if (m_Keyboard == null || !m_Keyboard.active)
				{
					OnSelect(eventData);
					return;
				}
				UpdateLabel();
				eventData.Use();
			}
		}

		public override void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
		}

		public override void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
		}

		public override void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!base.interactable)
			{
				return;
			}
			if (global::UnityEngine.Mathf.Abs(eventData.delta.y) > global::UnityEngine.Mathf.Abs(eventData.delta.x))
			{
				global::UnityEngine.Transform parent = base.transform.parent;
				if (parent != null)
				{
					eventData.pointerDrag = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IBeginDragHandler>(parent.gameObject);
					if (eventData.pointerDrag != null)
					{
						global::UnityEngine.EventSystems.ExecuteEvents.Execute(eventData.pointerDrag, eventData, global::UnityEngine.EventSystems.ExecuteEvents.beginDragHandler);
					}
				}
				return;
			}
			eventData.Use();
			_dragStartAmount = double.Parse(base.text);
			_currentValue = _dragStartAmount;
			float num = 1f;
			if (base.contentType == global::UnityEngine.UI.InputField.ContentType.IntegerNumber)
			{
				num *= 10f;
			}
			_dragStep = global::System.Math.Max(num, _dragStartAmount * 0.05000000074505806);
			if (base.isFocused)
			{
				DeactivateInputField();
			}
		}

		public override void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (base.interactable)
			{
				float x = eventData.delta.x;
				_currentValue += global::System.Math.Abs(_dragStep) * (double)x * (double)DragSensitivity;
				_currentValue = global::System.Math.Round(_currentValue, 2);
				if (_currentValue > MaxValue)
				{
					_currentValue = MaxValue;
				}
				if (_currentValue < MinValue)
				{
					_currentValue = MinValue;
				}
				if (base.contentType == global::UnityEngine.UI.InputField.ContentType.IntegerNumber)
				{
					base.text = ((int)global::System.Math.Round(_currentValue)).ToString();
				}
				else
				{
					base.text = _currentValue.ToString();
				}
			}
		}

		public override void OnEndDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (base.interactable)
			{
				eventData.Use();
				if (_dragStartAmount != _currentValue)
				{
					DeactivateInputField();
					SendOnSubmit();
				}
			}
		}
	}
}
