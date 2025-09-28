namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltScript : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.Header("Lightning General Properties")]
		[global::UnityEngine.Tooltip("The camera the lightning should be shown in. Defaults to the current camera, or the main camera if current camera is null. If you are using a different camera, you may want to put the lightning in it's own layer and cull that layer out of any other cameras.")]
		public global::UnityEngine.Camera Camera;

		[global::UnityEngine.Tooltip("Type of camera mode. Auto detects the camera and creates appropriate lightning. Can be overriden to do something more specific regardless of camera.")]
		public global::DigitalRuby.ThunderAndLightning.CameraMode CameraMode;

		internal global::DigitalRuby.ThunderAndLightning.CameraMode calculatedCameraMode = global::DigitalRuby.ThunderAndLightning.CameraMode.Unknown;

		[global::UnityEngine.Tooltip("True if you are using world space coordinates for the lightning bolt, false if you are using coordinates relative to the parent game object.")]
		public bool UseWorldSpace = true;

		[global::UnityEngine.Tooltip("Whether to compensate for the parent transform. Default is false. If true, rotation, scale and position are altered by the parent transform. Use this to fix scaling, rotation and other offset problems with the lightning.")]
		public bool CompensateForParentTransform;

		[global::UnityEngine.Tooltip("Lightning quality setting. This allows setting limits on generations, lights and shadow casting lights based on the global quality setting.")]
		public global::DigitalRuby.ThunderAndLightning.LightningBoltQualitySetting QualitySetting;

		[global::UnityEngine.Tooltip("Whether to use multi-threaded generation of lightning. Lightning will be delayed by about 1 frame if this is turned on, but this can significantly improve performance.")]
		public bool MultiThreaded;

		[global::UnityEngine.Range(0f, 1000f)]
		[global::UnityEngine.Tooltip("If non-zero, the Camera property is used to get distance of lightning from camera. Lightning generations is reduced for each distance from camera. For example, if LevelOfDetailDistance was 100 and the lightning was 200 away from camera, generations would be reduced by 2, to a minimum of 1.")]
		public float LevelOfDetailDistance;

		[global::UnityEngine.Header("Lightning 2D Settings")]
		[global::UnityEngine.Tooltip("Sort layer name")]
		public string SortLayerName;

		[global::UnityEngine.Tooltip("Order in sort layer")]
		public int SortOrderInLayer;

		[global::UnityEngine.Header("Lightning Rendering Properties")]
		[global::UnityEngine.Tooltip("Soft particles factor. 0.01 to 3.0 are typical, 100.0 to disable.")]
		[global::UnityEngine.Range(0.01f, 100f)]
		public float SoftParticlesFactor = 3f;

		[global::UnityEngine.Tooltip("The render queue for the lightning. -1 for default.")]
		public int RenderQueue = -1;

		[global::UnityEngine.Tooltip("Lightning material for mesh renderer")]
		public global::UnityEngine.Material LightningMaterialMesh;

		[global::UnityEngine.Tooltip("Lightning material for mesh renderer, without glow")]
		public global::UnityEngine.Material LightningMaterialMeshNoGlow;

		[global::UnityEngine.Tooltip("The texture to use for the lightning bolts, or null for the material default texture.")]
		public global::UnityEngine.Texture2D LightningTexture;

		[global::UnityEngine.Tooltip("The texture to use for the lightning glow, or null for the material default texture.")]
		public global::UnityEngine.Texture2D LightningGlowTexture;

		[global::UnityEngine.Tooltip("Particle system to play at the point of emission (start). 'Emission rate' particles will be emitted all at once.")]
		public global::UnityEngine.ParticleSystem LightningOriginParticleSystem;

		[global::UnityEngine.Tooltip("Particle system to play at the point of impact (end). 'Emission rate' particles will be emitted all at once.")]
		public global::UnityEngine.ParticleSystem LightningDestinationParticleSystem;

		[global::UnityEngine.Tooltip("Tint color for the lightning")]
		public global::UnityEngine.Color LightningTintColor = global::UnityEngine.Color.white;

		[global::UnityEngine.Tooltip("Tint color for the lightning glow")]
		public global::UnityEngine.Color GlowTintColor = new global::UnityEngine.Color(0.1f, 0.2f, 1f, 1f);

		[global::UnityEngine.Tooltip("Source blend mode. Default is SrcAlpha.")]
		public global::UnityEngine.Rendering.BlendMode SourceBlendMode = global::UnityEngine.Rendering.BlendMode.SrcAlpha;

		[global::UnityEngine.Tooltip("Destination blend mode. Default is One. For additive blend use One. For alpha blend use OneMinusSrcAlpha.")]
		public global::UnityEngine.Rendering.BlendMode DestinationBlendMode = global::UnityEngine.Rendering.BlendMode.One;

		[global::UnityEngine.Header("Lightning Movement Properties")]
		[global::UnityEngine.Tooltip("Jitter multiplier to randomize lightning size. Jitter depends on trunk width and will make the lightning move rapidly and jaggedly, giving a more lively and sometimes cartoony feel. Jitter may be shared with other bolts depending on materials. If you need different jitters for the same material, create a second script object.")]
		public float JitterMultiplier;

		[global::UnityEngine.Tooltip("Built in turbulance based on the direction of each segment. Small values usually work better, like 0.2.")]
		public float Turbulence;

		[global::UnityEngine.Tooltip("Global turbulence velocity for this script")]
		public global::UnityEngine.Vector3 TurbulenceVelocity = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.Tooltip("Cause lightning to flicker, x = min, y = max, z = time multiplier, w = add to intensity")]
		public global::UnityEngine.Vector4 IntensityFlicker = intensityFlickerDefault;

		private static readonly global::UnityEngine.Vector4 intensityFlickerDefault = new global::UnityEngine.Vector4(1f, 1f, 1f, 0f);

		[global::UnityEngine.Tooltip("Lightning intensity flicker lookup texture")]
		public global::UnityEngine.Texture2D IntensityFlickerTexture;

		public static float TimeScale = 1f;

		private static bool needsTimeUpdate = true;

		private global::UnityEngine.Texture2D lastLightningTexture;

		private global::UnityEngine.Texture2D lastLightningGlowTexture;

		private readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBolt> activeBolts = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBolt>();

		private readonly global::DigitalRuby.ThunderAndLightning.LightningBoltParameters[] oneParameterArray = new global::DigitalRuby.ThunderAndLightning.LightningBoltParameters[1];

		private readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBolt> lightningBoltCache = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBolt>();

		private readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies> dependenciesCache = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies>();

		private global::DigitalRuby.ThunderAndLightning.LightningThreadState threadState;

		private static int shaderId_MainTex = int.MinValue;

		private static int shaderId_GlowTex;

		private static int shaderId_TintColor;

		private static int shaderId_GlowTintColor;

		private static int shaderId_JitterMultiplier;

		private static int shaderId_Turbulence;

		private static int shaderId_TurbulenceVelocity;

		private static int shaderId_SrcBlendMode;

		private static int shaderId_DstBlendMode;

		private static int shaderId_InvFade;

		private static int shaderId_LightningTime;

		private static int shaderId_IntensityFlicker;

		private static int shaderId_IntensityFlickerTexture;

		public global::System.Action<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters, global::UnityEngine.Vector3, global::UnityEngine.Vector3> LightningStartedCallback { get; set; }

		public global::System.Action<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters, global::UnityEngine.Vector3, global::UnityEngine.Vector3> LightningEndedCallback { get; set; }

		public global::System.Action<global::UnityEngine.Light> LightAddedCallback { get; set; }

		public global::System.Action<global::UnityEngine.Light> LightRemovedCallback { get; set; }

		public bool HasActiveBolts => activeBolts.Count != 0;

		public static global::UnityEngine.Vector4 TimeVectorSinceStart { get; private set; }

		public static float TimeSinceStart { get; private set; }

		public static float DeltaTime { get; private set; }

		internal global::UnityEngine.Material lightningMaterialMeshInternal { get; private set; }

		internal global::UnityEngine.Material lightningMaterialMeshNoGlowInternal { get; private set; }

		public virtual void CreateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p)
		{
			if (p != null && Camera != null)
			{
				UpdateTexture();
				oneParameterArray[0] = p;
				global::DigitalRuby.ThunderAndLightning.LightningBolt orCreateLightningBolt = GetOrCreateLightningBolt();
				global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies dependencies = CreateLightningBoltDependencies(oneParameterArray);
				orCreateLightningBolt.SetupLightningBolt(dependencies);
			}
		}

		public void CreateLightningBolts(global::System.Collections.Generic.ICollection<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters> parameters)
		{
			if (parameters != null && parameters.Count != 0 && Camera != null)
			{
				UpdateTexture();
				global::DigitalRuby.ThunderAndLightning.LightningBolt orCreateLightningBolt = GetOrCreateLightningBolt();
				global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies dependencies = CreateLightningBoltDependencies(parameters);
				orCreateLightningBolt.SetupLightningBolt(dependencies);
			}
		}

		protected virtual void Awake()
		{
			UpdateShaderIds();
		}

		protected virtual void Start()
		{
			UpdateCamera();
			UpdateMaterialsForLastTexture();
			UpdateShaderParameters();
			CheckCompensateForParentTransform();
			global::UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
			if (MultiThreaded)
			{
				threadState = new global::DigitalRuby.ThunderAndLightning.LightningThreadState();
			}
		}

		protected virtual void Update()
		{
			if (needsTimeUpdate)
			{
				needsTimeUpdate = false;
				DeltaTime = global::UnityEngine.Time.unscaledDeltaTime * TimeScale;
				TimeSinceStart += DeltaTime;
			}
			if (HasActiveBolts)
			{
				UpdateCamera();
				UpdateShaderParameters();
				CheckCompensateForParentTransform();
				UpdateActiveBolts();
				global::UnityEngine.Shader.SetGlobalVector(shaderId_LightningTime, TimeVectorSinceStart = new global::UnityEngine.Vector4(TimeSinceStart * 0.05f, TimeSinceStart, TimeSinceStart * 2f, TimeSinceStart * 3f));
			}
			if (threadState != null)
			{
				threadState.UpdateMainThreadActions();
			}
		}

		protected virtual void LateUpdate()
		{
			needsTimeUpdate = true;
		}

		protected virtual global::DigitalRuby.ThunderAndLightning.LightningBoltParameters OnCreateParameters()
		{
			return global::DigitalRuby.ThunderAndLightning.LightningBoltParameters.GetOrCreateParameters();
		}

		protected global::DigitalRuby.ThunderAndLightning.LightningBoltParameters CreateParameters()
		{
			global::DigitalRuby.ThunderAndLightning.LightningBoltParameters lightningBoltParameters = OnCreateParameters();
			lightningBoltParameters.quality = QualitySetting;
			PopulateParameters(lightningBoltParameters);
			return lightningBoltParameters;
		}

		protected virtual void PopulateParameters(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p)
		{
		}

		private global::UnityEngine.Coroutine StartCoroutineWrapper(global::System.Collections.IEnumerator routine)
		{
			if (base.isActiveAndEnabled)
			{
				return StartCoroutine(routine);
			}
			return null;
		}

		private void OnSceneLoaded(global::UnityEngine.SceneManagement.Scene arg0, global::UnityEngine.SceneManagement.LoadSceneMode arg1)
		{
			global::DigitalRuby.ThunderAndLightning.LightningBolt.ClearCache();
		}

		private global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies CreateLightningBoltDependencies(global::System.Collections.Generic.ICollection<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters> parameters)
		{
			global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies lightningBoltDependencies;
			if (dependenciesCache.Count == 0)
			{
				lightningBoltDependencies = new global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies();
				lightningBoltDependencies.AddActiveBolt = AddActiveBolt;
				lightningBoltDependencies.LightAdded = OnLightAdded;
				lightningBoltDependencies.LightRemoved = OnLightRemoved;
				lightningBoltDependencies.ReturnToCache = ReturnLightningDependenciesToCache;
				lightningBoltDependencies.StartCoroutine = StartCoroutineWrapper;
				lightningBoltDependencies.Parent = base.gameObject;
			}
			else
			{
				int index = dependenciesCache.Count - 1;
				lightningBoltDependencies = dependenciesCache[index];
				dependenciesCache.RemoveAt(index);
			}
			lightningBoltDependencies.CameraPos = Camera.transform.position;
			lightningBoltDependencies.CameraIsOrthographic = Camera.orthographic;
			lightningBoltDependencies.CameraMode = calculatedCameraMode;
			lightningBoltDependencies.LevelOfDetailDistance = LevelOfDetailDistance;
			lightningBoltDependencies.DestParticleSystem = LightningDestinationParticleSystem;
			lightningBoltDependencies.LightningMaterialMesh = lightningMaterialMeshInternal;
			lightningBoltDependencies.LightningMaterialMeshNoGlow = lightningMaterialMeshNoGlowInternal;
			lightningBoltDependencies.OriginParticleSystem = LightningOriginParticleSystem;
			lightningBoltDependencies.SortLayerName = SortLayerName;
			lightningBoltDependencies.SortOrderInLayer = SortOrderInLayer;
			lightningBoltDependencies.UseWorldSpace = UseWorldSpace;
			lightningBoltDependencies.ThreadState = threadState;
			if (threadState != null)
			{
				lightningBoltDependencies.Parameters = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters>(parameters);
			}
			else
			{
				lightningBoltDependencies.Parameters = parameters;
			}
			lightningBoltDependencies.LightningBoltStarted = LightningStartedCallback;
			lightningBoltDependencies.LightningBoltEnded = LightningEndedCallback;
			return lightningBoltDependencies;
		}

		private void ReturnLightningDependenciesToCache(global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies d)
		{
			d.Parameters = null;
			d.OriginParticleSystem = null;
			d.DestParticleSystem = null;
			d.LightningMaterialMesh = null;
			d.LightningMaterialMeshNoGlow = null;
			dependenciesCache.Add(d);
		}

		internal void OnLightAdded(global::UnityEngine.Light l)
		{
			if (LightAddedCallback != null)
			{
				LightAddedCallback(l);
			}
		}

		internal void OnLightRemoved(global::UnityEngine.Light l)
		{
			if (LightRemovedCallback != null)
			{
				LightRemovedCallback(l);
			}
		}

		internal void AddActiveBolt(global::DigitalRuby.ThunderAndLightning.LightningBolt bolt)
		{
			activeBolts.Add(bolt);
		}

		private void UpdateShaderIds()
		{
			if (shaderId_MainTex == int.MinValue)
			{
				shaderId_MainTex = global::UnityEngine.Shader.PropertyToID("_MainTex");
				shaderId_GlowTex = global::UnityEngine.Shader.PropertyToID("_GlowTex");
				shaderId_TintColor = global::UnityEngine.Shader.PropertyToID("_TintColor");
				shaderId_GlowTintColor = global::UnityEngine.Shader.PropertyToID("_GlowTintColor");
				shaderId_JitterMultiplier = global::UnityEngine.Shader.PropertyToID("_JitterMultiplier");
				shaderId_Turbulence = global::UnityEngine.Shader.PropertyToID("_Turbulence");
				shaderId_TurbulenceVelocity = global::UnityEngine.Shader.PropertyToID("_TurbulenceVelocity");
				shaderId_SrcBlendMode = global::UnityEngine.Shader.PropertyToID("_SrcBlendMode");
				shaderId_DstBlendMode = global::UnityEngine.Shader.PropertyToID("_DstBlendMode");
				shaderId_InvFade = global::UnityEngine.Shader.PropertyToID("_InvFade");
				shaderId_LightningTime = global::UnityEngine.Shader.PropertyToID("_LightningTime");
				shaderId_IntensityFlicker = global::UnityEngine.Shader.PropertyToID("_IntensityFlicker");
				shaderId_IntensityFlickerTexture = global::UnityEngine.Shader.PropertyToID("_IntensityFlickerTexture");
			}
		}

		private void UpdateMaterialsForLastTexture()
		{
			if (global::UnityEngine.Application.isPlaying)
			{
				calculatedCameraMode = global::DigitalRuby.ThunderAndLightning.CameraMode.Unknown;
				lightningMaterialMeshInternal = new global::UnityEngine.Material(LightningMaterialMesh);
				lightningMaterialMeshNoGlowInternal = new global::UnityEngine.Material(LightningMaterialMeshNoGlow);
				if (LightningTexture != null)
				{
					lightningMaterialMeshInternal.SetTexture(shaderId_MainTex, LightningTexture);
					lightningMaterialMeshNoGlowInternal.SetTexture(shaderId_MainTex, LightningTexture);
				}
				if (LightningGlowTexture != null)
				{
					lightningMaterialMeshInternal.SetTexture(shaderId_GlowTex, LightningGlowTexture);
				}
				SetupMaterialCamera();
			}
		}

		private void UpdateTexture()
		{
			if (LightningTexture != null && LightningTexture != lastLightningTexture)
			{
				lastLightningTexture = LightningTexture;
				UpdateMaterialsForLastTexture();
			}
			if (LightningGlowTexture != null && LightningGlowTexture != lastLightningGlowTexture)
			{
				lastLightningGlowTexture = LightningGlowTexture;
				UpdateMaterialsForLastTexture();
			}
		}

		private void SetMaterialPerspective()
		{
			if (calculatedCameraMode != global::DigitalRuby.ThunderAndLightning.CameraMode.Perspective)
			{
				calculatedCameraMode = global::DigitalRuby.ThunderAndLightning.CameraMode.Perspective;
				lightningMaterialMeshInternal.EnableKeyword("PERSPECTIVE");
				lightningMaterialMeshNoGlowInternal.EnableKeyword("PERSPECTIVE");
				lightningMaterialMeshInternal.DisableKeyword("ORTHOGRAPHIC_XY");
				lightningMaterialMeshNoGlowInternal.DisableKeyword("ORTHOGRAPHIC_XY");
				lightningMaterialMeshInternal.DisableKeyword("ORTHOGRAPHIC_XZ");
				lightningMaterialMeshNoGlowInternal.DisableKeyword("ORTHOGRAPHIC_XZ");
			}
		}

		private void SetMaterialOrthographicXY()
		{
			if (calculatedCameraMode != global::DigitalRuby.ThunderAndLightning.CameraMode.OrthographicXY)
			{
				calculatedCameraMode = global::DigitalRuby.ThunderAndLightning.CameraMode.OrthographicXY;
				lightningMaterialMeshInternal.EnableKeyword("ORTHOGRAPHIC_XY");
				lightningMaterialMeshNoGlowInternal.EnableKeyword("ORTHOGRAPHIC_XY");
				lightningMaterialMeshInternal.DisableKeyword("ORTHOGRAPHIC_XZ");
				lightningMaterialMeshNoGlowInternal.DisableKeyword("ORTHOGRAPHIC_XZ");
				lightningMaterialMeshInternal.DisableKeyword("PERSPECTIVE");
				lightningMaterialMeshNoGlowInternal.DisableKeyword("PERSPECTIVE");
			}
		}

		private void SetMaterialOrthographicXZ()
		{
			if (calculatedCameraMode != global::DigitalRuby.ThunderAndLightning.CameraMode.OrthographicXZ)
			{
				calculatedCameraMode = global::DigitalRuby.ThunderAndLightning.CameraMode.OrthographicXZ;
				lightningMaterialMeshInternal.EnableKeyword("ORTHOGRAPHIC_XZ");
				lightningMaterialMeshNoGlowInternal.EnableKeyword("ORTHOGRAPHIC_XZ");
				lightningMaterialMeshInternal.DisableKeyword("ORTHOGRAPHIC_XY");
				lightningMaterialMeshNoGlowInternal.DisableKeyword("ORTHOGRAPHIC_XY");
				lightningMaterialMeshInternal.DisableKeyword("PERSPECTIVE");
				lightningMaterialMeshNoGlowInternal.DisableKeyword("PERSPECTIVE");
			}
		}

		private void SetupMaterialCamera()
		{
			if (Camera == null && CameraMode == global::DigitalRuby.ThunderAndLightning.CameraMode.Auto)
			{
				SetMaterialPerspective();
			}
			else if (CameraMode == global::DigitalRuby.ThunderAndLightning.CameraMode.Auto)
			{
				if (Camera.orthographic)
				{
					SetMaterialOrthographicXY();
				}
				else
				{
					SetMaterialPerspective();
				}
			}
			else if (CameraMode == global::DigitalRuby.ThunderAndLightning.CameraMode.Perspective)
			{
				SetMaterialPerspective();
			}
			else if (CameraMode == global::DigitalRuby.ThunderAndLightning.CameraMode.OrthographicXY)
			{
				SetMaterialOrthographicXY();
			}
			else
			{
				SetMaterialOrthographicXZ();
			}
		}

		private void EnableKeyword(string keyword, bool enable, global::UnityEngine.Material m)
		{
			if (enable)
			{
				m.EnableKeyword(keyword);
			}
			else
			{
				m.DisableKeyword(keyword);
			}
		}

		private void UpdateShaderParameters()
		{
			lightningMaterialMeshInternal.SetColor(shaderId_TintColor, LightningTintColor);
			lightningMaterialMeshInternal.SetColor(shaderId_GlowTintColor, GlowTintColor);
			lightningMaterialMeshInternal.SetFloat(shaderId_JitterMultiplier, JitterMultiplier);
			lightningMaterialMeshInternal.SetFloat(shaderId_Turbulence, Turbulence * global::DigitalRuby.ThunderAndLightning.LightningBoltParameters.Scale);
			lightningMaterialMeshInternal.SetVector(shaderId_TurbulenceVelocity, TurbulenceVelocity * global::DigitalRuby.ThunderAndLightning.LightningBoltParameters.Scale);
			lightningMaterialMeshInternal.SetInt(shaderId_SrcBlendMode, (int)SourceBlendMode);
			lightningMaterialMeshInternal.SetInt(shaderId_DstBlendMode, (int)DestinationBlendMode);
			lightningMaterialMeshInternal.renderQueue = RenderQueue;
			lightningMaterialMeshInternal.SetFloat(shaderId_InvFade, SoftParticlesFactor);
			lightningMaterialMeshNoGlowInternal.SetColor(shaderId_TintColor, LightningTintColor);
			lightningMaterialMeshNoGlowInternal.SetFloat(shaderId_JitterMultiplier, JitterMultiplier);
			lightningMaterialMeshNoGlowInternal.SetFloat(shaderId_Turbulence, Turbulence * global::DigitalRuby.ThunderAndLightning.LightningBoltParameters.Scale);
			lightningMaterialMeshNoGlowInternal.SetVector(shaderId_TurbulenceVelocity, TurbulenceVelocity * global::DigitalRuby.ThunderAndLightning.LightningBoltParameters.Scale);
			lightningMaterialMeshNoGlowInternal.SetInt(shaderId_SrcBlendMode, (int)SourceBlendMode);
			lightningMaterialMeshNoGlowInternal.SetInt(shaderId_DstBlendMode, (int)DestinationBlendMode);
			lightningMaterialMeshNoGlowInternal.renderQueue = RenderQueue;
			lightningMaterialMeshNoGlowInternal.SetFloat(shaderId_InvFade, SoftParticlesFactor);
			if (IntensityFlicker != intensityFlickerDefault && IntensityFlickerTexture != null)
			{
				lightningMaterialMeshInternal.SetVector(shaderId_IntensityFlicker, IntensityFlicker);
				lightningMaterialMeshInternal.SetTexture(shaderId_IntensityFlickerTexture, IntensityFlickerTexture);
				lightningMaterialMeshNoGlowInternal.SetVector(shaderId_IntensityFlicker, IntensityFlicker);
				lightningMaterialMeshNoGlowInternal.SetTexture(shaderId_IntensityFlickerTexture, IntensityFlickerTexture);
				lightningMaterialMeshInternal.EnableKeyword("INTENSITY_FLICKER");
				lightningMaterialMeshNoGlowInternal.EnableKeyword("INTENSITY_FLICKER");
			}
			else
			{
				lightningMaterialMeshInternal.DisableKeyword("INTENSITY_FLICKER");
				lightningMaterialMeshNoGlowInternal.DisableKeyword("INTENSITY_FLICKER");
			}
			SetupMaterialCamera();
		}

		private void CheckCompensateForParentTransform()
		{
			if (CompensateForParentTransform)
			{
				global::UnityEngine.Transform parent = base.transform.parent;
				if (parent != null)
				{
					base.transform.position = parent.position;
					base.transform.localScale = new global::UnityEngine.Vector3(1f / parent.localScale.x, 1f / parent.localScale.y, 1f / parent.localScale.z);
					base.transform.rotation = parent.rotation;
				}
			}
		}

		private void UpdateCamera()
		{
			Camera = ((!(Camera == null)) ? Camera : ((global::UnityEngine.Camera.current == null) ? global::UnityEngine.Camera.main : global::UnityEngine.Camera.current));
		}

		private global::DigitalRuby.ThunderAndLightning.LightningBolt GetOrCreateLightningBolt()
		{
			if (lightningBoltCache.Count == 0)
			{
				return new global::DigitalRuby.ThunderAndLightning.LightningBolt();
			}
			global::DigitalRuby.ThunderAndLightning.LightningBolt result = lightningBoltCache[lightningBoltCache.Count - 1];
			lightningBoltCache.RemoveAt(lightningBoltCache.Count - 1);
			return result;
		}

		private void UpdateActiveBolts()
		{
			for (int num = activeBolts.Count - 1; num >= 0; num--)
			{
				global::DigitalRuby.ThunderAndLightning.LightningBolt lightningBolt = activeBolts[num];
				if (!lightningBolt.Update())
				{
					activeBolts.RemoveAt(num);
					lightningBolt.Cleanup();
					lightningBoltCache.Add(lightningBolt);
				}
			}
		}

		private void OnApplicationQuit()
		{
			if (threadState != null)
			{
				threadState.Running = false;
			}
		}

		private void Cleanup()
		{
			foreach (global::DigitalRuby.ThunderAndLightning.LightningBolt activeBolt in activeBolts)
			{
				activeBolt.Cleanup();
			}
			activeBolts.Clear();
		}

		private void OnDestroy()
		{
			if (threadState != null)
			{
				threadState.TerminateAndWaitForEnd();
			}
			if (lightningMaterialMeshInternal != null)
			{
				global::UnityEngine.Object.Destroy(lightningMaterialMeshInternal);
			}
			if (lightningMaterialMeshNoGlowInternal != null)
			{
				global::UnityEngine.Object.Destroy(lightningMaterialMeshNoGlowInternal);
			}
			Cleanup();
		}

		private void OnDisable()
		{
			Cleanup();
		}
	}
}
