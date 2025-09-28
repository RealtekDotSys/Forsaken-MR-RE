public class StoreSectionMapper
{
	public enum StoreSectionType
	{
		None = 0,
		Pack = 1,
		FazCoins = 2,
		Device = 3,
		EndoskeletonSlot = 4,
		MiniPack = 5,
		Lure = 6,
		Buff = 7,
		BuffItem = 8
	}

	public const string STORE_SECTION_NONE = "None";

	public const string STORE_SECTION_PACK = "Pack";

	public const string STORE_SECTION_FAZCOINS = "FazCoins";

	public const string STORE_SECTION_DEVICE = "Device";

	public const string STORE_SECTION_ENDOSKELETON = "EndoskeletonSlot";

	public const string STORE_SECTION_MINIPACK = "MiniPack";

	public const string STORE_SECTION_LURE = "Lure";

	public const string STORE_SECTION_BUFF = "Buff";

	public const string STORE_SECTION_BUFFITETM = "BuffItem";

	public static string GetStringForType(StoreSectionMapper.StoreSectionType type)
	{
		return type switch
		{
			StoreSectionMapper.StoreSectionType.None => "None", 
			StoreSectionMapper.StoreSectionType.Pack => "Pack", 
			StoreSectionMapper.StoreSectionType.FazCoins => "FazCoins", 
			StoreSectionMapper.StoreSectionType.Device => "Device", 
			StoreSectionMapper.StoreSectionType.EndoskeletonSlot => "EndoskeletonSlot", 
			StoreSectionMapper.StoreSectionType.MiniPack => "MiniPack", 
			StoreSectionMapper.StoreSectionType.Lure => "Lure", 
			StoreSectionMapper.StoreSectionType.Buff => "Buff", 
			StoreSectionMapper.StoreSectionType.BuffItem => "BuffItem", 
			_ => "None", 
		};
	}

	public static StoreSectionMapper.StoreSectionType GetTypeForString(string typeString)
	{
		return typeString switch
		{
			"None" => StoreSectionMapper.StoreSectionType.None, 
			"Pack" => StoreSectionMapper.StoreSectionType.Pack, 
			"FazCoins" => StoreSectionMapper.StoreSectionType.FazCoins, 
			"Device" => StoreSectionMapper.StoreSectionType.Device, 
			"EndoskeletonSlot" => StoreSectionMapper.StoreSectionType.EndoskeletonSlot, 
			"MiniPack" => StoreSectionMapper.StoreSectionType.MiniPack, 
			"Lure" => StoreSectionMapper.StoreSectionType.Lure, 
			"Buff" => StoreSectionMapper.StoreSectionType.Buff, 
			"BuffItem" => StoreSectionMapper.StoreSectionType.BuffItem, 
			_ => StoreSectionMapper.StoreSectionType.None, 
		};
	}
}
