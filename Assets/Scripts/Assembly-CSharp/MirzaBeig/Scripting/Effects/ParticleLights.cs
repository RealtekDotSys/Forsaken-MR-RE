namespace MirzaBeig.Scripting.Effects
{
	[global::System.Serializable]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.ParticleSystem))]
	public class ParticleLights : global::UnityEngine.MonoBehaviour
	{
		private global::UnityEngine.ParticleSystem ps;

		private global::System.Collections.Generic.List<global::UnityEngine.Light> lights = new global::System.Collections.Generic.List<global::UnityEngine.Light>();

		public float scale = 2f;

		[global::UnityEngine.Range(0f, 8f)]
		public float intensity = 8f;

		public global::UnityEngine.Color colour = global::UnityEngine.Color.white;

		[global::UnityEngine.Range(0f, 1f)]
		public float colourFromParticle = 1f;

		public global::UnityEngine.LightShadows shadows;

		private global::UnityEngine.GameObject template;

		private void Awake()
		{
		}

		private void Start()
		{
			ps = GetComponent<global::UnityEngine.ParticleSystem>();
			template = new global::UnityEngine.GameObject();
			template.transform.SetParent(base.transform);
			template.name = "Template";
		}

		private void Update()
		{
		}

		private void LateUpdate()
		{
			global::UnityEngine.ParticleSystem.Particle[] array = new global::UnityEngine.ParticleSystem.Particle[ps.particleCount];
			int particles = ps.GetParticles(array);
			if (lights.Count != particles)
			{
				for (int i = 0; i < lights.Count; i++)
				{
					global::UnityEngine.Object.Destroy(lights[i].gameObject);
				}
				lights.Clear();
				for (int j = 0; j < particles; j++)
				{
					global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(template, base.transform);
					gameObject.name = "- " + (j + 1);
					lights.Add(gameObject.AddComponent<global::UnityEngine.Light>());
				}
			}
			bool flag = ps.main.simulationSpace == global::UnityEngine.ParticleSystemSimulationSpace.World;
			for (int k = 0; k < particles; k++)
			{
				global::UnityEngine.ParticleSystem.Particle particle = array[k];
				global::UnityEngine.Light light = lights[k];
				light.range = particle.GetCurrentSize(ps) * scale;
				light.color = global::UnityEngine.Color.Lerp(colour, particle.GetCurrentColor(ps), colourFromParticle);
				light.intensity = intensity;
				light.shadows = shadows;
				light.transform.position = (flag ? particle.position : ps.transform.TransformPoint(particle.position));
			}
		}
	}
}
