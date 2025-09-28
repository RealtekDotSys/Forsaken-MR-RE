public class MapEntityResponseHandler : EventResponseHandler
{
	private global::System.Func<ServerData, MapEntitySynchronizeableState> loader;

	private global::System.Action<global::System.Collections.Generic.List<MapEntitySynchronizeableState>> playerMapEntityDataLoaded;

	public void Setup(global::System.Func<ServerData, MapEntitySynchronizeableState> entityLoader, global::System.Action<global::System.Collections.Generic.List<MapEntitySynchronizeableState>> callback)
	{
		loader = entityLoader;
		playerMapEntityDataLoaded = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerData("Reports") != null)
		{
			HandleResponse(data.GetServerData("Reports"));
		}
	}

	private void HandleResponse(ServerData data)
	{
		playerMapEntityDataLoaded(ConstructMapEntities(data));
	}

	private global::System.Collections.Generic.List<MapEntitySynchronizeableState> ConstructMapEntities(ServerData data)
	{
		global::System.Collections.Generic.List<MapEntitySynchronizeableState> list = new global::System.Collections.Generic.List<MapEntitySynchronizeableState>();
		foreach (string key in data.Keys)
		{
			list.Add(loader(data.GetServerData(key.ToString())));
		}
		return list;
	}
}
