public class ModInventoryResponseHandler : EventResponseHandler
{
	private global::System.Action<ModInventory> ModInventoryUpdated;

	public void Setup(global::System.Action<ModInventory> callback)
	{
		ModInventoryUpdated = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerData("ModInventory") != null && ModInventoryUpdated != null)
		{
			ModInventoryUpdated(ConstructModInventory(data.GetServerData("ModInventory")));
		}
	}

	private static ModInventory ConstructModInventory(ServerData data)
	{
		ModInventory modInventory = new ModInventory();
		foreach (string key in data.Keys)
		{
			modInventory.AddMod(new ModData(key), data.GetInt(key).Value);
		}
		return modInventory;
	}
}
