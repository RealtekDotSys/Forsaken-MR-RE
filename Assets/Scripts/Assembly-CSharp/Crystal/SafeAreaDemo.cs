namespace Crystal
{
	public class SafeAreaDemo : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.KeyCode KeySafeArea = global::UnityEngine.KeyCode.A;

		private global::Crystal.SafeArea.SimDevice[] Sims;

		private int SimIdx;

		private void Awake()
		{
			if (!global::UnityEngine.Application.isEditor)
			{
				global::UnityEngine.Object.Destroy(this);
			}
			Sims = (global::Crystal.SafeArea.SimDevice[])global::System.Enum.GetValues(typeof(global::Crystal.SafeArea.SimDevice));
		}

		private void Update()
		{
			if (global::UnityEngine.Input.GetKeyDown(KeySafeArea))
			{
				ToggleSafeArea();
			}
		}

		private void ToggleSafeArea()
		{
			SimIdx++;
			if (SimIdx >= Sims.Length)
			{
				SimIdx = 0;
			}
			global::Crystal.SafeArea.Sim = Sims[SimIdx];
			global::UnityEngine.Debug.LogFormat("Switched to sim device {0} with debug key '{1}'", Sims[SimIdx], KeySafeArea);
		}
	}
}
