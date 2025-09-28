namespace SRDebugger.UI.Controls.Data
{
	public class ActionControl : global::SRDebugger.UI.Controls.OptionsControlBase
	{
		private global::SRF.Helpers.MethodReference _method;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Button Button;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Title;

		public global::SRF.Helpers.MethodReference Method => _method;

		protected override void Start()
		{
			base.Start();
			Button.onClick.AddListener(ButtonOnClick);
		}

		private void ButtonOnClick()
		{
			if (_method == null)
			{
				global::UnityEngine.Debug.LogWarning("[SRDebugger.Options] No method set for action control", this);
				return;
			}
			try
			{
				_method.Invoke(null);
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogError("[SRDebugger] Exception thrown while executing action.");
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		public void SetMethod(string methodName, global::SRF.Helpers.MethodReference method)
		{
			_method = method;
			Title.text = methodName;
		}
	}
}
