namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.Implementation.BugReportPopoverService))]
	public class BugReportPopoverService : global::SRF.Service.SRServiceBase<global::SRDebugger.Services.Implementation.BugReportPopoverService>
	{
		private global::SRDebugger.Services.BugReportCompleteCallback _callback;

		private bool _isVisible;

		private global::SRDebugger.UI.Other.BugReportPopoverRoot _popover;

		private global::SRDebugger.UI.Other.BugReportSheetController _sheet;

		public bool IsShowingPopover => _isVisible;

		public void ShowBugReporter(global::SRDebugger.Services.BugReportCompleteCallback callback, bool takeScreenshotFirst = true, string descriptionText = null)
		{
			if (_isVisible)
			{
				throw new global::System.InvalidOperationException("Bug report popover is already visible.");
			}
			if (_popover == null)
			{
				Load();
			}
			if (_popover == null)
			{
				global::UnityEngine.Debug.LogWarning("[SRDebugger] Bug report popover failed loading, executing callback with fail result");
				callback(didSucceed: false, "Resource load failed");
				return;
			}
			_callback = callback;
			_isVisible = true;
			global::SRDebugger.Internal.SRDebuggerUtil.EnsureEventSystemExists();
			StartCoroutine(OpenCo(takeScreenshotFirst, descriptionText));
		}

		private global::System.Collections.IEnumerator OpenCo(bool takeScreenshot, string descriptionText)
		{
			if (takeScreenshot)
			{
				yield return StartCoroutine(global::SRDebugger.Internal.BugReportScreenshotUtil.ScreenshotCaptureCo());
			}
			_popover.CachedGameObject.SetActive(value: true);
			yield return new global::UnityEngine.WaitForEndOfFrame();
			if (!string.IsNullOrEmpty(descriptionText))
			{
				_sheet.DescriptionField.text = descriptionText;
			}
		}

		private void SubmitComplete(bool didSucceed, string errorMessage)
		{
			OnComplete(didSucceed, errorMessage, close: false);
		}

		private void CancelPressed()
		{
			OnComplete(success: false, "User Cancelled", close: true);
		}

		private void OnComplete(bool success, string errorMessage, bool close)
		{
			if (!_isVisible)
			{
				global::UnityEngine.Debug.LogWarning("[SRDebugger] Received callback at unexpected time. ???");
			}
			else if (success || close)
			{
				_isVisible = false;
				_popover.gameObject.SetActive(value: false);
				global::UnityEngine.Object.Destroy(_popover.gameObject);
				_popover = null;
				_sheet = null;
				global::SRDebugger.Internal.BugReportScreenshotUtil.ScreenshotData = null;
				_callback(success, errorMessage);
			}
		}

		private void TakingScreenshot()
		{
			if (!IsShowingPopover)
			{
				global::UnityEngine.Debug.LogWarning("[SRDebugger] Received callback at unexpected time. ???");
			}
			else
			{
				_popover.CanvasGroup.alpha = 0f;
			}
		}

		private void ScreenshotComplete()
		{
			if (!IsShowingPopover)
			{
				global::UnityEngine.Debug.LogWarning("[SRDebugger] Received callback at unexpected time. ???");
			}
			else
			{
				_popover.CanvasGroup.alpha = 1f;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			base.CachedTransform.SetParent(global::SRF.Hierarchy.Get("SRDebugger"));
		}

		private void Load()
		{
			global::SRDebugger.UI.Other.BugReportPopoverRoot bugReportPopoverRoot = global::UnityEngine.Resources.Load<global::SRDebugger.UI.Other.BugReportPopoverRoot>("SRDebugger/UI/Prefabs/BugReportPopover");
			global::SRDebugger.UI.Other.BugReportSheetController bugReportSheetController = global::UnityEngine.Resources.Load<global::SRDebugger.UI.Other.BugReportSheetController>("SRDebugger/UI/Prefabs/BugReportSheet");
			if (bugReportPopoverRoot == null)
			{
				global::UnityEngine.Debug.LogError("[SRDebugger] Unable to load bug report popover prefab");
				return;
			}
			if (bugReportSheetController == null)
			{
				global::UnityEngine.Debug.LogError("[SRDebugger] Unable to load bug report sheet prefab");
				return;
			}
			_popover = SRInstantiate.Instantiate(bugReportPopoverRoot);
			_popover.CachedTransform.SetParent(base.CachedTransform, worldPositionStays: false);
			_sheet = SRInstantiate.Instantiate(bugReportSheetController);
			_sheet.CachedTransform.SetParent(_popover.Container, worldPositionStays: false);
			_sheet.SubmitComplete = SubmitComplete;
			_sheet.CancelPressed = CancelPressed;
			_sheet.TakingScreenshot = TakingScreenshot;
			_sheet.ScreenshotComplete = ScreenshotComplete;
			_popover.CachedGameObject.SetActive(value: false);
		}
	}
}
