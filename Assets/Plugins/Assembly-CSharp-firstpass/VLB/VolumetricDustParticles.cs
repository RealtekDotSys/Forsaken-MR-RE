namespace VLB
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.RequireComponent(typeof(global::VLB.VolumetricLightBeam))]
	[global::UnityEngine.HelpURL("http://saladgamer.com/vlb-doc/comp-dustparticles/")]
	public class VolumetricDustParticles : global::UnityEngine.MonoBehaviour
	{
		public enum Direction
		{
			Beam = 0,
			Random = 1
		}

		[global::UnityEngine.Range(0f, 1f)]
		public float alpha = 0.5f;

		[global::UnityEngine.Range(0.0001f, 0.1f)]
		public float size = 0.01f;

		public global::VLB.VolumetricDustParticles.Direction direction = global::VLB.VolumetricDustParticles.Direction.Random;

		public float speed = 0.03f;

		public float density = 5f;

		[global::UnityEngine.Range(0f, 1f)]
		public float spawnMaxDistance = 0.7f;

		public bool cullingEnabled = true;

		public float cullingMaxDistance = 10f;

		public static bool isFeatureSupported = true;

		private global::UnityEngine.ParticleSystem m_Particles;

		private global::UnityEngine.ParticleSystemRenderer m_Renderer;

		private static bool ms_NoMainCameraLogged = false;

		private static global::UnityEngine.Camera ms_MainCamera = null;

		private global::VLB.VolumetricLightBeam m_Master;

		public bool isCulled { get; private set; }

		public bool particlesAreInstantiated => m_Particles;

		public int particlesCurrentCount
		{
			get
			{
				if (!m_Particles)
				{
					return 0;
				}
				return m_Particles.particleCount;
			}
		}

		public int particlesMaxCount
		{
			get
			{
				if (!m_Particles)
				{
					return 0;
				}
				return m_Particles.main.maxParticles;
			}
		}

		public global::UnityEngine.Camera mainCamera
		{
			get
			{
				if (!ms_MainCamera)
				{
					ms_MainCamera = global::UnityEngine.Camera.main;
					if (!ms_MainCamera && !ms_NoMainCameraLogged)
					{
						global::UnityEngine.Debug.LogErrorFormat(base.gameObject, "In order to use 'VolumetricDustParticles' culling, you must have a MainCamera defined in your scene.");
						ms_NoMainCameraLogged = true;
					}
				}
				return ms_MainCamera;
			}
		}

		private void Start()
		{
			isCulled = false;
			m_Master = GetComponent<global::VLB.VolumetricLightBeam>();
			InstantiateParticleSystem();
			SetActiveAndPlay();
		}

		private void InstantiateParticleSystem()
		{
			global::UnityEngine.ParticleSystem[] componentsInChildren = GetComponentsInChildren<global::UnityEngine.ParticleSystem>(includeInactive: true);
			for (int num = componentsInChildren.Length - 1; num >= 0; num--)
			{
				global::UnityEngine.Object.DestroyImmediate(componentsInChildren[num].gameObject);
			}
			m_Particles = global::VLB.Config.Instance.NewVolumetricDustParticles();
			if ((bool)m_Particles)
			{
				m_Particles.transform.SetParent(base.transform, worldPositionStays: false);
				m_Renderer = m_Particles.GetComponent<global::UnityEngine.ParticleSystemRenderer>();
			}
		}

		private void OnEnable()
		{
			SetActiveAndPlay();
		}

		private void SetActiveAndPlay()
		{
			if ((bool)m_Particles)
			{
				m_Particles.gameObject.SetActive(value: true);
				SetParticleProperties();
				m_Particles.Play(withChildren: true);
			}
		}

		private void OnDisable()
		{
			if ((bool)m_Particles)
			{
				m_Particles.gameObject.SetActive(value: false);
			}
		}

		private void OnDestroy()
		{
			if ((bool)m_Particles)
			{
				global::UnityEngine.Object.DestroyImmediate(m_Particles.gameObject);
			}
			m_Particles = null;
		}

		private void Update()
		{
			if (global::UnityEngine.Application.isPlaying)
			{
				UpdateCulling();
			}
			SetParticleProperties();
		}

		private void SetParticleProperties()
		{
			if (!m_Particles || !m_Particles.gameObject.activeSelf)
			{
				return;
			}
			float t = global::UnityEngine.Mathf.Clamp01(1f - m_Master.fresnelPow / 10f);
			float num = m_Master.fallOffEnd * spawnMaxDistance;
			float num2 = num * density;
			int maxParticles = (int)(num2 * 4f);
			global::UnityEngine.ParticleSystem.MainModule main = m_Particles.main;
			global::UnityEngine.ParticleSystem.MinMaxCurve startLifetime = main.startLifetime;
			startLifetime.mode = global::UnityEngine.ParticleSystemCurveMode.TwoConstants;
			startLifetime.constantMin = 4f;
			startLifetime.constantMax = 6f;
			main.startLifetime = startLifetime;
			global::UnityEngine.ParticleSystem.MinMaxCurve startSize = main.startSize;
			startSize.mode = global::UnityEngine.ParticleSystemCurveMode.TwoConstants;
			startSize.constantMin = size * 0.9f;
			startSize.constantMax = size * 1.1f;
			main.startSize = startSize;
			global::UnityEngine.ParticleSystem.MinMaxGradient startColor = main.startColor;
			if (m_Master.colorMode == global::VLB.ColorMode.Flat)
			{
				startColor.mode = global::UnityEngine.ParticleSystemGradientMode.Color;
				global::UnityEngine.Color color = m_Master.color;
				color.a *= alpha;
				startColor.color = color;
			}
			else
			{
				startColor.mode = global::UnityEngine.ParticleSystemGradientMode.Gradient;
				global::UnityEngine.Gradient colorGradient = m_Master.colorGradient;
				global::UnityEngine.GradientColorKey[] colorKeys = colorGradient.colorKeys;
				global::UnityEngine.GradientAlphaKey[] alphaKeys = colorGradient.alphaKeys;
				for (int i = 0; i < alphaKeys.Length; i++)
				{
					alphaKeys[i].alpha *= alpha;
				}
				global::UnityEngine.Gradient gradient = new global::UnityEngine.Gradient();
				gradient.SetKeys(colorKeys, alphaKeys);
				startColor.gradient = gradient;
			}
			main.startColor = startColor;
			global::UnityEngine.ParticleSystem.MinMaxCurve startSpeed = main.startSpeed;
			startSpeed.constant = speed;
			main.startSpeed = startSpeed;
			main.maxParticles = maxParticles;
			global::UnityEngine.ParticleSystem.ShapeModule shape = m_Particles.shape;
			shape.shapeType = global::UnityEngine.ParticleSystemShapeType.ConeVolume;
			shape.radius = m_Master.coneRadiusStart * global::UnityEngine.Mathf.Lerp(0.3f, 1f, t);
			shape.angle = m_Master.coneAngle * 0.5f * global::UnityEngine.Mathf.Lerp(0.7f, 1f, t);
			shape.length = num;
			shape.arc = 360f;
			shape.randomDirectionAmount = ((direction == global::VLB.VolumetricDustParticles.Direction.Random) ? 1f : 0f);
			global::UnityEngine.ParticleSystem.EmissionModule emission = m_Particles.emission;
			global::UnityEngine.ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;
			rateOverTime.constant = num2;
			emission.rateOverTime = rateOverTime;
			if ((bool)m_Renderer)
			{
				m_Renderer.sortingLayerID = m_Master.sortingLayerID;
				m_Renderer.sortingOrder = m_Master.sortingOrder;
			}
		}

		private void UpdateCulling()
		{
			if (!m_Particles)
			{
				return;
			}
			bool flag = true;
			if (cullingEnabled && m_Master.hasGeometry)
			{
				if ((bool)mainCamera)
				{
					float num = cullingMaxDistance * cullingMaxDistance;
					flag = m_Master.bounds.SqrDistance(mainCamera.transform.position) <= num;
				}
				else
				{
					cullingEnabled = false;
				}
			}
			if (m_Particles.gameObject.activeSelf != flag)
			{
				m_Particles.gameObject.SetActive(flag);
				isCulled = !flag;
			}
			if (flag && !m_Particles.isPlaying)
			{
				m_Particles.Play();
			}
		}
	}
}
