public class LockerController : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform buttonTransform;

	[global::UnityEngine.SerializeField]
	private LockerButton lockerButton;

	private int lockerIndex = -1;

	private global::UnityEngine.Animator lockerAnimator;

	private void Start()
	{
		lockerAnimator = GetComponent<global::UnityEngine.Animator>();
		lockerAnimator.SetBool("IsClosed", value: false);
	}

	public void SetIndex(int index)
	{
		lockerIndex = index;
	}

	public void PlayJumpscareAnimation()
	{
		lockerAnimator.SetTrigger("Jumpscare");
		buttonTransform.gameObject.SetActive(value: false);
	}

	public void EnterLocker()
	{
		PlayerController.Instance.EnterLocker(lockerIndex, base.transform.position);
		lockerAnimator.SetBool("IsClosed", value: true);
	}

	public void ExitLocker()
	{
		lockerAnimator.SetBool("IsClosed", value: false);
		lockerButton.ExitLocker();
	}
}
