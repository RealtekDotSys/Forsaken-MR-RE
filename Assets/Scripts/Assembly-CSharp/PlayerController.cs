public class PlayerController : global::UnityEngine.MonoBehaviour
{
	public static PlayerController Instance;

	public int _playerSpeed = 3;

	public int activeLockerIndex;

	public bool InLocker;

	private bool canMove;

	private global::UnityEngine.CharacterController characterController;

	private global::UnityEngine.Vector3 cachedPos;

	private PlayerUIManager UIManager;

	private void Start()
	{
		Instance = this;
		characterController = GetComponent<global::UnityEngine.CharacterController>();
		UIManager = GetComponent<PlayerUIManager>();
		InLocker = false;
		canMove = true;
	}

	public void MovementUpdate(float horizontal, float vertical)
	{
		if (canMove)
		{
			float num = horizontal;
			float axis = global::UnityEngine.Input.GetAxis("Horizontal");
			if (axis != 0f)
			{
				num = axis;
			}
			float num2 = vertical;
			float axis2 = global::UnityEngine.Input.GetAxis("Vertical");
			if (axis2 != 0f)
			{
				num2 = axis2;
			}
			global::UnityEngine.Vector3 vector = base.transform.right * num + base.transform.forward * num2;
			vector.y = 0f;
			characterController.Move(vector * _playerSpeed * global::UnityEngine.Time.deltaTime);
		}
	}

	public void EnterLocker(int idx, global::UnityEngine.Vector3 lockerPos)
	{
		activeLockerIndex = idx;
		InLocker = true;
		canMove = false;
		cachedPos = base.transform.position;
		base.transform.position = new global::UnityEngine.Vector3(lockerPos.x, 0f, lockerPos.z);
		UIManager.EnterLocker();
	}

	public void ExitLocker()
	{
		InLocker = false;
		canMove = true;
		base.transform.position = cachedPos;
		UIManager.ExitLocker();
	}

	public void Jumpscare()
	{
		canMove = false;
	}
}
