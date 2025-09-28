namespace SRF.UI.Layout
{
	[global::UnityEngine.AddComponentMenu("SRF/UI/Layout/VerticalLayoutGroup (Virtualizing)")]
	public class VirtualVerticalLayoutGroup : global::UnityEngine.UI.LayoutGroup, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		[global::System.Serializable]
		public class SelectedItemChangedEvent : global::UnityEngine.Events.UnityEvent<object>
		{
		}

		[global::System.Serializable]
		private class Row
		{
			public object Data;

			public int Index;

			public global::UnityEngine.RectTransform Rect;

			public global::SRF.UI.StyleRoot Root;

			public global::SRF.UI.Layout.IVirtualView View;
		}

		private readonly global::SRF.SRList<object> _itemList = new global::SRF.SRList<object>();

		private readonly global::SRF.SRList<int> _visibleItemList = new global::SRF.SRList<int>();

		private bool _isDirty;

		private global::SRF.SRList<global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row> _rowCache = new global::SRF.SRList<global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row>();

		private global::UnityEngine.UI.ScrollRect _scrollRect;

		private int _selectedIndex;

		private object _selectedItem;

		[global::UnityEngine.SerializeField]
		private global::SRF.UI.Layout.VirtualVerticalLayoutGroup.SelectedItemChangedEvent _selectedItemChanged;

		private int _visibleItemCount;

		private global::SRF.SRList<global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row> _visibleRows = new global::SRF.SRList<global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row>();

		public global::SRF.UI.StyleSheet AltRowStyleSheet;

		public bool EnableSelection = true;

		public global::UnityEngine.RectTransform ItemPrefab;

		public int RowPadding = 2;

		public global::SRF.UI.StyleSheet RowStyleSheet;

		public global::SRF.UI.StyleSheet SelectedRowStyleSheet;

		public float Spacing;

		public bool StickToBottom = true;

		private float _itemHeight = -1f;

		public global::SRF.UI.Layout.VirtualVerticalLayoutGroup.SelectedItemChangedEvent SelectedItemChanged
		{
			get
			{
				return _selectedItemChanged;
			}
			set
			{
				_selectedItemChanged = value;
			}
		}

		public object SelectedItem
		{
			get
			{
				return _selectedItem;
			}
			set
			{
				if (_selectedItem != value && EnableSelection)
				{
					int num = ((value == null) ? (-1) : _itemList.IndexOf(value));
					if (value != null && num < 0)
					{
						throw new global::System.InvalidOperationException("Cannot select item not present in layout");
					}
					if (_selectedItem != null)
					{
						InvalidateItem(_selectedIndex);
					}
					_selectedItem = value;
					_selectedIndex = num;
					if (_selectedItem != null)
					{
						InvalidateItem(_selectedIndex);
					}
					SetDirty();
					if (_selectedItemChanged != null)
					{
						_selectedItemChanged.Invoke(_selectedItem);
					}
				}
			}
		}

		public override float minHeight => (float)_itemList.Count * ItemHeight + (float)base.padding.top + (float)base.padding.bottom + Spacing * (float)_itemList.Count;

		private global::UnityEngine.UI.ScrollRect ScrollRect
		{
			get
			{
				if (_scrollRect == null)
				{
					_scrollRect = GetComponentInParent<global::UnityEngine.UI.ScrollRect>();
				}
				return _scrollRect;
			}
		}

		private bool AlignBottom
		{
			get
			{
				if (base.childAlignment != global::UnityEngine.TextAnchor.LowerRight && base.childAlignment != global::UnityEngine.TextAnchor.LowerCenter)
				{
					return base.childAlignment == global::UnityEngine.TextAnchor.LowerLeft;
				}
				return true;
			}
		}

		private bool AlignTop
		{
			get
			{
				if (base.childAlignment != global::UnityEngine.TextAnchor.UpperLeft && base.childAlignment != global::UnityEngine.TextAnchor.UpperCenter)
				{
					return base.childAlignment == global::UnityEngine.TextAnchor.UpperRight;
				}
				return true;
			}
		}

		private float ItemHeight
		{
			get
			{
				if (_itemHeight <= 0f)
				{
					if (ItemPrefab.GetComponent(typeof(global::UnityEngine.UI.ILayoutElement)) is global::UnityEngine.UI.ILayoutElement layoutElement)
					{
						_itemHeight = layoutElement.preferredHeight;
					}
					else
					{
						_itemHeight = ItemPrefab.rect.height;
					}
					if (_itemHeight.ApproxZero())
					{
						global::UnityEngine.Debug.LogWarning("[VirtualVerticalLayoutGroup] ItemPrefab must have a preferred size greater than 0");
						_itemHeight = 10f;
					}
				}
				return _itemHeight;
			}
		}

		public void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!EnableSelection)
			{
				return;
			}
			global::UnityEngine.GameObject gameObject = eventData.pointerPressRaycast.gameObject;
			if (!(gameObject == null))
			{
				global::UnityEngine.Vector3 position = gameObject.transform.position;
				int num = global::UnityEngine.Mathf.FloorToInt(global::UnityEngine.Mathf.Abs(base.rectTransform.InverseTransformPoint(position).y) / ItemHeight);
				if (num >= 0 && num < _itemList.Count)
				{
					SelectedItem = _itemList[num];
				}
				else
				{
					SelectedItem = null;
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			ScrollRect.onValueChanged.AddListener(OnScrollRectValueChanged);
			if (ItemPrefab.GetComponent(typeof(global::SRF.UI.Layout.IVirtualView)) == null)
			{
				global::UnityEngine.Debug.LogWarning("[VirtualVerticalLayoutGroup] ItemPrefab does not have a component inheriting from IVirtualView, so no data binding can occur");
			}
		}

		private void OnScrollRectValueChanged(global::UnityEngine.Vector2 d)
		{
			if (d.y < 0f || d.y > 1f)
			{
				_scrollRect.verticalNormalizedPosition = global::UnityEngine.Mathf.Clamp01(d.y);
			}
			SetDirty();
		}

		protected override void Start()
		{
			base.Start();
			ScrollUpdate();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			SetDirty();
		}

		protected void Update()
		{
			if (!AlignBottom && !AlignTop)
			{
				global::UnityEngine.Debug.LogWarning("[VirtualVerticalLayoutGroup] Only Lower or Upper alignment is supported.", this);
				base.childAlignment = global::UnityEngine.TextAnchor.UpperLeft;
			}
			if (SelectedItem != null && !_itemList.Contains(SelectedItem))
			{
				SelectedItem = null;
			}
			if (_isDirty)
			{
				_isDirty = false;
				ScrollUpdate();
			}
		}

		protected void InvalidateItem(int itemIndex)
		{
			if (!_visibleItemList.Contains(itemIndex))
			{
				return;
			}
			_visibleItemList.Remove(itemIndex);
			for (int i = 0; i < _visibleRows.Count; i++)
			{
				if (_visibleRows[i].Index == itemIndex)
				{
					RecycleRow(_visibleRows[i]);
					_visibleRows.RemoveAt(i);
					break;
				}
			}
		}

		protected void RefreshIndexCache()
		{
			for (int i = 0; i < _visibleRows.Count; i++)
			{
				_visibleRows[i].Index = _itemList.IndexOf(_visibleRows[i].Data);
			}
		}

		protected void ScrollUpdate()
		{
			if (!global::UnityEngine.Application.isPlaying)
			{
				return;
			}
			float y = base.rectTransform.anchoredPosition.y;
			float height = ((global::UnityEngine.RectTransform)ScrollRect.transform).rect.height;
			int num = global::UnityEngine.Mathf.FloorToInt(y / (ItemHeight + Spacing));
			int num2 = global::UnityEngine.Mathf.CeilToInt((y + height) / (ItemHeight + Spacing));
			num -= RowPadding;
			num2 += RowPadding;
			num = global::UnityEngine.Mathf.Max(0, num);
			num2 = global::UnityEngine.Mathf.Min(_itemList.Count, num2);
			bool flag = false;
			for (int i = 0; i < _visibleRows.Count; i++)
			{
				global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row = _visibleRows[i];
				if (row.Index < num || row.Index > num2)
				{
					_visibleItemList.Remove(row.Index);
					_visibleRows.Remove(row);
					RecycleRow(row);
					flag = true;
				}
			}
			for (int j = num; j < num2 && j < _itemList.Count; j++)
			{
				if (!_visibleItemList.Contains(j))
				{
					global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row2 = GetRow(j);
					_visibleRows.Add(row2);
					_visibleItemList.Add(j);
					flag = true;
				}
			}
			if (flag || _visibleItemCount != _visibleRows.Count)
			{
				global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(base.rectTransform);
			}
			_visibleItemCount = _visibleRows.Count;
		}

		public override void CalculateLayoutInputVertical()
		{
			SetLayoutInputForAxis(minHeight, minHeight, -1f, 1);
		}

		public override void SetLayoutHorizontal()
		{
			float num = base.rectTransform.rect.width - (float)base.padding.left - (float)base.padding.right;
			for (int i = 0; i < _visibleRows.Count; i++)
			{
				global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row = _visibleRows[i];
				SetChildAlongAxis(row.Rect, 0, base.padding.left, num);
			}
			for (int j = 0; j < _rowCache.Count; j++)
			{
				global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row2 = _rowCache[j];
				SetChildAlongAxis(row2.Rect, 0, 0f - num - (float)base.padding.left, num);
			}
		}

		public override void SetLayoutVertical()
		{
			if (global::UnityEngine.Application.isPlaying)
			{
				for (int i = 0; i < _visibleRows.Count; i++)
				{
					global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row = _visibleRows[i];
					SetChildAlongAxis(row.Rect, 1, (float)row.Index * ItemHeight + (float)base.padding.top + Spacing * (float)row.Index, ItemHeight);
				}
			}
		}

		private new void SetDirty()
		{
			base.SetDirty();
			if (IsActive())
			{
				_isDirty = true;
			}
		}

		public void AddItem(object item)
		{
			_itemList.Add(item);
			SetDirty();
			if (StickToBottom && global::UnityEngine.Mathf.Approximately(ScrollRect.verticalNormalizedPosition, 0f))
			{
				ScrollRect.normalizedPosition = new global::UnityEngine.Vector2(0f, 0f);
			}
		}

		public void RemoveItem(object item)
		{
			if (SelectedItem == item)
			{
				SelectedItem = null;
			}
			int itemIndex = _itemList.IndexOf(item);
			InvalidateItem(itemIndex);
			_itemList.Remove(item);
			RefreshIndexCache();
			SetDirty();
		}

		public void ClearItems()
		{
			for (int num = _visibleRows.Count - 1; num >= 0; num--)
			{
				InvalidateItem(_visibleRows[num].Index);
			}
			_itemList.Clear();
			SetDirty();
		}

		private global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row GetRow(int forIndex)
		{
			if (_rowCache.Count == 0)
			{
				global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row = CreateRow();
				PopulateRow(forIndex, row);
				return row;
			}
			object obj = _itemList[forIndex];
			global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row2 = null;
			global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row3 = null;
			int num = forIndex % 2;
			for (int i = 0; i < _rowCache.Count; i++)
			{
				row2 = _rowCache[i];
				if (row2.Data == obj)
				{
					_rowCache.RemoveAt(i);
					PopulateRow(forIndex, row2);
					break;
				}
				if (row2.Index % 2 == num)
				{
					row3 = row2;
				}
				row2 = null;
			}
			if (row2 == null && row3 != null)
			{
				_rowCache.Remove(row3);
				row2 = row3;
				PopulateRow(forIndex, row2);
			}
			else if (row2 == null)
			{
				row2 = _rowCache.PopLast();
				PopulateRow(forIndex, row2);
			}
			return row2;
		}

		private void RecycleRow(global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row)
		{
			_rowCache.Add(row);
		}

		private void PopulateRow(int index, global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row)
		{
			row.Index = index;
			row.Data = _itemList[index];
			row.View.SetDataContext(_itemList[index]);
			if (RowStyleSheet != null || AltRowStyleSheet != null || SelectedRowStyleSheet != null)
			{
				if (SelectedRowStyleSheet != null && SelectedItem == row.Data)
				{
					row.Root.StyleSheet = SelectedRowStyleSheet;
				}
				else
				{
					row.Root.StyleSheet = ((index % 2 == 0) ? RowStyleSheet : AltRowStyleSheet);
				}
			}
		}

		private global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row CreateRow()
		{
			global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row = new global::SRF.UI.Layout.VirtualVerticalLayoutGroup.Row();
			global::UnityEngine.RectTransform rectTransform = (row.Rect = SRInstantiate.Instantiate(ItemPrefab));
			row.View = rectTransform.GetComponent(typeof(global::SRF.UI.Layout.IVirtualView)) as global::SRF.UI.Layout.IVirtualView;
			if (RowStyleSheet != null || AltRowStyleSheet != null || SelectedRowStyleSheet != null)
			{
				row.Root = rectTransform.gameObject.GetComponentOrAdd<global::SRF.UI.StyleRoot>();
				row.Root.StyleSheet = RowStyleSheet;
			}
			rectTransform.SetParent(base.rectTransform, worldPositionStays: false);
			return row;
		}
	}
}
