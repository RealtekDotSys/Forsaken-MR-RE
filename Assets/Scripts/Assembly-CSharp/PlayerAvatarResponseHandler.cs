public class PlayerAvatarResponseHandler : EventResponseHandler
{
	private const string PLAYER_AVATAR_KEY = "ProfileAvatar";

	private global::System.Action<global::System.Collections.Generic.List<string>> AvatarListReceived;

	public void Setup(global::System.Action<global::System.Collections.Generic.List<string>> callback)
	{
		AvatarListReceived = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetStringList("ProfileAvatar") != null && AvatarListReceived != null)
		{
			AvatarListReceived(data.GetStringList("ProfileAvatar"));
		}
	}
}
