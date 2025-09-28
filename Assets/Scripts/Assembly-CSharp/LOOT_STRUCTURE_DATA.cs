public sealed class LOOT_STRUCTURE_DATA
{
	public class Eligibility
	{
		public string LimitId { get; set; }

		public string Operator { get; set; }

		public int NumberValue { get; set; }

		public int RangeMin { get; set; }

		public int RangeMax { get; set; }

		public string StringValue { get; set; }
	}

	public class Entry
	{
		public string Logical { get; set; }

		public global::System.Collections.Generic.List<LOOT_STRUCTURE_DATA.lootPackage> LootPackages { get; set; }
	}

	public class lootPackage
	{
		public LOOT_STRUCTURE_DATA.Eligibility Eligibility { get; set; }

		public string LootPackage { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<LOOT_STRUCTURE_DATA.Entry> Entries { get; set; }
	}
}
