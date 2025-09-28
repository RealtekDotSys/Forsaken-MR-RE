namespace SRDebugger.UI.Other
{
	public class PinnedUIRoot : global::SRF.SRMonoBehaviourEx
	{
		[global::SRF.RequiredField]
		public global::UnityEngine.Canvas Canvas;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform Container;

		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Other.DockConsoleController DockConsoleController;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject Options;

		[global::SRF.RequiredField]
		public global::SRF.UI.Layout.FlowLayoutGroup OptionsLayoutGroup;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject Profiler;

		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Other.HandleManager ProfilerHandleManager;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.VerticalLayoutGroup ProfilerVerticalLayoutGroup;
	}
}
