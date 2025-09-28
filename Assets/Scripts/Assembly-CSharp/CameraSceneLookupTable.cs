public class CameraSceneLookupTable : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _displayParent;

	[global::UnityEngine.SerializeField]
	private CameraSceneController _cameraSceneController;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform _stableCameraTransform;

	[global::UnityEngine.SerializeField]
	private ShockerFxController _shockerFxController;

	[global::UnityEngine.SerializeField]
	private ModifiedGlitchShader _modifiedGlitchShader;

	[global::UnityEngine.SerializeField]
	private HaywireFxController _haywireFxController;

	[global::UnityEngine.SerializeField]
	private FlashlightFxController _flashlightFxController;

	[global::UnityEngine.SerializeField]
	private MaskController _maskController;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Camera _minireenaCamera;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform _droppedObjectsVisualsParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _audioListenerParent;

	[global::UnityEngine.SerializeField]
	private DisruptionFxController _disruptionFxController;

	public global::UnityEngine.GameObject DisplayParent => _displayParent;

	public CameraSceneController CameraController => _cameraSceneController;

	public global::UnityEngine.Transform StableCameraTransform => _stableCameraTransform;

	public ShockerFxController ShockerFxController => _shockerFxController;

	public ModifiedGlitchShader ModifiedGlitchShader => _modifiedGlitchShader;

	public HaywireFxController HaywireFxController => _haywireFxController;

	public FlashlightFxController FlashlightFxController => _flashlightFxController;

	public MaskController MaskController => _maskController;

	public global::UnityEngine.Camera MinireenaCamera => _minireenaCamera;

	public global::UnityEngine.Transform DroppedObjectsVisualsParent => _droppedObjectsVisualsParent;

	public global::UnityEngine.GameObject AudioListenerParent => _audioListenerParent;

	public DisruptionFxController DisruptionFxController => _disruptionFxController;
}
