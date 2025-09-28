public class LoadingManager : global::UnityEngine.MonoBehaviour
{
	private MasterDataDownloadCacheDeserialize masterdatadownloader;

	private AssetBundleDownloader _assetBundleDownloader;

	private ConstantVariables constantVariables;

	[global::UnityEngine.HideInInspector]
	public int PlayFabMasterdataVersion;

	[global::UnityEngine.HideInInspector]
	public string PlayFabDownloadURI = "";

	[global::UnityEngine.HideInInspector]
	public string PlayFabTOC = "";

	[global::UnityEngine.Header("Editor Hookups")]
	public global::UnityEngine.GameObject betaClosed;

	public global::TMPro.TextMeshProUGUI playerID;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Animator loadingAnimator;

	private const int BETA_VERSION = 18;

	private void Start()
	{
		constantVariables = ConstantVariables.Instance;
		masterdatadownloader = constantVariables.MasterDataDownloader;
		_assetBundleDownloader = constantVariables.AssetBundleDownloader;
	}

	public void Login()
	{
		string text = "";
		if (!global::UnityEngine.PlayerPrefs.HasKey("ForsakenMR_DeviceID"))
		{
			global::UnityEngine.PlayerPrefs.SetString("ForsakenMR_DeviceID", global::UnityEngine.SystemInfo.deviceUniqueIdentifier);
			global::UnityEngine.PlayerPrefs.Save();
			text = global::UnityEngine.SystemInfo.deviceUniqueIdentifier;
		}
		else
		{
			text = global::UnityEngine.PlayerPrefs.GetString("ForsakenMR_DeviceID");
		}
		global::PlayFab.PlayFabClientAPI.LoginWithCustomID(new global::PlayFab.ClientModels.LoginWithCustomIDRequest
		{
			CustomId = text,
			CreateAccount = true
		}, OnSuccess, OnError);
	}

	private void OnSuccess(global::PlayFab.ClientModels.LoginResult result)
	{
		ServerDomain.AuthContext = result.AuthenticationContext;
		ServerDomain.IllumixAuthContext = new IllumixAuthenticationContext(result.AuthenticationContext);
		global::UnityEngine.Debug.Log("Successfully Logged In To PlayFab!");
		global::UnityEngine.Debug.Log(result.PlayFabId);
		playerID.text = "PlayerID: " + result.PlayFabId;
		playerID.gameObject.SetActive(value: true);
		GetTitleData();
	}

	private void OnError(global::PlayFab.PlayFabError error)
	{
		global::UnityEngine.Debug.LogError("PlayFab Error.");
		global::UnityEngine.Debug.LogError(error.GenerateErrorReport());
		MasterDomain.GetDomain().ServerDomain.networkAvailabilityChecker.UpdatedConnection(connection: false);
	}

	private void GetTitleData()
	{
		global::PlayFab.PlayFabClientAPI.GetTitleData(new global::PlayFab.ClientModels.GetTitleDataRequest(), OnTitleDataReceived, OnError);
	}

	private void OnTitleDataReceived(global::PlayFab.ClientModels.GetTitleDataResult result)
	{
		if (int.Parse(result.Data["PrivateBetaOpen"]) != 18)
		{
			if (betaClosed != null)
			{
				betaClosed.SetActive(value: true);
			}
			return;
		}
		PlayFabMasterdataVersion = int.Parse(result.Data["MasterDataVersion"]);
		PlayFabTOC = result.Data["TOC"];
		PlayFabDownloadURI = result.Data["DownloadURI"];
		global::UnityEngine.Debug.Log("PlayFabMasterDataVersion = " + PlayFabMasterdataVersion);
		global::UnityEngine.Debug.Log("PlayFabDownloadURI = " + PlayFabDownloadURI);
		constantVariables.DownloadURI = PlayFabDownloadURI;
		LoadStage1();
	}

	public void LoadStage1()
	{
		if (constantVariables.UseStreamingAssets)
		{
			global::UnityEngine.Debug.Log("StreamingAssets active, Asked for new masterdata!");
			masterdatadownloader.DownloadNewMasterData(PlayFabMasterdataVersion, constantVariables.DownloadURI);
		}
		else if (constantVariables.ActiveMasterDataVersion != PlayFabMasterdataVersion)
		{
			global::UnityEngine.Debug.Log("Saved masterdata version doesn't match server, Asked for new masterdata!");
			masterdatadownloader.DownloadNewMasterData(PlayFabMasterdataVersion, constantVariables.DownloadURI);
		}
		else if (IsPLISTDataNull())
		{
			global::UnityEngine.Debug.Log("Error with saved masterdata, Asked for new masterdata!");
			masterdatadownloader.DownloadNewMasterData(PlayFabMasterdataVersion, constantVariables.DownloadURI);
		}
		else
		{
			masterdatadownloader.DeserializeMasterData();
		}
	}

	public bool IsPLISTDataNull()
	{
		foreach (MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo key in masterdatadownloader.masterDataSections.Keys)
		{
			if (!global::UnityEngine.PlayerPrefs.HasKey(key.MasterDataType.ToString()))
			{
				global::UnityEngine.Debug.LogError("Missing Masterdata Key " + key.MasterDataType.ToString());
				return true;
			}
			if (string.IsNullOrEmpty(global::UnityEngine.PlayerPrefs.GetString(key.MasterDataType.ToString())))
			{
				global::UnityEngine.Debug.LogError("Missing Masterdata String " + key.MasterDataType.ToString());
				return true;
			}
		}
		return false;
	}

	public void LoadStage2(string streamingAssetsTOC)
	{
		global::UnityEngine.Debug.Log("Load thinks masterdata is deserialized!");
		global::UnityEngine.PlayerPrefs.SetString("TOC", PlayFabTOC);
		global::UnityEngine.PlayerPrefs.Save();
		if (constantVariables.UseStreamingAssets)
		{
			global::UnityEngine.PlayerPrefs.SetInt("MasterDataVersion", 0);
			global::UnityEngine.PlayerPrefs.Save();
			_assetBundleDownloader.DeserializeTOC(streamingAssetsTOC);
		}
		else
		{
			_assetBundleDownloader.DeserializeTOC(PlayFabTOC);
		}
	}

	public void LoadComplete()
	{
		loadingAnimator.SetTrigger("Complete");
	}

	public void WarningEnded()
	{
		MasterDomain.GetDomain().TheGameDomain.gameDisplayChanger.RequestDisplayChange(GameDisplayData.DisplayType.map);
	}
}
