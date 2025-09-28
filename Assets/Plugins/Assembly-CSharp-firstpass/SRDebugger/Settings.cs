namespace SRDebugger
{
	public class Settings : global::UnityEngine.ScriptableObject
	{
		public enum ShortcutActions
		{
			None = 0,
			OpenSystemInfoTab = 1,
			OpenConsoleTab = 2,
			OpenOptionsTab = 3,
			OpenProfilerTab = 4,
			OpenBugReporterTab = 5,
			ClosePanel = 6,
			OpenPanel = 7,
			TogglePanel = 8,
			ShowBugReportPopover = 9,
			ToggleDockedConsole = 10,
			ToggleDockedProfiler = 11
		}

		public enum TriggerBehaviours
		{
			TripleTap = 0,
			TapAndHold = 1,
			DoubleTap = 2
		}

		public enum TriggerEnableModes
		{
			Enabled = 0,
			MobileOnly = 1,
			Off = 2,
			DevelopmentBuildsOnly = 3
		}

		[global::System.Serializable]
		public sealed class KeyboardShortcut
		{
			[global::UnityEngine.SerializeField]
			public global::SRDebugger.Settings.ShortcutActions Action;

			[global::UnityEngine.SerializeField]
			public bool Alt;

			[global::UnityEngine.SerializeField]
			public bool Control;

			[global::UnityEngine.SerializeField]
			public global::UnityEngine.KeyCode Key;

			[global::UnityEngine.SerializeField]
			public bool Shift;
		}

		private const string ResourcesPath = "/usr/Resources/SRDebugger";

		private const string ResourcesName = "Settings";

		private static global::SRDebugger.Settings _instance;

		[global::UnityEngine.SerializeField]
		private bool _isEnabled = true;

		[global::UnityEngine.SerializeField]
		private bool _autoLoad = true;

		[global::UnityEngine.SerializeField]
		private global::SRDebugger.DefaultTabs _defaultTab;

		[global::UnityEngine.SerializeField]
		private global::SRDebugger.Settings.TriggerEnableModes _triggerEnableMode;

		[global::UnityEngine.SerializeField]
		private global::SRDebugger.Settings.TriggerBehaviours _triggerBehaviour;

		[global::UnityEngine.SerializeField]
		private bool _enableKeyboardShortcuts = true;

		[global::UnityEngine.SerializeField]
		private global::SRDebugger.Settings.KeyboardShortcut[] _keyboardShortcuts;

		[global::UnityEngine.SerializeField]
		private global::SRDebugger.Settings.KeyboardShortcut[] _newKeyboardShortcuts = GetDefaultKeyboardShortcuts();

		[global::UnityEngine.SerializeField]
		private bool _keyboardModifierControl = true;

		[global::UnityEngine.SerializeField]
		private bool _keyboardModifierAlt;

		[global::UnityEngine.SerializeField]
		private bool _keyboardModifierShift = true;

		[global::UnityEngine.SerializeField]
		private bool _keyboardEscapeClose = true;

		[global::UnityEngine.SerializeField]
		private bool _enableBackgroundTransparency = true;

		[global::UnityEngine.SerializeField]
		private bool _collapseDuplicateLogEntries = true;

		[global::UnityEngine.SerializeField]
		private bool _richTextInConsole = true;

		[global::UnityEngine.SerializeField]
		private bool _requireEntryCode;

		[global::UnityEngine.SerializeField]
		private bool _requireEntryCodeEveryTime;

		[global::UnityEngine.SerializeField]
		private int[] _entryCode = new int[4];

		[global::UnityEngine.SerializeField]
		private bool _useDebugCamera;

		[global::UnityEngine.SerializeField]
		private int _debugLayer = 5;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(-100f, 100f)]
		private float _debugCameraDepth = 100f;

		[global::UnityEngine.SerializeField]
		private string _apiKey = "";

		[global::UnityEngine.SerializeField]
		private bool _enableBugReporter;

		[global::UnityEngine.SerializeField]
		private global::SRDebugger.DefaultTabs[] _disabledTabs = new global::SRDebugger.DefaultTabs[0];

		[global::UnityEngine.SerializeField]
		private global::SRDebugger.PinAlignment _profilerAlignment = global::SRDebugger.PinAlignment.BottomLeft;

		[global::UnityEngine.SerializeField]
		private global::SRDebugger.PinAlignment _optionsAlignment = global::SRDebugger.PinAlignment.BottomRight;

		[global::UnityEngine.SerializeField]
		private global::SRDebugger.ConsoleAlignment _consoleAlignment;

		[global::UnityEngine.SerializeField]
		private global::SRDebugger.PinAlignment _triggerPosition;

		[global::UnityEngine.SerializeField]
		private int _maximumConsoleEntries = 1500;

		[global::UnityEngine.SerializeField]
		private bool _enableEventSystemCreation = true;

		[global::UnityEngine.SerializeField]
		private bool _automaticShowCursor = true;

		[global::UnityEngine.SerializeField]
		private float _uiScale = 1f;

		public static global::SRDebugger.Settings Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = GetOrCreateInstance();
				}
				if (_instance._keyboardShortcuts != null && _instance._keyboardShortcuts.Length != 0)
				{
					_instance.UpgradeKeyboardShortcuts();
				}
				return _instance;
			}
		}

		public bool IsEnabled => _isEnabled;

		public bool AutoLoad => _autoLoad;

		public global::SRDebugger.DefaultTabs DefaultTab => _defaultTab;

		public global::SRDebugger.Settings.TriggerEnableModes EnableTrigger => _triggerEnableMode;

		public global::SRDebugger.Settings.TriggerBehaviours TriggerBehaviour => _triggerBehaviour;

		public bool EnableKeyboardShortcuts => _enableKeyboardShortcuts;

		public global::System.Collections.Generic.IList<global::SRDebugger.Settings.KeyboardShortcut> KeyboardShortcuts => _newKeyboardShortcuts;

		public bool KeyboardEscapeClose => _keyboardEscapeClose;

		public bool EnableBackgroundTransparency => _enableBackgroundTransparency;

		public bool RequireCode => _requireEntryCode;

		public bool RequireEntryCodeEveryTime => _requireEntryCodeEveryTime;

		public global::System.Collections.Generic.IList<int> EntryCode
		{
			get
			{
				return new global::System.Collections.ObjectModel.ReadOnlyCollection<int>(_entryCode);
			}
			set
			{
				if (value.Count != 4)
				{
					throw new global::System.Exception("Entry code must be length 4");
				}
				if (global::System.Linq.Enumerable.Any(value, (int p) => p > 9 || p < 0))
				{
					throw new global::System.Exception("All digits in entry code must be >= 0 and <= 9");
				}
				_entryCode = global::System.Linq.Enumerable.ToArray(value);
			}
		}

		public bool UseDebugCamera => _useDebugCamera;

		public int DebugLayer => _debugLayer;

		public float DebugCameraDepth => _debugCameraDepth;

		public bool CollapseDuplicateLogEntries => _collapseDuplicateLogEntries;

		public bool RichTextInConsole => _richTextInConsole;

		public string ApiKey => _apiKey;

		public bool EnableBugReporter => _enableBugReporter;

		public global::System.Collections.Generic.IList<global::SRDebugger.DefaultTabs> DisabledTabs => _disabledTabs;

		public global::SRDebugger.PinAlignment TriggerPosition => _triggerPosition;

		public global::SRDebugger.PinAlignment ProfilerAlignment => _profilerAlignment;

		public global::SRDebugger.PinAlignment OptionsAlignment => _optionsAlignment;

		public global::SRDebugger.ConsoleAlignment ConsoleAlignment
		{
			get
			{
				return _consoleAlignment;
			}
			set
			{
				_consoleAlignment = value;
			}
		}

		public int MaximumConsoleEntries
		{
			get
			{
				return _maximumConsoleEntries;
			}
			set
			{
				_maximumConsoleEntries = value;
			}
		}

		public bool EnableEventSystemGeneration
		{
			get
			{
				return _enableEventSystemCreation;
			}
			set
			{
				_enableEventSystemCreation = value;
			}
		}

		public bool AutomaticallyShowCursor => _automaticShowCursor;

		public float UIScale
		{
			get
			{
				return _uiScale;
			}
			set
			{
				if (value != _uiScale)
				{
					_uiScale = value;
					OnPropertyChanged("UIScale");
				}
			}
		}

		public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		private static global::SRDebugger.Settings.KeyboardShortcut[] GetDefaultKeyboardShortcuts()
		{
			return new global::SRDebugger.Settings.KeyboardShortcut[4]
			{
				new global::SRDebugger.Settings.KeyboardShortcut
				{
					Control = true,
					Shift = true,
					Key = global::UnityEngine.KeyCode.F1,
					Action = global::SRDebugger.Settings.ShortcutActions.OpenSystemInfoTab
				},
				new global::SRDebugger.Settings.KeyboardShortcut
				{
					Control = true,
					Shift = true,
					Key = global::UnityEngine.KeyCode.F2,
					Action = global::SRDebugger.Settings.ShortcutActions.OpenConsoleTab
				},
				new global::SRDebugger.Settings.KeyboardShortcut
				{
					Control = true,
					Shift = true,
					Key = global::UnityEngine.KeyCode.F3,
					Action = global::SRDebugger.Settings.ShortcutActions.OpenOptionsTab
				},
				new global::SRDebugger.Settings.KeyboardShortcut
				{
					Control = true,
					Shift = true,
					Key = global::UnityEngine.KeyCode.F4,
					Action = global::SRDebugger.Settings.ShortcutActions.OpenProfilerTab
				}
			};
		}

		private void UpgradeKeyboardShortcuts()
		{
			global::UnityEngine.Debug.Log("[SRDebugger] Upgrading Settings format");
			global::System.Collections.Generic.List<global::SRDebugger.Settings.KeyboardShortcut> list = new global::System.Collections.Generic.List<global::SRDebugger.Settings.KeyboardShortcut>();
			for (int i = 0; i < _keyboardShortcuts.Length; i++)
			{
				global::SRDebugger.Settings.KeyboardShortcut keyboardShortcut = _keyboardShortcuts[i];
				list.Add(new global::SRDebugger.Settings.KeyboardShortcut
				{
					Action = keyboardShortcut.Action,
					Key = keyboardShortcut.Key,
					Alt = _keyboardModifierAlt,
					Shift = _keyboardModifierShift,
					Control = _keyboardModifierControl
				});
			}
			_keyboardShortcuts = new global::SRDebugger.Settings.KeyboardShortcut[0];
			_newKeyboardShortcuts = list.ToArray();
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private static global::SRDebugger.Settings GetOrCreateInstance()
		{
			global::SRDebugger.Settings settings = global::UnityEngine.Resources.Load<global::SRDebugger.Settings>("SRDebugger/Settings");
			if (settings == null)
			{
				settings = global::UnityEngine.ScriptableObject.CreateInstance<global::SRDebugger.Settings>();
			}
			return settings;
		}
	}
}
