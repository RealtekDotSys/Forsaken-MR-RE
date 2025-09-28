public class BundleCache
{
	private global::System.Action<BundleCache> ortonBundlesDownloadedCallback;

	private bool allOrtonBundlesDownloaded;

	private BundleLoader _bundleLoader;

	public void LoadAssetBundleAsync(string bundleName, BundleLoadSuccess onSuccess, BundleLoadFailure onFailure)
	{
		_bundleLoader.LoadBundle(bundleName, onSuccess, onFailure);
	}

	public void ReleaseAssetBundleReference(string bundleName)
	{
		global::UnityEngine.Debug.LogError("ORTON!!! BUNDLE " + bundleName + " IS BEING UNLOADED!!");
		_bundleLoader.UnloadBundle(bundleName);
	}

	public void AllOrtonBundlesDownloaded()
	{
		allOrtonBundlesDownloaded = true;
		ortonBundlesDownloadedCallback?.Invoke(this);
		ClearCallbacks();
	}

	private void ClearCallbacks()
	{
		ortonBundlesDownloadedCallback = null;
	}

	public void GetInterfaceAsync(global::System.Action<BundleCache> callback)
	{
		if (allOrtonBundlesDownloaded)
		{
			callback?.Invoke(this);
		}
		else
		{
			ortonBundlesDownloadedCallback = (global::System.Action<BundleCache>)global::System.Delegate.Combine(ortonBundlesDownloadedCallback, callback);
		}
	}

	public BundleCache()
	{
		global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject
		{
			name = "BundleLoader"
		};
		_bundleLoader = gameObject.AddComponent<BundleLoader>();
		gameObject.AddComponent<DontDestroy>();
	}
}
