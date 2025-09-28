public class AnimatronicStatDisplay : IxItemCellDisplay<AnimatronicStatDisplayData>
{
	private global::TMPro.TextMeshProUGUI _nameLabel;

	private global::UnityEngine.UI.Image _backdrop;

	private global::UnityEngine.UI.Image _filler;

	private MasterDomain _masterDomain;

	protected override void PopulateComponents()
	{
		_masterDomain = MasterDomain.GetDomain();
		_components.CacheComponents(_root, ComponentContainer.TextsImages);
		_backdrop = _components.TryGetComponent<global::UnityEngine.UI.Image>("StatBackingImage");
		_filler = _components.TryGetComponent<global::UnityEngine.UI.Image>("StatFillImage");
		_nameLabel = _components.TryGetComponent<global::TMPro.TextMeshProUGUI>("StatText");
	}

	public override void UpdateData()
	{
		if (_dataItem.fillerSprite != null)
		{
			_filler.sprite = _dataItem.fillerSprite;
		}
		if (_dataItem.backdropSprite != null)
		{
			_backdrop.sprite = _dataItem.backdropSprite;
		}
	}

	public void SetStatValue(float value, float loadAmount, float minValue, float maxValue)
	{
		float fillAmount = 0f;
		if (maxValue != 0f)
		{
			fillAmount = (maxValue - minValue - (maxValue - value)) / (maxValue - minValue) * loadAmount;
		}
		_filler.fillAmount = fillAmount;
	}
}
