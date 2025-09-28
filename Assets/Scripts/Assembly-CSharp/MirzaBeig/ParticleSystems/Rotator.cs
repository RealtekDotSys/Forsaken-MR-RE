namespace MirzaBeig.ParticleSystems
{
	public class Rotator : global::UnityEngine.MonoBehaviour
	{
		public global::UnityEngine.Vector3 localRotationSpeed;

		public global::UnityEngine.Vector3 worldRotationSpeed;

		public bool executeInEditMode;

		public bool unscaledTime;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void OnRenderObject()
		{
			if (executeInEditMode && !global::UnityEngine.Application.isPlaying)
			{
				rotate();
			}
		}

		private void Update()
		{
			if (global::UnityEngine.Application.isPlaying)
			{
				rotate();
			}
		}

		private void rotate()
		{
			float num = ((!unscaledTime) ? global::UnityEngine.Time.deltaTime : global::UnityEngine.Time.unscaledDeltaTime);
			if (localRotationSpeed != global::UnityEngine.Vector3.zero)
			{
				base.transform.Rotate(localRotationSpeed * num, global::UnityEngine.Space.Self);
			}
			if (worldRotationSpeed != global::UnityEngine.Vector3.zero)
			{
				base.transform.Rotate(worldRotationSpeed * num, global::UnityEngine.Space.World);
			}
		}
	}
}
