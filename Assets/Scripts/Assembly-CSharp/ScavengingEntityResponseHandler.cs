public class ScavengingEntityResponseHandler : EventResponseHandler
{
	private global::System.Func<ServerData, ScavengingEntitySynchronizeableState> loader;

	private global::System.Action<global::System.Collections.Generic.List<ScavengingEntitySynchronizeableState>> playerScavengingEntityDataLoaded;

	public void Setup(global::System.Func<ServerData, ScavengingEntitySynchronizeableState> entityLoader, global::System.Action<global::System.Collections.Generic.List<ScavengingEntitySynchronizeableState>> callback)
	{
		loader = entityLoader;
		playerScavengingEntityDataLoaded = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerData("ScavengingEntities") != null)
		{
			HandleResponse(data.GetServerData("ScavengingEntities"));
		}
	}

	private void HandleResponse(ServerData data)
	{
		playerScavengingEntityDataLoaded(ConstructMapEntities(data));
	}

	private global::System.Collections.Generic.List<ScavengingEntitySynchronizeableState> ConstructMapEntities(ServerData data)
	{
		global::System.Collections.Generic.List<ScavengingEntitySynchronizeableState> list = new global::System.Collections.Generic.List<ScavengingEntitySynchronizeableState>();
		foreach (string key in data.Keys)
		{
			list.Add(loader(data.GetServerData(key.ToString())));
		}
		return list;
	}
}
