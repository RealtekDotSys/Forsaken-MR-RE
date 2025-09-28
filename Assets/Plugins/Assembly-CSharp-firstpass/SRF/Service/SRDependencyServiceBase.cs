namespace SRF.Service
{
	public abstract class SRDependencyServiceBase<T> : global::SRF.Service.SRServiceBase<T>, global::SRF.Service.IAsyncService where T : class
	{
		private bool _isLoaded;

		protected abstract global::System.Type[] Dependencies { get; }

		public bool IsLoaded => _isLoaded;

		[global::System.Diagnostics.Conditional("ENABLE_LOGGING")]
		private void Log(string msg, global::UnityEngine.Object target)
		{
			global::UnityEngine.Debug.Log(msg, target);
		}

		protected override void Start()
		{
			base.Start();
			StartCoroutine(LoadDependencies());
		}

		protected virtual void OnLoaded()
		{
		}

		private global::System.Collections.IEnumerator LoadDependencies()
		{
			global::SRF.Service.SRServiceManager.LoadingCount++;
			global::System.Type[] dependencies = Dependencies;
			foreach (global::System.Type type in dependencies)
			{
				if (global::SRF.Service.SRServiceManager.HasService(type))
				{
					continue;
				}
				object service = global::SRF.Service.SRServiceManager.GetService(type);
				if (service == null)
				{
					global::UnityEngine.Debug.LogError("[Service] Could not resolve dependency ({0})".Fmt(type.Name));
					base.enabled = false;
					yield break;
				}
				if (service is global::SRF.Service.IAsyncService a)
				{
					while (!a.IsLoaded)
					{
						yield return new global::UnityEngine.WaitForEndOfFrame();
					}
				}
			}
			_isLoaded = true;
			global::SRF.Service.SRServiceManager.LoadingCount--;
			OnLoaded();
		}
	}
}
