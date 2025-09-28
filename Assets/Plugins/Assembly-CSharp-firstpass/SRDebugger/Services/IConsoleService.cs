namespace SRDebugger.Services
{
	public interface IConsoleService
	{
		int ErrorCount { get; }

		int WarningCount { get; }

		int InfoCount { get; }

		global::SRDebugger.IReadOnlyList<global::SRDebugger.Services.ConsoleEntry> Entries { get; }

		global::SRDebugger.IReadOnlyList<global::SRDebugger.Services.ConsoleEntry> AllEntries { get; }

		event global::SRDebugger.Services.ConsoleUpdatedEventHandler Updated;

		void Clear();
	}
}
