namespace MirzaBeig.ParticleSystems.Demos
{
	public class CameraShake : global::UnityEngine.MonoBehaviour
	{
		[global::System.Serializable]
		public class Shake
		{
			public float amplitude = 1f;

			public float frequency = 1f;

			public float duration;

			[global::UnityEngine.HideInInspector]
			public global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget target;

			private float timeRemaining;

			private global::UnityEngine.Vector2 perlinNoiseX;

			private global::UnityEngine.Vector2 perlinNoiseY;

			private global::UnityEngine.Vector2 perlinNoiseZ;

			[global::UnityEngine.HideInInspector]
			public global::UnityEngine.Vector3 noise;

			public global::UnityEngine.AnimationCurve amplitudeOverLifetimeCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 1f), new global::UnityEngine.Keyframe(1f, 0f));

			public void Init()
			{
				timeRemaining = duration;
				ApplyRandomSeed();
			}

			private void Init(float amplitude, float frequency, float duration, global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget target)
			{
				this.amplitude = amplitude;
				this.frequency = frequency;
				this.duration = duration;
				timeRemaining = duration;
				this.target = target;
				ApplyRandomSeed();
			}

			public void ApplyRandomSeed()
			{
				float num = 32f;
				perlinNoiseX.x = global::UnityEngine.Random.Range(0f - num, num);
				perlinNoiseX.y = global::UnityEngine.Random.Range(0f - num, num);
				perlinNoiseY.x = global::UnityEngine.Random.Range(0f - num, num);
				perlinNoiseY.y = global::UnityEngine.Random.Range(0f - num, num);
				perlinNoiseZ.x = global::UnityEngine.Random.Range(0f - num, num);
				perlinNoiseZ.y = global::UnityEngine.Random.Range(0f - num, num);
			}

			public Shake(float amplitude, float frequency, float duration, global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget target, global::UnityEngine.AnimationCurve amplitudeOverLifetimeCurve)
			{
				Init(amplitude, frequency, duration, target);
				this.amplitudeOverLifetimeCurve = amplitudeOverLifetimeCurve;
			}

