public class PlayerStoreDataResponseHandler : EventResponseHandler
{
	private const string PLAYER_STORE_DATA = "PlayerStoreData";

	private const string DEFERRED_TIMESTAMP = "DeferredPaymentTimestamp";

	public global::System.Action<long> PlayerStoreDataReceived;

	public void Setup(global::System.Action<long> callback)
	{
		PlayerStoreDataReceived = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerData("PlayerStoreData") != null)
		{
			ProcessStoreData(data.GetServerData("PlayerStoreData"));
		}
	}

	public void ProcessStoreData(ServerData data)
	{
		if (PlayerStoreDataReceived != null)
		{
			PlayerStoreDataReceived(data.GetLong("DeferredPaymentTimestamp").Value);
		}
	}
}
