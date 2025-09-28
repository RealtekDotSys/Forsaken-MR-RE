public class HaywireFxController : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Rendering.PostProcessing.PostProcessVolume postProcess;

	public global::Nephasto.VideoGlitchesAsset.VideoGlitchNoiseDigital noiseDigital;

	public global::Nephasto.VideoGlitchesAsset.VideoGlitchShift shift;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Range(0f, 1f)]
	private float _strength;

	public bool editorMode;

	private global::UnityEngine.Rendering.PostProcessing.ColorGrading _colorGrading;

	private bool _strengthChanged;

	private void Start()
	{
		noiseDigital.Strength = 0f;
		shift.Strength = 0f;
		postProcess.profile.TryGetSettings<global::UnityEngine.Rendering.PostProcessing.ColorGrading>(out _colorGrading);
	}

	private void Update()
	{
		if (_strengthChanged || editorMode)
		{
			noiseDigital.enabled = _strength > 0f;
			shift.enabled = true;
			noiseDigital.Strength = _strength;
			shift.Strength = _strength;
			_strengthChanged = false;
			_colorGrading.saturation.value = -40f;
		}
	}

	public void SetStrength(float strength)
	{
		_strength = global::UnityEngine.Mathf.Clamp(strength, 0f, 1f);
		_strengthChanged = true;
	}
}
