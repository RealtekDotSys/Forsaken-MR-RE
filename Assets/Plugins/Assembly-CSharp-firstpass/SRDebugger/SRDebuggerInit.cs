namespace SRDebugger
{
	[global::UnityEngine.AddComponentMenu("SRDebugger Init")]
	public class SRDebuggerInit : global::SRF.SRMonoBehaviourEx
	{
		protected override void Awake()
		{
			base.Awake();
			if (global::SRDebugger.Settings.Instance.IsEnabled)
			{
				SRDebug.Init();
			}
		}

		protected override void Start()
		{
			base.Start();
			global::UnityEngine.Object.Destroy(base.CachedGameObject);
		}
	}
}
