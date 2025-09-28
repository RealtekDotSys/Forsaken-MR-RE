public class StoreSection : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI TitleText;

	public global::UnityEngine.Transform SectionContents;

	public StoreSectionData.DisplayType Type;

	private global::System.Collections.Generic.Dictionary<string, StoreCell> _cells;

	public void SetData(StoreSectionData data, StoreDomain storeDomain)
	{
		Type = data.Type;
		TitleText.text = data.DisplayName;
		_cells = new global::System.Collections.Generic.Dictionary<string, StoreCell>();
	}

	public void AddItem(string id, StoreCell cell)
	{
		cell.transform.SetParent(SectionContents, worldPositionStays: false);
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(value: true);
		}
		_cells.Add(id, cell);
	}

	public void Reset()
	{
		foreach (StoreCell value in _cells.Values)
		{
			global::UnityEngine.Object.Destroy(value.gameObject);
		}
		_cells.Clear();
		base.gameObject.SetActive(value: false);
	}

	public global::UnityEngine.RectTransform GetRectTransform()
	{
		return GetComponent<global::UnityEngine.RectTransform>();
	}

	public StoreCell GetCell(string itemId)
	{
		if (_cells.ContainsKey(itemId))
		{
			return _cells[itemId];
		}
		return null;
	}
}
