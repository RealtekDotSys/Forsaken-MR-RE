namespace SRDebugger.UI.Controls
{
	public class ProfilerMemoryBlock : global::SRF.SRMonoBehaviourEx
	{
		private float _lastRefresh;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text CurrentUsedText;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Slider Slider;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text TotalAllocatedText;

		protected override void OnEnable()
		{
			base.OnEnable();
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
			long totalReservedMemoryLong = global::UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong();
			long totalAllocatedMemoryLong = global::UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong();
			long num = totalReservedMemoryLong >> 10;
			num /= 1024;
			long num2 = totalAllocatedMemoryLong >> 10;
			num2 /= 1024;
			Slider.maxValue = num;
			Slider.value = num2;
			TotalAllocatedText.text = global::SRF.SRFStringExtensions.Fmt("Reserved: <color=#FFFFFF>{0}</color>MB", num);
			CurrentUsedText.text = global::SRF.SRFStringExtensions.Fmt("<color=#FFFFFF>{0}</color>MB", num2);
		}

		public void TriggerCleanup()
		{
			StartCoroutine(CleanUp());
		}

		private global::System.Collections.IEnumerator CleanUp()
		{
			global::System.GC.Collect();
			yield return global::UnityEngine.Resources.UnloadUnusedAssets();
			global::System.GC.Collect();
			TriggerRefresh();
		}
	}
}
