public class ScavengingWaitForEnvironmentPreload : Phase
{
	private string _bundleId;

	private string _assetId;

	private AssetCache assetCache;

	private global::UnityEngine.AsyncOperation asyncSceneLoad;

	public override AttackPhase AttackPhase => AttackPhase.WaitForEnvironmentPreload;

	protected override void EnterPhase()
	{
		_bundleId = Blackboard.ScavengingData.Environment.Bundle;
		_assetId = Blackboard.ScavengingData.Environment.Asset;
		Blackboard.Systems.IntroScreen.Enable(Blackboard.ScavengingAttackProfile);
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
		if (!(Blackboard.Systems.ScavengingEnvironment != null))
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
		assetCache.LoadScene(_bundleId, _assetId, SceneLoaded, OnAssetFailed);
	}

	private void SceneLoaded(global::UnityEngine.AsyncOperation op)
	{
		asyncSceneLoad = op;
	}

	private void PrefabSpawned()
	{
		ScavengingEnvironment scavengingEnvironment = global::UnityEngine.Object.FindFirstObjectByType<ScavengingEnvironment>();
		if (scavengingEnvironment != null)
		{
			OnEnvironmentInstantiated(scavengingEnvironment);
		}
		else
		{
			OnAssetFailed();
		}
	}

	private void OnEnvironmentInstantiated(ScavengingEnvironment instance)
	{
		instance.transform.localPosition = new global::UnityEngine.Vector3(0f, -2.2f, 0f);
		Blackboard.Systems.ScavengingEnvironment = instance;
	}

	private void OnAssetFailed()
	{
		global::UnityEngine.Debug.LogError("ScavengingWaitForEnvironmentPreload OnAssetFailed - Could not load Scavenging environment asset " + _assetId);
	}
}
