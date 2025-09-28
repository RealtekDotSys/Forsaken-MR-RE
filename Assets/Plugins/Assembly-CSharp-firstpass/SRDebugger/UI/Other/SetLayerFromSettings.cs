namespace SRDebugger.UI.Other
{
	public class SetLayerFromSettings : global::SRF.SRMonoBehaviour
	{
		private void Start()
		{
			global::SRF.SRFGameObjectExtensions.SetLayerRecursive(base.gameObject, global::SRDebugger.Settings.Instance.DebugLayer);
		}
	}
}
