public class AnimatronicDisplayAssetPopulator
{
	private const int ANIMATRONIC_DISPLAY_LAYER = 22;

	private const float ANIMATRONIC_CAMERA_DEPTH = 10f;

	private const string RIG_PREFAB_NAME = "AnimatronicUIRig";

	private const string RIG_ASSET_BUNDLE_NAME = "ui/mapentitycommon";

	private const string ANIMATRONIC_MOUNT_NAME = "AnimatronicMount";

	private const string RENDER_TEXTURE_PATH = "ContentAssets/DialogPrefabs/Map_UI_Animatronic";

	private global::UnityEngine.Transform _rootTransform;

	private global::UnityEngine.RectTransform _cameraReferenceRect;

	private AssetCache _assetCache;

	private bool _hasTornDown;

	private global::UnityEngine.GameObject _rigPrefab;

	private global::UnityEngine.GameObject _animatronicPrefab;

	private string _plushsuitBundleName;

	private string _plushsuitAssetName;

	private ComponentContainer _rigComponents;

	private global::System.Action completedPreviewLoadCallback;

	public void Setup(global::UnityEngine.Transform rootTransform, global::UnityEngine.RectTransform cameraReferenceRect, MapEntity entity, ItemDefinitionDomain itemDefinitionDomain, AssetCache assetCache, global::System.Action loadedCallback)
	{
		completedPreviewLoadCallback = loadedCallback;
		_rootTransform = rootTransform;
		_cameraReferenceRect = cameraReferenceRect;
		_assetCache = assetCache;
		if (!entity.SynchronizeableState.parts.ContainsKey("PlushSuit"))
		{
			global::UnityEngine.Debug.LogError("AnimatronicDisplayAssetPopulator Setup - Could not find plushsuit on entity to populate 3d animatronic for entity " + entity.EntityId);
			Teardown();
		}
		else
		{
			RequestAssetsForPlushSuitId(itemDefinitionDomain, entity.SynchronizeableState.parts["PlushSuit"]);
		}
	}

	public void Teardown()
	{
		if (!_hasTornDown)
		{
			_hasTornDown = true;
			_assetCache.ReleaseAsset(_rigPrefab);
			_assetCache.ReleaseAsset(_animatronicPrefab);
			_rigPrefab = null;
			_animatronicPrefab = null;
			_cameraReferenceRect.GetComponentInParent<global::UnityEngine.Canvas>().renderMode = global::UnityEngine.RenderMode.ScreenSpaceOverlay;
			_rigComponents = null;
			_assetCache = null;
		}
	}

	private void RequestAssetsForPlushSuitId(ItemDefinitionDomain itemDefinitionDomain, string plushSuitId)
	{
		PlushSuitData plushSuitById = itemDefinitionDomain.ItemDefinitions.GetPlushSuitById(plushSuitId);
		if (plushSuitById == null)
		{
			global::UnityEngine.Debug.LogError("BRO WHO TF IS " + plushSuitId);
		}
		_plushsuitBundleName = plushSuitById.AnimatronicAssetBundle;
		_plushsuitAssetName = plushSuitById.AnimatronicPrefab;
		_assetCache.LoadAsset<global::UnityEngine.GameObject>(_plushsuitBundleName, _plushsuitAssetName, RequestAssetsForPlushSuitIdb__17_2, RequestAssetsForPlushSuitIdb__17_3);
		_assetCache.LoadAsset<global::UnityEngine.GameObject>("ui/mapentitycommon", "AnimatronicUIRig", RequestAssetsForPlushSuitIdb__17_0, RequestAssetsForPlushSuitIdb__17_1);
	}

	private void OnAssetFailed(string role, string asset)
	{
		global::UnityEngine.Debug.LogError("AnimatronicDisplayAssetPopulator OnAssetFailed - Failed to load " + role + " '" + asset + "'");
		Teardown();
	}

