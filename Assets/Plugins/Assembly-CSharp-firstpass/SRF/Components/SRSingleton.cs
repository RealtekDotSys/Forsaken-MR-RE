namespace SRF.Components
{
	public abstract class SRSingleton<T> : global::SRF.SRMonoBehaviour where T : global::SRF.Components.SRSingleton<T>
	{
		private static T _instance;

		public static T Instance
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				if (_instance == null)
				{
					throw new global::System.InvalidOperationException("No instance of {0} present in scene".Fmt(typeof(T).Name));
				}
				return _instance;
			}
		}

		public static bool HasInstance
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return _instance != null;
			}
		}

		private void Register()
		{
			if (_instance != null)
			{
				global::UnityEngine.Debug.LogWarning("More than one singleton object of type {0} exists.".Fmt(typeof(T).Name));
				if (GetComponents<global::UnityEngine.Component>().Length == 2)
				{
					global::UnityEngine.Object.Destroy(base.gameObject);
				}
				else
				{
					global::UnityEngine.Object.Destroy(this);
				}
			}
			else
			{
				_instance = (T)this;
			}
		}

		protected virtual void Awake()
		{
			Register();
		}

		protected virtual void OnEnable()
		{
			if (_instance == null)
			{
				Register();
			}
		}

		private void OnApplicationQuit()
		{
			_instance = null;
		}
	}
}
