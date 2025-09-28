public class NumberChanger : global::UnityEngine.MonoBehaviour
{
	public global::TMPro.TextMeshProUGUI numberText;

	public float duration;

	private int _startNumber;

	private int _endNumber;

	private bool _shouldCountDown;

	private float _startTime;

	private int _lastNumber;

	private bool _hasStarted;

	public bool IsComplete { get; set; }

	public void InitValues(int startNumber, int endNumber)
	{
		_startNumber = startNumber;
		_endNumber = endNumber;
		_shouldCountDown = startNumber > endNumber;
		SetNumber(startNumber);
	}

	public void ChangeNumber()
	{
		_hasStarted = true;
		_startTime = global::UnityEngine.Time.time;
	}

	private void Update()
	{
		if (_hasStarted && !IsComplete)
		{
			int number = _endNumber;
			if (_startNumber != _endNumber && duration > 0f)
			{
				float value = global::UnityEngine.Mathf.SmoothStep(_startNumber, _endNumber, global::UnityEngine.Mathf.Max(global::UnityEngine.Time.time - _startTime, 0f) / duration);
				number = ((!_shouldCountDown) ? CountUp(value) : CountDown(value));
			}
			SetNumber(number);
		}
	}

	private int CountUp(float value)
	{
		return global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.FloorToInt(value), _startNumber, _endNumber);
	}

	private int CountDown(float value)
	{
		return global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.CeilToInt(value), _endNumber, _startNumber);
	}

	private void SetNumber(int newNumber)
	{
		if (!_hasStarted || _lastNumber != newNumber)
		{
			_lastNumber = newNumber;
			numberText.text = _lastNumber.ToString();
			if (_lastNumber == _endNumber)
			{
				IsComplete = true;
			}
		}
	}
}
