public class PlayFabValidatePurchaseReceiptRequest : IValidatePurchaseReceiptRequest
{
	private global::System.Action _resultCallback;

	private global::System.Action<IllumixErrorData> _errorCallback;

	private readonly global::System.Action<global::PlayFab.ClientModels.ValidateGooglePlayPurchaseRequest, global::System.Action<global::PlayFab.ClientModels.ValidateGooglePlayPurchaseResult>, global::System.Action<global::PlayFab.PlayFabError>, object, global::System.Collections.Generic.Dictionary<string, string>> _validateIosReceipt;

	public PlayFabValidatePurchaseReceiptRequest(global::System.Action<global::PlayFab.ClientModels.ValidateGooglePlayPurchaseRequest, global::System.Action<global::PlayFab.ClientModels.ValidateGooglePlayPurchaseResult>, global::System.Action<global::PlayFab.PlayFabError>, object, global::System.Collections.Generic.Dictionary<string, string>> validateIosReceipt)
	{
		if (validateIosReceipt != null)
		{
			_validateIosReceipt = validateIosReceipt;
		}
		else
		{
			new global::System.NullReferenceException("validateIosReceipt");
		}
	}

	public void SetCallbacks(global::System.Action result, global::System.Action<IllumixErrorData> error)
	{
		_resultCallback = result;
		_errorCallback = error;
	}

	public void ValidatePurchaseReceipt(GooglePlayPurchase purchase)
	{
		global::PlayFab.ClientModels.ValidateGooglePlayPurchaseRequest arg = new global::PlayFab.ClientModels.ValidateGooglePlayPurchaseRequest
		{
			CatalogVersion = purchase.catalogVersion,
			CurrencyCode = purchase.currencyCode,
			PurchasePrice = purchase.purchasePrice,
			Signature = purchase.signature,
			ReceiptJson = purchase.receiptJson
		};
		_validateIosReceipt(arg, OnValidateGooglePlayPurchaseSuccess, OnValidateGooglePlayPurchaseFailure, null, null);
	}

	private void OnValidateGooglePlayPurchaseSuccess(global::PlayFab.ClientModels.ValidateGooglePlayPurchaseResult result)
	{
		_resultCallback();
	}

	private void OnValidateGooglePlayPurchaseFailure(global::PlayFab.PlayFabError playFabError)
	{
		_errorCallback(DataConverter.GenerateErrorData(playFabError));
	}
}
