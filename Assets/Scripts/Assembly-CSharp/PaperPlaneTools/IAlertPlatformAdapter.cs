namespace PaperPlaneTools
{
	public interface IAlertPlatformAdapter
	{
		void SetOnDismiss(global::System.Action action);

		void Show(global::PaperPlaneTools.Alert alert);

		void Dismiss();

		void HandleEvent(string name, string value);
	}
}
