namespace SRF.UI
{
	[global::System.Serializable]
	public class Style
	{
		public global::UnityEngine.Color ActiveColor = global::UnityEngine.Color.white;

		public global::UnityEngine.Color DisabledColor = global::UnityEngine.Color.white;

		public global::UnityEngine.Color HoverColor = global::UnityEngine.Color.white;

		public global::UnityEngine.Sprite Image;

		public global::UnityEngine.Color NormalColor = global::UnityEngine.Color.white;

		public global::SRF.UI.Style Copy()
		{
			global::SRF.UI.Style style = new global::SRF.UI.Style();
			style.CopyFrom(this);
			return style;
		}

		public void CopyFrom(global::SRF.UI.Style style)
		{
			Image = style.Image;
			NormalColor = style.NormalColor;
			HoverColor = style.HoverColor;
			ActiveColor = style.ActiveColor;
			DisabledColor = style.DisabledColor;
		}
	}
}
