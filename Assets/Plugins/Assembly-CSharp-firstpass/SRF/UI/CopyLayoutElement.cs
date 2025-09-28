namespace SRF.UI
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Copy Layout Element")]
	public class CopyLayoutElement : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.UI.ILayoutElement
	{
		public bool CopyMinHeight;

		public bool CopyMinWidth;

		public bool CopyPreferredHeight;

		public bool CopyPreferredWidth;

		public global::UnityEngine.RectTransform CopySource;

		public float PaddingMinHeight;

		public float PaddingMinWidth;

		public float PaddingPreferredHeight;

		public float PaddingPreferredWidth;

		public float preferredWidth
		{
			get
			{
				if (!CopyPreferredWidth || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetPreferredWidth(CopySource) + PaddingPreferredWidth;
			}
		}

		public float preferredHeight
		{
			get
			{
				if (!CopyPreferredHeight || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetPreferredHeight(CopySource) + PaddingPreferredHeight;
			}
		}

		public float minWidth
		{
			get
			{
				if (!CopyMinWidth || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetMinWidth(CopySource) + PaddingMinWidth;
			}
		}

		public float minHeight
		{
			get
			{
				if (!CopyMinHeight || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetMinHeight(CopySource) + PaddingMinHeight;
			}
		}

		public int layoutPriority => 2;

		public float flexibleHeight => -1f;

		public float flexibleWidth => -1f;

		public void CalculateLayoutInputHorizontal()
		{
		}

		public void CalculateLayoutInputVertical()
		{
		}
	}
}
