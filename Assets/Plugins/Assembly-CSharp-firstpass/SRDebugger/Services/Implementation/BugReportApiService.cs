namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IBugReportService))]
	public class BugReportApiService : global::SRF.Service.SRServiceBase<global::SRDebugger.Services.IBugReportService>, global::SRDebugger.Services.IBugReportService
	{
		public const float Timeout = 12f;

		private global::SRDebugger.Services.BugReportCompleteCallback _completeCallback;

		private string _errorMessage;

		private bool _isBusy;

		private float _previousProgress;

		private global::SRDebugger.Services.BugReportProgressCallback _progressCallback;

		private global::SRDebugger.Internal.BugReportApi _reportApi;

		public void SendBugReport(global::SRDebugger.Services.BugReport report, global::SRDebugger.Services.BugReportCompleteCallback completeHandler, global::SRDebugger.Services.BugReportProgressCallback progressCallback = null)
		{
			if (report == null)
			{
				throw new global::System.ArgumentNullException("report");
			}
			if (completeHandler == null)
			{
				throw new global::System.ArgumentNullException("completeHandler");
			}
			if (_isBusy)
			{
				completeHandler(didSucceed: false, "BugReportApiService is already sending a bug report");
				return;
			}
			if (global::UnityEngine.Application.internetReachability == global::UnityEngine.NetworkReachability.NotReachable)
			{
				completeHandler(didSucceed: false, "No Internet Connection");
				return;
			}
			_errorMessage = "";
			base.enabled = true;
			_isBusy = true;
			_completeCallback = completeHandler;
			_progressCallback = progressCallback;
			_reportApi = new global::SRDebugger.Internal.BugReportApi(report, global::SRDebugger.Settings.Instance.ApiKey);
			StartCoroutine(_reportApi.Submit());
		}

		protected override void Awake()
		{
			base.Awake();
			base.CachedTransform.SetParent(global::SRF.Hierarchy.Get("SRDebugger"));
		}

		private void OnProgress(float progress)
		{
			if (_progressCallback != null)
			{
				_progressCallback(progress);
			}
		}

		private void OnComplete()
		{
			_isBusy = false;
			base.enabled = false;
			_completeCallback(_reportApi.WasSuccessful, string.IsNullOrEmpty(_reportApi.ErrorMessage) ? _errorMessage : _reportApi.ErrorMessage);
			_completeCallback = null;
		}

		protected override void Update()
		{
			base.Update();
			if (_isBusy)
			{
				if (_reportApi == null)
				{
					_isBusy = false;
				}
				if (_reportApi.IsComplete)
				{
					OnComplete();
				}
				else if (_previousProgress != _reportApi.Progress)
				{
					OnProgress(_reportApi.Progress);
					_previousProgress = _reportApi.Progress;
				}
			}
		}
	}
}
