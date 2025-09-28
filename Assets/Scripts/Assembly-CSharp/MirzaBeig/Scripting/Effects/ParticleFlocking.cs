namespace MirzaBeig.Scripting.Effects
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.ParticleSystem))]
	public class ParticleFlocking : global::UnityEngine.MonoBehaviour
	{
		public struct Voxel
		{
			public global::UnityEngine.Bounds bounds;

			public int[] particles;

			public int particleCount;
		}

		[global::UnityEngine.Header("N^2 Mode Settings")]
		public float maxDistance = 0.5f;

		[global::UnityEngine.Header("Forces")]
		public float cohesion = 0.5f;

		public float separation = 0.25f;

		[global::UnityEngine.Header("Voxel Mode Settings")]
		public bool useVoxels = true;

		public bool voxelLocalCenterFromBounds = true;

		public float voxelVolume = 8f;

		public int voxelsPerAxis = 5;

		private int previousVoxelsPerAxisValue;

		private global::MirzaBeig.Scripting.Effects.ParticleFlocking.Voxel[] voxels;

		private global::UnityEngine.ParticleSystem particleSystem;

		private global::UnityEngine.ParticleSystem.Particle[] particles;

		private global::UnityEngine.Vector3[] particlePositions;

		private global::UnityEngine.ParticleSystem.MainModule particleSystemMainModule;

		[global::UnityEngine.Header("General Performance Settings")]
		[global::UnityEngine.Range(0f, 1f)]
		public float delay;

		private float timer;

		public bool alwaysUpdate;

		private bool visible;

		private void Start()
		{
			particleSystem = GetComponent<global::UnityEngine.ParticleSystem>();
			particleSystemMainModule = particleSystem.main;
		}

		private void OnBecameVisible()
		{
			visible = true;
		}

		private void OnBecameInvisible()
		{
			visible = false;
		}

		private void buildVoxelGrid()
		{
			int num = voxelsPerAxis * voxelsPerAxis * voxelsPerAxis;
			voxels = new global::MirzaBeig.Scripting.Effects.ParticleFlocking.Voxel[num];
			float num2 = voxelVolume / (float)voxelsPerAxis;
			float num3 = num2 / 2f;
			float num4 = voxelVolume / 2f;
			global::UnityEngine.Vector3 position = base.transform.position;
			int num5 = 0;
			for (int i = 0; i < voxelsPerAxis; i++)
			{
				float x = 0f - num4 + num3 + (float)i * num2;
				for (int j = 0; j < voxelsPerAxis; j++)
				{
					float y = 0f - num4 + num3 + (float)j * num2;
					for (int k = 0; k < voxelsPerAxis; k++)
					{
						float z = 0f - num4 + num3 + (float)k * num2;
						voxels[num5].particleCount = 0;
						voxels[num5].bounds = new global::UnityEngine.Bounds(position + new global::UnityEngine.Vector3(x, y, z), global::UnityEngine.Vector3.one * num2);
						num5++;
					}
				}
			}
		}

		private void LateUpdate()
		{
			if (!alwaysUpdate && !visible)
			{
				return;
			}
			if (useVoxels)
			{
				int num = voxelsPerAxis * voxelsPerAxis * voxelsPerAxis;
				if (voxels == null || voxels.Length < num)
				{
					buildVoxelGrid();
				}
			}
			int maxParticles = particleSystemMainModule.maxParticles;
			if (particles == null || particles.Length < maxParticles)
			{
				particles = new global::UnityEngine.ParticleSystem.Particle[maxParticles];
				particlePositions = new global::UnityEngine.Vector3[maxParticles];
				if (useVoxels)
				{
					for (int i = 0; i < voxels.Length; i++)
					{
						voxels[i].particles = new int[maxParticles];
					}
				}
			}
			timer += global::UnityEngine.Time.deltaTime;
			if (!(timer >= delay))
			{
				return;
			}
			float num2 = timer;
			timer = 0f;
			particleSystem.GetParticles(particles);
			int particleCount = particleSystem.particleCount;
			float num3 = cohesion * num2;
			float num4 = separation * num2;
			for (int j = 0; j < particleCount; j++)
			{
				particlePositions[j] = particles[j].position;
			}
			if (useVoxels)
			{
				int num5 = voxels.Length;
				float num6 = voxelVolume / (float)voxelsPerAxis;
				for (int k = 0; k < particleCount; k++)
				{
					for (int l = 0; l < num5; l++)
					{
						if (voxels[l].bounds.Contains(particlePositions[k]))
						{
							voxels[l].particles[voxels[l].particleCount] = k;
							voxels[l].particleCount++;
							break;
						}
					}
				}
				for (int m = 0; m < num5; m++)
				{
					if (voxels[m].particleCount <= 1)
					{
						continue;
					}
					for (int n = 0; n < voxels[m].particleCount; n++)
					{
						global::UnityEngine.Vector3 vector = particlePositions[voxels[m].particles[n]];
						global::UnityEngine.Vector3 vector2;
						if (voxelLocalCenterFromBounds)
						{
							vector2 = voxels[m].bounds.center - particlePositions[voxels[m].particles[n]];
						}
						else
						{
							for (int num7 = 0; num7 < voxels[m].particleCount; num7++)
							{
								if (num7 != n)
								{
									vector += particlePositions[voxels[m].particles[num7]];
								}
							}
							vector /= (float)voxels[m].particleCount;
							vector2 = vector - particlePositions[voxels[m].particles[n]];
						}
						float sqrMagnitude = vector2.sqrMagnitude;
						vector2.Normalize();
						global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
						zero += vector2 * num3;
						zero -= vector2 * ((1f - sqrMagnitude / num6) * num4);
						global::UnityEngine.Vector3 velocity = particles[voxels[m].particles[n]].velocity;
						velocity.x += zero.x;
						velocity.y += zero.y;
						velocity.z += zero.z;
						particles[voxels[m].particles[n]].velocity = velocity;
					}
					voxels[m].particleCount = 0;
				}
			}
			else
			{
				float num8 = maxDistance * maxDistance;
				global::UnityEngine.Vector3 vector4 = default(global::UnityEngine.Vector3);
				for (int num9 = 0; num9 < particleCount; num9++)
				{
					int num10 = 1;
					global::UnityEngine.Vector3 vector3 = particlePositions[num9];
					for (int num11 = 0; num11 < particleCount; num11++)
					{
						if (num11 != num9)
						{
							vector4.x = particlePositions[num9].x - particlePositions[num11].x;
							vector4.y = particlePositions[num9].y - particlePositions[num11].y;
							vector4.z = particlePositions[num9].z - particlePositions[num11].z;
							if (global::UnityEngine.Vector3.SqrMagnitude(vector4) <= num8)
							{
								num10++;
								vector3 += particlePositions[num11];
							}
						}
					}
					if (num10 != 1)
					{
						vector3 /= (float)num10;
						global::UnityEngine.Vector3 vector5 = vector3 - particlePositions[num9];
						float sqrMagnitude2 = vector5.sqrMagnitude;
						vector5.Normalize();
						global::UnityEngine.Vector3 zero2 = global::UnityEngine.Vector3.zero;
						zero2 += vector5 * num3;
						zero2 -= vector5 * ((1f - sqrMagnitude2 / num8) * num4);
						global::UnityEngine.Vector3 velocity2 = particles[num9].velocity;
						velocity2.x += zero2.x;
						velocity2.y += zero2.y;
						velocity2.z += zero2.z;
						particles[num9].velocity = velocity2;
					}
				}
			}
			particleSystem.SetParticles(particles, particleCount);
		}

		private void OnDrawGizmosSelected()
		{
			float num = voxelVolume / (float)voxelsPerAxis;
			float num2 = num / 2f;
			float num3 = voxelVolume / 2f;
			global::UnityEngine.Vector3 position = base.transform.position;
			global::UnityEngine.Gizmos.color = global::UnityEngine.Color.red;
			global::UnityEngine.Gizmos.DrawWireCube(position, global::UnityEngine.Vector3.one * voxelVolume);
			global::UnityEngine.Gizmos.color = global::UnityEngine.Color.white;
			for (int i = 0; i < voxelsPerAxis; i++)
			{
				float x = 0f - num3 + num2 + (float)i * num;
				for (int j = 0; j < voxelsPerAxis; j++)
				{
					float y = 0f - num3 + num2 + (float)j * num;
					for (int k = 0; k < voxelsPerAxis; k++)
					{
						float z = 0f - num3 + num2 + (float)k * num;
						global::UnityEngine.Gizmos.DrawWireCube(position + new global::UnityEngine.Vector3(x, y, z), global::UnityEngine.Vector3.one * num);
					}
				}
			}
		}
	}
}
