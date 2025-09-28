public class StaticConfigs
{
	private static readonly StaticConfig NullConfig;

	private readonly global::System.Collections.Generic.Dictionary<string, StaticConfig> _staticConfigs;

	public StaticConfig GetStaticConfig(string profileId)
	{
		StaticConfig value = null;
		if (_staticConfigs.ContainsKey(profileId))
		{
			_staticConfigs.TryGetValue(profileId, out value);
			return value;
		}
		return NullConfig;
	}

	public StaticConfigs(MasterDataDomain masterDataDomain)
	{
		_staticConfigs = new global::System.Collections.Generic.Dictionary<string, StaticConfig>();
		masterDataDomain.GetAccessToData.GetStaticDataAsync(StaticDataReady);
	}

	private void StaticDataReady(STATIC_DATA.Root staticData)
	{
		if (staticData == null)
		{
			global::UnityEngine.Debug.LogError("Static data null??");
		}
		foreach (STATIC_DATA.Entry entry in staticData.Entries)
		{
			if (entry != null && !string.IsNullOrWhiteSpace(entry.Profile) && !_staticConfigs.ContainsKey(entry.Profile))
			{
				_staticConfigs.Add(entry.Profile, new StaticConfig(entry));
			}
		}
	}

	static StaticConfigs()
	{
		NullConfig = null;
	}
}
