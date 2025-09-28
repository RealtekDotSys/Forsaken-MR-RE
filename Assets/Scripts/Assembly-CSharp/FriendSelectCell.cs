public class FriendSelectCell : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button selectButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button sendButton;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI playerName;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image avatar;

	[global::UnityEngine.SerializeField]
	private HighlightToggle highlightToggle;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject sentPanel;

	private FriendsListCellData _data;

	private void UpdateText()
	{
		if (!(playerName == null) && _data != null && _data.playerFriendsEntry != null && _data.playerFriendsEntry.displayName != null)
		{
			playerName.text = _data.playerFriendsEntry.displayName;
		}
	}

	public void SetData(FriendsListCellData data)
	{
		_data = data;
		UpdateText();
	}

	public bool IsDataInstantiated()
	{
		return _data != null;
	}

	public void SetSprite(global::UnityEngine.Sprite sprite)
	{
		avatar.overrideSprite = sprite;
	}

	public void SendToFriend()
	{
		if (_data == null || sentPanel.activeSelf)
		{
			return;
		}
		sentPanel.SetActive(value: true);
		highlightToggle.SetHighlight(value: false);
		if (_data.callback != null)
		{
			if (_data.playerFriendsEntry == null)
			{
				_data.callback(null);
			}
			else
			{
				_data.callback(_data.playerFriendsEntry.userId);
			}
		}
	}

	private void Update()
	{
		if (_data != null)
		{
			selectButton.interactable = _data.getSendingEnabled();
			sendButton.interactable = _data.getSendingEnabled();
		}
	}

	private void OnDisable()
	{
		highlightToggle.SetHighlight(value: false);
		sentPanel.SetActive(value: false);
	}
}
