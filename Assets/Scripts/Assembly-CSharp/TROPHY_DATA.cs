public sealed class TROPHY_DATA
{
	public class Entry
	{
		public string Logical { get; set; }

		public string Rarity { get; set; }

		public string TrophyName { get; set; }

		public string TrophyDescription { get; set; }

		public string IconRef { get; set; }

		public string Bundle { get; set; }

		public string Asset { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<TROPHY_DATA.Entry> Entries { get; set; }
	}
}
