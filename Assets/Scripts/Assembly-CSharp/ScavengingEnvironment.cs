public class ScavengingEnvironment : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform AnimatronicStartPosition;

	public global::UnityEngine.Transform[] wayPoints;

	public LockerController[] lockers;

	private void Start()
	{
		SetLockerIndexes();
	}

	private void SetLockerIndexes()
	{
		for (int i = 0; i < lockers.Length; i++)
		{
			lockers[i].SetIndex(i);
		}
	}
}
