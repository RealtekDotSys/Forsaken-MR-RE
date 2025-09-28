public class MapEntityEncounterLeftRequester : AbstractRequester
{
	public MapEntityEncounterLeftRequester(LogEventHandler eventHandler)
		: base(eventHandler)
	{
	}

	public void LeaveEncounter(string entityId, global::System.Collections.Generic.List<string> buffsConsumed = null)
	{
		LogEventRequest logEventRequest = new LogEventRequest();
		logEventRequest.SetEventKey("MAP_ENTITY_ENCOUNTER_LEFT");
		logEventRequest.SetEventAttribute("entityId", entityId);
		if (buffsConsumed != null)
		{
			logEventRequest.SetEventAttribute("buffsConsumed", buffsConsumed);
		}
		else
		{
			logEventRequest.SetEventAttribute("buffsConsumed", new global::System.Collections.Generic.List<string>());
		}
		logEventHandler.Send(logEventRequest, AbstractRequester.NullResponse, AbstractRequester.NullErrorResponse);
	}
}
