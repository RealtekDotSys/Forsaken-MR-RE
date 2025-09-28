namespace EZCameraShake
{
	public static class CameraShakePresets
	{
		public static global::EZCameraShake.CameraShakeInstance Bump => new global::EZCameraShake.CameraShakeInstance(2.5f, 4f, 0.1f, 0.75f)
		{
			PositionInfluence = global::UnityEngine.Vector3.one * 0.15f,
			RotationInfluence = global::UnityEngine.Vector3.one
		};

		public static global::EZCameraShake.CameraShakeInstance Explosion => new global::EZCameraShake.CameraShakeInstance(5f, 10f, 0f, 1.5f)
		{
			PositionInfluence = global::UnityEngine.Vector3.one * 0.25f,
			RotationInfluence = new global::UnityEngine.Vector3(4f, 1f, 1f)
		};

		public static global::EZCameraShake.CameraShakeInstance Earthquake => new global::EZCameraShake.CameraShakeInstance(0.6f, 3.5f, 2f, 10f)
		{
			PositionInfluence = global::UnityEngine.Vector3.one * 0.25f,
			RotationInfluence = new global::UnityEngine.Vector3(1f, 1f, 4f)
		};

		public static global::EZCameraShake.CameraShakeInstance BadTrip => new global::EZCameraShake.CameraShakeInstance(10f, 0.15f, 5f, 10f)
		{
			PositionInfluence = new global::UnityEngine.Vector3(0f, 0f, 0.15f),
			RotationInfluence = new global::UnityEngine.Vector3(2f, 1f, 4f)
		};

		public static global::EZCameraShake.CameraShakeInstance HandheldCamera => new global::EZCameraShake.CameraShakeInstance(1f, 0.25f, 5f, 10f)
		{
			PositionInfluence = global::UnityEngine.Vector3.zero,
			RotationInfluence = new global::UnityEngine.Vector3(1f, 0.5f, 0.5f)
		};

		public static global::EZCameraShake.CameraShakeInstance Vibration => new global::EZCameraShake.CameraShakeInstance(0.4f, 20f, 2f, 2f)
		{
			PositionInfluence = new global::UnityEngine.Vector3(0f, 0.15f, 0f),
			RotationInfluence = new global::UnityEngine.Vector3(1.25f, 0f, 4f)
		};

		public static global::EZCameraShake.CameraShakeInstance RoughDriving => new global::EZCameraShake.CameraShakeInstance(1f, 2f, 1f, 1f)
		{
			PositionInfluence = global::UnityEngine.Vector3.zero,
			RotationInfluence = global::UnityEngine.Vector3.one
		};
	}
}
