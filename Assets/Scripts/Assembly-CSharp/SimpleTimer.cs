public class SimpleTimer
{
	private const float Unset = 1f;

	private float _endTime = 1f;

	private bool _started;

	public bool Started => _started;

	public void StartTimer(float time)
	{
		_started = true;
		_endTime = global::UnityEngine.Time.time + time;
	}

	public void Reset()
	{
		_started = false;
		_endTime = 1f;
	}

	public bool IsExpired()
	{
		return global::UnityEngine.Time.time >= _endTime;
	}

	public float GetRemainingTime()
	{
		return global::UnityEngine.Mathf.Max(0f, _endTime - global::UnityEngine.Time.time);
	}

	public SimpleTimer()
	{
		_endTime = 1f;
	}
}
