namespace SRDebugger.UI.Controls
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public class ConsoleEntryView : global::SRF.SRMonoBehaviourEx, global::SRF.UI.Layout.IVirtualView
	{
		public const string ConsoleBlobInfo = "Console_Info_Blob";

		public const string ConsoleBlobWarning = "Console_Warning_Blob";

		public const string ConsoleBlobError = "Console_Error_Blob";

		private int _count;

		private bool _hasCount;

		private global::SRDebugger.Services.ConsoleEntry _prevData;

		private global::UnityEngine.RectTransform _rectTransform;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Count;

		[global::SRF.RequiredField]
		public global::UnityEngine.CanvasGroup CountContainer;

		[global::SRF.RequiredField]
		public global::SRF.UI.StyleComponent ImageStyle;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Message;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text StackTrace;

		public void SetDataContext(object data)
		{
			if (!(data is global::SRDebugger.Services.ConsoleEntry consoleEntry))
			{
				throw new global::System.Exception("Data should be a ConsoleEntry");
			}
			if (consoleEntry.Count > 1)
			{
				if (!_hasCount)
				{
					CountContainer.alpha = 1f;
					_hasCount = true;
				}
				if (consoleEntry.Count != _count)
				{
					Count.text = global::SRDebugger.Internal.SRDebuggerUtil.GetNumberString(consoleEntry.Count, 999, "999+");
					_count = consoleEntry.Count;
				}
			}
			else if (_hasCount)
			{
				CountContainer.alpha = 0f;
				_hasCount = false;
			}
			if (consoleEntry != _prevData)
			{
				_prevData = consoleEntry;
				Message.text = consoleEntry.MessagePreview;
				StackTrace.text = consoleEntry.StackTracePreview;
				if (string.IsNullOrEmpty(StackTrace.text))
				{
					Message.rectTransform.SetInsetAndSizeFromParentEdge(global::UnityEngine.RectTransform.Edge.Bottom, 2f, _rectTransform.rect.height - 4f);
				}
				else
				{
					Message.rectTransform.SetInsetAndSizeFromParentEdge(global::UnityEngine.RectTransform.Edge.Bottom, 12f, _rectTransform.rect.height - 14f);
				}
				switch (consoleEntry.LogType)
				{
				case global::UnityEngine.LogType.Log:
					ImageStyle.StyleKey = "Console_Info_Blob";
					break;
				case global::UnityEngine.LogType.Warning:
					ImageStyle.StyleKey = "Console_Warning_Blob";
					break;
				case global::UnityEngine.LogType.Error:
				case global::UnityEngine.LogType.Assert:
				case global::UnityEngine.LogType.Exception:
					ImageStyle.StyleKey = "Console_Error_Blob";
					break;
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			_rectTransform = base.CachedTransform as global::UnityEngine.RectTransform;
			CountContainer.alpha = 0f;
			Message.supportRichText = global::SRDebugger.Settings.Instance.RichTextInConsole;
		}
	}
}
