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

	public void OnEnable()
	{
		postProcessProfile = GetComponent<global::UnityEngine.Rendering.PostProcessing.PostProcessVolume>().profile;
		postProcessProfile.TryGetSettings<global::UnityEngine.Rendering.PostProcessing.Vignette>(out vignette);
		postProcessProfile.TryGetSettings<global::UnityEngine.Rendering.PostProcessing.ColorGrading>(out colorGrading);
	}

	public void Update()
	{
		if (useExposure)
		{
			colorGrading.postExposure.value = global::UnityEngine.Mathf.Lerp(exposureMin, exposureMax, global::UnityEngine.Mathf.PingPong(global::UnityEngine.Time.time * speedExposure, 1f));
		}
		if (useVignette)
		{
			if (useVignetteIntensity)
			{
				vignette.intensity.value = global::UnityEngine.Mathf.Lerp(vignetteIntensityMin, vignetteIntensityMax, global::UnityEngine.Mathf.PingPong(global::UnityEngine.Time.time * speedVignetteIntensity, 1f));
			}
			if (useVignetteRoundness)
			{
				vignette.roundness.value = global::UnityEngine.Mathf.Lerp(vignetteRoundnessMin, vignetteRoundnessMax, global::UnityEngine.Mathf.PingPong(global::UnityEngine.Time.time * speedVignetteRoundness, 1f));
			}
		}
	}
}
