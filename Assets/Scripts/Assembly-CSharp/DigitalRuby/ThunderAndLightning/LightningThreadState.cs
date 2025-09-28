namespace DigitalRuby.ThunderAndLightning
{
	public class LightningThreadState
	{
		private global::System.Threading.Thread lightningThread;

		private global::System.Threading.AutoResetEvent lightningThreadEvent = new global::System.Threading.AutoResetEvent(initialState: false);

		private readonly global::System.Collections.Generic.Queue<global::System.Action> actionsForBackgroundThread = new global::System.Collections.Generic.Queue<global::System.Action>();

		private readonly global::System.Collections.Generic.Queue<global::System.Collections.Generic.KeyValuePair<global::System.Action, global::System.Threading.ManualResetEvent>> actionsForMainThread = new global::System.Collections.Generic.Queue<global::System.Collections.Generic.KeyValuePair<global::System.Action, global::System.Threading.ManualResetEvent>>();

		public bool Running = true;

		private bool isTerminating;

		private bool UpdateMainThreadActionsOnce()
		{
			global::System.Collections.Generic.KeyValuePair<global::System.Action, global::System.Threading.ManualResetEvent> keyValuePair;
			lock (actionsForMainThread)
			{
				if (actionsForMainThread.Count == 0)
				{
					return false;
				}
				keyValuePair = actionsForMainThread.Dequeue();
			}
			keyValuePair.Key();
			if (keyValuePair.Value != null)
			{
				keyValuePair.Value.Set();
			}
			return true;
		}

		private void BackgroundThreadMethod()
		{
			global::System.Action action = null;
			while (Running)
			{
				try
				{
					if (!lightningThreadEvent.WaitOne(500))
					{
						continue;
					}
					while (true)
					{
						lock (actionsForBackgroundThread)
						{
							if (actionsForBackgroundThread.Count == 0)
							{
								break;
							}
							action = actionsForBackgroundThread.Dequeue();
							goto IL_0051;
						}
						IL_0051:
						action();
					}
				}
				catch (global::System.Threading.ThreadAbortException)
				{
				}
				catch (global::System.Exception ex2)
				{
					global::UnityEngine.Debug.LogErrorFormat("Lightning thread exception: {0}", ex2);
				}
			}
		}

		public LightningThreadState()
		{
			lightningThread = new global::System.Threading.Thread(BackgroundThreadMethod)
			{
				IsBackground = true,
				Name = "LightningBoltScriptThread"
			};
			lightningThread.Start();
		}

		public void TerminateAndWaitForEnd()
		{
			isTerminating = true;
			while (true)
			{
				if (UpdateMainThreadActionsOnce())
				{
					continue;
				}
				lock (actionsForBackgroundThread)
				{
					if (actionsForBackgroundThread.Count == 0)
					{
						break;
					}
				}
			}
		}

		public void UpdateMainThreadActions()
		{
			while (UpdateMainThreadActionsOnce())
			{
			}
		}

		public bool AddActionForMainThread(global::System.Action action, bool waitForAction = false)
		{
			if (isTerminating)
			{
				return false;
			}
			global::System.Threading.ManualResetEvent manualResetEvent = (waitForAction ? new global::System.Threading.ManualResetEvent(initialState: false) : null);
			lock (actionsForMainThread)
			{
				actionsForMainThread.Enqueue(new global::System.Collections.Generic.KeyValuePair<global::System.Action, global::System.Threading.ManualResetEvent>(action, manualResetEvent));
			}
			manualResetEvent?.WaitOne(10000);
			return true;
		}

		public bool AddActionForBackgroundThread(global::System.Action action)
		{
			if (isTerminating)
			{
				return false;
			}
			lock (actionsForBackgroundThread)
			{
				actionsForBackgroundThread.Enqueue(action);
			}
			lightningThreadEvent.Set();
			return true;
		}
	}
}
