public static class AssetBundleManager
{
	private class AssetBundleRef
	{
		public global::UnityEngine.AssetBundle assetBundle;

		public int version;

		public string url;

		public AssetBundleRef(string strUrlIn, int intVersionIn)
		{
			url = strUrlIn;
			version = intVersionIn;
		}
	}

	private static global::System.Collections.Generic.Dictionary<string, AssetBundleManager.AssetBundleRef> dictAssetBundleRefs;

	static AssetBundleManager()
	{
		dictAssetBundleRefs = new global::System.Collections.Generic.Dictionary<string, AssetBundleManager.AssetBundleRef>();
	}

	public static global::UnityEngine.AssetBundle getAssetBundle(string url, int version)
	{
		string key = url + version;
		if (dictAssetBundleRefs.TryGetValue(key, out var value))
		{
			return value.assetBundle;
		}
		return null;
	}

	public static global::System.Collections.IEnumerator downloadAssetBundle(string url, int version, string bundleName)
	{
		global::UnityEngine.Debug.Log("recieved download request " + url + " " + version);
		string keyName = url + version;
		if (dictAssetBundleRefs.ContainsKey(keyName))
		{
			global::UnityEngine.Debug.Log("dict contains key " + keyName);
			yield return null;
			yield break;
		}
		if (ConstantVariables.Instance.UseStreamingAssets)
		{
			global::UnityEngine.Networking.UnityWebRequest request = global::UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(url);
			global::UnityEngine.Debug.Log("made request for " + url);
			yield return request.SendWebRequest();
			global::UnityEngine.Debug.Log(url + " is done!");
			if (request.result != global::UnityEngine.Networking.UnityWebRequest.Result.Success)
			{
				global::UnityEngine.Debug.LogError("WWW download:" + request.error + ": " + url);
			}
			else
			{
				AssetBundleManager.AssetBundleRef assetBundleRef = new AssetBundleManager.AssetBundleRef(url, version);
				assetBundleRef.assetBundle = global::UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request);
				global::UnityEngine.Debug.Log(assetBundleRef.assetBundle.name);
				dictAssetBundleRefs.Add(keyName, assetBundleRef);
			}
		}
		else
		{
			while (!global::UnityEngine.Caching.ready)
			{
				yield return null;
			}
			global::UnityEngine.Caching.ClearOtherCachedVersions(bundleName, new global::UnityEngine.Hash128(0u, 0u, 0u, (uint)version));
			using global::UnityEngine.Networking.UnityWebRequest request2 = global::UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(url, new global::UnityEngine.CachedAssetBundle(bundleName, new global::UnityEngine.Hash128(0u, 0u, 0u, (uint)version)));
			request2.SendWebRequest();
			global::UnityEngine.Debug.Log("made request for " + url);
			while (!request2.isDone)
			{
				yield return null;
			}
			global::UnityEngine.Debug.Log(url + " is done!");
			if (!request2.isNetworkError)
			{
				AssetBundleManager.AssetBundleRef assetBundleRef2 = new AssetBundleManager.AssetBundleRef(url, version);
				assetBundleRef2.assetBundle = global::UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request2);
				global::UnityEngine.Debug.Log(assetBundleRef2.assetBundle.name);
				dictAssetBundleRefs.Add(keyName, assetBundleRef2);
			}
			else
			{
				global::UnityEngine.Debug.LogError("WWW download:" + request2.error);
			}
		}
		yield return null;
	}

	public static void Unload(string url, int version, bool allObjects)
	{
		string key = url + version;
		if (dictAssetBundleRefs.TryGetValue(key, out var value))
		{
			value.assetBundle.Unload(allObjects);
			value.assetBundle = null;
			dictAssetBundleRefs.Remove(key);
		}
	}
}
