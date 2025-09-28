public class HaywireIndicator
{
	public enum IndicatorVisibility
	{
		Never = -1,
		Always = 0,
		AlwaysHaywire = 1,
		AnimatronicOnScreen = 2
	}

	public enum HaywireIndicateType
	{
		LookAway = 0,
		LookAt = 1
	}

	private HaywireIndicator.IndicatorVisibility visibility;

	private HaywireIndicator.HaywireIndicateType type;

	private AttackAnimatronicExternalSystems systems;

	private global::UnityEngine.Sprite currentIndicatorImage;

	private bool Available;

	private bool InHaywire;

	private bool HaywireOnScreen;

	private global::System.Collections.Generic.Dictionary<HaywireIndicator.HaywireIndicateType, global::UnityEngine.Sprite> indicatorSprites;

	public void Setup(AttackAnimatronicExternalSystems _systems)
	{
		systems = _systems;
		MasterDomain.GetDomain().MasterDataDomain.GetAccessToData.GetConfigDataEntryAsync(ConfigDataReady);
	}

	private void ConfigDataReady(CONFIG_DATA.Root data)
	{
		if (indicatorSprites != null)
		{
			indicatorSprites.Clear();
			indicatorSprites = null;
		}
		indicatorSprites = new global::System.Collections.Generic.Dictionary<HaywireIndicator.HaywireIndicateType, global::UnityEngine.Sprite>();
		IconLookup iconLookupAccess = MasterDomain.GetDomain().GameAssetManagementDomain.IconLookupAccess;
		iconLookupAccess.GetIcon(IconGroup.Encounter, data.Entries[0].ArtAssets.UI.LookAwayIconName, LookAwayIconReady);
		iconLookupAccess.GetIcon(IconGroup.Encounter, data.Entries[0].ArtAssets.UI.LookAtIconName, LookAtIconReady);
	}

	private void LookAwayIconReady(global::UnityEngine.Sprite sprite)
	{
		indicatorSprites.Add(HaywireIndicator.HaywireIndicateType.LookAway, sprite);
	}

	private void LookAtIconReady(global::UnityEngine.Sprite sprite)
	{
		indicatorSprites.Add(HaywireIndicator.HaywireIndicateType.LookAt, sprite);
	}

	public bool ShouldShow()
	{
		if (!Available)
		{
			return false;
		}
		switch (visibility)
		{
		case HaywireIndicator.IndicatorVisibility.Always:
			return true;
		case HaywireIndicator.IndicatorVisibility.AlwaysHaywire:
			return InHaywire;
		case HaywireIndicator.IndicatorVisibility.AnimatronicOnScreen:
			if (InHaywire)
			{
				return HaywireOnScreen;
			}
			return false;
		default:
			return false;
		}
	}

	public global::UnityEngine.Sprite CurrentSprite()
	{
		return currentIndicatorImage;
	}

	public void SetVisibilityType(HaywireIndicator.IndicatorVisibility type)
	{
		visibility = type;
		Available = false;
		if (global::UnityEngine.PlayerPrefs.HasKey("HaywireIndicators") && global::UnityEngine.PlayerPrefs.GetInt("HaywireIndicators") == 1)
		{
			Available = true;
		}
	}

	public void Update()
	{
		if (Available && indicatorSprites != null && indicatorSprites.ContainsKey(type))
		{
			currentIndicatorImage = indicatorSprites[type];
		}
	}

	public void SetHaywireInformation(bool onScreen, bool inHaywire, HaywireIndicator.HaywireIndicateType haywireType)
	{
		InHaywire = inHaywire;
		HaywireOnScreen = onScreen;
		type = haywireType;
	}

	public void Reset()
	{
		Available = false;
		InHaywire = false;
		HaywireOnScreen = false;
		currentIndicatorImage = null;
		visibility = HaywireIndicator.IndicatorVisibility.Never;
	}

	public void TearDown()
	{
		Reset();
		systems = null;
	}
}
