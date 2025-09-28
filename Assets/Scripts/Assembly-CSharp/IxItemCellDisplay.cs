public abstract class IxItemCellDisplay<T> : IIxItemCellDisplay
{
	protected global::UnityEngine.GameObject _root;

	protected ComponentContainer _components;

	protected T _dataItem;

	public virtual void Setup(global::UnityEngine.GameObject root, T dataItem)
	{
		_root = root;
		_components = new ComponentContainer();
		_dataItem = dataItem;
		PopulateComponents();
	}

	public virtual void TearDown()
	{
		if (_components != null)
		{
			_components.TearDown();
		}
		_components = null;
		global::UnityEngine.Object.Destroy(_root);
	}

	protected abstract void PopulateComponents();

	public abstract void UpdateData();

	public float GetWidth()
	{
		return ((global::UnityEngine.RectTransform)_root.transform).rect.xMin;
	}

	public float GetHeight()
	{
		return ((global::UnityEngine.RectTransform)_root.transform).rect.xMin;
	}
}
