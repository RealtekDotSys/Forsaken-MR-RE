public class WorkshopModifyStateUIActions : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private WorkshopModifyStateUIView workshopModifyStateUIView;

	[global::UnityEngine.SerializeField]
	private DialogHandler_WorkshopModify _dialogHandlerWorkshopModify;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button modCloseButton;

	private EventExposer _eventExposer;

	private void ConfirmSellSelectedMod(ModCell modCell)
	{
		workshopModifyStateUIView.SellMod(modCell.modContext);
	}

	private void ClearModifyTabs()
	{
		global::UnityEngine.Debug.Log("Cleared modify tabs");
		AssemblyButtonPressedPayload payload = new AssemblyButtonPressedPayload
		{
			ButtonType = SlotDisplayButtonType.None
		};
		_eventExposer.OnWorkshopModifyAssemblyButtonPressed(payload);
	}

	public void ConfirmSetEssenceValue()
	{
		workshopModifyStateUIView.SetEssenceValueFromSlider();
	}

	public void DisplaySellMod(ModCell modCell)
	{
		_dialogHandlerWorkshopModify.ShowSellModDialog(modCell, ConfirmSellSelectedMod);
	}

	public void ShowMoreInfoPlush()
	{
		_dialogHandlerWorkshopModify.ShowMoreInfoPlushDialog();
	}

	public void ShowMoreInfoCPU()
	{
		_dialogHandlerWorkshopModify.ShowMoreInfoCpuDialog();
	}

	public void ShowMoreInfoRemnant()
	{
		_dialogHandlerWorkshopModify.ShowMoreInfoRemnantDialog();
	}

	private void Awake()
	{
		MasterDomain domain = MasterDomain.GetDomain();
		if (domain != null)
		{
			_eventExposer = domain.eventExposer;
		}
		modCloseButton.onClick.RemoveListener(ClearModifyTabs);
		modCloseButton.onClick.AddListener(ClearModifyTabs);
	}
}
