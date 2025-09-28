namespace SRDebugger.UI.Other
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.UI.ScrollRect))]
	[global::UnityEngine.ExecuteInEditMode]
	public class ScrollRectPatch : global::UnityEngine.MonoBehaviour
	{
		public global::UnityEngine.RectTransform Content;

		public global::UnityEngine.UI.Mask ReplaceMask;

		public global::UnityEngine.RectTransform Viewport;

		private void Awake()
		{
			global::UnityEngine.UI.ScrollRect component = GetComponent<global::UnityEngine.UI.ScrollRect>();
			component.content = Content;
			component.viewport = Viewport;
			if (ReplaceMask != null)
			{
				global::UnityEngine.GameObject obj = ReplaceMask.gameObject;
				global::UnityEngine.Object.Destroy(obj.GetComponent<global::UnityEngine.UI.Graphic>());
				global::UnityEngine.Object.Destroy(obj.GetComponent<global::UnityEngine.CanvasRenderer>());
				global::UnityEngine.Object.Destroy(ReplaceMask);
				obj.AddComponent<global::UnityEngine.UI.RectMask2D>();
			}
		}
	}
}
