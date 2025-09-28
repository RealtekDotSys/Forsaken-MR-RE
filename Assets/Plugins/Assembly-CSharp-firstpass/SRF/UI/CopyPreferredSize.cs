namespace SRF.UI
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Copy Preferred Size")]
	public class CopyPreferredSize : global::UnityEngine.UI.LayoutElement
	{
		public global::UnityEngine.RectTransform CopySource;

		public float PaddingHeight;

		public float PaddingWidth;

		public override float preferredWidth
		{
			get
			{
				if (CopySource == null || !IsActive())
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetPreferredWidth(CopySource) + PaddingWidth;
			}
		}

		public override float preferredHeight
		{
			get
			{
				if (CopySource == null || !IsActive())
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetPreferredHeight(CopySource) + PaddingHeight;
			}
		}

		public override int layoutPriority => 2;
	}
}
