namespace SRDebugger.UI.Controls.Data
{
	public class EnumControl : global::SRDebugger.UI.Controls.DataBoundControl
	{
		private object _lastValue;

		private string[] _names;

		private global::System.Array _values;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.LayoutElement ContentLayoutElement;

		public global::UnityEngine.GameObject[] DisableOnReadOnly;

		[global::SRF.RequiredField]
		public global::SRF.UI.SRSpinner Spinner;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Title;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Value;

		protected override void Start()
		{
			base.Start();
		}

		protected override void OnBind(string propertyName, global::System.Type t)
		{
			base.OnBind(propertyName, t);
			Title.text = propertyName;
			Spinner.interactable = !base.IsReadOnly;
			if (DisableOnReadOnly != null)
			{
				global::UnityEngine.GameObject[] disableOnReadOnly = DisableOnReadOnly;
				for (int i = 0; i < disableOnReadOnly.Length; i++)
				{
					disableOnReadOnly[i].SetActive(!base.IsReadOnly);
				}
			}
			_names = global::System.Enum.GetNames(t);
			_values = global::System.Enum.GetValues(t);
			string text = "";
			for (int j = 0; j < _names.Length; j++)
			{
				if (_names[j].Length > text.Length)
				{
					text = _names[j];
				}
			}
			if (_names.Length != 0)
			{
				float preferredWidth = Value.cachedTextGeneratorForLayout.GetPreferredWidth(text, Value.GetGenerationSettings(new global::UnityEngine.Vector2(float.MaxValue, Value.preferredHeight)));
				ContentLayoutElement.preferredWidth = preferredWidth;
			}
		}

		protected override void OnValueUpdated(object newValue)
		{
			_lastValue = newValue;
			Value.text = newValue.ToString();
		}

		public override bool CanBind(global::System.Type type, bool isReadOnly)
		{
			return type.IsEnum;
		}

		private void SetIndex(int i)
		{
			UpdateValue(_values.GetValue(i));
			Refresh();
		}

		public void GoToNext()
		{
			int num = global::System.Array.IndexOf(_values, _lastValue);
			SetIndex(SRMath.Wrap(_values.Length, num + 1));
		}

		public void GoToPrevious()
		{
			int num = global::System.Array.IndexOf(_values, _lastValue);
			SetIndex(SRMath.Wrap(_values.Length, num - 1));
		}
	}
}
