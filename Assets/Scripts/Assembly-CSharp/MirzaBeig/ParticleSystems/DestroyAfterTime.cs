namespace MirzaBeig.ParticleSystems
{
	public class DestroyAfterTime : global::UnityEngine.MonoBehaviour
	{
		public float time = 2f;

		private void Start()
		{
			global::UnityEngine.Object.Destroy(base.gameObject, time);
		}
	}
}
