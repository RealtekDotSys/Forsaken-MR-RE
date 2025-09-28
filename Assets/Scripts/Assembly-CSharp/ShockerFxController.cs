public class ShockerFxController : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Light shockerLight;

	public global::UnityEngine.GameObject[] lightningBolts;

	public float duration;

	[global::UnityEngine.SerializeField]
	private bool _targetHit;

	[global::UnityEngine.SerializeField]
	private bool _batteryEmpty;

	[global::UnityEngine.SerializeField]
	private bool _continuous;

	private ShockerSettings _shockerSettings;

	private global::DigitalRuby.LightningBolt.LightningBoltScript[] _missedBoltScripts;

	private global::DigitalRuby.LightningBolt.LightningBoltScript[] _hitBoltScripts;

	private global::DigitalRuby.LightningBolt.LightningBoltScript[] _continuousBoltsScripts;

	private global::UnityEngine.ParticleSystem _spark;

	private global::UnityEngine.ParticleSystem.EmissionModule _emissionModule;

	private readonly SimpleTimer _durationTimer;

	private void Start()
	{
		_missedBoltScripts = lightningBolts[1].GetComponentsInChildren<global::DigitalRuby.LightningBolt.LightningBoltScript>();
		_hitBoltScripts = lightningBolts[2].GetComponentsInChildren<global::DigitalRuby.LightningBolt.LightningBoltScript>();
		if (lightningBolts[3] != null)
		{
			_continuousBoltsScripts = lightningBolts[3].GetComponentsInChildren<global::DigitalRuby.LightningBolt.LightningBoltScript>();
		}
		_spark = lightningBolts[2].GetComponentInChildren<global::UnityEngine.ParticleSystem>();
		_emissionModule = _spark.emission;
		Reset();
	}

	public void SetShockerData(ShockerSettings shockerSettings)
	{
		_shockerSettings = shockerSettings;
	}

	public void StartEffect(bool didHit)
	{
		global::UnityEngine.Debug.LogError("StartEffect");
		_batteryEmpty = false;
		_targetHit = didHit;
		_continuous = _shockerSettings.ContinuousShocker;
		StartEffect();
	}

	public void EndEffect()
	{
		global::UnityEngine.Debug.LogError("EndEffect");
		if (lightningBolts != null && !(shockerLight == null))
		{
			Reset();
		}
	}

	public void SetShockerOffset(global::UnityEngine.Vector3 offset)
	{
		UpdateScriptOffset(_missedBoltScripts, offset);
	}

	private void UpdateScriptOffset(global::DigitalRuby.LightningBolt.LightningBoltScript[] scripts, global::UnityEngine.Vector3 offset)
	{
		for (int i = 0; i < scripts.Length; i++)
		{
			scripts[i].Offset = offset;
		}
	}

	private void StartEffect()
	{
		if (lightningBolts == null || shockerLight == null)
		{
			return;
		}
		duration = 0f;
		Reset();
		if (_batteryEmpty)
		{
			duration = _shockerSettings.NoBatteryDuration;
			if (_shockerSettings.NoBatteryDuration > 0f)
			{
				_durationTimer.StartTimer(_shockerSettings.NoBatteryDuration + _shockerSettings.ResetBufferTime);
			}
			lightningBolts[0].SetActive(value: true);
			shockerLight.enabled = true;
			shockerLight.range = 0f;
		}
		else if (_continuous)
		{
			duration = _shockerSettings.HitDuration;
			if (_shockerSettings.HitDuration > 0f)
			{
				_durationTimer.StartTimer(_shockerSettings.HitDuration + _shockerSettings.ResetBufferTime);
			}
			shockerLight.enabled = true;
			shockerLight.range = 5f;
			global::DigitalRuby.LightningBolt.LightningBoltScript[] continuousBoltsScripts = _continuousBoltsScripts;
			for (int i = 0; i < continuousBoltsScripts.Length; i++)
			{
				continuousBoltsScripts[i].ManualMode = false;
			}
		}
		else if (_targetHit)
		{
			duration = _shockerSettings.HitDuration;
			if (_shockerSettings.HitDuration > 0f)
			{
				_durationTimer.StartTimer(_shockerSettings.HitDuration + _shockerSettings.ResetBufferTime);
			}
			shockerLight.enabled = true;
			shockerLight.range = 10f;
			_emissionModule.rateOverTime = 300f;
			global::DigitalRuby.LightningBolt.LightningBoltScript[] continuousBoltsScripts = _hitBoltScripts;
			for (int i = 0; i < continuousBoltsScripts.Length; i++)
			{
				continuousBoltsScripts[i].ManualMode = false;
			}
		}
		else
		{
			duration = _shockerSettings.MissDuration;
			if (_shockerSettings.MissDuration > 0f)
			{
				_durationTimer.StartTimer(_shockerSettings.MissDuration + _shockerSettings.ResetBufferTime);
			}
			shockerLight.enabled = true;
			shockerLight.range = 2.5f;
			global::DigitalRuby.LightningBolt.LightningBoltScript[] continuousBoltsScripts = _missedBoltScripts;
			for (int i = 0; i < continuousBoltsScripts.Length; i++)
			{
				continuousBoltsScripts[i].ManualMode = false;
			}
		}
	}

	private void Reset()
	{
		_durationTimer.Reset();
		global::DigitalRuby.LightningBolt.LightningBoltScript[] missedBoltScripts = _missedBoltScripts;
		for (int i = 0; i < missedBoltScripts.Length; i++)
		{
			missedBoltScripts[i].ManualMode = true;
		}
		missedBoltScripts = _hitBoltScripts;
		for (int i = 0; i < missedBoltScripts.Length; i++)
		{
			missedBoltScripts[i].ManualMode = true;
		}
		if (_continuousBoltsScripts != null)
		{
			missedBoltScripts = _continuousBoltsScripts;
			for (int i = 0; i < missedBoltScripts.Length; i++)
			{
				missedBoltScripts[i].ManualMode = true;
			}
		}
		_emissionModule.rateOverTime = 0f;
		lightningBolts[0].SetActive(value: false);
		shockerLight.enabled = false;
		shockerLight.range = 0f;
	}

	private void Update()
	{
		if (_durationTimer.Started && _durationTimer.IsExpired())
		{
			Reset();
		}
	}

	public ShockerFxController()
	{
		_durationTimer = new SimpleTimer();
	}
}
