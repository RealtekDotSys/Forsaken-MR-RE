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

	private const int BETA_VERSION = 19;

	private void Start()
	{
		constantVariables = ConstantVariables.Instance;
		masterdatadownloader = constantVariables != null ? constantVariables.MasterDataDownloader : null;
		_assetBundleDownloader = constantVariables != null ? constantVariables.AssetBundleDownloader : null;
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
		// Defensive null checks to prevent NullReferenceException
		if (result == null)
		{
			global::UnityEngine.Debug.LogError("LoginResult is null in OnSuccess.");
			OnError(new global::PlayFab.PlayFabError { ErrorMessage = "LoginResult is null" });
			return;
		}

		if (result.AuthenticationContext == null)
		{
			global::UnityEngine.Debug.LogError("AuthenticationContext is null in LoginResult.");
			OnError(new global::PlayFab.PlayFabError { ErrorMessage = "AuthenticationContext is null" });
			return;
		}

		ServerDomain.AuthContext = result.AuthenticationContext;
		ServerDomain.IllumixAuthContext = new IllumixAuthenticationContext(result.AuthenticationContext);

		global::UnityEngine.Debug.Log("Successfully Logged In To PlayFab!");
		global::UnityEngine.Debug.Log(result.PlayFabId);

		if (playerID != null)
		{
			playerID.text = "PlayerID: " + result.PlayFabId;
			if (playerID.gameObject != null)
			{
				playerID.gameObject.SetActive(value: true);
			}
			else
			{
				global::UnityEngine.Debug.LogWarning("playerID.gameObject is null in OnSuccess.");
			}
		}
		else
		{
			global::UnityEngine.Debug.LogWarning("playerID is null in OnSuccess.");
		}

		// Set the PlayFab display name to "XERA UNITY PROJECT"
		var updateRequest = new global::PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest
		{
			DisplayName = "XERA UNITY PROJECT"
		};
		global::PlayFab.PlayFabClientAPI.UpdateUserTitleDisplayName(updateRequest,
			(updateResult) => {
				global::UnityEngine.Debug.Log("Display name set to XERA UNITY PROJECT");
				GetTitleData();
			},
			(error) => {
				global::UnityEngine.Debug.LogWarning("Failed to set display name: " + error.GenerateErrorReport());
				GetTitleData();
			}
		);
	}

	private void OnError(global::PlayFab.PlayFabError error)
	{
		global::UnityEngine.Debug.LogError("PlayFab Error.");
		if (error != null)
			global::UnityEngine.Debug.LogError(error.GenerateErrorReport());
		else
			global::UnityEngine.Debug.LogError("PlayFabError is null in OnError.");

		var domain = MasterDomain.GetDomain();
		if (domain != null && domain.ServerDomain != null && domain.ServerDomain.networkAvailabilityChecker != null)
		{
			domain.ServerDomain.networkAvailabilityChecker.UpdatedConnection(connection: false);
		}
		else
		{
			global::UnityEngine.Debug.LogWarning("NetworkAvailabilityChecker is null in OnError.");
		}
	}

	private void GetTitleData()
	{
		global::PlayFab.PlayFabClientAPI.GetTitleData(new global::PlayFab.ClientModels.GetTitleDataRequest(), OnTitleDataReceived, OnError);
	}

	private void OnTitleDataReceived(global::PlayFab.ClientModels.GetTitleDataResult result)
	{
		if (result == null || result.Data == null)
		{
			global::UnityEngine.Debug.LogError("GetTitleDataResult or its Data is null in OnTitleDataReceived.");
			OnError(new global::PlayFab.PlayFabError { ErrorMessage = "GetTitleDataResult or Data is null" });
			return;
		}

		int privateBetaOpen = 0;
		if (!result.Data.ContainsKey("PrivateBetaOpen") || !int.TryParse(result.Data["PrivateBetaOpen"], out privateBetaOpen))
		{
			global::UnityEngine.Debug.LogWarning("PrivateBetaOpen key missing or invalid in title data.");
		}

		if (privateBetaOpen != BETA_VERSION)
		{
			global::UnityEngine.Debug.LogWarning("PrivateBetaOpen version is " + privateBetaOpen + " (expected " + BETA_VERSION + ")");
			if (betaClosed != null)
			{
				betaClosed.SetActive(value: true);
			}
			else
			{
				global::UnityEngine.Debug.LogWarning("betaClosed GameObject is null in OnTitleDataReceived.");
			}
			// Continue anyway
		}

		if (result.Data.ContainsKey("MasterDataVersion"))
			int.TryParse(result.Data["MasterDataVersion"], out PlayFabMasterdataVersion);
		else
			global::UnityEngine.Debug.LogWarning("MasterDataVersion key missing in title data.");

		if (result.Data.ContainsKey("TOC"))
			PlayFabTOC = result.Data["TOC"];
		else
			global::UnityEngine.Debug.LogWarning("TOC key missing in title data.");

		if (result.Data.ContainsKey("DownloadURI"))
			PlayFabDownloadURI = result.Data["DownloadURI"];
		else
			global::UnityEngine.Debug.LogWarning("DownloadURI key missing in title data.");

		global::UnityEngine.Debug.Log("PlayFabMasterDataVersion = " + PlayFabMasterdataVersion);
		global::UnityEngine.Debug.Log("PlayFabDownloadURI = " + PlayFabDownloadURI);

		if (constantVariables != null)
			constantVariables.DownloadURI = PlayFabDownloadURI;
		else
			global::UnityEngine.Debug.LogWarning("constantVariables is null in OnTitleDataReceived.");

		LoadStage1();
	}

	public void LoadStage1()
	{
		if (constantVariables == null)
		{
			global::UnityEngine.Debug.LogError("constantVariables is null in LoadStage1.");
			return;
		}
		if (masterdatadownloader == null)
		{
			global::UnityEngine.Debug.LogError("masterdatadownloader is null in LoadStage1.");
			return;
		}

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
		if (masterdatadownloader == null)
		{
			global::UnityEngine.Debug.LogError("masterdatadownloader is null in IsPLISTDataNull.");
			return true;
		}
		if (masterdatadownloader.masterDataSections == null)
		{
			global::UnityEngine.Debug.LogError("masterdatadownloader.masterDataSections is null in IsPLISTDataNull.");
			return true;
		}

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

		if (constantVariables == null)
		{
			global::UnityEngine.Debug.LogError("constantVariables is null in LoadStage2.");
			return;
		}
		if (_assetBundleDownloader == null)
		{
			global::UnityEngine.Debug.LogError("_assetBundleDownloader is null in LoadStage2.");
			return;
		}

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
		if (loadingAnimator != null)
		{
			loadingAnimator.SetTrigger("Complete");
		}
		else
		{
			global::UnityEngine.Debug.LogWarning("loadingAnimator is null in LoadComplete.");
		}
	}

	public void WarningEnded()
	{
		var domain = MasterDomain.GetDomain();
		if (domain != null && domain.TheGameDomain != null && domain.TheGameDomain.gameDisplayChanger != null)
		{
			domain.TheGameDomain.gameDisplayChanger.RequestDisplayChange(GameDisplayData.DisplayType.map);
		}
		else
		{
			global::UnityEngine.Debug.LogWarning("gameDisplayChanger is null in WarningEnded.");
		}
	}
}
