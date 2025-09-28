namespace SRDebugger.UI.Other
{
	[global::UnityEngine.RequireComponent(typeof(global::SRF.UI.StyleComponent))]
	public class DebugPanelBackgroundBehaviour : global::SRF.SRMonoBehaviour
	{
		private string _defaultKey;

		private bool _isTransparent;

		private global::SRF.UI.StyleComponent _styleComponent;

		public string TransparentStyleKey = "";

		private void Awake()
		{
			_styleComponent = GetComponent<global::SRF.UI.StyleComponent>();
			_defaultKey = _styleComponent.StyleKey;
			Update();
		}

		private void Update()
		{
			if (!_isTransparent && global::SRDebugger.Settings.Instance.EnableBackgroundTransparency)
			{
				_styleComponent.StyleKey = TransparentStyleKey;
				_isTransparent = true;
			}
			else if (_isTransparent && !global::SRDebugger.Settings.Instance.EnableBackgroundTransparency)
			{
				_styleComponent.StyleKey = _defaultKey;
				_isTransparent = false;
			}
		}
	}
}
