namespace SRDebugger.UI.Controls
{
	public class SRTabButton : global::SRF.SRMonoBehaviourEx
	{
		[global::SRF.RequiredField]
		public global::UnityEngine.Behaviour ActiveToggle;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Button Button;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform ExtraContentContainer;

		[global::SRF.RequiredField]
		public global::SRF.UI.StyleComponent IconStyleComponent;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text TitleText;

		public bool IsActive
		{
			get
			{
				return ActiveToggle.enabled;
			}
			set
			{
				ActiveToggle.enabled = value;
			}
		}
	}
}
