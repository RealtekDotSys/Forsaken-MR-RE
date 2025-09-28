namespace SRDebugger.Internal
{
	public class BugReportApi
	{
		private readonly string _apiKey;

		private readonly global::SRDebugger.Services.BugReport _bugReport;

		private bool _isBusy;

		private global::UnityEngine.WWW _www;

		public bool IsComplete { get; private set; }

		public bool WasSuccessful { get; private set; }

		public string ErrorMessage { get; private set; }

		public float Progress
		{
			get
			{
				if (_www == null)
				{
					return 0f;
				}
				return global::UnityEngine.Mathf.Clamp01(_www.progress + _www.uploadProgress);
			}
		}

		public BugReportApi(global::SRDebugger.Services.BugReport report, string apiKey)
		{
			_bugReport = report;
			_apiKey = apiKey;
		}

		public global::System.Collections.IEnumerator Submit()
		{
			if (_isBusy)
			{
				throw new global::System.InvalidOperationException("BugReportApi is already sending a bug report");
			}
			_isBusy = true;
			ErrorMessage = "";
			IsComplete = false;
			WasSuccessful = false;
			_www = null;
			try
			{
				string s = BuildJsonRequest(_bugReport);
				byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(s);
				global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
				dictionary["Content-Type"] = "application/json";
				dictionary["Accept"] = "application/json";
				dictionary["Method"] = "POST";
				dictionary["X-ApiKey"] = _apiKey;
				_www = new global::UnityEngine.WWW("http://srdebugger.stompyrobot.uk/report/submit", bytes, dictionary);
			}
			catch (global::System.Exception ex)
			{
				ErrorMessage = ex.Message;
			}
			if (_www == null)
			{
				SetCompletionState(wasSuccessful: false);
				yield break;
			}
			yield return _www;
			if (!string.IsNullOrEmpty(_www.error))
			{
				ErrorMessage = _www.error;
				SetCompletionState(wasSuccessful: false);
				yield break;
			}
			if (!_www.responseHeaders.ContainsKey("X-STATUS"))
			{
				ErrorMessage = "Completion State Unknown";
				SetCompletionState(wasSuccessful: false);
				yield break;
			}
			string text = _www.responseHeaders["X-STATUS"];
			if (!text.Contains("200"))
			{
				ErrorMessage = global::SRDebugger.Internal.SRDebugApiUtil.ParseErrorResponse(_www.text, text);
				SetCompletionState(wasSuccessful: false);
			}
			else
			{
				SetCompletionState(wasSuccessful: true);
			}
		}

		private void SetCompletionState(bool wasSuccessful)
		{
			_bugReport.ScreenshotData = null;
			WasSuccessful = wasSuccessful;
			IsComplete = true;
			_isBusy = false;
			if (!wasSuccessful)
			{
				global::UnityEngine.Debug.LogError("Bug Reporter Error: " + ErrorMessage);
			}
		}

		private static string BuildJsonRequest(global::SRDebugger.Services.BugReport report)
		{
			global::System.Collections.Hashtable hashtable = new global::System.Collections.Hashtable();
			hashtable.Add("userEmail", report.Email);
			hashtable.Add("userDescription", report.UserDescription);
			hashtable.Add("console", CreateConsoleDump());
			hashtable.Add("systemInformation", report.SystemInformation);
			if (report.ScreenshotData != null)
			{
				hashtable.Add("screenshot", global::System.Convert.ToBase64String(report.ScreenshotData));
			}
			return global::SRF.Json.Serialize(hashtable);
		}

		private static global::System.Collections.Generic.IList<global::System.Collections.Generic.IList<string>> CreateConsoleDump()
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.IList<string>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.IList<string>>();
			foreach (global::SRDebugger.Services.ConsoleEntry entry in global::SRDebugger.Internal.Service.Console.Entries)
			{
				global::System.Collections.Generic.List<string> list2 = new global::System.Collections.Generic.List<string>();
				list2.Add(entry.LogType.ToString());
				list2.Add(entry.Message);
				list2.Add(entry.StackTrace);
				if (entry.Count > 1)
				{
					list2.Add(entry.Count.ToString());
				}
				list.Add(list2);
			}
			return list;
		}
	}
}
