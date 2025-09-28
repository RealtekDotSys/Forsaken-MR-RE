public class SoundBankLoader
{
	private static readonly string ClassName;

	private AssetCache _assetCache;

	private readonly string _platform;

	private global::System.Collections.Generic.Dictionary<string, WwiseSoundBank> _loadedSoundBanks;

	private global::System.Collections.Generic.Dictionary<string, LoadingSoundBank> _loadingSoundBanks;

	public void RequestSoundBank(SoundBankRequest request)
	{
		if (request == null)
		{
			global::UnityEngine.Debug.LogError("SoundBankLoader RequestSoundBank - SoundBankRequest is null");
			return;
		}
		global::UnityEngine.Debug.Log("received soundbank request for " + request.SoundBankName);
		if (_loadedSoundBanks.ContainsKey(request.SoundBankName))
		{
			global::UnityEngine.Debug.Log("Soundbank already loaded");
			request.Success?.Invoke(request.SoundBankName);
		}
		else if (_loadingSoundBanks.ContainsKey(request.SoundBankName))
		{
			_loadingSoundBanks[request.SoundBankName].AddRequest(request);
		}
		else
		{
			LoadingSoundBank loadingSoundBank = new LoadingSoundBank(request, LoadSuccess, LoadFailure);
			_loadingSoundBanks.Add(request.SoundBankName, loadingSoundBank);
			loadingSoundBank.Load(_assetCache);
		}
	}

	private void LoadSuccess(string soundBankName, WwiseSoundBank loadedSoundBank)
	{
		_loadedSoundBanks.Add(soundBankName, loadedSoundBank);
		_loadingSoundBanks[soundBankName].Teardown();
		_loadingSoundBanks.Remove(soundBankName);
	}

	private void LoadFailure(string soundBankName)
	{
		_loadingSoundBanks[soundBankName].Teardown();
		_loadingSoundBanks.Remove(soundBankName);
	}

	public SoundBankLoader(AssetCache assetCache)
	{
		_loadedSoundBanks = new global::System.Collections.Generic.Dictionary<string, WwiseSoundBank>();
		_loadingSoundBanks = new global::System.Collections.Generic.Dictionary<string, LoadingSoundBank>();
		_assetCache = assetCache;
	}

	public void Teardown()
	{
		foreach (WwiseSoundBank value in _loadedSoundBanks.Values)
		{
			value.Teardown();
		}
		_loadedSoundBanks.Clear();
		_loadedSoundBanks = null;
		foreach (LoadingSoundBank value2 in _loadingSoundBanks.Values)
		{
			value2.Teardown();
		}
		_loadingSoundBanks.Clear();
		_loadingSoundBanks = null;
		_assetCache = null;
	}
}
