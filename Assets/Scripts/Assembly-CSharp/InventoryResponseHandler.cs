public class InventoryResponseHandler : EventResponseHandler
{
	private global::System.Action<PlayerInventory> InventoryUpdated;

	public void Setup(global::System.Action<PlayerInventory> callback)
	{
		InventoryUpdated = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerData("Inventory") != null)
		{
			InventoryUpdated(ConstructPlayerInventory(data.GetServerData("Inventory")));
		}
	}

	private static PlayerInventory ConstructPlayerInventory(ServerData data)
	{
		global::UnityEngine.Debug.LogWarning("Constructing player inventory!");
		PlayerInventory playerInventory = new PlayerInventory();
		foreach (string key in data.Keys)
		{
			playerInventory.SetItem(key, ConstructAnimatronicInventory(data.GetServerData(key)));
		}
		return playerInventory;
	}

	private static PlayerAnimatronicInventory ConstructAnimatronicInventory(ServerData data)
	{
		PlayerAnimatronicInventory playerAnimatronicInventory = new PlayerAnimatronicInventory();
		foreach (string key in data.Keys)
		{
			global::UnityEngine.Debug.LogWarning("OWNED SUIT " + data.ToString());
			playerAnimatronicInventory.AddItem(key, data.GetInt(key).Value);
		}
		return playerAnimatronicInventory;
	}
}
