namespace SRF.Service
{
	public abstract class SRSceneServiceBase<T, TImpl> : global::SRF.Service.SRServiceBase<T>, global::SRF.Service.IAsyncService where T : class where TImpl : global::UnityEngine.Component
	{
		private TImpl _rootObject;

		protected abstract string SceneName { get; }

		protected TImpl RootObject => _rootObject;

		public bool IsLoaded => _rootObject != null;

		[global::System.Diagnostics.Conditional("ENABLE_LOGGING")]
		private void Log(string msg, global::UnityEngine.Object target)
		{
			global::UnityEngine.Debug.Log(msg, target);
		}

		protected override void Start()
		{
			base.Start();
			StartCoroutine(LoadCoroutine());
		}

		protected override void OnDestroy()
		{
			if (IsLoaded)
			{
				global::UnityEngine.Object.Destroy(_rootObject.gameObject);
			}
			base.OnDestroy();
		}

		protected virtual void OnLoaded()
		{
		}

		private global::System.Collections.IEnumerator LoadCoroutine()
		{
			if (_rootObject != null)
			{
				yield break;
			}
			global::SRF.Service.SRServiceManager.LoadingCount++;
			if (!global::UnityEngine.SceneManagement.SceneManager.GetSceneByName(SceneName).isLoaded)
			{
				yield return global::UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneName, global::UnityEngine.SceneManagement.LoadSceneMode.Additive);
			}
			global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find(SceneName);
			if (!(gameObject == null))
			{
				TImpl component = gameObject.GetComponent<TImpl>();
				if (!(component == null))
				{
					_rootObject = component;
					_rootObject.transform.parent = base.CachedTransform;
					global::UnityEngine.Object.DontDestroyOnLoad(gameObject);
					global::UnityEngine.Debug.Log("[Service] Loading {0} complete. (Scene: {1})".Fmt(GetType().Name, SceneName), this);
					global::SRF.Service.SRServiceManager.LoadingCount--;
					OnLoaded();
					yield break;
				}
			}
			global::SRF.Service.SRServiceManager.LoadingCount--;
			global::UnityEngine.Debug.LogError("[Service] Root object ({0}) not found".Fmt(SceneName), this);
			base.enabled = false;
		}
	}
}
