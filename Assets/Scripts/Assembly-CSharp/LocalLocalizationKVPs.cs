public class LocalLocalizationKVPs : global::UnityEngine.MonoBehaviour
{
	[global::System.Serializable]
	public class LanguageToStringKVP
	{
		public Language language;

		public string localizedString;
	}

	[global::System.Serializable]
	public class LocalLocalizationKVP
	{
		public string Key;

		public global::System.Collections.Generic.List<LocalLocalizationKVPs.LanguageToStringKVP> localization;

		public string this[Language key] => localization.Find((LocalLocalizationKVPs.LanguageToStringKVP x) => x.language == key).localizedString;

		public bool ContainsKey(Language searchLanguage)
		{
			return localization.Find((LocalLocalizationKVPs.LanguageToStringKVP x) => x.language == searchLanguage) != null;
		}
	}

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<LocalLocalizationKVPs.LocalLocalizationKVP> locKVPs;

	public LocalLocalizationKVPs.LocalLocalizationKVP this[string key] => locKVPs.Find((LocalLocalizationKVPs.LocalLocalizationKVP x) => x.Key == key);

	public bool ContainsKey(string searchKey)
	{
		return locKVPs.Find((LocalLocalizationKVPs.LocalLocalizationKVP x) => x.Key == searchKey) != null;
	}
}
