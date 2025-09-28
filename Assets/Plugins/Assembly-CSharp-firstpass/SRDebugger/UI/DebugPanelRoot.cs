namespace SRDebugger.UI
{
	public class DebugPanelRoot : global::SRF.SRMonoBehaviourEx
	{
		[global::SRF.RequiredField]
		public global::UnityEngine.Canvas Canvas;

		[global::SRF.RequiredField]
		public global::UnityEngine.CanvasGroup CanvasGroup;

		[global::SRF.RequiredField]
		public global::SRDebugger.Scripts.DebuggerTabController TabController;

		public void Close()
		{
			global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IDebugService>().HideDebugPanel();
		}

		public void CloseAndDestroy()
		{
			global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IDebugService>().DestroyDebugPanel();
		}
	}
}
