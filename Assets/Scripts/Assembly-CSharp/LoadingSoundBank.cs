public class LoadingSoundBank
{
	private readonly string _soundBankName;

	private global::System.Collections.Generic.List<SoundBankRequest> _pendingRequests;

	private WwiseSoundBank _loadingSoundBank;

	private global::System.Action<string, WwiseSoundBank> _onSuccess;

	private global::System.Action<string> _onFailure;

	public LoadingSoundBank(SoundBankRequest request, global::System.Action<string, WwiseSoundBank> onSuccess, global::System.Action<string> onFailure)
	{
		_pendingRequests = new global::System.Collections.Generic.List<SoundBankRequest>();
		_soundBankName = request.SoundBankName;
		_pendingRequests.Add(request);
		_onSuccess = onSuccess;
		_onFailure = onFailure;
	}

	public void AddRequest(SoundBankRequest request)
	{
		_pendingRequests.Add(request);
	}

	public void Load(AssetCache assetCache)
	{
		_loadingSoundBank = new WwiseSoundBank();
		_loadingSoundBank.Setup(assetCache, _soundBankName, LoadSuccess, LoadFailure);
	}

	private void LoadSuccess(string soundBankName)
	{
		foreach (SoundBankRequest pendingRequest in _pendingRequests)
		{
			pendingRequest.Success?.Invoke(_soundBankName);
		}
		_onSuccess?.Invoke(_soundBankName, _loadingSoundBank);
	}

	private void LoadFailure(string soundBankName)
	{
		foreach (SoundBankRequest pendingRequest in _pendingRequests)
		{
			pendingRequest.Failure?.Invoke(_soundBankName);
		}
		_onFailure?.Invoke(_soundBankName);
	}

	public void Teardown()
	{
		_pendingRequests.Clear();
		_pendingRequests = null;
	}
}
