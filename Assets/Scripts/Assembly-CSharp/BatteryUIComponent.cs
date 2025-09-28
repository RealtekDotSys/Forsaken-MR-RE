public class BatteryUIComponent : EncounterHUDComponent
{
	private const string PERCENT_STRING = "%";

	private global::UnityEngine.UI.Slider _batterySlider;

	private global::UnityEngine.UI.Image _batteryBG;

	private global::UnityEngine.UI.Image _batteryColor;

	private global::TMPro.TextMeshProUGUI _batteryPct;

	private global::UnityEngine.RectTransform _batteryDefenderVFX;

	private BatteryColorHandler _batteryColorHandler;

	private AttackSequenceDomain _attackSequenceDomain;

	private IBattery _battery;

	private bool _allowed;

	public bool Active => _root.activeSelf;

	public BatteryUIComponent(global::UnityEngine.GameObject mainCanvas)
		: base(mainCanvas)
	{
	}

	public void Setup(SurgeMechanicUIHandler surgeHandler, global::System.Collections.Generic.List<BatteryColorToValue> colorToValues, global::UnityEngine.Color surgeColor, AttackSequenceDomain attackSequenceDomain, IBattery battery)
	{
		_batteryColorHandler = new BatteryColorHandler(new BatteryColorHandlerData(surgeHandler, _batterySlider, _batteryColor, _batteryBG, _batteryPct, colorToValues, surgeColor));
		_attackSequenceDomain = attackSequenceDomain;
		_battery = battery;
		_allowed = true;
	}

	protected override void CacheAndPopulateComponents()
	{
		_components = new ComponentContainer();
		global::System.Type[] onlyCacheTypes = new global::System.Type[4]
		{
			typeof(global::UnityEngine.RectTransform),
			typeof(global::UnityEngine.UI.Image),
			typeof(global::UnityEngine.UI.Slider),
			typeof(global::TMPro.TextMeshProUGUI)
		};
		_components.CacheComponents(_root, onlyCacheTypes);
		_batteryBG = _components.TryGetComponent<global::UnityEngine.UI.Image>("BG");
		_batteryColor = _components.TryGetComponent<global::UnityEngine.UI.Image>("Fill");
		_batterySlider = _components.TryGetComponent<global::UnityEngine.UI.Slider>("BatterySlider");
		_batteryPct = _components.TryGetComponent<global::TMPro.TextMeshProUGUI>("ValueText");
	}

	public override void Update()
	{
		if (_root.activeSelf)
		{
			UpdateBatteryChargeNumber();
			UpdateBatteryChargeSlider();
			_batteryColorHandler.Update();
		}
	}

	private void UpdateBatteryChargeNumber()
	{
		if (_batteryPct != null)
		{
			_batteryPct.text = global::UnityEngine.Mathf.FloorToInt(_battery.Charge * 100f) + "%";
		}
	}

	private void UpdateBatteryChargeSlider()
	{
		if (_batterySlider != null)
		{
			_batterySlider.value = _battery.Charge;
		}
	}

	public override void UpdateVisibility(bool isMaskFullyOff)
	{
		bool flag = false;
		if (_attackSequenceDomain.GetEncounterUIConfig() != null)
		{
			flag = _attackSequenceDomain.GetEncounterUIConfig().ShowBattery;
		}
		bool flag2 = _allowed && isMaskFullyOff;
		_root.SetActive(flag && flag2);
	}
}
