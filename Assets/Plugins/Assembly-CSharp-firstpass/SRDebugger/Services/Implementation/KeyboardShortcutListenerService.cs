namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.Implementation.KeyboardShortcutListenerService))]
	public class KeyboardShortcutListenerService : global::SRF.Service.SRServiceBase<global::SRDebugger.Services.Implementation.KeyboardShortcutListenerService>
	{
		private global::System.Collections.Generic.List<global::SRDebugger.Settings.KeyboardShortcut> _shortcuts;

		protected override void Awake()
		{
			base.Awake();
			base.CachedTransform.SetParent(global::SRF.Hierarchy.Get("SRDebugger"));
			_shortcuts = new global::System.Collections.Generic.List<global::SRDebugger.Settings.KeyboardShortcut>(global::SRDebugger.Settings.Instance.KeyboardShortcuts);
		}

		private void ToggleTab(global::SRDebugger.DefaultTabs t)
		{
			global::SRDebugger.DefaultTabs? activeTab = global::SRDebugger.Internal.Service.Panel.ActiveTab;
			if (global::SRDebugger.Internal.Service.Panel.IsVisible && activeTab.HasValue && activeTab.Value == t)
			{
				SRDebug.Instance.HideDebugPanel();
			}
			else
			{
				SRDebug.Instance.ShowDebugPanel(t);
			}
		}

		private void ExecuteShortcut(global::SRDebugger.Settings.KeyboardShortcut shortcut)
		{
			switch (shortcut.Action)
			{
			case global::SRDebugger.Settings.ShortcutActions.OpenSystemInfoTab:
				ToggleTab(global::SRDebugger.DefaultTabs.SystemInformation);
				break;
			case global::SRDebugger.Settings.ShortcutActions.OpenConsoleTab:
				ToggleTab(global::SRDebugger.DefaultTabs.Console);
				break;
			case global::SRDebugger.Settings.ShortcutActions.OpenOptionsTab:
				ToggleTab(global::SRDebugger.DefaultTabs.Options);
				break;
			case global::SRDebugger.Settings.ShortcutActions.OpenProfilerTab:
				ToggleTab(global::SRDebugger.DefaultTabs.Profiler);
				break;
			case global::SRDebugger.Settings.ShortcutActions.OpenBugReporterTab:
				ToggleTab(global::SRDebugger.DefaultTabs.BugReporter);
				break;
			case global::SRDebugger.Settings.ShortcutActions.ClosePanel:
				SRDebug.Instance.HideDebugPanel();
				break;
			case global::SRDebugger.Settings.ShortcutActions.OpenPanel:
				SRDebug.Instance.ShowDebugPanel();
				break;
			case global::SRDebugger.Settings.ShortcutActions.TogglePanel:
				if (SRDebug.Instance.IsDebugPanelVisible)
				{
					SRDebug.Instance.HideDebugPanel();
				}
				else
				{
					SRDebug.Instance.ShowDebugPanel();
				}
				break;
			case global::SRDebugger.Settings.ShortcutActions.ShowBugReportPopover:
				SRDebug.Instance.ShowBugReportSheet();
				break;
			case global::SRDebugger.Settings.ShortcutActions.ToggleDockedConsole:
				SRDebug.Instance.DockConsole.IsVisible = !SRDebug.Instance.DockConsole.IsVisible;
				break;
			case global::SRDebugger.Settings.ShortcutActions.ToggleDockedProfiler:
				SRDebug.Instance.IsProfilerDocked = !SRDebug.Instance.IsProfilerDocked;
				break;
			default:
				global::UnityEngine.Debug.LogWarning("[SRDebugger] Unhandled keyboard shortcut: " + shortcut.Action);
				break;
			}
		}

		protected override void Update()
		{
			base.Update();
			if (global::SRDebugger.Settings.Instance.KeyboardEscapeClose && global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) && global::SRDebugger.Internal.Service.Panel.IsVisible)
			{
				SRDebug.Instance.HideDebugPanel();
			}
			bool flag = global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.RightControl);
			bool flag2 = global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftAlt) || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.RightAlt);
			bool flag3 = global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftShift) || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.RightShift);
			for (int i = 0; i < _shortcuts.Count; i++)
			{
				global::SRDebugger.Settings.KeyboardShortcut keyboardShortcut = _shortcuts[i];
				if ((!keyboardShortcut.Control || flag) && (!keyboardShortcut.Shift || flag3) && (!keyboardShortcut.Alt || flag2) && global::UnityEngine.Input.GetKeyDown(keyboardShortcut.Key))
				{
					ExecuteShortcut(keyboardShortcut);
					break;
				}
			}
		}
	}
}
