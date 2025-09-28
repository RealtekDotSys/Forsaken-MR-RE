namespace SRDebugger.Services
{
	public interface IBugReportService
	{
		void SendBugReport(global::SRDebugger.Services.BugReport report, global::SRDebugger.Services.BugReportCompleteCallback completeHandler, global::SRDebugger.Services.BugReportProgressCallback progressCallback = null);
	}
}
