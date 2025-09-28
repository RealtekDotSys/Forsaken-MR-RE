namespace SRF.UI
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Copy Size Into Layout Element")]
	public class CopySizeIntoLayoutElement : global::UnityEngine.UI.LayoutElement
	{
		public global::UnityEngine.RectTransform CopySource;

		public float PaddingHeight;

		public float PaddingWidth;

		public bool SetPreferredSize;

		public bool SetMinimumSize;

		public override float preferredWidth
		{
			get
			{
				if (!SetPreferredSize || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return CopySource.rect.width + PaddingWidth;
			}
		}

		public override float preferredHeight
		{
			get
			{
				if (!SetPreferredSize || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return CopySource.rect.height + PaddingHeight;
			}
		}

		public override float minWidth
		{
			get
			{
				if (!SetMinimumSize || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return CopySource.rect.width + PaddingWidth;
			}
		}

		public override float minHeight
		{
			get
			{
				if (!SetMinimumSize || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return CopySource.rect.height + PaddingHeight;
			}
		}

		public override int layoutPriority => 2;
	}
}
