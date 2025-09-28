namespace SRDebugger.UI.Other
{
	public class CategoryGroup : global::SRF.SRMonoBehaviourEx
	{
		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform Container;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Header;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject Background;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle SelectionToggle;

		public global::UnityEngine.GameObject[] EnabledDuringSelectionMode = new global::UnityEngine.GameObject[0];

		private bool _selectionModeEnabled = true;

		public bool IsSelected
		{
			get
			{
				return SelectionToggle.isOn;
			}
			set
			{
				SelectionToggle.isOn = value;
				if (SelectionToggle.graphic != null)
				{
					SelectionToggle.graphic.CrossFadeAlpha((!value) ? 0f : (_selectionModeEnabled ? 1f : 0.2f), 0f, ignoreTimeScale: true);
				}
			}
		}

		public bool SelectionModeEnabled
		{
			get
			{
				return _selectionModeEnabled;
			}
			set
			{
				if (value != _selectionModeEnabled)
				{
					_selectionModeEnabled = value;
					for (int i = 0; i < EnabledDuringSelectionMode.Length; i++)
					{
						EnabledDuringSelectionMode[i].SetActive(_selectionModeEnabled);
					}
				}
			}
		}
	}
}
