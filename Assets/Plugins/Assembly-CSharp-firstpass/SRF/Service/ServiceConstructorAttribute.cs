namespace SRF.Service
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Method)]
	public sealed class ServiceConstructorAttribute : global::System.Attribute
	{
		public global::System.Type ServiceType { get; private set; }

		public ServiceConstructorAttribute(global::System.Type serviceType)
		{
			ServiceType = serviceType;
		}
	}
}
