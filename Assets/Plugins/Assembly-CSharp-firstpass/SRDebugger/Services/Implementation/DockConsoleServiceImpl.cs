namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IDockConsoleService))]
	public class DockConsoleServiceImpl : global::SRDebugger.Services.IDockConsoleService
	{
		private global::SRDebugger.ConsoleAlignment _alignment;

		private global::SRDebugger.UI.Other.DockConsoleController _consoleRoot;

		private bool _didSuspendTrigger;

		private bool _isExpanded = true;

		private bool _isVisible;

		public bool IsVisible
		{
			get
			{
				return _isVisible;
			}
			set
			{
				if (value != _isVisible)
				{
					_isVisible = value;
					if (_consoleRoot == null && value)
					{
						Load();
					}
					else
					{
						_consoleRoot.CachedGameObject.SetActive(value);
					}
					CheckTrigger();
				}
			}
		}

		public bool IsExpanded
		{
			get
			{
				return _isExpanded;
			}
			set
			{
				if (value != _isExpanded)
				{
					_isExpanded = value;
					if (_consoleRoot == null && value)
					{
						Load();
					}
					else
					{
						_consoleRoot.SetDropdownVisibility(value);
					}
					CheckTrigger();
				}
			}
		}

		public global::SRDebugger.ConsoleAlignment Alignment
		{
			get
			{
				return _alignment;
			}
			set
			{
				_alignment = value;
				if (_consoleRoot != null)
				{
					_consoleRoot.SetAlignmentMode(value);
				}
				CheckTrigger();
			}
		}

		public DockConsoleServiceImpl()
		{
			_alignment = global::SRDebugger.Settings.Instance.ConsoleAlignment;
		}

		private void Load()
		{
			global::SRDebugger.Services.IPinnedUIService service = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IPinnedUIService>();
			if (service == null)
			{
				global::UnityEngine.Debug.LogError("[DockConsoleService] PinnedUIService not found");
				return;
			}
			global::SRDebugger.Services.Implementation.PinnedUIServiceImpl pinnedUIServiceImpl = service as global::SRDebugger.Services.Implementation.PinnedUIServiceImpl;
			if (pinnedUIServiceImpl == null)
			{
				global::UnityEngine.Debug.LogError("[DockConsoleService] Expected IPinnedUIService to be PinnedUIServiceImpl");
				return;
			}
			_consoleRoot = pinnedUIServiceImpl.DockConsoleController;
			_consoleRoot.SetDropdownVisibility(_isExpanded);
			_consoleRoot.IsVisible = _isVisible;
			_consoleRoot.SetAlignmentMode(_alignment);
			CheckTrigger();
		}

		private void CheckTrigger()
		{
			global::SRDebugger.ConsoleAlignment? consoleAlignment = null;
			switch (global::SRDebugger.Internal.Service.Trigger.Position)
			{
			case global::SRDebugger.PinAlignment.TopLeft:
			case global::SRDebugger.PinAlignment.TopRight:
			case global::SRDebugger.PinAlignment.TopCenter:
				consoleAlignment = global::SRDebugger.ConsoleAlignment.Top;
				break;
			case global::SRDebugger.PinAlignment.BottomLeft:
			case global::SRDebugger.PinAlignment.BottomRight:
			case global::SRDebugger.PinAlignment.BottomCenter:
				consoleAlignment = global::SRDebugger.ConsoleAlignment.Bottom;
				break;
			}
			bool flag = consoleAlignment.HasValue && IsVisible && Alignment == consoleAlignment.Value;
			if (_didSuspendTrigger && !flag)
			{
				global::SRDebugger.Internal.Service.Trigger.IsEnabled = true;
				_didSuspendTrigger = false;
			}
			else if (global::SRDebugger.Internal.Service.Trigger.IsEnabled && flag)
			{
				global::SRDebugger.Internal.Service.Trigger.IsEnabled = false;
				_didSuspendTrigger = true;
			}
		}
	}
}
