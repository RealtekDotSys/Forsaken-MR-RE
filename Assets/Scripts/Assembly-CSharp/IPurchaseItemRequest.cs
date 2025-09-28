public interface IPurchaseItemRequest
{
	void SetCallbacks(global::System.Action<PurchaseItemResultData> result, global::System.Action<IllumixErrorData> error);

	void SetAuthenticationContext(IllumixAuthenticationContext authContext);

	void PurchaseItem(PurchaseItemParameters purchaseItemParameters);
}
