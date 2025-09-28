public class FriendsListResponseHandler : EventResponseHandler
{
	private global::System.Action<global::System.Collections.Generic.List<PlayerFriendsEntry>> OnFriendsListUpdated;

	public void Setup(global::System.Action<global::System.Collections.Generic.List<PlayerFriendsEntry>> callback)
	{
		OnFriendsListUpdated = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerData("friendsList") != null)
		{
			HandleResponse(data.GetServerData("friendsList"));
		}
	}

	private void HandleResponse(ServerData friendsListData)
	{
		global::System.Collections.Generic.List<PlayerFriendsEntry> list = new global::System.Collections.Generic.List<PlayerFriendsEntry>();
		foreach (string key in friendsListData.Keys)
		{
			list.Add(ServerProcessors.ProcessPlayerFriendsEntry(friendsListData.GetServerData(key)));
		}
		OnFriendsListUpdated(list);
	}
}
