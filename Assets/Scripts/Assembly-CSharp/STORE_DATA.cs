public sealed class STORE_DATA
{
	public class Badge
	{
		public string BadgeArtRef { get; set; }

		public string BadgeLocRef { get; set; }
	}

	public class Contents
	{
		public STORE_DATA.Item Item1 { get; set; }

		public STORE_DATA.Item Item2 { get; set; }

		public STORE_DATA.Item Item3 { get; set; }

		public STORE_DATA.Item Item4 { get; set; }

		public STORE_DATA.Item Item5 { get; set; }

		public STORE_DATA.Item Item6 { get; set; }
	}

	public class Cost
	{
		public int HardCurrency { get; set; }

		public int SoftCurrency { get; set; }
	}

	public class Entry
	{
		public string PurchasableLogical { get; set; }

		public string PurchasableName { get; set; }

		public string PurchasableNameRef { get; set; }

		public string LiveState { get; set; }

		public string Description { get; set; }

		public string DescriptionRef { get; set; }

		public string StoreSection { get; set; }

		public int Order { get; set; }

		public string DialogArtRef { get; set; }

		public string ArtRef { get; set; }

		public string Repeatable { get; set; }

		public string Subscription { get; set; }

		public STORE_DATA.Contents Contents { get; set; }

		public STORE_DATA.Cost Cost { get; set; }

		public STORE_DATA.Badge Badge { get; set; }

		public string ButtonLocOverride { get; set; }
	}

	public class Item
	{
		public string Logical { get; set; }

		public string Type { get; set; }

		public int Qty { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<STORE_DATA.Entry> Entries { get; set; }
	}
}
