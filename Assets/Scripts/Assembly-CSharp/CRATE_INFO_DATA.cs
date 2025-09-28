public sealed class CRATE_INFO_DATA
{
	public class ArtAssets
	{
		public string AssetBundle { get; set; }

		public string CratePrefabName { get; set; }

		public string FramePrefabName { get; set; }

		public string RigPrefabName { get; set; }

		public string SpriteAsset { get; set; }
	}

	public class Entry
	{
		public string Logical { get; set; }

		public CRATE_INFO_DATA.ArtAssets ArtAssets { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<CRATE_INFO_DATA.Entry> Entries { get; set; }
	}
}
