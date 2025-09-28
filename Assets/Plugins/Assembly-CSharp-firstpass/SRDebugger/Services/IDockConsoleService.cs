namespace SRDebugger.Services
{
	public interface IDockConsoleService
	{
		bool IsVisible { get; set; }

		bool IsExpanded { get; set; }

		global::SRDebugger.ConsoleAlignment Alignment { get; set; }
	}
}
