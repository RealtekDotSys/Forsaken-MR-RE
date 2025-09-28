namespace EZCameraShake
{
	public class CameraShakeInstance
	{
		public float Magnitude;

		public float Roughness;

		public global::UnityEngine.Vector3 PositionInfluence;

		public global::UnityEngine.Vector3 RotationInfluence;

		public bool DeleteOnInactive = true;

		private float roughMod = 1f;

		private float magnMod = 1f;

		private float fadeOutDuration;

		private float fadeInDuration;

		private bool sustain;

		private float currentFadeTime;

		private float tick;

		private global::UnityEngine.Vector3 amt;

		public float ScaleRoughness
		{
			get
			{
				return roughMod;
			}
			set
			{
				roughMod = value;
			}
		}

		public float ScaleMagnitude
		{
			get
			{
				return magnMod;
			}
			set
			{
				magnMod = value;
			}
		}

		public float NormalizedFadeTime => currentFadeTime;

		private bool IsShaking
		{
			get
			{
				if (!(currentFadeTime > 0f))
				{
					return sustain;
				}
				return true;
			}
		}

		private bool IsFadingOut
		{
			get
			{
				if (!sustain)
				{
					return currentFadeTime > 0f;
				}
				return false;
			}
		}

		private bool IsFadingIn
		{
			get
			{
				if (currentFadeTime < 1f && sustain)
				{
					return fadeInDuration > 0f;
				}
				return false;
			}
		}

		public global::EZCameraShake.CameraShakeState CurrentState
		{
			get
			{
				if (IsFadingIn)
				{
					return global::EZCameraShake.CameraShakeState.FadingIn;
				}
				if (IsFadingOut)
				{
					return global::EZCameraShake.CameraShakeState.FadingOut;
				}
				if (IsShaking)
				{
					return global::EZCameraShake.CameraShakeState.Sustained;
				}
				return global::EZCameraShake.CameraShakeState.Inactive;
			}
		}

		public CameraShakeInstance(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
		{
			Magnitude = magnitude;
			fadeOutDuration = fadeOutTime;
			fadeInDuration = fadeInTime;
			Roughness = roughness;
			if (fadeInTime > 0f)
			{
				sustain = true;
				currentFadeTime = 0f;
			}
			else
			{
				sustain = false;
				currentFadeTime = 1f;
			}
			tick = global::UnityEngine.Random.Range(-100, 100);
		}

		public CameraShakeInstance(float magnitude, float roughness)
		{
			Magnitude = magnitude;
			Roughness = roughness;
			sustain = true;
			tick = global::UnityEngine.Random.Range(-100, 100);
		}

		public global::UnityEngine.Vector3 UpdateShake()
		{
			amt.x = global::UnityEngine.Mathf.PerlinNoise(tick, 0f) - 0.5f;
			amt.y = global::UnityEngine.Mathf.PerlinNoise(0f, tick) - 0.5f;
			amt.z = global::UnityEngine.Mathf.PerlinNoise(tick, tick) - 0.5f;
			if (fadeInDuration > 0f && sustain)
			{
				if (currentFadeTime < 1f)
				{
					currentFadeTime += global::UnityEngine.Time.deltaTime / fadeInDuration;
				}
				else if (fadeOutDuration > 0f)
				{
					sustain = false;
				}
			}
			if (!sustain)
			{
				currentFadeTime -= global::UnityEngine.Time.deltaTime / fadeOutDuration;
			}
			if (sustain)
			{
				tick += global::UnityEngine.Time.deltaTime * Roughness * roughMod;
			}
			else
			{
				tick += global::UnityEngine.Time.deltaTime * Roughness * roughMod * currentFadeTime;
			}
			return amt * Magnitude * magnMod * currentFadeTime;
		}

		public void StartFadeOut(float fadeOutTime)
		{
			if (fadeOutTime == 0f)
			{
				currentFadeTime = 0f;
			}
			fadeOutDuration = fadeOutTime;
			fadeInDuration = 0f;
			sustain = false;
		}

		public void StartFadeIn(float fadeInTime)
		{
			if (fadeInTime == 0f)
			{
				currentFadeTime = 1f;
			}
			fadeInDuration = fadeInTime;
			fadeOutDuration = 0f;
			sustain = true;
		}
	}
}
