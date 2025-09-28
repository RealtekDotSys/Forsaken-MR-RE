namespace SRDebugger.Services
{
	public interface IDebugPanelService
	{
		bool IsLoaded { get; }

		bool IsVisible { get; set; }

		global::SRDebugger.DefaultTabs? ActiveTab { get; }

		event global::System.Action<global::SRDebugger.Services.IDebugPanelService, bool> VisibilityChanged;

		void Unload();

		void OpenTab(global::SRDebugger.DefaultTabs tab);
	}
}
