[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("")]
public class AmplifyMotionEffectBase : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Motion Blur")]
	public global::AmplifyMotion.Quality QualityLevel = global::AmplifyMotion.Quality.Standard;

	public int QualitySteps = 1;

	public float MotionScale = 3f;

	public float CameraMotionMult = 1f;

	public float ObjectMotionMult = 1f;

	public float MinVelocity = 1f;

	public float MaxVelocity = 10f;

	public float DepthThreshold = 0.01f;

	public bool Noise;

	[global::UnityEngine.Header("Camera")]
	public global::UnityEngine.Camera[] OverlayCameras = new global::UnityEngine.Camera[0];

	public global::UnityEngine.LayerMask CullingMask = -1;

	[global::UnityEngine.Header("Objects")]
	public bool AutoRegisterObjs = true;

	public float MinResetDeltaDist = 1000f;

	[global::System.NonSerialized]
	public float MinResetDeltaDistSqr;

	public int ResetFrameDelay = 1;

	[global::UnityEngine.Header("Low-Level")]
	[global::UnityEngine.Serialization.FormerlySerializedAs("workerThreads")]
	public int WorkerThreads;

	public bool SystemThreadPool;

	public bool ForceCPUOnly;

	public bool DebugMode;

	private global::UnityEngine.Camera m_camera;

	private bool m_starting = true;

	private int m_width;

	private int m_height;

	private global::UnityEngine.RenderTexture m_motionRT;

	private global::UnityEngine.Material m_blurMaterial;

	private global::UnityEngine.Material m_solidVectorsMaterial;

	private global::UnityEngine.Material m_skinnedVectorsMaterial;

	private global::UnityEngine.Material m_clothVectorsMaterial;

	private global::UnityEngine.Material m_reprojectionMaterial;

	private global::UnityEngine.Material m_combineMaterial;

	private global::UnityEngine.Material m_dilationMaterial;

	private global::UnityEngine.Material m_depthMaterial;

	private global::UnityEngine.Material m_debugMaterial;

	private global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, AmplifyMotionCamera> m_linkedCameras = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, AmplifyMotionCamera>();

	internal global::UnityEngine.Camera[] m_linkedCameraKeys;

	internal AmplifyMotionCamera[] m_linkedCameraValues;

	internal bool m_linkedCamerasChanged = true;

	private AmplifyMotionPostProcess m_currentPostProcess;

	private int m_globalObjectId = 1;

	private float m_deltaTime;

	private float m_fixedDeltaTime;

	private float m_motionScaleNorm;

	private float m_fixedMotionScaleNorm;

	private global::AmplifyMotion.Quality m_qualityLevel;

	private AmplifyMotionCamera m_baseCamera;

	private global::AmplifyMotion.WorkerThreadPool m_workerThreadPool;

	public static global::System.Collections.Generic.Dictionary<global::UnityEngine.GameObject, AmplifyMotionObjectBase> m_activeObjects = new global::System.Collections.Generic.Dictionary<global::UnityEngine.GameObject, AmplifyMotionObjectBase>();

	public static global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, AmplifyMotionCamera> m_activeCameras = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, AmplifyMotionCamera>();

	private static bool m_isD3D = false;

	private bool m_canUseGPU;

	private const global::UnityEngine.Rendering.CameraEvent m_updateCBEvent = global::UnityEngine.Rendering.CameraEvent.BeforeImageEffectsOpaque;

	private global::UnityEngine.Rendering.CommandBuffer m_updateCB;

	private const global::UnityEngine.Rendering.CameraEvent m_fixedUpdateCBEvent = global::UnityEngine.Rendering.CameraEvent.BeforeImageEffectsOpaque;

	private global::UnityEngine.Rendering.CommandBuffer m_fixedUpdateCB;

	private static bool m_ignoreMotionScaleWarning = false;

	private static AmplifyMotionEffectBase m_firstInstance = null;

	[global::System.Obsolete("workerThreads is deprecated, please use WorkerThreads instead.")]
	public int workerThreads
	{
		get
		{
			return WorkerThreads;
		}
		set
		{
			WorkerThreads = value;
		}
	}

	internal global::UnityEngine.Material ReprojectionMaterial => m_reprojectionMaterial;

	internal global::UnityEngine.Material SolidVectorsMaterial => m_solidVectorsMaterial;

	internal global::UnityEngine.Material SkinnedVectorsMaterial => m_skinnedVectorsMaterial;

	internal global::UnityEngine.Material ClothVectorsMaterial => m_clothVectorsMaterial;

	internal global::UnityEngine.RenderTexture MotionRenderTexture => m_motionRT;

	public global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, AmplifyMotionCamera> LinkedCameras => m_linkedCameras;

	internal float MotionScaleNorm => m_motionScaleNorm;

	internal float FixedMotionScaleNorm => m_fixedMotionScaleNorm;

	public AmplifyMotionCamera BaseCamera => m_baseCamera;

	internal global::AmplifyMotion.WorkerThreadPool WorkerPool => m_workerThreadPool;

	public static bool IsD3D => m_isD3D;

	public bool CanUseGPU => m_canUseGPU;

	public static bool IgnoreMotionScaleWarning => m_ignoreMotionScaleWarning;

	public static AmplifyMotionEffectBase FirstInstance => m_firstInstance;

	public static AmplifyMotionEffectBase Instance => m_firstInstance;

	private void Awake()
	{
		if (m_firstInstance == null)
		{
			m_firstInstance = this;
		}
		m_isD3D = global::UnityEngine.SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D");
		m_globalObjectId = 1;
		m_width = (m_height = 0);
		if (ForceCPUOnly)
		{
			m_canUseGPU = false;
			return;
		}
		bool flag = global::UnityEngine.SystemInfo.graphicsShaderLevel >= 30;
		bool flag2 = global::UnityEngine.SystemInfo.SupportsTextureFormat(global::UnityEngine.TextureFormat.RHalf);
		bool flag3 = global::UnityEngine.SystemInfo.SupportsTextureFormat(global::UnityEngine.TextureFormat.RGHalf);
		bool flag4 = global::UnityEngine.SystemInfo.SupportsTextureFormat(global::UnityEngine.TextureFormat.RGBAHalf);
		bool flag5 = global::UnityEngine.SystemInfo.SupportsRenderTextureFormat(global::UnityEngine.RenderTextureFormat.ARGBFloat);
		m_canUseGPU = flag && flag2 && flag3 && flag4 && flag5;
	}

	internal void ResetObjectId()
	{
		m_globalObjectId = 1;
	}

	internal int GenerateObjectId(global::UnityEngine.GameObject obj)
	{
		if (obj.isStatic)
		{
			return 0;
		}
		m_globalObjectId++;
		if (m_globalObjectId > 254)
		{
			m_globalObjectId = 1;
		}
		return m_globalObjectId;
	}

	private void SafeDestroyMaterial(ref global::UnityEngine.Material mat)
	{
		if (mat != null)
		{
			global::UnityEngine.Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	private bool CheckMaterialAndShader(global::UnityEngine.Material material, string name)
	{
		bool result = true;
		if (material == null || material.shader == null)
		{
			global::UnityEngine.Debug.LogWarning("[AmplifyMotion] Error creating " + name + " material");
			result = false;
		}
		else if (!material.shader.isSupported)
		{
			global::UnityEngine.Debug.LogWarning("[AmplifyMotion] " + name + " shader not supported on this platform");
			result = false;
		}
		return result;
	}

	private void DestroyMaterials()
	{
		SafeDestroyMaterial(ref m_blurMaterial);
		SafeDestroyMaterial(ref m_solidVectorsMaterial);
		SafeDestroyMaterial(ref m_skinnedVectorsMaterial);
		SafeDestroyMaterial(ref m_clothVectorsMaterial);
		SafeDestroyMaterial(ref m_reprojectionMaterial);
		SafeDestroyMaterial(ref m_combineMaterial);
		SafeDestroyMaterial(ref m_dilationMaterial);
		SafeDestroyMaterial(ref m_depthMaterial);
		SafeDestroyMaterial(ref m_debugMaterial);
	}

	private bool CreateMaterials()
	{
		DestroyMaterials();
		string text = "Hidden/Amplify Motion/MotionBlurSM" + ((global::UnityEngine.SystemInfo.graphicsShaderLevel >= 30) ? 3 : 2);
		string text2 = "Hidden/Amplify Motion/SolidVectors";
		string text3 = "Hidden/Amplify Motion/SkinnedVectors";
		string text4 = "Hidden/Amplify Motion/ClothVectors";
		string text5 = "Hidden/Amplify Motion/ReprojectionVectors";
		string text6 = "Hidden/Amplify Motion/Combine";
		string text7 = "Hidden/Amplify Motion/Dilation";
		string text8 = "Hidden/Amplify Motion/Depth";
		string text9 = "Hidden/Amplify Motion/Debug";
		try
		{
			m_blurMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find(text))
			{
				hideFlags = global::UnityEngine.HideFlags.DontSave
			};
			m_solidVectorsMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find(text2))
			{
				hideFlags = global::UnityEngine.HideFlags.DontSave
			};
			m_skinnedVectorsMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find(text3))
			{
				hideFlags = global::UnityEngine.HideFlags.DontSave
			};
			m_clothVectorsMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find(text4))
			{
				hideFlags = global::UnityEngine.HideFlags.DontSave
			};
			m_reprojectionMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find(text5))
			{
				hideFlags = global::UnityEngine.HideFlags.DontSave
			};
			m_combineMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find(text6))
			{
				hideFlags = global::UnityEngine.HideFlags.DontSave
			};
			m_dilationMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find(text7))
			{
				hideFlags = global::UnityEngine.HideFlags.DontSave
			};
			m_depthMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find(text8))
			{
				hideFlags = global::UnityEngine.HideFlags.DontSave
			};
			m_debugMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find(text9))
			{
				hideFlags = global::UnityEngine.HideFlags.DontSave
			};
		}
		catch (global::System.Exception)
		{
		}
		if (CheckMaterialAndShader(m_blurMaterial, text) && CheckMaterialAndShader(m_solidVectorsMaterial, text2) && CheckMaterialAndShader(m_skinnedVectorsMaterial, text3) && CheckMaterialAndShader(m_clothVectorsMaterial, text4) && CheckMaterialAndShader(m_reprojectionMaterial, text5) && CheckMaterialAndShader(m_combineMaterial, text6) && CheckMaterialAndShader(m_dilationMaterial, text7) && CheckMaterialAndShader(m_depthMaterial, text8))
		{
			return CheckMaterialAndShader(m_debugMaterial, text9);
		}
		return false;
	}

	private global::UnityEngine.RenderTexture CreateRenderTexture(string name, int depth, global::UnityEngine.RenderTextureFormat fmt, global::UnityEngine.RenderTextureReadWrite rw, global::UnityEngine.FilterMode fm)
	{
		global::UnityEngine.RenderTexture renderTexture = new global::UnityEngine.RenderTexture(m_width, m_height, depth, fmt, rw);
		renderTexture.hideFlags = global::UnityEngine.HideFlags.DontSave;
		renderTexture.name = name;
		renderTexture.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		renderTexture.filterMode = fm;
		renderTexture.Create();
		return renderTexture;
	}

	private void SafeDestroyRenderTexture(ref global::UnityEngine.RenderTexture rt)
	{
		if (rt != null)
		{
			global::UnityEngine.RenderTexture.active = null;
			rt.Release();
			global::UnityEngine.Object.DestroyImmediate(rt);
			rt = null;
		}
	}

	private void SafeDestroyTexture(ref global::UnityEngine.Texture tex)
	{
		if (tex != null)
		{
			global::UnityEngine.Object.DestroyImmediate(tex);
			tex = null;
		}
	}

	private void DestroyRenderTextures()
	{
		global::UnityEngine.RenderTexture.active = null;
		SafeDestroyRenderTexture(ref m_motionRT);
	}

	private void UpdateRenderTextures(bool qualityChanged)
	{
		int num = global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.FloorToInt((float)m_camera.pixelWidth + 0.5f), 1);
		int num2 = global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.FloorToInt((float)m_camera.pixelHeight + 0.5f), 1);
		if (QualityLevel == global::AmplifyMotion.Quality.Mobile)
		{
			num /= 2;
			num2 /= 2;
		}
		if (m_width != num || m_height != num2)
		{
			m_width = num;
			m_height = num2;
			DestroyRenderTextures();
		}
		if (m_motionRT == null)
		{
			m_motionRT = CreateRenderTexture("AM-MotionVectors", 24, global::UnityEngine.RenderTextureFormat.ARGB32, global::UnityEngine.RenderTextureReadWrite.Linear, global::UnityEngine.FilterMode.Point);
		}
	}

	public bool CheckSupport()
	{
		if (!global::UnityEngine.SystemInfo.supportsImageEffects)
		{
			global::UnityEngine.Debug.LogError("[AmplifyMotion] Initialization failed. This plugin requires support for Image Effects and Render Textures.");
			return false;
		}
		return true;
	}

	private void InitializeThreadPool()
	{
		if (WorkerThreads <= 0)
		{
			WorkerThreads = global::UnityEngine.Mathf.Max(global::System.Environment.ProcessorCount / 2, 1);
		}
		m_workerThreadPool = new global::AmplifyMotion.WorkerThreadPool();
		m_workerThreadPool.InitializeAsyncUpdateThreads(WorkerThreads, SystemThreadPool);
	}

	private void ShutdownThreadPool()
	{
		if (m_workerThreadPool != null)
		{
			m_workerThreadPool.FinalizeAsyncUpdateThreads();
			m_workerThreadPool = null;
		}
	}

	private void InitializeCommandBuffers()
	{
		ShutdownCommandBuffers();
		m_updateCB = new global::UnityEngine.Rendering.CommandBuffer();
		m_updateCB.name = "AmplifyMotion.Update";
		m_camera.AddCommandBuffer(global::UnityEngine.Rendering.CameraEvent.BeforeImageEffectsOpaque, m_updateCB);
		m_fixedUpdateCB = new global::UnityEngine.Rendering.CommandBuffer();
		m_fixedUpdateCB.name = "AmplifyMotion.FixedUpdate";
		m_camera.AddCommandBuffer(global::UnityEngine.Rendering.CameraEvent.BeforeImageEffectsOpaque, m_fixedUpdateCB);
	}

	private void ShutdownCommandBuffers()
	{
		if (m_updateCB != null)
		{
			m_camera.RemoveCommandBuffer(global::UnityEngine.Rendering.CameraEvent.BeforeImageEffectsOpaque, m_updateCB);
			m_updateCB.Release();
			m_updateCB = null;
		}
		if (m_fixedUpdateCB != null)
		{
			m_camera.RemoveCommandBuffer(global::UnityEngine.Rendering.CameraEvent.BeforeImageEffectsOpaque, m_fixedUpdateCB);
			m_fixedUpdateCB.Release();
			m_fixedUpdateCB = null;
		}
	}

	private void OnEnable()
	{
		m_camera = GetComponent<global::UnityEngine.Camera>();
		if (!CheckSupport())
		{
			base.enabled = false;
			return;
		}
		InitializeThreadPool();
		m_starting = true;
		if (!CreateMaterials())
		{
			global::UnityEngine.Debug.LogError("[AmplifyMotion] Failed loading or compiling necessary shaders. Please try reinstalling Amplify Motion or contact support@amplify.pt");
			base.enabled = false;
			return;
		}
		if (AutoRegisterObjs)
		{
			UpdateActiveObjects();
		}
		InitializeCameras();
		InitializeCommandBuffers();
		UpdateRenderTextures(qualityChanged: true);
		m_linkedCameras.TryGetValue(m_camera, out m_baseCamera);
		if (m_baseCamera == null)
		{
			global::UnityEngine.Debug.LogError("[AmplifyMotion] Failed setting up Base Camera. Please contact support@amplify.pt");
			base.enabled = false;
			return;
		}
		if (m_currentPostProcess != null)
		{
			m_currentPostProcess.enabled = true;
		}
		m_qualityLevel = QualityLevel;
	}

	private void OnDisable()
	{
		if (m_currentPostProcess != null)
		{
			m_currentPostProcess.enabled = false;
		}
		ShutdownCommandBuffers();
		ShutdownThreadPool();
	}

	private void Start()
	{
		UpdatePostProcess();
	}

	internal void RemoveCamera(global::UnityEngine.Camera reference)
	{
		m_linkedCameras.Remove(reference);
	}

	private void OnDestroy()
	{
		AmplifyMotionCamera[] array = global::System.Linq.Enumerable.ToArray(m_linkedCameras.Values);
		foreach (AmplifyMotionCamera amplifyMotionCamera in array)
		{
			if (amplifyMotionCamera != null && amplifyMotionCamera.gameObject != base.gameObject)
			{
				global::UnityEngine.Camera component = amplifyMotionCamera.GetComponent<global::UnityEngine.Camera>();
				if (component != null)
				{
					component.targetTexture = null;
				}
				global::UnityEngine.Object.DestroyImmediate(amplifyMotionCamera);
			}
		}
		DestroyRenderTextures();
		DestroyMaterials();
	}

	private global::UnityEngine.GameObject RecursiveFindCamera(global::UnityEngine.GameObject obj, string auxCameraName)
	{
		global::UnityEngine.GameObject gameObject = null;
		if (obj.name == auxCameraName)
		{
			gameObject = obj;
		}
		else
		{
			foreach (global::UnityEngine.Transform item in obj.transform)
			{
				gameObject = RecursiveFindCamera(item.gameObject, auxCameraName);
				if (gameObject != null)
				{
					break;
				}
			}
		}
		return gameObject;
	}

	private void InitializeCameras()
	{
		global::System.Collections.Generic.List<global::UnityEngine.Camera> list = new global::System.Collections.Generic.List<global::UnityEngine.Camera>(OverlayCameras.Length);
		for (int i = 0; i < OverlayCameras.Length; i++)
		{
			if (OverlayCameras[i] != null)
			{
				list.Add(OverlayCameras[i]);
			}
		}
		global::UnityEngine.Camera[] array = new global::UnityEngine.Camera[list.Count + 1];
		array[0] = m_camera;
		for (int j = 0; j < list.Count; j++)
		{
			array[j + 1] = list[j];
		}
		m_linkedCameras.Clear();
		for (int k = 0; k < array.Length; k++)
		{
			global::UnityEngine.Camera camera = array[k];
			if (!m_linkedCameras.ContainsKey(camera))
			{
				AmplifyMotionCamera amplifyMotionCamera = camera.gameObject.GetComponent<AmplifyMotionCamera>();
				if (amplifyMotionCamera != null)
				{
					amplifyMotionCamera.enabled = false;
					amplifyMotionCamera.enabled = true;
				}
				else
				{
					amplifyMotionCamera = camera.gameObject.AddComponent<AmplifyMotionCamera>();
				}
				amplifyMotionCamera.LinkTo(this, k > 0);
				m_linkedCameras.Add(camera, amplifyMotionCamera);
				m_linkedCamerasChanged = true;
			}
		}
	}

	public void UpdateActiveCameras()
	{
		InitializeCameras();
	}

	internal static void RegisterCamera(AmplifyMotionCamera cam)
	{
		if (!m_activeCameras.ContainsValue(cam))
		{
			m_activeCameras.Add(cam.GetComponent<global::UnityEngine.Camera>(), cam);
		}
		foreach (AmplifyMotionObjectBase value in m_activeObjects.Values)
		{
			value.RegisterCamera(cam);
		}
	}

	internal static void UnregisterCamera(AmplifyMotionCamera cam)
	{
		foreach (AmplifyMotionObjectBase value in m_activeObjects.Values)
		{
			value.UnregisterCamera(cam);
		}
		m_activeCameras.Remove(cam.GetComponent<global::UnityEngine.Camera>());
	}

	public void UpdateActiveObjects()
	{
		global::UnityEngine.GameObject[] array = global::UnityEngine.Object.FindObjectsOfType(typeof(global::UnityEngine.GameObject)) as global::UnityEngine.GameObject[];
		for (int i = 0; i < array.Length; i++)
		{
			if (!m_activeObjects.ContainsKey(array[i]))
			{
				TryRegister(array[i], autoReg: true);
			}
		}
	}

	internal static void RegisterObject(AmplifyMotionObjectBase obj)
	{
		m_activeObjects.Add(obj.gameObject, obj);
		foreach (AmplifyMotionCamera value in m_activeCameras.Values)
		{
			obj.RegisterCamera(value);
		}
	}

	internal static void UnregisterObject(AmplifyMotionObjectBase obj)
	{
		foreach (AmplifyMotionCamera value in m_activeCameras.Values)
		{
			obj.UnregisterCamera(value);
		}
		m_activeObjects.Remove(obj.gameObject);
	}

	internal static bool FindValidTag(global::UnityEngine.Material[] materials)
	{
		foreach (global::UnityEngine.Material material in materials)
		{
			if (!(material != null))
			{
				continue;
			}
			string text = material.GetTag("RenderType", searchFallbacks: false);
			if (text == "Opaque" || text == "TransparentCutout")
			{
				if (!material.IsKeywordEnabled("_ALPHABLEND_ON"))
				{
					return !material.IsKeywordEnabled("_ALPHAPREMULTIPLY_ON");
				}
				return false;
			}
		}
		return false;
	}

	internal static bool CanRegister(global::UnityEngine.GameObject gameObj, bool autoReg)
	{
		if (gameObj.isStatic)
		{
			return false;
		}
		global::UnityEngine.Renderer component = gameObj.GetComponent<global::UnityEngine.Renderer>();
		if (component == null || component.sharedMaterials == null || component.isPartOfStaticBatch)
		{
			return false;
		}
		if (!component.enabled)
		{
			return false;
		}
		if (component.shadowCastingMode == global::UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly)
		{
			return false;
		}
		if (component.GetType() == typeof(global::UnityEngine.SpriteRenderer))
		{
			return false;
		}
		if (!FindValidTag(component.sharedMaterials))
		{
			return false;
		}
		global::System.Type type = component.GetType();
		if (type == typeof(global::UnityEngine.MeshRenderer) || type == typeof(global::UnityEngine.SkinnedMeshRenderer))
		{
			return true;
		}
		if (type == typeof(global::UnityEngine.ParticleSystemRenderer) && !autoReg)
		{
			global::UnityEngine.ParticleSystemRenderMode renderMode = (component as global::UnityEngine.ParticleSystemRenderer).renderMode;
			if (renderMode != global::UnityEngine.ParticleSystemRenderMode.Mesh)
			{
				return renderMode == global::UnityEngine.ParticleSystemRenderMode.Billboard;
			}
			return true;
		}
		return false;
	}

	internal static void TryRegister(global::UnityEngine.GameObject gameObj, bool autoReg)
	{
		if (CanRegister(gameObj, autoReg) && gameObj.GetComponent<AmplifyMotionObjectBase>() == null)
		{
			AmplifyMotionObjectBase.ApplyToChildren = false;
			gameObj.AddComponent<AmplifyMotionObjectBase>();
			AmplifyMotionObjectBase.ApplyToChildren = true;
		}
	}

	internal static void TryUnregister(global::UnityEngine.GameObject gameObj)
	{
		AmplifyMotionObjectBase component = gameObj.GetComponent<AmplifyMotionObjectBase>();
		if (component != null)
		{
			global::UnityEngine.Object.Destroy(component);
		}
	}

	public void Register(global::UnityEngine.GameObject gameObj)
	{
		if (!m_activeObjects.ContainsKey(gameObj))
		{
			TryRegister(gameObj, autoReg: false);
		}
	}

	public static void RegisterS(global::UnityEngine.GameObject gameObj)
	{
		if (!m_activeObjects.ContainsKey(gameObj))
		{
			TryRegister(gameObj, autoReg: false);
		}
	}

	public void RegisterRecursively(global::UnityEngine.GameObject gameObj)
	{
		if (!m_activeObjects.ContainsKey(gameObj))
		{
			TryRegister(gameObj, autoReg: false);
		}
		foreach (global::UnityEngine.Transform item in gameObj.transform)
		{
			RegisterRecursively(item.gameObject);
		}
	}

	public static void RegisterRecursivelyS(global::UnityEngine.GameObject gameObj)
	{
		if (!m_activeObjects.ContainsKey(gameObj))
		{
			TryRegister(gameObj, autoReg: false);
		}
		foreach (global::UnityEngine.Transform item in gameObj.transform)
		{
			RegisterRecursivelyS(item.gameObject);
		}
	}

	public void Unregister(global::UnityEngine.GameObject gameObj)
	{
		if (m_activeObjects.ContainsKey(gameObj))
		{
			TryUnregister(gameObj);
		}
	}

	public static void UnregisterS(global::UnityEngine.GameObject gameObj)
	{
		if (m_activeObjects.ContainsKey(gameObj))
		{
			TryUnregister(gameObj);
		}
	}

	public void UnregisterRecursively(global::UnityEngine.GameObject gameObj)
	{
		if (m_activeObjects.ContainsKey(gameObj))
		{
			TryUnregister(gameObj);
		}
		foreach (global::UnityEngine.Transform item in gameObj.transform)
		{
			UnregisterRecursively(item.gameObject);
		}
	}

	public static void UnregisterRecursivelyS(global::UnityEngine.GameObject gameObj)
	{
		if (m_activeObjects.ContainsKey(gameObj))
		{
			TryUnregister(gameObj);
		}
		foreach (global::UnityEngine.Transform item in gameObj.transform)
		{
			UnregisterRecursivelyS(item.gameObject);
		}
	}

	private void UpdatePostProcess()
	{
		global::UnityEngine.Camera camera = null;
		float num = float.MinValue;
		if (m_linkedCamerasChanged)
		{
			UpdateLinkedCameras();
		}
		for (int i = 0; i < m_linkedCameraKeys.Length; i++)
		{
			if (m_linkedCameraKeys[i] != null && m_linkedCameraKeys[i].isActiveAndEnabled && m_linkedCameraKeys[i].depth > num)
			{
				camera = m_linkedCameraKeys[i];
				num = m_linkedCameraKeys[i].depth;
			}
		}
		if (m_currentPostProcess != null && m_currentPostProcess.gameObject != camera.gameObject)
		{
			global::UnityEngine.Object.DestroyImmediate(m_currentPostProcess);
			m_currentPostProcess = null;
		}
		if (!(m_currentPostProcess == null) || !(camera != null) || !(camera != m_camera))
		{
			return;
		}
		AmplifyMotionPostProcess[] components = base.gameObject.GetComponents<AmplifyMotionPostProcess>();
		if (components != null && components.Length != 0)
		{
			for (int j = 0; j < components.Length; j++)
			{
				global::UnityEngine.Object.DestroyImmediate(components[j]);
			}
		}
		m_currentPostProcess = camera.gameObject.AddComponent<AmplifyMotionPostProcess>();
		m_currentPostProcess.Instance = this;
	}

	private void LateUpdate()
	{
		if (m_baseCamera.AutoStep)
		{
			float num = (global::UnityEngine.Application.isPlaying ? global::UnityEngine.Time.unscaledDeltaTime : global::UnityEngine.Time.fixedDeltaTime);
			float fixedDeltaTime = global::UnityEngine.Time.fixedDeltaTime;
			m_deltaTime = ((num > float.Epsilon) ? num : m_deltaTime);
			m_fixedDeltaTime = ((num > float.Epsilon) ? fixedDeltaTime : m_fixedDeltaTime);
		}
		QualitySteps = global::UnityEngine.Mathf.Clamp(QualitySteps, 0, 16);
		MotionScale = global::UnityEngine.Mathf.Max(MotionScale, 0f);
		MinVelocity = global::UnityEngine.Mathf.Min(MinVelocity, MaxVelocity);
		DepthThreshold = global::UnityEngine.Mathf.Max(DepthThreshold, 0f);
		MinResetDeltaDist = global::UnityEngine.Mathf.Max(MinResetDeltaDist, 0f);
		MinResetDeltaDistSqr = MinResetDeltaDist * MinResetDeltaDist;
		ResetFrameDelay = global::UnityEngine.Mathf.Max(ResetFrameDelay, 0);
		UpdatePostProcess();
	}

	public void StopAutoStep()
	{
		foreach (AmplifyMotionCamera value in m_linkedCameras.Values)
		{
			value.StopAutoStep();
		}
	}

	public void StartAutoStep()
	{
		foreach (AmplifyMotionCamera value in m_linkedCameras.Values)
		{
			value.StartAutoStep();
		}
	}

	public void Step(float delta)
	{
		m_deltaTime = delta;
		m_fixedDeltaTime = delta;
		foreach (AmplifyMotionCamera value in m_linkedCameras.Values)
		{
			value.Step();
		}
	}

	private void UpdateLinkedCameras()
	{
		global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, AmplifyMotionCamera>.KeyCollection keys = m_linkedCameras.Keys;
		global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, AmplifyMotionCamera>.ValueCollection values = m_linkedCameras.Values;
		if (m_linkedCameraKeys == null || keys.Count != m_linkedCameraKeys.Length)
		{
			m_linkedCameraKeys = new global::UnityEngine.Camera[keys.Count];
		}
		if (m_linkedCameraValues == null || values.Count != m_linkedCameraValues.Length)
		{
			m_linkedCameraValues = new AmplifyMotionCamera[values.Count];
		}
		keys.CopyTo(m_linkedCameraKeys, 0);
		values.CopyTo(m_linkedCameraValues, 0);
		m_linkedCamerasChanged = false;
	}

	private void FixedUpdate()
	{
		if (!m_camera.enabled)
		{
			return;
		}
		if (m_linkedCamerasChanged)
		{
			UpdateLinkedCameras();
		}
		m_fixedUpdateCB.Clear();
		for (int i = 0; i < m_linkedCameraValues.Length; i++)
		{
			if (m_linkedCameraValues[i] != null && m_linkedCameraValues[i].isActiveAndEnabled)
			{
				m_linkedCameraValues[i].FixedUpdateTransform(this, m_fixedUpdateCB);
			}
		}
	}

	private void OnPreRender()
	{
		if (!m_camera.enabled || (global::UnityEngine.Time.frameCount != 1 && !(global::UnityEngine.Mathf.Abs(global::UnityEngine.Time.unscaledDeltaTime) > float.Epsilon)))
		{
			return;
		}
		if (m_linkedCamerasChanged)
		{
			UpdateLinkedCameras();
		}
		m_updateCB.Clear();
		for (int i = 0; i < m_linkedCameraValues.Length; i++)
		{
			if (m_linkedCameraValues[i] != null && m_linkedCameraValues[i].isActiveAndEnabled)
			{
				m_linkedCameraValues[i].UpdateTransform(this, m_updateCB);
			}
		}
	}

	private void OnPostRender()
	{
		bool qualityChanged = QualityLevel != m_qualityLevel;
		m_qualityLevel = QualityLevel;
		UpdateRenderTextures(qualityChanged);
		ResetObjectId();
		bool flag = CameraMotionMult > float.Epsilon;
		bool clearColor = !flag || m_starting;
		float num = ((DepthThreshold > float.Epsilon) ? (1f / DepthThreshold) : float.MaxValue);
		m_motionScaleNorm = ((m_deltaTime >= float.Epsilon) ? (MotionScale * (1f / m_deltaTime)) : 0f);
		m_fixedMotionScaleNorm = ((m_fixedDeltaTime >= float.Epsilon) ? (MotionScale * (1f / m_fixedDeltaTime)) : 0f);
		float scale = ((!m_starting) ? m_motionScaleNorm : 0f);
		float fixedScale = ((!m_starting) ? m_fixedMotionScaleNorm : 0f);
		global::UnityEngine.Shader.SetGlobalFloat("_AM_MIN_VELOCITY", MinVelocity);
		global::UnityEngine.Shader.SetGlobalFloat("_AM_MAX_VELOCITY", MaxVelocity);
		global::UnityEngine.Shader.SetGlobalFloat("_AM_RCP_TOTAL_VELOCITY", 1f / (MaxVelocity - MinVelocity));
		global::UnityEngine.Shader.SetGlobalVector("_AM_DEPTH_THRESHOLD", new global::UnityEngine.Vector2(DepthThreshold, num));
		m_motionRT.DiscardContents();
		m_baseCamera.PreRenderVectors(m_motionRT, clearColor, num);
		for (int i = 0; i < m_linkedCameraValues.Length; i++)
		{
			AmplifyMotionCamera amplifyMotionCamera = m_linkedCameraValues[i];
			if (amplifyMotionCamera != null && amplifyMotionCamera.Overlay && amplifyMotionCamera.isActiveAndEnabled)
			{
				amplifyMotionCamera.PreRenderVectors(m_motionRT, clearColor, num);
				amplifyMotionCamera.RenderVectors(scale, fixedScale, QualityLevel);
			}
		}
		if (flag)
		{
			float num2 = ((m_deltaTime >= float.Epsilon) ? (MotionScale * CameraMotionMult * (1f / m_deltaTime)) : 0f);
			float scale2 = ((!m_starting) ? num2 : 0f);
			m_motionRT.DiscardContents();
			m_baseCamera.RenderReprojectionVectors(m_motionRT, scale2);
		}
		m_baseCamera.RenderVectors(scale, fixedScale, QualityLevel);
		for (int j = 0; j < m_linkedCameraValues.Length; j++)
		{
			AmplifyMotionCamera amplifyMotionCamera2 = m_linkedCameraValues[j];
			if (amplifyMotionCamera2 != null && amplifyMotionCamera2.Overlay && amplifyMotionCamera2.isActiveAndEnabled)
			{
				amplifyMotionCamera2.RenderVectors(scale, fixedScale, QualityLevel);
			}
		}
		m_starting = false;
	}

	private void ApplyMotionBlur(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination, global::UnityEngine.Vector4 blurStep)
	{
		bool flag = QualityLevel == global::AmplifyMotion.Quality.Mobile;
		int pass = (int)(QualityLevel + (Noise ? 4 : 0));
		global::UnityEngine.RenderTexture renderTexture = null;
		if (flag)
		{
			renderTexture = global::UnityEngine.RenderTexture.GetTemporary(m_width, m_height, 0, global::UnityEngine.RenderTextureFormat.ARGB32);
			renderTexture.name = "AM-DepthTemp";
			renderTexture.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
			renderTexture.filterMode = global::UnityEngine.FilterMode.Point;
		}
		global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(m_width, m_height, 0, source.format);
		temporary.name = "AM-CombinedTemp";
		temporary.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		temporary.filterMode = global::UnityEngine.FilterMode.Point;
		temporary.DiscardContents();
		m_combineMaterial.SetTexture("_MotionTex", m_motionRT);
		source.filterMode = global::UnityEngine.FilterMode.Point;
		global::UnityEngine.Graphics.Blit(source, temporary, m_combineMaterial, 0);
		m_blurMaterial.SetTexture("_MotionTex", m_motionRT);
		if (flag)
		{
			global::UnityEngine.Graphics.Blit(null, renderTexture, m_depthMaterial, 0);
			m_blurMaterial.SetTexture("_DepthTex", renderTexture);
		}
		if (QualitySteps > 1)
		{
			global::UnityEngine.RenderTexture temporary2 = global::UnityEngine.RenderTexture.GetTemporary(m_width, m_height, 0, source.format);
			temporary2.name = "AM-CombinedTemp2";
			temporary2.filterMode = global::UnityEngine.FilterMode.Point;
			float num = 1f / (float)QualitySteps;
			float num2 = 1f;
			global::UnityEngine.RenderTexture renderTexture2 = temporary;
			global::UnityEngine.RenderTexture renderTexture3 = temporary2;
			for (int i = 0; i < QualitySteps; i++)
			{
				if (renderTexture3 != destination)
				{
					renderTexture3.DiscardContents();
				}
				m_blurMaterial.SetVector("_AM_BLUR_STEP", blurStep * num2);
				global::UnityEngine.Graphics.Blit(renderTexture2, renderTexture3, m_blurMaterial, pass);
				if (i < QualitySteps - 2)
				{
					global::UnityEngine.RenderTexture renderTexture4 = renderTexture3;
					renderTexture3 = renderTexture2;
					renderTexture2 = renderTexture4;
				}
				else
				{
					renderTexture2 = renderTexture3;
					renderTexture3 = destination;
				}
				num2 -= num;
			}
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary2);
		}
		else
		{
			m_blurMaterial.SetVector("_AM_BLUR_STEP", blurStep);
			global::UnityEngine.Graphics.Blit(temporary, destination, m_blurMaterial, pass);
		}
		if (flag)
		{
			m_combineMaterial.SetTexture("_MotionTex", m_motionRT);
			global::UnityEngine.Graphics.Blit(source, destination, m_combineMaterial, 1);
		}
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
		if (renderTexture != null)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
		}
	}

	private void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (m_currentPostProcess == null)
		{
			PostProcess(source, destination);
		}
		else
		{
			global::UnityEngine.Graphics.Blit(source, destination);
		}
	}

	public void PostProcess(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
		zero.x = MaxVelocity / 1000f;
		zero.y = MaxVelocity / 1000f;
		global::UnityEngine.RenderTexture renderTexture = null;
		if (global::UnityEngine.QualitySettings.antiAliasing > 1)
		{
			renderTexture = global::UnityEngine.RenderTexture.GetTemporary(m_width, m_height, 0, global::UnityEngine.RenderTextureFormat.ARGB32, global::UnityEngine.RenderTextureReadWrite.Linear);
			renderTexture.name = "AM-DilatedTemp";
			renderTexture.filterMode = global::UnityEngine.FilterMode.Point;
			m_dilationMaterial.SetTexture("_MotionTex", m_motionRT);
			global::UnityEngine.Graphics.Blit(m_motionRT, renderTexture, m_dilationMaterial, 0);
			m_dilationMaterial.SetTexture("_MotionTex", renderTexture);
			global::UnityEngine.Graphics.Blit(renderTexture, m_motionRT, m_dilationMaterial, 1);
		}
		if (DebugMode)
		{
			m_debugMaterial.SetTexture("_MotionTex", m_motionRT);
			global::UnityEngine.Graphics.Blit(source, destination, m_debugMaterial);
		}
		else
		{
			ApplyMotionBlur(source, destination, zero);
		}
		if (renderTexture != null)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
		}
	}
}
