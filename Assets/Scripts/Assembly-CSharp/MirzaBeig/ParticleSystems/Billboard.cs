namespace MirzaBeig.ParticleSystems
{
	public class Billboard : global::UnityEngine.MonoBehaviour
	{
		private void LateUpdate()
		{
			base.transform.LookAt(global::UnityEngine.Camera.main.transform.position);
		}
	}
}
