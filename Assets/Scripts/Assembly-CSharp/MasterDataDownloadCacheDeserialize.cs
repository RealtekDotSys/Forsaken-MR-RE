public class MasterDataDownloadCacheDeserialize
{
	public class MasterDataDownloadInfo
	{
		public global::System.Type MasterDataType;

		public global::System.Type DeserializeType;

		public MasterDataDownloadInfo(global::System.Type type, global::System.Type deserialize)
		{
			MasterDataType = type;
			DeserializeType = deserialize;
		}
	}

	private ConstantVariables _constant;

	private LoadingManager _loadingManager;

	private string _downloadURI;

	private global::System.Action OnMasterDataDeserialized;

	public bool IsDeserialized;

	public global::System.Collections.Generic.Dictionary<MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo, object> masterDataSections = new global::System.Collections.Generic.Dictionary<MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo, object>
	{
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(ATTACK_DATA), typeof(ATTACK_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(PLUSHSUIT_DATA), typeof(PLUSHSUIT_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(CPU_DATA), typeof(CPU_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(STATIC_DATA), typeof(STATIC_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(CONFIG_DATA), typeof(CONFIG_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(LOC_DATA), typeof(LOC_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(AUDIO_DATA), typeof(AUDIO_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(LOOT_STRUCTURE_DATA), typeof(LOOT_STRUCTURE_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(LOOT_PACKAGE_DATA), typeof(LOOT_PACKAGE_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(LOOT_TABLE_DATA), typeof(LOOT_TABLE_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(LOOT_ITEM_DATA), typeof(LOOT_ITEM_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(CRATE_INFO_DATA), typeof(CRATE_INFO_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(STORE_DATA), typeof(STORE_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(STORESECTIONS_DATA), typeof(STORESECTIONS_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(TROPHY_DATA), typeof(TROPHY_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(PROFILE_AVATAR_DATA), typeof(PROFILE_AVATAR_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(MODS_DATA), typeof(MODS_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(MODCATEGORIES_DATA), typeof(MODCATEGORIES_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(SUB_ENTITY_DATA), typeof(SUB_ENTITY_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(SCAVENGING_ATTACK_DATA), typeof(SCAVENGING_ATTACK_DATA.Root)),
			null
		},
		{
			new MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo(typeof(SCAVENGING_DATA), typeof(SCAVENGING_DATA.Root)),
			null
		}
	};

	private global::System.Collections.Generic.Dictionary<MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo, bool> downloadedSections = new global::System.Collections.Generic.Dictionary<MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo, bool>();

	public object GetMasterDataDeserialized(global::System.Type type)
	{
		foreach (MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo key in masterDataSections.Keys)
		{
			if (key.MasterDataType == type)
			{
				return masterDataSections[key];
			}
		}
		return null;
	}

	public MasterDataDownloadCacheDeserialize(LoadingManager loading, ConstantVariables constant)
	{
		_constant = constant;
		_loadingManager = loading;
	}

	public void add_OnMasterDataDeserialized(global::System.Action value)
	{
		OnMasterDataDeserialized = (global::System.Action)global::System.Delegate.Combine(OnMasterDataDeserialized, value);
	}

	public void remove_OnMasterDataDeserialized(global::System.Action value)
	{
		OnMasterDataDeserialized = (global::System.Action)global::System.Delegate.Remove(OnMasterDataDeserialized, value);
	}

	private void ClearCurrentMasterData()
	{
		foreach (MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo item in new global::System.Collections.Generic.List<MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo>(masterDataSections.Keys))
		{
			ClearMasterDataSection(item);
		}
		global::UnityEngine.Debug.Log("Cleared masterdata. This should be done before the /'Okay! I got it!/' Debug Log.");
	}

	private void ClearMasterDataSection(MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo info)
	{
		global::UnityEngine.PlayerPrefs.SetString(info.MasterDataType.ToString(), null);
		global::UnityEngine.PlayerPrefs.Save();
	}

	public void DownloadNewMasterData(int playfabversion, string uri)
	{
		ClearCurrentMasterData();
		_downloadURI = uri;
		global::UnityEngine.Debug.Log("Okay! I got it! I'll download!");
		global::System.Collections.Generic.List<MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo> list = new global::System.Collections.Generic.List<MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo>(masterDataSections.Keys);
		foreach (MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo item in list)
		{
			downloadedSections.Add(item, value: false);
			masterDataSections[item] = null;
		}
		foreach (MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo item2 in list)
		{
			CoroutineHelper.StartCoroutine(DownloadMasterDataSection(item2, playfabversion));
		}
	}

	private void SectionComplete(int masterdata)
	{
		foreach (MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo key in masterDataSections.Keys)
		{
			if (!downloadedSections[key])
			{
				return;
			}
		}
		global::UnityEngine.Debug.Log("All sections downloaded.");
		if (!_loadingManager.IsPLISTDataNull())
		{
			_constant.ActiveMasterDataVersion = masterdata;
			global::UnityEngine.Debug.Log("Set ActiveMasterDataVersion.");
			global::UnityEngine.PlayerPrefs.SetInt("MasterDataVersion", masterdata);
			global::UnityEngine.PlayerPrefs.Save();
			global::UnityEngine.Debug.Log("MasterData is installed boss.");
			DeserializeMasterData();
		}
		else
		{
			global::UnityEngine.Debug.LogError("Masterdata section didnt download. error. error. error.");
			MasterDomain.GetDomain().ServerDomain.networkAvailabilityChecker.UpdatedConnection(connection: false);
		}
	}

	private global::System.Collections.IEnumerator DownloadMasterDataSection(MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo downloadInfo, int masterdata)
	{
		global::System.Type type = downloadInfo.MasterDataType;
		yield return new global::UnityEngine.WaitForSeconds(1f);
		if (ProgressUpdaterDebug.text != null)
		{
			ProgressUpdaterDebug.text.text = "DOWNLOADING " + type.ToString();
		}
		string text = masterdata.ToString();
		string masterdataURI = ((!_constant.UseStreamingAssets) ? (_downloadURI + "/masterdata/" + text + "/" + type.ToString()) : ("file://" + global::UnityEngine.Application.streamingAssetsPath + "/MasterData/0/" + type.ToString()));
		using global::UnityEngine.Networking.UnityWebRequest request = global::UnityEngine.Networking.UnityWebRequest.Get(masterdataURI);
		yield return request.SendWebRequest();
		global::UnityEngine.Debug.Log(masterdataURI);
		if (request.result == global::UnityEngine.Networking.UnityWebRequest.Result.ProtocolError || request.result == global::UnityEngine.Networking.UnityWebRequest.Result.ConnectionError || request.result == global::UnityEngine.Networking.UnityWebRequest.Result.DataProcessingError)
		{
			global::UnityEngine.Debug.LogError(request.error);
			MasterDomain.GetDomain().ServerDomain.networkAvailabilityChecker.UpdatedConnection(connection: false);
		}
		else
		{
			global::UnityEngine.Debug.Log(type.ToString() + " Downloaded.");
			if (type.ToString() == "PLUSHSUIT_DATA")
			{
				global::UnityEngine.Debug.Log(request.downloadHandler.text);
			}
			global::UnityEngine.PlayerPrefs.SetString(type.ToString(), request.downloadHandler.text);
			global::UnityEngine.PlayerPrefs.Save();
			global::UnityEngine.Debug.Log("PLIST " + type.ToString() + " SET!");
			downloadedSections[downloadInfo] = true;
			SectionComplete(masterdata);
		}
		yield return null;
	}

	public void DeserializeMasterData()
	{
		foreach (MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo item in new global::System.Collections.Generic.List<MasterDataDownloadCacheDeserialize.MasterDataDownloadInfo>(masterDataSections.Keys))
		{
			string value = global::UnityEngine.PlayerPrefs.GetString(item.MasterDataType.ToString());
			global::System.Type deserializeType = item.DeserializeType;
			masterDataSections[item] = global::Newtonsoft.Json.JsonConvert.DeserializeObject(value, deserializeType);
			global::UnityEngine.Debug.Log("IS MASTER DATA SECTION DESERIALIZED PROPERLY? - " + (masterDataSections[item] != null));
		}
		if (_constant.UseStreamingAssets)
		{
			CoroutineHelper.StartCoroutine(GetStreamingAssetsTOC());
		}
		else
		{
			_loadingManager.LoadStage2("");
		}
		InvokeMasterDataDeserialized();
	}

	private global::System.Collections.IEnumerator GetStreamingAssetsTOC()
	{
		string streamingAssetFilePath = GetStreamingAssetFilePath("TOC/TOC.json");
		global::UnityEngine.Networking.UnityWebRequest request = global::UnityEngine.Networking.UnityWebRequest.Get(streamingAssetFilePath);
		yield return request.SendWebRequest();
		if (request.result != global::UnityEngine.Networking.UnityWebRequest.Result.Success)
		{
			global::UnityEngine.Debug.Log("Could not get StreamingAssets TOC. Should be fine, unless UseStreamingAssets is true in ConstantVariables.");
			_loadingManager.LoadStage2("");
		}
		else
		{
			string text = request.downloadHandler.text;
			_loadingManager.LoadStage2(text);
		}
	}

	private string GetStreamingAssetFilePath(string fileName)
	{
		return "file://" + global::UnityEngine.Application.streamingAssetsPath + "/" + fileName;
	}

	public void InvokeMasterDataDeserialized()
	{
		OnMasterDataDeserialized?.Invoke();
		IsDeserialized = true;
	}
}
