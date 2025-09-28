namespace SRF.UI
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Responsive (Enable)")]
	public class ResponsiveEnable : global::SRF.UI.ResponsiveBase
	{
		public enum Modes
		{
			EnableAbove = 0,
			EnableBelow = 1
		}

		[global::System.Serializable]
		public struct Entry
		{
			public global::UnityEngine.Behaviour[] Components;

			public global::UnityEngine.GameObject[] GameObjects;

			public global::SRF.UI.ResponsiveEnable.Modes Mode;

			public float ThresholdHeight;

			public float ThresholdWidth;
		}

		public global::SRF.UI.ResponsiveEnable.Entry[] Entries = new global::SRF.UI.ResponsiveEnable.Entry[0];

		protected override void Refresh()
		{
			global::UnityEngine.Rect rect = base.RectTransform.rect;
			for (int i = 0; i < Entries.Length; i++)
			{
				global::SRF.UI.ResponsiveEnable.Entry entry = Entries[i];
				bool flag = true;
				switch (entry.Mode)
				{
				case global::SRF.UI.ResponsiveEnable.Modes.EnableAbove:
					if (entry.ThresholdHeight > 0f)
					{
						flag = rect.height >= entry.ThresholdHeight && flag;
					}
					if (entry.ThresholdWidth > 0f)
					{
						flag = rect.width >= entry.ThresholdWidth && flag;
					}
					break;
				case global::SRF.UI.ResponsiveEnable.Modes.EnableBelow:
					if (entry.ThresholdHeight > 0f)
					{
						flag = rect.height <= entry.ThresholdHeight && flag;
					}
					if (entry.ThresholdWidth > 0f)
					{
						flag = rect.width <= entry.ThresholdWidth && flag;
					}
					break;
				default:
					throw new global::System.IndexOutOfRangeException();
				}
				if (entry.GameObjects != null)
				{
					for (int j = 0; j < entry.GameObjects.Length; j++)
					{
						global::UnityEngine.GameObject gameObject = entry.GameObjects[j];
						if (gameObject != null)
						{
							gameObject.SetActive(flag);
						}
					}
				}
				if (entry.Components == null)
				{
					continue;
				}
				for (int k = 0; k < entry.Components.Length; k++)
				{
					global::UnityEngine.Behaviour behaviour = entry.Components[k];
					if (behaviour != null)
					{
						behaviour.enabled = flag;
					}
				}
			}
		}
	}
}
