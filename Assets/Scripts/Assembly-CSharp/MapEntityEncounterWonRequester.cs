public class MapEntityEncounterWonRequester : AbstractRequester
{
	private sealed class _003C_003Ec__DisplayClass1_0
	{
		public global::System.Action onComplete;

		internal void _003CFinishEncounter_003Eb__0(ServerResponse response)
		{
			onComplete();
		}

		internal void _003CFinishEncounter_003Eb__1(ServerData response)
		{
			onComplete();
		}
	}

	public MapEntityEncounterWonRequester(LogEventHandler eventHandler)
		: base(eventHandler)
	{
	}

	public void FinishEncounter(string entityId, global::System.Action onComplete, global::System.Collections.Generic.List<string> buffsConsumed = null)
	{
		MapEntityEncounterWonRequester._003C_003Ec__DisplayClass1_0 _003C_003Ec__DisplayClass1_ = new MapEntityEncounterWonRequester._003C_003Ec__DisplayClass1_0();
		_003C_003Ec__DisplayClass1_.onComplete = onComplete;
		LogEventRequest logEventRequest = new LogEventRequest();
		logEventRequest.SetEventKey("MAP_ENTITY_ENCOUNTER_WON");
		logEventRequest.SetEventAttribute("entityId", entityId);
		if (buffsConsumed != null)
		{
			logEventRequest.SetEventAttribute("buffsConsumed", buffsConsumed);
		}
		else
		{
			logEventRequest.SetEventAttribute("buffsConsumed", new global::System.Collections.Generic.List<string>());
		}
		logEventHandler.Send(logEventRequest, _003C_003Ec__DisplayClass1_._003CFinishEncounter_003Eb__0, _003C_003Ec__DisplayClass1_._003CFinishEncounter_003Eb__1);
	}
}
