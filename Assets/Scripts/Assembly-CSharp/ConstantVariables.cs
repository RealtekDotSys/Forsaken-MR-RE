public class ConstantVariables : global::UnityEngine.MonoBehaviour
{
	public static ConstantVariables Instance;

	[global::UnityEngine.HideInInspector]
	public int ActiveMasterDataVersion;

	[global::UnityEngine.HideInInspector]
	public string DownloadURI = "null";

	[global::UnityEngine.Header("Debug Settings")]
	public int SpecificRevision;

	public bool UseStreamingAssets;

	public MasterDataDownloadCacheDeserialize MasterDataDownloader;

	public AssetBundleDownloader AssetBundleDownloader;

	private LoadingManager _loadingManager;

	private void Awake()
	{
		Instance = this;
		global::UnityEngine.Caching.compressionEnabled = false;
		ActiveMasterDataVersion = global::UnityEngine.PlayerPrefs.GetInt("MasterDataVersion");
		_loadingManager = global::UnityEngine.GameObject.Find("LoadingManager").GetComponent<LoadingManager>();
		MasterDataDownloadCacheDeserialize masterDataDownloader = new MasterDataDownloadCacheDeserialize(_loadingManager, this);
		MasterDataDownloader = masterDataDownloader;
		LoadingBarController component = global::UnityEngine.GameObject.Find("LoadingScreen").GetComponent<LoadingBarController>();
		AssetBundleDownloader assetBundleDownloader = new AssetBundleDownloader(_loadingManager, this, component);
		AssetBundleDownloader = assetBundleDownloader;
	}

	private void Start()
	{
		_loadingManager.Login();
	}
}
