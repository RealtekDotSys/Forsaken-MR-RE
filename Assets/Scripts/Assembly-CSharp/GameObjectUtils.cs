public static class GameObjectUtils
{
	public static void Enable(global::UnityEngine.GameObject gameObject, bool state)
	{
		if (!IsDestroyed(gameObject))
		{
			if (state)
			{
				gameObject.transform.localScale = global::UnityEngine.Vector3.one;
			}
			else
			{
				gameObject.transform.localScale = global::UnityEngine.Vector3.zero;
			}
		}
	}

	public static bool IsDestroyed(global::UnityEngine.GameObject gameObject)
	{
		return gameObject == null;
	}
}
