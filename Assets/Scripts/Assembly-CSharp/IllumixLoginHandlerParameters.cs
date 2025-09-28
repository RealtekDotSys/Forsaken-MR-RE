public class IllumixLoginHandlerParameters
{
	public readonly IUpdateUserTitleDisplayNameRequest updateUserTitleDisplayNameRequest;

	public readonly global::System.Action<bool> onGeneratePlayStreamEventReceived;

	public IllumixLoginHandlerParameters(ServerDomain illumixServerDomain, global::System.Action<bool> generatePlayStreamEventReceived)
	{
		updateUserTitleDisplayNameRequest = illumixServerDomain.CreateUpdateUserTitleDisplayNameRequest();
		onGeneratePlayStreamEventReceived = generatePlayStreamEventReceived;
	}
}
