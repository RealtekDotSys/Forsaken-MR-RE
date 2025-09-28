public class WorkshopSceneLookupTable : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _displayParent;

	[global::UnityEngine.SerializeField]
	private WorkshopStageView _workshopStage;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _audioListenerParent;

	public global::UnityEngine.GameObject DisplayParent => _displayParent;

	public WorkshopStageView WorkshopStage => _workshopStage;

	public global::UnityEngine.GameObject AudioListenerParent => _audioListenerParent;
}
