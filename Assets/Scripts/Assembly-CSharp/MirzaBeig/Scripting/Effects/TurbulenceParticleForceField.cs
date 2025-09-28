namespace MirzaBeig.Scripting.Effects
{
	[global::UnityEngine.AddComponentMenu("Effects/Particle Force Fields/Turbulence Particle Force Field")]
	public class TurbulenceParticleForceField : global::MirzaBeig.Scripting.Effects.ParticleForceField
	{
		public enum NoiseType
		{
			PseudoPerlin = 0,
			Perlin = 1,
			Simplex = 2,
			OctavePerlin = 3,
			OctaveSimplex = 4
		}

		[global::UnityEngine.Header("ForceField Controls")]
		[global::UnityEngine.Tooltip("Noise texture mutation speed.")]
		public float scrollSpeed = 1f;

		[global::UnityEngine.Range(0f, 8f)]
		[global::UnityEngine.Tooltip("Noise texture detail amplifier.")]
		public float frequency = 1f;

		public global::MirzaBeig.Scripting.Effects.TurbulenceParticleForceField.NoiseType noiseType = global::MirzaBeig.Scripting.Effects.TurbulenceParticleForceField.NoiseType.Perlin;

		[global::UnityEngine.Header("Octave Variant-Only Controls")]
		[global::UnityEngine.Range(1f, 8f)]
		[global::UnityEngine.Tooltip("Overlapping noise iterations. 1 = no additional iterations.")]
		public int octaves = 1;

		[global::UnityEngine.Range(0f, 4f)]
		[global::UnityEngine.Tooltip("Frequency scale per-octave. Can be used to change the overlap every iteration.")]
		public float octaveMultiplier = 0.5f;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.Tooltip("Amplitude scale per-octave. Can be used to change the overlap every iteration.")]
		public float octaveScale = 2f;

		private float time;

		private float randomX;

		private float randomY;

		private float randomZ;

		private float offsetX;

		private float offsetY;

		private float offsetZ;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
			randomX = global::UnityEngine.Random.Range(-32f, 32f);
			randomY = global::UnityEngine.Random.Range(-32f, 32f);
			randomZ = global::UnityEngine.Random.Range(-32f, 32f);
		}

		protected override void Update()
		{
			time = global::UnityEngine.Time.time;
			base.Update();
		}

		protected override void LateUpdate()
		{
			offsetX = time * scrollSpeed + randomX;
			offsetY = time * scrollSpeed + randomY;
			offsetZ = time * scrollSpeed + randomZ;
			base.LateUpdate();
		}

		protected override global::UnityEngine.Vector3 GetForce()
		{
			float num = parameters.particlePosition.x + offsetX;
			float num2 = parameters.particlePosition.y + offsetX;
			float num3 = parameters.particlePosition.z + offsetX;
			float num4 = parameters.particlePosition.x + offsetY;
			float num5 = parameters.particlePosition.y + offsetY;
			float num6 = parameters.particlePosition.z + offsetY;
			float num7 = parameters.particlePosition.x + offsetZ;
			float num8 = parameters.particlePosition.y + offsetZ;
			float num9 = parameters.particlePosition.z + offsetZ;
			global::UnityEngine.Vector3 result = default(global::UnityEngine.Vector3);
			switch (noiseType)
			{
			case global::MirzaBeig.Scripting.Effects.TurbulenceParticleForceField.NoiseType.PseudoPerlin:
			{
				float t = global::UnityEngine.Mathf.PerlinNoise(num * frequency, num5 * frequency);
				float t2 = global::UnityEngine.Mathf.PerlinNoise(num * frequency, num6 * frequency);
				float t3 = global::UnityEngine.Mathf.PerlinNoise(num * frequency, num4 * frequency);
				t = global::UnityEngine.Mathf.Lerp(-1f, 1f, t);
				t2 = global::UnityEngine.Mathf.Lerp(-1f, 1f, t2);
				t3 = global::UnityEngine.Mathf.Lerp(-1f, 1f, t3);
				global::UnityEngine.Vector3 vector = global::UnityEngine.Vector3.right * t;
				global::UnityEngine.Vector3 vector2 = global::UnityEngine.Vector3.up * t2;
				global::UnityEngine.Vector3 vector3 = global::UnityEngine.Vector3.forward * t3;
				return vector + vector2 + vector3;
			}
			default:
				result.x = global::MirzaBeig.Scripting.Effects.Noise2.perlin(num * frequency, num2 * frequency, num3 * frequency);
				result.y = global::MirzaBeig.Scripting.Effects.Noise2.perlin(num4 * frequency, num5 * frequency, num6 * frequency);
				result.z = global::MirzaBeig.Scripting.Effects.Noise2.perlin(num7 * frequency, num8 * frequency, num9 * frequency);
				return result;
			case global::MirzaBeig.Scripting.Effects.TurbulenceParticleForceField.NoiseType.Simplex:
				result.x = global::MirzaBeig.Scripting.Effects.Noise2.simplex(num * frequency, num2 * frequency, num3 * frequency);
				result.y = global::MirzaBeig.Scripting.Effects.Noise2.simplex(num4 * frequency, num5 * frequency, num6 * frequency);
				result.z = global::MirzaBeig.Scripting.Effects.Noise2.simplex(num7 * frequency, num8 * frequency, num9 * frequency);
				break;
			case global::MirzaBeig.Scripting.Effects.TurbulenceParticleForceField.NoiseType.OctavePerlin:
				result.x = global::MirzaBeig.Scripting.Effects.Noise2.octavePerlin(num, num2, num3, frequency, octaves, octaveMultiplier, octaveScale);
				result.y = global::MirzaBeig.Scripting.Effects.Noise2.octavePerlin(num4, num5, num6, frequency, octaves, octaveMultiplier, octaveScale);
				result.z = global::MirzaBeig.Scripting.Effects.Noise2.octavePerlin(num7, num8, num9, frequency, octaves, octaveMultiplier, octaveScale);
				break;
			case global::MirzaBeig.Scripting.Effects.TurbulenceParticleForceField.NoiseType.OctaveSimplex:
				result.x = global::MirzaBeig.Scripting.Effects.Noise2.octaveSimplex(num, num2, num3, frequency, octaves, octaveMultiplier, octaveScale);
				result.y = global::MirzaBeig.Scripting.Effects.Noise2.octaveSimplex(num4, num5, num6, frequency, octaves, octaveMultiplier, octaveScale);
				result.z = global::MirzaBeig.Scripting.Effects.Noise2.octaveSimplex(num7, num8, num9, frequency, octaves, octaveMultiplier, octaveScale);
				break;
			}
			return result;
		}

		protected override void OnDrawGizmosSelected()
		{
			if (base.enabled)
			{
				base.OnDrawGizmosSelected();
			}
		}
	}
}
