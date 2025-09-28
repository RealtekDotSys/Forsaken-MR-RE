public sealed class SCAVENGING_DATA
{
	public class Entry
	{
		public string Logical { get; set; }

		public SCAVENGING_DATA.Environment Environment { get; set; }
	}

	public class Environment
	{
		public string Bundle { get; set; }

		public string Asset { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<SCAVENGING_DATA.Entry> Entries { get; set; }
	}
}
