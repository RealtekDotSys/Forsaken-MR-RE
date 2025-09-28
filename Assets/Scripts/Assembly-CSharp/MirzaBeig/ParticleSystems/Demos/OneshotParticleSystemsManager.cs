namespace MirzaBeig.ParticleSystems.Demos
{
	public class OneshotParticleSystemsManager : global::MirzaBeig.ParticleSystems.Demos.ParticleManager
	{
		public global::UnityEngine.LayerMask mouseRaycastLayerMask = -1;

		private global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem[]> spawnedPrefabs;

		public bool disableSpawn { get; set; }

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
			disableSpawn = false;
			spawnedPrefabs = new global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem[]>();
		}

		private void OnEnable()
		{
		}

		public void Clear()
		{
			if (spawnedPrefabs == null)
			{
				return;
			}
			for (int i = 0; i < spawnedPrefabs.Count; i++)
			{
				if ((bool)spawnedPrefabs[i][0])
				{
					global::UnityEngine.Object.Destroy(spawnedPrefabs[i][0].gameObject);
				}
			}
			spawnedPrefabs.Clear();
		}

		protected override void Update()
		{
			base.Update();
		}

		public void InstantiateParticlePrefab(global::UnityEngine.Vector2 mousePosition, float maxDistance)
		{
			if (spawnedPrefabs != null && !disableSpawn)
			{
				global::UnityEngine.Vector3 position = mousePosition;
				position.z = maxDistance;
				global::UnityEngine.Vector3 vector = global::UnityEngine.Camera.main.ScreenToWorldPoint(position);
				global::UnityEngine.Vector3 direction = vector - global::UnityEngine.Camera.main.transform.position;
				global::UnityEngine.Physics.Raycast(global::UnityEngine.Camera.main.transform.position + global::UnityEngine.Camera.main.transform.forward * 0.01f, direction, out var hitInfo, maxDistance);
				global::UnityEngine.Vector3 position2 = ((!hitInfo.collider) ? vector : hitInfo.point);
				global::UnityEngine.ParticleSystem[] array = particlePrefabs[currentParticlePrefabIndex];
				global::UnityEngine.ParticleSystem particleSystem = global::UnityEngine.Object.Instantiate(array[0], position2, array[0].transform.rotation);
				particleSystem.gameObject.SetActive(value: true);
				particleSystem.transform.parent = base.transform;
				spawnedPrefabs.Add(particleSystem.GetComponentsInChildren<global::UnityEngine.ParticleSystem>());
			}
		}

		public void Randomize()
		{
			currentParticlePrefabIndex = global::UnityEngine.Random.Range(0, particlePrefabs.Count);
		}

		public override int GetParticleCount()
		{
			int num = 0;
			if (spawnedPrefabs != null)
			{
				for (int i = 0; i < spawnedPrefabs.Count; i++)
				{
					if ((bool)spawnedPrefabs[i][0])
					{
						for (int j = 0; j < spawnedPrefabs[i].Length; j++)
						{
							num += spawnedPrefabs[i][j].particleCount;
						}
					}
					else
					{
						spawnedPrefabs.RemoveAt(i);
					}
				}
			}
			return num;
		}
	}
}
