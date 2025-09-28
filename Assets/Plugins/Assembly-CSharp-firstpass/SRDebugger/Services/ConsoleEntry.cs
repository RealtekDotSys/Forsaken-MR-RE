namespace SRDebugger.Services
{
	public class ConsoleEntry
	{
		private const int MessagePreviewLength = 180;

		private const int StackTracePreviewLength = 120;

		private string _messagePreview;

		private string _stackTracePreview;

		public int Count = 1;

		public global::UnityEngine.LogType LogType;

		public string Message;

		public string StackTrace;

		public string MessagePreview
		{
			get
			{
				if (_messagePreview != null)
				{
					return _messagePreview;
				}
				if (string.IsNullOrEmpty(Message))
				{
					return "";
				}
				_messagePreview = Message.Split('\n')[0];
				_messagePreview = _messagePreview.Substring(0, global::UnityEngine.Mathf.Min(_messagePreview.Length, 180));
				return _messagePreview;
			}
		}

		public string StackTracePreview
		{
			get
			{
				if (_stackTracePreview != null)
				{
					return _stackTracePreview;
				}
				if (string.IsNullOrEmpty(StackTrace))
				{
					return "";
				}
				_stackTracePreview = StackTrace.Split('\n')[0];
				_stackTracePreview = _stackTracePreview.Substring(0, global::UnityEngine.Mathf.Min(_stackTracePreview.Length, 120));
				return _stackTracePreview;
			}
		}

		public ConsoleEntry()
		{
		}

		public ConsoleEntry(global::SRDebugger.Services.ConsoleEntry other)
		{
			Message = other.Message;
			StackTrace = other.StackTrace;
			LogType = other.LogType;
			Count = other.Count;
		}

		public bool Matches(global::SRDebugger.Services.ConsoleEntry other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (string.Equals(Message, other.Message) && string.Equals(StackTrace, other.StackTrace))
			{
				return LogType == other.LogType;
			}
			return false;
		}
	}
}
