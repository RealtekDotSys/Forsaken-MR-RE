public class PlayFabExecuteScriptRequest : IExecuteScriptRequest
{
	private sealed class _003C_003Ec__DisplayClass2_0
	{
		public ExecuteScriptParameters parameters;

		internal void _003CExecuteParameters_003Eg__OnExecuteCloudScriptResponse_007C0(global::PlayFab.ClientModels.ExecuteCloudScriptResult executeCloudScriptResult)
		{
			if (parameters.resultCallback != null)
			{
				parameters.resultCallback(DataConverter.GenerateExecuteScriptResult(executeCloudScriptResult));
			}
		}

		internal void _003CExecuteParameters_003Eg__OnExecuteCloudScriptError_007C1(global::PlayFab.PlayFabError playfabError)
		{
			if (parameters.errorCallback != null)
			{
				parameters.errorCallback(DataConverter.GenerateErrorData(playfabError));
			}
		}
	}

	private readonly global::System.Action<global::PlayFab.ClientModels.ExecuteCloudScriptRequest, global::System.Action<global::PlayFab.ClientModels.ExecuteCloudScriptResult>, global::System.Action<global::PlayFab.PlayFabError>, object, global::System.Collections.Generic.Dictionary<string, string>> _executeCloudScriptApi;

	public PlayFabExecuteScriptRequest(global::System.Action<global::PlayFab.ClientModels.ExecuteCloudScriptRequest, global::System.Action<global::PlayFab.ClientModels.ExecuteCloudScriptResult>, global::System.Action<global::PlayFab.PlayFabError>, object, global::System.Collections.Generic.Dictionary<string, string>> executeCloudScriptApi)
	{
		if (executeCloudScriptApi != null)
		{
			_executeCloudScriptApi = executeCloudScriptApi;
		}
		else
		{
			new global::System.NullReferenceException("executeCloudScriptAPI");
		}
	}

	public void ExecuteParameters(ExecuteScriptParameters parameters)
	{
		int value = 0;
		global::PlayFab.ClientModels.CloudScriptRevisionOption value2 = global::PlayFab.ClientModels.CloudScriptRevisionOption.Live;
		if (global::UnityEngine.PlayerPrefs.HasKey("SpecificRevision") || ConstantVariables.Instance.SpecificRevision != 0)
		{
			value2 = global::PlayFab.ClientModels.CloudScriptRevisionOption.Specific;
			value = (global::UnityEngine.PlayerPrefs.HasKey("SpecificRevision") ? global::UnityEngine.PlayerPrefs.GetInt("SpecificRevision") : ConstantVariables.Instance.SpecificRevision);
		}
		global::PlayFab.ClientModels.ExecuteCloudScriptRequest arg = new global::PlayFab.ClientModels.ExecuteCloudScriptRequest
		{
			FunctionName = parameters.functionName,
			FunctionParameter = parameters.functionParameter,
			GeneratePlayStreamEvent = true,
			AuthenticationContext = DataConverter.GenerateAuthContext(parameters.authContext),
			RevisionSelection = value2,
			SpecificRevision = value
		};
		PlayFabExecuteScriptRequest._003C_003Ec__DisplayClass2_0 _003C_003Ec__DisplayClass2_ = new PlayFabExecuteScriptRequest._003C_003Ec__DisplayClass2_0();
		_003C_003Ec__DisplayClass2_.parameters = parameters;
		global::UnityEngine.Debug.LogError("EXECUTING SCRIPT REQUEST " + parameters.functionName);
		_executeCloudScriptApi(arg, _003C_003Ec__DisplayClass2_._003CExecuteParameters_003Eg__OnExecuteCloudScriptResponse_007C0, _003C_003Ec__DisplayClass2_._003CExecuteParameters_003Eg__OnExecuteCloudScriptError_007C1, null, null);
	}
}
