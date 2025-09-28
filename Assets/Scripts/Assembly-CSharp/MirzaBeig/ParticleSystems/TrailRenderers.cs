namespace MirzaBeig.ParticleSystems
{
	public class TrailRenderers : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.TrailRenderer[] trailRenderers;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
			trailRenderers = GetComponentsInChildren<global::UnityEngine.TrailRenderer>();
		}

		protected virtual void Update()
		{
		}

		public void setAutoDestruct(bool value)
		{
			for (int i = 0; i < trailRenderers.Length; i++)
			{
				trailRenderers[i].autodestruct = value;
			}
		}
	}
}
