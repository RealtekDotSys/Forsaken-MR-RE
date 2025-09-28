namespace DigitalRuby.ThunderAndLightning
{
	public interface ICollisionHandler
	{
		void HandleCollision(global::UnityEngine.GameObject obj, global::System.Collections.Generic.List<global::UnityEngine.ParticleCollisionEvent> collision, int collisionCount);
	}
}
