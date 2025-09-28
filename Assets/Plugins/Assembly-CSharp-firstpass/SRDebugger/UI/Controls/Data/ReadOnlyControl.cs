namespace SRDebugger.UI.Controls.Data
{
	public class ReadOnlyControl : global::SRDebugger.UI.Controls.DataBoundControl
	{
		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text ValueText;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Title;

		protected override void Start()
		{
			base.Start();
		}

		protected override void OnBind(string propertyName, global::System.Type t)
		{
			base.OnBind(propertyName, t);
			Title.text = propertyName;
		}

		protected override void OnValueUpdated(object newValue)
		{
			ValueText.text = global::System.Convert.ToString(newValue);
		}

		public override bool CanBind(global::System.Type type, bool isReadOnly)
		{
			return type == typeof(string) && isReadOnly;
		}
	}
}
