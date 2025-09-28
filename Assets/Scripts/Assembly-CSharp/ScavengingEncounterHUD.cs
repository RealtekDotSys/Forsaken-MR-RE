public class ScavengingEncounterHUD : IxSceneDisplay
{
	private ScavengingStateUIActions _actions;

	private AttackSequenceDomain _attackSequenceDomain;

	private CameraEquipmentDomain _cameraEquipmentDomain;

	private readonly global::System.Collections.Generic.Dictionary<EncounterHUD.HUDComponent, EncounterHUDComponent> _activeComponents;

	public global::UnityEngine.Transform Transform => _root.transform;

	public ScavengingEncounterHUD(global::UnityEngine.GameObject mainCanvas)
		: base(mainCanvas)
	{
		_activeComponents = new global::System.Collections.Generic.Dictionary<EncounterHUD.HUDComponent, EncounterHUDComponent>();
	}

	public void UpdateRoot(global::UnityEngine.GameObject root)
	{
		_root = root;
		_activeComponents.Clear();
		_root.SetActive(value: true);
	}

	public void Setup(AttackSequenceDomain attackSequenceDomain, CameraEquipmentDomain cameraEquipmentDomain, ScavengingStateUIActions actions)
	{
		_activeComponents.Clear();
		_actions = actions;
		_attackSequenceDomain = attackSequenceDomain;
		_cameraEquipmentDomain = cameraEquipmentDomain;
		_root.SetActive(value: true);
	}

	protected override void CacheAndPopulateComponents()
	{
		ComponentContainer components = new ComponentContainer();
		_components = components;
		global::System.Type[] onlyCacheTypes = new global::System.Type[1] { typeof(global::UnityEngine.RectTransform) };
		_components.CacheComponents(_root, onlyCacheTypes);
	}

	public void InitializeShockerComponent()
	{
		global::UnityEngine.GameObject gameObject = _components.TryGetComponent<global::UnityEngine.RectTransform>("Button_Shocker").gameObject;
		if (gameObject == null)
		{
			global::UnityEngine.Debug.LogError("ScavengingEncounterHUD InitializeShockerComponent - The current HUD layout has no Shocker Component");
		}
		ShockerUIComponent shockerUIComponent = new ShockerUIComponent(gameObject);
		shockerUIComponent.Setup(_actions, _attackSequenceDomain, _cameraEquipmentDomain.Shocker);
		_activeComponents.Add(EncounterHUD.HUDComponent.Shocker, shockerUIComponent);
	}

	public void InitializeFlashlightComponent()
	{
		global::UnityEngine.GameObject gameObject = _components.TryGetComponent<global::UnityEngine.RectTransform>("Button_Flashlight").gameObject;
		if (gameObject == null)
		{
			global::UnityEngine.Debug.LogError("EncounterHUD InitializeFlashlightComponent - The current HUD layout has no Flashlight Component");
		}
		FlashlightUIComponent flashlightUIComponent = new FlashlightUIComponent(gameObject);
		flashlightUIComponent.Setup(_actions, _attackSequenceDomain, _cameraEquipmentDomain.Flashlight);
		_activeComponents.Add(EncounterHUD.HUDComponent.Flashlight, flashlightUIComponent);
	}

	public void InitializeBatteryComponent(global::System.Collections.Generic.List<BatteryColorToValue> colorValues, global::UnityEngine.Color surgeColor)
	{
		global::UnityEngine.GameObject gameObject = _components.TryGetComponent<global::UnityEngine.RectTransform>("BatteryPanel").gameObject;
		if (gameObject == null)
		{
			global::UnityEngine.Debug.LogError("EncounterHUD InitializeBatteryComponent - The current HUD layout has no Battery Component");
		}
		BatteryUIComponent batteryUIComponent = new BatteryUIComponent(gameObject);
		batteryUIComponent.Setup(null, colorValues, surgeColor, _attackSequenceDomain, _cameraEquipmentDomain.Battery);
		_activeComponents.Add(EncounterHUD.HUDComponent.Battery, batteryUIComponent);
	}

	public void Update()
	{
		foreach (EncounterHUDComponent value in _activeComponents.Values)
		{
			value.Update();
		}
	}

	public void UpdateVisibility()
	{
		foreach (EncounterHUDComponent value in _activeComponents.Values)
		{
			value.UpdateVisibility(_cameraEquipmentDomain.Mask.IsMaskFullyOff());
		}
	}

	public override void Teardown()
	{
		foreach (EncounterHUDComponent value in _activeComponents.Values)
		{
			value.Teardown();
		}
		_activeComponents.Clear();
	}
}
