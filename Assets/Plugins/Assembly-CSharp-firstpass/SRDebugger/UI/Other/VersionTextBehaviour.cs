namespace SRDebugger.UI.Other
{
	public class VersionTextBehaviour : global::SRF.SRMonoBehaviourEx
	{
		public string Format = "SRDebugger {0}";

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Text;

		protected override void Start()
		{
			base.Start();
			Text.text = string.Format(Format, "1.7.1");
		}
	}
}
