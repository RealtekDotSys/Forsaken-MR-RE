public class Currency
{
	public enum CurrencyType
	{
		HardCurrency = 0,
		Parts = 1,
		Essence = 2,
		RealMoney = 3,
		EventCurrency = 4
	}

	public const string FAZ_TOKEN = "FAZ_TOKENS";

	public const string PARTS = "PARTS";

	public const string ESSENCE = "ESSENCE";

	public const string REALMONEY = "REAL_MONEY";

	public const string EVENT_CURRENCY = "EVENT_CURRENCY";

	public const string PF_FAZ_TOKEN = "FC";

	public const string PF_PARTS = "PA";

	public const string PF_ESSENCE = "RE";

	public const string PF_REALMONEY = "RM";

	public const string PF_EVENT_CURRENCY = "EC";

	public static string GetCurrencyString(Currency.CurrencyType type)
	{
		return GetPFCurrencyString(type);
	}

	private static string GetPFCurrencyString(Currency.CurrencyType type)
	{
		return type switch
		{
			Currency.CurrencyType.HardCurrency => "FC", 
			Currency.CurrencyType.Parts => "PA", 
			Currency.CurrencyType.Essence => "RE", 
			Currency.CurrencyType.RealMoney => "RM", 
			Currency.CurrencyType.EventCurrency => "EC", 
			_ => "", 
		};
	}

	public static Currency.CurrencyType GetTypeForString(string currencyString)
	{
		switch (currencyString)
		{
		case "FAZ_TOKENS":
			return Currency.CurrencyType.HardCurrency;
		case "FC":
			return Currency.CurrencyType.HardCurrency;
		case "PARTS":
			return Currency.CurrencyType.Parts;
		case "PA":
			return Currency.CurrencyType.Parts;
		case "ESSENCE":
			return Currency.CurrencyType.Essence;
		case "RE":
			return Currency.CurrencyType.Essence;
		case "REAL_MONEY":
			return Currency.CurrencyType.RealMoney;
		case "RM":
			return Currency.CurrencyType.RealMoney;
		case "EVENT_CURRENCY":
			return Currency.CurrencyType.EventCurrency;
		case "EC":
			return Currency.CurrencyType.EventCurrency;
		default:
			global::UnityEngine.Debug.LogError("Currency GetTypeForString - No currency corresponding to " + currencyString);
			return Currency.CurrencyType.HardCurrency;
		}
	}
}
