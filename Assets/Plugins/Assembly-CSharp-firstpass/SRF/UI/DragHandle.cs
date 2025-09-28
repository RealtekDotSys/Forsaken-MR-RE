namespace SRF.UI
{
	public class DragHandle : global::UnityEngine.MonoBehaviour, global::UnityEngine.EventSystems.IBeginDragHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IEndDragHandler, global::UnityEngine.EventSystems.IDragHandler
	{
		private global::UnityEngine.UI.CanvasScaler _canvasScaler;

		private float _delta;

		private float _startValue;

		public global::UnityEngine.RectTransform.Axis Axis;

		public bool Invert;

		public float MaxSize = -1f;

		public global::UnityEngine.UI.LayoutElement TargetLayoutElement;

		public global::UnityEngine.RectTransform TargetRectTransform;

		private float Mult => (!Invert) ? 1 : (-1);

		public void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (Verify())
			{
				_startValue = GetCurrentValue();
				_delta = 0f;
			}
		}

		public void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (Verify())
			{
				float num = 0f;
				num = ((Axis != global::UnityEngine.RectTransform.Axis.Horizontal) ? (num + eventData.delta.y) : (num + eventData.delta.x));
				if (_canvasScaler != null)
				{
					num /= _canvasScaler.scaleFactor;
				}
				num *= Mult;
				_delta += num;
				SetCurrentValue(global::UnityEngine.Mathf.Clamp(_startValue + _delta, GetMinSize(), GetMaxSize()));
			}
		}

		public void OnEndDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (Verify())
			{
				SetCurrentValue(global::UnityEngine.Mathf.Max(_startValue + _delta, GetMinSize()));
				_delta = 0f;
				CommitCurrentValue();
			}
		}

		private void Start()
		{
			Verify();
			_canvasScaler = GetComponentInParent<global::UnityEngine.UI.CanvasScaler>();
		}

		private bool Verify()
		{
			if (TargetLayoutElement == null && TargetRectTransform == null)
			{
				global::UnityEngine.Debug.LogWarning("DragHandle: TargetLayoutElement and TargetRectTransform are both null. Disabling behaviour.");
				base.enabled = false;
				return false;
			}
			return true;
		}

		private float GetCurrentValue()
		{
			if (TargetLayoutElement != null)
			{
				if (Axis != global::UnityEngine.RectTransform.Axis.Horizontal)
				{
					return TargetLayoutElement.preferredHeight;
				}
				return TargetLayoutElement.preferredWidth;
			}
			if (TargetRectTransform != null)
			{
				if (Axis != global::UnityEngine.RectTransform.Axis.Horizontal)
				{
					return TargetRectTransform.sizeDelta.y;
				}
				return TargetRectTransform.sizeDelta.x;
			}
			throw new global::System.InvalidOperationException();
		}

		private void SetCurrentValue(float value)
		{
			if (TargetLayoutElement != null)
			{
				if (Axis == global::UnityEngine.RectTransform.Axis.Horizontal)
				{
					TargetLayoutElement.preferredWidth = value;
				}
				else
				{
					TargetLayoutElement.preferredHeight = value;
				}
				return;
			}
			if (TargetRectTransform != null)
			{
				global::UnityEngine.Vector2 sizeDelta = TargetRectTransform.sizeDelta;
				if (Axis == global::UnityEngine.RectTransform.Axis.Horizontal)
				{
					sizeDelta.x = value;
				}
				else
				{
					sizeDelta.y = value;
				}
				TargetRectTransform.sizeDelta = sizeDelta;
				return;
			}
			throw new global::System.InvalidOperationException();
		}

		private void CommitCurrentValue()
		{
			if (TargetLayoutElement != null)
			{
				if (Axis == global::UnityEngine.RectTransform.Axis.Horizontal)
				{
					TargetLayoutElement.preferredWidth = ((global::UnityEngine.RectTransform)TargetLayoutElement.transform).sizeDelta.x;
				}
				else
				{
					TargetLayoutElement.preferredHeight = ((global::UnityEngine.RectTransform)TargetLayoutElement.transform).sizeDelta.y;
				}
			}
		}

		private float GetMinSize()
		{
			if (TargetLayoutElement == null)
			{
				return 0f;
			}
			if (Axis != global::UnityEngine.RectTransform.Axis.Horizontal)
			{
				return TargetLayoutElement.minHeight;
			}
			return TargetLayoutElement.minWidth;
		}

		private float GetMaxSize()
		{
			if (MaxSize > 0f)
			{
				return MaxSize;
			}
			return float.MaxValue;
		}
	}
}
