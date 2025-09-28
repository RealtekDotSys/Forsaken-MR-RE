namespace SRF.Service
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	public sealed class ServiceAttribute : global::System.Attribute
	{
		public global::System.Type ServiceType { get; private set; }

		public ServiceAttribute(global::System.Type serviceType)
		{
			ServiceType = serviceType;
		}
	}
}
