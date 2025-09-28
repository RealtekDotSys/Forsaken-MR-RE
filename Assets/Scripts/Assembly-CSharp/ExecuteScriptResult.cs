public class ExecuteScriptResult
{
	public ScriptExecutionError error;

	public global::PlayFab.Json.JsonObject functionResult;

	public bool HasError()
	{
		return !string.IsNullOrEmpty(error.error);
	}
}
