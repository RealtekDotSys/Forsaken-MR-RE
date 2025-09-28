public class ScriptExecutionError
{
	public string error;

	public string message;

	public string stackTrace;

	public global::PlayFab.Json.JsonObject ToJson()
	{
		return new global::PlayFab.Json.JsonObject
		{
			["Error"] = error,
			["Message"] = message,
			["StackTrace"] = stackTrace
		};
	}
}
