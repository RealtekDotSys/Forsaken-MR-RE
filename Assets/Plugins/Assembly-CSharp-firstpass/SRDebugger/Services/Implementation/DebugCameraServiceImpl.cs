namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IDebugCameraService))]
	public class DebugCameraServiceImpl : global::SRDebugger.Services.IDebugCameraService
	{
		private global::UnityEngine.Camera _debugCamera;

		public global::UnityEngine.Camera Camera => _debugCamera;

		public DebugCameraServiceImpl()
		{
			if (global::SRDebugger.Settings.Instance.UseDebugCamera)
			{
				_debugCamera = new global::UnityEngine.GameObject("SRDebugCamera").AddComponent<global::UnityEngine.Camera>();
				_debugCamera.cullingMask = 1 << global::SRDebugger.Settings.Instance.DebugLayer;
				_debugCamera.depth = global::SRDebugger.Settings.Instance.DebugCameraDepth;
				_debugCamera.clearFlags = global::UnityEngine.CameraClearFlags.Depth;
				_debugCamera.transform.SetParent(global::SRF.Hierarchy.Get("SRDebugger"));
			}
		}
	}
}
