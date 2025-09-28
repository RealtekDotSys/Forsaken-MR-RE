namespace MirzaBeig.Scripting.Effects
{
	public abstract class ParticleAffector : global::UnityEngine.MonoBehaviour
	{
		protected struct GetForceParameters
		{
			public float distanceToAffectorCenterSqr;

			public global::UnityEngine.Vector3 scaledDirectionToAffectorCenter;

			public global::UnityEngine.Vector3 particlePosition;
		}

		[global::UnityEngine.Header("Common Controls")]
		public float radius = float.PositiveInfinity;

		public float force = 5f;

		public global::UnityEngine.Vector3 offset = global::UnityEngine.Vector3.zero;

		private float _radius;

		private float radiusSqr;

		private float forceDeltaTime;

		private global::UnityEngine.Vector3 transformPosition;

		private float[] particleSystemExternalForcesMultipliers;

		public global::UnityEngine.AnimationCurve scaleForceByDistance = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 1f), new global::UnityEngine.Keyframe(1f, 1f));

		private global::UnityEngine.ParticleSystem particleSystem;

		public global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem> _particleSystems;

		private int particleSystemsCount;

		private global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem> particleSystems = new global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem>();

		private global::UnityEngine.ParticleSystem.Particle[][] particleSystemParticles;

		private global::UnityEngine.ParticleSystem.MainModule[] particleSystemMainModules;

		private global::UnityEngine.Renderer[] particleSystemRenderers;

		protected global::UnityEngine.ParticleSystem currentParticleSystem;

		protected global::MirzaBeig.Scripting.Effects.ParticleAffector.GetForceParameters parameters;

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
			forceDeltaTime = force * global::UnityEngine.Time.deltaTime;
			transformPosition = base.transform.position + offset;
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
			parameters = default(global::MirzaBeig.Scripting.Effects.ParticleAffector.GetForceParameters);
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
						parameters.scaledDirectionToAffectorCenter.x = transformPosition.x - parameters.particlePosition.x;
						parameters.scaledDirectionToAffectorCenter.y = transformPosition.y - parameters.particlePosition.y;
						parameters.scaledDirectionToAffectorCenter.z = transformPosition.z - parameters.particlePosition.z;
						parameters.distanceToAffectorCenterSqr = parameters.scaledDirectionToAffectorCenter.sqrMagnitude;
						if (parameters.distanceToAffectorCenterSqr < radiusSqr)
						{
							float time = parameters.distanceToAffectorCenterSqr / radiusSqr;
							float num = scaleForceByDistance.Evaluate(time);
							global::UnityEngine.Vector3 vector = GetForce();
							float num2 = forceDeltaTime * num * particleSystemExternalForcesMultipliers[k];
							vector.x *= num2;
							vector.y *= num2;
							vector.z *= num2;
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
						parameters.scaledDirectionToAffectorCenter.x = transformPosition.x - parameters.particlePosition.x;
						parameters.scaledDirectionToAffectorCenter.y = transformPosition.y - parameters.particlePosition.y;
						parameters.scaledDirectionToAffectorCenter.z = transformPosition.z - parameters.particlePosition.z;
						parameters.distanceToAffectorCenterSqr = parameters.scaledDirectionToAffectorCenter.sqrMagnitude;
						if (!(parameters.distanceToAffectorCenterSqr < radiusSqr))
						{
							continue;
						}
						float time2 = parameters.distanceToAffectorCenterSqr / radiusSqr;
						float num3 = scaleForceByDistance.Evaluate(time2);
						global::UnityEngine.Vector3 vector2 = GetForce();
						float num4 = forceDeltaTime * num3 * particleSystemExternalForcesMultipliers[k];
						vector2.x *= num4;
						vector2.y *= num4;
						vector2.z *= num4;
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
			global::UnityEngine.Gizmos.DrawWireSphere(base.transform.position + offset, scaledRadius);
		}
	}
}
