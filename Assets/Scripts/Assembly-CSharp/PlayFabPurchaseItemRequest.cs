public class PlayFabPurchaseItemRequest : IPurchaseItemRequest
{
	private global::System.Action<PurchaseItemResultData> _resultCallback;

	private global::System.Action<IllumixErrorData> _errorCallback;

	private IllumixAuthenticationContext _authenticationContext;

	private readonly global::System.Action<global::PlayFab.ClientModels.PurchaseItemRequest, global::System.Action<global::PlayFab.ClientModels.PurchaseItemResult>, global::System.Action<global::PlayFab.PlayFabError>, object, global::System.Collections.Generic.Dictionary<string, string>> _purchaseItemApi;

	public PlayFabPurchaseItemRequest(global::System.Action<global::PlayFab.ClientModels.PurchaseItemRequest, global::System.Action<global::PlayFab.ClientModels.PurchaseItemResult>, global::System.Action<global::PlayFab.PlayFabError>, object, global::System.Collections.Generic.Dictionary<string, string>> purchaseItem)
	{
		if (purchaseItem != null)
		{
			_purchaseItemApi = purchaseItem;
		}
		else
		{
			new global::System.NullReferenceException("purchaseItem");
		}
	}

	public void SetCallbacks(global::System.Action<PurchaseItemResultData> result, global::System.Action<IllumixErrorData> error)
	{
		_resultCallback = result;
		_errorCallback = error;
	}

	public void SetAuthenticationContext(IllumixAuthenticationContext authContext)
	{
		_authenticationContext = authContext;
	}

	public void PurchaseItem(PurchaseItemParameters purchaseItemParameters)
	{
		global::PlayFab.ClientModels.PurchaseItemRequest arg = new global::PlayFab.ClientModels.PurchaseItemRequest
		{
			AuthenticationContext = DataConverter.GenerateAuthContext(_authenticationContext),
			CatalogVersion = purchaseItemParameters.catalogVersion,
			ItemId = purchaseItemParameters.itemId,
			Price = purchaseItemParameters.price,
			StoreId = purchaseItemParameters.storeId,
			VirtualCurrency = purchaseItemParameters.virtualCurrency
		};
		_purchaseItemApi(arg, OnPurchaseItemSuccess, OnPurchaseItemError, null, null);
	}

	private void OnPurchaseItemSuccess(global::PlayFab.ClientModels.PurchaseItemResult result)
	{
		_resultCallback(new PurchaseItemResultData(result));
	}

	private void OnPurchaseItemError(global::PlayFab.PlayFabError error)
	{
		_errorCallback(DataConverter.GenerateErrorData(error));
	}
}
