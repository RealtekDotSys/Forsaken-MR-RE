public class SendSelectDialog : global::UnityEngine.MonoBehaviour
{
	private const string LocalizedStringId_Send = "ui_friend_cell_send_button_text";

	private const string LocalizedStringId_Sent = "ui_friend_cell_send_button_text";

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform listParent;

	[global::UnityEngine.SerializeField]
	private FriendSelectCell friendSelectCellPrefab;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject noFriendsTextObject;

	private MasterDomain _masterDomain;

	private global::System.Collections.Generic.List<FriendSelectCell> _sendListCells;

	private global::System.Collections.Generic.List<PlayerFriendsEntry> _friendsList;

	private global::System.Action<string> _sendSlotIdToUserId;

	private PlayerAvatarIconHandler _avatarIconLookup;

	private bool _enableSending;

	private void EventExposer_FriendListUpdated(global::System.Collections.Generic.List<PlayerFriendsEntry> obj)
	{
		_masterDomain.eventExposer.remove_FriendListUpdated(EventExposer_FriendListUpdated);
		PopulateSelectList(obj);
	}

	private bool SendingIsEnabled()
	{
		return _enableSending;
	}

	private void SendAnimatronicDelegate(string userId)
	{
		_enableSending = false;
		if (_sendSlotIdToUserId != null)
		{
			_sendSlotIdToUserId(userId);
		}
	}

	private void PopulateSelectList(global::System.Collections.Generic.List<PlayerFriendsEntry> friendsList)
	{
		_friendsList.Clear();
		_friendsList = friendsList;
		noFriendsTextObject.SetActive(friendsList.Count == 0);
		CreateFriendCells();
		PopulateFriendList();
	}

	private void PopulateFriendList()
	{
		foreach (PlayerFriendsEntry friends in _friendsList)
		{
			FriendSelectCell friendSelectCell = _sendListCells[_friendsList.IndexOf(friends)];
			FriendsListCellData friendsListCellData = new FriendsListCellData();
			friendsListCellData.playerFriendsEntry = friends;
			friendsListCellData.callback = SendAnimatronicDelegate;
			friendsListCellData.getSendingEnabled = SendingIsEnabled;
			friendSelectCell.SetData(friendsListCellData);
			_avatarIconLookup.GetAvatarProfileSprite(friends.avatarId, friendSelectCell.SetSprite);
		}
	}

	private void CreateFriendCells()
	{
		int num = _friendsList.Count - _sendListCells.Count;
		if (num >= 1)
		{
			do
			{
				_sendListCells.Add(global::UnityEngine.Object.Instantiate(friendSelectCellPrefab, listParent));
				num--;
			}
			while (num != 0);
		}
		else if (num != 0)
		{
			_sendListCells.RemoveRange(0, -num);
		}
	}

	private void QueryFriendsList()
	{
		_masterDomain.eventExposer.add_FriendListUpdated(EventExposer_FriendListUpdated);
		_masterDomain.ServerDomain.getPlayerFriendsRequester.GetPlayerFriends();
	}

	public void ReenableSendSelect()
	{
		_enableSending = true;
	}

	public void SetupSendSelectDialog(MasterDomain masterDomain, global::System.Action<string> sendSelectedSlotToUserId, PlayerAvatarIconHandler avatarIconLookup)
	{
		_masterDomain = masterDomain;
		_sendSlotIdToUserId = sendSelectedSlotToUserId;
		_avatarIconLookup = avatarIconLookup;
		QueryFriendsList();
	}

	public SendSelectDialog()
	{
		_sendListCells = new global::System.Collections.Generic.List<FriendSelectCell>();
		_friendsList = new global::System.Collections.Generic.List<PlayerFriendsEntry>();
		_enableSending = true;
	}
}
