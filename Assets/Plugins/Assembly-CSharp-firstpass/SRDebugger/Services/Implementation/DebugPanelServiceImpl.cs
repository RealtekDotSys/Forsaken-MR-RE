namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IDebugPanelService))]
	public class DebugPanelServiceImpl : global::UnityEngine.ScriptableObject, global::SRDebugger.Services.IDebugPanelService
	{
		private global::SRDebugger.UI.DebugPanelRoot _debugPanelRootObject;

		private bool _isVisible;

		private bool? _cursorWasVisible;

		private global::UnityEngine.CursorLockMode? _cursorLockMode;

		public global::SRDebugger.UI.DebugPanelRoot RootObject => _debugPanelRootObject;

		public bool IsLoaded => _debugPanelRootObject != null;

		public bool IsVisible
		{
			get
			{
				if (IsLoaded)
				{
					return _isVisible;
				}
				return false;
			}
			set
			{
				if (_isVisible == value)
				{
					return;
				}
				if (value)
				{
					if (!IsLoaded)
					{
						Load();
					}
					global::SRDebugger.Internal.SRDebuggerUtil.EnsureEventSystemExists();
					_debugPanelRootObject.CanvasGroup.alpha = 1f;
					_debugPanelRootObject.CanvasGroup.interactable = true;
					_debugPanelRootObject.CanvasGroup.blocksRaycasts = true;
					_cursorWasVisible = global::UnityEngine.Cursor.visible;
					_cursorLockMode = global::UnityEngine.Cursor.lockState;
					if (global::SRDebugger.Settings.Instance.AutomaticallyShowCursor)
					{
						global::UnityEngine.Cursor.visible = true;
						global::UnityEngine.Cursor.lockState = global::UnityEngine.CursorLockMode.None;
					}
				}
				else
				{
					if (IsLoaded)
					{
						_debugPanelRootObject.CanvasGroup.alpha = 0f;
						_debugPanelRootObject.CanvasGroup.interactable = false;
						_debugPanelRootObject.CanvasGroup.blocksRaycasts = false;
					}
					if (_cursorWasVisible.HasValue)
					{
						global::UnityEngine.Cursor.visible = _cursorWasVisible.Value;
						_cursorWasVisible = null;
					}
					if (_cursorLockMode.HasValue)
					{
						global::UnityEngine.Cursor.lockState = _cursorLockMode.Value;
						_cursorLockMode = null;
					}
				}
				_isVisible = value;
				if (this.VisibilityChanged != null)
				{
					this.VisibilityChanged(this, _isVisible);
				}
			}
		}

		public global::SRDebugger.DefaultTabs? ActiveTab
		{
			get
			{
				if (_debugPanelRootObject == null)
				{
					return null;
				}
				return _debugPanelRootObject.TabController.ActiveTab;
			}
		}

		public event global::System.Action<global::SRDebugger.Services.IDebugPanelService, bool> VisibilityChanged;

		public void OpenTab(global::SRDebugger.DefaultTabs tab)
		{
			if (!IsVisible)
			{
				IsVisible = true;
			}
			_debugPanelRootObject.TabController.OpenTab(tab);
		}

		public void Unload()
		{
			if (!(_debugPanelRootObject == null))
			{
				IsVisible = false;
				_debugPanelRootObject.CachedGameObject.SetActive(value: false);
				global::UnityEngine.Object.Destroy(_debugPanelRootObject.CachedGameObject);
				_debugPanelRootObject = null;
			}
		}

		private void Load()
		{
			global::SRDebugger.UI.DebugPanelRoot debugPanelRoot = global::UnityEngine.Resources.Load<global::SRDebugger.UI.DebugPanelRoot>("SRDebugger/UI/Prefabs/DebugPanel");
			if (debugPanelRoot == null)
			{
				global::UnityEngine.Debug.LogError("[SRDebugger] Error loading debug panel prefab");
				return;
			}
			_debugPanelRootObject = SRInstantiate.Instantiate(debugPanelRoot);
			_debugPanelRootObject.name = "Panel";
			global::UnityEngine.Object.DontDestroyOnLoad(_debugPanelRootObject);
			_debugPanelRootObject.CachedTransform.SetParent(global::SRF.Hierarchy.Get("SRDebugger"), worldPositionStays: true);
			global::SRDebugger.Internal.SRDebuggerUtil.EnsureEventSystemExists();
		}
	}
}
