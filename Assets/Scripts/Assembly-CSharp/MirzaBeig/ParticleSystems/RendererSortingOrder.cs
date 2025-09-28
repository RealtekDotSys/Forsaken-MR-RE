namespace MirzaBeig.ParticleSystems
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Renderer))]
	public class RendererSortingOrder : global::UnityEngine.MonoBehaviour
	{
		public int sortingOrder;

		private void Awake()
		{
		}

		private void Start()
		{
			GetComponent<global::UnityEngine.Renderer>().sortingOrder = sortingOrder;
		}

		private void Update()
		{
		}
	}
}
