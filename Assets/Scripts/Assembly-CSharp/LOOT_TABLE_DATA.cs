public sealed class LOOT_TABLE_DATA
{
	public class Entry
	{
		public string Logical { get; set; }

		public global::System.Collections.Generic.List<LOOT_TABLE_DATA.Item> Items { get; set; }
	}

	public class Item
	{
		public string ItemName { get; set; }

		public float Min { get; set; }

		public float Max { get; set; }

		public int Weight { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<LOOT_TABLE_DATA.Entry> Entries { get; set; }
	}
}
