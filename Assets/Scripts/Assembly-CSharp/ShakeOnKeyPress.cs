public class ShakeOnKeyPress : global::UnityEngine.MonoBehaviour
{
	public float Magnitude = 2f;

	public float Roughness = 10f;

	public float FadeOutTime = 5f;

	private void Update()
	{
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.LeftShift))
		{
			global::EZCameraShake.CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, 0f, FadeOutTime);
		}
	}

	public void ShakeDebug()
	{
		global::EZCameraShake.CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, 0f, FadeOutTime);
	}
}
