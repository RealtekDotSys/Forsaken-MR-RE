namespace VLB_Samples
{
	public class FreeCameraController : global::UnityEngine.MonoBehaviour
	{
		public float cameraSensitivity = 90f;

		public float speedNormal = 10f;

		public float speedFactorSlow = 0.25f;

		public float speedFactorFast = 3f;

		public float speedClimb = 4f;

		private float rotationH;

		private float rotationV;

		private bool m_UseMouseView = true;

		private bool useMouseView
		{
			get
			{
				return m_UseMouseView;
			}
			set
			{
				m_UseMouseView = value;
				global::UnityEngine.Cursor.lockState = (value ? global::UnityEngine.CursorLockMode.Locked : global::UnityEngine.CursorLockMode.None);
				global::UnityEngine.Cursor.visible = !value;
			}
		}

		private void Start()
		{
			useMouseView = true;
			global::UnityEngine.Vector3 eulerAngles = base.transform.rotation.eulerAngles;
			rotationH = eulerAngles.y;
			rotationV = eulerAngles.x;
			if (rotationV > 180f)
			{
				rotationV -= 360f;
			}
		}

		private void Update()
		{
			if (useMouseView)
			{
				rotationH += global::UnityEngine.Input.GetAxis("Mouse X") * cameraSensitivity * global::UnityEngine.Time.deltaTime;
				rotationV -= global::UnityEngine.Input.GetAxis("Mouse Y") * cameraSensitivity * global::UnityEngine.Time.deltaTime;
			}
			rotationV = global::UnityEngine.Mathf.Clamp(rotationV, -90f, 90f);
			base.transform.rotation = global::UnityEngine.Quaternion.AngleAxis(rotationH, global::UnityEngine.Vector3.up);
			base.transform.rotation *= global::UnityEngine.Quaternion.AngleAxis(rotationV, global::UnityEngine.Vector3.right);
			float num = speedNormal;
			if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftShift) || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.RightShift))
			{
				num *= speedFactorFast;
			}
			else if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.RightControl))
			{
				num *= speedFactorSlow;
			}
			base.transform.position += base.transform.forward * num * global::UnityEngine.Input.GetAxis("Vertical") * global::UnityEngine.Time.deltaTime;
			base.transform.position += base.transform.right * num * global::UnityEngine.Input.GetAxis("Horizontal") * global::UnityEngine.Time.deltaTime;
			if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.Q))
			{
				base.transform.position += global::UnityEngine.Vector3.up * speedClimb * global::UnityEngine.Time.deltaTime;
			}
			if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.E))
			{
				base.transform.position += global::UnityEngine.Vector3.down * speedClimb * global::UnityEngine.Time.deltaTime;
			}
			if (global::UnityEngine.Input.GetMouseButtonDown(0) || global::UnityEngine.Input.GetMouseButtonDown(1) || global::UnityEngine.Input.GetMouseButtonDown(2))
			{
				useMouseView = !useMouseView;
			}
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape))
			{
				useMouseView = false;
			}
		}
	}
}
