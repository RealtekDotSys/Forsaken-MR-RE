namespace SRF.UI
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Content Fit Text")]
	public class ContentFitText : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.UI.ILayoutElement
	{
		public global::SRF.UI.SRText CopySource;

		public global::UnityEngine.Vector2 Padding;

		public float minWidth
		{
			get
			{
				if (CopySource == null)
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetMinWidth(CopySource.rectTransform) + Padding.x;
			}
		}

		public float preferredWidth
		{
			get
			{
				if (CopySource == null)
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetPreferredWidth(CopySource.rectTransform) + Padding.x;
			}
		}

		public float flexibleWidth
		{
			get
			{
				if (CopySource == null)
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetFlexibleWidth(CopySource.rectTransform);
			}
		}

		public float minHeight
		{
			get
			{
				if (CopySource == null)
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetFlexibleHeight(CopySource.rectTransform) + Padding.y;
			}
		}

		public float preferredHeight
		{
			get
			{
				if (CopySource == null)
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetPreferredHeight(CopySource.rectTransform) + Padding.y;
			}
		}

		public float flexibleHeight
		{
			get
			{
				if (CopySource == null)
				{
					return -1f;
				}
				return global::UnityEngine.UI.LayoutUtility.GetFlexibleHeight(CopySource.rectTransform);
			}
		}

		public int layoutPriority => 0;

		public void CalculateLayoutInputHorizontal()
		{
			CopySource.CalculateLayoutInputHorizontal();
		}

		public void CalculateLayoutInputVertical()
		{
			CopySource.CalculateLayoutInputVertical();
		}

		protected override void OnEnable()
		{
			SetDirty();
			CopySource.LayoutDirty += CopySourceOnLayoutDirty;
		}

		private void CopySourceOnLayoutDirty(global::SRF.UI.SRText srText)
		{
			SetDirty();
		}

		protected override void OnTransformParentChanged()
		{
			SetDirty();
		}

		protected override void OnDisable()
		{
			CopySource.LayoutDirty -= CopySourceOnLayoutDirty;
			SetDirty();
		}

		protected override void OnDidApplyAnimationProperties()
		{
			SetDirty();
		}

		protected override void OnBeforeTransformParentChanged()
		{
			SetDirty();
		}

		protected void SetDirty()
		{
			if (IsActive())
			{
				global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(base.transform as global::UnityEngine.RectTransform);
			}
		}
	}
}
