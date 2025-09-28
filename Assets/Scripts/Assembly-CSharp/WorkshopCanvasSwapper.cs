public class WorkshopCanvasSwapper : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Buttons")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button AssembleButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button ModifyBackButton;

	[global::UnityEngine.Header("Canvases")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject WorkshopCanvas;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject WorkshopModifyCanvas;

	[global::UnityEngine.Header("SlotSaveHookup")]
	[global::UnityEngine.SerializeField]
	private WorkshopModifyStateUIView uiView;

	private void OnEnable()
	{
		AssembleButton.onClick.RemoveListener(AssembleButtonPressed);
		AssembleButton.onClick.AddListener(AssembleButtonPressed);
		ModifyBackButton.onClick.RemoveListener(ModifyBackButtonPressed);
		ModifyBackButton.onClick.AddListener(ModifyBackButtonPressed);
	}

	private void AssembleButtonPressed()
	{
		WorkshopCanvas.SetActive(value: false);
		WorkshopModifyCanvas.SetActive(value: true);
		uiView.SwitchedToTab();
	}

	private void ModifyBackButtonPressed()
	{
		WorkshopCanvas.SetActive(value: true);
		WorkshopModifyCanvas.SetActive(value: false);
		AssemblyButtonPressedPayload payload = new AssemblyButtonPressedPayload
		{
			ButtonType = SlotDisplayButtonType.None
		};
		MasterDomain.GetDomain().eventExposer.OnWorkshopModifyAssemblyButtonPressed(payload);
		uiView.TabSwitched();
	}
}