			public Shake(float amplitude, float frequency, float duration, global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget target, global::MirzaBeig.ParticleSystems.Demos.CameraShakeAmplitudeCurve amplitudeOverLifetimeCurve)
			{
				Init(amplitude, frequency, duration, target);
				switch (amplitudeOverLifetimeCurve)
				{
				case global::MirzaBeig.ParticleSystems.Demos.CameraShakeAmplitudeCurve.Constant:
					this.amplitudeOverLifetimeCurve = global::UnityEngine.AnimationCurve.Linear(0f, 1f, 1f, 1f);
					break;
				case global::MirzaBeig.ParticleSystems.Demos.CameraShakeAmplitudeCurve.FadeInOut25:
					this.amplitudeOverLifetimeCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 0f), new global::UnityEngine.Keyframe(0.25f, 1f), new global::UnityEngine.Keyframe(1f, 0f));
					break;
				case global::MirzaBeig.ParticleSystems.Demos.CameraShakeAmplitudeCurve.FadeInOut50:
					this.amplitudeOverLifetimeCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 0f), new global::UnityEngine.Keyframe(0.5f, 1f), new global::UnityEngine.Keyframe(1f, 0f));
					break;
				case global::MirzaBeig.ParticleSystems.Demos.CameraShakeAmplitudeCurve.FadeInOut75:
					this.amplitudeOverLifetimeCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 0f), new global::UnityEngine.Keyframe(0.75f, 1f), new global::UnityEngine.Keyframe(1f, 0f));
					break;
				default:
					throw new global::System.Exception("Unknown enum.");
				}
			}

			public bool IsAlive()
			{
				return timeRemaining > 0f;
			}

			public void Update()
			{
				if (!(timeRemaining < 0f))
				{
					global::UnityEngine.Vector2 vector = global::UnityEngine.Time.deltaTime * new global::UnityEngine.Vector2(frequency, frequency);
					perlinNoiseX += vector;
					perlinNoiseY += vector;
					perlinNoiseZ += vector;
					noise.x = global::UnityEngine.Mathf.PerlinNoise(perlinNoiseX.x, perlinNoiseX.y) - 0.5f;
					noise.y = global::UnityEngine.Mathf.PerlinNoise(perlinNoiseY.x, perlinNoiseY.y) - 0.5f;
					noise.z = global::UnityEngine.Mathf.PerlinNoise(perlinNoiseZ.x, perlinNoiseZ.y) - 0.5f;
					float num = amplitudeOverLifetimeCurve.Evaluate(1f - timeRemaining / duration);
					noise *= amplitude * num;
					timeRemaining -= global::UnityEngine.Time.deltaTime;
				}
			}
		}

		public float smoothDampTime = 0.025f;

		private global::UnityEngine.Vector3 smoothDampPositionVelocity;

		private float smoothDampRotationVelocityX;

		private float smoothDampRotationVelocityY;

		private float smoothDampRotationVelocityZ;

		private global::System.Collections.Generic.List<global::MirzaBeig.ParticleSystems.Demos.CameraShake.Shake> shakes = new global::System.Collections.Generic.List<global::MirzaBeig.ParticleSystems.Demos.CameraShake.Shake>();

		private void Start()
		{
		}

		public void Add(float amplitude, float frequency, float duration, global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget target, global::UnityEngine.AnimationCurve amplitudeOverLifetimeCurve)
		{
			shakes.Add(new global::MirzaBeig.ParticleSystems.Demos.CameraShake.Shake(amplitude, frequency, duration, target, amplitudeOverLifetimeCurve));
		}

		public void Add(float amplitude, float frequency, float duration, global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget target, global::MirzaBeig.ParticleSystems.Demos.CameraShakeAmplitudeCurve amplitudeOverLifetimeCurve)
		{
			shakes.Add(new global::MirzaBeig.ParticleSystems.Demos.CameraShake.Shake(amplitude, frequency, duration, target, amplitudeOverLifetimeCurve));
		}

		private void Update()
		{
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.F))
			{
				Add(0.25f, 1f, 2f, global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget.Position, global::MirzaBeig.ParticleSystems.Demos.CameraShakeAmplitudeCurve.FadeInOut25);
			}
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.G))
			{
				Add(15f, 1f, 2f, global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget.Rotation, global::MirzaBeig.ParticleSystems.Demos.CameraShakeAmplitudeCurve.FadeInOut25);
			}
			global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.H);
			global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
			global::UnityEngine.Vector3 zero2 = global::UnityEngine.Vector3.zero;
			for (int i = 0; i < shakes.Count; i++)
			{
				shakes[i].Update();
				if (shakes[i].target == global::MirzaBeig.ParticleSystems.Demos.CameraShakeTarget.Position)
				{
					zero += shakes[i].noise;
				}
				else
				{
					zero2 += shakes[i].noise;
				}
			}
			shakes.RemoveAll((global::MirzaBeig.ParticleSystems.Demos.CameraShake.Shake x) => !x.IsAlive());
			base.transform.localPosition = global::UnityEngine.Vector3.SmoothDamp(base.transform.localPosition, zero, ref smoothDampPositionVelocity, smoothDampTime);
			global::UnityEngine.Vector3 localEulerAngles = base.transform.localEulerAngles;
			localEulerAngles.x = global::UnityEngine.Mathf.SmoothDampAngle(localEulerAngles.x, zero2.x, ref smoothDampRotationVelocityX, smoothDampTime);
			localEulerAngles.y = global::UnityEngine.Mathf.SmoothDampAngle(localEulerAngles.y, zero2.y, ref smoothDampRotationVelocityY, smoothDampTime);
			localEulerAngles.z = global::UnityEngine.Mathf.SmoothDampAngle(localEulerAngles.z, zero2.z, ref smoothDampRotationVelocityZ, smoothDampTime);
			base.transform.localEulerAngles = localEulerAngles;
		}
	}
}
