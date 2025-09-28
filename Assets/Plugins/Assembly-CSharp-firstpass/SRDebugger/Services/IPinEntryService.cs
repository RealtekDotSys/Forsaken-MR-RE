namespace SRDebugger.Services
{
	public interface IPinEntryService
	{
		bool IsShowingKeypad { get; }

		void ShowPinEntry(global::System.Collections.Generic.IList<int> requiredPin, string message, global::SRDebugger.Services.PinEntryCompleteCallback callback, bool allowCancel = true);

		[global::System.Obsolete("blockInput param is deprecated (and ignored), please use overload without it.")]
		void ShowPinEntry(global::System.Collections.Generic.IList<int> requiredPin, string message, global::SRDebugger.Services.PinEntryCompleteCallback callback, bool blockInput, bool allowCancel);
	}
}
