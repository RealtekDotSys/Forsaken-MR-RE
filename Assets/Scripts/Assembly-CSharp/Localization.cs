public class Localization
{
	public const string PlayerPrefsPreviousSystemLanguageKey = "LocalizationPreviousSystemLanguage";

	public const string PlayerPrefsCurrentLanguageKey = "LocalizationCurrentLanguage";

	private const string PlayerPrefsShowDebugKey = "LocalizationShowDebug";

	private Language _currentLanguage;

	private bool _initialized;

	private bool _showDebug;

	private global::System.Collections.Generic.Dictionary<string, LocDataEntry> _locDataQuickLookup;

	private global::System.Action<Localization> callbacks;

	protected bool IsReady => _initialized;

	protected Localization GetPublicInterface => this;

	public bool ShowDebug
	{
		get
		{
			return global::UnityEngine.PlayerPrefs.GetInt("LocalizationShowDebug") != 0;
		}
		set
		{
			int value2 = (value ? 1 : 0);
			global::UnityEngine.PlayerPrefs.SetInt("LocalizationShowDebug", value2);
			_showDebug = value;
		}
	}

	private Language PreviousSystemLanguage
	{
		get
		{
			Language result = Language.English;
			global::System.Enum.TryParse<Language>(global::UnityEngine.PlayerPrefs.GetString("LocalizationPreviousSystemLanguage", "None"), out result);
			return result;
		}
		set
		{
			global::UnityEngine.PlayerPrefs.SetString("LocalizationPreviousSystemLanguage", value.ToString());
		}
	}

	public Language CurrentLanguage
	{
		get
		{
			Language result = Language.English;
			global::System.Enum.TryParse<Language>(global::UnityEngine.PlayerPrefs.GetString("LocalizationCurrentLanguage", "English"), out result);
			return result;
		}
		set
		{
			global::UnityEngine.PlayerPrefs.SetString("LocalizationCurrentLanguage", value.ToString());
			Language result = Language.English;
			global::System.Enum.TryParse<Language>(global::UnityEngine.PlayerPrefs.GetString("LocalizationCurrentLanguage", "English"), out result);
			_currentLanguage = result;
		}
	}

	public Localization()
	{
		_locDataQuickLookup = new global::System.Collections.Generic.Dictionary<string, LocDataEntry>();
		_showDebug = ShowDebug;
		InitCurrentLanguage();
	}

	public void Setup(global::System.Collections.Generic.Dictionary<string, LocDataEntry> locData)
	{
		_locDataQuickLookup = locData;
		_initialized = true;
		TryToDispatchPublicInterface();
	}

	public void Teardown()
	{
		_initialized = false;
		_locDataQuickLookup = null;
	}

	public string GetLocalizedString(string localizedStringId, string originalString)
	{
		if (!_locDataQuickLookup.ContainsKey(localizedStringId))
		{
			if (!_showDebug)
			{
				return originalString;
			}
			return "#>!!" + originalString + "!!<#";
		}
		LocDataEntry locDataEntry = _locDataQuickLookup[localizedStringId];
		string text = _currentLanguage switch
		{
			Language.English => locDataEntry.English, 
			Language.French => locDataEntry.French, 
			Language.Italian => locDataEntry.Italian, 
			Language.German => locDataEntry.German, 
			Language.Spanish_Spain => locDataEntry.SpanishSpain, 
			Language.Portuguese_Brazil => locDataEntry.PortugueseBrazil, 
			Language.Russian => locDataEntry.Russian, 
			_ => null, 
		};
		if (_showDebug)
		{
			bool flag = string.IsNullOrEmpty(text);
			string result = (flag ? "#>!!" : "#>") + (flag ? originalString : text) + (flag ? "!!<#" : "<#");
			if (!flag)
			{
				return result;
			}
			return originalString;
		}
		if (!string.IsNullOrEmpty(text))
		{
			return text;
		}
		return originalString;
	}

	public string GetLocalizedString(string localizedStringId, string originalString, global::System.Collections.Generic.Dictionary<string, string> replacementTokens)
	{
		string text = localizedStringId;
		string originalString2 = originalString;
		foreach (string key in replacementTokens.Keys)
		{
			if (key == null)
			{
				return GetLocalizedString(localizedStringId, originalString);
			}
			text = GetLocalizedString(text, originalString2).Replace("<fnaf " + key + " fnaf>", GetLocalizedString(replacementTokens[key], replacementTokens[key]));
			originalString2 = text;
		}
		return text;
	}

	private void InitCurrentLanguage()
	{
		Language currentSystemLanguage = GetCurrentSystemLanguage();
		if (PreviousSystemLanguage != currentSystemLanguage)
		{
			CurrentLanguage = currentSystemLanguage;
			PreviousSystemLanguage = currentSystemLanguage;
		}
		_currentLanguage = currentSystemLanguage;
	}

	private Language GetCurrentSystemLanguage()
	{
		return global::UnityEngine.Application.systemLanguage switch
		{
			global::UnityEngine.SystemLanguage.English => Language.English, 
			global::UnityEngine.SystemLanguage.French => Language.French, 
			global::UnityEngine.SystemLanguage.Italian => Language.Italian, 
			global::UnityEngine.SystemLanguage.German => Language.German, 
			global::UnityEngine.SystemLanguage.Spanish => Language.Spanish_Spain, 
			global::UnityEngine.SystemLanguage.Portuguese => Language.Portuguese_Brazil, 
			global::UnityEngine.SystemLanguage.Russian => Language.Russian, 
			_ => Language.English, 
		};
	}

	public void GetInterfaceAsync(global::System.Action<Localization> callback)
	{
		if (IsReady)
		{
			callback(this);
		}
		else
		{
			callbacks = (global::System.Action<Localization>)global::System.Delegate.Combine(callbacks, callback);
		}
	}

	public void TryToDispatchPublicInterface()
	{
		if (callbacks != null)
		{
			callbacks(this);
		}
	}
}
