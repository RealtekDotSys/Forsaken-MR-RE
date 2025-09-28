public class ShakeByDistance : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Player;

	public float Distance = 10f;

	private global::EZCameraShake.CameraShakeInstance _shakeInstance;

	private void Start()
	{
		_shakeInstance = global::EZCameraShake.CameraShaker.Instance.StartShake(2f, 14f, 0f);
	}

	private void Update()
	{
		float num = global::UnityEngine.Vector3.Distance(Player.transform.position, base.transform.position);
		_shakeInstance.ScaleMagnitude = 1f - global::UnityEngine.Mathf.Clamp01(num / Distance);
	}
}
