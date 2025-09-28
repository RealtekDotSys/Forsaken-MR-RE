namespace SRDebugger.Services
{
	public interface IDebugService
	{
		global::SRDebugger.Settings Settings { get; }

		bool IsDebugPanelVisible { get; }

		bool IsTriggerEnabled { get; set; }

		global::SRDebugger.Services.IDockConsoleService DockConsole { get; }

		bool IsProfilerDocked { get; set; }

		event global::SRDebugger.VisibilityChangedDelegate PanelVisibilityChanged;

		void AddSystemInfo(global::SRDebugger.InfoEntry entry, string category = "Default");

		void ShowDebugPanel(bool requireEntryCode = true);

		void ShowDebugPanel(global::SRDebugger.DefaultTabs tab, bool requireEntryCode = true);

		void HideDebugPanel();

		void DestroyDebugPanel();

		void AddOptionContainer(object container);

		void RemoveOptionContainer(object container);

		void PinAllOptions(string category);

		void UnpinAllOptions(string category);

		void PinOption(string name);

		void UnpinOption(string name);

		void ClearPinnedOptions();

		void ShowBugReportSheet(global::SRDebugger.ActionCompleteCallback onComplete = null, bool takeScreenshot = true, string descriptionContent = null);

		global::UnityEngine.RectTransform EnableWorldSpaceMode();
	}
}
