public class TrophyData
{
	public enum Rarity
	{
		Auto = 0,
		Common = 1,
		Uncommon = 2,
		Rare = 3
	}

	public readonly string Logical;

	public readonly string IconRef;

	public readonly TrophyData.Rarity TrophyRarity;

	public readonly string TrophyName;

	public readonly string TrophyDescription;

	public readonly string Bundle;

	public readonly string Asset;

	public TrophyData(TROPHY_DATA.Entry data)
	{
		Logical = data.Logical;
		IconRef = data.IconRef;
		TrophyRarity = (TrophyData.Rarity)global::System.Enum.Parse(typeof(TrophyData.Rarity), data.Rarity);
		TrophyName = data.TrophyName;
		TrophyDescription = data.TrophyDescription;
		Bundle = data.Bundle;
		Asset = data.Asset;
	}
}
