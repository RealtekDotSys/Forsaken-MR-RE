public class VirtualGoodsListResponseHandler : EventResponseHandler
{
	private const string BUNDLE_PRODUCT = "Product";

	private const string SERVER_STORE_DATA = "StoreItems";

	private const string SERVER_STORE_ITEMS = "Store";

	private const string STORE_ID_CATALOG = "CatalogVersion";

	private const string STORE_ID_STORE = "StoreId";

	private const string STORE_ID_MARKETING = "MarketingData";

	private const string STORE_ID_METADATA = "Metadata";

	private const string STORE_ID_VERSION = "version";

	private const string STOREDATA_ITEMID = "ItemId";

	private const string STOREDATA_CUSTOMDATA = "CustomData";

	private const string STOREDATA_VC_PRICES = "VirtualCurrencyPrices";

	public global::System.Action<global::System.Collections.Generic.List<StoreContainer.StorefrontData>> VirtualGoodsReceived;

	public void Setup(global::System.Action<global::System.Collections.Generic.List<StoreContainer.StorefrontData>> callback)
	{
		VirtualGoodsReceived = callback;
	}

	public void TryHandleResponse(ServerData data)
	{
		if (data.GetServerData("StoreItems") != null)
		{
			ProcessStoreData(data.GetServerData("StoreItems"));
		}
	}

	public void ProcessStoreData(ServerData data)
	{
		global::System.Collections.Generic.List<StoreContainer.StorefrontData> list = new global::System.Collections.Generic.List<StoreContainer.StorefrontData>();
		foreach (ServerData serverData in data.GetServerDataList("Stores"))
		{
			list.Add(GetStorefrontData(serverData));
		}
		if (VirtualGoodsReceived != null)
		{
			VirtualGoodsReceived(list);
		}
	}

	private StoreContainer.StorefrontData GetStorefrontData(ServerData storeData)
	{
		StoreContainer.StorefrontData storefrontData = new StoreContainer.StorefrontData();
		storefrontData.Name = storeData.GetString("Name");
		storefrontData.Type = storeData.GetString("Type");
		storefrontData.Version = storeData.GetInt("Version").Value;
		storefrontData.EndTime = (storeData.GetDouble("EndTime").HasValue ? storeData.GetDouble("EndTime").Value : ((double)(ServerTime.GetCurrentTime() + 999999)));
		storefrontData.BadgeArtRef = storeData.GetString("BadgeArtRef");
		storefrontData.Goods = GetStoreItemList(storeData.GetServerDataList("Items"), storefrontData);
		return storefrontData;
	}

	public global::System.Collections.Generic.List<StoreContainer.VirtualGoodData> GetStoreItemList(global::System.Collections.Generic.List<ServerData> entries, StoreContainer.StorefrontData storefront)
	{
		global::System.Collections.Generic.List<StoreContainer.VirtualGoodData> list = new global::System.Collections.Generic.List<StoreContainer.VirtualGoodData>();
		if (entries.Count < 1)
		{
			return list;
		}
		foreach (ServerData entry in entries)
		{
			StoreContainer.VirtualGoodData virtualGoodData = new StoreContainer.VirtualGoodData();
			virtualGoodData.ShortCode = entry.GetString("ItemId");
			virtualGoodData.BundleProduct = entry.GetString("ItemId");
			virtualGoodData.storefrontData = storefront;
			virtualGoodData.Currency = global::System.Linq.Enumerable.First(entry.GetServerData("VirtualCurrencyPrices").Keys);
			virtualGoodData.Cost = entry.GetServerData("VirtualCurrencyPrices").GetInt(virtualGoodData.Currency).Value;
			list.Add(virtualGoodData);
			global::UnityEngine.Debug.Log(virtualGoodData.ShortCode);
			global::UnityEngine.Debug.Log(virtualGoodData.storefrontData.Name);
			global::UnityEngine.Debug.Log(virtualGoodData.Currency);
			global::UnityEngine.Debug.Log(virtualGoodData.Cost);
		}
		return list;
	}

	private string GetCustomDataString(ServerData data, string key, string fallback)
	{
		if (data != null)
		{
			if (!string.IsNullOrEmpty(data.GetString(key)))
			{
				return data.GetString(key);
			}
			return fallback;
		}
		return fallback;
	}
}
