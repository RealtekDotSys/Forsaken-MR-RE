public class FlashlightFxController : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject flashlight;

	public global::UnityEngine.GameObject ambientLight;

	public VignetteController vignetteController;

	public float onValue;

	public float offValue;

	private bool _isFlashlightOn;

	private void Update()
	{
		setFlashlightStatus();
	}

	public void ToggleFlashlight(bool status)
	{
		_isFlashlightOn = status;
		setFlashlightStatus();
	}

	private void setFlashlightStatus()
	{
		flashlight.SetActive(_isFlashlightOn);
		ambientLight.SetActive(!_isFlashlightOn);
		vignetteController.SetStrength(_isFlashlightOn ? onValue : offValue);
	}
}
