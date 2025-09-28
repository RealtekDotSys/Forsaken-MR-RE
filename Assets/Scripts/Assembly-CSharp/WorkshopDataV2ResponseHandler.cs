public class WorkshopDataV2ResponseHandler : EventResponseHandler
{
	private global::System.Action<WorkshopData> WorkshopDataV2Updated;

	private global::System.Func<ServerData, WorkshopEntry> ProcessWorkshopDataEntry;

	public void Setup(global::System.Action<WorkshopData> callback, global::System.Func<ServerData, WorkshopEntry> processor)
	{
		WorkshopDataV2Updated = callback;
		ProcessWorkshopDataEntry = processor;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerDataList("WarehouseDataV2") != null)
		{
			HandleResponse(data.GetServerDataList("WarehouseDataV2"));
		}
	}

	private void HandleResponse(global::System.Collections.Generic.List<ServerData> data)
	{
		WorkshopData workshopData = new WorkshopData();
		foreach (ServerData datum in data)
		{
			workshopData.Add(ProcessWorkshopDataEntry(datum));
		}
		WorkshopDataV2Updated(workshopData);
	}
}
