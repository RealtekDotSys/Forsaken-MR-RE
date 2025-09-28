public class CrateOpener
{
	private CrateEligibilityValidator _eligibilityValidator;

	private LootContainer _lootContainer;

	private PseudoRandom _prng;

	public CrateOpener(LootContainer lootContainer, PseudoRandom rand)
	{
		_prng = rand;
		_eligibilityValidator = new CrateEligibilityValidator();
		_lootContainer = lootContainer;
	}

	private int GetRandomNumberBetweenRange(int min, int max)
	{
		int num = _prng.Next();
		return (int)(float)(num - num / (max - min) * (max - min)) + min;
	}

	public global::System.Collections.Generic.List<LootRewardEntry> OpenCrate(LootStructureData structureData, EligibilityContext context)
	{
		if (structureData == null)
		{
			return new global::System.Collections.Generic.List<LootRewardEntry>();
		}
		LootPackageData lootPackageData = SelectPackageFromCrate(structureData, context);
		if (lootPackageData == null)
		{
			return new global::System.Collections.Generic.List<LootRewardEntry>();
		}
		return RollItemsForLootPackage(lootPackageData);
	}

	public LootPackageData SelectPackageFromCrate(LootStructureData crate, EligibilityContext context)
	{
		if (crate.LootPackages == null)
		{
			return null;
		}
		LootStructurePackage[] lootPackages = crate.LootPackages;
		foreach (LootStructurePackage lootStructurePackage in lootPackages)
		{
			if (lootStructurePackage != null && _eligibilityValidator != null && _eligibilityValidator.IsValidForEligibilityData(context, lootStructurePackage.Eligibility) && _lootContainer != null)
			{
				return _lootContainer.GetLootPackageForId(lootStructurePackage.LootPackage);
			}
		}
		return null;
	}

	private global::System.Collections.Generic.List<LootRewardEntry> RollItemsForLootPackage(LootPackageData packageData)
	{
		global::System.Collections.Generic.List<LootRewardEntry> result = new global::System.Collections.Generic.List<LootRewardEntry>();
		LootPackageTable[] tables = packageData.Tables;
		foreach (LootPackageTable lootPackageTable in tables)
		{
			int num = 0;
			do
			{
				global::UnityEngine.Debug.LogError("YOU NEVER FINISHED CRATE OPENER");
				num++;
			}
			while (num < lootPackageTable.Rolls);
		}
		return result;
	}
}
