namespace PaperPlaneTools
{
	public class AlertButton
	{
		public string Title { get; private set; }

		public global::System.Action Handler { get; private set; }

		public AlertButton(string title, global::System.Action handler)
		{
			Title = title;
			Handler = handler;
		}
	}
}
