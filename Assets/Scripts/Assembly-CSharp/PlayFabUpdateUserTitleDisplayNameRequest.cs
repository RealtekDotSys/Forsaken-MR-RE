public class PlayFabUpdateUserTitleDisplayNameRequest : IUpdateUserTitleDisplayNameRequest
{
	private global::System.Action<string> _resultCallback;

	private global::System.Action<IllumixErrorData> _errorCallback;

	private readonly global::System.Action<global::PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest, global::System.Action<global::PlayFab.ClientModels.UpdateUserTitleDisplayNameResult>, global::System.Action<global::PlayFab.PlayFabError>, object, global::System.Collections.Generic.Dictionary<string, string>> _updateUserTitleDisplayName;

	public PlayFabUpdateUserTitleDisplayNameRequest(global::System.Action<global::PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest, global::System.Action<global::PlayFab.ClientModels.UpdateUserTitleDisplayNameResult>, global::System.Action<global::PlayFab.PlayFabError>, object, global::System.Collections.Generic.Dictionary<string, string>> updateUserTitleDisplayName)
	{
		if (updateUserTitleDisplayName != null)
		{
			_updateUserTitleDisplayName = updateUserTitleDisplayName;
		}
		else
		{
			new global::System.NullReferenceException("updateUserTitleDisplayName");
		}
	}

	public void SetCallbacks(global::System.Action<string> result, global::System.Action<IllumixErrorData> error)
	{
		_resultCallback = result;
		_errorCallback = error;
	}

	public void UpdateWithDisplayName(string displayName)
	{
		global::PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest updateUserTitleDisplayNameRequest = new global::PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest();
		updateUserTitleDisplayNameRequest.DisplayName = displayName;
		_updateUserTitleDisplayName(updateUserTitleDisplayNameRequest, OnChangeDisplayNameSuccess, OnChangeDisplayNameError, null, null);
	}

	private void OnChangeDisplayNameSuccess(global::PlayFab.ClientModels.UpdateUserTitleDisplayNameResult result)
	{
		_resultCallback(result.DisplayName);
	}

	private void OnChangeDisplayNameError(global::PlayFab.PlayFabError playFabError)
	{
		_errorCallback(DataConverter.GenerateErrorData(playFabError));
	}
}
