public sealed class STORESECTIONS_DATA
{
	public class Entry
	{
		public string Logical { get; set; }

		public string StoreSectionName { get; set; }

		public string StoreSectionNameRef { get; set; }

		public string DisplayType { get; set; }

		public int Order { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<STORESECTIONS_DATA.Entry> Entries { get; set; }
	}
}
