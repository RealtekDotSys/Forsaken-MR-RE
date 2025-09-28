namespace SRDebugger.Profiler
{
	public class ProfilerLateUpdateListener : global::UnityEngine.MonoBehaviour
	{
		public global::System.Action OnLateUpdate;

		private void LateUpdate()
		{
			if (OnLateUpdate != null)
			{
				OnLateUpdate();
			}
		}
	}
}
