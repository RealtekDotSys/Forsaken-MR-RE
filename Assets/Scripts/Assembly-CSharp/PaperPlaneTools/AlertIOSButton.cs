namespace PaperPlaneTools
{
	public class AlertIOSButton
	{
		public enum Type
		{
			Default = 0,
			Cancel = 1,
			Destructive = 2
		}

		public global::PaperPlaneTools.AlertIOSButton.Type WhichButton { get; private set; }

		public string Title { get; private set; }

		public global::System.Action Handler { get; private set; }

		public bool IsPreferable { get; private set; }

		public AlertIOSButton(global::PaperPlaneTools.AlertIOSButton.Type whichButton, string title, global::System.Action handler, bool isPreferable)
		{
			WhichButton = whichButton;
			Title = title;
			Handler = handler;
			IsPreferable = isPreferable;
		}
	}
}
