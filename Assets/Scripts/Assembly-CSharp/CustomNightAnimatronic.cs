public class CustomNightAnimatronic
{
	public enum CustomNightRooms
	{
		Stage = 1,
		DiningArea = 2,
		Kitchen = 3,
		Restrooms = 4,
		Backstage = 5,
		Closet = 6,
		WestHall = 7,
		EastHall = 8,
		WestCorner = 9,
		EastCorner = 10,
		Office = 11,
		PirateCove = 12,
		DiningAreaWest = 13,
		DiningAreaEast = 14,
		PirateCove2 = 15,
		PirateCove3 = 16
	}

	public string Id;

	public string Bundle;

	public string Prefab;

	public string PortraitImageName;

	public int InitialAIValue;

	public bool CanGoKitchen = true;

	public bool CanGoBackstage = true;

	public bool CanGoRestrooms = true;

	public bool CanGoCloset = true;

	public CustomNightAnimatronic.CustomNightRooms originRoom;

	public CustomNightAnimatronic.CustomNightRooms pushbackRoom;

	public bool EastPath;

	public int ThreeAMValueBoost;

	public static global::System.Collections.Generic.Dictionary<CustomNightAnimatronic.CustomNightRooms, global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms>> possibleRooms;

	public static global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> StunLockRooms;

	static CustomNightAnimatronic()
	{
		possibleRooms = new global::System.Collections.Generic.Dictionary<CustomNightAnimatronic.CustomNightRooms, global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms>>();
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.Stage, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.DiningArea });
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.DiningArea, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms>
		{
			CustomNightAnimatronic.CustomNightRooms.DiningAreaWest,
			CustomNightAnimatronic.CustomNightRooms.DiningAreaEast
		});
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.DiningAreaWest, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms>
		{
			CustomNightAnimatronic.CustomNightRooms.Backstage,
			CustomNightAnimatronic.CustomNightRooms.WestHall
		});
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.DiningAreaEast, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms>
		{
			CustomNightAnimatronic.CustomNightRooms.Restrooms,
			CustomNightAnimatronic.CustomNightRooms.Kitchen,
			CustomNightAnimatronic.CustomNightRooms.EastHall
		});
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.Backstage, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.DiningAreaWest });
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.Restrooms, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.DiningAreaEast });
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.Kitchen, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.DiningAreaEast });
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.WestHall, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms>
		{
			CustomNightAnimatronic.CustomNightRooms.Closet,
			CustomNightAnimatronic.CustomNightRooms.WestCorner
		});
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.EastHall, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.EastCorner });
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.Closet, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.WestHall });
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.WestCorner, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.Office });
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.EastCorner, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.Office });
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.PirateCove, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.PirateCove2 });
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.PirateCove2, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.PirateCove3 });
		possibleRooms.Add(CustomNightAnimatronic.CustomNightRooms.PirateCove3, new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms> { CustomNightAnimatronic.CustomNightRooms.WestHall });
		StunLockRooms = new global::System.Collections.Generic.List<CustomNightAnimatronic.CustomNightRooms>();
		StunLockRooms.Add(CustomNightAnimatronic.CustomNightRooms.EastCorner);
		StunLockRooms.Add(CustomNightAnimatronic.CustomNightRooms.PirateCove);
		StunLockRooms.Add(CustomNightAnimatronic.CustomNightRooms.PirateCove2);
		StunLockRooms.Add(CustomNightAnimatronic.CustomNightRooms.PirateCove3);
	}
}
