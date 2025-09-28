public class ScavengingStateUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Player")]
	[global::UnityEngine.SerializeField]
	private PlayerController playerController;

	[global::UnityEngine.SerializeField]
	private Joystick playerMove;

	[global::UnityEngine.Header("UI")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject encounterHUDParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject maskParent;

	[global::UnityEngine.SerializeField]
	private HoldableButton maskButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject shockerParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject batteryParent;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<BatteryColorToValue> batteryColorToValues;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Color batterySurgeColor;

	private MasterDomain _masterDomain;

	private EventExposer _masterEventExposer;

	private ScavengingEncounterHUD _scavengingEncounterHUD;

	private void UpdateVisibility()
	{
		if (_masterDomain != null)
		{
			maskParent.SetActive(ShouldMaskBeVisible());
			if (_scavengingEncounterHUD != null)
			{
				_scavengingEncounterHUD.UpdateVisibility();
			}
		}
	}

	private bool ShouldMaskBeVisible()
	{
		try
		{
			return _masterDomain.AttackSequenceDomain.GetEncounterUIConfig().ShowMask;
		}
		catch
		{
			return false;
		}
	}

	private void UpdateValues()
	{
		if (maskParent.activeSelf)
		{
			UpdateMask();
		}
		if (_scavengingEncounterHUD != null)
		{
			_scavengingEncounterHUD.Update();
		}
	}

	private void UpdateMask()
	{
		_masterDomain.CameraEquipmentDomain.Mask.SetDesiredMaskState(maskButton.IsPressed || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftShift));
	}

	private void InitializeEncounterHUD(string cpuId)
	{
		InitializeHUDForEncounter();
	}

	private void OnAttackUIUpdated()
	{
		UpdateVisibility();
	}

	private void Awake()
	{
		_masterDomain = MasterDomain.GetDomain();
		_masterEventExposer = _masterDomain.eventExposer;
		_masterDomain.AttackSequenceDomain.UpdateAttackUI += OnAttackUIUpdated;
		InitializeHUD();
	}

	private void InitializeLegacyEncounterHUD()
	{
		_scavengingEncounterHUD.UpdateRoot(encounterHUDParent);
		_scavengingEncounterHUD.InitializeShockerComponent();
		_scavengingEncounterHUD.InitializeFlashlightComponent();
		_scavengingEncounterHUD.InitializeBatteryComponent(batteryColorToValues, batterySurgeColor);
		UpdateVisibility();
	}

	private void InitializeHUD()
	{
		encounterHUDParent.SetActive(value: false);
		_scavengingEncounterHUD = new ScavengingEncounterHUD(encounterHUDParent);
		_scavengingEncounterHUD.Setup(_masterDomain.AttackSequenceDomain, _masterDomain.CameraEquipmentDomain, base.gameObject.GetComponent<ScavengingStateUIActions>());
		InitializeLegacyEncounterHUD();
		_masterEventExposer.add_ScavengingEncounterAnimatronicInitialized(InitializeEncounterHUD);
	}

	private void InitializeHUDForEncounter()
	{
		encounterHUDParent.SetActive(value: false);
		InitializeLegacyEncounterHUD();
		_masterDomain.eventExposer.add_MaskStateChanged(MaskStateChanged);
	}

	private void MaskStateChanged(bool isMaskGoingOn, bool isMaskTransitionBeginning)
	{
		UpdateVisibility();
	}

	private void OnDestroy()
	{
		_scavengingEncounterHUD = null;
		_masterDomain.AttackSequenceDomain.UpdateAttackUI -= OnAttackUIUpdated;
		_masterEventExposer.remove_ScavengingEncounterAnimatronicInitialized(InitializeEncounterHUD);
		_masterDomain.eventExposer.remove_MaskStateChanged(MaskStateChanged);
		_masterEventExposer = null;
		_masterDomain = null;
	}

	private void Update()
	{
		UpdateVisibility();
		UpdateValues();
		UpdateMovement();
	}

	private void UpdateMovement()
	{
		playerController.MovementUpdate(playerMove.Horizontal, playerMove.Vertical);
	}
}
