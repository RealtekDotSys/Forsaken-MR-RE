public interface IUpdateUserTitleDisplayNameRequest
{
	void SetCallbacks(global::System.Action<string> result, global::System.Action<IllumixErrorData> error);

	void UpdateWithDisplayName(string displayName);
}
