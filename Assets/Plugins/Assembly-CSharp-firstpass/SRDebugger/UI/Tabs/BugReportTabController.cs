namespace SRDebugger.UI.Tabs
{
	public class BugReportTabController : global::SRF.SRMonoBehaviourEx, global::SRDebugger.UI.Other.IEnableTab
	{
		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Other.BugReportSheetController BugReportSheetPrefab;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform Container;

		public bool IsEnabled => global::SRDebugger.Settings.Instance.EnableBugReporter;

		protected override void Start()
		{
			base.Start();
			global::SRDebugger.UI.Other.BugReportSheetController bugReportSheetController = SRInstantiate.Instantiate(BugReportSheetPrefab);
			bugReportSheetController.IsCancelButtonEnabled = false;
			bugReportSheetController.TakingScreenshot = TakingScreenshot;
			bugReportSheetController.ScreenshotComplete = ScreenshotComplete;
			bugReportSheetController.CachedTransform.SetParent(Container, worldPositionStays: false);
		}

		private void TakingScreenshot()
		{
			SRDebug.Instance.HideDebugPanel();
		}

		private void ScreenshotComplete()
		{
			SRDebug.Instance.ShowDebugPanel(requireEntryCode: false);
		}
	}
}
