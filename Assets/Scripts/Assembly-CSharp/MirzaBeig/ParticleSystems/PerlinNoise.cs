namespace MirzaBeig.ParticleSystems
{
	[global::System.Serializable]
	public class PerlinNoise
	{
		private global::UnityEngine.Vector2 offset;

		public float amplitude = 1f;

		public float frequency = 1f;

		public bool unscaledTime;

		public void init()
		{
			offset.x = global::UnityEngine.Random.Range(-32f, 32f);
			offset.y = global::UnityEngine.Random.Range(-32f, 32f);
		}

		public float GetValue(float time)
		{
			float num = time * frequency;
			return (global::UnityEngine.Mathf.PerlinNoise(num + offset.x, num + offset.y) - 0.5f) * amplitude;
		}
	}
}
