public class BundleLoader : global::UnityEngine.MonoBehaviour
{
	public void LoadBundle(string name, BundleLoadSuccess onSuccess, BundleLoadFailure onFailure)
	{
		ConstantVariables instance = ConstantVariables.Instance;
		string downloadURI = instance.DownloadURI;
		int num = 0;
		Entry entry = null;
		foreach (Entry entry2 in instance.AssetBundleDownloader.DeserializedTOC.Entries)
		{
			if (entry2.BundleName == name)
			{
				entry = entry2;
				break;
			}
		}
		string text;
		if (instance.UseStreamingAssets)
		{
			global::UnityEngine.Debug.Log("Using streaming assets");
			text = "file://" + global::UnityEngine.Application.streamingAssetsPath + "/Bundles/Windows/0/" + name;
		}
		else
		{
			num = int.Parse(entry.WindowsBundleVersion);
			text = downloadURI + "/bundles/Windows/" + num + "/" + name;
		}
		global::UnityEngine.Debug.Log(text);
		global::UnityEngine.AssetBundle assetBundle = AssetBundleManager.getAssetBundle(text, (!instance.UseStreamingAssets) ? num : 0);
		if (!assetBundle)
		{
			StartCoroutine(DownloadAB(name, text, (!instance.UseStreamingAssets) ? num : 0, name, onSuccess, onFailure));
			global::UnityEngine.Debug.Log("starting coroutine");
		}
		else
		{
			onSuccess?.Invoke(new global::UnityEngine.CachedAssetBundle(name, new global::UnityEngine.Hash128(0u, 0u, 0u, (!instance.UseStreamingAssets) ? ((uint)num) : 0u)), assetBundle);
		}
	}

	public void UnloadBundle(string bundleName)
	{
		global::UnityEngine.Debug.LogError("Requested unload of bundle " + bundleName);
		ConstantVariables instance = ConstantVariables.Instance;
		string downloadURI = instance.DownloadURI;
		int num = 0;
		Entry entry = null;
		foreach (Entry entry2 in instance.AssetBundleDownloader.DeserializedTOC.Entries)
		{
			if (entry2.BundleName == bundleName)
			{
				entry = entry2;
				break;
			}
		}
		if (entry == null)
		{
			global::UnityEngine.Debug.LogError("Couldn't find bundle " + bundleName + " in DeserializedTOC.");
		}
		string url;
		if (instance.UseStreamingAssets)
		{
			global::UnityEngine.Debug.Log("Using streaming assets");
			url = "file://" + global::UnityEngine.Application.streamingAssetsPath + "/Bundles/Windows/0/" + bundleName;
		}
		else
		{
			num = int.Parse(entry.WindowsBundleVersion);
			url = downloadURI + "/bundles/Windows/" + num + "/" + bundleName;
		}
		AssetBundleManager.Unload(url, (!instance.UseStreamingAssets) ? num : 0, allObjects: false);
	}

	private global::System.Collections.IEnumerator DownloadAB(string name, string activeURL, int version, string bundleName, BundleLoadSuccess onSuccess, BundleLoadFailure onFailure)
	{
		yield return StartCoroutine(AssetBundleManager.downloadAssetBundle(activeURL, version, bundleName));
		global::UnityEngine.AssetBundle assetBundle = AssetBundleManager.getAssetBundle(activeURL, version);
		if (!assetBundle)
		{
			onFailure?.Invoke(new global::UnityEngine.CachedAssetBundle("null", default(global::UnityEngine.Hash128)));
		}
		else
		{
			onSuccess?.Invoke(new global::UnityEngine.CachedAssetBundle(name, default(global::UnityEngine.Hash128)), assetBundle);
		}
	}
}
