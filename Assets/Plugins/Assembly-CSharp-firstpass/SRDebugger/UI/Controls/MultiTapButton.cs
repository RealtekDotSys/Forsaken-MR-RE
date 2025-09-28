namespace SRDebugger.UI.Controls
{
	public class MultiTapButton : global::UnityEngine.UI.Button
	{
		private float _lastTap;

		private int _tapCount;

		public int RequiredTapCount = 3;

		public float ResetTime = 0.5f;

		public override void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (global::UnityEngine.Time.unscaledTime - _lastTap > ResetTime)
			{
				_tapCount = 0;
			}
			_lastTap = global::UnityEngine.Time.unscaledTime;
			_tapCount++;
			if (_tapCount == RequiredTapCount)
			{
				base.OnPointerClick(eventData);
				_tapCount = 0;
			}
		}
	}
}
