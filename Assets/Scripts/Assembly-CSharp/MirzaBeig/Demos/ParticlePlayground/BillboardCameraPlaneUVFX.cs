namespace MirzaBeig.Demos.ParticlePlayground
{
	public class BillboardCameraPlaneUVFX : global::UnityEngine.MonoBehaviour
	{
		private global::UnityEngine.Transform cameraTransform;

		private void Awake()
		{
		}

		private void Start()
		{
			cameraTransform = global::UnityEngine.Camera.main.transform;
		}

		private void Update()
		{
		}

		private void LateUpdate()
		{
			base.transform.forward = -cameraTransform.forward;
		}
	}
}
