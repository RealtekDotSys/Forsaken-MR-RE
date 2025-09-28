public class ParticlePlexusDemo_CameraRig : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform pivot;

	private global::UnityEngine.Vector3 targetRotation;

	[global::UnityEngine.Range(0f, 90f)]
	public float rotationLimit = 90f;

	public float rotationSpeed = 2f;

	public float rotationLerpSpeed = 4f;

	private global::UnityEngine.Vector3 startRotation;

	private void Start()
	{
		startRotation = pivot.localEulerAngles;
		targetRotation = startRotation;
	}

	private void Update()
	{
		float axis = global::UnityEngine.Input.GetAxis("Horizontal");
		float axis2 = global::UnityEngine.Input.GetAxis("Vertical");
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.R))
		{
			targetRotation = startRotation;
		}
		axis *= rotationSpeed;
		axis2 *= rotationSpeed;
		targetRotation.y += axis;
		targetRotation.x += axis2;
		targetRotation.x = global::UnityEngine.Mathf.Clamp(targetRotation.x, 0f - rotationLimit, rotationLimit);
		targetRotation.y = global::UnityEngine.Mathf.Clamp(targetRotation.y, 0f - rotationLimit, rotationLimit);
		global::UnityEngine.Vector3 localEulerAngles = pivot.localEulerAngles;
		localEulerAngles.x = global::UnityEngine.Mathf.LerpAngle(localEulerAngles.x, targetRotation.x, global::UnityEngine.Time.deltaTime * rotationLerpSpeed);
		localEulerAngles.y = global::UnityEngine.Mathf.LerpAngle(localEulerAngles.y, targetRotation.y, global::UnityEngine.Time.deltaTime * rotationLerpSpeed);
		pivot.localEulerAngles = localEulerAngles;
	}
}
