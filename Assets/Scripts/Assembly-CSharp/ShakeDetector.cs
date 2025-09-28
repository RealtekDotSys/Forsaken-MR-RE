public class ShakeDetector
{
	private const float AccelerometerUpdateInterval = 1f / 60f;

	private const float LowPassKernelWidthInSeconds = 1f;

	private const float LowPassFilterFactor = 1f / 60f;

	private const float DefaultMagnitude = 2f;

	private const float DefaultGraceTime = 0.1f;

	private global::UnityEngine.Vector3 _lowPassValue;

	private float _graceTime;

	private RangeData _shakeMagnitudeRange;

	private float _rampTime;

	private float _currentTime;

	private bool _isDisruptionRamping;

	private readonly SimpleTimer _shakeGracePeriod;

	private global::UnityEngine.Transform _cameraTransform;

	private global::UnityEngine.Quaternion _lastRotation;

	private global::UnityEngine.Quaternion _currentRotation;

	private const float TRANSFORM_MOTION_MOD = 1f;

	public bool IsShaking { get; set; }

	private float _magnitude
	{
		get
		{
			if (_shakeMagnitudeRange == null)
			{
				return 2f;
			}
			return global::UnityEngine.Mathf.Lerp(_shakeMagnitudeRange.Min, _shakeMagnitudeRange.Max, _currentTime / _rampTime);
		}
	}

	public void SetConfigValues(DisruptionData settings)
	{
		_graceTime = settings.ShakeGraceTime;
		_shakeMagnitudeRange = settings.ShakeMagnitude;
		_rampTime = settings.RampTime;
		_currentTime = 0f;
	}

	public void ClearConfigValues()
	{
		_graceTime = 0.1f;
		_shakeMagnitudeRange = null;
	}

	public void Reset()
	{
		_currentTime = 0f;
		_isDisruptionRamping = true;
	}

	public void Stop()
	{
		_isDisruptionRamping = false;
	}

	public void Update()
	{
		if (!(_cameraTransform == null))
		{
			_lastRotation = _currentRotation;
			_currentRotation = _cameraTransform.rotation;
			global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
			zero = new global::UnityEngine.Vector3(global::UnityEngine.Input.GetAxis("Mouse X"), global::UnityEngine.Input.GetAxis("Mouse Y"), 0f) * 1f;
			_lowPassValue = global::UnityEngine.Vector3.Lerp(_lowPassValue, zero, 1f / 60f);
			if ((zero - _lowPassValue).magnitude >= _magnitude)
			{
				IsShaking = true;
				_shakeGracePeriod.StartTimer(_graceTime);
			}
			else if (_shakeGracePeriod.Started && _shakeGracePeriod.IsExpired())
			{
				IsShaking = false;
				_shakeGracePeriod.Reset();
			}
			if (_isDisruptionRamping)
			{
				_currentTime += global::UnityEngine.Time.deltaTime;
			}
		}
	}

	public ShakeDetector()
	{
		_graceTime = 0.1f;
		_rampTime = 1f;
		_shakeGracePeriod = new SimpleTimer();
		_lowPassValue = global::UnityEngine.Input.acceleration;
	}

	public void SetTransform(global::UnityEngine.Transform t)
	{
		_cameraTransform = t;
		_lastRotation = _cameraTransform.rotation;
		_currentRotation = _cameraTransform.rotation;
		_lowPassValue = global::UnityEngine.Vector3.zero;
	}
}
