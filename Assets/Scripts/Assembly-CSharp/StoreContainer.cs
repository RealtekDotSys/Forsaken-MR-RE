public class StoreContainer
{
	public class VirtualGoodData
	{
		public string ShortCode;

		public string Currency;

		public int Cost;

		public string BundleProduct;

		public StoreContainer.StorefrontData storefrontData;
	}

	public class StorefrontData
	{
		public const string STORETYPE_DEFAULT = "Default";

		public const string STORETYPE_EVENT = "Event";

		public const string STORETYPE_SALE = "Sale";

		public const string STORETYPE_TAPJOY = "TapJoy";

		public global::System.Collections.Generic.List<StoreContainer.VirtualGoodData> Goods;

		public string Name;

		public string Type;

		public double EndTime;

		public int Version;

		public string BadgeArtRef;
	}

	[global::System.Serializable]
	private sealed class _003C_003Ec
	{
	}

	private const string FRAME_PREFIX = "Frame_";

	private const string ENDOSKELETON_SLOT = "EndoskeletonSlot";

	private const string AVATARICON_KEY = "AvatarIcon";

	private global::System.Collections.Generic.List<StoreContainer.StorefrontData> _storefronts;

	private global::System.Collections.Generic.Dictionary<string, StoreDisplayData> _displayItems;

	private global::System.Collections.Generic.Dictionary<string, StoreDisplayData> _tapJoyItems;

	private global::System.Collections.Generic.Dictionary<string, StoreData> _rawStoreData;

	private global::System.Collections.Generic.List<StoreDisplayData> _additionalCarouselItems;

	private global::System.Collections.Generic.Dictionary<string, int> _ownedItems;

	private global::System.Collections.Generic.List<StoreSectionData> _sectionData;

	private long _deferredPaymentTimestamp;

	private bool _numOwnedChanged;

	private ItemDefinitions _itemDefinitions;

	public global::System.Action VirtualGoodsIntegrated;

	public global::System.Action AppStoreProductsIntegrated;

	public global::System.Action OwnedGoodsReceived;

	public global::System.Action StoreLoadError;

	public global::System.Collections.Generic.Dictionary<string, StoreDisplayData> DisplayItems => _displayItems;

	public global::System.Collections.Generic.Dictionary<string, StoreDisplayData> TapJoyItems => _tapJoyItems;

	public global::System.Collections.Generic.Dictionary<string, StoreData> RawStoreData => _rawStoreData;

	public global::System.Collections.Generic.List<StoreDisplayData> AdditionalCarouselItems => _additionalCarouselItems;

	public global::System.Collections.Generic.List<StoreSectionData> SectionData => _sectionData;

	public StoreContainer()
	{
		_rawStoreData = new global::System.Collections.Generic.Dictionary<string, StoreData>();
		_additionalCarouselItems = new global::System.Collections.Generic.List<StoreDisplayData>();
		_storefronts = new global::System.Collections.Generic.List<StoreContainer.StorefrontData>();
		_displayItems = new global::System.Collections.Generic.Dictionary<string, StoreDisplayData>();
		_tapJoyItems = new global::System.Collections.Generic.Dictionary<string, StoreDisplayData>();
		_ownedItems = new global::System.Collections.Generic.Dictionary<string, int>();
		_sectionData = new global::System.Collections.Generic.List<StoreSectionData>();
		_deferredPaymentTimestamp = 0L;
	}

	public void SetItemDefinitions(ItemDefinitions itemDefinitions)
	{
		_itemDefinitions = itemDefinitions;
	}

	public void LoadStoreSectionDataFromServer(STORESECTIONS_DATA.Root data)
	{
		_sectionData.Clear();
		foreach (STORESECTIONS_DATA.Entry entry in data.Entries)
		{
			_sectionData.Add(new StoreSectionData(entry));
		}
		global::System.Linq.Enumerable.OrderBy(_sectionData, (StoreSectionData x) => x.Order);
	}

	public void LoadStoreDataFromServer(STORE_DATA.Root data)
	{
		foreach (STORE_DATA.Entry entry in data.Entries)
		{
			StoreData storeData = new StoreData(entry);
			if (storeData.Live)
			{
				_rawStoreData.Add(storeData.Id, storeData);
			}
		}
	}

	public StoreDisplayData GetTapJoyStoreItem(string productId)
	{
		if (_tapJoyItems.ContainsKey(productId))
		{
			return _tapJoyItems[productId];
		}
		return null;
	}

	public global::System.Collections.Generic.List<string> GetAvatarIcons()
	{
		global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
		foreach (string key in _ownedItems.Keys)
		{
			if (key.Contains("AvatarIcon"))
			{
				list.Add(key);
			}
		}
		return list;
	}

	private bool SameStoreId(global::System.Collections.Generic.List<StoreContainer.StorefrontData> newData)
	{
		if (newData.Count != _storefronts.Count)
		{
			return false;
		}
		int num = 0;
		do
		{
			if (num >= newData.Count)
			{
				return false;
			}
			if (newData[num].Name != _storefronts[num].Name)
			{
				return false;
			}
			if (newData[num].Version != _storefronts[num].Version)
			{
				return false;
			}
			num++;
		}
		while (num != _storefronts.Count + 1);
		return false;
	}

	public void UpdateVirtualGoodsCatalogue(global::System.Collections.Generic.List<StoreContainer.StorefrontData> storefronts)
	{
		global::UnityEngine.Debug.Log("retrieved storefronts");
		if (GetNumItemsInStorefronts(storefronts) == 0)
		{
			if (StoreLoadError != null)
			{
				global::UnityEngine.Debug.Log("store error");
				StoreLoadError();
			}
			return;
		}
		if (SameStoreId(storefronts))
		{
			global::UnityEngine.Debug.Log("same store id");
			if (!_numOwnedChanged)
			{
				return;
			}
		}
		_numOwnedChanged = false;
		_storefronts = storefronts;
		_displayItems.Clear();
		_tapJoyItems.Clear();
		foreach (StoreContainer.StorefrontData storefront in _storefronts)
		{
			ProcessStorefrontData(storefront);
		}
		if (VirtualGoodsIntegrated != null)
		{
			VirtualGoodsIntegrated();
		}
	}

	private int GetNumItemsInStorefronts(global::System.Collections.Generic.List<StoreContainer.StorefrontData> storefronts)
	{
		int num = 0;
		foreach (StoreContainer.StorefrontData storefront in storefronts)
		{
			num = storefront.Goods.Count + num;
		}
		return num;
	}

	private void ProcessStorefrontData(StoreContainer.StorefrontData storefront)
	{
		global::UnityEngine.Debug.Log("processing storefront");
		if (storefront.Type == "TapJoy")
		{
			RegisterTapJoyStoreItems(storefront);
		}
		else
		{
			IntegrateStoreFrontForDisplay(storefront);
		}
	}

	private void IntegrateStoreFrontForDisplay(StoreContainer.StorefrontData storefront)
	{
		foreach (StoreContainer.VirtualGoodData good in storefront.Goods)
		{
			global::UnityEngine.Debug.Log("attempting to integrate good: " + good.ShortCode);
			StoreData rawDataForVirtualGoodData = GetRawDataForVirtualGoodData(good);
			if (rawDataForVirtualGoodData != null && ValidForDisplay(rawDataForVirtualGoodData))
			{
				if (!_displayItems.ContainsKey(good.ShortCode))
				{
					_displayItems.Add(good.ShortCode, new StoreDisplayData(rawDataForVirtualGoodData, good));
					global::UnityEngine.Debug.Log("adding display item " + good.ShortCode);
				}
				else if (storefront.Type == "Sale")
				{
					_displayItems[good.ShortCode].SetGood(good);
				}
			}
		}
	}

	private void RegisterTapJoyStoreItems(StoreContainer.StorefrontData storefront)
	{
		foreach (StoreContainer.VirtualGoodData good in storefront.Goods)
		{
			_ = good;
		}
	}

	private StoreData GetRawDataForVirtualGoodData(StoreContainer.VirtualGoodData virtualGood)
	{
		if (_rawStoreData == null)
		{
			global::UnityEngine.Debug.LogError("raw store data is null.");
			return null;
		}
		if (!_rawStoreData.ContainsKey(virtualGood.ShortCode))
		{
			if (!_rawStoreData.ContainsKey(virtualGood.BundleProduct))
			{
				global::UnityEngine.Debug.LogError("Raw store data does not contain " + virtualGood.ShortCode);
				return null;
			}
			return _rawStoreData[virtualGood.BundleProduct];
		}
		return _rawStoreData[virtualGood.ShortCode];
	}

	private bool ValidForDisplay(StoreData rawData)
	{
		foreach (string nonRepeatableItemsFromStoreDatum in GetNonRepeatableItemsFromStoreData(rawData))
		{
			if (NumOwned(nonRepeatableItemsFromStoreDatum) > 0)
			{
				global::UnityEngine.Debug.Log("StoreData " + rawData.Name + " not valid for display");
				return false;
			}
		}
		return true;
	}

	private global::System.Collections.Generic.List<string> GetNonRepeatableItemsFromStoreData(StoreData storeData)
	{
		global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
		foreach (StoreItem item in storeData.Items)
		{
			if (IsUniqueItem(item.Type))
			{
				list.Add(item.Id);
				string concreteItemIDForItem = GetConcreteItemIDForItem(item.Id, item.Type);
				if (!string.IsNullOrEmpty(concreteItemIDForItem))
				{
					list.Add(concreteItemIDForItem);
				}
			}
		}
		return list;
	}

	private bool IsUniqueItem(string type)
	{
		return type switch
		{
			"CPU" => true, 
			"PlushSuit" => true, 
			"AvatarIcon" => true, 
			"EndoskeletonSlot" => true, 
			"PhotoboothFrame" => true, 
			"Trophy" => true, 
			_ => type == "EpisodicContent", 
		};
	}

	private string GetConcreteItemIDForItem(string itemId, string type)
	{
		string text = null;
		switch (type)
		{
		case "CPU":
			if (_itemDefinitions.GetCPUById(itemId) == null)
			{
				global::UnityEngine.Debug.LogError("StoreContainer GetConcreteItemIDForItem - Unable to get ID for '" + itemId + "' of type " + type + "'");
				return null;
			}
			text = _itemDefinitions.GetCPUById(itemId).ConcreteItemId;
			break;
		case "PlushSuit":
			if (_itemDefinitions.GetPlushSuitById(itemId) == null)
			{
				global::UnityEngine.Debug.LogError("StoreContainer GetConcreteItemIDForItem - Unable to get ID for '" + itemId + "' of type " + type + "'");
				return null;
			}
			text = _itemDefinitions.GetPlushSuitById(itemId).ConcreteItemId;
			break;
		case "AvatarIcon":
			if (_itemDefinitions.GetProfileAvatarById(itemId) == null)
			{
				global::UnityEngine.Debug.LogError("StoreContainer GetConcreteItemIDForItem - Unable to get ID for '" + itemId + "' of type " + type + "'");
				return null;
			}
			text = _itemDefinitions.GetProfileAvatarById(itemId).ConcreteItemId;
			break;
		case "Trophy":
			if (_itemDefinitions.GetTrophyById(itemId) == null)
			{
				global::UnityEngine.Debug.LogError("StoreContainer GetConcreteItemIDForItem - Unable to get ID for '" + itemId + "' of type " + type + "'");
				return null;
			}
			text = "Trophy_" + _itemDefinitions.GetTrophyById(itemId).Logical;
			break;
		}
		if (!string.IsNullOrEmpty(text))
		{
			return text;
		}
		global::UnityEngine.Debug.LogError("StoreContainer GetConcreteItemIDForItem - Unable to get ID for '" + itemId + "' of type " + type + "'");
		return null;
	}

	public void ProcessOwnedGoods(global::System.Collections.Generic.Dictionary<string, int> goods)
	{
		_ownedItems = goods;
		_numOwnedChanged = true;
		if (OwnedGoodsReceived != null)
		{
			OwnedGoodsReceived();
		}
	}

	public int NumOwned(string shortcode)
	{
		if (!_ownedItems.ContainsKey(shortcode))
		{
			return 0;
		}
		if (_ownedItems != null)
		{
			return _ownedItems[shortcode];
		}
		return 0;
	}

	public void PlayerStoreDataUpdated(long timestamp)
	{
		_deferredPaymentTimestamp = timestamp;
	}

	public bool PaymentInFlight()
	{
		if (_deferredPaymentTimestamp >= 1)
		{
			return ServerTime.GetCurrentTime() < _deferredPaymentTimestamp;
		}
		return false;
	}

	public StoreDisplayData GetNextAvailableEndoskeleton()
	{
		StoreDisplayData result = null;
		foreach (StoreDisplayData value in _displayItems.Values)
		{
			if (value.Items[0].Type == "EndoskeletonSlot" && !_ownedItems.ContainsKey(value.shortCode))
			{
				result = value;
			}
		}
		return result;
	}

	public void AddOwnedItem(string shortCode, int amount)
	{
		if (!_ownedItems.ContainsKey(shortCode))
		{
			_ownedItems.Add(shortCode, 0);
		}
		if (_ownedItems != null)
		{
			_ownedItems[shortCode] += amount;
		}
	}

	public StoreDisplayData GetDisplayDataForShortCode(string shortCode)
	{
		if (_displayItems.ContainsKey(shortCode))
		{
			return _displayItems[shortCode];
		}
		return null;
	}

	public StoreDisplayData GetStoreItemContainingId(string id)
	{
		foreach (StoreDisplayData value in _displayItems.Values)
		{
			foreach (StoreItem item in value.Items)
			{
				if (item.Id == id)
				{
					return value;
				}
			}
		}
		return null;
	}

	private bool IsValidProductForSubscriptionManager(string receipt)
	{
		return true;
	}

	public StoreDisplayData GetFrameDisplayDataForId(string frameId)
	{
		if (_displayItems.ContainsKey("Frame_" + frameId))
		{
			return _displayItems["Frame_" + frameId];
		}
		return null;
	}
}
