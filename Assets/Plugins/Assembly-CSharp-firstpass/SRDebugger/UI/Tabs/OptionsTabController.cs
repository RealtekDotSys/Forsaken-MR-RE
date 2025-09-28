namespace SRDebugger.UI.Tabs
{
	public class OptionsTabController : global::SRF.SRMonoBehaviourEx
	{
		private class CategoryInstance
		{
			public readonly global::System.Collections.Generic.List<global::SRDebugger.UI.Controls.OptionsControlBase> Options = new global::System.Collections.Generic.List<global::SRDebugger.UI.Controls.OptionsControlBase>();

			public global::SRDebugger.UI.Other.CategoryGroup CategoryGroup { get; private set; }

			public CategoryInstance(global::SRDebugger.UI.Other.CategoryGroup group)
			{
				CategoryGroup = group;
			}
		}

		private readonly global::System.Collections.Generic.List<global::SRDebugger.UI.Controls.OptionsControlBase> _controls = new global::System.Collections.Generic.List<global::SRDebugger.UI.Controls.OptionsControlBase>();

		private readonly global::System.Collections.Generic.List<global::SRDebugger.UI.Tabs.OptionsTabController.CategoryInstance> _categories = new global::System.Collections.Generic.List<global::SRDebugger.UI.Tabs.OptionsTabController.CategoryInstance>();

		private readonly global::System.Collections.Generic.Dictionary<global::SRDebugger.Internal.OptionDefinition, global::SRDebugger.UI.Controls.OptionsControlBase> _options = new global::System.Collections.Generic.Dictionary<global::SRDebugger.Internal.OptionDefinition, global::SRDebugger.UI.Controls.OptionsControlBase>();

		private bool _queueRefresh;

		private bool _selectionModeEnabled;

		private global::UnityEngine.Canvas _optionCanvas;

		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Controls.Data.ActionControl ActionControlPrefab;

		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Other.CategoryGroup CategoryGroupPrefab;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform ContentContainer;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject NoOptionsNotice;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle PinButton;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject PinPromptSpacer;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject PinPromptText;

		private bool _isTogglingCategory;

		protected override void Start()
		{
			base.Start();
			PinButton.onValueChanged.AddListener(SetSelectionModeEnabled);
			PinPromptText.SetActive(value: false);
			Populate();
			_optionCanvas = GetComponent<global::UnityEngine.Canvas>();
			global::SRDebugger.Internal.Service.Options.OptionsUpdated += OnOptionsUpdated;
			global::SRDebugger.Internal.Service.Options.OptionsValueUpdated += OnOptionsValueChanged;
			global::SRDebugger.Internal.Service.PinnedUI.OptionPinStateChanged += OnOptionPinnedStateChanged;
		}

		private void OnOptionPinnedStateChanged(global::SRDebugger.Internal.OptionDefinition optionDefinition, bool isPinned)
		{
			if (_options.ContainsKey(optionDefinition))
			{
				_options[optionDefinition].IsSelected = isPinned;
			}
		}

		private void OnOptionsUpdated(object sender, global::System.EventArgs eventArgs)
		{
			Clear();
			Populate();
		}

		private void OnOptionsValueChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs propertyChangedEventArgs)
		{
			_queueRefresh = true;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			global::SRDebugger.Internal.Service.Panel.VisibilityChanged += PanelOnVisibilityChanged;
		}

		protected override void OnDisable()
		{
			SetSelectionModeEnabled(isEnabled: false);
			if (global::SRDebugger.Internal.Service.Panel != null)
			{
				global::SRDebugger.Internal.Service.Panel.VisibilityChanged -= PanelOnVisibilityChanged;
			}
			base.OnDisable();
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

		private void PanelOnVisibilityChanged(global::SRDebugger.Services.IDebugPanelService debugPanelService, bool b)
		{
			if (!b)
			{
				SetSelectionModeEnabled(isEnabled: false);
				Refresh();
			}
			else if (b && base.CachedGameObject.activeInHierarchy)
			{
				Refresh();
			}
			if (_optionCanvas != null)
			{
				_optionCanvas.enabled = b;
			}
		}

		public void SetSelectionModeEnabled(bool isEnabled)
		{
			if (_selectionModeEnabled == isEnabled)
			{
				return;
			}
			_selectionModeEnabled = isEnabled;
			PinButton.isOn = isEnabled;
			PinPromptText.SetActive(isEnabled);
			foreach (global::System.Collections.Generic.KeyValuePair<global::SRDebugger.Internal.OptionDefinition, global::SRDebugger.UI.Controls.OptionsControlBase> option in _options)
			{
				option.Value.SelectionModeEnabled = isEnabled;
				if (isEnabled)
				{
					option.Value.IsSelected = global::SRDebugger.Internal.Service.PinnedUI.HasPinned(option.Key);
				}
			}
			foreach (global::SRDebugger.UI.Tabs.OptionsTabController.CategoryInstance category in _categories)
			{
				category.CategoryGroup.SelectionModeEnabled = isEnabled;
			}
			RefreshCategorySelection();
		}

		private void Refresh()
		{
			for (int i = 0; i < _options.Count; i++)
			{
				_controls[i].Refresh();
				_controls[i].IsSelected = global::SRDebugger.Internal.Service.PinnedUI.HasPinned(_controls[i].Option);
			}
		}

		private void CommitPinnedOptions()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::SRDebugger.Internal.OptionDefinition, global::SRDebugger.UI.Controls.OptionsControlBase> option in _options)
			{
				global::SRDebugger.UI.Controls.OptionsControlBase value = option.Value;
				if (value.IsSelected && !global::SRDebugger.Internal.Service.PinnedUI.HasPinned(option.Key))
				{
					global::SRDebugger.Internal.Service.PinnedUI.Pin(option.Key);
				}
				else if (!value.IsSelected && global::SRDebugger.Internal.Service.PinnedUI.HasPinned(option.Key))
				{
					global::SRDebugger.Internal.Service.PinnedUI.Unpin(option.Key);
				}
			}
		}

		private void RefreshCategorySelection()
		{
			_isTogglingCategory = true;
			foreach (global::SRDebugger.UI.Tabs.OptionsTabController.CategoryInstance category in _categories)
			{
				bool isSelected = true;
				for (int i = 0; i < category.Options.Count; i++)
				{
					if (!category.Options[i].IsSelected)
					{
						isSelected = false;
						break;
					}
				}
				category.CategoryGroup.IsSelected = isSelected;
			}
			_isTogglingCategory = false;
		}

		private void OnOptionSelectionToggle(bool selected)
		{
			if (!_isTogglingCategory)
			{
				RefreshCategorySelection();
				CommitPinnedOptions();
			}
		}

		private void OnCategorySelectionToggle(global::SRDebugger.UI.Tabs.OptionsTabController.CategoryInstance category, bool selected)
		{
			_isTogglingCategory = true;
			for (int i = 0; i < category.Options.Count; i++)
			{
				category.Options[i].IsSelected = selected;
			}
			_isTogglingCategory = false;
			CommitPinnedOptions();
		}

		protected void Populate()
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::SRDebugger.Internal.OptionDefinition>> dictionary = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::SRDebugger.Internal.OptionDefinition>>();
			foreach (global::SRDebugger.Internal.OptionDefinition option in global::SRDebugger.Internal.Service.Options.Options)
			{
				if (!dictionary.TryGetValue(option.Category, out var value))
				{
					value = new global::System.Collections.Generic.List<global::SRDebugger.Internal.OptionDefinition>();
					dictionary.Add(option.Category, value);
				}
				value.Add(option);
			}
			bool flag = false;
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<global::SRDebugger.Internal.OptionDefinition>> item in dictionary)
			{
				if (item.Value.Count != 0)
				{
					flag = true;
					CreateCategory(item.Key, item.Value);
				}
			}
			if (flag)
			{
				NoOptionsNotice.SetActive(value: false);
			}
		}

		protected void CreateCategory(string title, global::System.Collections.Generic.List<global::SRDebugger.Internal.OptionDefinition> options)
		{
			options.Sort((global::SRDebugger.Internal.OptionDefinition d1, global::SRDebugger.Internal.OptionDefinition d2) => d1.SortPriority.CompareTo(d2.SortPriority));
			global::SRDebugger.UI.Other.CategoryGroup categoryGroup = SRInstantiate.Instantiate(CategoryGroupPrefab);
			global::SRDebugger.UI.Tabs.OptionsTabController.CategoryInstance categoryInstance = new global::SRDebugger.UI.Tabs.OptionsTabController.CategoryInstance(categoryGroup);
			_categories.Add(categoryInstance);
			categoryGroup.CachedTransform.SetParent(ContentContainer, worldPositionStays: false);
			categoryGroup.Header.text = title;
			categoryGroup.SelectionModeEnabled = false;
			categoryInstance.CategoryGroup.SelectionToggle.onValueChanged.AddListener(delegate(bool b)
			{
				OnCategorySelectionToggle(categoryInstance, b);
			});
			foreach (global::SRDebugger.Internal.OptionDefinition option in options)
			{
				global::SRDebugger.UI.Controls.OptionsControlBase optionsControlBase = global::SRDebugger.Internal.OptionControlFactory.CreateControl(option, title);
				if (optionsControlBase == null)
				{
					global::UnityEngine.Debug.LogError(global::SRF.SRFStringExtensions.Fmt("[SRDebugger.OptionsTab] Failed to create option control for {0}", option.Name));
					continue;
				}
				categoryInstance.Options.Add(optionsControlBase);
				optionsControlBase.CachedTransform.SetParent(categoryGroup.Container, worldPositionStays: false);
				optionsControlBase.IsSelected = global::SRDebugger.Internal.Service.PinnedUI.HasPinned(option);
				optionsControlBase.SelectionModeEnabled = false;
				optionsControlBase.SelectionModeToggle.onValueChanged.AddListener(OnOptionSelectionToggle);
				_options.Add(option, optionsControlBase);
				_controls.Add(optionsControlBase);
			}
		}

		private void Clear()
		{
			foreach (global::SRDebugger.UI.Tabs.OptionsTabController.CategoryInstance category in _categories)
			{
				global::UnityEngine.Object.Destroy(category.CategoryGroup.gameObject);
			}
			_categories.Clear();
			_controls.Clear();
			_options.Clear();
		}
	}
}
