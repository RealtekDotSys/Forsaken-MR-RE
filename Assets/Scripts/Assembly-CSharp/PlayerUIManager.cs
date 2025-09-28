public class PlayerUIManager : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject LockerExitButton;

	public void EnterLocker()
	{
		LockerExitButton.SetActive(value: true);
	}

	public void ExitLocker()
	{
		LockerExitButton.SetActive(value: false);
	}
}
