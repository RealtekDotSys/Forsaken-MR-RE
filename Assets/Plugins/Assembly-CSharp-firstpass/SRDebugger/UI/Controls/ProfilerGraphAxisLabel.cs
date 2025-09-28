namespace SRDebugger.UI.Controls
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public class ProfilerGraphAxisLabel : global::SRF.SRMonoBehaviourEx
	{
		private float _prevFrameTime;

		private float? _queuedFrameTime;

		private float _yPosition;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Text;

		protected override void Update()
		{
			base.Update();
			if (_queuedFrameTime.HasValue)
			{
				SetValueInternal(_queuedFrameTime.Value);
				_queuedFrameTime = null;
			}
		}

		public void SetValue(float frameTime, float yPosition)
		{
			if (_prevFrameTime != frameTime || _yPosition != yPosition)
			{
				_queuedFrameTime = frameTime;
				_yPosition = yPosition;
			}
		}

		private void SetValueInternal(float frameTime)
		{
			_prevFrameTime = frameTime;
			int num = global::UnityEngine.Mathf.FloorToInt(frameTime * 1000f);
			int num2 = global::UnityEngine.Mathf.RoundToInt(1f / frameTime);
			Text.text = global::SRF.SRFStringExtensions.Fmt("{0}ms ({1}FPS)", num, num2);
			global::UnityEngine.RectTransform obj = (global::UnityEngine.RectTransform)base.CachedTransform;
			obj.anchoredPosition = new global::UnityEngine.Vector2(obj.rect.width * 0.5f + 10f, _yPosition);
		}
	}
}
