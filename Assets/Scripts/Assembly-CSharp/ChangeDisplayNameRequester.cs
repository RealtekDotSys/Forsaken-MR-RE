public interface ChangeDisplayNameRequester
{
	void ChangeDisplayName(string displayName);

	void Setup(global::System.Action<PlayerProfile> playerProfileCallback, global::System.Action<UserNameSetError> obscenityFoundCallback);
}
