namespace SRDebugger.Internal
{
	public static class Service
	{
		private static global::SRDebugger.Services.IConsoleService _consoleService;

		private static global::SRDebugger.Services.IDebugPanelService _debugPanelService;

		private static global::SRDebugger.Services.IDebugTriggerService _debugTriggerService;

		private static global::SRDebugger.Services.IPinnedUIService _pinnedUiService;

		private static global::SRDebugger.Services.IDebugCameraService _debugCameraService;

		private static global::SRDebugger.Services.IOptionsService _optionsService;

		private static global::SRDebugger.Services.IDockConsoleService _dockConsoleService;

		public static global::SRDebugger.Services.IConsoleService Console
		{
			get
			{
				if (_consoleService == null)
				{
					_consoleService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IConsoleService>();
				}
				return _consoleService;
			}
		}

		public static global::SRDebugger.Services.IDockConsoleService DockConsole
		{
			get
			{
				if (_dockConsoleService == null)
				{
					_dockConsoleService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IDockConsoleService>();
				}
				return _dockConsoleService;
			}
		}

		public static global::SRDebugger.Services.IDebugPanelService Panel
		{
			get
			{
				if (_debugPanelService == null)
				{
					_debugPanelService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IDebugPanelService>();
				}
				return _debugPanelService;
			}
		}

		public static global::SRDebugger.Services.IDebugTriggerService Trigger
		{
			get
			{
				if (_debugTriggerService == null)
				{
					_debugTriggerService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IDebugTriggerService>();
				}
				return _debugTriggerService;
			}
		}

		public static global::SRDebugger.Services.IPinnedUIService PinnedUI
		{
			get
			{
				if (_pinnedUiService == null)
				{
					_pinnedUiService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IPinnedUIService>();
				}
				return _pinnedUiService;
			}
		}

		public static global::SRDebugger.Services.IDebugCameraService DebugCamera
		{
			get
			{
				if (_debugCameraService == null)
				{
					_debugCameraService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IDebugCameraService>();
				}
				return _debugCameraService;
			}
		}

		public static global::SRDebugger.Services.IOptionsService Options
		{
			get
			{
				if (_optionsService == null)
				{
					_optionsService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IOptionsService>();
				}
				return _optionsService;
			}
		}
	}
}
