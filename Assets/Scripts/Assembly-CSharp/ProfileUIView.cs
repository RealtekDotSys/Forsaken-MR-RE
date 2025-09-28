public class ProfileUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Hookups")]
	private DialogHandler_Profile dialogHandler_Profile;

	[global::UnityEngine.SerializeField]
	private ProfileUIActions profileUIActions;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<TabParentKeyValue> tabParents;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("PanelTop Button HighlightToggles")]
	private HighlightToggle profileButtonHighlightToggle;

	[global::UnityEngine.SerializeField]
	private HighlightToggle friendsButtonHighlightToggle;

	[global::UnityEngine.SerializeField]
	private HighlightToggle leaderboardButtonHighlightToggle;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Profile")]
	private global::TMPro.TextMeshProUGUI userNameText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI levelText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI experienceText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI bonusXPText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI encountersText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI winsText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Slider xpSlider;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject bonusXPGroup;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI currentStreakNumText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TMP_InputField nameInputField;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject errorPanel;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI errorMessage;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform avatarCellParent;

	[global::UnityEngine.SerializeField]
	private AvatarCell avatarCellPrefab;

	[global::UnityEngine.SerializeField]
	private AvatarCell mainAvatarImage;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Friends List")]
	private global::UnityEngine.Transform friendsCellParent;

	[global::UnityEngine.SerializeField]
	private FriendRemoveCell friendsCellPrefab;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI numInvitesText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button inviteFriendButton;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Discord")]
	private global::UnityEngine.UI.RawImage discordProfileRawImage;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI discordUsernameText;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Friend Code")]
	private global::UnityEngine.GameObject friendCodePanel;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI generatedFriendCode;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TMP_InputField enteredFriendCode;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI addFriendDefaultText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI addFriendErrorText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button[] addFriendButtons;

	private MasterDomain _masterDomain;

	private MenuTabsHandler menuTabsHandler;

	private ProfileTabHandler profileTabHandler;

	private ProfileTabData profileTabData;

	private AvatarSelectionHandler avatarSelectionHandler;

	private FriendsListTabHandler friendsListTabHandler;

	private FriendsListTabData friendsListTabData;

	private FriendCodeModel friendCodeModel;

	private DiscordTabHandler discordTabHandler;

	private DiscordTabData discordTabData;

	private readonly string[] UserNameSetErrorStrings;

	private readonly string NumInvitesLocString;

	private void EventExposer_PersonalFriendCodeUpdate(string personalFriendCode)
	{
		generatedFriendCode.text = personalFriendCode;
		dialogHandler_Profile.DismissAddFriendDialog();
	}

	private void EventExposer_PlayerProfileUpdated(PlayerProfile obj)
	{
		UpdatePlayerAvatar();
	}

	private void EventExposer_DisplayNameObscenityFound(UserNameSetError error)
	{
		string text = UserNameSetErrorStrings[(int)error];
		StartCoroutine(ShowError(LocalizationDomain.Instance.Localization.GetLocalizedString(text, text)));
	}

	private void UpdateFazPassInfo()
	{
		numInvitesText.gameObject.SetActive(value: false);
		inviteFriendButton.interactable = false;
	}

	private void DidSwitchTabs(Tab tab)
	{
		switch (tab)
		{
		case Tab.profile:
			friendsListTabHandler.ClearFriendsFromList();
			break;
		case Tab.leaderboard:
			friendsListTabHandler.ClearFriendsFromList();
			break;
		}
	}

	public void ChooseLeaderboard(string name)
	{
	}

	private void WillSwitchTabs(Tab tab)
	{
		switch (tab)
		{
		case Tab.profile:
			UpdatePlayerAvatar();
			break;
		case Tab.friends:
			friendsListTabHandler.PopulateFriendsTab();
			UpdateNumInvitesText();
			break;
		case Tab.leaderboard:
			discordTabHandler.PopulateDiscordTab();
			break;
		}
	}

	private void InitializeData()
	{
		ProfileTabData profileTabData = new ProfileTabData();
		profileTabData.userNameText = userNameText;
		profileTabData.levelText = levelText;
		profileTabData.experienceText = experienceText;
		profileTabData.currentStreakNumText = currentStreakNumText;
		profileTabData.tMP_Input = nameInputField;
		profileTabData.avatarImage = mainAvatarImage;
		profileTabData.avatarIconLookup = _masterDomain.PlayerAvatarDomain.AvatarIconHandler;
		profileTabData.masterDomain = _masterDomain;
		profileTabData.profileUIActions = profileUIActions;
		profileTabData.bonusXPText = bonusXPText;
		profileTabData.encountersText = encountersText;
		profileTabData.winsText = winsText;
		profileTabData.xpSlider = xpSlider;
		profileTabData.bonusXPGroup = bonusXPGroup;
		this.profileTabData = profileTabData;
		FriendsListTabData friendsListTabData = new FriendsListTabData();
		friendsListTabData.cellParent = friendsCellParent;
		friendsListTabData.friendSelectCellPrefab = friendsCellPrefab;
		this.friendsListTabData = friendsListTabData;
		DiscordTabData discordTabData = new DiscordTabData();
		discordTabData.controller = global::UnityEngine.GameObject.Find("Constant Scripts").GetComponent<Discord_Controller>();
		discordTabData.profileRawImage = discordProfileRawImage;
		discordTabData.usernameText = discordUsernameText;
		this.discordTabData = discordTabData;
	}

	private global::System.Collections.IEnumerator ShowError(string message)
	{
		errorMessage.text = ((message == null) ? "" : message);
		errorPanel.SetActive(value: true);
		yield return new global::UnityEngine.WaitForSeconds(3f);
		errorPanel.SetActive(value: false);
		yield return null;
	}

	private void AddSubcriptions()
	{
		_masterDomain.eventExposer.add_PlayerProfileUpdated(EventExposer_PlayerProfileUpdated);
		_masterDomain.eventExposer.add_DisplayNameObscenityFound(EventExposer_DisplayNameObscenityFound);
		_masterDomain.eventExposer.add_PersonalFriendCodeUpdated(EventExposer_PersonalFriendCodeUpdate);
	}

	private void RemoveSubcriptions()
	{
		if (_masterDomain.eventExposer != null)
		{
			_masterDomain.eventExposer.remove_PlayerProfileUpdated(EventExposer_PlayerProfileUpdated);
			_masterDomain.eventExposer.remove_DisplayNameObscenityFound(EventExposer_DisplayNameObscenityFound);
			_masterDomain.eventExposer.remove_PersonalFriendCodeUpdated(EventExposer_PersonalFriendCodeUpdate);
		}
	}

	public string GetGeneratedFriendCode()
	{
		if (generatedFriendCode == null)
		{
			return "";
		}
		return generatedFriendCode.text;
	}

	public void SelectedAvatar()
	{
		UpdatePlayerAvatar();
		_masterDomain.eventExposer.OnPlayerAvatarUpdated();
		dialogHandler_Profile.DismissAvatarSelectDialog();
	}

	public void UpdatePlayerAvatar()
	{
		profileTabHandler.RefreshAvatarIcon();
	}

	public void ShowAvatarSelectionDialog()
	{
		avatarSelectionHandler.GenerateCells();
		dialogHandler_Profile.ShowAvatarSelectionDialog();
	}

	public void ShowTab(Tab tab)
	{
		menuTabsHandler.ShowTab(tab);
	}

	public void ShowErrorOnName()
	{
		StartCoroutine(ShowError(null));
	}

	public void ChangeTopPanelHighlight(Tab tab)
	{
		profileButtonHighlightToggle.SetHighlight(tab == Tab.profile);
		friendsButtonHighlightToggle.SetHighlight(tab == Tab.friends);
		leaderboardButtonHighlightToggle.SetHighlight(tab == Tab.leaderboard);
	}

	public void UpdateNumInvitesText()
	{
		numInvitesText.text = LocalizationDomain.Instance.Localization.GetLocalizedString(NumInvitesLocString, "Num invites left:") + " 0";
	}

	public void DisableInviteButton()
	{
		inviteFriendButton.interactable = false;
	}

	public void ShowFriendCodeScreen()
	{
		generatedFriendCode.text = friendCodeModel.MyCurrentFriendCode;
		friendCodePanel.SetActive(value: true);
	}

	public void DisplayAddFriendCodeError()
	{
		addFriendDefaultText.gameObject.SetActive(value: false);
		addFriendErrorText.gameObject.SetActive(value: true);
	}

	public void HideFriendCodeScreen()
	{
		friendCodePanel.SetActive(value: false);
	}

	public void ShowAddFriend()
	{
		addFriendDefaultText.gameObject.SetActive(value: true);
		addFriendErrorText.gameObject.SetActive(value: false);
		dialogHandler_Profile.ShowAddFriendAlert();
	}

	public void HideAddFriend()
	{
		dialogHandler_Profile.DismissAddFriendDialog();
	}

	public void ShowRefreshFriendCodeConfirmation()
	{
		dialogHandler_Profile.ShowRefreshFriendCodeConfirmation();
	}

	public void HideFriendCodeRefreshConfirmation()
	{
		dialogHandler_Profile.DismissFriendCodeChangeDialog();
	}

	public void SetAddFriendButtonsEnabled(bool enabled)
	{
		global::UnityEngine.UI.Button[] array = addFriendButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = enabled;
		}
	}

	public string GetRequestedFriendCode()
	{
		return enteredFriendCode.text;
	}

	private void Awake()
	{
		_masterDomain = MasterDomain.GetDomain();
		InitializeData();
	}

	private void Start()
	{
		menuTabsHandler = new MenuTabsHandler(tabParents, DidSwitchTabs, WillSwitchTabs);
		profileTabHandler = new ProfileTabHandler(profileTabData);
		avatarSelectionHandler = new AvatarSelectionHandler(avatarCellParent, avatarCellPrefab, profileUIActions.SelectAvatarCell, _masterDomain.PlayerAvatarDomain.AvatarIconHandler);
		friendsListTabHandler = new FriendsListTabHandler(_masterDomain, friendsListTabData, dialogHandler_Profile, _masterDomain.PlayerAvatarDomain.AvatarIconHandler);
		discordTabHandler = new DiscordTabHandler(discordTabData);
		friendCodeModel = _masterDomain.GameUIDomain.GameUIData.friendCodeModel;
		AddSubcriptions();
		UpdatePlayerAvatar();
		ShowTab(Tab.profile);
		UpdateFazPassInfo();
	}

	private void Update()
	{
		profileTabHandler.Update();
	}

	private void OnDestroy()
	{
		RemoveSubcriptions();
		friendsListTabHandler.OnDestroy();
		discordTabHandler.OnDestroy();
	}

	public ProfileUIView()
	{
		UserNameSetErrorStrings = new string[6] { "ui_error_user_rename_too_short", "ui_error_user_rename_too_long", "ui_error_user_rename_uncapitalized", "ui_error_user_rename_invalid_character_found", "ui_error_user_rename_profanity_found", "ui_error_user_rename_name_not_unique" };
		NumInvitesLocString = "ui_profile_btn_invite_numInvites";
	}
}
