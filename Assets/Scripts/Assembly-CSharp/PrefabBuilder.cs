public class PrefabBuilder
{
	public static bool BuildPrefab(out PrefabInstance prefabInstance, string prefabPath, string prefabParentName)
	{
		global::UnityEngine.Transform parent = null;
		prefabInstance = new PrefabInstance();
		if (!string.IsNullOrEmpty(prefabPath))
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Resources.Load<global::UnityEngine.GameObject>(prefabPath);
			if (gameObject != null)
			{
				if (!string.IsNullOrEmpty(prefabParentName))
				{
					global::UnityEngine.GameObject gameObject2 = global::UnityEngine.GameObject.Find(prefabParentName);
					if (gameObject2 != null)
					{
						parent = gameObject2.transform;
					}
				}
				prefabInstance.Root = global::UnityEngine.Object.Instantiate(gameObject, parent);
				prefabInstance.ComponentContainer = new ComponentContainer();
				return true;
			}
		}
		return false;
	}
}
