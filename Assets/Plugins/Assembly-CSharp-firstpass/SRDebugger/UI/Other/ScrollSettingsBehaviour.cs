namespace SRDebugger.UI.Other
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.UI.ScrollRect))]
	public class ScrollSettingsBehaviour : global::UnityEngine.MonoBehaviour
	{
		public const float ScrollSensitivity = 40f;

		private void Awake()
		{
			global::UnityEngine.UI.ScrollRect component = GetComponent<global::UnityEngine.UI.ScrollRect>();
			component.scrollSensitivity = 40f;
			if (!global::SRDebugger.Internal.SRDebuggerUtil.IsMobilePlatform)
			{
				component.movementType = global::UnityEngine.UI.ScrollRect.MovementType.Clamped;
				component.inertia = false;
			}
		}
	}
}
