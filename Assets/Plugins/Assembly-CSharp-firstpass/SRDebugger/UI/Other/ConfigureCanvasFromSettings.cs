namespace SRDebugger.UI.Other
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Canvas))]
	public class ConfigureCanvasFromSettings : global::SRF.SRMonoBehaviour
	{
		private global::UnityEngine.Canvas _canvas;

		private global::UnityEngine.UI.CanvasScaler _canvasScaler;

		private float _originalScale;

		private float _lastSetScale;

		private void Start()
		{
			_canvas = GetComponent<global::UnityEngine.Canvas>();
			_canvasScaler = GetComponent<global::UnityEngine.UI.CanvasScaler>();
			global::SRDebugger.Internal.SRDebuggerUtil.ConfigureCanvas(_canvas);
			_originalScale = _canvasScaler.scaleFactor;
			_canvasScaler.scaleFactor = _originalScale * SRDebug.Instance.Settings.UIScale;
			_lastSetScale = _canvasScaler.scaleFactor;
			SRDebug.Instance.Settings.PropertyChanged += SettingsOnPropertyChanged;
		}

		private void SettingsOnPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (_canvasScaler.scaleFactor != _lastSetScale)
			{
				_originalScale = _canvasScaler.scaleFactor;
			}
			_canvasScaler.scaleFactor = _originalScale * SRDebug.Instance.Settings.UIScale;
			_lastSetScale = _canvasScaler.scaleFactor;
		}
	}
}
