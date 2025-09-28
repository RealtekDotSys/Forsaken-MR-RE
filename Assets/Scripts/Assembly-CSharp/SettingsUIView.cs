public class SettingsUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Hookups")]
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI screenSpaceText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI bloomText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI haywireIndicatorText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI denyFriendsText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI denySendsText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI purgeBundlesText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI aboutDialogVersionText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI aboutDialogModelText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI aboutPlayerIdText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI serverVersionText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI dataVersionText;

	private MasterDomain _masterDomain;

	private string vibrationOnString = "ON";

	private string vibrationOffString = "OFF";

	private string occlusionOnString = "ON";

	private string occlusionOffString = "OFF";

	private string facebookLogOutString;

	private string facebookLoginString = "Login with Facebook";

	private string vibrationNotSupportedString = "NOT SUPPORTED";

	private string pushNotifsAdjust = "ENABLED";

	private string pushNotifsEnable = "DISABLED";

	private string osSignedInString = "Signed in";

	private string osSignInString = "Sign in";

	private string helpUrlString;

	private string privacyPolicyUrlString;

	private string termsOfUseUrlString;

	[global::UnityEngine.SerializeField]
	public global::UnityEngine.UI.Slider ResolutionSlider;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI ResolutionSliderCurrentText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject FpsParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject ScreenSpaceButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button DenyFriendsButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button DenySendsButton;

	[global::UnityEngine.SerializeField]
	private HighlightToggle FpsButton_30;

	[global::UnityEngine.SerializeField]
	private HighlightToggle FpsButton_60;

	[global::UnityEngine.SerializeField]
	private HighlightToggle FpsButton_120;

	[global::UnityEngine.SerializeField]
	public HighlightToggle MaskControlButton_Button;

	[global::UnityEngine.SerializeField]
	public HighlightToggle MaskControlButton_Flick;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.Rendering.PostProcessing.PostProcessProfile> PostProcessingProfiles;

	private bool screenSpaceActive = true;

	private bool bloomActive = true;

	private bool haywireIndicatorsActive;

	private bool denyFriends;

	private bool denySends;

	private void UpdateAboutDialogText()
	{
		aboutDialogVersionText.text = global::UnityEngine.Application.version;
		aboutDialogModelText.text = global::UnityEngine.SystemInfo.deviceModel;
		LoginDomain loginDomain = _masterDomain.TheGameDomain.loginDomain;
		aboutPlayerIdText.text = loginDomain.playerProfile.userId;
		dataVersionText.text = global::UnityEngine.PlayerPrefs.GetInt("MasterDataVersion").ToString();
		serverVersionText.text = "1";
	}

	public string GetHelpUrl()
	{
		return helpUrlString;
	}

	public string GetPrivacyPolicyUrl()
	{
		return privacyPolicyUrlString;
	}

	public string GetTermsOfUseUrl()
	{
		return termsOfUseUrlString;
	}

	public void SetAccountOSLoginSectionState(bool isActive)
	{
	}

	private void UpdateButtonStates()
	{
		UpdateVibrationButtonText();
		UpdateOcclusionButtonText();
		UpdateOcclusionButtonVisibility();
		UpdateLoginButtons();
		UpdateScreenSpaceButtonText();
		UpdateBloomButtonText();
		UpdateHaywireIndicatorsButtonText();
		UpdateDenyFriendsButtonText();
		UpdateDenySendsButtonText();
		UpdatePurgeBundlesButtonText();
	}

	private void SetOsAccountSignInText()
	{
	}

	private void UpdateFacebookInteractivity()
	{
	}

	private void UpdateFacebookConnectedText()
	{
	}

	private void UpdateVibrationButtonText()
	{
	}

	private void UpdateOcclusionButtonText()
	{
	}

	private void CheckVibrationSupported()
	{
	}

	private void UpdateOcclusionButtonVisibility()
	{
	}

	private void UpdateScreenSpaceButtonText()
	{
		screenSpaceText.text = (screenSpaceActive ? occlusionOnString : occlusionOffString);
	}

	private void UpdateBloomButtonText()
	{
		bloomText.text = (bloomActive ? occlusionOnString : occlusionOffString);
	}

	private void UpdateHaywireIndicatorsButtonText()
	{
		haywireIndicatorText.text = (haywireIndicatorsActive ? occlusionOnString : occlusionOffString);
	}

	private void UpdateDenyFriendsButtonText()
	{
		denyFriendsText.text = (denyFriends ? occlusionOffString : occlusionOnString);
	}

	private void UpdateDenySendsButtonText()
	{
		denySendsText.text = (denySends ? occlusionOffString : occlusionOnString);
	}

	private void UpdatePurgeBundlesButtonText()
	{
		purgeBundlesText.text = "PURGE";
	}

	private void FetchLocalizedStrings()
	{
		SetVibrationButtonText();
		SetOcclusionButtonText();
		SetFacebookConnectedText();
		SetPushNotifText();
		SetOSAccountConnectedText();
	}

	private void SetPushNotifText()
	{
		LocalizationDomain.Instance.Localization.GetInterfaceAsync(SetPushNotifTextb__49_0);
	}

	private void SetFacebookConnectedText()
	{
		LocalizationDomain.Instance.Localization.GetInterfaceAsync(SetFacebookConnectedTextb__50_0);
	}

	private void SetOSAccountConnectedText()
	{
		LocalizationDomain.Instance.Localization.GetInterfaceAsync(SetOSAccountConnectedTextb__51_0);
	}

	private void SetVibrationButtonText()
	{
		LocalizationDomain.Instance.Localization.GetInterfaceAsync(SetVibrationButtonTextb__52_0);
	}

	private void SetOcclusionButtonText()
	{
		LocalizationDomain.Instance.Localization.GetInterfaceAsync(SetOcclusionButtonTextb__53_0);
	}

	private void Start()
	{
		_masterDomain = MasterDomain.GetDomain();
		_masterDomain.eventExposer.add_PlayerProfileUpdated(PlayerProfileUpdated);
		FetchLocalizedStrings();
		CheckVibrationSupported();
		UpdateAboutDialogText();
		UpdateLoginButtons();
		GetInitialResolution();
		SetInitialFrameRate();
		SetInitialPostProcessSettings();
		SetInitialAccessibilitySettings();
		SetInitialSocialSettings();
		SetInitialMaskOption();
	}

	private void OnDestroy()
	{
		_masterDomain.eventExposer.remove_PlayerProfileUpdated(PlayerProfileUpdated);
	}

	private void PlayerProfileUpdated(PlayerProfile profile)
	{
		denyFriends = !profile.friendAddsEnabled;
		denySends = !profile.friendSendsEnabled;
		DenySendsButton.interactable = true;
		DenyFriendsButton.interactable = true;
	}

	public void UpdateLoginButtons()
	{
		UpdateFacebookConnectedText();
		UpdateFacebookInteractivity();
		UpdateOsAccountButton();
	}

	public void UpdateOsAccountButton()
	{
		SetOsAccountSignInText();
		UpdateLoginButtonVisibilityAndInteractivity();
	}

	private void UpdateLoginButtonVisibilityAndInteractivity()
	{
	}

	private void UpdateOsButtonVisibility(bool isAccountLinked)
	{
	}

	private void UpdateOSButtonsInteractivity(bool interactivity)
	{
	}

	private void GetInitialResolution()
	{
		int num = global::UnityEngine.PlayerPrefs.GetInt("ResolutionMultiplier");
		UpdateResolutionSliderText((float)num / 10f);
		ResolutionSlider.value = num;
		QualitySetter.Instance.SetResolutionMultiplier(num);
	}

	public void UpdateResolutionSliderText(float value)
	{
		ResolutionSliderCurrentText.text = value + "x";
	}

	private void SetInitialFrameRate()
	{
		FpsParent.SetActive(value: false);
	}

	private void SetInitialMaskOption()
	{
		string text = global::UnityEngine.PlayerPrefs.GetString("MaskControl");
		if (!(text == "button"))
		{
			if (text == "flick")
			{
				MaskControlButton_Flick.SetHighlightAndOtherCellsHighlightState(value: true);
			}
		}
		else
		{
			MaskControlButton_Button.SetHighlightAndOtherCellsHighlightState(value: true);
		}
	}

	private void SetInitialPostProcessSettings()
	{
		if (global::UnityEngine.PlayerPrefs.HasKey("Bloom"))
		{
			bloomActive = global::UnityEngine.PlayerPrefs.GetInt("Bloom") == 1;
		}
		foreach (global::UnityEngine.Rendering.PostProcessing.PostProcessProfile postProcessingProfile in PostProcessingProfiles)
		{
			global::UnityEngine.Rendering.PostProcessing.Bloom outSetting = null;
			if (postProcessingProfile.TryGetSettings<global::UnityEngine.Rendering.PostProcessing.Bloom>(out outSetting))
			{
				outSetting.active = bloomActive;
			}
		}
		if (global::UnityEngine.PlayerPrefs.HasKey("ScreenSpaceReflections"))
		{
			screenSpaceActive = global::UnityEngine.PlayerPrefs.GetInt("ScreenSpaceReflections") == 1;
		}
		foreach (global::UnityEngine.Rendering.PostProcessing.PostProcessProfile postProcessingProfile2 in PostProcessingProfiles)
		{
			global::UnityEngine.Rendering.PostProcessing.ScreenSpaceReflections outSetting2 = null;
			if (postProcessingProfile2.TryGetSettings<global::UnityEngine.Rendering.PostProcessing.ScreenSpaceReflections>(out outSetting2))
			{
				outSetting2.active = screenSpaceActive;
			}
		}
	}

	public void ToggleScreenSpaceReflections()
	{
		screenSpaceActive = !screenSpaceActive;
		foreach (global::UnityEngine.Rendering.PostProcessing.PostProcessProfile postProcessingProfile in PostProcessingProfiles)
		{
			global::UnityEngine.Rendering.PostProcessing.ScreenSpaceReflections outSetting = null;
			if (postProcessingProfile.TryGetSettings<global::UnityEngine.Rendering.PostProcessing.ScreenSpaceReflections>(out outSetting))
			{
				outSetting.active = screenSpaceActive;
			}
		}
		global::UnityEngine.PlayerPrefs.SetInt("ScreenSpaceReflections", screenSpaceActive ? 1 : 0);
		global::UnityEngine.PlayerPrefs.Save();
	}

	public void ToggleBloom()
	{
		bloomActive = !bloomActive;
		foreach (global::UnityEngine.Rendering.PostProcessing.PostProcessProfile postProcessingProfile in PostProcessingProfiles)
		{
			global::UnityEngine.Rendering.PostProcessing.Bloom outSetting = null;
			if (postProcessingProfile.TryGetSettings<global::UnityEngine.Rendering.PostProcessing.Bloom>(out outSetting))
			{
				outSetting.active = bloomActive;
			}
		}
		global::UnityEngine.PlayerPrefs.SetInt("Bloom", bloomActive ? 1 : 0);
		global::UnityEngine.PlayerPrefs.Save();
	}

	private void SetInitialAccessibilitySettings()
	{
		if (global::UnityEngine.PlayerPrefs.HasKey("HaywireIndicators"))
		{
			haywireIndicatorsActive = global::UnityEngine.PlayerPrefs.GetInt("HaywireIndicators") == 1;
		}
	}

	public void ToggleHaywireIndicators()
	{
		haywireIndicatorsActive = !haywireIndicatorsActive;
		global::UnityEngine.PlayerPrefs.SetInt("HaywireIndicators", haywireIndicatorsActive ? 1 : 0);
		global::UnityEngine.PlayerPrefs.Save();
	}

	private void SetInitialSocialSettings()
	{
		denyFriends = !MasterDomain.GetDomain().TheGameDomain.loginDomain.playerProfile.friendAddsEnabled;
		denySends = !MasterDomain.GetDomain().TheGameDomain.loginDomain.playerProfile.friendSendsEnabled;
	}

	public void ToggleDenyFriends()
	{
		DenyFriendsButton.interactable = false;
		_masterDomain.ServerDomain.toggleFriendAddsRequester.ToggleFriendsAdd(denyFriends);
	}

	public void ToggleDenySends()
	{
		DenySendsButton.interactable = false;
		_masterDomain.ServerDomain.toggleFriendSendsRequester.ToggleFriendSends(denySends);
	}

	private void Update()
	{
		UpdateButtonStates();
	}

	public SettingsUIView()
	{
		occlusionOnString = "ON";
		vibrationOnString = "ON";
		vibrationOffString = "OFF";
		occlusionOffString = "OFF";
		facebookLogOutString = "Log Out";
		facebookLoginString = "Login with Facebook";
		vibrationNotSupportedString = "NOT SUPPORTED";
		pushNotifsAdjust = "ENABLED";
		pushNotifsEnable = "DISABLED";
		osSignedInString = "Signed in";
		osSignInString = "Sign in";
	}

	private void SetPushNotifTextb__49_0(Localization localization)
	{
		pushNotifsAdjust = localization.GetLocalizedString("ui_settings_push_notifs_enabled", pushNotifsAdjust);
		pushNotifsEnable = localization.GetLocalizedString("ui_settings_push_notifs_disabled", pushNotifsEnable);
	}

	private void SetFacebookConnectedTextb__50_0(Localization localization)
	{
		facebookLogOutString = localization.GetLocalizedString("ui_settings_logout_button_text", facebookLogOutString);
		facebookLoginString = localization.GetLocalizedString("ui_settings_sign_in_with_facebook", facebookLoginString);
	}

	private void SetOSAccountConnectedTextb__51_0(Localization localization)
	{
		osSignedInString = localization.GetLocalizedString("ui_settings_logout_button_text", osSignedInString);
		osSignInString = localization.GetLocalizedString("ui_settings_sign_in_with_Google", osSignInString);
	}

	private void SetVibrationButtonTextb__52_0(Localization localization)
	{
		vibrationOnString = localization.GetLocalizedString("ui_settings_vibration_on", vibrationOnString);
		vibrationOffString = localization.GetLocalizedString("ui_settings_vibration_off", vibrationOffString);
		vibrationNotSupportedString = localization.GetLocalizedString("ui_settings_vibration_not_supported", vibrationNotSupportedString);
	}

	private void SetOcclusionButtonTextb__53_0(Localization localization)
	{
		occlusionOnString = localization.GetLocalizedString("ui_settings_depth_api_on", occlusionOnString);
		occlusionOffString = localization.GetLocalizedString("ui_settings_depth_api_off", occlusionOffString);
	}
}
