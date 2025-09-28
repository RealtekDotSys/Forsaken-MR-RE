public static class CoroutineHelper
{
	private static global::UnityEngine.MonoBehaviour _monoBehaviour;

	private static global::UnityEngine.MonoBehaviour MonoBehaviour => global::UnityEngine.GameObject.Find("Bootstrap").GetComponent<global::UnityEngine.MonoBehaviour>();

	public static global::UnityEngine.Coroutine StartCoroutine(global::System.Collections.IEnumerator coroutine)
	{
		return MonoBehaviour.StartCoroutine(coroutine);
	}

	public static void StopCoroutine(global::UnityEngine.Coroutine coroutine)
	{
		if (coroutine != null)
		{
			MonoBehaviour.StopCoroutine(coroutine);
		}
	}
}
