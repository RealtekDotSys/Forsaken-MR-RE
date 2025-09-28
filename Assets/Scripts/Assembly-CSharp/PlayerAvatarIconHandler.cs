public class PlayerAvatarIconHandler
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public global::System.Action<global::UnityEngine.Sprite> iconCallback;

		public PlayerAvatarIconHandler _003C_003E4__this;

		internal void _003CGetAvatarProfileSprite_003Eb__0()
		{
			iconCallback(null);
		}
	}

	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public global::System.Collections.Generic.List<string> keys;

		internal int _003CGetAllAvatarIDs_003Eb__0(string a, string b)
		{
			if (keys.Contains(a))
			{
				if (keys.Contains(b))
				{
					return 0;
				}
				return -1;
			}
			return 1;
		}
	}

	private const string DEFAULT = "default";

	private IconLookup _iconLookup;

	private readonly ItemDefinitions _itemDefinitions;

	private readonly GameUIDomain _gameUIDomain;

	public PlayerAvatarIconHandler(GameAssetManagementDomain assetDomain, ItemDefinitions itemDefinitions, GameUIDomain gameUIDomain)
	{
		_itemDefinitions = itemDefinitions;
		_gameUIDomain = gameUIDomain;
		assetDomain.IconLookupAccess.GetInterfaceAsync(LookupReady);
	}

	private void LookupReady(IconLookup lookup)
	{
		_iconLookup = lookup;
	}

	public void GetAvatarProfileSprite(string avatarId, global::System.Action<global::UnityEngine.Sprite> iconCallback)
	{
		PlayerAvatarIconHandler._003C_003Ec__DisplayClass6_0 _003C_003Ec__DisplayClass6_ = new PlayerAvatarIconHandler._003C_003Ec__DisplayClass6_0();
		_003C_003Ec__DisplayClass6_.iconCallback = iconCallback;
		_003C_003Ec__DisplayClass6_._003C_003E4__this = this;
		if (_iconLookup != null)
		{
			ProfileAvatarData profileAvatarById = _itemDefinitions.GetProfileAvatarById((avatarId == null) ? "default" : avatarId);
			if (profileAvatarById == null)
			{
				global::UnityEngine.Debug.LogError("ProfileAvatarData is null for AvatarID " + ((avatarId == null) ? "default" : avatarId));
			}
			else
			{
				_iconLookup.GetIconFromAtlas(IconGroup.AvatarAtlases, profileAvatarById.Asset, profileAvatarById.SpriteAtlas, iconCallback, _003C_003Ec__DisplayClass6_._003CGetAvatarProfileSprite_003Eb__0);
			}
		}
	}

	public global::System.Collections.Generic.List<string> GetAllAvatarIDs()
	{
		global::System.Collections.Generic.List<string> second = new global::System.Collections.Generic.List<string>(_gameUIDomain.GameUIData.playerAvatarDataModel.UnlockedAvatarIDs);
		if (_itemDefinitions == null)
		{
			return null;
		}
		global::System.Collections.Generic.List<string> keys = new global::System.Collections.Generic.List<string>(_itemDefinitions.ProfileAvatars.Keys);
		return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.OrderBy(global::System.Linq.Enumerable.Intersect(keys, second), (string s) => keys.IndexOf(s)));
	}
}
