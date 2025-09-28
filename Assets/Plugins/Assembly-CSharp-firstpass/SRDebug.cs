public static class SRDebug
{
	public const string Version = "1.7.1";

	public static global::SRDebugger.Services.IDebugService Instance => global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IDebugService>();

	public static void Init()
	{
		if (!global::SRF.Service.SRServiceManager.HasService<global::SRDebugger.Services.IConsoleService>())
		{
			new global::SRDebugger.Services.Implementation.StandardConsoleService();
		}
		global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IDebugService>();
	}
}
