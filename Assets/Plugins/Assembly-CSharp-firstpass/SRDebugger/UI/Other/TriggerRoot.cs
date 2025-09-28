namespace SRDebugger.UI.Other
{
	public class TriggerRoot : global::SRF.SRMonoBehaviourEx
	{
		[global::SRF.RequiredField]
		public global::UnityEngine.Canvas Canvas;

		[global::SRF.RequiredField]
		public global::SRF.UI.LongPressButton TapHoldButton;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform TriggerTransform;

		[global::SRF.RequiredField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("TriggerButton")]
		public global::SRDebugger.UI.Controls.MultiTapButton TripleTapButton;
	}
}
