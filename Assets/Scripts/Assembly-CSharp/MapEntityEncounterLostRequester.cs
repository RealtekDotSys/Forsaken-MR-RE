public class MapEntityEncounterLostRequester : AbstractRequester
{
	public MapEntityEncounterLostRequester(LogEventHandler eventHandler)
		: base(eventHandler)
	{
	}

	public void FinishEncounter(string entityId, global::System.Collections.Generic.List<string> buffsConsumed = null)
	{
		LogEventRequest logEventRequest = new LogEventRequest();
		logEventRequest.SetEventKey("MAP_ENTITY_ENCOUNTER_LOST");
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
