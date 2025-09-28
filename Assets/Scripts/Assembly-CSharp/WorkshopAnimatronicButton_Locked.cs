public class WorkshopAnimatronicButton_Locked : global::UnityEngine.MonoBehaviour, IWorkshopSlotButton
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public global::System.Action<WorkshopAnimatronicButton_Locked> buttonCallback;

		public WorkshopAnimatronicButton_Locked _003C_003E4__this;

		internal void _003CInitialize_003Eb__0()
		{
			global::System.Action<WorkshopAnimatronicButton_Locked> action = buttonCallback;
			_ = _003C_003E4__this;
			action?.Invoke(_003C_003E4__this);
		}
	}

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Local Hookups")]
	private global::UnityEngine.GameObject selectedStateImage;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI streakUnlockNumText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI fazTokenPriceText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button thisButton;

	private int _displayNumber;

	[global::System.Runtime.CompilerServices.SpecialName]
	global::UnityEngine.GameObject IWorkshopSlotButton.gameObject => base.gameObject;

	public void Initialize(int displayNumber, global::System.Action<WorkshopAnimatronicButton_Locked> buttonCallback)
	{
		WorkshopAnimatronicButton_Locked._003C_003Ec__DisplayClass6_0 _003C_003Ec__DisplayClass6_ = new WorkshopAnimatronicButton_Locked._003C_003Ec__DisplayClass6_0();
		_003C_003Ec__DisplayClass6_.buttonCallback = buttonCallback;
		_003C_003Ec__DisplayClass6_._003C_003E4__this = this;
		_displayNumber = displayNumber;
		string dataForStreak = displayNumber.ToString();
		SetDataForStreak(dataForStreak);
		thisButton.onClick.AddListener(_003C_003Ec__DisplayClass6_._003CInitialize_003Eb__0);
	}

	public void SetDataForStreak(string streak)
	{
		streakUnlockNumText.transform.parent.gameObject.SetActive(value: true);
		streakUnlockNumText.text = streak;
		fazTokenPriceText.transform.parent.gameObject.SetActive(value: false);
	}

	public void SetDataForPurchase(string price)
	{
		streakUnlockNumText.transform.parent.gameObject.SetActive(value: false);
		fazTokenPriceText.transform.parent.gameObject.SetActive(value: true);
		fazTokenPriceText.text = price;
	}

	public void SetSelectedUI(bool value)
	{
		selectedStateImage.SetActive(value);
	}

	public int GetSortWeight()
	{
		return 10;
	}

	public WorkshopSlotData GetWorkshopSlotData()
	{
		return null;
	}

	public bool IsReadyToCollect()
	{
		return false;
	}

	public void OverrideSlotDataState(WorkshopEntry.Status status)
	{
	}

	public void TearDown()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
