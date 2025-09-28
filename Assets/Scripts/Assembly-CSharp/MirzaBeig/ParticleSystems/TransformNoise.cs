namespace MirzaBeig.ParticleSystems
{
	public class TransformNoise : global::UnityEngine.MonoBehaviour
	{
		public global::MirzaBeig.ParticleSystems.PerlinNoiseXYZ positionNoise;

		public global::MirzaBeig.ParticleSystems.PerlinNoiseXYZ rotationNoise;

		public bool unscaledTime;

		private float time;

		private void Start()
		{
			positionNoise.init();
			rotationNoise.init();
		}

		private void Update()
		{
			time = ((!unscaledTime) ? global::UnityEngine.Time.time : global::UnityEngine.Time.unscaledTime);
			base.transform.localPosition = positionNoise.GetXYZ(time);
			base.transform.localEulerAngles = rotationNoise.GetXYZ(time);
		}
	}
}
