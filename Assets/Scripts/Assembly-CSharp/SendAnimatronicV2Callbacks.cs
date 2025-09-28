public class SendAnimatronicV2Callbacks
{
	public readonly global::System.Action<SendAnimatronicV2ResponseData> successCallback;

	public readonly global::System.Action<string> errorCallback;

	public SendAnimatronicV2Callbacks(global::System.Action<SendAnimatronicV2ResponseData> success, global::System.Action<string> error)
	{
		successCallback = success;
		errorCallback = error;
	}
}
