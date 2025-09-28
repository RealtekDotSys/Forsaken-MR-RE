public class FlashlightUIComponent : EncounterHUDComponent
{
	private global::UnityEngine.UI.Image _flashlightOn;

	private global::UnityEngine.UI.Image _flashlightCooldown;

	private global::UnityEngine.UI.Button _flashlighButton;

	private global::UnityEngine.Animator _flashlighAnimator;

	private AttackSequenceDomain _attackSequenceDomain;

	private IFlashlight _flashlight;

	private bool _allowed;

	public FlashlightUIComponent(global::UnityEngine.GameObject mainCanvas)
		: base(mainCanvas)
	{
	}

	public void Setup(CameraStateUIActions actions, AttackSequenceDomain attackSequenceDomain, IFlashlight flashlight)
	{
		_flashlighButton.onClick.RemoveAllListeners();
		_flashlighButton.onClick.AddListener(actions.FlashlightButtonTriggered);
		_attackSequenceDomain = attackSequenceDomain;
		_flashlight = flashlight;
		_allowed = true;
	}

	public void Setup(ScavengingStateUIActions actions, AttackSequenceDomain attackSequenceDomain, IFlashlight flashlight)
	{
		_flashlighButton.onClick.RemoveAllListeners();
		_flashlighButton.onClick.AddListener(actions.FlashlightButtonTriggered);
		_attackSequenceDomain = attackSequenceDomain;
		_flashlight = flashlight;
		_allowed = true;
	}

	protected override void CacheAndPopulateComponents()
	{
		_components = new ComponentContainer();
		global::System.Type[] onlyCacheTypes = new global::System.Type[2]
		{
			typeof(global::UnityEngine.UI.Image),
			typeof(global::UnityEngine.UI.Button)
		};
		_components.CacheComponents(_root, onlyCacheTypes);
		_flashlightOn = _components.TryGetComponent<global::UnityEngine.UI.Image>("FlashlightOn");
		_flashlightCooldown = _components.TryGetComponent<global::UnityEngine.UI.Image>("Fill");
		_flashlighButton = _components.TryGetComponent<global::UnityEngine.UI.Button>("Button_Flashlight");
		_flashlighAnimator = _flashlighButton.GetComponent<global::UnityEngine.Animator>();
	}

	public override void Update()
	{
		if (!_root.activeSelf)
		{
			return;
		}
		_flashlightOn.gameObject.SetActive(_flashlight.IsOn);
		_flashlighAnimator.SetBool("IsOn", _flashlight.IsOn);
		if (_flashlightCooldown != null)
		{
			if (_flashlight.HasEnoughBatteryToActivate())
			{
				_flashlightCooldown.fillAmount = _flashlight.GetCooldownPercent();
			}
			else
			{
				_flashlightCooldown.fillAmount = 0f;
			}
		}
		if (!_flashlight.IsOn && !_flashlight.CanTurnOn())
		{
			if (_flashlighButton != null)
			{
				_flashlighButton.interactable = false;
			}
			return;
		}
		if (_flashlighButton != null)
		{
			_flashlighButton.interactable = !_attackSequenceDomain.IsDisruptionFullyActive();
		}
		if (_flashlighButton.interactable && (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.LeftControl) || global::UnityEngine.Input.GetMouseButtonDown(1)))
		{
			_flashlighButton.onClick.Invoke();
		}
	}

	public override void UpdateVisibility(bool isMaskFullyOff)
	{
		if (!_flashlight.IsFlashlightAvailable)
		{
			_root.SetActive(value: false);
			return;
		}
		bool flag = _attackSequenceDomain.GetEncounterUIConfig() != null && _attackSequenceDomain.GetEncounterUIConfig().UseFlashlight;
		_root.SetActive(flag && isMaskFullyOff);
	}
}
