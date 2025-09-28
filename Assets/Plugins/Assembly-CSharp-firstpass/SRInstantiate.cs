public static class SRInstantiate
{
	public static T Instantiate<T>(T prefab) where T : global::UnityEngine.Component
	{
		return global::UnityEngine.Object.Instantiate(prefab);
	}

	public static global::UnityEngine.GameObject Instantiate(global::UnityEngine.GameObject prefab)
	{
		return global::UnityEngine.Object.Instantiate(prefab);
	}

	public static T Instantiate<T>(T prefab, global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation) where T : global::UnityEngine.Component
	{
		return global::UnityEngine.Object.Instantiate(prefab, position, rotation);
	}
}
