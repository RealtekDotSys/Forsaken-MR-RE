namespace DigitalRuby.ThunderAndLightning
{
	public class LightningParticleSpellScript : global::DigitalRuby.ThunderAndLightning.LightningSpellScript, global::DigitalRuby.ThunderAndLightning.ICollisionHandler
	{
		[global::UnityEngine.Header("Particle system")]
		public global::UnityEngine.ParticleSystem ParticleSystem;

		[global::UnityEngine.Tooltip("Particle system collision interval. This time must elapse before another collision will be registered.")]
		public float CollisionInterval;

		protected float collisionTimer;

		[global::UnityEngine.HideInInspector]
		public global::System.Action<global::UnityEngine.GameObject, global::System.Collections.Generic.List<global::UnityEngine.ParticleCollisionEvent>, int> CollisionCallback;

		[global::UnityEngine.Header("Particle Light Properties")]
		[global::UnityEngine.Tooltip("Whether to enable point lights for the particles")]
		public bool EnableParticleLights = true;

		[global::DigitalRuby.ThunderAndLightning.SingleLineClamp("Possible range for particle lights", 0.001, 100.0)]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats ParticleLightRange = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 2f,
			Maximum = 5f
		};

		[global::DigitalRuby.ThunderAndLightning.SingleLineClamp("Possible range of intensity for particle lights", 0.009999999776482582, 8.0)]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats ParticleLightIntensity = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 0.2f,
			Maximum = 0.3f
		};

		[global::UnityEngine.Tooltip("Possible range of colors for particle lights")]
		public global::UnityEngine.Color ParticleLightColor1 = global::UnityEngine.Color.white;

		[global::UnityEngine.Tooltip("Possible range of colors for particle lights")]
		public global::UnityEngine.Color ParticleLightColor2 = global::UnityEngine.Color.white;

		[global::UnityEngine.Tooltip("The culling mask for particle lights")]
		public global::UnityEngine.LayerMask ParticleLightCullingMask = -1;

		private global::UnityEngine.ParticleSystem.Particle[] particles = new global::UnityEngine.ParticleSystem.Particle[512];

		private readonly global::System.Collections.Generic.List<global::UnityEngine.GameObject> particleLights = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

		private void PopulateParticleLight(global::UnityEngine.Light src)
		{
			src.bounceIntensity = 0f;
			src.type = global::UnityEngine.LightType.Point;
			src.shadows = global::UnityEngine.LightShadows.None;
			src.color = new global::UnityEngine.Color(global::UnityEngine.Random.Range(ParticleLightColor1.r, ParticleLightColor2.r), global::UnityEngine.Random.Range(ParticleLightColor1.g, ParticleLightColor2.g), global::UnityEngine.Random.Range(ParticleLightColor1.b, ParticleLightColor2.b), 1f);
			src.cullingMask = ParticleLightCullingMask;
			src.intensity = global::UnityEngine.Random.Range(ParticleLightIntensity.Minimum, ParticleLightIntensity.Maximum);
			src.range = global::UnityEngine.Random.Range(ParticleLightRange.Minimum, ParticleLightRange.Maximum);
		}

		private void UpdateParticleLights()
		{
			if (EnableParticleLights)
			{
				int num = ParticleSystem.GetParticles(particles);
				while (particleLights.Count < num)
				{
					global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("LightningParticleSpellLight");
					gameObject.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
					PopulateParticleLight(gameObject.AddComponent<global::UnityEngine.Light>());
					particleLights.Add(gameObject);
				}
				while (particleLights.Count > num)
				{
					global::UnityEngine.Object.Destroy(particleLights[particleLights.Count - 1]);
					particleLights.RemoveAt(particleLights.Count - 1);
				}
				for (int i = 0; i < num; i++)
				{
					particleLights[i].transform.position = particles[i].position;
				}
			}
		}

		private void UpdateParticleSystems()
		{
			if (EmissionParticleSystem != null && EmissionParticleSystem.isPlaying)
			{
				EmissionParticleSystem.transform.position = SpellStart.transform.position;
				EmissionParticleSystem.transform.forward = Direction;
			}
			if (ParticleSystem != null)
			{
				if (ParticleSystem.isPlaying)
				{
					ParticleSystem.transform.position = SpellStart.transform.position;
					ParticleSystem.transform.forward = Direction;
				}
				UpdateParticleLights();
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			foreach (global::UnityEngine.GameObject particleLight in particleLights)
			{
				global::UnityEngine.Object.Destroy(particleLight);
			}
		}

		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			base.Update();
			UpdateParticleSystems();
			collisionTimer -= global::DigitalRuby.ThunderAndLightning.LightningBoltScript.DeltaTime;
		}

		protected override void OnCastSpell()
		{
			if (ParticleSystem != null)
			{
				ParticleSystem.Play();
				UpdateParticleSystems();
			}
		}

		protected override void OnStopSpell()
		{
			if (ParticleSystem != null)
			{
				ParticleSystem.Stop();
			}
		}

		void global::DigitalRuby.ThunderAndLightning.ICollisionHandler.HandleCollision(global::UnityEngine.GameObject obj, global::System.Collections.Generic.List<global::UnityEngine.ParticleCollisionEvent> collisions, int collisionCount)
		{
			if (collisionTimer <= 0f)
			{
				collisionTimer = CollisionInterval;
				PlayCollisionSound(collisions[0].intersection);
				ApplyCollisionForce(collisions[0].intersection);
				if (CollisionCallback != null)
				{
					CollisionCallback(obj, collisions, collisionCount);
				}
			}
		}
	}
}
