namespace SRF.UI
{
	[global::UnityEngine.AddComponentMenu("SRF/UI/Spinner")]
	public class SRSpinner : global::UnityEngine.UI.Selectable, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IBeginDragHandler
	{
		[global::System.Serializable]
		public class SpinEvent : global::UnityEngine.Events.UnityEvent
		{
		}

		private float _dragDelta;

		[global::UnityEngine.SerializeField]
		private global::SRF.UI.SRSpinner.SpinEvent _onSpinDecrement = new global::SRF.UI.SRSpinner.SpinEvent();

		[global::UnityEngine.SerializeField]
		private global::SRF.UI.SRSpinner.SpinEvent _onSpinIncrement = new global::SRF.UI.SRSpinner.SpinEvent();

		public float DragThreshold = 20f;

		public global::SRF.UI.SRSpinner.SpinEvent OnSpinIncrement
		{
			get
			{
				return _onSpinIncrement;
			}
			set
			{
				_onSpinIncrement = value;
			}
		}

		public global::SRF.UI.SRSpinner.SpinEvent OnSpinDecrement
		{
			get
			{
				return _onSpinDecrement;
			}
			set
			{
				_onSpinDecrement = value;
			}
		}

		public void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			_dragDelta = 0f;
		}

		public void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!base.interactable)
			{
				return;
			}
			_dragDelta += eventData.delta.x;
			if (global::UnityEngine.Mathf.Abs(_dragDelta) > DragThreshold)
			{
				float num = global::UnityEngine.Mathf.Sign(_dragDelta);
				int num2 = global::UnityEngine.Mathf.FloorToInt(global::UnityEngine.Mathf.Abs(_dragDelta) / DragThreshold);
				if (num > 0f)
				{
					OnIncrement(num2);
				}
				else
				{
					OnDecrement(num2);
				}
				_dragDelta -= (float)num2 * DragThreshold * num;
			}
		}

		private void OnIncrement(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				OnSpinIncrement.Invoke();
			}
		}

		private void OnDecrement(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				OnSpinDecrement.Invoke();
			}
		}
	}
}
