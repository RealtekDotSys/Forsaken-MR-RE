public class CurrencyBank
{
	private readonly global::System.Collections.Generic.Dictionary<Currency.CurrencyType, int> accounts;

	public CurrencyBank()
	{
		accounts = new global::System.Collections.Generic.Dictionary<Currency.CurrencyType, int>();
	}

	public void SetBank(global::System.Collections.Generic.Dictionary<string, int> bankData)
	{
		accounts.Clear();
		UpdateBank(bankData);
	}

	public void UpdateBank(global::System.Collections.Generic.Dictionary<string, int> bankData)
	{
		foreach (string key in bankData.Keys)
		{
			Currency.CurrencyType typeForString = Currency.GetTypeForString(key);
			if (!accounts.ContainsKey(typeForString))
			{
				accounts.Add(typeForString, bankData[key]);
			}
			else
			{
				accounts[typeForString] = bankData[key];
			}
		}
	}

	public void AddCurrency(string currencyName, int amount)
	{
		Currency.CurrencyType typeForString = Currency.GetTypeForString(currencyName);
		if (accounts.ContainsKey(typeForString))
		{
			accounts[typeForString] += amount;
		}
	}

	public int GetCurrency(string currencyName)
	{
		Currency.CurrencyType typeForString = Currency.GetTypeForString(currencyName);
		if (!accounts.ContainsKey(typeForString))
		{
			global::UnityEngine.Debug.LogError("no currency account for currency " + currencyName);
			return 0;
		}
		return accounts[typeForString];
	}

	public bool CanAfford(Currency.CurrencyType type, int amount)
	{
		return GetCurrency(Currency.GetCurrencyString(type)) >= amount;
	}
}
