public static class SRMath
{
	public enum EaseType
	{
		Linear = 0,
		QuadEaseOut = 1,
		QuadEaseIn = 2,
		QuadEaseInOut = 3,
		QuadEaseOutIn = 4,
		ExpoEaseOut = 5,
		ExpoEaseIn = 6,
		ExpoEaseInOut = 7,
		ExpoEaseOutIn = 8,
		CubicEaseOut = 9,
		CubicEaseIn = 10,
		CubicEaseInOut = 11,
		CubicEaseOutIn = 12,
		QuartEaseOut = 13,
		QuartEaseIn = 14,
		QuartEaseInOut = 15,
		QuartEaseOutIn = 16,
		QuintEaseOut = 17,
		QuintEaseIn = 18,
		QuintEaseInOut = 19,
		QuintEaseOutIn = 20,
		CircEaseOut = 21,
		CircEaseIn = 22,
		CircEaseInOut = 23,
		CircEaseOutIn = 24,
		SineEaseOut = 25,
		SineEaseIn = 26,
		SineEaseInOut = 27,
		SineEaseOutIn = 28,
		ElasticEaseOut = 29,
		ElasticEaseIn = 30,
		ElasticEaseInOut = 31,
		ElasticEaseOutIn = 32,
		BounceEaseOut = 33,
		BounceEaseIn = 34,
		BounceEaseInOut = 35,
		BounceEaseOutIn = 36,
		BackEaseOut = 37,
		BackEaseIn = 38,
		BackEaseInOut = 39,
		BackEaseOutIn = 40
	}

	private static class TweenFunctions
	{
		public static float Linear(float t, float b, float c, float d)
		{
			return c * t / d + b;
		}

		public static float ExpoEaseOut(float t, float b, float c, float d)
		{
			if (t != d)
			{
				return c * (0f - global::UnityEngine.Mathf.Pow(2f, -10f * t / d) + 1f) + b;
			}
			return b + c;
		}

		public static float ExpoEaseIn(float t, float b, float c, float d)
		{
			if (t != 0f)
			{
				return c * global::UnityEngine.Mathf.Pow(2f, 10f * (t / d - 1f)) + b;
			}
			return b;
		}

		public static float ExpoEaseInOut(float t, float b, float c, float d)
		{
			if (t == 0f)
			{
				return b;
			}
			if (t == d)
			{
				return b + c;
			}
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * global::UnityEngine.Mathf.Pow(2f, 10f * (t - 1f)) + b;
			}
			return c / 2f * (0f - global::UnityEngine.Mathf.Pow(2f, -10f * (t -= 1f)) + 2f) + b;
		}

