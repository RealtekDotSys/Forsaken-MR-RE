namespace DigitalRuby.ThunderAndLightning
{
	public class ThunderAndLightningScript : global::UnityEngine.MonoBehaviour
	{
		private class LightningBoltHandler
		{
			private global::DigitalRuby.ThunderAndLightning.ThunderAndLightningScript script;

			private readonly global::System.Random random = new global::System.Random();

			public float VolumeMultiplier { get; set; }

			public LightningBoltHandler(global::DigitalRuby.ThunderAndLightning.ThunderAndLightningScript script)
			{
				this.script = script;
				CalculateNextLightningTime();
			}

			private void UpdateLighting()
			{
				if (script.lightningInProgress)
				{
					return;
				}
				if (script.ModifySkyboxExposure)
				{
					script.skyboxExposureStorm = 0.35f;
					if (script.skyboxMaterial != null && script.skyboxMaterial.HasProperty("_Exposure"))
					{
						script.skyboxMaterial.SetFloat("_Exposure", script.skyboxExposureStorm);
					}
				}
				CheckForLightning();
			}

			private void CalculateNextLightningTime()
			{
				script.nextLightningTime = global::DigitalRuby.ThunderAndLightning.LightningBoltScript.TimeSinceStart + script.LightningIntervalTimeRange.Random(random);
				script.lightningInProgress = false;
				if (script.ModifySkyboxExposure && script.skyboxMaterial.HasProperty("_Exposure"))
				{
					script.skyboxMaterial.SetFloat("_Exposure", script.skyboxExposureStorm);
				}
			}

			public global::System.Collections.IEnumerator ProcessLightning(global::UnityEngine.Vector3? _start, global::UnityEngine.Vector3? _end, bool intense, bool visible)
			{
				script.lightningInProgress = true;
				float intensity;
				float time;
				global::UnityEngine.AudioClip[] sounds;
				if (intense)
				{
					float t = global::UnityEngine.Random.Range(0f, 1f);
					intensity = global::UnityEngine.Mathf.Lerp(2f, 8f, t);
					time = 5f / intensity;
					sounds = script.ThunderSoundsIntense;
				}
				else
				{
					float t2 = global::UnityEngine.Random.Range(0f, 1f);
					intensity = global::UnityEngine.Mathf.Lerp(0f, 2f, t2);
					time = 30f / intensity;
					sounds = script.ThunderSoundsNormal;
				}
				if (script.skyboxMaterial != null && script.ModifySkyboxExposure)
				{
					script.skyboxMaterial.SetFloat("_Exposure", global::UnityEngine.Mathf.Max(intensity * 0.5f, script.skyboxExposureStorm));
				}
				Strike(_start, _end, intense, intensity, script.Camera, visible ? script.Camera : null);
				CalculateNextLightningTime();
				if (intensity >= 1f && sounds != null && sounds.Length != 0)
				{
					yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(time);
					global::UnityEngine.AudioClip audioClip;
					do
					{
						audioClip = sounds[global::UnityEngine.Random.Range(0, sounds.Length - 1)];
					}
					while (sounds.Length > 1 && audioClip == script.lastThunderSound);
					script.lastThunderSound = audioClip;
					script.audioSourceThunder.PlayOneShot(audioClip, intensity * 0.5f * VolumeMultiplier);
				}
			}

			private void Strike(global::UnityEngine.Vector3? _start, global::UnityEngine.Vector3? _end, bool intense, float intensity, global::UnityEngine.Camera camera, global::UnityEngine.Camera visibleInCamera)
			{
				float minInclusive = (intense ? (-1000f) : (-5000f));
				float maxInclusive = (intense ? 1000f : 5000f);
				float num = (intense ? 500f : 2500f);
				float num2 = ((global::UnityEngine.Random.Range(0, 2) == 0) ? global::UnityEngine.Random.Range(minInclusive, 0f - num) : global::UnityEngine.Random.Range(num, maxInclusive));
				float lightningYStart = script.LightningYStart;
				float num3 = ((global::UnityEngine.Random.Range(0, 2) == 0) ? global::UnityEngine.Random.Range(minInclusive, 0f - num) : global::UnityEngine.Random.Range(num, maxInclusive));
				global::UnityEngine.Vector3 vector = script.Camera.transform.position;
				vector.x += num2;
				vector.y = lightningYStart;
				vector.z += num3;
				if (visibleInCamera != null)
				{
					global::UnityEngine.Quaternion rotation = visibleInCamera.transform.rotation;
					visibleInCamera.transform.rotation = global::UnityEngine.Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);
					float x = global::UnityEngine.Random.Range((float)visibleInCamera.pixelWidth * 0.1f, (float)visibleInCamera.pixelWidth * 0.9f);
					float z = global::UnityEngine.Random.Range(visibleInCamera.nearClipPlane + num + num, maxInclusive);
					vector = visibleInCamera.ScreenToWorldPoint(new global::UnityEngine.Vector3(x, 0f, z));
					vector.y = lightningYStart;
					visibleInCamera.transform.rotation = rotation;
				}
				global::UnityEngine.Vector3 vector2 = vector;
				num2 = global::UnityEngine.Random.Range(-100f, 100f);
				lightningYStart = ((global::UnityEngine.Random.Range(0, 4) == 0) ? global::UnityEngine.Random.Range(-1f, 600f) : (-1f));
				num3 += global::UnityEngine.Random.Range(-100f, 100f);
				vector2.x += num2;
				vector2.y = lightningYStart;
				vector2.z += num3;
				vector2.x += num * camera.transform.forward.x;
				vector2.z += num * camera.transform.forward.z;
				while ((vector - vector2).magnitude < 500f)
				{
					vector2.x += num * camera.transform.forward.x;
					vector2.z += num * camera.transform.forward.z;
				}
				vector = _start ?? vector;
				vector2 = _end ?? vector2;
				if (global::UnityEngine.Physics.Raycast(vector, (vector - vector2).normalized, out var hitInfo, float.MaxValue))
				{
					vector2 = hitInfo.point;
				}
				int generations = script.LightningBoltScript.Generations;
				global::DigitalRuby.ThunderAndLightning.RangeOfFloats trunkWidthRange = script.LightningBoltScript.TrunkWidthRange;
				if (global::UnityEngine.Random.value < script.CloudLightningChance)
				{
					script.LightningBoltScript.TrunkWidthRange = default(global::DigitalRuby.ThunderAndLightning.RangeOfFloats);
					script.LightningBoltScript.Generations = 1;
				}
				script.LightningBoltScript.LightParameters.LightIntensity = intensity * 0.5f;
				script.LightningBoltScript.Trigger(vector, vector2);
				script.LightningBoltScript.TrunkWidthRange = trunkWidthRange;
				script.LightningBoltScript.Generations = generations;
			}

			private void CheckForLightning()
			{
				if (global::UnityEngine.Time.time >= script.nextLightningTime)
				{
					bool intense = global::UnityEngine.Random.value < script.LightningIntenseProbability;
					script.StartCoroutine(ProcessLightning(null, null, intense, script.LightningAlwaysVisible));
				}
			}

			public void Update()
			{
				UpdateLighting();
			}
		}

		[global::UnityEngine.Tooltip("Lightning bolt script - optional, leave null if you don't want lightning bolts")]
		public global::DigitalRuby.ThunderAndLightning.LightningBoltPrefabScript LightningBoltScript;

		[global::UnityEngine.Tooltip("Camera where the lightning should be centered over. Defaults to main camera.")]
		public global::UnityEngine.Camera Camera;

		[global::DigitalRuby.ThunderAndLightning.SingleLine("Random interval between strikes.")]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats LightningIntervalTimeRange = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 10f,
			Maximum = 25f
		};

		[global::UnityEngine.Tooltip("Probability (0-1) of an intense lightning bolt that hits really close. Intense lightning has increased brightness and louder thunder compared to normal lightning, and the thunder sounds plays a lot sooner.")]
		[global::UnityEngine.Range(0f, 1f)]
		public float LightningIntenseProbability = 0.2f;

		[global::UnityEngine.Tooltip("Sounds to play for normal thunder. One will be chosen at random for each lightning strike. Depending on intensity, some normal lightning may not play a thunder sound.")]
		public global::UnityEngine.AudioClip[] ThunderSoundsNormal;

		[global::UnityEngine.Tooltip("Sounds to play for intense thunder. One will be chosen at random for each lightning strike.")]
		public global::UnityEngine.AudioClip[] ThunderSoundsIntense;

		[global::UnityEngine.Tooltip("Whether lightning strikes should always try to be in the camera view")]
		public bool LightningAlwaysVisible = true;

		[global::UnityEngine.Tooltip("The chance lightning will simply be in the clouds with no visible bolt")]
		[global::UnityEngine.Range(0f, 1f)]
		public float CloudLightningChance = 0.5f;

		[global::UnityEngine.Tooltip("Whether to modify the skybox exposure when lightning is created")]
		public bool ModifySkyboxExposure;

		[global::UnityEngine.Tooltip("Base point light range for lightning bolts. Increases as intensity increases.")]
		[global::UnityEngine.Range(1f, 10000f)]
		public float BaseLightRange = 2000f;

		[global::UnityEngine.Tooltip("Starting y value for the lightning strikes")]
		[global::UnityEngine.Range(0f, 100000f)]
		public float LightningYStart = 500f;

		[global::UnityEngine.Tooltip("Volume multiplier")]
		[global::UnityEngine.Range(0f, 1f)]
		public float VolumeMultiplier = 1f;

		private float skyboxExposureOriginal;

		private float skyboxExposureStorm;

		private float nextLightningTime;

		private bool lightningInProgress;

		private global::UnityEngine.AudioSource audioSourceThunder;

		private global::DigitalRuby.ThunderAndLightning.ThunderAndLightningScript.LightningBoltHandler lightningBoltHandler;

		private global::UnityEngine.Material skyboxMaterial;

		private global::UnityEngine.AudioClip lastThunderSound;

		public float SkyboxExposureOriginal => skyboxExposureOriginal;

		public bool EnableLightning { get; set; }

		private void Start()
		{
			EnableLightning = true;
			if (Camera == null)
			{
				Camera = global::UnityEngine.Camera.main;
			}
			if (global::UnityEngine.RenderSettings.skybox != null)
			{
				skyboxMaterial = (global::UnityEngine.RenderSettings.skybox = new global::UnityEngine.Material(global::UnityEngine.RenderSettings.skybox));
			}
			skyboxExposureOriginal = (skyboxExposureStorm = ((skyboxMaterial == null || !skyboxMaterial.HasProperty("_Exposure")) ? 1f : skyboxMaterial.GetFloat("_Exposure")));
			audioSourceThunder = base.gameObject.AddComponent<global::UnityEngine.AudioSource>();
			lightningBoltHandler = new global::DigitalRuby.ThunderAndLightning.ThunderAndLightningScript.LightningBoltHandler(this);
			lightningBoltHandler.VolumeMultiplier = VolumeMultiplier;
		}

		private void Update()
		{
			if (lightningBoltHandler != null && EnableLightning)
			{
				lightningBoltHandler.VolumeMultiplier = VolumeMultiplier;
				lightningBoltHandler.Update();
			}
		}

		public void CallNormalLightning()
		{
			CallNormalLightning(null, null);
		}

		public void CallNormalLightning(global::UnityEngine.Vector3? start, global::UnityEngine.Vector3? end)
		{
			StartCoroutine(lightningBoltHandler.ProcessLightning(start, end, intense: false, visible: true));
		}

		public void CallIntenseLightning()
		{
			CallIntenseLightning(null, null);
		}

		public void CallIntenseLightning(global::UnityEngine.Vector3? start, global::UnityEngine.Vector3? end)
		{
			StartCoroutine(lightningBoltHandler.ProcessLightning(start, end, intense: true, visible: true));
		}
	}
}
