public class SharedUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Dialog Handler")]
	private DialogHandler_Shared dialogHandler_Shared;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("FAB Menu")]
	private global::UnityEngine.UI.Button returnToMapButton;

	[global::UnityEngine.SerializeField]
	private float cameraAlertToggleTime;

	private MasterDomain _masterDomain;

	private EventExposer _masterEventExposer;

	private CurrencyBank _currencyBank;

	private int _jammerCost;

	private bool ShouldReturnToMapBeVisible()
	{
		if (!_masterDomain.AttackSequenceDomain.IsInEncounter())
		{
			return false;
		}
		if (_masterDomain.AttackSequenceDomain.IsEncounterPlayingOutro())
		{
			return false;
		}
		if (!_masterDomain.AttackSequenceDomain.CanReturnToMap())
		{
			return false;
		}
		return _masterDomain.CameraEquipmentDomain.Mask.IsMaskFullyOff();
	}

	private void BuildData()
	{
		_masterDomain.MasterDataDomain.GetAccessToData.GetConfigDataEntryAsync(OnMasterDataConfigEntryLoaded);
		_currencyBank = _masterDomain.TheGameDomain.bank;
	}

	private void BuildInboxBadgeBrokerData()
	{
	}

	private void OnMasterDataConfigEntryLoaded(CONFIG_DATA.Root e)
	{
		if (e.Entries[0].MapEntities != null)
		{
			_jammerCost = e.Entries[0].MapEntities.Interaction.JammerCost;
		}
		else
		{
			new global::System.ArgumentException("e.MapEntities is Null!", "e");
		}
	}

	public void ClosePopDown()
	{
	}

	public bool ShouldJammerBeInteractable()
	{
		return _currencyBank.CanAfford(Currency.CurrencyType.HardCurrency, _jammerCost);
	}

	public string GetJammerCost()
	{
		return _jammerCost.ToString();
	}

	private void Awake()
	{
		_masterDomain = MasterDomain.GetDomain();
		_masterEventExposer = _masterDomain.eventExposer;
		BuildData();
	}

	private void Update()
	{
		if (_masterDomain != null && _masterDomain.TheGameDomain != null)
		{
			returnToMapButton.gameObject.SetActive(ShouldReturnToMapBeVisible());
		}
	}

	private void LateUpdate()
	{
	}

	private void OnDestroy()
	{
	}

	public bool OnJammerClicked()
	{
		if (_jammerCost >= 1)
		{
			if (!ShouldJammerBeInteractable())
			{
				return false;
			}
			_currencyBank.AddCurrency("FAZ_TOKENS", -_jammerCost);
			return true;
		}
		return false;
	}
}
