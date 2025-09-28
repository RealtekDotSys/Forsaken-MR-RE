public class ScavengingSceneCommunicator : global::UnityEngine.MonoBehaviour
{
	private void Start()
	{
		MasterDomain.GetDomain().TheGameDomain.OnGameDisplayDidChangeScavenging();
	}
}
