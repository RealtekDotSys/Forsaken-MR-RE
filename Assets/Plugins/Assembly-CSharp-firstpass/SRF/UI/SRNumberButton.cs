namespace SRF.UI
{
	[global::UnityEngine.AddComponentMenu("SRF/UI/SRNumberButton")]
	public class SRNumberButton : global::UnityEngine.UI.Button, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IPointerUpHandler
	{
		private const float ExtraThreshold = 3f;

		public const float Delay = 0.4f;

		private float _delayTime;

		private float _downTime;

		private bool _isDown;

		public double Amount = 1.0;

		public global::SRF.UI.SRNumberSpinner TargetField;

		public override void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			if (base.interactable)
			{
				Apply();
				_isDown = true;
				_downTime = global::UnityEngine.Time.realtimeSinceStartup;
				_delayTime = _downTime + 0.4f;
			}
		}

		public override void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			_isDown = false;
		}

		protected virtual void Update()
		{
			if (_isDown && _delayTime <= global::UnityEngine.Time.realtimeSinceStartup)
			{
				Apply();
				float num = 0.2f;
				int num2 = global::UnityEngine.Mathf.RoundToInt((global::UnityEngine.Time.realtimeSinceStartup - _downTime) / 3f);
				for (int i = 0; i < num2; i++)
				{
					num *= 0.5f;
				}
				_delayTime = global::UnityEngine.Time.realtimeSinceStartup + num;
			}
		}

		private void Apply()
		{
			double num = double.Parse(TargetField.text);
			num += Amount;
			if (num > TargetField.MaxValue)
			{
				num = TargetField.MaxValue;
			}
			if (num < TargetField.MinValue)
			{
				num = TargetField.MinValue;
			}
			TargetField.text = num.ToString();
			TargetField.onEndEdit.Invoke(TargetField.text);
		}
	}
}
