public class ScavengingEntityRetireEntitiesRequester : AbstractRequester
{
	public ScavengingEntityRetireEntitiesRequester(LogEventHandler eventHandler)
		: base(eventHandler)
	{
	}

	public void RetireEntities(global::System.Collections.Generic.List<string> entitiesToRetire)
	{
		LogEventRequest logEventRequest = new LogEventRequest();
		logEventRequest.SetEventKey("SCAVENGING_ENTITY_RETIRE_ENTITIES");
		logEventRequest.SetEventAttribute("entitiesToRetire", entitiesToRetire);
		logEventHandler.Send(logEventRequest, AbstractRequester.NullResponse, AbstractRequester.NullErrorResponse);
	}
}
