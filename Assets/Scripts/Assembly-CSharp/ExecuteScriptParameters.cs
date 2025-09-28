public class ExecuteScriptParameters
{
	public string functionName;

	public global::PlayFab.Json.JsonObject functionParameter;

	public global::System.Action<ExecuteScriptResult> resultCallback;

	public global::System.Action<IllumixErrorData> errorCallback;

	public IllumixAuthenticationContext authContext;

	public bool generatePlayStreamEvent;

	public ExecuteScriptParameters()
	{
	}

	public ExecuteScriptParameters(ExecuteScriptParameters scriptParameters)
	{
		if (scriptParameters != null)
		{
			functionName = scriptParameters.functionName;
			functionParameter = scriptParameters.functionParameter;
			resultCallback = scriptParameters.resultCallback;
			errorCallback = scriptParameters.errorCallback;
			authContext = scriptParameters.authContext;
			generatePlayStreamEvent = scriptParameters.generatePlayStreamEvent;
		}
	}
}
