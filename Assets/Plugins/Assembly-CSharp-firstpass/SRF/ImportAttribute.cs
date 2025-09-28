namespace SRF
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Field)]
	public class ImportAttribute : global::System.Attribute
	{
		public readonly global::System.Type Service;

		public ImportAttribute()
		{
		}

		public ImportAttribute(global::System.Type serviceType)
		{
			Service = serviceType;
		}
	}
}
