public class VignetteController : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Rendering.PostProcessing.PostProcessVolume postProcess;

	private global::UnityEngine.Rendering.PostProcessing.Vignette _vignette;

	private void Start()
	{
		postProcess.profile.TryGetSettings<global::UnityEngine.Rendering.PostProcessing.Vignette>(out _vignette);
	}

	public void SetStrength(float strength)
	{
		_vignette.intensity.value = strength;
	}
}
