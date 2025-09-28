namespace PaperPlaneTools
{
	public class Alert
	{
		public enum ButtonType
		{
			Positive = 0,
			Negative = 1,
			Neutral = 2
		}

		public string Title { get; private set; }

		public string Message { get; private set; }

		public global::PaperPlaneTools.AlertButton PositiveButton { get; private set; }

		public global::PaperPlaneTools.AlertButton NegativeButton { get; private set; }

		public global::PaperPlaneTools.AlertButton NeutralButton { get; private set; }

		public global::System.Collections.Generic.List<object> Options { get; private set; }

		public global::System.Action OnDismiss { get; private set; }

		public global::PaperPlaneTools.IAlertPlatformAdapter Adapter { get; private set; }

		public Alert(string title = null, string message = null)
		{
			Title = title;
			Message = message;
			Options = new global::System.Collections.Generic.List<object>();
		}

		public global::PaperPlaneTools.Alert SetTitle(string title)
		{
			Title = title;
			global::UnityEngine.Debug.LogError(Title);
			return this;
		}

		public global::PaperPlaneTools.Alert SetMessage(string title)
		{
			Message = title;
			return this;
		}

		public global::PaperPlaneTools.Alert SetPositiveButton(string title, global::System.Action handler = null)
		{
			PositiveButton = new global::PaperPlaneTools.AlertButton(title, handler);
			return this;
		}

		public global::PaperPlaneTools.Alert SetNegativeButton(string title, global::System.Action handler = null)
		{
			global::UnityEngine.Debug.LogError(title + " is negative button");
			NegativeButton = new global::PaperPlaneTools.AlertButton(title, handler);
			return this;
		}

		public global::PaperPlaneTools.Alert SetNeutralButton(string title, global::System.Action handler = null)
		{
			NeutralButton = new global::PaperPlaneTools.AlertButton(title, handler);
			return this;
		}

		public void ClearPositiveButton()
		{
			PositiveButton = null;
		}

		public void ClearNeutralButton()
		{
			NeutralButton = null;
		}

		public void ClearNegativeButton()
		{
			NegativeButton = null;
		}

		public global::PaperPlaneTools.Alert AddOptions(object opt)
		{
			Options.Add(opt);
			return this;
		}

		public global::PaperPlaneTools.Alert SetOptions(global::System.Collections.Generic.List<object> options)
		{
			Options = options;
			return this;
		}

		public global::PaperPlaneTools.Alert SetOnDismiss(global::System.Action handler)
		{
			OnDismiss = handler;
			return this;
		}

		public global::PaperPlaneTools.Alert SetAdapter(global::PaperPlaneTools.IAlertPlatformAdapter adaper)
		{
			Adapter = adaper;
			return this;
		}

		public void Show()
		{
			global::PaperPlaneTools.AlertManager.Instance.Show(this);
		}

		public void Dismiss()
		{
			global::PaperPlaneTools.AlertManager.Instance.Dismiss(this);
		}
	}
}
