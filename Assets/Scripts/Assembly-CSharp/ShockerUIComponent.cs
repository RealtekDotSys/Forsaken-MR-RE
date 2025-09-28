public class ShockerUIComponent : EncounterHUDComponent
{
	private sealed class _003C_003Ec__DisplayClass10_0
	{
		public CameraStateUIActions actions;

		internal void _003CSetup_003Eb__0(global::UnityEngine.EventSystems.BaseEventData listener)
		{
			actions.ShockButtonTriggered();
		}

		internal void _003CSetup_003Eb__1(global::UnityEngine.EventSystems.BaseEventData listener)
		{
			actions.ShockButtonReleased();
		}
	}

	private sealed class _003C_003Ec__DisplayClass10_0Scavenge
	{
		public ScavengingStateUIActions actions;

		internal void _003CSetup_003Eb__0(global::UnityEngine.EventSystems.BaseEventData listener)
		{
			actions.ShockButtonTriggered();
		}

		internal void _003CSetup_003Eb__1(global::UnityEngine.EventSystems.BaseEventData listener)
		{
			actions.ShockButtonReleased();
		}
	}

	private static readonly global::UnityEngine.Vector3 NEW_HUD_OFFSET;

	private global::UnityEngine.UI.Image _shockerCooldown;

	private global::UnityEngine.UI.Button _shockerButton;

	private global::UnityEngine.Animator _shockerAnimator;

	private AttackSequenceDomain _attackSequenceDomain;

	private IShocker _shocker;

	private global::UnityEngine.Transform _shockerBoostVFX;

	private global::UnityEngine.Transform _shockerCooldownVFX;

	private global::UnityEngine.Transform _ultimateShockVFX;

	private bool _allowed;

	private CameraStateUIActions UIActions;

	private ScavengingStateUIActions ScavengeUIActions;

	public ShockerUIComponent(global::UnityEngine.GameObject mainCanvas)
		: base(mainCanvas)
	{
	}

	public void Setup(CameraStateUIActions actions, AttackSequenceDomain attackSequenceDomain, IShocker shocker)
	{
		_attackSequenceDomain = attackSequenceDomain;
		_shocker = shocker;
		UIActions = actions;
		ScavengeUIActions = null;
		ShockerUIComponent._003C_003Ec__DisplayClass10_0 _003C_003Ec__DisplayClass10_ = new ShockerUIComponent._003C_003Ec__DisplayClass10_0();
		_003C_003Ec__DisplayClass10_.actions = actions;
		global::UnityEngine.EventSystems.EventTrigger eventTrigger = _shockerButton.gameObject.AddComponent<global::UnityEngine.EventSystems.EventTrigger>();
		global::UnityEngine.EventSystems.EventTrigger.Entry entry = new global::UnityEngine.EventSystems.EventTrigger.Entry();
		entry.eventID = global::UnityEngine.EventSystems.EventTriggerType.PointerDown;
		entry.callback.AddListener(_003C_003Ec__DisplayClass10_._003CSetup_003Eb__0);
		global::UnityEngine.EventSystems.EventTrigger.Entry entry2 = new global::UnityEngine.EventSystems.EventTrigger.Entry();
		entry2.eventID = global::UnityEngine.EventSystems.EventTriggerType.PointerUp;
		entry2.callback.AddListener(_003C_003Ec__DisplayClass10_._003CSetup_003Eb__1);
		eventTrigger.triggers.Add(entry);
		eventTrigger.triggers.Add(entry2);
		_allowed = true;
		if (!_attackSequenceDomain.IsLegacyAnimatronic())
		{
			_shocker.SetShockerOffset(global::UnityEngine.Vector3.zero);
		}
	}

	public void Setup(ScavengingStateUIActions actions, AttackSequenceDomain attackSequenceDomain, IShocker shocker)
	{
		_attackSequenceDomain = attackSequenceDomain;
		_shocker = shocker;
		UIActions = null;
		ScavengeUIActions = actions;
		ShockerUIComponent._003C_003Ec__DisplayClass10_0Scavenge _003C_003Ec__DisplayClass10_0Scavenge = new ShockerUIComponent._003C_003Ec__DisplayClass10_0Scavenge();
		_003C_003Ec__DisplayClass10_0Scavenge.actions = actions;
		global::UnityEngine.EventSystems.EventTrigger eventTrigger = _shockerButton.gameObject.AddComponent<global::UnityEngine.EventSystems.EventTrigger>();
		global::UnityEngine.EventSystems.EventTrigger.Entry entry = new global::UnityEngine.EventSystems.EventTrigger.Entry();
		entry.eventID = global::UnityEngine.EventSystems.EventTriggerType.PointerDown;
		entry.callback.AddListener(_003C_003Ec__DisplayClass10_0Scavenge._003CSetup_003Eb__0);
		global::UnityEngine.EventSystems.EventTrigger.Entry entry2 = new global::UnityEngine.EventSystems.EventTrigger.Entry();
		entry2.eventID = global::UnityEngine.EventSystems.EventTriggerType.PointerUp;
		entry2.callback.AddListener(_003C_003Ec__DisplayClass10_0Scavenge._003CSetup_003Eb__1);
		eventTrigger.triggers.Add(entry);
		eventTrigger.triggers.Add(entry2);
		_allowed = true;
		if (!_attackSequenceDomain.IsLegacyAnimatronic())
		{
			_shocker.SetShockerOffset(global::UnityEngine.Vector3.zero);
		}
	}

	protected override void CacheAndPopulateComponents()
	{
		_components = new ComponentContainer();
		global::System.Type[] onlyCacheTypes = new global::System.Type[3]
		{
			typeof(global::UnityEngine.Transform),
			typeof(global::UnityEngine.UI.Image),
			typeof(global::UnityEngine.UI.Button)
		};
		_components.CacheComponents(_root, onlyCacheTypes);
		_shockerCooldown = _components.TryGetComponent<global::UnityEngine.UI.Image>("Fill");
		_shockerButton = _components.TryGetComponent<global::UnityEngine.UI.Button>("Button_Shocker");
		_shockerAnimator = _shockerButton.gameObject.GetComponent<global::UnityEngine.Animator>();
	}

	public override void Update()
	{
		if (!_root.activeSelf)
		{
			return;
		}
		if (_shockerButton != null && _shocker != null)
		{
			if (_shocker.GetStatus() == ShockerStatus.ReadyToShock)
			{
				_shockerButton.interactable = !_attackSequenceDomain.IsDisruptionFullyActive();
			}
			else
			{
				_shockerButton.interactable = false;
			}
		}
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Space) || global::UnityEngine.Input.GetMouseButtonDown(0))
		{
			_shockerAnimator.SetTrigger("Pressed");
			if (UIActions != null)
			{
				UIActions.ShockButtonTriggered();
			}
			else
			{
				ScavengeUIActions.ShockButtonTriggered();
			}
		}
		if (global::UnityEngine.Input.GetKeyUp(global::UnityEngine.KeyCode.Space) || global::UnityEngine.Input.GetMouseButtonUp(0))
		{
			if (UIActions != null)
			{
				UIActions.ShockButtonReleased();
			}
			else
			{
				ScavengeUIActions.ShockButtonReleased();
			}
		}
		if (_shockerCooldown != null)
		{
			if (_shocker != null && _shocker.GetStatus() != ShockerStatus.NotEnoughBattery)
			{
				_shockerCooldown.fillAmount = _shocker.GetCooldownPercent();
			}
			else
			{
				_shockerCooldown.fillAmount = 0f;
			}
		}
	}

	public override void UpdateVisibility(bool isMaskFullyOff)
	{
		if (_attackSequenceDomain.GetEncounterUIConfig() == null)
		{
			_root.SetActive(value: false);
		}
		else if (!isMaskFullyOff)
		{
			_root.SetActive(value: false);
		}
		else if (MasterDomain.GetDomain().AttackSequenceDomain.GetEncounterDropsObjectsViewModel().IsDroppedObjectActive)
		{
			_root.SetActive(value: false);
		}
		else if (MasterDomain.GetDomain().DialogDomain._dialogShower.IsShowingDialog())
		{
			_root.SetActive(value: false);
		}
		else
		{
			_root.SetActive(_attackSequenceDomain.GetEncounterUIConfig().ShowShocker & _attackSequenceDomain.IsInEncounter());
		}
	}
}
