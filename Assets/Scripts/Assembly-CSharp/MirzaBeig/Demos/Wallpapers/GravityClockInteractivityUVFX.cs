namespace MirzaBeig.Demos.Wallpapers
{
	public class GravityClockInteractivityUVFX : global::UnityEngine.MonoBehaviour
	{
		public global::UnityEngine.GameObject forceAffectors;

		public global::UnityEngine.GameObject forceAffectors2;

		public global::UnityEngine.ParticleSystem gravityClockPrefab;

		private global::UnityEngine.ParticleSystem gravityClock;

		public bool enableGravityClockVisualEffects = true;

		public bool enableGravityClockAttractionForce = true;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void SetGravityClockVisualEffectsActive(bool value)
		{
			if (value)
			{
				if (enableGravityClockVisualEffects)
				{
					gravityClock = global::UnityEngine.Object.Instantiate(gravityClockPrefab, base.transform);
					gravityClock.transform.localPosition = global::UnityEngine.Vector3.zero;
				}
			}
			else if ((bool)gravityClock)
			{
				gravityClock.Stop();
				gravityClock.transform.SetParent(null, worldPositionStays: true);
			}
		}

		public void SetGravityClockAttractionForceActive(bool value)
		{
			if (value)
			{
				if (enableGravityClockAttractionForce)
				{
					forceAffectors.gameObject.SetActive(value: true);
					forceAffectors2.gameObject.SetActive(value: true);
				}
			}
			else
			{
				forceAffectors.gameObject.SetActive(value: false);
				forceAffectors2.gameObject.SetActive(value: false);
			}
		}
	}
}
