namespace DigitalRuby.ThunderAndLightning
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.ParticleSystem))]
	public class LightningParticleCollisionForwarder : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.Tooltip("The script to forward the collision to. Must implement ICollisionHandler.")]
		public global::UnityEngine.MonoBehaviour CollisionHandler;

		private global::UnityEngine.ParticleSystem _particleSystem;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.ParticleCollisionEvent> collisionEvents = new global::System.Collections.Generic.List<global::UnityEngine.ParticleCollisionEvent>();

		private void Start()
		{
			_particleSystem = GetComponent<global::UnityEngine.ParticleSystem>();
		}

		private void OnParticleCollision(global::UnityEngine.GameObject other)
		{
			if (CollisionHandler is global::DigitalRuby.ThunderAndLightning.ICollisionHandler collisionHandler)
			{
				int num = global::UnityEngine.ParticlePhysicsExtensions.GetCollisionEvents(_particleSystem, other, collisionEvents);
				if (num != 0)
				{
					collisionHandler.HandleCollision(other, collisionEvents, num);
				}
			}
		}
	}
}
