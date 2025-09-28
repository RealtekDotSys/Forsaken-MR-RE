public class LockerButton : global::UnityEngine.MonoBehaviour
{
	private global::UnityEngine.Transform playerTransform;

	public global::UnityEngine.UI.Button button;

	private bool playerInLocker;

	private bool lockerInUse;

	private global::UnityEngine.Transform canvasTransform;

	public global::UnityEngine.Canvas lockerCanvas;

	public LockerController lockerController;

	public float InteractDistance = 3f;

	private void Start()
	{
		lockerInUse = false;
		lockerCanvas.worldCamera = global::UnityEngine.GameObject.FindGameObjectWithTag("MainCamera").GetComponent<global::UnityEngine.Camera>();
		canvasTransform = lockerCanvas.transform;
	}

	private void Update()
	{
		canvasTransform.LookAt(playerTransform);
		playerInLocker = PlayerController.Instance.InLocker;
		playerTransform = PlayerController.Instance.transform;
		if (lockerInUse || playerInLocker || global::UnityEngine.Vector3.Distance(PlayerController.Instance.transform.position, base.transform.position) > InteractDistance)
		{
			base.transform.localScale = new global::UnityEngine.Vector3(0f, 0f, 0f);
			button.interactable = false;
		}
		else
		{
			base.transform.localScale = new global::UnityEngine.Vector3(0.5f, 0.5f, 0.5f);
			button.interactable = true;
		}
	}

	public void ButtonPressed()
	{
		global::UnityEngine.Debug.Log("ButtonPressed!");
		lockerInUse = true;
		lockerController.EnterLocker();
	}

	public void ExitLocker()
	{
		lockerInUse = false;
	}
}
