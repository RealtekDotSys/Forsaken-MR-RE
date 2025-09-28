public class IxTableDisplay<T1, T2> where T1 : IxItemCellDisplay<T2>, new()
{
	private const int STARTING_LIST_CAPACITY = 16;

	private static global::UnityEngine.GameObject _templateItemFosterHome;

	protected global::System.Collections.Generic.List<T1> _displayItems;

	protected global::System.Collections.Generic.List<T2> _dataItems;

	protected global::UnityEngine.GameObject _templateItem;

	protected global::UnityEngine.UI.LayoutGroup _itemDisplayParent;

	public global::System.Collections.Generic.List<T1> DisplayItems => _displayItems;

	public IxTableDisplay(global::UnityEngine.UI.LayoutGroup itemDisplayParent)
	{
		_itemDisplayParent = itemDisplayParent;
		_displayItems = new global::System.Collections.Generic.List<T1>();
		_dataItems = new global::System.Collections.Generic.List<T2>();
		EnsureOrphanParent();
		PrepareTemplateItem();
	}

	public void Teardown()
	{
		global::UnityEngine.Debug.Log("Torndown table display");
		global::UnityEngine.Object.Destroy(_templateItem);
		foreach (T1 displayItem in _displayItems)
		{
			displayItem.TearDown();
		}
	}

	public void SetItems(global::System.Collections.Generic.List<T2> dataItems)
	{
		Clear();
		foreach (T2 dataItem in dataItems)
		{
			AddItem(dataItem);
		}
	}

	public T1 GetItem(int index)
	{
		if (_displayItems.Count - 1 < index)
		{
			return null;
		}
		return _displayItems[index];
	}

	public void AddItem(T2 dataItem)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_templateItem, _itemDisplayParent.transform, worldPositionStays: false);
		gameObject.SetActive(value: true);
		T1 val = new T1();
		val.Setup(gameObject, dataItem);
		val.UpdateData();
		_displayItems.Add(val);
		_dataItems.Add(dataItem);
	}

	public void Clear()
	{
		global::UnityEngine.Debug.LogError("Clearing! " + _displayItems.Count);
		if (_displayItems.Count > 0)
		{
			foreach (T1 displayItem in _displayItems)
			{
				global::UnityEngine.Debug.Log("tearin down");
				displayItem.TearDown();
			}
		}
		_displayItems.Clear();
		_dataItems.Clear();
	}

	private void EnsureOrphanParent()
	{
		if (!(_templateItemFosterHome != null))
		{
			_templateItemFosterHome = new global::UnityEngine.GameObject("TemplateItemFosterHome");
			_templateItemFosterHome.SetActive(value: false);
		}
	}

	private void PrepareTemplateItem()
	{
		(_templateItem = _itemDisplayParent.transform.GetChild(0).gameObject).SetActive(value: false);
	}
}
