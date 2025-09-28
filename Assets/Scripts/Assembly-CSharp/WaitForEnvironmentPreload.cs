public class WaitForEnvironmentPreload : Phase
{
	private string _bundleId;

	private string _assetId;

	private AssetCache assetCache;

	private global::UnityEngine.AsyncOperation asyncSceneLoad;

	public override AttackPhase AttackPhase => AttackPhase.WaitForEnvironmentPreload;

	protected override void EnterPhase()
	{
		_bundleId = Blackboard.AttackProfile.Environment.Bundle;
		_assetId = Blackboard.AttackProfile.Environment.Asset;
		Blackboard.Systems.IntroScreen.Enable(Blackboard.AttackProfile);
		if (string.IsNullOrEmpty(_bundleId))
		{
			OnEnvironmentInstantiated(global::UnityEngine.Resources.Load<global::UnityEngine.GameObject>(_assetId));
			return;
		}
		global::UnityEngine.Debug.Log("getting environment " + _bundleId);
		Blackboard.Systems.AssetCacheAccess.GetInterfaceAsync(OnAssetCacheReady);
	}

	protected override AttackPhase UpdatePhase()
	{
		if (asyncSceneLoad != null && asyncSceneLoad.isDone)
		{
			PrefabSpawned();
			asyncSceneLoad = null;
		}
		if (!(Blackboard.Systems.EncounterEnvironment != null))
		{
			return AttackPhase.Null;
		}
		return AttackPhase.WaitForCamera;
	}

	protected override void ExitPhase()
	{
		Blackboard.Systems.IntroScreen.MarkReady();
	}

	private void OnAssetCacheReady(AssetCache cache)
	{
		assetCache = cache;
		global::UnityEngine.Debug.Log("Loading environment from bundle! - " + _bundleId + " - " + _assetId);
		bool flag = false;
		foreach (Entry entry in ConstantVariables.Instance.AssetBundleDownloader.DeserializedTOC.Entries)
		{
			if (entry.BundleName == _bundleId)
			{
				flag = true;
				break;
			}
		}
		string bundleName = _bundleId;
		string assetName = _assetId;
		if (!flag)
		{
			bundleName = "environments/freddy";
			assetName = "Environment";
		}
		assetCache.LoadScene(bundleName, assetName, SceneLoaded, OnAssetFailed);
	}

	private void SceneLoaded(global::UnityEngine.AsyncOperation op)
	{
		asyncSceneLoad = op;
	}

	private void PrefabSpawned()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find("EncounterEnvironment");
		if (gameObject != null)
		{
			OnEnvironmentInstantiated(gameObject);
		}
		else
		{
			OnAssetFailed();
		}
	}

	private void OnEnvironmentInstantiated(global::UnityEngine.GameObject instance)
	{
		instance.transform.localPosition = new global::UnityEngine.Vector3(0f, -0.2f, 0f);
		Blackboard.Systems.EncounterEnvironment = instance.GetComponent<EncounterEnvironment>();
	}

	private void OnAssetFailed()
	{
		global::UnityEngine.Debug.LogError("WaitForEnvironmentPreload OnAssetFailed - Could not load encounter environment asset " + _assetId);
	}
}
