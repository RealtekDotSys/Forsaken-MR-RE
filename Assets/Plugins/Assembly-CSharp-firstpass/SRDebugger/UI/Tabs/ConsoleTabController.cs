namespace SRDebugger.UI.Tabs
{
	public class ConsoleTabController : global::SRF.SRMonoBehaviourEx
	{
		private const int MaxLength = 2600;

		private global::UnityEngine.Canvas _consoleCanvas;

		private bool _isDirty;

		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Controls.ConsoleLogControl ConsoleLogControl;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle PinToggle;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.ScrollRect StackTraceScrollRect;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text StackTraceText;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle ToggleErrors;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text ToggleErrorsText;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle ToggleInfo;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text ToggleInfoText;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle ToggleWarnings;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text ToggleWarningsText;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle FilterToggle;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.InputField FilterField;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject FilterBarContainer;

		protected override void Start()
		{
			base.Start();
			_consoleCanvas = GetComponent<global::UnityEngine.Canvas>();
			ToggleErrors.onValueChanged.AddListener(delegate
			{
				_isDirty = true;
			});
			ToggleWarnings.onValueChanged.AddListener(delegate
			{
				_isDirty = true;
			});
			ToggleInfo.onValueChanged.AddListener(delegate
			{
				_isDirty = true;
			});
			PinToggle.onValueChanged.AddListener(PinToggleValueChanged);
			FilterToggle.onValueChanged.AddListener(FilterToggleValueChanged);
			FilterBarContainer.SetActive(FilterToggle.isOn);
			FilterField.onValueChanged.AddListener(FilterValueChanged);
			ConsoleLogControl.SelectedItemChanged = ConsoleLogSelectedItemChanged;
			global::SRDebugger.Internal.Service.Console.Updated += ConsoleOnUpdated;
			global::SRDebugger.Internal.Service.Panel.VisibilityChanged += PanelOnVisibilityChanged;
			StackTraceText.supportRichText = global::SRDebugger.Settings.Instance.RichTextInConsole;
			PopulateStackTraceArea(null);
			Refresh();
		}

		private void FilterToggleValueChanged(bool isOn)
		{
			if (isOn)
			{
				FilterBarContainer.SetActive(value: true);
				ConsoleLogControl.Filter = FilterField.text;
			}
			else
			{
				ConsoleLogControl.Filter = null;
				FilterBarContainer.SetActive(value: false);
			}
		}

		private void FilterValueChanged(string filterText)
		{
			if (FilterToggle.isOn && !string.IsNullOrEmpty(filterText) && filterText.Trim().Length != 0)
			{
				ConsoleLogControl.Filter = filterText;
			}
			else
			{
				ConsoleLogControl.Filter = null;
			}
		}

		private void PanelOnVisibilityChanged(global::SRDebugger.Services.IDebugPanelService debugPanelService, bool b)
		{
			if (!(_consoleCanvas == null))
			{
				if (b)
				{
					_consoleCanvas.enabled = true;
				}
				else
				{
					_consoleCanvas.enabled = false;
				}
			}
		}

		private void PinToggleValueChanged(bool isOn)
		{
			global::SRDebugger.Internal.Service.DockConsole.IsVisible = isOn;
		}

		protected override void OnDestroy()
		{
			if (global::SRDebugger.Internal.Service.Console != null)
			{
				global::SRDebugger.Internal.Service.Console.Updated -= ConsoleOnUpdated;
			}
			base.OnDestroy();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			_isDirty = true;
		}

		private void ConsoleLogSelectedItemChanged(object item)
		{
			global::SRDebugger.Services.ConsoleEntry entry = item as global::SRDebugger.Services.ConsoleEntry;
			PopulateStackTraceArea(entry);
		}

		protected override void Update()
		{
			base.Update();
			if (_isDirty)
			{
				Refresh();
			}
		}

		private void PopulateStackTraceArea(global::SRDebugger.Services.ConsoleEntry entry)
		{
			if (entry == null)
			{
				StackTraceText.text = "";
			}
			else
			{
				string text = entry.Message + global::System.Environment.NewLine + ((!string.IsNullOrEmpty(entry.StackTrace)) ? entry.StackTrace : global::SRDebugger.Internal.SRDebugStrings.Current.Console_NoStackTrace);
				if (text.Length > 2600)
				{
					text = text.Substring(0, 2600);
					text = text + "\n" + global::SRDebugger.Internal.SRDebugStrings.Current.Console_MessageTruncated;
				}
				StackTraceText.text = text;
			}
			StackTraceScrollRect.normalizedPosition = new global::UnityEngine.Vector2(0f, 1f);
		}

		private void Refresh()
		{
			ToggleInfoText.text = global::SRDebugger.Internal.SRDebuggerUtil.GetNumberString(global::SRDebugger.Internal.Service.Console.InfoCount, 999, "999+");
			ToggleWarningsText.text = global::SRDebugger.Internal.SRDebuggerUtil.GetNumberString(global::SRDebugger.Internal.Service.Console.WarningCount, 999, "999+");
			ToggleErrorsText.text = global::SRDebugger.Internal.SRDebuggerUtil.GetNumberString(global::SRDebugger.Internal.Service.Console.ErrorCount, 999, "999+");
			ConsoleLogControl.ShowErrors = ToggleErrors.isOn;
			ConsoleLogControl.ShowWarnings = ToggleWarnings.isOn;
			ConsoleLogControl.ShowInfo = ToggleInfo.isOn;
			PinToggle.isOn = global::SRDebugger.Internal.Service.DockConsole.IsVisible;
			_isDirty = false;
		}

		private void ConsoleOnUpdated(global::SRDebugger.Services.IConsoleService console)
		{
			_isDirty = true;
		}

		public void Clear()
		{
			global::SRDebugger.Internal.Service.Console.Clear();
			_isDirty = true;
		}
	}
}
