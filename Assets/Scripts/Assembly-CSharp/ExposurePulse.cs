public class ExposurePulse : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Post Processing")]
	public global::UnityEngine.Rendering.PostProcessing.PostProcessProfile postProcessProfile;

	public global::UnityEngine.Rendering.PostProcessing.Vignette vignette;

	public global::UnityEngine.Rendering.PostProcessing.ColorGrading colorGrading;

	[global::UnityEngine.Header("Exposure")]
	public bool useExposure;

	[global::UnityEngine.Range(-5f, 5f)]
	public float exposureMin = -1f;

	[global::UnityEngine.Range(-5f, 5f)]
	public float exposureMax = 1f;

	[global::UnityEngine.Space(10f)]
	[global::UnityEngine.Header("Vignette")]
	public bool useVignette;

	public bool useVignetteIntensity;

	public bool useVignetteRoundness;

	[global::UnityEngine.Space(10f)]
	[global::UnityEngine.Header("Vignette Intensity")]
	[global::UnityEngine.Range(-1f, 1f)]
	public float vignetteIntensityMin;

	[global::UnityEngine.Range(-1f, 1f)]
	public float vignetteIntensityMax = 1f;

	[global::UnityEngine.Space(10f)]
	[global::UnityEngine.Header("Vignette Roundness")]
	[global::UnityEngine.Range(0f, 1f)]
	public float vignetteRoundnessMin;

	[global::UnityEngine.Range(0f, 1f)]
	public float vignetteRoundnessMax = 1f;

	[global::UnityEngine.Header("Pulse")]
	public float speedExposure = 1f;

	public float speedVignetteIntensity = 1f;

	public float speedVignetteRoundness = 1f;

	// Pseudo implementation: nothing runs, but inspector fields remain
	public void OnEnable()
	{
		// Pseudo: do nothing
	}

	public void Update()
	{
		// Pseudo: do nothing
	}
}
