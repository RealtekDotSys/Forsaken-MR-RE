namespace MirzaBeig.ParticleSystems.Demos
{
	public class FPSDisplay : global::UnityEngine.MonoBehaviour
	{
		private float timer;

		public float updateTime = 1f;

		private int frameCount;

		private float fpsAccum;

		private global::UnityEngine.UI.Text fpsText;

		private void Awake()
		{
		}

		private void Start()
		{
			fpsText = GetComponent<global::UnityEngine.UI.Text>();
		}

		private void Update()
		{
			frameCount++;
			timer += global::UnityEngine.Time.deltaTime;
			fpsAccum += 1f / global::UnityEngine.Time.deltaTime;
			if (timer >= updateTime)
			{
				timer = 0f;
				int num = global::UnityEngine.Mathf.RoundToInt(fpsAccum / (float)frameCount);
				fpsText.text = "Average FPS: " + num;
				frameCount = 0;
				fpsAccum = 0f;
			}
		}
	}
}
