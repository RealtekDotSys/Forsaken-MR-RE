public class GyroCamera : global::UnityEngine.MonoBehaviour
{
	private float _initialYAngle;

	private float _appliedGyroYAngle;

	private float _calibrationYAngle;

	private global::UnityEngine.Transform _rawGyroRotation;

	private float _tempSmoothing;

	[global::UnityEngine.SerializeField]
	private float _smoothing = 0.1f;

	private void Start()
	{
		global::UnityEngine.Input.gyro.enabled = true;
		_initialYAngle = base.transform.eulerAngles.y;
		_rawGyroRotation = new global::UnityEngine.GameObject("GyroRaw").transform;
		_rawGyroRotation.position = base.transform.position;
		_rawGyroRotation.rotation = base.transform.rotation;
	}

	private void Update()
	{
		ApplyGyroRotation();
		ApplyCalibration();
		base.transform.rotation = global::UnityEngine.Quaternion.Slerp(base.transform.rotation, _rawGyroRotation.rotation, _smoothing);
	}

	private global::System.Collections.IEnumerator CalibrateYAngle()
	{
		_tempSmoothing = _smoothing;
		_smoothing = 1f;
		_calibrationYAngle = _appliedGyroYAngle - _initialYAngle;
		yield return null;
		_smoothing = _tempSmoothing;
	}

	private void ApplyGyroRotation()
	{
		_rawGyroRotation.rotation = global::UnityEngine.Input.gyro.attitude;
		_rawGyroRotation.Rotate(0f, 0f, 180f, global::UnityEngine.Space.Self);
		_rawGyroRotation.Rotate(90f, 180f, 0f, global::UnityEngine.Space.World);
		_appliedGyroYAngle = _rawGyroRotation.eulerAngles.y;
	}

	private void ApplyCalibration()
	{
		_rawGyroRotation.Rotate(0f, 0f - _calibrationYAngle, 0f, global::UnityEngine.Space.World);
	}

	public void SetEnabled(bool value)
	{
		global::UnityEngine.Input.gyro.enabled = true;
		_initialYAngle = base.transform.eulerAngles.y;
		if (_rawGyroRotation == null)
		{
			_rawGyroRotation = new global::UnityEngine.GameObject("GyroRaw").transform;
		}
		_rawGyroRotation.position = base.transform.position;
		_rawGyroRotation.rotation = base.transform.rotation;
		base.enabled = true;
	}
}
