public class FirstPersonCameraRotation : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Range(0.1f, 9f)]
	[global::UnityEngine.SerializeField]
	private float sensitivity = 2f;

	[global::UnityEngine.Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
	[global::UnityEngine.Range(0f, 90f)]
	[global::UnityEngine.SerializeField]
	private float yRotationLimit = 88f;

	private global::UnityEngine.Vector2 rotation = global::UnityEngine.Vector2.zero;

	private const string xAxis = "Mouse X";

	private const string yAxis = "Mouse Y";

	public float Sensitivity
	{
		get
		{
			return sensitivity;
		}
		set
		{
			sensitivity = value;
		}
	}

	private void Update()
	{
		rotation.x += global::UnityEngine.Input.GetAxis("Mouse X") * sensitivity;
		rotation.y += global::UnityEngine.Input.GetAxis("Mouse Y") * sensitivity;
		rotation.y = global::UnityEngine.Mathf.Clamp(rotation.y, 0f - yRotationLimit, yRotationLimit);
		global::UnityEngine.Quaternion quaternion = global::UnityEngine.Quaternion.AngleAxis(rotation.x, global::UnityEngine.Vector3.up);
		global::UnityEngine.Quaternion quaternion2 = global::UnityEngine.Quaternion.AngleAxis(rotation.y, global::UnityEngine.Vector3.left);
		base.transform.localRotation = quaternion * quaternion2;
	}

	private void OnEnable()
	{
		global::UnityEngine.Cursor.lockState = global::UnityEngine.CursorLockMode.Locked;
		global::UnityEngine.Cursor.visible = false;
	}

	private void OnDisable()
	{
		global::UnityEngine.Cursor.lockState = global::UnityEngine.CursorLockMode.None;
		global::UnityEngine.Cursor.visible = true;
	}
}
