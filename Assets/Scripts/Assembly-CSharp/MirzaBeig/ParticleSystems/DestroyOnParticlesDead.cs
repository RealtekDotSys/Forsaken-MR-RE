namespace MirzaBeig.ParticleSystems
{
	public class DestroyOnParticlesDead : global::MirzaBeig.ParticleSystems.ParticleSystems
	{
		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
		}

		private void onParticleSystemsDead()
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}

		protected override void Update()
		{
			base.Update();
		}

		protected override void LateUpdate()
		{
			base.LateUpdate();
		}
	}
}
