namespace AmplifyMotion
{
	internal class WorkerThreadPool
	{
		private const int ThreadStateQueueCapacity = 1024;

		internal global::System.Collections.Generic.Queue<global::AmplifyMotion.MotionState>[] m_threadStateQueues;

		internal object[] m_threadStateQueueLocks;

		private int m_threadPoolSize;

		private global::System.Threading.ManualResetEvent m_threadPoolTerminateSignal;

		private global::System.Threading.AutoResetEvent[] m_threadPoolContinueSignals;

		private global::System.Threading.Thread[] m_threadPool;

		private bool m_threadPoolFallback;

		internal object m_threadPoolLock;

		internal int m_threadPoolIndex;

		internal void InitializeAsyncUpdateThreads(int threadCount, bool systemThreadPool)
		{
			if (systemThreadPool)
			{
				m_threadPoolFallback = true;
				return;
			}
			try
			{
				m_threadPoolSize = threadCount;
				m_threadStateQueues = new global::System.Collections.Generic.Queue<global::AmplifyMotion.MotionState>[m_threadPoolSize];
				m_threadStateQueueLocks = new object[m_threadPoolSize];
				m_threadPool = new global::System.Threading.Thread[m_threadPoolSize];
				m_threadPoolTerminateSignal = new global::System.Threading.ManualResetEvent(initialState: false);
				m_threadPoolContinueSignals = new global::System.Threading.AutoResetEvent[m_threadPoolSize];
				m_threadPoolLock = new object();
				m_threadPoolIndex = 0;
				for (int i = 0; i < m_threadPoolSize; i++)
				{
					m_threadStateQueues[i] = new global::System.Collections.Generic.Queue<global::AmplifyMotion.MotionState>(1024);
					m_threadStateQueueLocks[i] = new object();
					m_threadPoolContinueSignals[i] = new global::System.Threading.AutoResetEvent(initialState: false);
					m_threadPool[i] = new global::System.Threading.Thread(AsyncUpdateThread);
					m_threadPool[i].Start(new global::System.Collections.Generic.KeyValuePair<object, int>(this, i));
				}
			}
			catch (global::System.Exception ex)
			{
				global::UnityEngine.Debug.LogWarning("[AmplifyMotion] Non-critical error while initializing WorkerThreads. Falling back to using System.Threading.ThreadPool().\n" + ex.Message);
				m_threadPoolFallback = true;
			}
		}

		internal void FinalizeAsyncUpdateThreads()
		{
			if (m_threadPoolFallback)
			{
				return;
			}
			m_threadPoolTerminateSignal.Set();
			for (int i = 0; i < m_threadPoolSize; i++)
			{
				if (m_threadPool[i].IsAlive)
				{
					m_threadPoolContinueSignals[i].Set();
					m_threadPool[i].Join();
					m_threadPool[i] = null;
				}
				lock (m_threadStateQueueLocks[i])
				{
					while (m_threadStateQueues[i].Count > 0)
					{
						m_threadStateQueues[i].Dequeue().AsyncUpdate();
					}
				}
			}
			m_threadStateQueues = null;
			m_threadStateQueueLocks = null;
			m_threadPoolSize = 0;
			m_threadPool = null;
			m_threadPoolTerminateSignal = null;
			m_threadPoolContinueSignals = null;
			m_threadPoolLock = null;
			m_threadPoolIndex = 0;
		}

		internal void EnqueueAsyncUpdate(global::AmplifyMotion.MotionState state)
		{
			if (!m_threadPoolFallback)
			{
				lock (m_threadStateQueueLocks[m_threadPoolIndex])
				{
					m_threadStateQueues[m_threadPoolIndex].Enqueue(state);
				}
				m_threadPoolContinueSignals[m_threadPoolIndex].Set();
				m_threadPoolIndex++;
				if (m_threadPoolIndex >= m_threadPoolSize)
				{
					m_threadPoolIndex = 0;
				}
			}
			else
			{
				global::System.Threading.ThreadPool.QueueUserWorkItem(AsyncUpdateCallback, state);
			}
		}

		private static void AsyncUpdateCallback(object obj)
		{
			((global::AmplifyMotion.MotionState)obj).AsyncUpdate();
		}

		private static void AsyncUpdateThread(object obj)
		{
			global::System.Collections.Generic.KeyValuePair<object, int> keyValuePair = (global::System.Collections.Generic.KeyValuePair<object, int>)obj;
			global::AmplifyMotion.WorkerThreadPool workerThreadPool = (global::AmplifyMotion.WorkerThreadPool)keyValuePair.Key;
			int value = keyValuePair.Value;
			while (true)
			{
				try
				{
					workerThreadPool.m_threadPoolContinueSignals[value].WaitOne();
					if (workerThreadPool.m_threadPoolTerminateSignal.WaitOne(0))
					{
						break;
					}
					while (true)
					{
						global::AmplifyMotion.MotionState motionState = null;
						lock (workerThreadPool.m_threadStateQueueLocks[value])
						{
							if (workerThreadPool.m_threadStateQueues[value].Count > 0)
							{
								motionState = workerThreadPool.m_threadStateQueues[value].Dequeue();
							}
						}
						if (motionState != null)
						{
							motionState.AsyncUpdate();
							continue;
						}
						break;
					}
				}
				catch (global::System.Exception ex)
				{
					if (ex.GetType() != typeof(global::System.Threading.ThreadAbortException))
					{
						global::UnityEngine.Debug.LogWarning(ex);
					}
				}
			}
		}
	}
}
