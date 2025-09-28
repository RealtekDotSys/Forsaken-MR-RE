public class IllumixChangeDisplayNameRequester : ChangeDisplayNameRequester
{
	private const string ClassName = "IllumixChangeDisplayNameRequester";

	private global::System.Action<UserNameSetError> _obscenityFoundCallback;

	private global::System.Action<PlayerProfile> _playerProfileUpdated;

	private IUpdateUserTitleDisplayNameRequest _updateUserTitleDisplayNameRequest;

	public IllumixChangeDisplayNameRequester(IUpdateUserTitleDisplayNameRequest updateUserTitleDisplayNameRequest)
	{
		_updateUserTitleDisplayNameRequest = updateUserTitleDisplayNameRequest;
		updateUserTitleDisplayNameRequest.SetCallbacks(OnUserTitleDisplayNameUpdated, IllumixErrorCallback);
	}

	public void Setup(global::System.Action<PlayerProfile> playerProfileCallback, global::System.Action<UserNameSetError> obscenityFoundCallback)
	{
		_obscenityFoundCallback = obscenityFoundCallback;
		_playerProfileUpdated = playerProfileCallback;
	}

	public void ChangeDisplayName(string displayName)
	{
		_updateUserTitleDisplayNameRequest.UpdateWithDisplayName(displayName);
	}

	private void OnUserTitleDisplayNameUpdated(string displayName)
	{
		PlayerProfile playerProfile = MasterDomain.GetDomain().TheGameDomain.loginDomain.playerProfile;
		playerProfile.displayName = displayName;
		if (_playerProfileUpdated != null)
		{
			_playerProfileUpdated(playerProfile);
		}
	}

	private void IllumixErrorCallback(IllumixErrorData error)
	{
		if (error.errorCode == "NameNotAvailable")
		{
			_obscenityFoundCallback(UserNameSetError.NAME_NOT_UNIQUE);
		}
		else if (error.errorCode == "ProfaneDisplayName")
		{
			_obscenityFoundCallback(UserNameSetError.NAME_OBSCENE);
		}
		else
		{
			global::UnityEngine.Debug.LogError("IllumixChangeDisplayNameRequester IllumixErrorCallback - " + error.errorMessage);
		}
	}
}
