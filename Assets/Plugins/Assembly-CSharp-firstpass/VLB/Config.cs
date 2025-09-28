namespace VLB
{
	[global::UnityEngine.HelpURL("http://saladgamer.com/vlb-doc/config/")]
	public class Config : global::UnityEngine.ScriptableObject
	{
		public bool geometryOverrideLayer = true;

		public int geometryLayerID = 1;

		public string geometryTag = "Untagged";

		public int geometryRenderQueue = 3000;

		public global::VLB.RenderPipeline renderPipeline;

		[global::System.Obsolete("Use 'renderingMode' instead")]
		public bool forceSinglePass;

		public global::VLB.RenderingMode renderingMode;

		[global::UnityEngine.SerializeField]
		[global::VLB.HighlightNull]
		private global::UnityEngine.Shader beamShader1Pass;

		[global::UnityEngine.Serialization.FormerlySerializedAs("BeamShader")]
		[global::UnityEngine.Serialization.FormerlySerializedAs("beamShader")]
		[global::UnityEngine.SerializeField]
		[global::VLB.HighlightNull]
		private global::UnityEngine.Shader beamShader2Pass;

		public int sharedMeshSides = 24;

		public int sharedMeshSegments = 5;

		[global::UnityEngine.Range(0.01f, 2f)]
		public float globalNoiseScale = 0.5f;

		public global::UnityEngine.Vector3 globalNoiseVelocity = global::VLB.Consts.NoiseVelocityDefault;

		public string fadeOutCameraTag = "MainCamera";

		[global::VLB.HighlightNull]
		public global::UnityEngine.TextAsset noise3DData;

		public int noise3DSize = 64;

		[global::VLB.HighlightNull]
		public global::UnityEngine.ParticleSystem dustParticlesPrefab;

		[global::UnityEngine.SerializeField]
		private int pluginVersion = -1;

		private global::UnityEngine.Transform m_CachedFadeOutCamera;

		private const string kAssetName = "Config";

		private static global::VLB.Config m_Instance;

		public global::VLB.RenderingMode actualRenderingMode
		{
			get
			{
				_ = renderingMode;
				_ = 2;
				return renderingMode;
			}
		}

		public bool useSinglePassShader => actualRenderingMode != global::VLB.RenderingMode.MultiPass;

		public global::UnityEngine.Shader beamShader
		{
			get
			{
				if (!useSinglePassShader)
				{
					return beamShader2Pass;
				}
				return beamShader1Pass;
			}
		}

		public global::UnityEngine.Vector4 globalNoiseParam => new global::UnityEngine.Vector4(globalNoiseVelocity.x, globalNoiseVelocity.y, globalNoiseVelocity.z, globalNoiseScale);

		public global::UnityEngine.Transform fadeOutCameraTransform
		{
			get
			{
				if (m_CachedFadeOutCamera == null)
				{
					ForceUpdateFadeOutCamera();
				}
				return m_CachedFadeOutCamera;
			}
		}

		public static global::VLB.Config Instance
		{
			get
			{
				if (m_Instance == null)
				{
					global::VLB.ConfigOverride configOverride = global::UnityEngine.Resources.Load<global::VLB.ConfigOverride>("VLBConfigOverride");
					if ((bool)configOverride)
					{
						m_Instance = configOverride;
					}
					else
					{
						m_Instance = global::UnityEngine.Resources.Load<global::VLB.Config>("Config");
					}
				}
				return m_Instance;
			}
		}

		public void ForceUpdateFadeOutCamera()
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.FindGameObjectWithTag(fadeOutCameraTag);
			if ((bool)gameObject)
			{
				m_CachedFadeOutCamera = gameObject.transform;
			}
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod]
		private static void OnStartup()
		{
			Instance.m_CachedFadeOutCamera = null;
			OnRenderPipelineChanged(Instance.renderPipeline);
		}

		public static void OnRenderPipelineChanged(global::VLB.RenderPipeline pipeline)
		{
			bool enabled = global::VLB.BeamGeometry.isCustomRenderPipelineSupported && pipeline == global::VLB.RenderPipeline.SRP_4_0_0_or_higher;
			global::VLB.Utils.SetShaderKeywordEnabled("VLB_SRP_API", enabled);
		}

		public void Reset()
		{
			geometryOverrideLayer = true;
			geometryLayerID = 1;
			geometryTag = "Untagged";
			geometryRenderQueue = 3000;
			beamShader1Pass = global::UnityEngine.Shader.Find("Hidden/VolumetricLightBeam1Pass");
			beamShader2Pass = global::UnityEngine.Shader.Find("Hidden/VolumetricLightBeam2Pass");
			sharedMeshSides = 24;
			sharedMeshSegments = 5;
			globalNoiseScale = 0.5f;
			globalNoiseVelocity = global::VLB.Consts.NoiseVelocityDefault;
			noise3DData = global::UnityEngine.Resources.Load("Noise3D_64x64x64") as global::UnityEngine.TextAsset;
			noise3DSize = 64;
			dustParticlesPrefab = global::UnityEngine.Resources.Load("DustParticles", typeof(global::UnityEngine.ParticleSystem)) as global::UnityEngine.ParticleSystem;
			renderPipeline = global::VLB.RenderPipeline.BuiltIn;
			renderingMode = global::VLB.RenderingMode.MultiPass;
		}

		public global::UnityEngine.ParticleSystem NewVolumetricDustParticles()
		{
			if (!dustParticlesPrefab)
			{
				if (global::UnityEngine.Application.isPlaying)
				{
					global::UnityEngine.Debug.LogError("Failed to instantiate VolumetricDustParticles prefab.");
				}
				return null;
			}
			global::UnityEngine.ParticleSystem particleSystem = global::UnityEngine.Object.Instantiate(dustParticlesPrefab);
			particleSystem.useAutoRandomSeed = false;
			particleSystem.name = "Dust Particles";
			particleSystem.gameObject.hideFlags = global::VLB.Consts.ProceduralObjectsHideFlags;
			particleSystem.gameObject.SetActive(value: true);
			return particleSystem;
		}

		private void OnEnable()
		{
			HandleBackwardCompatibility(pluginVersion, 1700);
			pluginVersion = 1700;
		}

		private void HandleBackwardCompatibility(int serializedVersion, int newVersion)
		{
			if (serializedVersion != newVersion)
			{
				if (serializedVersion < 1600)
				{
					renderingMode = (forceSinglePass ? global::VLB.RenderingMode.SinglePass : global::VLB.RenderingMode.MultiPass);
				}
				global::VLB.Utils.MarkObjectDirty(this);
			}
		}
	}
}
