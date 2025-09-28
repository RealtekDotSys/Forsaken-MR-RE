public class EncounterSceneCommunicator : global::UnityEngine.MonoBehaviour
{
	private void Start()
	{
		MasterDomain.GetDomain().TheGameDomain.OnGameDisplayDidChange();
	}
}
