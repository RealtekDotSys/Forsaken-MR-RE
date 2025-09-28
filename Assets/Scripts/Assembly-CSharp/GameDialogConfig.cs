public struct GameDialogConfig
{
	public string ResourcePath;

	public string AttachParentName;

	public global::UnityEngine.Events.UnityAction OnDismissCallback;

	public global::System.Collections.Generic.Dictionary<string, string> Strings;

	public global::System.Collections.Generic.Dictionary<string, global::UnityEngine.Events.UnityAction> ButtonCallbacks;

	public global::System.Collections.Generic.Dictionary<string, global::UnityEngine.Sprite> Sprites;

	public global::System.Collections.Generic.Dictionary<string, IconGroup> IconGroups;

	public global::System.Collections.Generic.Dictionary<string, string> SpriteNames;

	public global::System.Collections.Generic.KeyValuePair<string, int> NumberOfStars;

	public global::System.Collections.Generic.Dictionary<string, bool> GameObjectEnables;

	public global::System.Collections.Generic.Dictionary<string, bool> ButtonInteractables;

	public global::System.Collections.Generic.List<string> DismissButtons;

	public bool PlayAudioOnShow;

	public AudioEventName AudioEventName;

	public AudioMode AudioMode;

	public bool EnableAndroidBackButton;

	public global::System.Action<PrefabInstance> CustomCachingAction;
}
