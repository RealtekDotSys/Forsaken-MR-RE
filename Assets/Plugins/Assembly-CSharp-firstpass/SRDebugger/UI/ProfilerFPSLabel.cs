namespace SRDebugger.UI
{
	public class ProfilerFPSLabel : global::SRF.SRMonoBehaviourEx
	{
		private float _nextUpdate;

		[global::SRF.Import]
		private global::SRDebugger.Services.IProfilerService _profilerService;

		public float UpdateFrequency = 1f;

		[global::SRF.RequiredField]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Text _text;

		protected override void Update()
		{
			base.Update();
			if (global::UnityEngine.Time.realtimeSinceStartup > _nextUpdate)
			{
				Refresh();
			}
		}

		private void Refresh()
		{
			_text.text = global::SRF.SRFStringExtensions.Fmt("FPS: {0:0.00}", 1f / _profilerService.AverageFrameTime);
			_nextUpdate = global::UnityEngine.Time.realtimeSinceStartup + UpdateFrequency;
		}
	}
}
