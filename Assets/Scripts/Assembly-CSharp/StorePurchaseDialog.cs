public class StorePurchaseDialog : global::UnityEngine.MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass21_0
	{
		public global::System.Action<string> callback;

		internal void _003CFetchLocalization_003Eb__0(Localization localization)
		{
			callback(localization.GetLocalizedString("ui_workshop_dialogs_cancel", "ui_workshop_dialogs_cancel"));
		}
	}

	private sealed class _003C_003Ec__DisplayClass22_0
	{
		public string eventCurrencyIcon;

		public StorePurchaseDialog _003C_003E4__this;

		public global::System.Action<global::UnityEngine.Sprite> _003C_003E9__1;

		internal void _003CInitializeEventCurrencyIcon_003Eb__0(IconLookup lookup)
		{
			lookup.GetIcon(IconGroup.Store, eventCurrencyIcon, _003CInitializeEventCurrencyIcon_003Eb__1);
		}

		internal void _003CInitializeEventCurrencyIcon_003Eb__1(global::UnityEngine.Sprite sprite)
		{
			_003C_003E4__this._purchaseButtonEventCurrencyIcon.sprite = sprite;
		}
	}

	private const string CANCEL_TEXT = "ui_workshop_dialogs_cancel";

	private const float PURCHASE_TEXT_REFRESH_RATE = 0.5f;

	private const float PURCHASE_TIMEOUT_SECONDS = 60f;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button _purchaseButton;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _purchaseButtonText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image _purchaseButtonFazTokensIcon;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image _purchaseButtonPartsIcon;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image _purchaseButtonEventCurrencyIcon;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image _icon;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _panelFooter;

	private global::PaperPlaneTools.Alert _alert;

	private StoreDisplayData _data;

	private StoreDomain _storeDomain;

	private EventExposer _eventExposer;

	private global::System.Action<StoreDisplayData> _cantAffordCallback;

	private global::System.Collections.IEnumerator _loadingAnimationCoroutine;

	public void Initialize(StoreDomain storeDomain, EventExposer eventExposer)
	{
		_storeDomain = storeDomain;
		_eventExposer = eventExposer;
		_purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
	}

	public void SetData(StoreDisplayData data, global::System.Action<StoreDisplayData> cantAffordCallback)
	{
		_data = data;
		_cantAffordCallback = cantAffordCallback;
		FetchLocalization(SetupAlert);
	}

	public void SetSprite(global::UnityEngine.Sprite sprite)
	{
		_icon.overrideSprite = sprite;
	}

	private void SetupAlert(string cancelText)
	{
		if (_alert != null)
		{
			StopLoadingAnimationCoroutine();
			_alert.Dismiss();
			_alert = null;
		}
		_alert = new global::PaperPlaneTools.Alert();
		_alert.SetAdapter(base.gameObject.GetComponent<global::PaperPlaneTools.IAlertPlatformAdapter>());
		_alert.SetNegativeButton(cancelText);
		_alert.SetTitle(_data.storeData.Name);
		_alert.SetMessage(_data.storeData.Description);
		SetupPurchaseButton();
		_panelFooter.SetActive(value: true);
		AlertUtilities.ShowAlertWithAndroidBackButtonAction(_alert, SetupAlertb__19_0);
	}

	private void SetupPurchaseButton()
	{
		_purchaseButtonPartsIcon.gameObject.SetActive(_data.currency == Currency.CurrencyType.Parts);
		_purchaseButtonFazTokensIcon.gameObject.SetActive(_data.currency == Currency.CurrencyType.HardCurrency);
		_purchaseButtonEventCurrencyIcon.gameObject.SetActive(_data.currency == Currency.CurrencyType.EventCurrency);
		if (_data.currency == Currency.CurrencyType.EventCurrency)
		{
			InitializeEventCurrencyIcon();
		}
		_purchaseButton.interactable = true;
		_purchaseButtonText.text = _data.Cost.ToString();
	}

	private void FetchLocalization(global::System.Action<string> callback)
	{
		StorePurchaseDialog._003C_003Ec__DisplayClass21_0 _003C_003Ec__DisplayClass21_ = new StorePurchaseDialog._003C_003Ec__DisplayClass21_0();
		_003C_003Ec__DisplayClass21_.callback = callback;
		_003C_003Ec__DisplayClass21_._003CFetchLocalization_003Eb__0(LocalizationDomain.Instance.Localization);
	}

	private void InitializeEventCurrencyIcon()
	{
	}

	private void OnDisable()
	{
		StopLoadingAnimationCoroutine();
		if (_alert != null)
		{
			_alert.Dismiss();
		}
		_alert = null;
	}

	private void OnPurchaseButtonClicked()
	{
		if (!_storeDomain.CanAfford(_data))
		{
			if (_cantAffordCallback != null)
			{
				_cantAffordCallback(_data);
			}
			if (_alert != null)
			{
				_alert.Dismiss();
			}
		}
		else
		{
			_loadingAnimationCoroutine = AnimateLoadingMessage();
			StartCoroutine(AnimateLoadingMessage());
			_panelFooter.SetActive(value: false);
			_purchaseButton.interactable = false;
			StoreContainer storeContainer = _storeDomain.StoreContainer;
			storeContainer.OwnedGoodsReceived = (global::System.Action)global::System.Delegate.Combine(storeContainer.OwnedGoodsReceived, new global::System.Action(OnOwnedGoodsReceived));
			StoreDomain storeDomain = _storeDomain;
			storeDomain.PurchaseErrorCallback = (global::System.Action<string>)global::System.Delegate.Combine(storeDomain.PurchaseErrorCallback, new global::System.Action<string>(OnPurchaseError));
			_eventExposer.OnPurchaseRequestAudioInvoked(canAfford: true);
			_storeDomain.InitiatePurchase(_data);
		}
	}

	private void OnOwnedGoodsReceived()
	{
		StoreContainer storeContainer = _storeDomain.StoreContainer;
		storeContainer.OwnedGoodsReceived = (global::System.Action)global::System.Delegate.Remove(storeContainer.OwnedGoodsReceived, new global::System.Action(OnOwnedGoodsReceived));
		StopLoadingAnimationCoroutine();
		if (_alert != null)
		{
			_alert.Dismiss();
		}
	}

	private void OnPurchaseError(string errorMessage)
	{
		StoreDomain storeDomain = _storeDomain;
		storeDomain.PurchaseErrorCallback = (global::System.Action<string>)global::System.Delegate.Remove(storeDomain.PurchaseErrorCallback, new global::System.Action<string>(OnPurchaseError));
		StopLoadingAnimationCoroutine();
		if (_alert != null)
		{
			_alert.Dismiss();
		}
	}

	private void StopLoadingAnimationCoroutine()
	{
		if (_loadingAnimationCoroutine != null)
		{
			StopCoroutine(_loadingAnimationCoroutine);
			_loadingAnimationCoroutine = null;
		}
	}

	private global::System.Collections.IEnumerator AnimateLoadingMessage()
	{
		_purchaseButtonPartsIcon.gameObject.SetActive(value: false);
		_purchaseButtonFazTokensIcon.gameObject.SetActive(value: false);
		_purchaseButtonEventCurrencyIcon.gameObject.SetActive(value: false);
		_purchaseButtonText.text = ".";
		while (_alert != null)
		{
			yield return new global::UnityEngine.WaitForSeconds(0.5f);
			if (_purchaseButtonText.text == "...")
			{
				_purchaseButtonText.text = ".";
			}
			else
			{
				_purchaseButtonText.text += ".";
			}
		}
	}

	private void SetupAlertb__19_0()
	{
		_alert.Dismiss();
	}
}
