public class AssetBundleDownloader
{
	public Root DeserializedTOC;

	private ConstantVariables _constant;

	private LoadingManager loadingManager;

	private LoadingBarController loadingBarController;

	public AssetBundleDownloader(LoadingManager loading, ConstantVariables constant, LoadingBarController loadingBar)
	{
		_constant = constant;
		loadingManager = loading;
		loadingBarController = loadingBar;
	}

	public void DeserializeTOC(string jsonResponse)
	{
		DeserializedTOC = global::Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(jsonResponse);
		CoroutineHelper.StartCoroutine(BundleDownload());
	}

	public global::System.Collections.IEnumerator BundleDownload()
	{
		if (ProgressUpdaterDebug.text != null)
		{
			ProgressUpdaterDebug.text.text = "DOWNLOADING BUNDLES.";
		}
		int TOCLength = 0;
		foreach (Entry entry2 in DeserializedTOC.Entries)
		{
			if (entry2.DownloadOnStartup)
			{
				TOCLength++;
			}
		}
		foreach (Entry entry in DeserializedTOC.Entries)
		{
			while (!global::UnityEngine.Caching.ready)
			{
				yield return null;
			}
			if (_constant.UseStreamingAssets)
			{
				if (entry.DownloadOnStartup)
				{
					global::UnityEngine.Debug.Log("Telling GetBundle to get " + entry.BundleName);
					yield return GetBundle(entry.BundleName, 0);
				}
			}
			else if (entry.DownloadOnStartup)
			{
				global::UnityEngine.Debug.Log("Telling GetBundle to get " + entry.BundleName);
				yield return GetBundle(entry.BundleName, int.Parse(entry.WindowsBundleVersion));
			}
			loadingBarController.AddSliderValue(TOCLength);
		}
		MasterDomain.GetDomain().eventExposer.add_VirtualGoodsDataReceived(Done);
		MasterDomain.GetDomain().ServerDomain.playerDataRequester.GetPlayerData(PlayerDataError);
		MasterDomain.GetDomain().ServerDomain.getOwnedGoodsRequester.GetOwnedGoods();
		if (ProgressUpdaterDebug.text != null)
		{
			ProgressUpdaterDebug.text.text = "";
		}
	}

	private void Done(global::System.Collections.Generic.List<StoreContainer.StorefrontData> data)
	{
		MasterDomain.GetDomain().eventExposer.remove_VirtualGoodsDataReceived(Done);
		MasterDomain.GetDomain().eventExposer.OnAllOrtonBundlesDownloaded();
		loadingBarController.Complete();
		loadingManager.LoadComplete();
	}

	private void PlayerDataError(ServerData response)
	{
		global::UnityEngine.Debug.LogError("Couldnt get player data heres the error ig: " + response.JSON);
	}

	private global::System.Collections.IEnumerator GetBundle(string requestedBundle, int version)
	{
		string downloadURI = _constant.DownloadURI;
		string text;
		if (_constant.UseStreamingAssets)
		{
			global::UnityEngine.Debug.Log("Using streaming assets");
			text = "file://" + global::UnityEngine.Application.streamingAssetsPath + "/Bundles/Windows/" + version + "/" + requestedBundle;
		}
		else
		{
			global::UnityEngine.Debug.Log("Not using streaming assets.");
			text = downloadURI + "/bundles/Windows/" + version + "/" + requestedBundle;
		}
		global::UnityEngine.Debug.Log(text);
		yield return CoroutineHelper.StartCoroutine(AssetBundleManager.downloadAssetBundle(text, version, requestedBundle));
	}
}
