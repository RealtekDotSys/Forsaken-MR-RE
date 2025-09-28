public class AssetLoader<T> : IAssetLoader where T : class
{
	private string _bundleName;

	private string _assetName;

	private string _assetPath;

	private global::System.Action<T> _successCallbacks;

	private global::System.Action _failureCallbacks;

	private global::System.Action<IAssetLoader, T> _completeCallback;

	private int _numRequests;

	public void Load(BundleCache bundleCache, string bundleName, string assetName)
	{
		_assetName = assetName;
		_bundleName = bundleName;
		bundleCache.LoadAssetBundleAsync(bundleName, BundleLoadSuccess, BundleLoadFailure);
	}

	public string GetBundleName()
	{
		return _bundleName;
	}

	public string GetAssetName()
	{
		return _assetName;
	}

	public int GetNumRequests()
	{
		return _numRequests;
	}

	public void AddRequest(global::System.Action<T> onSuccess, global::System.Action onFailure)
	{
		_successCallbacks = (global::System.Action<T>)global::System.Delegate.Combine(_successCallbacks, onSuccess);
		_failureCallbacks = (global::System.Action)global::System.Delegate.Combine(_failureCallbacks, onFailure);
		_numRequests++;
	}

	private void BundleLoadSuccess(global::UnityEngine.CachedAssetBundle info, global::UnityEngine.AssetBundle bundle)
	{
		bundle.LoadAssetAsync(_assetName, typeof(T)).completed += AssetLoadFromBundleComplete;
	}

	private void BundleLoadFailure(global::UnityEngine.CachedAssetBundle info)
	{
		_failureCallbacks?.Invoke();
	}

	private void AssetLoadFromBundleComplete(global::UnityEngine.AsyncOperation request)
	{
		global::UnityEngine.Object obj = null;
		if (request != null)
		{
			obj = ((global::UnityEngine.AssetBundleRequest)request).asset;
		}
		if (obj == null)
		{
			AssetLoadFailure();
		}
		else
		{
			AssetLoadSuccess((T)global::System.Convert.ChangeType(obj, typeof(T)));
		}
	}

	private void AssetLoadSuccess(T asset)
	{
		_completeCallback?.Invoke(this, asset);
		_successCallbacks?.Invoke(asset);
		ClearCallbacks();
	}

	private void AssetLoadFailure()
	{
		_failureCallbacks?.Invoke();
		ClearCallbacks();
	}

	public AssetLoader(global::System.Action<T> onSuccess, global::System.Action onFailure, global::System.Action<IAssetLoader, T> onComplete)
	{
		_successCallbacks = (global::System.Action<T>)global::System.Delegate.Combine(_successCallbacks, onSuccess);
		_failureCallbacks = (global::System.Action)global::System.Delegate.Combine(_failureCallbacks, onFailure);
		_completeCallback = (global::System.Action<IAssetLoader, T>)global::System.Delegate.Combine(_completeCallback, onComplete);
		_numRequests = 1;
	}

	private void ClearCallbacks()
	{
		_successCallbacks = null;
		_failureCallbacks = null;
		_completeCallback = null;
		_numRequests = 0;
	}
}
