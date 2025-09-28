namespace SRF.UI
{
	[global::UnityEngine.AddComponentMenu("SRF/UI/SRText")]
	public class SRText : global::UnityEngine.UI.Text
	{
		public event global::System.Action<global::SRF.UI.SRText> LayoutDirty;

		public override void SetLayoutDirty()
		{
			base.SetLayoutDirty();
			if (this.LayoutDirty != null)
			{
				this.LayoutDirty(this);
			}
		}
	}
}
