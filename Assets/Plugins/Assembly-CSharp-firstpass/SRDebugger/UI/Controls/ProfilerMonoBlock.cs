namespace SRDebugger.UI.Controls
{
	public class ProfilerMonoBlock : global::SRF.SRMonoBehaviourEx
	{
		private float _lastRefresh;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text CurrentUsedText;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject NotSupportedMessage;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Slider Slider;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text TotalAllocatedText;

		private bool _isSupported;

		protected override void OnEnable()
		{
			base.OnEnable();
			_isSupported = global::UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong() > 0;
			NotSupportedMessage.SetActive(!_isSupported);
			CurrentUsedText.gameObject.SetActive(_isSupported);
			TriggerRefresh();
		}

		protected override void Update()
		{
			base.Update();
			if (SRDebug.Instance.IsDebugPanelVisible && global::UnityEngine.Time.realtimeSinceStartup - _lastRefresh > 1f)
			{
				TriggerRefresh();
				_lastRefresh = global::UnityEngine.Time.realtimeSinceStartup;
			}
		}

		public void TriggerRefresh()
		{
			long num = (_isSupported ? global::UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong() : global::System.GC.GetTotalMemory(forceFullCollection: false));
			long monoUsedSizeLong = global::UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong();
			long num2 = num >> 10;
			num2 /= 1024;
			long num3 = monoUsedSizeLong >> 10;
			num3 /= 1024;
			Slider.maxValue = num2;
			Slider.value = num3;
			TotalAllocatedText.text = global::SRF.SRFStringExtensions.Fmt("Total: <color=#FFFFFF>{0}</color>MB", num2);
			if (num3 > 0)
			{
				CurrentUsedText.text = global::SRF.SRFStringExtensions.Fmt("<color=#FFFFFF>{0}</color>MB", num3);
			}
		}

		public void TriggerCollection()
		{
			global::System.GC.Collect();
			TriggerRefresh();
		}
	}
}
