public class HaywireIndicatorUIComponent : EncounterHUDComponent
{
	private global::UnityEngine.UI.Image indicatorImage;

	private HaywireIndicator _indicator;

	public HaywireIndicatorUIComponent(global::UnityEngine.GameObject mainCanvas)
		: base(mainCanvas)
	{
	}

	public void Setup(HaywireIndicator indicator)
	{
		_indicator = indicator;
	}

	protected override void CacheAndPopulateComponents()
	{
		_components = new ComponentContainer();
		global::System.Type[] onlyCacheTypes = new global::System.Type[1] { typeof(global::UnityEngine.UI.Image) };
		_components.CacheComponents(_root, onlyCacheTypes);
		indicatorImage = _components.TryGetComponent<global::UnityEngine.UI.Image>("IndicatorImage");
	}

	public override void Update()
	{
		if (_root.activeSelf && indicatorImage != null)
		{
			indicatorImage.overrideSprite = _indicator.CurrentSprite();
		}
	}

	public override void UpdateVisibility(bool isMaskFullyOff)
	{
		_root.SetActive(_indicator.ShouldShow());
	}
}
