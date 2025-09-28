public class LobbyController : global::UnityEngine.MonoBehaviour
{
	public static LobbyController Instance;

	public bool canRotate;

	public bool canMove;

	public bool possibleMove;

	public bool rotDisabled;

	public bool posDisabled;

	public string CurrentPos;

	public string CurrentTargetPos;

	public int CurrentTargetDir;

	public int CurrentDirection;

	public global::UnityEngine.Transform CamTransform;

	public global::UnityEngine.Transform MainPos;

	public global::UnityEngine.Transform StorePos;

	public global::UnityEngine.Transform WorkshopPos;

	public global::UnityEngine.Transform PhonePos;

	public global::UnityEngine.UI.Button FrontButton;

	public global::UnityEngine.UI.Button RightButton;

	public global::UnityEngine.UI.Button LeftButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject WorkshopUI;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject StoreUI;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject TrophyUI;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject CorkboardUI;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject ScavengingSelectUI;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TMP_InputField usernameField;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TMP_InputField friendCodeField;

	private global::UnityEngine.Animator lobbyCamAnimator;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		lobbyCamAnimator = CamTransform.GetComponent<global::UnityEngine.Animator>();
		CurrentPos = "Main";
		CurrentTargetPos = "Main";
		CurrentTargetDir = 180;
		CurrentDirection = 180;
		rotDisabled = false;
		posDisabled = false;
		FrontButton.interactable = true;
		RightButton.interactable = true;
		LeftButton.interactable = true;
		CamTransform.position = MainPos.position;
		lobbyCamAnimator.SetInteger("Rotation", CurrentTargetDir);
	}

	private void Update()
	{
		if (usernameField.isFocused || friendCodeField.isFocused)
		{
			canRotate = false;
			canMove = false;
		}
		if (canRotate)
		{
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.A))
			{
				RotateCamera("left");
			}
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.D))
			{
				RotateCamera("right");
			}
			EnableButtons("Rotation");
		}
		else
		{
			DisableButtons("Rotation");
		}
		if (canMove && possibleMove)
		{
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.W))
			{
				MoveCamera();
			}
			EnableButtons("Position");
		}
		else
		{
			DisableButtons("Position");
		}
		if (CurrentDirection != CurrentTargetDir)
		{
			canRotate = false;
			Rotate();
		}
		else
		{
			canRotate = true;
		}
		if (CurrentPos != CurrentTargetPos)
		{
			canMove = false;
			Move();
		}
		else
		{
			canMove = true;
		}
		CheckPossibleMoves();
		if (CurrentDirection == 270 && CurrentPos == "Workshop")
		{
			WorkshopUI.SetActive(value: true);
		}
		else
		{
			WorkshopUI.SetActive(value: false);
		}
		if (CurrentDirection == 90 && CurrentPos == "Store")
		{
			StoreUI.SetActive(value: true);
		}
		else
		{
			StoreUI.SetActive(value: false);
		}
		if (CurrentDirection == 90 && CurrentPos == "Phone")
		{
			TrophyUI.SetActive(value: true);
		}
		else
		{
			TrophyUI.SetActive(value: false);
		}
		if (CurrentDirection == 0 && CurrentPos == "Workshop")
		{
			CorkboardUI.SetActive(value: true);
		}
		else
		{
			CorkboardUI.SetActive(value: false);
		}
		if (CurrentDirection == 0 && CurrentPos == "Main")
		{
			ScavengingSelectUI.SetActive(value: true);
		}
		else
		{
			ScavengingSelectUI.SetActive(value: false);
		}
	}

	public void RotateCamera(string direction)
	{
		if (MasterDomain.GetDomain().DialogDomain._dialogShower.IsShowingDialog() || global::PaperPlaneTools.AlertManager.Instance.IsShowingAlert())
		{
			return;
		}
		if (!(direction == "left"))
		{
			if (direction == "right")
			{
				switch (CurrentDirection)
				{
				case 0:
					CurrentTargetDir = 90;
					break;
				case 90:
					CurrentTargetDir = 180;
					break;
				case 180:
					CurrentTargetDir = 270;
					break;
				case 270:
					CurrentTargetDir = 0;
					break;
				}
			}
		}
		else
		{
			switch (CurrentDirection)
			{
			case 0:
				CurrentTargetDir = 270;
				break;
			case 90:
				CurrentTargetDir = 0;
				break;
			case 180:
				CurrentTargetDir = 90;
				break;
			case 270:
				CurrentTargetDir = 180;
				break;
			}
		}
	}

	public void MoveCamera()
	{
		if (MasterDomain.GetDomain().DialogDomain._dialogShower.IsShowingDialog() || global::PaperPlaneTools.AlertManager.Instance.IsShowingAlert())
		{
			return;
		}
		switch (CurrentPos)
		{
		case "Main":
			switch (CurrentDirection)
			{
			case 90:
				CurrentTargetPos = "Store";
				break;
			case 270:
				CurrentTargetPos = "Workshop";
				break;
			}
			break;
		case "Store":
			if (CurrentDirection == 270)
			{
				CurrentTargetPos = "Main";
			}
			break;
		case "Workshop":
			switch (CurrentDirection)
			{
			case 90:
				CurrentTargetPos = "Main";
				break;
			case 180:
				CurrentTargetPos = "Phone";
				break;
			}
			break;
		case "Phone":
			if (CurrentDirection == 0)
			{
				CurrentTargetPos = "Workshop";
			}
			break;
		}
	}

	private void Rotate()
	{
		lobbyCamAnimator.SetInteger("Rotation", CurrentTargetDir);
		CurrentDirection = CurrentTargetDir;
	}

	private void Move()
	{
		switch (CurrentTargetPos)
		{
		case "Main":
		{
			string currentPos = CurrentPos;
			if (!(currentPos == "Store"))
			{
				if (currentPos == "Workshop")
				{
					CamTransform.position = MainPos.position;
					CurrentPos = CurrentTargetPos;
				}
			}
			else
			{
				CamTransform.position = MainPos.position;
				CurrentPos = CurrentTargetPos;
			}
			break;
		}
		case "Store":
			CamTransform.position = StorePos.position;
			CurrentPos = CurrentTargetPos;
			break;
		case "Workshop":
		{
			string currentPos = CurrentPos;
			if (!(currentPos == "Main"))
			{
				if (currentPos == "Phone")
				{
					CamTransform.position = WorkshopPos.position;
					CurrentPos = CurrentTargetPos;
				}
			}
			else
			{
				CamTransform.position = WorkshopPos.position;
				CurrentPos = CurrentTargetPos;
			}
			break;
		}
		case "Phone":
			CamTransform.position = PhonePos.position;
			CurrentPos = CurrentTargetPos;
			break;
		}
	}

	private void DisableButtons(string type)
	{
		if (!(type == "Rotation"))
		{
			if (type == "Position")
			{
				FrontButton.interactable = false;
				posDisabled = true;
			}
		}
		else
		{
			RightButton.interactable = false;
			LeftButton.interactable = false;
			rotDisabled = true;
		}
	}

	private void EnableButtons(string type)
	{
		if (!(type == "Rotation"))
		{
			if (type == "Position")
			{
				FrontButton.interactable = true;
				posDisabled = false;
			}
		}
		else
		{
			RightButton.interactable = true;
			LeftButton.interactable = true;
			rotDisabled = false;
		}
	}

	private void CheckPossibleMoves()
	{
		if (CurrentPos == "Main")
		{
			switch (CurrentDirection)
			{
			case 90:
				possibleMove = true;
				break;
			case 270:
				possibleMove = true;
				break;
			default:
				possibleMove = false;
				break;
			}
		}
		if (CurrentPos == "Workshop")
		{
			switch (CurrentDirection)
			{
			case 90:
				possibleMove = true;
				break;
			case 180:
				possibleMove = true;
				break;
			default:
				possibleMove = false;
				break;
			}
		}
		if (CurrentPos == "Phone")
		{
			if (CurrentDirection == 0)
			{
				possibleMove = true;
			}
			else
			{
				possibleMove = false;
			}
		}
		if (CurrentPos == "Store")
		{
			if (CurrentDirection == 270)
			{
				possibleMove = true;
			}
			else
			{
				possibleMove = false;
			}
		}
	}
}
