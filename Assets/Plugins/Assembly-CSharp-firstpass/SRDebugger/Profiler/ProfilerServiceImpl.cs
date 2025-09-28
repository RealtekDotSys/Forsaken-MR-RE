namespace SRDebugger.Profiler
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IProfilerService))]
	public class ProfilerServiceImpl : global::SRF.Service.SRServiceBase<global::SRDebugger.Services.IProfilerService>, global::SRDebugger.Services.IProfilerService
	{
		private const int FrameBufferSize = 400;

		private readonly global::SRF.SRList<global::SRDebugger.Profiler.ProfilerCameraListener> _cameraListeners = new global::SRF.SRList<global::SRDebugger.Profiler.ProfilerCameraListener>();

		private readonly global::SRDebugger.CircularBuffer<global::SRDebugger.Services.ProfilerFrame> _frameBuffer = new global::SRDebugger.CircularBuffer<global::SRDebugger.Services.ProfilerFrame>(400);

		private global::UnityEngine.Camera[] _cameraCache = new global::UnityEngine.Camera[6];

		private global::SRDebugger.Profiler.ProfilerLateUpdateListener _lateUpdateListener;

		private double _renderDuration;

		private int _reportedCameras;

		private global::System.Diagnostics.Stopwatch _stopwatch = new global::System.Diagnostics.Stopwatch();

		private double _updateDuration;

		private double _updateToRenderDuration;

		public float AverageFrameTime { get; private set; }

		public float LastFrameTime { get; private set; }

		public global::SRDebugger.CircularBuffer<global::SRDebugger.Services.ProfilerFrame> FrameBuffer => _frameBuffer;

		protected void PushFrame(double totalTime, double updateTime, double renderTime)
		{
			_frameBuffer.PushBack(new global::SRDebugger.Services.ProfilerFrame
			{
				OtherTime = totalTime - updateTime - renderTime,
				UpdateTime = updateTime,
				RenderTime = renderTime
			});
		}

		protected override void Awake()
		{
			base.Awake();
			_lateUpdateListener = base.gameObject.AddComponent<global::SRDebugger.Profiler.ProfilerLateUpdateListener>();
			_lateUpdateListener.OnLateUpdate = OnLateUpdate;
			base.CachedGameObject.hideFlags = global::UnityEngine.HideFlags.NotEditable;
			base.CachedTransform.SetParent(global::SRF.Hierarchy.Get("SRDebugger"), worldPositionStays: true);
		}

		protected override void Update()
		{
			base.Update();
			if (FrameBuffer.Count > 0)
			{
				global::SRDebugger.Services.ProfilerFrame value = FrameBuffer.Back();
				value.FrameTime = global::UnityEngine.Time.deltaTime;
				FrameBuffer[FrameBuffer.Count - 1] = value;
			}
			LastFrameTime = global::UnityEngine.Time.deltaTime;
			int num = global::UnityEngine.Mathf.Min(20, FrameBuffer.Count);
			double num2 = 0.0;
			for (int i = 0; i < num; i++)
			{
				num2 += FrameBuffer[i].FrameTime;
			}
			AverageFrameTime = (float)num2 / (float)num;
			_ = _reportedCameras;
			_ = _cameraListeners.Count;
			if (_stopwatch.IsRunning)
			{
				_stopwatch.Stop();
				_stopwatch.Reset();
			}
			_updateDuration = (_renderDuration = (_updateToRenderDuration = 0.0));
			_reportedCameras = 0;
			CameraCheck();
			_stopwatch.Start();
		}

		private void OnLateUpdate()
		{
			_updateDuration = _stopwatch.Elapsed.TotalSeconds;
		}

		private void EndFrame()
		{
			if (_stopwatch.IsRunning)
			{
				PushFrame(_stopwatch.Elapsed.TotalSeconds, _updateDuration, _renderDuration);
				_stopwatch.Reset();
				_stopwatch.Start();
			}
		}

		private void CameraDurationCallback(global::SRDebugger.Profiler.ProfilerCameraListener listener, double duration)
		{
			_reportedCameras++;
			_renderDuration = _stopwatch.Elapsed.TotalSeconds - _updateDuration - _updateToRenderDuration;
			if (_reportedCameras >= GetExpectedCameraCount())
			{
				EndFrame();
			}
		}

		private int GetExpectedCameraCount()
		{
			int num = 0;
			for (int i = 0; i < _cameraListeners.Count; i++)
			{
				if (!(_cameraListeners[i] != null) || (_cameraListeners[i].isActiveAndEnabled && _cameraListeners[i].Camera.isActiveAndEnabled))
				{
					num++;
				}
			}
			return num;
		}

		private void CameraCheck()
		{
			for (int num = _cameraListeners.Count - 1; num >= 0; num--)
			{
				if (_cameraListeners[num] == null)
				{
					_cameraListeners.RemoveAt(num);
				}
			}
			if (global::UnityEngine.Camera.allCamerasCount == _cameraListeners.Count)
			{
				return;
			}
			if (global::UnityEngine.Camera.allCamerasCount > _cameraCache.Length)
			{
				_cameraCache = new global::UnityEngine.Camera[global::UnityEngine.Camera.allCamerasCount];
			}
			int allCameras = global::UnityEngine.Camera.GetAllCameras(_cameraCache);
			for (int i = 0; i < allCameras; i++)
			{
				global::UnityEngine.Camera camera = _cameraCache[i];
				bool flag = false;
				for (int j = 0; j < _cameraListeners.Count; j++)
				{
					if (_cameraListeners[j].Camera == camera)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					global::SRDebugger.Profiler.ProfilerCameraListener profilerCameraListener = camera.gameObject.AddComponent<global::SRDebugger.Profiler.ProfilerCameraListener>();
					profilerCameraListener.hideFlags = global::UnityEngine.HideFlags.DontSave | global::UnityEngine.HideFlags.NotEditable;
					profilerCameraListener.RenderDurationCallback = CameraDurationCallback;
					_cameraListeners.Add(profilerCameraListener);
				}
			}
		}
	}
}
