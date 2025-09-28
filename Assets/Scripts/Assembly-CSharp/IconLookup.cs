public class IconLookup
{
	private static readonly string ClassName;

	private const string DefaultIconResourcesPath = "Icons/alpine_ui_icon_default";

	private EventExposer _masterEventExposer;

	private MasterDataDomain _masterDataDomain;

	private AssetCache _assetCache;

	private global::UnityEngine.Sprite _defaultIcon;

	private global::System.Collections.Generic.Dictionary<IconGroup, string> _bundleNames;

	private global::System.Collections.Generic.Dictionary<IconGroup, IconGroupData> _iconLookup;

	private string _storeSkinOverride;

	private global::System.Action<IconLookup> lookupCallbacks;

	private bool Ready;

	public global::UnityEngine.Sprite DefaultIcon => _defaultIcon;

	protected bool IsReady => _iconLookup == null;

	protected IconLookup GetPublicInterface => this;

	public void GetIcon(IconGroup group, string iconName, global::System.Action<global::UnityEngine.Sprite> iconCallback)
	{
		if (group != IconGroup.AvatarAtlases && ValidateSpriteInfo(group, iconName))
		{
			_iconLookup[group]?.GetIcon(iconName, iconCallback, delegate
			{
				iconCallback(DefaultIcon);
			});
		}
		else
		{
			iconCallback?.Invoke(_defaultIcon);
		}
	}

	public void GetIcon(IconGroup group, string iconName, global::System.Action<global::UnityEngine.Sprite> iconCallback, global::System.Action failedCallback)
	{
		if (group != IconGroup.AvatarAtlases && ValidateSpriteInfo(group, iconName))
		{
			_iconLookup[group]?.GetIcon(iconName, iconCallback, failedCallback);
		}
		else
		{
			iconCallback?.Invoke(_defaultIcon);
		}
	}

	public void GetIconFromAtlas(IconGroup group, string iconName, string atlasName, global::System.Action<global::UnityEngine.Sprite> iconCallback, global::System.Action failureCallback)
	{
		if (group == IconGroup.AvatarAtlases && ValidateSpriteInfo(IconGroup.AvatarAtlases, iconName))
		{
			_iconLookup[IconGroup.AvatarAtlases]?.GetIconFromAtlas(iconName, atlasName, iconCallback, failureCallback);
		}
		else
		{
			iconCallback?.Invoke(_defaultIcon);
		}
	}

	public void SetIconWithDefaultFirst(IconGroup group, string name, global::UnityEngine.UI.Image image)
	{
		image.sprite = _defaultIcon;
		if (group == IconGroup.AvatarAtlases || !ValidateSpriteInfo(group, name))
		{
			return;
		}
		_iconLookup[group]?.GetIcon(name, delegate(global::UnityEngine.Sprite sprite)
		{
			if (image != null)
			{
				image.sprite = sprite;
			}
			else
			{
				global::UnityEngine.Debug.LogError("SetIconWithDefaultFirst - Callback for image that doesn't exist with sprite: Callback for image that doesn't exist with sprite: " + sprite.name);
			}
		}, null);
	}

	public void ReleaseAllCachedAssets()
	{
		foreach (IconGroupData value in _iconLookup.Values)
		{
			value.ReleaseAssets();
		}
	}

	public bool IsAtlasIconGroup(IconGroup iconGroup)
	{
		return iconGroup == IconGroup.AvatarAtlases;
	}

	public IconLookup(EventExposer masterEventExposer, MasterDataDomain masterDataDomain, AssetCache assetCacheAccess)
	{
		_masterEventExposer = masterEventExposer;
		_masterDataDomain = masterDataDomain;
		CacheDefaultIcon();
		SeasonalDataLoaded();
		assetCacheAccess.GetInterfaceAsync(AssetCacheReady);
	}

	public void Teardown()
	{
		if (_iconLookup != null)
		{
			foreach (IconGroupData value in _iconLookup.Values)
			{
				value.Teardown();
			}
			_iconLookup.Clear();
			_iconLookup = null;
		}
		_bundleNames.Clear();
		_masterEventExposer = null;
		_defaultIcon = null;
		_bundleNames = null;
		_assetCache = null;
	}

	private void StreamingAssetsGameplayAssetsReady(int failedAssetCount)
	{
		global::System.Collections.Generic.Dictionary<IconGroup, IconGroupData> dictionary = new global::System.Collections.Generic.Dictionary<IconGroup, IconGroupData>();
		dictionary.Add(IconGroup.OneOffScripted, new IconGroupData());
		dictionary.Add(IconGroup.Portrait, new IconGroupData());
		dictionary.Add(IconGroup.ShopUI, new IconGroupData());
		dictionary.Add(IconGroup.Reward, new IconGroupData());
		dictionary.Add(IconGroup.LocalBuffRewards, new IconGroupData());
		dictionary.Add(IconGroup.LocalLoadout, new IconGroupData());
		_iconLookup = dictionary;
		TryToStartLoadingIcons();
	}

	private void StreamingAssetsGameplayEnded()
	{
		if (_iconLookup == null)
		{
			return;
		}
		foreach (IconGroupData value in _iconLookup.Values)
		{
			value.Teardown();
		}
		_iconLookup.Clear();
		_iconLookup = null;
	}

	private void AssetBundleDownloadSegmentsAllComplete(int failedAssetCount)
	{
		global::System.Collections.Generic.Dictionary<IconGroup, IconGroupData> dictionary = new global::System.Collections.Generic.Dictionary<IconGroup, IconGroupData>();
		dictionary.Add(IconGroup.Cpu, new IconGroupData());
		dictionary.Add(IconGroup.Mod, new IconGroupData());
		dictionary.Add(IconGroup.PlushSuit, new IconGroupData());
		dictionary.Add(IconGroup.Portrait, new IconGroupData());
		dictionary.Add(IconGroup.Reward, new IconGroupData());
		dictionary.Add(IconGroup.Store, new IconGroupData());
		dictionary.Add(IconGroup.OneOffScripted, new IconGroupData());
		dictionary.Add(IconGroup.ShopUI, new IconGroupData());
		dictionary.Add(IconGroup.DailyChallenges, new IconGroupData());
		dictionary.Add(IconGroup.PhotoboothFrames, new IconGroupData());
		dictionary.Add(IconGroup.Buffs, new IconGroupData());
		dictionary.Add(IconGroup.Loadout, new IconGroupData());
		dictionary.Add(IconGroup.LocalBuffRewards, new IconGroupData());
		dictionary.Add(IconGroup.LocalLoadout, new IconGroupData());
		dictionary.Add(IconGroup.AvatarAtlases, new IconGroupData());
		dictionary.Add(IconGroup.Trophy, new IconGroupData());
		dictionary.Add(IconGroup.Encounter, new IconGroupData());
		_iconLookup = dictionary;
		TryToStartLoadingIcons();
	}

	private void SeasonalDataLoaded()
	{
		_masterDataDomain.GetAccessToData.GetConfigDataEntryAsync(ConfigDataReady);
	}

	private void ConfigDataReady(CONFIG_DATA.Root data)
	{
		global::System.Collections.Generic.Dictionary<IconGroup, string> dictionary = new global::System.Collections.Generic.Dictionary<IconGroup, string>();
		dictionary[IconGroup.Cpu] = data.Entries[0].ArtAssets.UI.CpuIconsBundle;
		dictionary[IconGroup.Mod] = data.Entries[0].ArtAssets.UI.ModIconsBundle;
		dictionary[IconGroup.PlushSuit] = data.Entries[0].ArtAssets.UI.PlushSuitIconsBundle;
		dictionary[IconGroup.Portrait] = data.Entries[0].ArtAssets.UI.PortraitIconsBundle;
		dictionary[IconGroup.Reward] = data.Entries[0].ArtAssets.UI.RewardIconsBundle;
		dictionary[IconGroup.Store] = data.Entries[0].ArtAssets.UI.StoreIconsBundle;
		dictionary[IconGroup.OneOffScripted] = data.Entries[0].ArtAssets.UI.OneOffScriptedIconsBundle;
		dictionary[IconGroup.ShopUI] = data.Entries[0].ArtAssets.UI.ShopUIIconsBundle;
		dictionary[IconGroup.DailyChallenges] = data.Entries[0].ArtAssets.UI.DailyChallengesIconBundle;
		dictionary[IconGroup.AvatarAtlases] = data.Entries[0].ArtAssets.UI.ProfileAvatarIconBundle;
		dictionary[IconGroup.PhotoboothFrames] = data.Entries[0].ArtAssets.UI.PhotoboothFrameIconBundle;
		dictionary[IconGroup.Buffs] = data.Entries[0].ArtAssets.UI.BuffIconsBundle;
		dictionary[IconGroup.Loadout] = data.Entries[0].ArtAssets.UI.LoadoutIconsBundle;
		dictionary[IconGroup.LocalBuffRewards] = data.Entries[0].ArtAssets.UI.LocalBuffRewardIconsBundle;
		dictionary[IconGroup.LocalLoadout] = data.Entries[0].ArtAssets.UI.LocalLoadoutIconsBundle;
		dictionary[IconGroup.Trophy] = data.Entries[0].ArtAssets.UI.TrophyIconsBundle;
		dictionary[IconGroup.Encounter] = data.Entries[0].ArtAssets.UI.EncounterIconsBundle;
		if (!string.IsNullOrEmpty(_storeSkinOverride))
		{
			dictionary[IconGroup.ShopUI] = _storeSkinOverride;
		}
		_bundleNames = dictionary;
		TryToStartLoadingIcons();
	}

	private void AssetCacheReady(AssetCache assetCache)
	{
		_assetCache = assetCache;
		AssetBundleDownloadSegmentsAllComplete(0);
	}

	private void TryToStartLoadingIcons()
	{
		if (_bundleNames == null || _assetCache == null || _iconLookup == null)
		{
			return;
		}
		foreach (IconGroup key in _iconLookup.Keys)
		{
			IconGroupData iconGroupData = _iconLookup[key];
			iconGroupData.SetAssetCache(_assetCache);
			string bundleName = ((!_bundleNames.ContainsKey(key)) ? string.Empty : _bundleNames[key]);
			iconGroupData.BundleName = bundleName;
		}
		TryToDispatchPublicInterface();
	}

	private bool ValidateSpriteInfo(IconGroup group, string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			return false;
		}
		if (_iconLookup == null)
		{
			return false;
		}
		return _iconLookup.ContainsKey(group);
	}

	private void CacheDefaultIcon()
	{
		_defaultIcon = global::UnityEngine.Resources.Load("Icons/alpine_ui_icon_default") as global::UnityEngine.Sprite;
		if (_defaultIcon == null)
		{
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(1, 1);
			texture2D.SetPixel(0, 0, global::UnityEngine.Color.clear);
			texture2D.Apply();
			_defaultIcon = global::UnityEngine.Sprite.Create(texture2D, new global::UnityEngine.Rect(0f, 0f, 0f, 0f), new global::UnityEngine.Vector2(0.5f, 0.5f));
		}
	}

	private void TryToDispatchPublicInterface()
	{
		lookupCallbacks?.Invoke(this);
		lookupCallbacks = null;
		Ready = true;
	}

	public void GetInterfaceAsync(global::System.Action<IconLookup> callback)
	{
		if (Ready)
		{
			callback?.Invoke(this);
		}
		else
		{
			lookupCallbacks = (global::System.Action<IconLookup>)global::System.Delegate.Combine(lookupCallbacks, callback);
		}
	}
}
