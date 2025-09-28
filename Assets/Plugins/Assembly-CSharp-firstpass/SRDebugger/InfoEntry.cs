namespace SRDebugger
{
	public sealed class InfoEntry
	{
		private global::System.Func<object> _valueGetter;

		public string Title { get; set; }

		public object Value
		{
			get
			{
				try
				{
					return _valueGetter();
				}
				catch (global::System.Exception ex)
				{
					return global::SRF.SRFStringExtensions.Fmt("Error ({0})", ex.GetType().Name);
				}
			}
		}

		public bool IsPrivate { get; private set; }

		public static global::SRDebugger.InfoEntry Create(string name, global::System.Func<object> getter, bool isPrivate = false)
		{
			return new global::SRDebugger.InfoEntry
			{
				Title = name,
				_valueGetter = getter,
				IsPrivate = isPrivate
			};
		}

		public static global::SRDebugger.InfoEntry Create(string name, object value, bool isPrivate = false)
		{
			return new global::SRDebugger.InfoEntry
			{
				Title = name,
				_valueGetter = () => value,
				IsPrivate = isPrivate
			};
		}
	}
}
