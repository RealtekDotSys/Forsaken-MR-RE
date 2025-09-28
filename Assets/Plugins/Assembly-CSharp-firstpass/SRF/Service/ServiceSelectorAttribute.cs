namespace SRF.Service
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Method)]
	public sealed class ServiceSelectorAttribute : global::System.Attribute
	{
		public global::System.Type ServiceType { get; private set; }

		public ServiceSelectorAttribute(global::System.Type serviceType)
		{
			ServiceType = serviceType;
		}
	}
}
