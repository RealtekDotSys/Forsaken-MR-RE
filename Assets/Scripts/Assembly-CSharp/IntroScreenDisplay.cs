public class IntroScreenDisplay : IxDisplay
{
	private IntroScreen.IntroScreenDialogData data;

	private MasterDomain _masterDomain;

	private global::UnityEngine.RectTransform cardsParent;

	private global::UnityEngine.GameObject objectToRelease;

	private AssetCache assetCache;

	private global::UnityEngine.UI.Button startButton;

	private global::UnityEngine.RectTransform loadingIcon;

	private global::UnityEngine.UI.Button nextCardButton;

	public IntroScreenDisplay(PrefabInstance instance)
		: base(instance)
	{
	}

	public void Setup(IntroScreen.IntroScreenDialogData dialogData)
	{
		data = dialogData;
		TryCallHookups();
		_masterDomain.GameAssetManagementDomain.AssetCacheAccess.GetInterfaceAsync(AssetCacheReceived);
	}

	private void AssetCacheReceived(AssetCache cache)
	{
		assetCache = cache;
		if (data.attackProfile != null)
		{
			cache.LoadAsset<global::UnityEngine.GameObject>(data.attackProfile.IntroScreen.Bundle, data.attackProfile.IntroScreen.Asset, IntroAssetLoaded, IntroAssetFailed);
		}
		else if (data.scavengingAttackProfile != null)
		{
			cache.LoadAsset<global::UnityEngine.GameObject>(data.scavengingAttackProfile.IntroScreen.Bundle, data.scavengingAttackProfile.IntroScreen.Asset, IntroAssetLoaded, IntroAssetFailed);
		}
		else
		{
			global::UnityEngine.Debug.LogError("No available bundle/asset info for Intro Screen display.");
		}
	}

	private void IntroAssetFailed()
	{
		global::UnityEngine.Debug.LogError("Intro load asset failed");
	}

	private void IntroAssetLoaded(global::UnityEngine.GameObject obj)
	{
		objectToRelease = obj;
		global::UnityEngine.Object.Instantiate(obj, cardsParent).GetComponent<IntroScreenBehavior>().Activate(nextCardButton);
		nextCardButton.enabled = true;
	}

	protected override void CacheAndPopulateComponents()
	{
		global::System.Type[] onlyCacheTypes = new global::System.Type[4]
		{
			typeof(global::UnityEngine.RectTransform),
			typeof(global::TMPro.TextMeshProUGUI),
			typeof(global::UnityEngine.UI.Image),
			typeof(global::UnityEngine.UI.Button)
		};
		_components.CacheComponents(_root, onlyCacheTypes);
		_masterDomain = MasterDomain.GetDomain();
		cardsParent = _components.TryGetComponent<global::UnityEngine.RectTransform>("CardsParent");
		startButton = _components.TryGetComponent<global::UnityEngine.UI.Button>("CTA1");
		nextCardButton = _components.TryGetComponent<global::UnityEngine.UI.Button>("NextCardButton");
		loadingIcon = _components.TryGetComponent<global::UnityEngine.RectTransform>("LoadingIcon");
		TryCallHookups();
	}

	private void TryCallHookups()
	{
		if (!(startButton == null) && !(loadingIcon == null) && data != null)
		{
			data.hookupsCallback(startButton, loadingIcon.gameObject);
		}
	}

	public override void Teardown()
	{
		_masterDomain = null;
		data = null;
		assetCache.ReleaseAsset(objectToRelease);
		assetCache = null;
		base.Teardown();
	}
}
