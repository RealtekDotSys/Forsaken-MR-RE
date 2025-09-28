public interface IValidatePurchaseReceiptRequest
{
	void SetCallbacks(global::System.Action result, global::System.Action<IllumixErrorData> error);

	void ValidatePurchaseReceipt(GooglePlayPurchase purchase);
}
