namespace SRDebugger.UI.Controls
{
	public class ProfilerEnableControl : global::SRF.SRMonoBehaviourEx
	{
		private bool _previousState;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text ButtonText;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Button EnableButton;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Text;

		protected override void Start()
		{
			base.Start();
			if (!global::UnityEngine.Profiling.Profiler.supported)
			{
				Text.text = global::SRDebugger.Internal.SRDebugStrings.Current.Profiler_NotSupported;
				EnableButton.gameObject.SetActive(value: false);
				base.enabled = false;
			}
			else if (!global::UnityEngine.Application.HasProLicense())
			{
				Text.text = global::SRDebugger.Internal.SRDebugStrings.Current.Profiler_NoProInfo;
				EnableButton.gameObject.SetActive(value: false);
				base.enabled = false;
			}
			else
			{
				UpdateLabels();
			}
		}

		protected void UpdateLabels()
		{
			if (!global::UnityEngine.Profiling.Profiler.enabled)
			{
				Text.text = global::SRDebugger.Internal.SRDebugStrings.Current.Profiler_EnableProfilerInfo;
				ButtonText.text = "Enable";
			}
			else
			{
				Text.text = global::SRDebugger.Internal.SRDebugStrings.Current.Profiler_DisableProfilerInfo;
				ButtonText.text = "Disable";
			}
			_previousState = global::UnityEngine.Profiling.Profiler.enabled;
		}

		protected override void Update()
		{
			base.Update();
			if (global::UnityEngine.Profiling.Profiler.enabled != _previousState)
			{
				UpdateLabels();
			}
		}

		public void ToggleProfiler()
		{
			global::UnityEngine.Debug.Log("Toggle Profiler");
			global::UnityEngine.Profiling.Profiler.enabled = !global::UnityEngine.Profiling.Profiler.enabled;
		}
	}
}
