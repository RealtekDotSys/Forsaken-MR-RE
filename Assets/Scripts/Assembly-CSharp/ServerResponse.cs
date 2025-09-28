public class ServerResponse
{
	public readonly ServerData ScriptData;

	public readonly ServerData Errors;

	public virtual string JSONString => ScriptData.ToString();

	public virtual bool HasErrors
	{
		get
		{
			if (Errors == null)
			{
				return false;
			}
			global::UnityEngine.Debug.LogError("scriptError key count: " + Errors.Keys.Count);
			global::UnityEngine.Debug.LogError("scriptErrors: " + Errors.JSON);
			return Errors.Keys.Count != 0;
		}
	}

	public virtual ServerData BaseData => ScriptData;

	public ServerResponse(ServerData response)
	{
		ScriptData = response.GetServerData("scriptData");
		Errors = response.GetServerData("scriptError");
	}
}
