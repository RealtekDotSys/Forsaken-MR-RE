public class AngleStrength
{
	private readonly float _angleMin;

	private readonly float _angleMax;

	public float GetAngleStrength(float angle)
	{
		return CalculateStrength(_angleMin, _angleMax, angle);
	}

	private static float CalculateStrength(float min, float max, float actual)
	{
		if (global::System.Math.Abs(min - max) < 0f)
		{
			if (!(actual <= min))
			{
				return 0f;
			}
			return 1f;
		}
		float num = max - min;
		float num2 = actual - min;
		return global::UnityEngine.Mathf.Clamp(1f - num2 / num, 0f, 1f);
	}

	public AngleStrength(StaticValueRange angleRange)
	{
		_angleMin = angleRange.Min;
		_angleMax = angleRange.Max;
	}
}
