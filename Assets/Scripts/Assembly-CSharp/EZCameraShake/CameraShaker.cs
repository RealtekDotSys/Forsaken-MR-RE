namespace EZCameraShake
{
	[global::UnityEngine.AddComponentMenu("EZ Camera Shake/Camera Shaker")]
	public class CameraShaker : global::UnityEngine.MonoBehaviour
	{
		public static global::EZCameraShake.CameraShaker Instance;

		private static global::System.Collections.Generic.Dictionary<string, global::EZCameraShake.CameraShaker> instanceList = new global::System.Collections.Generic.Dictionary<string, global::EZCameraShake.CameraShaker>();

		public global::UnityEngine.Vector3 DefaultPosInfluence = new global::UnityEngine.Vector3(0.15f, 0.15f, 0.15f);

		public global::UnityEngine.Vector3 DefaultRotInfluence = new global::UnityEngine.Vector3(1f, 1f, 1f);

		public global::UnityEngine.Vector3 RestPositionOffset = new global::UnityEngine.Vector3(0f, 0f, 0f);

		public global::UnityEngine.Vector3 RestRotationOffset = new global::UnityEngine.Vector3(0f, 0f, 0f);

		private global::UnityEngine.Vector3 posAddShake;

		private global::UnityEngine.Vector3 rotAddShake;

		private global::System.Collections.Generic.List<global::EZCameraShake.CameraShakeInstance> cameraShakeInstances = new global::System.Collections.Generic.List<global::EZCameraShake.CameraShakeInstance>();

		public global::System.Collections.Generic.List<global::EZCameraShake.CameraShakeInstance> ShakeInstances => new global::System.Collections.Generic.List<global::EZCameraShake.CameraShakeInstance>(cameraShakeInstances);

		private void Awake()
		{
			Instance = this;
			instanceList.Add(base.gameObject.name, this);
		}

		private void Update()
		{
			posAddShake = global::UnityEngine.Vector3.zero;
			rotAddShake = global::UnityEngine.Vector3.zero;
			for (int i = 0; i < cameraShakeInstances.Count && i < cameraShakeInstances.Count; i++)
			{
				global::EZCameraShake.CameraShakeInstance cameraShakeInstance = cameraShakeInstances[i];
				if (cameraShakeInstance.CurrentState == global::EZCameraShake.CameraShakeState.Inactive && cameraShakeInstance.DeleteOnInactive)
				{
					cameraShakeInstances.RemoveAt(i);
					i--;
				}
				else if (cameraShakeInstance.CurrentState != global::EZCameraShake.CameraShakeState.Inactive)
				{
					posAddShake += global::EZCameraShake.CameraUtilities.MultiplyVectors(cameraShakeInstance.UpdateShake(), cameraShakeInstance.PositionInfluence);
					rotAddShake += global::EZCameraShake.CameraUtilities.MultiplyVectors(cameraShakeInstance.UpdateShake(), cameraShakeInstance.RotationInfluence);
				}
			}
			base.transform.localPosition = posAddShake + RestPositionOffset;
			base.transform.localEulerAngles = rotAddShake + RestRotationOffset;
		}

		public static global::EZCameraShake.CameraShaker GetInstance(string name)
		{
			if (instanceList.TryGetValue(name, out var value))
			{
				return value;
			}
			global::UnityEngine.Debug.LogError("CameraShake " + name + " not found!");
			return null;
		}

		public global::EZCameraShake.CameraShakeInstance Shake(global::EZCameraShake.CameraShakeInstance shake)
		{
			cameraShakeInstances.Add(shake);
			return shake;
		}

		public global::EZCameraShake.CameraShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
		{
			global::EZCameraShake.CameraShakeInstance cameraShakeInstance = new global::EZCameraShake.CameraShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime);
			cameraShakeInstance.PositionInfluence = DefaultPosInfluence;
			cameraShakeInstance.RotationInfluence = DefaultRotInfluence;
			cameraShakeInstances.Add(cameraShakeInstance);
			return cameraShakeInstance;
		}

		public global::EZCameraShake.CameraShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime, global::UnityEngine.Vector3 posInfluence, global::UnityEngine.Vector3 rotInfluence)
		{
			global::EZCameraShake.CameraShakeInstance cameraShakeInstance = new global::EZCameraShake.CameraShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime);
			cameraShakeInstance.PositionInfluence = posInfluence;
			cameraShakeInstance.RotationInfluence = rotInfluence;
			cameraShakeInstances.Add(cameraShakeInstance);
			return cameraShakeInstance;
		}

		public global::EZCameraShake.CameraShakeInstance StartShake(float magnitude, float roughness, float fadeInTime)
		{
			global::EZCameraShake.CameraShakeInstance cameraShakeInstance = new global::EZCameraShake.CameraShakeInstance(magnitude, roughness);
			cameraShakeInstance.PositionInfluence = DefaultPosInfluence;
			cameraShakeInstance.RotationInfluence = DefaultRotInfluence;
			cameraShakeInstance.StartFadeIn(fadeInTime);
			cameraShakeInstances.Add(cameraShakeInstance);
			return cameraShakeInstance;
		}

		public global::EZCameraShake.CameraShakeInstance StartShake(float magnitude, float roughness, float fadeInTime, global::UnityEngine.Vector3 posInfluence, global::UnityEngine.Vector3 rotInfluence)
		{
			global::EZCameraShake.CameraShakeInstance cameraShakeInstance = new global::EZCameraShake.CameraShakeInstance(magnitude, roughness);
			cameraShakeInstance.PositionInfluence = posInfluence;
			cameraShakeInstance.RotationInfluence = rotInfluence;
			cameraShakeInstance.StartFadeIn(fadeInTime);
			cameraShakeInstances.Add(cameraShakeInstance);
			return cameraShakeInstance;
		}

		private void OnDestroy()
		{
			instanceList.Remove(base.gameObject.name);
		}
	}
}
