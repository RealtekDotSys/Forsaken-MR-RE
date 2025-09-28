public class PlayerGoodsResponseHandler : EventResponseHandler
{
	private global::System.Action<global::System.Collections.Generic.Dictionary<string, int>> PlayerGoodsUpdated;

	private global::System.Func<global::System.Collections.Generic.List<ServerData>, global::System.Collections.Generic.Dictionary<string, int>> ProcessPlayerGoods;

	public void Setup(global::System.Action<global::System.Collections.Generic.Dictionary<string, int>> callback, global::System.Func<global::System.Collections.Generic.List<ServerData>, global::System.Collections.Generic.Dictionary<string, int>> processor)
	{
		PlayerGoodsUpdated = callback;
		ProcessPlayerGoods = processor;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerDataList("Goods") != null)
		{
			HandleResponse(data.GetServerDataList("Goods"));
		}
	}

	private void HandleResponse(global::System.Collections.Generic.List<ServerData> data)
	{
		if (PlayerGoodsUpdated != null)
		{
			PlayerGoodsUpdated(ProcessPlayerGoods(data));
		}
	}
}
