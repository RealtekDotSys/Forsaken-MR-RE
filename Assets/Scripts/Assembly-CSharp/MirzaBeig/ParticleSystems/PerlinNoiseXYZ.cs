namespace MirzaBeig.ParticleSystems
{
	[global::System.Serializable]
	public class PerlinNoiseXYZ
	{
		public global::MirzaBeig.ParticleSystems.PerlinNoise x;

		public global::MirzaBeig.ParticleSystems.PerlinNoise y;

		public global::MirzaBeig.ParticleSystems.PerlinNoise z;

		public bool unscaledTime;

		public float amplitudeScale = 1f;

		public float frequencyScale = 1f;

		public void init()
		{
			x.init();
			y.init();
			z.init();
		}

		public global::UnityEngine.Vector3 GetXYZ(float time)
		{
			float time2 = time * frequencyScale;
			return new global::UnityEngine.Vector3(x.GetValue(time2), y.GetValue(time2), z.GetValue(time2)) * amplitudeScale;
		}
	}
}
