public class EntityScanDisplay : IxDisplay
{
	private const string LKEY_SCAN_TEXT = "map_interaction_scanning";

	private EventExposer _eventExposer;

	private global::System.Action _onCompleteCallback;

	private AssetCache _assetCache;

	private global::UnityEngine.UI.Button _fullScreenButton;

	private global::UnityEngine.UI.Image _scanImageFrame;

	private global::TMPro.TextMeshPro _scanningText;

	public EntityScanDisplay(PrefabInstance instance)
		: base(instance)
	{
	}

	public void Setup(global::System.Action onCompleteCallback)
	{
		_onCompleteCallback = onCompleteCallback;
		_scanningText.text = LocalizationDomain.Instance.Localization.GetLocalizedString("map_interaction_scanning", "Scanning... (unlocalized)");
		MasterDomain.GetDomain().MasterDataDomain.GetAccessToData.GetConfigDataEntryAsync(OnMasterDataConfigEntryLoaded);
	}

	public override void Teardown()
	{
		base.Teardown();
		_onCompleteCallback = null;
		_fullScreenButton = null;
	}

	protected override void CacheAndPopulateComponents()
	{
		global::System.Type[] onlyCacheTypes = new global::System.Type[3]
		{
			typeof(global::UnityEngine.UI.Button),
			typeof(global::UnityEngine.UI.Image),
			typeof(global::TMPro.TextMeshPro)
		};
		_components.CacheComponents(_root, onlyCacheTypes);
		_fullScreenButton = _components.TryGetComponent<global::UnityEngine.UI.Button>("FullScreenButton");
		_fullScreenButton.onClick.AddListener(OnFullScreenClicked);
		_fullScreenButton.interactable = false;
		_scanImageFrame = _components.TryGetComponent<global::UnityEngine.UI.Image>("Animation");
		_scanningText = _components.TryGetComponent<global::TMPro.TextMeshPro>("Text");
	}

	private void OnMasterDataConfigEntryLoaded(CONFIG_DATA.Root e)
	{
		CoroutineHelper.StartCoroutine(WaitToDismiss((float)e.Entries[0].MapEntities.Interaction.ScanningDuration));
	}

	private global::System.Collections.IEnumerator WaitToDismiss(float seconds)
	{
		yield return new global::UnityEngine.WaitForSeconds(seconds);
		if (_onCompleteCallback != null)
		{
			_onCompleteCallback();
		}
		ForceClose();
		yield return null;
	}

	private void OnFullScreenClicked()
	{
	}
}
