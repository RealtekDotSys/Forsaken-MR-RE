public class SellModsRequester : AbstractRequester
{
	public SellModsRequester(LogEventHandler eventHandler)
		: base(eventHandler)
	{
	}

	public void SellMods(global::System.Collections.Generic.Dictionary<ModData, int> modTable)
	{
		LogEventRequest logEventRequest = new LogEventRequest();
		logEventRequest.SetEventKey("SELL_MODS");
		logEventRequest.SetEventAttribute("mods", ConvertTableToRequest(modTable));
		logEventHandler.Send(logEventRequest, AbstractRequester.NullResponse, AbstractRequester.NullErrorResponse);
	}

	private ServerRequestData ConvertTableToRequest(global::System.Collections.Generic.Dictionary<ModData, int> table)
	{
		ServerRequestData serverRequestData = new ServerRequestData();
		foreach (ModData key in table.Keys)
		{
			serverRequestData.AddNumber(key.Id, table[key]);
		}
		return serverRequestData;
	}
}
