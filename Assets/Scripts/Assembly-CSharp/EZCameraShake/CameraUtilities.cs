namespace EZCameraShake
{
	public class CameraUtilities
	{
		public static global::UnityEngine.Vector3 SmoothDampEuler(global::UnityEngine.Vector3 current, global::UnityEngine.Vector3 target, ref global::UnityEngine.Vector3 velocity, float smoothTime)
		{
			global::UnityEngine.Vector3 result = default(global::UnityEngine.Vector3);
			result.x = global::UnityEngine.Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, smoothTime);
			result.y = global::UnityEngine.Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, smoothTime);
			result.z = global::UnityEngine.Mathf.SmoothDampAngle(current.z, target.z, ref velocity.z, smoothTime);
			return result;
		}

		public static global::UnityEngine.Vector3 MultiplyVectors(global::UnityEngine.Vector3 v, global::UnityEngine.Vector3 w)
		{
			v.x *= w.x;
			v.y *= w.y;
			v.z *= w.z;
			return v;
		}
	}
}
