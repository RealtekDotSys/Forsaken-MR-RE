namespace SRF.UI
{
	[global::System.Serializable]
	public class StyleSheet : global::UnityEngine.ScriptableObject
	{
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<string> _keys = new global::System.Collections.Generic.List<string>();

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::SRF.UI.Style> _styles = new global::System.Collections.Generic.List<global::SRF.UI.Style>();

		[global::UnityEngine.SerializeField]
		public global::SRF.UI.StyleSheet Parent;

		public global::SRF.UI.Style GetStyle(string key, bool searchParent = true)
		{
			int num = _keys.IndexOf(key);
			if (num < 0)
			{
				if (searchParent && Parent != null)
				{
					return Parent.GetStyle(key);
				}
				return null;
			}
			return _styles[num];
		}
	}
}
