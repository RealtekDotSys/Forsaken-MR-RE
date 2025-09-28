public class CameraConfigurationController : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Rendering.PostProcessing.PostProcessLayer _postProcessLayer;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Rendering.PostProcessing.PostProcessVolume _postProcessVolume;

	[global::UnityEngine.SerializeField]
	private AmplifyMotionEffect _amplifyMotionEffect;

	[global::UnityEngine.SerializeField]
	private ModifiedGlitchShader _modifiedGlitchShader;

	[global::UnityEngine.SerializeField]
	private VignetteController _vignetteController;

	[global::UnityEngine.SerializeField]
	private HaywireFxController _haywireFxController;

	[global::UnityEngine.SerializeField]
	private global::Nephasto.VideoGlitchesAsset.VideoGlitchShift _videoGlitchShift;

	[global::UnityEngine.SerializeField]
	private global::Nephasto.VideoGlitchesAsset.VideoGlitchNoiseDigital _videoGlitchNoiseDigital;
}
