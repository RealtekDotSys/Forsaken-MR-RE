public class ParticleSystemDuration : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.ParticleSystem[] particleSystems;

	public float GetDuration()
	{
		global::System.Collections.Generic.List<float> list = new global::System.Collections.Generic.List<float>();
		global::UnityEngine.ParticleSystem[] array = particleSystems;
		foreach (global::UnityEngine.ParticleSystem particle in array)
		{
			list.Add(GetIndividualDuration(particle));
		}
		return global::System.Linq.Enumerable.Max(list);
	}

	private static float GetIndividualDuration(global::UnityEngine.ParticleSystem particle)
	{
		float num = particle.main.startLifetime.constant;
		_ = particle.emission;
		if (particle.main.duration > num)
		{
			num = particle.main.duration;
		}
		if (!particle.trails.enabled)
		{
			return num;
		}
		if (particle.trails.lifetime.constant > num)
		{
			return particle.trails.lifetime.constant;
		}
		return num;
	}
}
