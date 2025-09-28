namespace MirzaBeig.ParticleSystems
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Light))]
	public class AnimatedLight : global::UnityEngine.MonoBehaviour
	{
		private global::UnityEngine.Light light;

		public float duration = 1f;

		private bool evaluating = true;

		public global::UnityEngine.Gradient colourOverLifetime;

		public global::UnityEngine.AnimationCurve intensityOverLifetime = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 0f), new global::UnityEngine.Keyframe(0.5f, 1f), new global::UnityEngine.Keyframe(1f, 0f));

		public bool loop = true;

		public bool autoDestruct;

		private global::UnityEngine.Color startColour;

		private float startIntensity;

		public float time { get; set; }

		private void Awake()
		{
			light = GetComponent<global::UnityEngine.Light>();
		}

		private void Start()
		{
			startColour = light.color;
			startIntensity = light.intensity;
			light.color = startColour * colourOverLifetime.Evaluate(0f);
			light.intensity = startIntensity * intensityOverLifetime.Evaluate(0f);
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
			light.color = startColour;
			light.intensity = startIntensity;
			time = 0f;
			evaluating = true;
			light.color = startColour * colourOverLifetime.Evaluate(0f);
			light.intensity = startIntensity * intensityOverLifetime.Evaluate(0f);
		}

		private void Update()
		{
			if (!evaluating)
			{
				return;
			}
			if (time < duration)
			{
				time += global::UnityEngine.Time.deltaTime;
				if (time > duration)
				{
					if (autoDestruct)
					{
						global::UnityEngine.Object.Destroy(base.gameObject);
					}
					else if (loop)
					{
						time = 0f;
					}
					else
					{
						time = duration;
						evaluating = false;
					}
				}
			}
			if (time <= duration)
			{
				float num = time / duration;
				light.color = startColour * colourOverLifetime.Evaluate(num);
				light.intensity = startIntensity * intensityOverLifetime.Evaluate(num);
			}
		}
	}
}
