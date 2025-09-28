public class LoseFxTrigger : global::UnityEngine.MonoBehaviour
{
	public global::Nephasto.VideoGlitchesAsset.VideoGlitchVHSNoise vhsNoise;

	public global::Nephasto.VideoGlitchesAsset.VideoGlitchNoiseDigital noiseDigital;

	private void OnEnable()
	{
		vhsNoise.enabled = true;
		noiseDigital.enabled = true;
	}

	private void OnDisable()
	{
		vhsNoise.enabled = false;
		noiseDigital.enabled = false;
	}
}
