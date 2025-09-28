namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IConsoleService))]
	public class StandardConsoleService : global::SRDebugger.Services.IConsoleService
	{
		private readonly bool _collapseEnabled;

		private bool _hasCleared;

		private readonly global::SRDebugger.CircularBuffer<global::SRDebugger.Services.ConsoleEntry> _allConsoleEntries;

		private global::SRDebugger.CircularBuffer<global::SRDebugger.Services.ConsoleEntry> _consoleEntries;

		private readonly object _threadLock = new object();

		public int ErrorCount { get; private set; }

		public int WarningCount { get; private set; }

		public int InfoCount { get; private set; }

		public global::SRDebugger.IReadOnlyList<global::SRDebugger.Services.ConsoleEntry> Entries
		{
			get
			{
				if (!_hasCleared)
				{
					return _allConsoleEntries;
				}
				return _consoleEntries;
			}
		}

		public global::SRDebugger.IReadOnlyList<global::SRDebugger.Services.ConsoleEntry> AllEntries => _allConsoleEntries;

		public event global::SRDebugger.Services.ConsoleUpdatedEventHandler Updated;

		public StandardConsoleService()
		{
			global::UnityEngine.Application.logMessageReceivedThreaded += UnityLogCallback;
			global::SRF.Service.SRServiceManager.RegisterService<global::SRDebugger.Services.IConsoleService>(this);
			_collapseEnabled = global::SRDebugger.Settings.Instance.CollapseDuplicateLogEntries;
			_allConsoleEntries = new global::SRDebugger.CircularBuffer<global::SRDebugger.Services.ConsoleEntry>(global::SRDebugger.Settings.Instance.MaximumConsoleEntries);
		}

		public void Clear()
		{
			lock (_threadLock)
			{
				_hasCleared = true;
				if (_consoleEntries == null)
				{
					_consoleEntries = new global::SRDebugger.CircularBuffer<global::SRDebugger.Services.ConsoleEntry>(global::SRDebugger.Settings.Instance.MaximumConsoleEntries);
				}
				else
				{
					_consoleEntries.Clear();
				}
				int num = (InfoCount = 0);
				int errorCount = (WarningCount = num);
				ErrorCount = errorCount;
			}
			OnUpdated();
		}

		protected void OnEntryAdded(global::SRDebugger.Services.ConsoleEntry entry)
		{
			if (_hasCleared)
			{
				if (_consoleEntries.IsFull)
				{
					AdjustCounter(_consoleEntries.Front().LogType, -1);
					_consoleEntries.PopFront();
				}
				_consoleEntries.PushBack(entry);
			}
			else if (_allConsoleEntries.IsFull)
			{
				AdjustCounter(_allConsoleEntries.Front().LogType, -1);
				_allConsoleEntries.PopFront();
			}
			_allConsoleEntries.PushBack(entry);
			OnUpdated();
		}

		protected void OnEntryDuplicated(global::SRDebugger.Services.ConsoleEntry entry)
		{
			entry.Count++;
			OnUpdated();
			if (_hasCleared && _consoleEntries.Count == 0)
			{
				OnEntryAdded(new global::SRDebugger.Services.ConsoleEntry(entry)
				{
					Count = 1
				});
			}
		}

		private void OnUpdated()
		{
			if (this.Updated != null)
			{
				try
				{
					this.Updated(this);
				}
				catch
				{
				}
			}
		}

		private void UnityLogCallback(string condition, string stackTrace, global::UnityEngine.LogType type)
		{
			lock (_threadLock)
			{
				global::SRDebugger.Services.ConsoleEntry consoleEntry = ((_collapseEnabled && _allConsoleEntries.Count > 0) ? _allConsoleEntries[_allConsoleEntries.Count - 1] : null);
				if (consoleEntry != null && consoleEntry.LogType == type && consoleEntry.Message == condition && consoleEntry.StackTrace == stackTrace)
				{
					OnEntryDuplicated(consoleEntry);
				}
				else
				{
					global::SRDebugger.Services.ConsoleEntry entry = new global::SRDebugger.Services.ConsoleEntry
					{
						LogType = type,
						StackTrace = stackTrace,
						Message = condition
					};
					OnEntryAdded(entry);
				}
				AdjustCounter(type, 1);
			}
		}

		private void AdjustCounter(global::UnityEngine.LogType type, int amount)
		{
			switch (type)
			{
			case global::UnityEngine.LogType.Error:
			case global::UnityEngine.LogType.Assert:
			case global::UnityEngine.LogType.Exception:
				ErrorCount += amount;
				break;
			case global::UnityEngine.LogType.Warning:
				WarningCount += amount;
				break;
			case global::UnityEngine.LogType.Log:
				InfoCount += amount;
				break;
			}
		}
	}
}
