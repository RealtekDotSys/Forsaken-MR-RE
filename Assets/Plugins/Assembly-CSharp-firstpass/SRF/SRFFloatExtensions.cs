namespace SRF
{
	public static class SRFFloatExtensions
	{
		public static float Sqr(this float f)
		{
			return f * f;
		}

		public static float SqrRt(this float f)
		{
			return global::UnityEngine.Mathf.Sqrt(f);
		}

		public static bool ApproxZero(this float f)
		{
			return global::UnityEngine.Mathf.Approximately(0f, f);
		}

		public static bool Approx(this float f, float f2)
		{
			return global::UnityEngine.Mathf.Approximately(f, f2);
		}
	}
}
