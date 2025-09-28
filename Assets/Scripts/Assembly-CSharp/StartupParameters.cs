public class StartupParameters
{
	public global::System.Func<MasterDomain> MasterDomainGetter;

	public global::System.Collections.Generic.List<global::System.Action> RegisteredTeardownCallbacks;

	public bool HasToSBeenAcceptedYet;

	public global::UnityEngine.MonoBehaviour HostMonobehavior;

	public GameLifecycleProxy GameLifecycleProxy;

	public GameUnityHooks UnityHooks;
}
