public class FriendRemoveCell : global::UnityEngine.MonoBehaviour, ICellInterface<FriendsRemoveCellData>
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

	private FriendsRemoveCellData _data;

	private void UpdateText()
	{
		if (!(playerName == null) && _data != null && _data.playerFriendsEntry != null && _data.playerFriendsEntry.displayName != null)
		{
			playerName.text = _data.playerFriendsEntry.displayName;
		}
	}

	public void RemoveFriend()
	{
		if (_data.callback != null)
		{
			_data.callback(_data.playerFriendsEntry.userId, this);
		}
	}

	public void SetData(FriendsRemoveCellData data)
	{
		_data = data;
		UpdateText();
	}

	public void SetSprite(global::UnityEngine.Sprite sprite)
	{
		avatar.overrideSprite = sprite;
	}

	private void OnDisable()
	{
		highlightToggle.SetHighlight(value: false);
		sentPanel.SetActive(value: false);
	}
}
