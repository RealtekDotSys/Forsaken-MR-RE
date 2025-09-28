public class Animatronic3DDomain
{
	private const string CoreShaderCollectionName = "CoreAnimatronicShaderCollection";

	private const string ShaderCollectionName = "AnimatronicShaderCollection";

	private const string SharedCoreBundleName = "animatronics/sharedcore";

	private const string SharedBundleName = "animatronics/shared";

	private const string Animatronic3DPrefabName = "Animatronic3D";

	private global::UnityEngine.GameObject _domainGameObject;

	private EventExposer _masterEventExposer;

	private GameAssetManagementDomain _gameAssetManagementDomain;

	private GameAudioDomain _gameAudioDomain;

	private AssetCache _assetCache;

	private Animatronic3D _animatronic3DPrefab;

	private ShaderCollectionInitializer _shaderInitializer;

	private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<CreationRequest>> _pendingCreationRequests;

	public bool IsReady => true;

	public Animatronic3DDomain GetPublicInterface => this;

	public global::UnityEngine.Transform Get3DRoot()
	{
		return _domainGameObject.transform;
	}

	public void CreateAnimatronic3D(CreationRequest request)
	{
		Animatronic3DPrefabLoaderComplete(global::UnityEngine.Resources.Load<global::UnityEngine.GameObject>("Animatronic3D").GetComponent<Animatronic3D>());
		if (_animatronic3DPrefab == null)
		{
			request.SetAnimatronicCreationFailure();
			return;
		}
		PlushSuitData plushSuit = request.ConfigData.PlushSuitData;
		if (!_pendingCreationRequests.ContainsKey(plushSuit.Id))
		{
			_pendingCreationRequests.Add(plushSuit.Id, new global::System.Collections.Generic.List<CreationRequest>());
		}
		_pendingCreationRequests[plushSuit.Id].Add(request);
		request.OnRequestComplete += CreationRequestComplete;
		request.LoadAnimatronicShaders(_assetCache);
		request.LoadCpuSoundBank(_gameAudioDomain.AudioPlayer);
		request.LoadPlushSuitSoundBank(_gameAudioDomain.AudioPlayer);
		MasterDomain.GetDomain().eventExposer.OnAnimatronicCreationRequestStarted();
		global::UnityEngine.Debug.LogError("Instantiating Animatronic Prefab with Animatronic3DDomain");
		_assetCache.Instantiate(plushSuit.AnimatronicAssetBundle, plushSuit.AnimatronicPrefab, delegate(global::UnityEngine.GameObject prefab)
		{
			OnPlushsuitInstantiated(plushSuit.Id, prefab, request);
		}, delegate
		{
			PrefabLoadFailure(plushSuit.Id, request);
		});
	}

	public void ReleaseAnimatronic3D(Animatronic3D animatronic3D)
	{
		global::UnityEngine.Debug.Log("Destroying releasing animatronic 3d " + animatronic3D.name);
		_assetCache.ReleaseInstance(animatronic3D.GetModelConfig().gameObject);
	}

	private void OnPlushsuitInstantiated(string plushsuitId, global::UnityEngine.GameObject gameObject, CreationRequest creationRequest)
	{
		if (_pendingCreationRequests.ContainsKey(plushsuitId))
		{
			SetupAnimatronic3D(creationRequest, gameObject.GetComponent<AnimatronicModelConfig>());
		}
	}

	private void PrefabLoadFailure(string plushSuitId, CreationRequest request)
	{
		if (_pendingCreationRequests.ContainsKey(plushSuitId))
		{
			_pendingCreationRequests[plushSuitId].Remove(request);
		}
		global::UnityEngine.Debug.LogError("Received a PrefabLoadFailure callback for an animatronic '" + plushSuitId + "' with no associated CreationRequest");
	}

	private void SetupAnimatronic3D(CreationRequest request, AnimatronicModelConfig animatronicModelConfig)
	{
		global::UnityEngine.Transform parent;
		if (request.Parent != null)
		{
			parent = request.Parent;
		}
		else
		{
			if (_domainGameObject == null)
			{
				_domainGameObject = new global::UnityEngine.GameObject("Animatronic3D");
				_domainGameObject.AddComponent<DontDestroy>();
			}
			parent = _domainGameObject.transform;
		}
		Animatronic3D animatronic3D = global::UnityEngine.Object.Instantiate(_animatronic3DPrefab, parent);
		animatronic3D.SetAudioPlayer(_gameAudioDomain.AudioPlayer);
		animatronic3D.SetModelConfig(animatronicModelConfig);
		animatronic3D.gameObject.SetActive(value: false);
		animatronic3D.name += request.ConfigData.CpuData.Id;
		request.SetAnimatronicCreationSuccess(animatronic3D);
	}

	private void CreationRequestComplete(CreationRequest request)
	{
		global::UnityEngine.Debug.Log("Creation request complete actually worked somehow");
		request.OnRequestComplete -= CreationRequestComplete;
		_pendingCreationRequests[request.ConfigData.PlushSuitData.Id].Remove(request);
		MasterDomain.GetDomain().eventExposer.OnAnimatronicCreationRequestCompleted();
	}

	public Animatronic3DDomain(global::UnityEngine.Transform parent)
	{
		_pendingCreationRequests = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<CreationRequest>>();
	}

	public void Setup(GameAssetManagementDomain gameAssetManagementDomain, GameAudioDomain audioDomain)
	{
		_gameAudioDomain = audioDomain;
		_gameAssetManagementDomain = gameAssetManagementDomain;
		_gameAssetManagementDomain.AssetCacheAccess.GetInterfaceAsync(AssetCacheReady);
	}

	public void Teardown()
	{
		_pendingCreationRequests.Clear();
		_pendingCreationRequests = null;
		_animatronic3DPrefab = null;
		_domainGameObject = null;
	}

	private void Animatronic3DPrefabLoaderComplete(Animatronic3D animatronic3DPrefab)
	{
		if (animatronic3DPrefab != null)
		{
			_animatronic3DPrefab = animatronic3DPrefab;
		}
		else
		{
			global::UnityEngine.Debug.Log("Animatronic3d Prefab could not be loaded.");
		}
	}

	private void AssetCacheReady(AssetCache assetCache)
	{
		_assetCache = assetCache;
		_shaderInitializer = new ShaderCollectionInitializer(_assetCache, "animatronics/shared", "AnimatronicShaderCollection", forceLoad: true, ShaderCollectionInitializerFinished);
	}

	private void ShaderCollectionInitializerFinished()
	{
		if (_shaderInitializer == null || !_shaderInitializer.ShadersReady)
		{
			global::UnityEngine.Debug.LogError("Animatronic3DDomain.Animatronic3DPrefabName ShaderCollectionInitializerFinished - Shaders could not be initialized. Animatronic3d will not be able to function.");
		}
	}
}
