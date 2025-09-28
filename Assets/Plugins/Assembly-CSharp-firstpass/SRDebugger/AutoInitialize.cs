namespace SRDebugger
{
	public static class AutoInitialize
	{
		[global::UnityEngine.RuntimeInitializeOnLoadMethod]
		public static void OnLoad()
		{
			if (global::SRDebugger.Settings.Instance.IsEnabled && global::SRDebugger.Settings.Instance.AutoLoad)
			{
				SRDebug.Init();
			}
		}
	}
}
