namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IDebugService))]
	public class SRDebugService : global::SRDebugger.Services.IDebugService
	{
		private readonly global::SRDebugger.Services.IDebugPanelService _debugPanelService;

		private readonly global::SRDebugger.Services.IDebugTriggerService _debugTrigger;

		private readonly global::SRDebugger.Services.ISystemInformationService _informationService;

		private readonly global::SRDebugger.Services.IOptionsService _optionsService;

		private readonly global::SRDebugger.Services.IPinnedUIService _pinnedUiService;

		private bool _entryCodeEnabled;

		private bool _hasAuthorised;

		private global::SRDebugger.DefaultTabs? _queuedTab;

		private global::UnityEngine.RectTransform _worldSpaceTransform;

		public global::SRDebugger.Settings Settings => global::SRDebugger.Settings.Instance;

		public bool IsDebugPanelVisible => _debugPanelService.IsVisible;

		public bool IsTriggerEnabled
		{
			get
			{
				return _debugTrigger.IsEnabled;
			}
			set
			{
				_debugTrigger.IsEnabled = value;
			}
		}

		public bool IsProfilerDocked
		{
			get
			{
				return global::SRDebugger.Internal.Service.PinnedUI.IsProfilerPinned;
			}
			set
			{
				global::SRDebugger.Internal.Service.PinnedUI.IsProfilerPinned = value;
			}
		}

		public global::SRDebugger.Services.IDockConsoleService DockConsole => global::SRDebugger.Internal.Service.DockConsole;

		public event global::SRDebugger.VisibilityChangedDelegate PanelVisibilityChanged;

		public SRDebugService()
		{
			global::SRF.Service.SRServiceManager.RegisterService<global::SRDebugger.Services.IDebugService>(this);
			global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IProfilerService>();
			_debugTrigger = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IDebugTriggerService>();
			_informationService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.ISystemInformationService>();
			_pinnedUiService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IPinnedUIService>();
			_optionsService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IOptionsService>();
			_debugPanelService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IDebugPanelService>();
			_debugPanelService.VisibilityChanged += DebugPanelServiceOnVisibilityChanged;
			_debugTrigger.IsEnabled = Settings.EnableTrigger == global::SRDebugger.Settings.TriggerEnableModes.Enabled || (Settings.EnableTrigger == global::SRDebugger.Settings.TriggerEnableModes.MobileOnly && global::UnityEngine.Application.isMobilePlatform) || (Settings.EnableTrigger == global::SRDebugger.Settings.TriggerEnableModes.DevelopmentBuildsOnly && global::UnityEngine.Debug.isDebugBuild);
			_debugTrigger.Position = Settings.TriggerPosition;
			if (Settings.EnableKeyboardShortcuts)
			{
				global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.Implementation.KeyboardShortcutListenerService>();
			}
			_entryCodeEnabled = global::SRDebugger.Settings.Instance.RequireCode && global::SRDebugger.Settings.Instance.EntryCode.Count == 4;
			if (global::SRDebugger.Settings.Instance.RequireCode && !_entryCodeEnabled)
			{
				global::UnityEngine.Debug.LogError("[SRDebugger] RequireCode is enabled, but pin is not 4 digits");
			}
			global::UnityEngine.Object.DontDestroyOnLoad(global::SRF.Hierarchy.Get("SRDebugger").gameObject);
		}

		public void AddSystemInfo(global::SRDebugger.InfoEntry entry, string category = "Default")
		{
			_informationService.Add(entry, category);
		}

		public void ShowDebugPanel(bool requireEntryCode = true)
		{
			if (requireEntryCode && _entryCodeEnabled && !_hasAuthorised)
			{
				PromptEntryCode();
			}
			else
			{
				_debugPanelService.IsVisible = true;
			}
		}

		public void ShowDebugPanel(global::SRDebugger.DefaultTabs tab, bool requireEntryCode = true)
		{
			if (requireEntryCode && _entryCodeEnabled && !_hasAuthorised)
			{
				_queuedTab = tab;
				PromptEntryCode();
			}
			else
			{
				_debugPanelService.IsVisible = true;
				_debugPanelService.OpenTab(tab);
			}
		}

		public void HideDebugPanel()
		{
			_debugPanelService.IsVisible = false;
		}

		public void DestroyDebugPanel()
		{
			_debugPanelService.IsVisible = false;
			_debugPanelService.Unload();
		}

		public void AddOptionContainer(object container)
		{
			_optionsService.AddContainer(container);
		}

		public void RemoveOptionContainer(object container)
		{
			_optionsService.RemoveContainer(container);
		}

		public void PinAllOptions(string category)
		{
			foreach (global::SRDebugger.Internal.OptionDefinition option in _optionsService.Options)
			{
				if (option.Category == category)
				{
					_pinnedUiService.Pin(option);
				}
			}
		}

		public void UnpinAllOptions(string category)
		{
			foreach (global::SRDebugger.Internal.OptionDefinition option in _optionsService.Options)
			{
				if (option.Category == category)
				{
					_pinnedUiService.Unpin(option);
				}
			}
		}

		public void PinOption(string name)
		{
			foreach (global::SRDebugger.Internal.OptionDefinition option in _optionsService.Options)
			{
				if (option.Name == name)
				{
					_pinnedUiService.Pin(option);
				}
			}
		}

		public void UnpinOption(string name)
		{
			foreach (global::SRDebugger.Internal.OptionDefinition option in _optionsService.Options)
			{
				if (option.Name == name)
				{
					_pinnedUiService.Unpin(option);
				}
			}
		}

		public void ClearPinnedOptions()
		{
			_pinnedUiService.UnpinAll();
		}

		public void ShowBugReportSheet(global::SRDebugger.ActionCompleteCallback onComplete = null, bool takeScreenshot = true, string descriptionContent = null)
		{
			global::SRDebugger.Services.Implementation.BugReportPopoverService service = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.Implementation.BugReportPopoverService>();
			if (service.IsShowingPopover)
			{
				return;
			}
			service.ShowBugReporter(delegate(bool succeed, string message)
			{
				if (onComplete != null)
				{
					onComplete(succeed);
				}
			}, takeScreenshot, descriptionContent);
		}

		private void DebugPanelServiceOnVisibilityChanged(global::SRDebugger.Services.IDebugPanelService debugPanelService, bool b)
		{
			if (this.PanelVisibilityChanged == null)
			{
				return;
			}
			try
			{
				this.PanelVisibilityChanged(b);
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogError("[SRDebugger] Event target threw exception (IDebugService.PanelVisiblityChanged)");
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		private void PromptEntryCode()
		{
			global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IPinEntryService>().ShowPinEntry(global::SRDebugger.Settings.Instance.EntryCode, global::SRDebugger.Internal.SRDebugStrings.Current.PinEntryPrompt, delegate(bool entered)
			{
				if (entered)
				{
					if (!global::SRDebugger.Settings.Instance.RequireEntryCodeEveryTime)
					{
						_hasAuthorised = true;
					}
					if (_queuedTab.HasValue)
					{
						global::SRDebugger.DefaultTabs value = _queuedTab.Value;
						_queuedTab = null;
						ShowDebugPanel(value, requireEntryCode: false);
					}
					else
					{
						ShowDebugPanel(requireEntryCode: false);
					}
				}
				_queuedTab = null;
			});
		}

		public global::UnityEngine.RectTransform EnableWorldSpaceMode()
		{
			if (_worldSpaceTransform != null)
			{
				return _worldSpaceTransform;
			}
			if (global::SRDebugger.Settings.Instance.UseDebugCamera)
			{
				throw new global::System.InvalidOperationException("UseDebugCamera cannot be enabled at the same time as EnableWorldSpaceMode.");
			}
			_debugPanelService.IsVisible = true;
			global::SRDebugger.UI.DebugPanelRoot rootObject = ((global::SRDebugger.Services.Implementation.DebugPanelServiceImpl)_debugPanelService).RootObject;
			global::SRF.SRFGameObjectExtensions.RemoveComponentIfExists<global::SRF.UI.SRRetinaScaler>(rootObject.Canvas.gameObject);
			global::SRF.SRFGameObjectExtensions.RemoveComponentIfExists<global::UnityEngine.UI.CanvasScaler>(rootObject.Canvas.gameObject);
			rootObject.Canvas.renderMode = global::UnityEngine.RenderMode.WorldSpace;
			global::UnityEngine.RectTransform component = rootObject.Canvas.GetComponent<global::UnityEngine.RectTransform>();
			component.sizeDelta = new global::UnityEngine.Vector2(1024f, 768f);
			component.position = global::UnityEngine.Vector3.zero;
			return _worldSpaceTransform = component;
		}
	}
}
