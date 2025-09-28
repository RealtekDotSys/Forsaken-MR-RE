namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IPinnedUIService))]
	public class PinnedUIServiceImpl : global::SRF.Service.SRServiceBase<global::SRDebugger.Services.IPinnedUIService>, global::SRDebugger.Services.IPinnedUIService
	{
		private readonly global::System.Collections.Generic.List<global::SRDebugger.UI.Controls.OptionsControlBase> _controlList = new global::System.Collections.Generic.List<global::SRDebugger.UI.Controls.OptionsControlBase>();

		private readonly global::System.Collections.Generic.Dictionary<global::SRDebugger.Internal.OptionDefinition, global::SRDebugger.UI.Controls.OptionsControlBase> _pinnedObjects = new global::System.Collections.Generic.Dictionary<global::SRDebugger.Internal.OptionDefinition, global::SRDebugger.UI.Controls.OptionsControlBase>();

		private bool _queueRefresh;

		private global::SRDebugger.UI.Other.PinnedUIRoot _uiRoot;

		public global::SRDebugger.UI.Other.DockConsoleController DockConsoleController
		{
			get
			{
				if (_uiRoot == null)
				{
					Load();
				}
				return _uiRoot.DockConsoleController;
			}
		}

		public bool IsProfilerPinned
		{
			get
			{
				if (_uiRoot == null)
				{
					return false;
				}
				return _uiRoot.Profiler.activeSelf;
			}
			set
			{
				if (_uiRoot == null)
				{
					Load();
				}
				_uiRoot.Profiler.SetActive(value);
			}
		}

		public event global::System.Action<global::SRDebugger.Internal.OptionDefinition, bool> OptionPinStateChanged;

		public void Pin(global::SRDebugger.Internal.OptionDefinition obj, int order = -1)
		{
			if (_uiRoot == null)
			{
				Load();
			}
			if (!_pinnedObjects.ContainsKey(obj))
			{
				global::SRDebugger.UI.Controls.OptionsControlBase optionsControlBase = global::SRDebugger.Internal.OptionControlFactory.CreateControl(obj);
				optionsControlBase.CachedTransform.SetParent(_uiRoot.Container, worldPositionStays: false);
				if (order >= 0)
				{
					optionsControlBase.CachedTransform.SetSiblingIndex(order);
				}
				_pinnedObjects.Add(obj, optionsControlBase);
				_controlList.Add(optionsControlBase);
				OnPinnedStateChanged(obj, isPinned: true);
			}
		}

		public void Unpin(global::SRDebugger.Internal.OptionDefinition obj)
		{
			if (_pinnedObjects.ContainsKey(obj))
			{
				global::SRDebugger.UI.Controls.OptionsControlBase optionsControlBase = _pinnedObjects[obj];
				_pinnedObjects.Remove(obj);
				_controlList.Remove(optionsControlBase);
				global::UnityEngine.Object.Destroy(optionsControlBase.CachedGameObject);
				OnPinnedStateChanged(obj, isPinned: false);
			}
		}

		private void OnPinnedStateChanged(global::SRDebugger.Internal.OptionDefinition option, bool isPinned)
		{
			if (this.OptionPinStateChanged != null)
			{
				this.OptionPinStateChanged(option, isPinned);
			}
		}

		public void UnpinAll()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::SRDebugger.Internal.OptionDefinition, global::SRDebugger.UI.Controls.OptionsControlBase> pinnedObject in _pinnedObjects)
			{
				global::UnityEngine.Object.Destroy(pinnedObject.Value.CachedGameObject);
			}
			_pinnedObjects.Clear();
			_controlList.Clear();
		}

		public bool HasPinned(global::SRDebugger.Internal.OptionDefinition option)
		{
			return _pinnedObjects.ContainsKey(option);
		}

		protected override void Awake()
		{
			base.Awake();
			base.CachedTransform.SetParent(global::SRF.Hierarchy.Get("SRDebugger"));
		}

		private void Load()
		{
			global::SRDebugger.UI.Other.PinnedUIRoot pinnedUIRoot = global::UnityEngine.Resources.Load<global::SRDebugger.UI.Other.PinnedUIRoot>("SRDebugger/UI/Prefabs/PinnedUI");
			if (pinnedUIRoot == null)
			{
				global::UnityEngine.Debug.LogError("[SRDebugger.PinnedUI] Error loading ui prefab");
				return;
			}
			global::SRDebugger.UI.Other.PinnedUIRoot pinnedUIRoot2 = SRInstantiate.Instantiate(pinnedUIRoot);
			pinnedUIRoot2.CachedTransform.SetParent(base.CachedTransform, worldPositionStays: false);
			_uiRoot = pinnedUIRoot2;
			UpdateAnchors();
			SRDebug.Instance.PanelVisibilityChanged += OnDebugPanelVisibilityChanged;
			global::SRDebugger.Internal.Service.Options.OptionsUpdated += OnOptionsUpdated;
			global::SRDebugger.Internal.Service.Options.OptionsValueUpdated += OptionsOnPropertyChanged;
		}

		private void UpdateAnchors()
		{
			switch (global::SRDebugger.Settings.Instance.ProfilerAlignment)
			{
			case global::SRDebugger.PinAlignment.TopLeft:
			case global::SRDebugger.PinAlignment.BottomLeft:
			case global::SRDebugger.PinAlignment.CenterLeft:
				_uiRoot.Profiler.transform.SetSiblingIndex(0);
				break;
			case global::SRDebugger.PinAlignment.TopRight:
			case global::SRDebugger.PinAlignment.BottomRight:
			case global::SRDebugger.PinAlignment.CenterRight:
				_uiRoot.Profiler.transform.SetSiblingIndex(1);
				break;
			}
			switch (global::SRDebugger.Settings.Instance.ProfilerAlignment)
			{
			case global::SRDebugger.PinAlignment.TopLeft:
			case global::SRDebugger.PinAlignment.TopRight:
				_uiRoot.ProfilerVerticalLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.UpperCenter;
				break;
			case global::SRDebugger.PinAlignment.BottomLeft:
			case global::SRDebugger.PinAlignment.BottomRight:
				_uiRoot.ProfilerVerticalLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.LowerCenter;
				break;
			case global::SRDebugger.PinAlignment.CenterLeft:
			case global::SRDebugger.PinAlignment.CenterRight:
				_uiRoot.ProfilerVerticalLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.MiddleCenter;
				break;
			}
			_uiRoot.ProfilerHandleManager.SetAlignment(global::SRDebugger.Settings.Instance.ProfilerAlignment);
			switch (global::SRDebugger.Settings.Instance.OptionsAlignment)
			{
			case global::SRDebugger.PinAlignment.BottomLeft:
				_uiRoot.OptionsLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.LowerLeft;
				break;
			case global::SRDebugger.PinAlignment.TopLeft:
				_uiRoot.OptionsLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.UpperLeft;
				break;
			case global::SRDebugger.PinAlignment.BottomRight:
				_uiRoot.OptionsLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.LowerRight;
				break;
			case global::SRDebugger.PinAlignment.TopRight:
				_uiRoot.OptionsLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.UpperRight;
				break;
			case global::SRDebugger.PinAlignment.BottomCenter:
				_uiRoot.OptionsLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.LowerCenter;
				break;
			case global::SRDebugger.PinAlignment.TopCenter:
				_uiRoot.OptionsLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.UpperCenter;
				break;
			case global::SRDebugger.PinAlignment.CenterLeft:
				_uiRoot.OptionsLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.MiddleLeft;
				break;
			case global::SRDebugger.PinAlignment.CenterRight:
				_uiRoot.OptionsLayoutGroup.childAlignment = global::UnityEngine.TextAnchor.MiddleRight;
				break;
			}
		}

		protected override void Update()
		{
			base.Update();
			if (_queueRefresh)
			{
				_queueRefresh = false;
				Refresh();
			}
		}

		private void OnOptionsUpdated(object sender, global::System.EventArgs eventArgs)
		{
			foreach (global::SRDebugger.Internal.OptionDefinition item in global::System.Linq.Enumerable.ToList(_pinnedObjects.Keys))
			{
				if (!global::SRDebugger.Internal.Service.Options.Options.Contains(item))
				{
					Unpin(item);
				}
			}
		}

		private void OptionsOnPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs propertyChangedEventArgs)
		{
			_queueRefresh = true;
		}

		private void OnDebugPanelVisibilityChanged(bool isVisible)
		{
			if (!isVisible)
			{
				_queueRefresh = true;
			}
		}

		private void Refresh()
		{
			for (int i = 0; i < _controlList.Count; i++)
			{
				_controlList[i].Refresh();
			}
		}
	}
}
