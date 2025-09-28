public class SetEndoskeletonConfigRequester : AbstractRequester
{
	public SetEndoskeletonConfigRequester(LogEventHandler eventHandler)
		: base(eventHandler)
	{
	}

	public void SetEndoskeletonConfig(int slot, EndoskeletonData endoskeleton)
	{
		LogEventRequest logEventRequest = new LogEventRequest();
		logEventRequest.SetEventKey("SET_ENDOSKELETON_CONFIG_V2");
		logEventRequest.SetEventAttribute("slot", slot);
		global::UnityEngine.Debug.LogError(slot);
		logEventRequest.SetEventAttribute("config", ConvertEndoskeletonToGSRequestData(endoskeleton));
		logEventHandler.Send(logEventRequest, AbstractRequester.NullResponse, AbstractRequester.NullErrorResponse);
	}

	private ServerRequestData ConvertEndoskeletonToGSRequestData(EndoskeletonData endoskeleton)
	{
		ServerRequestData serverRequestData = new ServerRequestData();
		serverRequestData.AddStringList("mods", endoskeleton.mods);
		serverRequestData.AddString("cpu", endoskeleton.cpu);
		serverRequestData.AddString("plushSuit", endoskeleton.plushSuit);
		serverRequestData.AddNumber("essence", endoskeleton.numEssence);
		global::UnityEngine.Debug.LogError(serverRequestData.BaseData);
		return serverRequestData;
	}
}
