namespace SRDebugger.Services
{
	public class BugReport
	{
		public global::System.Collections.Generic.List<global::SRDebugger.Services.ConsoleEntry> ConsoleLog;

		public string Email;

		public byte[] ScreenshotData;

		public global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<string, object>> SystemInformation;

		public string UserDescription;
	}
}
