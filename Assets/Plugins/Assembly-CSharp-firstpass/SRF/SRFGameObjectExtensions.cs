namespace SRF
{
	public static class SRFGameObjectExtensions
	{
		public static T GetIComponent<T>(this global::UnityEngine.GameObject t) where T : class
		{
			return t.GetComponent(typeof(T)) as T;
		}

		public static T GetComponentOrAdd<T>(this global::UnityEngine.GameObject obj) where T : global::UnityEngine.Component
		{
			T val = obj.GetComponent<T>();
			if (val == null)
			{
				val = obj.AddComponent<T>();
			}
			return val;
		}

		public static void RemoveComponentIfExists<T>(this global::UnityEngine.GameObject obj) where T : global::UnityEngine.Component
		{
			T component = obj.GetComponent<T>();
			if (component != null)
			{
				global::UnityEngine.Object.Destroy(component);
			}
		}

		public static void RemoveComponentsIfExists<T>(this global::UnityEngine.GameObject obj) where T : global::UnityEngine.Component
		{
			T[] components = obj.GetComponents<T>();
			for (int i = 0; i < components.Length; i++)
			{
				global::UnityEngine.Object.Destroy(components[i]);
			}
		}

		public static bool EnableComponentIfExists<T>(this global::UnityEngine.GameObject obj, bool enable = true) where T : global::UnityEngine.MonoBehaviour
		{
			T component = obj.GetComponent<T>();
			if (component == null)
			{
				return false;
			}
			component.enabled = enable;
			return true;
		}

		public static void SetLayerRecursive(this global::UnityEngine.GameObject o, int layer)
		{
			SetLayerInternal(o.transform, layer);
		}

		private static void SetLayerInternal(global::UnityEngine.Transform t, int layer)
		{
			t.gameObject.layer = layer;
			foreach (global::UnityEngine.Transform item in t)
			{
				SetLayerInternal(item, layer);
			}
		}
	}
}
