namespace SRF.UI
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Responsive (Enable)")]
	public class ResponsiveResize : global::SRF.UI.ResponsiveBase
	{
		[global::System.Serializable]
		public struct Element
		{
			[global::System.Serializable]
			public struct SizeDefinition
			{
				[global::UnityEngine.Tooltip("Width to apply when over the threshold width")]
				public float ElementWidth;

				[global::UnityEngine.Tooltip("Threshold over which this width will take effect")]
				public float ThresholdWidth;
			}

			public global::SRF.UI.ResponsiveResize.Element.SizeDefinition[] SizeDefinitions;

			public global::UnityEngine.RectTransform Target;
		}

		public global::SRF.UI.ResponsiveResize.Element[] Elements = new global::SRF.UI.ResponsiveResize.Element[0];

		protected override void Refresh()
		{
			global::UnityEngine.Rect rect = base.RectTransform.rect;
			for (int i = 0; i < Elements.Length; i++)
			{
				global::SRF.UI.ResponsiveResize.Element element = Elements[i];
				if (element.Target == null)
				{
					continue;
				}
				float num = float.MinValue;
				float num2 = -1f;
				for (int j = 0; j < element.SizeDefinitions.Length; j++)
				{
					global::SRF.UI.ResponsiveResize.Element.SizeDefinition sizeDefinition = element.SizeDefinitions[j];
					if (sizeDefinition.ThresholdWidth <= rect.width && sizeDefinition.ThresholdWidth > num)
					{
						num = sizeDefinition.ThresholdWidth;
						num2 = sizeDefinition.ElementWidth;
					}
				}
				if (num2 > 0f)
				{
					element.Target.SetSizeWithCurrentAnchors(global::UnityEngine.RectTransform.Axis.Horizontal, num2);
					global::UnityEngine.UI.LayoutElement component = element.Target.GetComponent<global::UnityEngine.UI.LayoutElement>();
					if (component != null)
					{
						component.preferredWidth = num2;
					}
				}
			}
		}
	}
}
