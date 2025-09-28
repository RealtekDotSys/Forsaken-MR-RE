namespace SRF
{
	public class Hierarchy
	{
		private static readonly char[] Seperator = new char[1] { '/' };

		private static readonly global::System.Collections.Generic.Dictionary<string, global::UnityEngine.Transform> Cache = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.Transform>();

		[global::System.Obsolete("Use static Get() instead")]
		public global::UnityEngine.Transform this[string key] => Get(key);

		public static global::UnityEngine.Transform Get(string key)
		{
			if (Cache.TryGetValue(key, out var value))
			{
				return value;
			}
			global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find(key);
			if ((bool)gameObject)
			{
				value = gameObject.transform;
				Cache.Add(key, value);
				return value;
			}
			string[] array = key.Split(Seperator, global::System.StringSplitOptions.RemoveEmptyEntries);
			value = new global::UnityEngine.GameObject(global::System.Linq.Enumerable.Last(array)).transform;
			Cache.Add(key, value);
			if (array.Length == 1)
			{
				return value;
			}
			value.parent = Get(string.Join("/", array, 0, array.Length - 1));
			return value;
		}
	}
}
