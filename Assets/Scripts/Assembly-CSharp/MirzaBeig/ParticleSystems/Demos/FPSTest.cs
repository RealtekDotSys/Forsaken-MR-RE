namespace MirzaBeig.ParticleSystems.Demos
{
	public class FPSTest : global::UnityEngine.MonoBehaviour
	{
		public int targetFPS1 = 60;

		public int targetFPS2 = 10;

		private int previousVSyncCount;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void Update()
		{
			if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.Space))
			{
				global::UnityEngine.Application.targetFrameRate = targetFPS2;
				previousVSyncCount = global::UnityEngine.QualitySettings.vSyncCount;
				global::UnityEngine.QualitySettings.vSyncCount = 0;
			}
			else if (global::UnityEngine.Input.GetKeyUp(global::UnityEngine.KeyCode.Space))
			{
				global::UnityEngine.Application.targetFrameRate = targetFPS1;
				global::UnityEngine.QualitySettings.vSyncCount = previousVSyncCount;
			}
		}
	}
}
