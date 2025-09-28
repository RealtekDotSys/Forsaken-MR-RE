namespace SRDebugger.Services
{
	public interface ISystemInformationService
	{
		global::System.Collections.Generic.IEnumerable<string> GetCategories();

		global::System.Collections.Generic.IList<global::SRDebugger.InfoEntry> GetInfo(string category);

		void Add(global::SRDebugger.InfoEntry info, string category = "Default");

		global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<string, object>> CreateReport(bool includePrivate = false);
	}
}
