namespace SRF.Components
{
	public abstract class SRAutoSingleton<T> : global::SRF.SRMonoBehaviour where T : global::SRF.Components.SRAutoSingleton<T>
	{
		private static T _instance;

		public static T Instance
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				if (_instance == null && global::UnityEngine.Application.isPlaying)
				{
					new global::UnityEngine.GameObject("_" + typeof(T).Name).AddComponent<T>();
				}
				return _instance;
			}
		}

		public static bool HasInstance => _instance != null;

		protected virtual void Awake()
		{
			if (_instance != null)
			{
				global::UnityEngine.Debug.LogWarning("More than one singleton object of type {0} exists.".Fmt(typeof(T).Name));
			}
			else
			{
				_instance = (T)this;
			}
		}

		private void OnApplicationQuit()
		{
			_instance = null;
		}
	}
}
