public class CurrencyDataResponseHandler : EventResponseHandler
{
	private global::System.Action<global::System.Collections.Generic.Dictionary<string, int>> GotCurrencyData;

	public void Setup(global::System.Action<global::System.Collections.Generic.Dictionary<string, int>> callback)
	{
		GotCurrencyData = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerData("Currency") != null)
		{
			HandleResponse(data.GetServerData("Currency"));
		}
	}

	private void HandleResponse(ServerData currency)
	{
		global::System.Collections.Generic.Dictionary<string, int> dictionary = new global::System.Collections.Generic.Dictionary<string, int>();
		if (currency.GetInt("FAZ_TOKENS").HasValue)
		{
			dictionary.Add("FAZ_TOKENS", currency.GetInt("FAZ_TOKENS").Value);
		}
		if (currency.GetInt("PARTS").HasValue)
		{
			dictionary.Add("PARTS", currency.GetInt("PARTS").Value);
		}
		if (currency.GetInt("ESSENCE").HasValue)
		{
			dictionary.Add("ESSENCE", currency.GetInt("ESSENCE").Value);
		}
		if (currency.GetInt("EVENT_CURRENCY").HasValue)
		{
			dictionary.Add("EVENT_CURRENCY", currency.GetInt("EVENT_CURRENCY").Value);
		}
		GotCurrencyData(dictionary);
	}
}
