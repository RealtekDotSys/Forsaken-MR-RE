namespace MirzaBeig.ParticleSystems.Demos
{
	public class MouseFollow : global::UnityEngine.MonoBehaviour
	{
		public float speed = 8f;

		public float distanceFromCamera = 5f;

		public bool ignoreTimeScale;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void Update()
		{
			global::UnityEngine.Vector3 mousePosition = global::UnityEngine.Input.mousePosition;
			mousePosition.z = distanceFromCamera;
			global::UnityEngine.Vector3 b = global::UnityEngine.Camera.main.ScreenToWorldPoint(mousePosition);
			float num = ((!ignoreTimeScale) ? global::UnityEngine.Time.deltaTime : global::UnityEngine.Time.unscaledDeltaTime);
			global::UnityEngine.Vector3 position = global::UnityEngine.Vector3.Lerp(base.transform.position, b, 1f - global::UnityEngine.Mathf.Exp((0f - speed) * num));
			base.transform.position = position;
		}

		private void LateUpdate()
		{
		}
	}
}
