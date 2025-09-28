public class DisruptionFxController : global::UnityEngine.MonoBehaviour
{
	public global::Nephasto.VideoGlitchesAsset.VideoGlitchVHSNoise vhsNoise1;

	public global::Nephasto.VideoGlitchesAsset.VideoGlitchVHSNoise vhsNoise2;

	public global::Nephasto.VideoGlitchesAsset.VideoGlitchNoiseAnalog noiseAnalog;

	public global::Nephasto.VideoGlitchesAsset.VideoGlitchBrokenCamera brokenCamera;

	public global::UnityEngine.GameObject frostDisruption;

	public global::UnityEngine.GameObject smokeDisruption;

	public bool useFrostDisruption;

	public bool useSmokeDisruption;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Range(0f, 1f)]
	private float _strength;

	private bool _isFrostMaterialNull;

	private bool _isSmokePsNull;

	private global::UnityEngine.ParticleSystem[] _smokePs;

	private global::UnityEngine.Material _frostMaterial;

	private bool _isFxEnabled;

	private void Start()
	{
		_frostMaterial = frostDisruption.GetComponent<global::UnityEngine.MeshRenderer>().sharedMaterial;
		_isFrostMaterialNull = _frostMaterial == null;
		_smokePs = smokeDisruption.GetComponentsInChildren<global::UnityEngine.ParticleSystem>();
		_isSmokePsNull = _smokePs == null;
	}

	private void Update()
	{
		if (!_isFrostMaterialNull && !_isSmokePsNull)
		{
			if (_strength > 0f)
			{
				enableFx();
			}
			else
			{
				disableFx();
			}
		}
	}

	public void SetStrength(float strength)
	{
		_strength = global::UnityEngine.Mathf.Clamp(strength, 0f, 1f);
	}

	private void enableFx()
	{
		if (useFrostDisruption)
		{
			enableFrostFx();
		}
		else if (useSmokeDisruption)
		{
			enableSmokeFx();
		}
		else
		{
			enableImageFx();
		}
		_isFxEnabled = true;
	}

	private void disableFx()
	{
		disableFrostFx();
		disableSmokeFx();
		disableImageFx();
		_isFxEnabled = false;
	}

	private void enableFrostFx()
	{
		_frostMaterial.SetFloat("_Grow", _strength);
		if (!_isFxEnabled)
		{
			frostDisruption.SetActive(value: true);
		}
	}

	private void disableFrostFx()
	{
		frostDisruption.SetActive(value: false);
		_frostMaterial.SetFloat("_Grow", 0f);
	}

	private void enableSmokeFx()
	{
		global::UnityEngine.ParticleSystem[] smokePs = _smokePs;
		foreach (global::UnityEngine.ParticleSystem obj in smokePs)
		{
			global::UnityEngine.ParticleSystem.MainModule main = obj.main;
			main.startColor = new global::UnityEngine.Color(1f, 1f, 1f, _strength);
			global::UnityEngine.ParticleSystem.EmissionModule emission = obj.emission;
			emission.rateOverTime = _strength * 4f;
		}
	}

	private void disableSmokeFx()
	{
		global::UnityEngine.ParticleSystem[] smokePs = _smokePs;
		foreach (global::UnityEngine.ParticleSystem obj in smokePs)
		{
			global::UnityEngine.ParticleSystem.MainModule main = obj.main;
			main.startColor = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			global::UnityEngine.ParticleSystem.EmissionModule emission = obj.emission;
			emission.rateOverTime = 0f;
		}
	}

	private void enableImageFx()
	{
		float num = _strength * global::UnityEngine.Random.Range(5f, 30f);
		noiseAnalog.BarsCount = (int)num;
		vhsNoise1.Strength = _strength;
		vhsNoise2.Strength = _strength * 1.8f;
		noiseAnalog.Strength = _strength * 0.8f;
		brokenCamera.Strength = _strength * 1.1f;
		if (!_isFxEnabled)
		{
			vhsNoise1.enabled = true;
			vhsNoise2.enabled = true;
			noiseAnalog.enabled = true;
			brokenCamera.enabled = true;
		}
	}

	private void disableImageFx()
	{
		vhsNoise1.Strength = 0f;
		vhsNoise2.Strength = 0f;
		noiseAnalog.Strength = 0f;
		brokenCamera.Strength = 0f;
		vhsNoise1.enabled = false;
		vhsNoise2.enabled = false;
		noiseAnalog.enabled = false;
		brokenCamera.enabled = false;
	}
}
