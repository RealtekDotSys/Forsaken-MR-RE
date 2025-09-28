namespace MirzaBeig.Scripting.Effects
{
	public abstract class ParticleForceField : global::UnityEngine.MonoBehaviour
	{
		protected struct GetForceParameters
		{
			public float distanceToForceFieldCenterSqr;

			public global::UnityEngine.Vector3 scaledDirectionToForceFieldCenter;

			public global::UnityEngine.Vector3 particlePosition;
		}

		[global::UnityEngine.Header("Common Controls")]
		[global::UnityEngine.Tooltip("Force field spherical range.")]
		public float radius = float.PositiveInfinity;

		[global::UnityEngine.Tooltip("Maximum baseline force.")]
		public float force = 5f;

		[global::UnityEngine.Tooltip("Internal force field position offset.")]
		public global::UnityEngine.Vector3 center = global::UnityEngine.Vector3.zero;

		private float _radius;

		private float radiusSqr;

		private global::UnityEngine.Vector3 transformPosition;

		private float[] particleSystemExternalForcesMultipliers;

		[global::UnityEngine.Tooltip("Force scale as determined by distance to individual particles.")]
		public global::UnityEngine.AnimationCurve forceOverDistance = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 1f), new global::UnityEngine.Keyframe(1f, 1f));

		private global::UnityEngine.ParticleSystem particleSystem;

		[global::UnityEngine.Tooltip("If nothing no particle systems are assigned, this force field will operate globally on ALL particle systems in the scene (NOT recommended).\n\nIf attached to a particle system, the force field will operate only on that system.\n\nIf specific particle systems are assigned, then the force field will operate on those systems only, even if attached to a particle system.")]
		public global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem> _particleSystems;

		private int particleSystemsCount;

		private global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem> particleSystems = new global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem>();

		private global::UnityEngine.ParticleSystem.Particle[][] particleSystemParticles;

		private global::UnityEngine.ParticleSystem.MainModule[] particleSystemMainModules;

		private global::UnityEngine.Renderer[] particleSystemRenderers;

		protected global::UnityEngine.ParticleSystem currentParticleSystem;

		protected global::MirzaBeig.Scripting.Effects.ParticleForceField.GetForceParameters parameters;

		[global::UnityEngine.Tooltip("If TRUE, update even if target particle system(s) are invisible/offscreen.\n\nIf FALSE, update only if particles of the target system(s) are visible/onscreen.")]
		public bool alwaysUpdate;

		public float scaledRadius => radius * base.transform.lossyScale.x;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
			particleSystem = GetComponent<global::UnityEngine.ParticleSystem>();
		}

		protected virtual void PerParticleSystemSetup()
		{
		}

		protected virtual global::UnityEngine.Vector3 GetForce()
		{
			return global::UnityEngine.Vector3.zero;
		}

		protected virtual void Update()
		{
		}

		public void AddParticleSystem(global::UnityEngine.ParticleSystem particleSystem)
		{
			_particleSystems.Add(particleSystem);
		}

		public void RemoveParticleSystem(global::UnityEngine.ParticleSystem particleSystem)
		{
			_particleSystems.Remove(particleSystem);
		}

		protected virtual void LateUpdate()
		{
			_radius = scaledRadius;
			radiusSqr = _radius * _radius;
			transformPosition = base.transform.position + center;
			if (_particleSystems.Count != 0)
			{
				if (particleSystems.Count != _particleSystems.Count)
				{
					particleSystems.Clear();
					particleSystems.AddRange(_particleSystems);
				}
				else
				{
					for (int i = 0; i < _particleSystems.Count; i++)
					{
						particleSystems[i] = _particleSystems[i];
					}
				}
			}
			else if ((bool)particleSystem)
			{
				if (particleSystems.Count == 1)
				{
					particleSystems[0] = particleSystem;
				}
				else
				{
					particleSystems.Clear();
					particleSystems.Add(particleSystem);
				}
			}
			else
			{
				particleSystems.Clear();
				particleSystems.AddRange(global::UnityEngine.Object.FindObjectsOfType<global::UnityEngine.ParticleSystem>());
			}
			parameters = default(global::MirzaBeig.Scripting.Effects.ParticleForceField.GetForceParameters);
			particleSystemsCount = particleSystems.Count;
			if (particleSystemParticles == null || particleSystemParticles.Length < particleSystemsCount)
			{
				particleSystemParticles = new global::UnityEngine.ParticleSystem.Particle[particleSystemsCount][];
				particleSystemMainModules = new global::UnityEngine.ParticleSystem.MainModule[particleSystemsCount];
				particleSystemRenderers = new global::UnityEngine.Renderer[particleSystemsCount];
				particleSystemExternalForcesMultipliers = new float[particleSystemsCount];
				for (int j = 0; j < particleSystemsCount; j++)
				{
					particleSystemMainModules[j] = particleSystems[j].main;
					particleSystemRenderers[j] = particleSystems[j].GetComponent<global::UnityEngine.Renderer>();
					particleSystemExternalForcesMultipliers[j] = particleSystems[j].externalForces.multiplier;
				}
			}
			for (int k = 0; k < particleSystemsCount; k++)
			{
				if (!particleSystemRenderers[k].isVisible && !alwaysUpdate)
				{
					continue;
				}
				int maxParticles = particleSystemMainModules[k].maxParticles;
				float num = force * (particleSystemMainModules[k].useUnscaledTime ? global::UnityEngine.Time.unscaledDeltaTime : global::UnityEngine.Time.deltaTime);
				if (particleSystemParticles[k] == null || particleSystemParticles[k].Length < maxParticles)
				{
					particleSystemParticles[k] = new global::UnityEngine.ParticleSystem.Particle[maxParticles];
				}
				currentParticleSystem = particleSystems[k];
				PerParticleSystemSetup();
				int particles = currentParticleSystem.GetParticles(particleSystemParticles[k]);
				global::UnityEngine.ParticleSystemSimulationSpace simulationSpace = particleSystemMainModules[k].simulationSpace;
				global::UnityEngine.ParticleSystemScalingMode scalingMode = particleSystemMainModules[k].scalingMode;
				global::UnityEngine.Transform transform = currentParticleSystem.transform;
				global::UnityEngine.Transform customSimulationSpace = particleSystemMainModules[k].customSimulationSpace;
				if (simulationSpace == global::UnityEngine.ParticleSystemSimulationSpace.World)
				{
					for (int l = 0; l < particles; l++)
					{
						parameters.particlePosition = particleSystemParticles[k][l].position;
						parameters.scaledDirectionToForceFieldCenter.x = transformPosition.x - parameters.particlePosition.x;
						parameters.scaledDirectionToForceFieldCenter.y = transformPosition.y - parameters.particlePosition.y;
						parameters.scaledDirectionToForceFieldCenter.z = transformPosition.z - parameters.particlePosition.z;
						parameters.distanceToForceFieldCenterSqr = parameters.scaledDirectionToForceFieldCenter.sqrMagnitude;
						if (parameters.distanceToForceFieldCenterSqr < radiusSqr)
						{
							float time = parameters.distanceToForceFieldCenterSqr / radiusSqr;
							float num2 = forceOverDistance.Evaluate(time);
							global::UnityEngine.Vector3 vector = GetForce();
							float num3 = num * num2 * particleSystemExternalForcesMultipliers[k];
							vector.x *= num3;
							vector.y *= num3;
							vector.z *= num3;
							global::UnityEngine.Vector3 velocity = particleSystemParticles[k][l].velocity;
							velocity.x += vector.x;
							velocity.y += vector.y;
							velocity.z += vector.z;
							particleSystemParticles[k][l].velocity = velocity;
						}
					}
				}
				else
				{
					global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
					global::UnityEngine.Quaternion identity = global::UnityEngine.Quaternion.identity;
					global::UnityEngine.Vector3 one = global::UnityEngine.Vector3.one;
					global::UnityEngine.Transform transform2 = transform;
					switch (simulationSpace)
					{
					case global::UnityEngine.ParticleSystemSimulationSpace.Local:
						zero = transform2.position;
						identity = transform2.rotation;
						one = transform2.localScale;
						break;
					case global::UnityEngine.ParticleSystemSimulationSpace.Custom:
						transform2 = customSimulationSpace;
						zero = transform2.position;
						identity = transform2.rotation;
						one = transform2.localScale;
						break;
					default:
						throw new global::System.NotSupportedException($"Unsupported scaling mode '{simulationSpace}'.");
					}
					for (int m = 0; m < particles; m++)
					{
						parameters.particlePosition = particleSystemParticles[k][m].position;
						if (simulationSpace == global::UnityEngine.ParticleSystemSimulationSpace.Local || simulationSpace == global::UnityEngine.ParticleSystemSimulationSpace.Custom)
						{
							switch (scalingMode)
							{
							case global::UnityEngine.ParticleSystemScalingMode.Hierarchy:
								parameters.particlePosition = transform2.TransformPoint(particleSystemParticles[k][m].position);
								break;
							case global::UnityEngine.ParticleSystemScalingMode.Local:
								parameters.particlePosition = global::UnityEngine.Vector3.Scale(parameters.particlePosition, one);
								parameters.particlePosition = identity * parameters.particlePosition;
								parameters.particlePosition += zero;
								break;
							case global::UnityEngine.ParticleSystemScalingMode.Shape:
								parameters.particlePosition = identity * parameters.particlePosition;
								parameters.particlePosition += zero;
								break;
							default:
								throw new global::System.NotSupportedException($"Unsupported scaling mode '{scalingMode}'.");
							}
						}
						parameters.scaledDirectionToForceFieldCenter.x = transformPosition.x - parameters.particlePosition.x;
						parameters.scaledDirectionToForceFieldCenter.y = transformPosition.y - parameters.particlePosition.y;
						parameters.scaledDirectionToForceFieldCenter.z = transformPosition.z - parameters.particlePosition.z;
						parameters.distanceToForceFieldCenterSqr = parameters.scaledDirectionToForceFieldCenter.sqrMagnitude;
						if (!(parameters.distanceToForceFieldCenterSqr < radiusSqr))
						{
							continue;
						}
						float time2 = parameters.distanceToForceFieldCenterSqr / radiusSqr;
						float num4 = forceOverDistance.Evaluate(time2);
						global::UnityEngine.Vector3 vector2 = GetForce();
						float num5 = num * num4 * particleSystemExternalForcesMultipliers[k];
						vector2.x *= num5;
						vector2.y *= num5;
						vector2.z *= num5;
						if (simulationSpace == global::UnityEngine.ParticleSystemSimulationSpace.Local || simulationSpace == global::UnityEngine.ParticleSystemSimulationSpace.Custom)
						{
							switch (scalingMode)
							{
							case global::UnityEngine.ParticleSystemScalingMode.Hierarchy:
								vector2 = transform2.InverseTransformVector(vector2);
								break;
							case global::UnityEngine.ParticleSystemScalingMode.Local:
								vector2 = global::UnityEngine.Quaternion.Inverse(identity) * vector2;
								vector2 = global::UnityEngine.Vector3.Scale(vector2, new global::UnityEngine.Vector3(1f / one.x, 1f / one.y, 1f / one.z));
								break;
							case global::UnityEngine.ParticleSystemScalingMode.Shape:
								vector2 = global::UnityEngine.Quaternion.Inverse(identity) * vector2;
								break;
							default:
								throw new global::System.NotSupportedException($"Unsupported scaling mode '{scalingMode}'.");
							}
						}
						global::UnityEngine.Vector3 velocity2 = particleSystemParticles[k][m].velocity;
						velocity2.x += vector2.x;
						velocity2.y += vector2.y;
						velocity2.z += vector2.z;
						particleSystemParticles[k][m].velocity = velocity2;
					}
				}
				currentParticleSystem.SetParticles(particleSystemParticles[k], particles);
			}
		}

		private void OnApplicationQuit()
		{
		}

		protected virtual void OnDrawGizmosSelected()
		{
			global::UnityEngine.Gizmos.color = global::UnityEngine.Color.green;
			global::UnityEngine.Gizmos.DrawWireSphere(base.transform.position + center, scaledRadius);
		}
	}
}
