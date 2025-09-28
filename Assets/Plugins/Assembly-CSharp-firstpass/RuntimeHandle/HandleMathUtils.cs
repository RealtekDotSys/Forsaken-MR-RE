namespace RuntimeHandle
{
	public static class HandleMathUtils
	{
		public const float PRECISION_THRESHOLD = 0.001f;

		public static float ClosestPointOnRay(global::UnityEngine.Ray ray, global::UnityEngine.Ray other)
		{
			float num = global::UnityEngine.Vector3.Dot(ray.direction, other.direction);
			float num2 = global::UnityEngine.Vector3.Dot(other.origin, other.direction);
			float num3 = global::UnityEngine.Vector3.Dot(ray.origin, other.direction);
			float num4 = global::UnityEngine.Vector3.Dot(ray.direction, other.origin);
			float num5 = global::UnityEngine.Vector3.Dot(ray.origin, ray.direction);
			float num6 = num * num - 1f;
			if (global::UnityEngine.Mathf.Abs(num6) < 0.001f)
			{
				return 0f;
			}
			return (num5 - num4 + num * (num2 - num3)) / num6;
		}
	}
}
