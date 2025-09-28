public class ShakeOnTrigger : global::UnityEngine.MonoBehaviour
{
	private global::EZCameraShake.CameraShakeInstance _shakeInstance;

	private void Start()
	{
		_shakeInstance = global::EZCameraShake.CameraShaker.Instance.StartShake(2f, 15f, 2f);
		_shakeInstance.StartFadeOut(0f);
		_shakeInstance.DeleteOnInactive = true;
	}

	private void OnTriggerEnter(global::UnityEngine.Collider c)
	{
		if (c.CompareTag("Player"))
		{
			_shakeInstance.StartFadeIn(1f);
		}
	}

	private void OnTriggerExit(global::UnityEngine.Collider c)
	{
		if (c.CompareTag("Player"))
		{
			_shakeInstance.StartFadeOut(3f);
		}
	}
}
