namespace SRDebugger.UI.Controls
{
	public abstract class DataBoundControl : global::SRDebugger.UI.Controls.OptionsControlBase
	{
		private bool _hasStarted;

		private bool _isReadOnly;

		private object _prevValue;

		private global::SRF.Helpers.PropertyReference _prop;

		public global::SRF.Helpers.PropertyReference Property => _prop;

		public bool IsReadOnly => _isReadOnly;

		public string PropertyName { get; private set; }

		public void Bind(string propertyName, global::SRF.Helpers.PropertyReference prop)
		{
			PropertyName = propertyName;
			_prop = prop;
			_isReadOnly = !prop.CanWrite;
			OnBind(propertyName, prop.PropertyType);
			Refresh();
		}

		protected void UpdateValue(object newValue)
		{
			if (newValue != _prevValue && !IsReadOnly)
			{
				_prop.SetValue(newValue);
				_prevValue = newValue;
			}
		}

		public override void Refresh()
		{
			if (_prop == null)
			{
				return;
			}
			object value = _prop.GetValue();
			if (value != _prevValue)
			{
				try
				{
					OnValueUpdated(value);
				}
				catch (global::System.Exception exception)
				{
					global::UnityEngine.Debug.LogError("[SROptions] Error refreshing binding.");
					global::UnityEngine.Debug.LogException(exception);
				}
			}
			_prevValue = value;
		}

		protected virtual void OnBind(string propertyName, global::System.Type t)
		{
		}

		protected abstract void OnValueUpdated(object newValue);

		public abstract bool CanBind(global::System.Type type, bool isReadOnly);

		protected override void Start()
		{
			base.Start();
			Refresh();
			_hasStarted = true;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (_hasStarted)
			{
				Refresh();
			}
		}
	}
}
