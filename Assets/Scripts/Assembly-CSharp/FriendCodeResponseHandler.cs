public class FriendCodeResponseHandler : EventResponseHandler
{
	public enum FriendLookupResponse
	{
		SUCCESS = 0,
		GROUP_NOT_FOUND = 1,
		USER_TRIED_TO_FRIEND_SELF = 2,
		USERS_WERE_ALREADY_FRIENDS = 3,
		UNSPECIFIED_ERROR = 4
	}

	private global::System.Action<string> OnFriendCodeUpdated;

	private global::System.Action<FriendCodeResponseHandler.FriendLookupResponse> OnFriendCodeLookedUp;

	private static readonly string[] FriendLookupServerString;

	public void Setup(global::System.Action<string> callbackUpdated, global::System.Action<FriendCodeResponseHandler.FriendLookupResponse> callbackLookedUp)
	{
		OnFriendCodeUpdated = callbackUpdated;
		OnFriendCodeLookedUp = callbackLookedUp;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetString("friendCode") != null)
		{
			HandleResponseRefresh(data.GetString("friendCode"));
		}
		if (data.GetString("lookupFriendCodeResponse") != null)
		{
			HandleResponseLookup(data.GetString("lookupFriendCodeResponse"));
		}
	}

	private void HandleResponseRefresh(string friendCode)
	{
		OnFriendCodeUpdated(friendCode);
	}

	private void HandleResponseLookup(string resultString)
	{
		switch (resultString)
		{
		case "success":
			OnFriendCodeLookedUp(FriendCodeResponseHandler.FriendLookupResponse.SUCCESS);
			break;
		case "groupNotFound":
			OnFriendCodeLookedUp(FriendCodeResponseHandler.FriendLookupResponse.GROUP_NOT_FOUND);
			break;
		case "userTriedToFriendSelf":
			OnFriendCodeLookedUp(FriendCodeResponseHandler.FriendLookupResponse.USER_TRIED_TO_FRIEND_SELF);
			break;
		case "usersWereAlreadyFriends":
			OnFriendCodeLookedUp(FriendCodeResponseHandler.FriendLookupResponse.USERS_WERE_ALREADY_FRIENDS);
			break;
		default:
			OnFriendCodeLookedUp(FriendCodeResponseHandler.FriendLookupResponse.UNSPECIFIED_ERROR);
			break;
		}
	}

	static FriendCodeResponseHandler()
	{
		FriendLookupServerString = new string[4] { "success", "groupNotFound", "userTriedToFriendSelf", "usersWereAlreadyFriends" };
	}
}
