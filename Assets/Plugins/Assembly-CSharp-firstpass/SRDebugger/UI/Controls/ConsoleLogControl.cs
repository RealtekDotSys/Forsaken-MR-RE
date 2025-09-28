namespace SRDebugger.UI.Controls
{
	public class ConsoleLogControl : global::SRF.SRMonoBehaviourEx
	{
		[global::SRF.RequiredField]
		[global::UnityEngine.SerializeField]
		private global::SRF.UI.Layout.VirtualVerticalLayoutGroup _consoleScrollLayoutGroup;

		[global::SRF.RequiredField]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.ScrollRect _consoleScrollRect;

		private bool _isDirty;

		private global::UnityEngine.Vector2? _scrollPosition;

		private bool _showErrors = true;

		private bool _showInfo = true;

		private bool _showWarnings = true;

		public global::System.Action<global::SRDebugger.Services.ConsoleEntry> SelectedItemChanged;

		private string _filter;

		public bool ShowErrors
		{
			get
			{
				return _showErrors;
			}
			set
			{
				_showErrors = value;
				SetIsDirty();
			}
		}

		public bool ShowWarnings
		{
			get
			{
				return _showWarnings;
			}
			set
			{
				_showWarnings = value;
				SetIsDirty();
			}
		}

		public bool ShowInfo
		{
			get
			{
				return _showInfo;
			}
			set
			{
				_showInfo = value;
				SetIsDirty();
			}
		}

		public bool EnableSelection
		{
			get
			{
				return _consoleScrollLayoutGroup.EnableSelection;
			}
			set
			{
				_consoleScrollLayoutGroup.EnableSelection = value;
			}
		}

		public string Filter
		{
			get
			{
				return _filter;
			}
			set
			{
				if (_filter != value)
				{
					_filter = value;
					_isDirty = true;
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			_consoleScrollLayoutGroup.SelectedItemChanged.AddListener(OnSelectedItemChanged);
			global::SRDebugger.Internal.Service.Console.Updated += ConsoleOnUpdated;
		}

		protected override void Start()
		{
			base.Start();
			SetIsDirty();
			StartCoroutine(ScrollToBottom());
		}

		private global::System.Collections.IEnumerator ScrollToBottom()
		{
			yield return new global::UnityEngine.WaitForEndOfFrame();
			yield return new global::UnityEngine.WaitForEndOfFrame();
			yield return new global::UnityEngine.WaitForEndOfFrame();
			_scrollPosition = new global::UnityEngine.Vector2(0f, 0f);
		}

		protected override void OnDestroy()
		{
			if (global::SRDebugger.Internal.Service.Console != null)
			{
				global::SRDebugger.Internal.Service.Console.Updated -= ConsoleOnUpdated;
			}
			base.OnDestroy();
		}

		private void OnSelectedItemChanged(object arg0)
		{
			global::SRDebugger.Services.ConsoleEntry obj = arg0 as global::SRDebugger.Services.ConsoleEntry;
			if (SelectedItemChanged != null)
			{
				SelectedItemChanged(obj);
			}
		}

		protected override void Update()
		{
			base.Update();
			if (_scrollPosition.HasValue)
			{
				_consoleScrollRect.normalizedPosition = _scrollPosition.Value;
				_scrollPosition = null;
			}
			if (_isDirty)
			{
				Refresh();
			}
		}

		private void Refresh()
		{
			if (global::SRF.SRFFloatExtensions.ApproxZero(_consoleScrollRect.normalizedPosition.y))
			{
				_scrollPosition = _consoleScrollRect.normalizedPosition;
			}
			_consoleScrollLayoutGroup.ClearItems();
			global::SRDebugger.IReadOnlyList<global::SRDebugger.Services.ConsoleEntry> entries = global::SRDebugger.Internal.Service.Console.Entries;
			for (int i = 0; i < entries.Count; i++)
			{
				global::SRDebugger.Services.ConsoleEntry consoleEntry = entries[i];
				if ((consoleEntry.LogType == global::UnityEngine.LogType.Error || consoleEntry.LogType == global::UnityEngine.LogType.Exception || consoleEntry.LogType == global::UnityEngine.LogType.Assert) && !ShowErrors)
				{
					if (consoleEntry == _consoleScrollLayoutGroup.SelectedItem)
					{
						_consoleScrollLayoutGroup.SelectedItem = null;
					}
				}
				else if (consoleEntry.LogType == global::UnityEngine.LogType.Warning && !ShowWarnings)
				{
					if (consoleEntry == _consoleScrollLayoutGroup.SelectedItem)
					{
						_consoleScrollLayoutGroup.SelectedItem = null;
					}
				}
				else if (consoleEntry.LogType == global::UnityEngine.LogType.Log && !ShowInfo)
				{
					if (consoleEntry == _consoleScrollLayoutGroup.SelectedItem)
					{
						_consoleScrollLayoutGroup.SelectedItem = null;
					}
				}
				else if (!string.IsNullOrEmpty(Filter) && consoleEntry.Message.IndexOf(Filter, global::System.StringComparison.OrdinalIgnoreCase) < 0)
				{
					if (consoleEntry == _consoleScrollLayoutGroup.SelectedItem)
					{
						_consoleScrollLayoutGroup.SelectedItem = null;
					}
				}
				else
				{
					_consoleScrollLayoutGroup.AddItem(consoleEntry);
				}
			}
			_isDirty = false;
		}

		private void SetIsDirty()
		{
			_isDirty = true;
		}

		private void ConsoleOnUpdated(global::SRDebugger.Services.IConsoleService console)
		{
			SetIsDirty();
		}
	}
}
