public static class DataConverter
{
	public static IllumixErrorData GenerateErrorData(global::PlayFab.PlayFabError playFabError)
	{
		return new IllumixErrorData
		{
			apiEndpoint = playFabError.ApiEndpoint,
			ErrorDetails = playFabError.ErrorDetails,
			httpCode = playFabError.HttpCode,
			httpStatus = playFabError.HttpStatus,
			errorMessage = playFabError.ErrorMessage,
			errorCode = playFabError.Error.ToString()
		};
	}

	public static ExecuteScriptResult GenerateExecuteScriptResult(global::PlayFab.ClientModels.ExecuteCloudScriptResult cloudScriptResult)
	{
		ExecuteScriptResult executeScriptResult = new ExecuteScriptResult();
		ScriptExecutionError error = GenerateScriptExecutionError(cloudScriptResult.Error);
		executeScriptResult.error = error;
		executeScriptResult.functionResult = (global::PlayFab.Json.JsonObject)cloudScriptResult.FunctionResult;
		return executeScriptResult;
	}

	public static global::PlayFab.PlayFabAuthenticationContext GenerateAuthContext(IllumixAuthenticationContext authContext)
	{
		if (authContext != null)
		{
			return new global::PlayFab.PlayFabAuthenticationContext(authContext.clientSessionTicket, authContext.entityToken, authContext.illumixId, authContext.entityId, authContext.entityType);
		}
		return null;
	}

	private static ScriptExecutionError GenerateScriptExecutionError(global::PlayFab.ClientModels.ScriptExecutionError scriptExecutionError)
	{
		ScriptExecutionError scriptExecutionError2 = new ScriptExecutionError();
		if (scriptExecutionError == null)
		{
			return scriptExecutionError2;
		}
		scriptExecutionError2.error = scriptExecutionError.Error;
		scriptExecutionError2.message = scriptExecutionError.Message;
		scriptExecutionError2.stackTrace = scriptExecutionError.StackTrace;
		return scriptExecutionError2;
	}
}
