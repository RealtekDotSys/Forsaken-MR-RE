public class PlayerDataRequester : AbstractRequester
{
	public PlayerDataRequester(LogEventHandler eventHandler)
		: base(eventHandler)
	{
	}

	public void GetPlayerData(global::System.Action<ServerData> ErrorResponse)
	{
		LogEventRequest logEventRequest = new LogEventRequest();
		logEventRequest.SetEventKey("GET_PLAYER_DATA");
		logEventHandler.Send(logEventRequest, AbstractRequester.NullResponse, ErrorResponse);
	}
}
