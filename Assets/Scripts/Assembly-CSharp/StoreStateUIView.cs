public class StoreStateUIView : global::UnityEngine.MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass26_0
	{
		public StoreCell carouselItem;

		internal void _003CPopulateCarouselSection_003Eb__0(global::UnityEngine.Sprite icon)
		{
			carouselItem.SetSprite(icon);
		}
	}

	private sealed class _003C_003Ec__DisplayClass28_0
	{
		public StoreCell cell;

		internal void _003CAddItemsToStoreSection_003Eb__0(global::UnityEngine.Sprite icon)
		{
			cell.SetSprite(icon);
		}
	}

	[global::UnityEngine.SerializeField]
	private StoreStateUIActions storeStateUIActions;

	[global::UnityEngine.SerializeField]
	private StoreCell storeCellPrefab;

	[global::UnityEngine.SerializeField]
	private StoreSection storeSectionPrefab;

	[global::UnityEngine.SerializeField]
	private StoreCell CarouselBannerPrefab;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform StoreSectionParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.ScrollRect StoreScrollRect;

	public const string SCROLL_TARGET_DEFAULT = "None";

	public const string SCROLL_TARGET_DEVICE = "Device";

	public const string SCROLL_TARGET_FAZCOINS = "FazCoins";

	public const string SCROLL_TARGET_LURE = "Lure";

	public const string SCROLL_TARGET_MINIPACK = "MiniPack";

	public const string SCROLL_TARGET_PACK = "Pack";

	public const string SCROLL_TARGET_BUFF = "Buff";

	private GameUIDomain _uiDomain;

	private StoreDomain _storeDomain;

	private EventExposer _eventExposer;

	private global::System.Collections.Generic.Dictionary<string, StoreSection> _storeSectionsByType;

	private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<StoreDisplayData>> _sortedDisplayItems;

	public void CreateStoreSections()
	{
		if (_storeSectionsByType == null)
		{
			_storeSectionsByType = new global::System.Collections.Generic.Dictionary<string, StoreSection>();
		}
		else
		{
			foreach (StoreSection value in _storeSectionsByType.Values)
			{
				value.Reset();
				global::UnityEngine.Object.Destroy(value.gameObject);
			}
		}
		foreach (StoreSectionData sectionDatum in _storeDomain.StoreContainer.SectionData)
		{
			_storeSectionsByType[sectionDatum.Logical] = CreateSection(sectionDatum);
		}
	}

	private StoreSection CreateSection(StoreSectionData data)
	{
		StoreSection storeSection = global::UnityEngine.Object.Instantiate(storeSectionPrefab, StoreSectionParent);
		storeSection.SetData(data, _storeDomain);
		return storeSection;
	}

	private void ResetSections()
	{
		global::UnityEngine.Debug.LogError("resetting sections");
		foreach (StoreSection value in _storeSectionsByType.Values)
		{
			value.Reset();
		}
	}

	public void IconsLoaded()
	{
		PopulateStore();
	}

	public void PopulateStore()
	{
		if (_storeDomain.Icons.IconsReady())
		{
			PopulateStoreItems();
		}
	}

	private void PopulateStoreItems()
	{
		SortDisplayItems();
		ResetSections();
		global::UnityEngine.Debug.LogError("populating store items");
		foreach (StoreSectionData sectionDatum in _storeDomain.StoreContainer.SectionData)
		{
			if (!_sortedDisplayItems.ContainsKey(sectionDatum.Logical))
			{
				continue;
			}
			global::System.Collections.Generic.List<StoreDisplayData> list = _sortedDisplayItems[sectionDatum.Logical];
			if (sectionDatum.Type != StoreSectionData.DisplayType.Carousel && sectionDatum.Type == StoreSectionData.DisplayType.Main)
			{
				global::System.Linq.Enumerable.OrderBy(list, (StoreDisplayData x) => x.storeData.Order);
				PopulateMainSection(sectionDatum.Logical, list);
			}
		}
	}

	private void PopulateMainSection(string key, global::System.Collections.Generic.List<StoreDisplayData> items)
	{
		if (_storeSectionsByType.ContainsKey(key) && _sortedDisplayItems.ContainsKey(key))
		{
			AddItemsToStoreSection(_storeSectionsByType[key], items);
		}
	}

	private void PopulateCarouselSection(global::System.Collections.Generic.List<StoreDisplayData> items)
	{
		foreach (StoreDisplayData item in items)
		{
			StoreStateUIView._003C_003Ec__DisplayClass26_0 _003C_003Ec__DisplayClass26_ = new StoreStateUIView._003C_003Ec__DisplayClass26_0();
			_003C_003Ec__DisplayClass26_.carouselItem = global::UnityEngine.Object.Instantiate(CarouselBannerPrefab);
			_storeDomain.Icons.GetStoreIcon(item.storeData.ArtRef, _003C_003Ec__DisplayClass26_._003CPopulateCarouselSection_003Eb__0);
			StoreCellData storeCellData = new StoreCellData();
			storeCellData.OnClickedDelegate = OnStoreButtonClicked;
			storeCellData.displayData = item;
			_003C_003Ec__DisplayClass26_.carouselItem.SetData(storeCellData);
		}
	}

	private void SortDisplayItems()
	{
		_sortedDisplayItems.Clear();
		foreach (StoreDisplayData value in _storeDomain.StoreContainer.DisplayItems.Values)
		{
			if (!_sortedDisplayItems.ContainsKey(value.storeData.StoreSection))
			{
				_sortedDisplayItems[value.storeData.StoreSection] = new global::System.Collections.Generic.List<StoreDisplayData>();
			}
			_sortedDisplayItems[value.storeData.StoreSection].Add(value);
		}
	}

	private void AddItemsToStoreSection(StoreSection section, global::System.Collections.Generic.List<StoreDisplayData> displayData)
	{
		foreach (StoreDisplayData displayDatum in displayData)
		{
			StoreStateUIView._003C_003Ec__DisplayClass28_0 _003C_003Ec__DisplayClass28_ = new StoreStateUIView._003C_003Ec__DisplayClass28_0();
			_003C_003Ec__DisplayClass28_.cell = global::UnityEngine.Object.Instantiate(storeCellPrefab);
			_storeDomain.Icons.GetStoreIcon(displayDatum.storeData.ArtRef, _003C_003Ec__DisplayClass28_._003CAddItemsToStoreSection_003Eb__0);
			StoreCellData storeCellData = new StoreCellData();
			storeCellData.OnClickedDelegate = OnStoreButtonClicked;
			storeCellData.displayData = displayDatum;
			_003C_003Ec__DisplayClass28_.cell.SetData(storeCellData);
			section.AddItem(displayDatum.shortCode, _003C_003Ec__DisplayClass28_.cell);
		}
	}

	public void OnStoreButtonClicked(StoreCell cell)
	{
		if (!HandleSpecialTypes(cell))
		{
			storeStateUIActions.ShowPurchaseDialog(cell);
		}
	}

	public void OnStoreDataUpdated()
	{
		PopulateStore();
	}

	public void OnPurchaseError(string error)
	{
		PopulateStore();
	}

	private bool HandleSpecialTypes(StoreCell cell)
	{
		if (cell.Data.storeData.ActionType == "url")
		{
			global::UnityEngine.Application.OpenURL(cell.Data.storeData.ActionPayload);
			return true;
		}
		return false;
	}

	private global::System.Collections.IEnumerator DelayByFrames(int frames, global::System.Action action)
	{
		int framesLeft = frames;
		while (framesLeft > 0)
		{
			framesLeft--;
			yield return null;
		}
		action();
	}

	private void HandleScrollTarget()
	{
		_uiDomain.ResetStoreScrollSettings();
		if (ShouldScroll(_uiDomain.GameUIData.storeScrollSection, _uiDomain.GameUIData.storeScrollItem))
		{
			if (_uiDomain.GameUIData.storeScrollSection == "Carousel")
			{
				ScrollToCarouselItem(_uiDomain.GameUIData.storeScrollItem);
			}
			else
			{
				ScrollToSection(_uiDomain.GameUIData.storeScrollSection, _uiDomain.GameUIData.storeScrollItem);
			}
		}
	}

	private void ScrollToCarouselItem(string itemId)
	{
	}

	private bool ShouldScroll(string section, string item)
	{
		if (section == "None")
		{
			return !string.IsNullOrEmpty(item);
		}
		return true;
	}

	private void ScrollToSection(string sectionType, string itemType)
	{
		if (_storeSectionsByType.ContainsKey(sectionType) && !(_storeSectionsByType[sectionType] == null))
		{
			ScrollToRectTransform(StoreScrollRect, _storeSectionsByType[sectionType], itemType);
		}
	}

	private void ScrollToRectTransform(global::UnityEngine.UI.ScrollRect scrollRect, StoreSection section, string storeItem)
	{
		StoreCell cell = section.GetCell(storeItem);
		if (!(cell == null))
		{
			global::UnityEngine.Vector2 snapToPositionToBringChildIntoView = GetSnapToPositionToBringChildIntoView(scrollRect, section.GetRectTransform(), cell.GetCellTransform());
			scrollRect.content.localPosition = new global::UnityEngine.Vector3
			{
				x = scrollRect.content.localPosition.x,
				y = snapToPositionToBringChildIntoView.y,
				z = scrollRect.content.localPosition.z
			};
			OnStoreButtonClicked(cell);
			global::UnityEngine.Canvas.ForceUpdateCanvases();
		}
	}

	private static global::UnityEngine.Vector2 GetSnapToPositionToBringChildIntoView(global::UnityEngine.UI.ScrollRect scrollRect, global::UnityEngine.RectTransform sectionTransform, global::UnityEngine.RectTransform itemTransform)
	{
		global::UnityEngine.Canvas.ForceUpdateCanvases();
		global::UnityEngine.Vector2 vector = scrollRect.viewport.localPosition;
		global::UnityEngine.Vector2 vector2 = sectionTransform.localPosition;
		global::UnityEngine.Vector2 vector3 = itemTransform.localPosition;
		return new global::UnityEngine.Vector2(0f - (vector.x + vector2.x + vector3.x), 0f - (vector.y + vector2.y + vector3.y));
	}

	private void OnEnable()
	{
		MasterDomain domain = MasterDomain.GetDomain();
		_storeDomain = domain.StoreDomain;
		_eventExposer = domain.eventExposer;
		_uiDomain = domain.GameUIDomain;
		_sortedDisplayItems = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<StoreDisplayData>>();
		CreateStoreSections();
		PopulateStore();
		_eventExposer.OnStoreOpened();
		StoreDomain storeDomain = _storeDomain;
		storeDomain.IconsLoaded = (global::System.Action)global::System.Delegate.Combine(storeDomain.IconsLoaded, new global::System.Action(IconsLoaded));
		StoreDomain storeDomain2 = _storeDomain;
		storeDomain2.PurchaseErrorCallback = (global::System.Action<string>)global::System.Delegate.Combine(storeDomain2.PurchaseErrorCallback, new global::System.Action<string>(OnPurchaseError));
		_storeDomain.RequestVirtualGoods();
	}

	private void Start()
	{
	}

	private void OnDisable()
	{
		StoreDomain storeDomain = _storeDomain;
		storeDomain.IconsLoaded = (global::System.Action)global::System.Delegate.Remove(storeDomain.IconsLoaded, new global::System.Action(IconsLoaded));
		StoreDomain storeDomain2 = _storeDomain;
		storeDomain2.PurchaseErrorCallback = (global::System.Action<string>)global::System.Delegate.Remove(storeDomain2.PurchaseErrorCallback, new global::System.Action<string>(OnPurchaseError));
		_eventExposer.OnStoreClosed();
	}
}