	private void OnAssetsLoaded()
	{
		if (!(_rigPrefab == null) && !(_animatronicPrefab == null))
		{
			ComponentContainer rigComponents = new ComponentContainer();
			_rigComponents = rigComponents;
			global::System.Type[] onlyCacheTypes = new global::System.Type[4]
			{
				typeof(global::UnityEngine.Camera),
				typeof(global::UnityEngine.Transform),
				typeof(global::UnityEngine.Animator),
				typeof(global::UnityEngine.Light)
			};
			_rigComponents.CacheComponents(global::UnityEngine.Object.Instantiate(_rigPrefab, _rootTransform, worldPositionStays: false), onlyCacheTypes);
			_cameraReferenceRect.GetComponentInParent<global::UnityEngine.Canvas>().renderMode = global::UnityEngine.RenderMode.ScreenSpaceCamera;
			SetupAnimatronicCamera(_rigComponents);
			SetupAnimatronicInstance();
			completedPreviewLoadCallback();
		}
	}

	private global::UnityEngine.Rect GetViewportRectFromUIElement(global::UnityEngine.Canvas dialogCanvas, global::UnityEngine.RectTransform element)
	{
		global::UnityEngine.Camera worldCamera = dialogCanvas.worldCamera;
		global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[4];
		element.GetWorldCorners(array);
		global::UnityEngine.Vector3 vector = worldCamera.WorldToViewportPoint(array[0]);
		global::UnityEngine.Vector3 vector2 = worldCamera.WorldToViewportPoint(array[2]);
		return new global::UnityEngine.Rect(vector, vector2 - vector);
	}

	private void SetupAnimatronicCamera(ComponentContainer container)
	{
		if (container.TryGetComponent<global::UnityEngine.Camera>("Camera", out var returnComponent))
		{
			returnComponent.depth = 10f;
			returnComponent.farClipPlane = 200f;
			global::UnityEngine.Rendering.PostProcessing.PostProcessLayer postProcessLayer = returnComponent.gameObject.AddComponent<global::UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
			postProcessLayer.enabled = false;
			postProcessLayer.volumeLayer = 8388608;
			postProcessLayer.volumeTrigger = returnComponent.transform;
			postProcessLayer.Init(global::UnityEngine.Resources.Load<global::UnityEngine.GameObject>("PostProcessResources").GetComponent<PostProcessResourceFinder>().Resource);
			global::UnityEngine.Rendering.PostProcessing.PostProcessVolume postProcessVolume = returnComponent.gameObject.AddComponent<global::UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
			postProcessVolume.enabled = false;
			postProcessVolume.isGlobal = true;
			postProcessVolume.weight = 1f;
			postProcessVolume.priority = 0f;
			postProcessVolume.profile = global::UnityEngine.Resources.Load<global::UnityEngine.Rendering.PostProcessing.PostProcessProfile>("MapEntityCommonProfile");
			postProcessVolume.enabled = true;
			postProcessLayer.enabled = true;
			global::UnityEngine.RenderTexture renderTexture = global::UnityEngine.Resources.Load<global::UnityEngine.RenderTexture>("ContentAssets/DialogPrefabs/Map_UI_Animatronic");
			if (!(renderTexture == null))
			{
				returnComponent.targetTexture = renderTexture;
			}
		}
	}

	private void SetupAnimatronicInstance()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_animatronicPrefab, _rigComponents.TryGetComponent<global::UnityEngine.Transform>("AnimatronicMount"), worldPositionStays: false);
		AnimatronicModelConfig component = gameObject.GetComponent<AnimatronicModelConfig>();
		global::SRF.SRFGameObjectExtensions.SetLayerRecursive(gameObject, 23);
		component.Animator.SetInteger("AnimationMode", 1);
		component.Animator.SetTrigger("SwitchMode");
		component.Animator.SetTrigger("Idle");
		gameObject.transform.localPosition = new global::UnityEngine.Vector3(0f, -0.9f, -2f);
	}

	private void RequestAssetsForPlushSuitIdb__17_0(global::UnityEngine.GameObject prefab)
	{
		_rigPrefab = prefab;
		OnAssetsLoaded();
	}

	private void RequestAssetsForPlushSuitIdb__17_1()
	{
		OnAssetFailed("RigPrefab", "AnimatronicUIRig");
	}

	private void RequestAssetsForPlushSuitIdb__17_2(global::UnityEngine.GameObject prefab)
	{
		_animatronicPrefab = prefab;
		OnAssetsLoaded();
	}

	private void RequestAssetsForPlushSuitIdb__17_3()
	{
		OnAssetFailed("AnimatronicPrefab", _plushsuitAssetName);
	}
}
