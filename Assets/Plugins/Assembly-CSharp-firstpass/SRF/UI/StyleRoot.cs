namespace SRF.UI
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Style Root")]
	public sealed class StyleRoot : global::SRF.SRMonoBehaviour
	{
		private global::SRF.UI.StyleSheet _activeStyleSheet;

		public global::SRF.UI.StyleSheet StyleSheet;

		public global::SRF.UI.Style GetStyle(string key)
		{
			if (StyleSheet == null)
			{
				global::UnityEngine.Debug.LogWarning("[StyleRoot] StyleSheet is not set.", this);
				return null;
			}
			return StyleSheet.GetStyle(key);
		}

		private void OnEnable()
		{
			_activeStyleSheet = null;
			if (StyleSheet != null)
			{
				OnStyleSheetChanged();
			}
		}

		private void OnDisable()
		{
			OnStyleSheetChanged();
		}

		private void Update()
		{
			if (_activeStyleSheet != StyleSheet)
			{
				OnStyleSheetChanged();
			}
		}

		private void OnStyleSheetChanged()
		{
			_activeStyleSheet = StyleSheet;
			BroadcastMessage("SRStyleDirty", global::UnityEngine.SendMessageOptions.DontRequireReceiver);
		}

		public void SetDirty()
		{
			_activeStyleSheet = null;
		}
	}
}
