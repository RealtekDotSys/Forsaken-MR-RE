public class TrophyInventoryResponseHandler : EventResponseHandler
{
	private global::System.Action<TrophyInventory> TrophyInventoryUpdated;

	public void Setup(global::System.Action<TrophyInventory> callback)
	{
		TrophyInventoryUpdated = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerData("TrophyInventory") != null)
		{
			HandleResponse(data.GetServerData("TrophyInventory"));
		}
	}

	private void HandleResponse(ServerData data)
	{
		TrophyInventory trophyInventory = new TrophyInventory();
		foreach (string key in data.Keys)
		{
			trophyInventory.Add(key, data.GetInt(key).Value);
		}
		TrophyInventoryUpdated(trophyInventory);
	}
}
