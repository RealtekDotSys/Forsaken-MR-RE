namespace SRDebugger.Profiler
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
	public class ProfilerCameraListener : global::UnityEngine.MonoBehaviour
	{
		private global::UnityEngine.Camera _camera;

		private global::System.Diagnostics.Stopwatch _stopwatch;

		public global::System.Action<global::SRDebugger.Profiler.ProfilerCameraListener, double> RenderDurationCallback;

		protected global::System.Diagnostics.Stopwatch Stopwatch
		{
			get
			{
				if (_stopwatch == null)
				{
					_stopwatch = new global::System.Diagnostics.Stopwatch();
				}
				return _stopwatch;
			}
		}

		public global::UnityEngine.Camera Camera
		{
			get
			{
				if (_camera == null)
				{
					_camera = GetComponent<global::UnityEngine.Camera>();
				}
				return _camera;
			}
		}

		private void OnPreCull()
		{
			if (base.isActiveAndEnabled)
			{
				Stopwatch.Start();
			}
		}

		private void OnPostRender()
		{
			if (base.isActiveAndEnabled)
			{
				double totalSeconds = _stopwatch.Elapsed.TotalSeconds;
				Stopwatch.Stop();
				Stopwatch.Reset();
				if (RenderDurationCallback == null)
				{
					global::UnityEngine.Object.Destroy(this);
				}
				else
				{
					RenderDurationCallback(this, totalSeconds);
				}
			}
		}
	}
}
