public class CameraSceneController : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Canvas _effectsCanvas;

	[global::UnityEngine.SerializeField]
	private global::EZCameraShake.CameraShaker _cameraShaker;

	[global::UnityEngine.SerializeField]
	private CameraConfigurationController _configurationController;

	[global::UnityEngine.SerializeField]
	private FlashlightFxController _flashlightFx;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Camera _defaultCamera;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform _cameraContainer;

	private global::UnityEngine.Camera _currentCamera;

	public global::UnityEngine.Camera Camera => _currentCamera;

	public CameraConfigurationController ConfigurationController => _configurationController;

	public void Awake()
	{
		_currentCamera = _defaultCamera;
	}

	public void InstallNewCamera(global::UnityEngine.Camera newCamera)
	{
		if (!(_currentCamera == newCamera) && !(newCamera == null))
		{
			if (_currentCamera != _defaultCamera)
			{
				global::UnityEngine.Object.Destroy(_currentCamera.gameObject);
			}
			_currentCamera = newCamera;
			newCamera.transform.SetParent(_cameraContainer, worldPositionStays: true);
			_currentCamera.gameObject.SetActive(value: true);
			if (_currentCamera != _defaultCamera)
			{
				_defaultCamera.gameObject.SetActive(value: false);
			}
			HookupCurrentCamera();
		}
	}

	public void UninstallCamera()
	{
		InstallNewCamera(_defaultCamera);
	}

	private void HookupCurrentCamera()
	{
		_effectsCanvas.worldCamera = _currentCamera;
		_flashlightFx.vignetteController = _currentCamera.GetComponent<VignetteController>();
	}
}
