namespace DigitalRuby.ThunderAndLightning
{
	public abstract class LightningSpellScript : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.Header("Direction and distance")]
		[global::UnityEngine.Tooltip("The start point of the spell. Set this to a muzzle end or hand.")]
		public global::UnityEngine.GameObject SpellStart;

		[global::UnityEngine.Tooltip("The end point of the spell. Set this to an empty game object. This will change depending on things like collisions, randomness, etc. Not all spells need an end object, but create this anyway to be sure.")]
		public global::UnityEngine.GameObject SpellEnd;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.Tooltip("The direction of the spell. Should be normalized. Does not change unless explicitly modified.")]
		public global::UnityEngine.Vector3 Direction;

		[global::UnityEngine.Tooltip("The maximum distance of the spell")]
		public float MaxDistance = 15f;

		[global::UnityEngine.Header("Collision")]
		[global::UnityEngine.Tooltip("Whether the collision is an exploision. If not explosion, collision is directional.")]
		public bool CollisionIsExplosion;

		[global::UnityEngine.Tooltip("The radius of the collision explosion")]
		public float CollisionRadius = 1f;

		[global::UnityEngine.Tooltip("The force to explode with when there is a collision")]
		public float CollisionForce = 50f;

		[global::UnityEngine.Tooltip("Collision force mode")]
		public global::UnityEngine.ForceMode CollisionForceMode = global::UnityEngine.ForceMode.Impulse;

		[global::UnityEngine.Tooltip("The particle system for collisions. For best effects, this should emit particles in bursts at time 0 and not loop.")]
		public global::UnityEngine.ParticleSystem CollisionParticleSystem;

		[global::UnityEngine.Tooltip("The layers that the spell should collide with")]
		public global::UnityEngine.LayerMask CollisionMask = -1;

		[global::UnityEngine.Tooltip("Collision audio source")]
		public global::UnityEngine.AudioSource CollisionAudioSource;

		[global::UnityEngine.Tooltip("Collision audio clips. One will be chosen at random and played one shot with CollisionAudioSource.")]
		public global::UnityEngine.AudioClip[] CollisionAudioClips;

		[global::UnityEngine.Tooltip("Collision sound volume range.")]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats CollisionVolumeRange = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 0.4f,
			Maximum = 0.6f
		};

		[global::UnityEngine.Header("Duration and Cooldown")]
		[global::UnityEngine.Tooltip("The duration in seconds that the spell will last. Not all spells support a duration. For one shot spells, this is how long the spell cast / emission light, etc. will last.")]
		public float Duration;

		[global::UnityEngine.Tooltip("The cooldown in seconds. Once cast, the spell must wait for the cooldown before being cast again.")]
		public float Cooldown;

		[global::UnityEngine.Header("Emission")]
		[global::UnityEngine.Tooltip("Emission sound")]
		public global::UnityEngine.AudioSource EmissionSound;

		[global::UnityEngine.Tooltip("Emission particle system. For best results use world space, turn off looping and play on awake.")]
		public global::UnityEngine.ParticleSystem EmissionParticleSystem;

		[global::UnityEngine.Tooltip("Light to illuminate when spell is cast")]
		public global::UnityEngine.Light EmissionLight;

		private int stopToken;

		protected float DurationTimer { get; private set; }

		protected float CooldownTimer { get; private set; }

		public bool Casting { get; private set; }

		public bool CanCastSpell
		{
			get
			{
				if (!Casting)
				{
					return CooldownTimer <= 0f;
				}
				return false;
			}
		}

		private global::System.Collections.IEnumerator StopAfterSecondsCoRoutine(float seconds)
		{
			int token = stopToken;
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(seconds);
			if (token == stopToken)
			{
				StopSpell();
			}
		}

		protected void ApplyCollisionForce(global::UnityEngine.Vector3 point)
		{
			if (!(CollisionForce > 0f) || !(CollisionRadius > 0f))
			{
				return;
			}
			global::UnityEngine.Collider[] array = global::UnityEngine.Physics.OverlapSphere(point, CollisionRadius, CollisionMask);
			for (int i = 0; i < array.Length; i++)
			{
				global::UnityEngine.Rigidbody component = array[i].GetComponent<global::UnityEngine.Rigidbody>();
				if (component != null)
				{
					if (CollisionIsExplosion)
					{
						component.AddExplosionForce(CollisionForce, point, CollisionRadius, CollisionForce * 0.02f, CollisionForceMode);
					}
					else
					{
						component.AddForce(CollisionForce * Direction, CollisionForceMode);
					}
				}
			}
		}

		protected void PlayCollisionSound(global::UnityEngine.Vector3 pos)
		{
			if (CollisionAudioSource != null && CollisionAudioClips != null && CollisionAudioClips.Length != 0)
			{
				int num = global::UnityEngine.Random.Range(0, CollisionAudioClips.Length - 1);
				float volumeScale = global::UnityEngine.Random.Range(CollisionVolumeRange.Minimum, CollisionVolumeRange.Maximum);
				CollisionAudioSource.transform.position = pos;
				CollisionAudioSource.PlayOneShot(CollisionAudioClips[num], volumeScale);
			}
		}

		protected virtual void Start()
		{
			if (EmissionLight != null)
			{
				EmissionLight.enabled = false;
			}
		}

		protected virtual void Update()
		{
			CooldownTimer = global::UnityEngine.Mathf.Max(0f, CooldownTimer - global::DigitalRuby.ThunderAndLightning.LightningBoltScript.DeltaTime);
			DurationTimer = global::UnityEngine.Mathf.Max(0f, DurationTimer - global::DigitalRuby.ThunderAndLightning.LightningBoltScript.DeltaTime);
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void OnDestroy()
		{
		}

		protected abstract void OnCastSpell();

		protected abstract void OnStopSpell();

		protected virtual void OnActivated()
		{
		}

		protected virtual void OnDeactivated()
		{
		}

		public bool CastSpell()
		{
			if (!CanCastSpell)
			{
				return false;
			}
			Casting = true;
			DurationTimer = Duration;
			CooldownTimer = Cooldown;
			OnCastSpell();
			if (Duration > 0f)
			{
				StopAfterSeconds(Duration);
			}
			if (EmissionParticleSystem != null)
			{
				EmissionParticleSystem.Play();
			}
			if (EmissionLight != null)
			{
				EmissionLight.transform.position = SpellStart.transform.position;
				EmissionLight.enabled = true;
			}
			if (EmissionSound != null)
			{
				EmissionSound.Play();
			}
			return true;
		}

		public void StopSpell()
		{
			if (Casting)
			{
				stopToken++;
				if (EmissionParticleSystem != null)
				{
					EmissionParticleSystem.Stop();
				}
				if (EmissionLight != null)
				{
					EmissionLight.enabled = false;
				}
				if (EmissionSound != null && EmissionSound.loop)
				{
					EmissionSound.Stop();
				}
				DurationTimer = 0f;
				Casting = false;
				OnStopSpell();
			}
		}

		public void ActivateSpell()
		{
			OnActivated();
		}

		public void DeactivateSpell()
		{
			OnDeactivated();
		}

		public void StopAfterSeconds(float seconds)
		{
			StartCoroutine(StopAfterSecondsCoRoutine(seconds));
		}

		public static global::UnityEngine.GameObject FindChildRecursively(global::UnityEngine.Transform t, string name)
		{
			if (t.name == name)
			{
				return t.gameObject;
			}
			for (int i = 0; i < t.childCount; i++)
			{
				global::UnityEngine.GameObject gameObject = FindChildRecursively(t.GetChild(i), name);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			return null;
		}
	}
}
