public class PlayerAvatarDataModel
{
	private global::System.Action<global::System.Collections.Generic.List<string>> AvatarUnlocksUpdated;

	public global::System.Collections.Generic.List<string> UnlockedAvatarIDs { get; set; }

	public void add_AvatarUnlocksUpdated(global::System.Action<global::System.Collections.Generic.List<string>> value)
	{
		AvatarUnlocksUpdated = (global::System.Action<global::System.Collections.Generic.List<string>>)global::System.Delegate.Combine(AvatarUnlocksUpdated, value);
	}

	public void remove_AvatarUnlocksUpdated(global::System.Action<global::System.Collections.Generic.List<string>> value)
	{
		AvatarUnlocksUpdated = (global::System.Action<global::System.Collections.Generic.List<string>>)global::System.Delegate.Remove(AvatarUnlocksUpdated, value);
	}

	private void EventExposer_PlayerAvatarUnlockedListReceived(global::System.Collections.Generic.List<string> list)
	{
		UnlockedAvatarIDs = list;
		if (AvatarUnlocksUpdated != null)
		{
			AvatarUnlocksUpdated(list);
		}
	}

	public PlayerAvatarDataModel(EventExposer eventExposer)
	{
		UnlockedAvatarIDs = new global::System.Collections.Generic.List<string> { "default", "female", "male" };
		eventExposer.add_PlayerAvatarUnlockedListReceived(EventExposer_PlayerAvatarUnlockedListReceived);
	}
}
