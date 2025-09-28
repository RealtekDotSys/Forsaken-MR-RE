public class ShutdownParameters
{
	public global::System.Func<MasterDomain> MasterDomainGetter;

	public global::System.Collections.Generic.List<global::System.Action> RegisteredTeardownCallbacks;
}
