public class DialogHandler_Profile : global::UnityEngine.MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass17_0
	{
		public DialogHandler_Profile _003C_003E4__this;

		public string userId;

		public global::System.Action<FriendRemoveCell> ConfirmedRemoveFriend;

		public FriendRemoveCell friendRemoveCell;

		internal void _003CShowRemoveFriendDialog_003Eb__0()
		{
			_003C_003E4__this.RemoveFriend(userId, ConfirmedRemoveFriend, friendRemoveCell);
		}
	}

	public const string REMOVE_FRIEND_TITLE = "ui_delete_friend_confirm_text";

	public const string REMOVE_FRIEND_YES = "ui_delete_friend_confirm_yes";

	public const string REMOVE_FRIEND_NO = "ui_delete_friend_confirm_no";

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Dialog GameObjects")]
	private global::UnityEngine.GameObject avatarSelectionDialog;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject addFriendDialog;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject friendCodeChangeConfirmationDialog;

	[global::UnityEngine.SerializeField]
	private ProfileUIActions profileUIActions;

	private global::PaperPlaneTools.Alert avatarSelectionAlert;

	private global::PaperPlaneTools.Alert addFriendAlert;

	private global::PaperPlaneTools.Alert friendCodeChangeConfirmationAlert;

	private string _removeFriendTitle;

	private string _removeFriendYes;

	private string _removeFriendNo;

	private void RemoveFriend(string id, global::System.Action<FriendRemoveCell> ConfirmedRemoveFriend, FriendRemoveCell friendRemoveCell)
	{
		ConfirmedRemoveFriend(friendRemoveCell);
		profileUIActions.RemoveFriend(id);
	}

	public void ShowAvatarSelectionDialog()
	{
		AlertUtilities.ShowAlertWithAndroidBackButtonAction(avatarSelectionAlert, ShowAvatarSelectionDialogb__14_0);
	}

	public void ShowAddFriendAlert()
	{
		AlertUtilities.ShowAlertWithAndroidBackButtonAction(addFriendAlert, ShowAddFriendAlertb__15_0);
	}

	public void ShowRefreshFriendCodeConfirmation()
	{
		AlertUtilities.ShowAlertWithAndroidBackButtonAction(friendCodeChangeConfirmationAlert, ShowRefreshFriendCodeConfirmationb__16_0);
	}

	public void ShowRemoveFriendDialog(string userId, global::System.Action<FriendRemoveCell> ConfirmedRemoveFriend, FriendRemoveCell friendRemoveCell)
	{
		DialogHandler_Profile._003C_003Ec__DisplayClass17_0 _003C_003Ec__DisplayClass17_ = new DialogHandler_Profile._003C_003Ec__DisplayClass17_0();
		_003C_003Ec__DisplayClass17_._003C_003E4__this = this;
		_003C_003Ec__DisplayClass17_.userId = userId;
		_003C_003Ec__DisplayClass17_.ConfirmedRemoveFriend = ConfirmedRemoveFriend;
		_003C_003Ec__DisplayClass17_.friendRemoveCell = friendRemoveCell;
		GenericDialogData genericDialogData = new GenericDialogData();
		genericDialogData.title = _removeFriendTitle;
		genericDialogData.negativeButtonText = _removeFriendNo;
		genericDialogData.positiveButtonText = _removeFriendYes;
		genericDialogData.positiveButtonAction = _003C_003Ec__DisplayClass17_._003CShowRemoveFriendDialog_003Eb__0;
		MasterDomain.GetDomain().eventExposer.GenericDialogRequest(genericDialogData);
	}

	internal void DismissAvatarSelectDialog()
	{
		avatarSelectionAlert.Dismiss();
	}

	internal void DismissFriendCodeChangeDialog()
	{
		friendCodeChangeConfirmationAlert.Dismiss();
	}

	internal void DismissAddFriendDialog()
	{
		addFriendAlert.Dismiss();
	}

	private void Awake()
	{
		avatarSelectionAlert = new global::PaperPlaneTools.Alert();
		avatarSelectionAlert.SetAdapter(avatarSelectionDialog.GetComponent<global::PaperPlaneTools.IAlertPlatformAdapter>());
		addFriendAlert = new global::PaperPlaneTools.Alert();
		addFriendAlert.SetAdapter(addFriendDialog.GetComponent<global::PaperPlaneTools.IAlertPlatformAdapter>());
		friendCodeChangeConfirmationAlert = new global::PaperPlaneTools.Alert();
		friendCodeChangeConfirmationAlert.SetAdapter(friendCodeChangeConfirmationDialog.GetComponent<global::PaperPlaneTools.IAlertPlatformAdapter>());
		GetLocStrings();
	}

	private void GetLocStrings()
	{
		_removeFriendTitle = LocalizationDomain.Instance.Localization.GetLocalizedString("ui_delete_friend_confirm_text", "ui_delete_friend_confirm_text");
		_removeFriendYes = LocalizationDomain.Instance.Localization.GetLocalizedString("ui_delete_friend_confirm_yes", "ui_delete_friend_confirm_yes");
		_removeFriendNo = LocalizationDomain.Instance.Localization.GetLocalizedString("ui_delete_friend_confirm_no", "ui_delete_friend_confirm_no");
	}

	private void OnDestroy()
	{
		if (avatarSelectionAlert != null)
		{
			avatarSelectionAlert.Dismiss();
		}
		avatarSelectionAlert = null;
		if (addFriendAlert != null)
		{
			addFriendAlert.Dismiss();
		}
		addFriendAlert = null;
		if (friendCodeChangeConfirmationAlert != null)
		{
			friendCodeChangeConfirmationAlert.Dismiss();
		}
		friendCodeChangeConfirmationAlert = null;
	}

	private void ShowAvatarSelectionDialogb__14_0()
	{
		avatarSelectionAlert.Dismiss();
	}

	private void ShowAddFriendAlertb__15_0()
	{
		addFriendAlert.Dismiss();
	}

	private void ShowRefreshFriendCodeConfirmationb__16_0()
	{
		friendCodeChangeConfirmationAlert.Dismiss();
	}
}
