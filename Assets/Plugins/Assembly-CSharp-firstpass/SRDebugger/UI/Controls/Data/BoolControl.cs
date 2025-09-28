namespace SRDebugger.UI.Controls.Data
{
	public class BoolControl : global::SRDebugger.UI.Controls.DataBoundControl
	{
		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Title;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle Toggle;

		protected override void Start()
		{
			base.Start();
			Toggle.onValueChanged.AddListener(ToggleOnValueChanged);
		}

		private void ToggleOnValueChanged(bool isOn)
		{
			UpdateValue(isOn);
		}

		protected override void OnBind(string propertyName, global::System.Type t)
		{
			base.OnBind(propertyName, t);
			Title.text = propertyName;
			Toggle.interactable = !base.IsReadOnly;
		}

		protected override void OnValueUpdated(object newValue)
		{
			bool isOn = (bool)newValue;
			Toggle.isOn = isOn;
		}

		public override bool CanBind(global::System.Type type, bool isReadOnly)
		{
			return type == typeof(bool);
		}
	}
}
