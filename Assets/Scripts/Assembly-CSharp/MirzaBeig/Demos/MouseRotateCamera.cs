namespace MirzaBeig.Demos
{
	public class MouseRotateCamera : global::UnityEngine.MonoBehaviour
	{
		public float maxRotation = 5f;

		public float speed = 2f;

		public bool unscaledTime;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		private void LateUpdate()
		{
			global::UnityEngine.Vector2 vector = global::UnityEngine.Input.mousePosition;
			float num = (float)global::UnityEngine.Screen.width / 2f;
			float num2 = (float)global::UnityEngine.Screen.height / 2f;
			float num3 = (vector.x - num) / num;
			float num4 = (vector.y - num2) / num2;
			global::UnityEngine.Vector3 localEulerAngles = base.transform.localEulerAngles;
			localEulerAngles.y = num3 * (0f - maxRotation);
			localEulerAngles.x = num4 * maxRotation;
			float t = ((!unscaledTime) ? global::UnityEngine.Time.deltaTime : global::UnityEngine.Time.unscaledDeltaTime) * speed;
			localEulerAngles.x = global::UnityEngine.Mathf.LerpAngle(base.transform.localEulerAngles.x, localEulerAngles.x, t);
			localEulerAngles.y = global::UnityEngine.Mathf.LerpAngle(base.transform.localEulerAngles.y, localEulerAngles.y, t);
			base.transform.localEulerAngles = localEulerAngles;
		}
	}
}
