public class WwiseSoundBank
{
	private global::System.Action<string> _onSuccess;

	private global::System.Action<string> _onFailure;

	private AssetCache _assetCache;

	private string _bankName;

	public void Setup(AssetCache assetCache, string bankName, global::System.Action<string> onSuccess, global::System.Action<string> onFailure)
	{
		_bankName = bankName;
		_onSuccess = onSuccess;
		_onFailure = onFailure;
		_assetCache = assetCache;
		_assetCache.LoadAsset<global::UnityEngine.TextAsset>("audio", _bankName + ".bytes", LoadSuccess, LoadFailure);
	}

	private void LoadSuccess(global::UnityEngine.TextAsset loadedFile)
	{
		if (loadedFile != null)
		{
			global::UnityEngine.Debug.Log("success loading bank " + _bankName);
			global::FMODUnity.RuntimeManager.LoadBank(loadedFile);
			_onSuccess?.Invoke(_bankName);
			_onSuccess = null;
			_onFailure = null;
			_assetCache.ReleaseAsset(loadedFile);
			_assetCache = null;
		}
		else
		{
			_onFailure?.Invoke(_bankName);
			_onSuccess = null;
			_onFailure = null;
		}
	}

	private void LoadFailure()
	{
		_onFailure?.Invoke(_bankName);
		_onSuccess = null;
		_onFailure = null;
	}

	private void ClearCallbacks()
	{
		_onSuccess = null;
		_onFailure = null;
	}

	public void Teardown()
	{
		_onSuccess = null;
		_onFailure = null;
		global::UnityEngine.Debug.Log("unloaded bank " + _bankName);
		global::FMODUnity.RuntimeManager.UnloadBank(_bankName);
	}
}
