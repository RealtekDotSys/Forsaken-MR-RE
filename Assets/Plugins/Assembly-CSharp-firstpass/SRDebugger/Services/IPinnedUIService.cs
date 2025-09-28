namespace SRDebugger.Services
{
	public interface IPinnedUIService
	{
		bool IsProfilerPinned { get; set; }

		event global::System.Action<global::SRDebugger.Internal.OptionDefinition, bool> OptionPinStateChanged;

		void Pin(global::SRDebugger.Internal.OptionDefinition option, int order = -1);

		void Unpin(global::SRDebugger.Internal.OptionDefinition option);

		void UnpinAll();

		bool HasPinned(global::SRDebugger.Internal.OptionDefinition option);
	}
}