		public static float ExpoEaseOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return ExpoEaseOut(t * 2f, b, c / 2f, d);
			}
			return ExpoEaseIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float CircEaseOut(float t, float b, float c, float d)
		{
			return c * global::UnityEngine.Mathf.Sqrt(1f - (t = t / d - 1f) * t) + b;
		}

		public static float CircEaseIn(float t, float b, float c, float d)
		{
			return (0f - c) * (global::UnityEngine.Mathf.Sqrt(1f - (t /= d) * t) - 1f) + b;
		}

		public static float CircEaseInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return (0f - c) / 2f * (global::UnityEngine.Mathf.Sqrt(1f - t * t) - 1f) + b;
			}
			return c / 2f * (global::UnityEngine.Mathf.Sqrt(1f - (t -= 2f) * t) + 1f) + b;
		}

		public static float CircEaseOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return CircEaseOut(t * 2f, b, c / 2f, d);
			}
			return CircEaseIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float QuadEaseOut(float t, float b, float c, float d)
		{
			return (0f - c) * (t /= d) * (t - 2f) + b;
		}

		public static float QuadEaseIn(float t, float b, float c, float d)
		{
			return c * (t /= d) * t + b;
		}

		public static float QuadEaseInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * t * t + b;
			}
			return (0f - c) / 2f * ((t -= 1f) * (t - 2f) - 1f) + b;
		}

		public static float QuadEaseOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return QuadEaseOut(t * 2f, b, c / 2f, d);
			}
			return QuadEaseIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float SineEaseOut(float t, float b, float c, float d)
		{
			return c * global::UnityEngine.Mathf.Sin(t / d * (global::System.MathF.PI / 2f)) + b;
		}

		public static float SineEaseIn(float t, float b, float c, float d)
		{
			return (0f - c) * global::UnityEngine.Mathf.Cos(t / d * (global::System.MathF.PI / 2f)) + c + b;
		}

		public static float SineEaseInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * global::UnityEngine.Mathf.Sin(global::System.MathF.PI * t / 2f) + b;
			}
			return (0f - c) / 2f * (global::UnityEngine.Mathf.Cos(global::System.MathF.PI * (t -= 1f) / 2f) - 2f) + b;
		}

		public static float SineEaseOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return SineEaseOut(t * 2f, b, c / 2f, d);
			}
			return SineEaseIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float CubicEaseOut(float t, float b, float c, float d)
		{
			return c * ((t = t / d - 1f) * t * t + 1f) + b;
		}

		public static float CubicEaseIn(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * t + b;
		}

		public static float CubicEaseInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * t * t * t + b;
			}
			return c / 2f * ((t -= 2f) * t * t + 2f) + b;
		}

		public static float CubicEaseOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return CubicEaseOut(t * 2f, b, c / 2f, d);
			}
			return CubicEaseIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float QuartEaseOut(float t, float b, float c, float d)
		{
			return (0f - c) * ((t = t / d - 1f) * t * t * t - 1f) + b;
		}

		public static float QuartEaseIn(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * t * t + b;
		}

		public static float QuartEaseInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * t * t * t * t + b;
			}
			return (0f - c) / 2f * ((t -= 2f) * t * t * t - 2f) + b;
		}

		public static float QuartEaseOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return QuartEaseOut(t * 2f, b, c / 2f, d);
			}
			return QuartEaseIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float QuintEaseOut(float t, float b, float c, float d)
		{
			return c * ((t = t / d - 1f) * t * t * t * t + 1f) + b;
		}

		public static float QuintEaseIn(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * t * t * t + b;
		}

		public static float QuintEaseInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * t * t * t * t * t + b;
			}
			return c / 2f * ((t -= 2f) * t * t * t * t + 2f) + b;
		}

		public static float QuintEaseOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return QuintEaseOut(t * 2f, b, c / 2f, d);
			}
			return QuintEaseIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float ElasticEaseOut(float t, float b, float c, float d)
		{
			if ((t /= d) == 1f)
			{
				return b + c;
			}
			float num = d * 0.3f;
			float num2 = num / 4f;
			return c * global::UnityEngine.Mathf.Pow(2f, -10f * t) * global::UnityEngine.Mathf.Sin((t * d - num2) * (global::System.MathF.PI * 2f) / num) + c + b;
		}

		public static float ElasticEaseIn(float t, float b, float c, float d)
		{
			if ((t /= d) == 1f)
			{
				return b + c;
			}
			float num = d * 0.3f;
			float num2 = num / 4f;
			return 0f - c * global::UnityEngine.Mathf.Pow(2f, 10f * (t -= 1f)) * global::UnityEngine.Mathf.Sin((t * d - num2) * (global::System.MathF.PI * 2f) / num) + b;
		}

		public static float ElasticEaseInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) == 2f)
			{
				return b + c;
			}
			float num = d * 0.45000002f;
			float num2 = num / 4f;
			if (t < 1f)
			{
				return -0.5f * (c * global::UnityEngine.Mathf.Pow(2f, 10f * (t -= 1f)) * global::UnityEngine.Mathf.Sin((t * d - num2) * (global::System.MathF.PI * 2f) / num)) + b;
			}
			return c * global::UnityEngine.Mathf.Pow(2f, -10f * (t -= 1f)) * global::UnityEngine.Mathf.Sin((t * d - num2) * (global::System.MathF.PI * 2f) / num) * 0.5f + c + b;
		}

		public static float ElasticEaseOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return ElasticEaseOut(t * 2f, b, c / 2f, d);
			}
			return ElasticEaseIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float BounceEaseOut(float t, float b, float c, float d)
		{
			if ((t /= d) < 0.36363637f)
			{
				return c * (7.5625f * t * t) + b;
			}
			if ((double)t < 0.7272727272727273)
			{
				return c * (7.5625f * (t -= 0.54545456f) * t + 0.75f) + b;
			}
			if ((double)t < 0.9090909090909091)
			{
				return c * (7.5625f * (t -= 0.8181818f) * t + 0.9375f) + b;
			}
			return c * (7.5625f * (t -= 21f / 22f) * t + 63f / 64f) + b;
		}

		public static float BounceEaseIn(float t, float b, float c, float d)
		{
			return c - BounceEaseOut(d - t, 0f, c, d) + b;
		}

		public static float BounceEaseInOut(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return BounceEaseIn(t * 2f, 0f, c, d) * 0.5f + b;
			}
			return BounceEaseOut(t * 2f - d, 0f, c, d) * 0.5f + c * 0.5f + b;
		}

		public static float BounceEaseOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return BounceEaseOut(t * 2f, b, c / 2f, d);
			}
			return BounceEaseIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float BackEaseOut(float t, float b, float c, float d)
		{
			return c * ((t = t / d - 1f) * t * (2.70158f * t + 1.70158f) + 1f) + b;
		}

		public static float BackEaseIn(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * (2.70158f * t - 1.70158f) + b;
		}

		public static float BackEaseInOut(float t, float b, float c, float d)
		{
			float num = 1.70158f;
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * (t * t * (((num *= 1.525f) + 1f) * t - num)) + b;
			}
			return c / 2f * ((t -= 2f) * t * (((num *= 1.525f) + 1f) * t + num) + 2f) + b;
		}

		public static float BackEaseOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return BackEaseOut(t * 2f, b, c / 2f, d);
			}
			return BackEaseIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}
	}

	public static float LerpUnclamped(float from, float to, float t)
	{
		return (1f - t) * from + t * to;
	}

	public static global::UnityEngine.Vector3 LerpUnclamped(global::UnityEngine.Vector3 from, global::UnityEngine.Vector3 to, float t)
	{
		return new global::UnityEngine.Vector3(LerpUnclamped(from.x, to.x, t), LerpUnclamped(from.y, to.y, t), LerpUnclamped(from.z, to.z, t));
	}

	public static float FacingNormalized(global::UnityEngine.Vector3 dir1, global::UnityEngine.Vector3 dir2)
	{
		dir1.Normalize();
		dir2.Normalize();
		return global::UnityEngine.Mathf.InverseLerp(-1f, 1f, global::UnityEngine.Vector3.Dot(dir1, dir2));
	}

	public static float WrapAngle(float angle)
	{
		if (angle <= -180f)
		{
			angle += 360f;
		}
		else if (angle > 180f)
		{
			angle -= 360f;
		}
		return angle;
	}

	public static float NearestAngle(float to, float angle1, float angle2)
	{
		if (global::UnityEngine.Mathf.Abs(global::UnityEngine.Mathf.DeltaAngle(to, angle1)) > global::UnityEngine.Mathf.Abs(global::UnityEngine.Mathf.DeltaAngle(to, angle2)))
		{
			return angle2;
		}
		return angle1;
	}

	public static int Wrap(int max, int value)
	{
		if (max < 0)
		{
			throw new global::System.ArgumentOutOfRangeException("max", "max must be greater than 0");
		}
		while (value < 0)
		{
			value += max;
		}
		while (value >= max)
		{
			value -= max;
		}
		return value;
	}

	public static float Wrap(float max, float value)
	{
		while (value < 0f)
		{
			value += max;
		}
		while (value >= max)
		{
			value -= max;
		}
		return value;
	}

	public static float Average(float v1, float v2)
	{
		return (v1 + v2) * 0.5f;
	}

	public static float Angle(global::UnityEngine.Vector2 direction)
	{
		float num = global::UnityEngine.Vector3.Angle(global::UnityEngine.Vector3.up, direction);
		if (global::UnityEngine.Vector3.Cross(direction, global::UnityEngine.Vector3.up).z > 0f)
		{
			num *= -1f;
		}
		return num;
	}

	public static float Ease(float from, float to, float t, SRMath.EaseType type)
	{
		return type switch
		{
			SRMath.EaseType.Linear => SRMath.TweenFunctions.Linear(t, from, to, 1f), 
			SRMath.EaseType.QuadEaseOut => SRMath.TweenFunctions.QuadEaseOut(t, from, to, 1f), 
			SRMath.EaseType.QuadEaseIn => SRMath.TweenFunctions.QuadEaseIn(t, from, to, 1f), 
			SRMath.EaseType.QuadEaseInOut => SRMath.TweenFunctions.QuadEaseInOut(t, from, to, 1f), 
			SRMath.EaseType.QuadEaseOutIn => SRMath.TweenFunctions.QuadEaseOutIn(t, from, to, 1f), 
			SRMath.EaseType.ExpoEaseOut => SRMath.TweenFunctions.ExpoEaseOut(t, from, to, 1f), 
			SRMath.EaseType.ExpoEaseIn => SRMath.TweenFunctions.ExpoEaseIn(t, from, to, 1f), 
			SRMath.EaseType.ExpoEaseInOut => SRMath.TweenFunctions.ExpoEaseInOut(t, from, to, 1f), 
			SRMath.EaseType.ExpoEaseOutIn => SRMath.TweenFunctions.ExpoEaseOutIn(t, from, to, 1f), 
			SRMath.EaseType.CubicEaseOut => SRMath.TweenFunctions.CubicEaseOut(t, from, to, 1f), 
			SRMath.EaseType.CubicEaseIn => SRMath.TweenFunctions.CubicEaseIn(t, from, to, 1f), 
			SRMath.EaseType.CubicEaseInOut => SRMath.TweenFunctions.CubicEaseInOut(t, from, to, 1f), 
			SRMath.EaseType.CubicEaseOutIn => SRMath.TweenFunctions.CubicEaseOutIn(t, from, to, 1f), 
			SRMath.EaseType.QuartEaseOut => SRMath.TweenFunctions.QuartEaseOut(t, from, to, 1f), 
			SRMath.EaseType.QuartEaseIn => SRMath.TweenFunctions.QuartEaseIn(t, from, to, 1f), 
			SRMath.EaseType.QuartEaseInOut => SRMath.TweenFunctions.QuartEaseInOut(t, from, to, 1f), 
			SRMath.EaseType.QuartEaseOutIn => SRMath.TweenFunctions.QuartEaseOutIn(t, from, to, 1f), 
			SRMath.EaseType.QuintEaseOut => SRMath.TweenFunctions.QuintEaseOut(t, from, to, 1f), 
			SRMath.EaseType.QuintEaseIn => SRMath.TweenFunctions.QuintEaseIn(t, from, to, 1f), 
			SRMath.EaseType.QuintEaseInOut => SRMath.TweenFunctions.QuintEaseInOut(t, from, to, 1f), 
			SRMath.EaseType.QuintEaseOutIn => SRMath.TweenFunctions.QuintEaseOutIn(t, from, to, 1f), 
			SRMath.EaseType.CircEaseOut => SRMath.TweenFunctions.CircEaseOut(t, from, to, 1f), 
			SRMath.EaseType.CircEaseIn => SRMath.TweenFunctions.CircEaseIn(t, from, to, 1f), 
			SRMath.EaseType.CircEaseInOut => SRMath.TweenFunctions.CircEaseInOut(t, from, to, 1f), 
			SRMath.EaseType.CircEaseOutIn => SRMath.TweenFunctions.CircEaseOutIn(t, from, to, 1f), 
			SRMath.EaseType.SineEaseOut => SRMath.TweenFunctions.SineEaseOut(t, from, to, 1f), 
			SRMath.EaseType.SineEaseIn => SRMath.TweenFunctions.SineEaseIn(t, from, to, 1f), 
			SRMath.EaseType.SineEaseInOut => SRMath.TweenFunctions.SineEaseInOut(t, from, to, 1f), 
			SRMath.EaseType.SineEaseOutIn => SRMath.TweenFunctions.SineEaseOutIn(t, from, to, 1f), 
			SRMath.EaseType.ElasticEaseOut => SRMath.TweenFunctions.ElasticEaseOut(t, from, to, 1f), 
			SRMath.EaseType.ElasticEaseIn => SRMath.TweenFunctions.ElasticEaseIn(t, from, to, 1f), 
			SRMath.EaseType.ElasticEaseInOut => SRMath.TweenFunctions.ElasticEaseInOut(t, from, to, 1f), 
			SRMath.EaseType.ElasticEaseOutIn => SRMath.TweenFunctions.ElasticEaseOutIn(t, from, to, 1f), 
			SRMath.EaseType.BounceEaseOut => SRMath.TweenFunctions.BounceEaseOut(t, from, to, 1f), 
			SRMath.EaseType.BounceEaseIn => SRMath.TweenFunctions.BounceEaseIn(t, from, to, 1f), 
			SRMath.EaseType.BounceEaseInOut => SRMath.TweenFunctions.BounceEaseInOut(t, from, to, 1f), 
			SRMath.EaseType.BounceEaseOutIn => SRMath.TweenFunctions.BounceEaseOutIn(t, from, to, 1f), 
			SRMath.EaseType.BackEaseOut => SRMath.TweenFunctions.BackEaseOut(t, from, to, 1f), 
			SRMath.EaseType.BackEaseIn => SRMath.TweenFunctions.BackEaseIn(t, from, to, 1f), 
			SRMath.EaseType.BackEaseInOut => SRMath.TweenFunctions.BackEaseInOut(t, from, to, 1f), 
			SRMath.EaseType.BackEaseOutIn => SRMath.TweenFunctions.BackEaseOutIn(t, from, to, 1f), 
			_ => throw new global::System.ArgumentOutOfRangeException("type"), 
		};
	}

	public static float SpringLerp(float strength, float deltaTime)
	{
		int num = global::UnityEngine.Mathf.RoundToInt(deltaTime * 1000f);
		float t = 0.001f * strength;
		float num2 = 0f;
		float b = 1f;
		for (int i = 0; i < num; i++)
		{
			num2 = global::UnityEngine.Mathf.Lerp(num2, b, t);
		}
		return num2;
	}

	public static float SpringLerp(float from, float to, float strength, float deltaTime)
	{
		return global::UnityEngine.Mathf.Lerp(from, to, SpringLerp(strength, deltaTime));
	}

	public static global::UnityEngine.Vector3 SpringLerp(global::UnityEngine.Vector3 from, global::UnityEngine.Vector3 to, float strength, float deltaTime)
	{
		return global::UnityEngine.Vector3.Lerp(from, to, SpringLerp(strength, deltaTime));
	}

	public static global::UnityEngine.Quaternion SpringLerp(global::UnityEngine.Quaternion from, global::UnityEngine.Quaternion to, float strength, float deltaTime)
	{
		return global::UnityEngine.Quaternion.Slerp(from, to, SpringLerp(strength, deltaTime));
	}

	public static float SmoothClamp(float value, float min, float max, float scrollMax, SRMath.EaseType easeType = SRMath.EaseType.ExpoEaseOut)
	{
		if (value < min)
		{
			return value;
		}
		float num = global::UnityEngine.Mathf.Clamp01((value - min) / (scrollMax - min));
		global::UnityEngine.Debug.Log(num);
		return global::UnityEngine.Mathf.Clamp(min + global::UnityEngine.Mathf.Lerp(value - min, max, Ease(0f, 1f, num, easeType)), 0f, max);
	}
}
