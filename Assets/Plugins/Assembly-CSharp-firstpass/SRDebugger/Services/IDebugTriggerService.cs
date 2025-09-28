namespace SRDebugger.Services
{
	public interface IDebugTriggerService
	{
		bool IsEnabled { get; set; }

		global::SRDebugger.PinAlignment Position { get; set; }
	}
}
