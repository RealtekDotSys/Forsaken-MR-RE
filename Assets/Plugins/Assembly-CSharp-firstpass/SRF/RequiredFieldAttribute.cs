namespace SRF
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Field)]
	public sealed class RequiredFieldAttribute : global::System.Attribute
	{
		private bool _autoCreate;

		private bool _autoSearch;

		private bool _editorOnly = true;

		public bool AutoSearch
		{
			get
			{
				return _autoSearch;
			}
			set
			{
				_autoSearch = value;
			}
		}

		public bool AutoCreate
		{
			get
			{
				return _autoCreate;
			}
			set
			{
				_autoCreate = value;
			}
		}

		[global::System.Obsolete]
		public bool EditorOnly
		{
			get
			{
				return _editorOnly;
			}
			set
			{
				_editorOnly = value;
			}
		}

		public RequiredFieldAttribute(bool autoSearch)
		{
			AutoSearch = autoSearch;
		}

		public RequiredFieldAttribute()
		{
		}
	}
}
