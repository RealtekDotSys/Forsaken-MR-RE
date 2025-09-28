namespace SRDebugger.UI.Other
{
	public class SRTab : global::SRF.SRMonoBehaviourEx
	{
		public global::UnityEngine.RectTransform HeaderExtraContent;

		[global::System.Obsolete]
		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.Sprite Icon;

		public global::UnityEngine.RectTransform IconExtraContent;

		public string IconStyleKey = "Icon_Stompy";

		public int SortIndex;

		[global::UnityEngine.HideInInspector]
		public global::SRDebugger.UI.Controls.SRTabButton TabButton;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("Title")]
		private string _title;

		[global::UnityEngine.SerializeField]
		private string _longTitle;

		[global::UnityEngine.SerializeField]
		private string _key;

		public string Title => _title;

		public string LongTitle
		{
			get
			{
				if (string.IsNullOrEmpty(_longTitle))
				{
					return _title;
				}
				return _longTitle;
			}
		}

		public string Key => _key;
	}
}
