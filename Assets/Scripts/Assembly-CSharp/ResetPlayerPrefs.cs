public class ResetPlayerPrefs : global::UnityEngine.MonoBehaviour
{
	public void ResetPreferences()
	{
		global::UnityEngine.PlayerPrefs.DeleteAll();
		global::UnityEngine.AssetBundle.UnloadAllAssetBundles(unloadAllObjects: true);
		if (!global::UnityEngine.Caching.ClearCache())
		{
			global::UnityEngine.Debug.LogError("COULD NOT CLEAR CACHE.");
		}
		else
		{
			global::UnityEngine.Debug.Log("success!");
		}
	}
}
