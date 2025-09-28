public class CustomNightUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Custom Night Controller")]
	[global::UnityEngine.SerializeField]
	private CustomNightController controller;

	[global::UnityEngine.Header("Begin Button")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button beginButton;

	[global::UnityEngine.Header("Animatronic Value Selector Cell Prefab")]
	[global::UnityEngine.SerializeField]
	private CustomNightCell customNightCell;

	[global::UnityEngine.Header("UI Hookups")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform cellParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject selectorCanvas;

	private MasterDataConnector masterDataConnector;

	public global::System.Collections.Generic.List<CustomNightAnimatronic> customNightAnimatronics;

	private void Start()
	{
		customNightAnimatronics = new global::System.Collections.Generic.List<CustomNightAnimatronic>();
		beginButton.onClick.AddListener(BeginCustomNight);
		selectorCanvas.SetActive(value: true);
		CreateSelectorCells();
	}

	private void BeginCustomNight()
	{
		selectorCanvas.SetActive(value: false);
		controller.Begin(customNightAnimatronics);
	}

	public void CreateSelectorCells()
	{
		CustomNightAnimatronic customNightAnimatronic = new CustomNightAnimatronic
		{
			Id = "Freddy",
			Bundle = "animatronics_freddy",
			Prefab = "FreddyPrefab",
			PortraitImageName = "alpine_ui_portrait_freddy",
			InitialAIValue = 0,
			originRoom = CustomNightAnimatronic.CustomNightRooms.Stage,
			EastPath = true,
			ThreeAMValueBoost = 1,
			pushbackRoom = CustomNightAnimatronic.CustomNightRooms.EastHall
		};
		customNightAnimatronics.Add(customNightAnimatronic);
		global::UnityEngine.Object.Instantiate(customNightCell, cellParent).SetData(customNightAnimatronic);
		CustomNightAnimatronic customNightAnimatronic2 = new CustomNightAnimatronic
		{
			Id = "Bonnie",
			Bundle = "animatronics_bonnie",
			Prefab = "BonniePrefab",
			PortraitImageName = "alpine_ui_portrait_bonnie",
			InitialAIValue = 0,
			originRoom = CustomNightAnimatronic.CustomNightRooms.Stage,
			EastPath = false,
			ThreeAMValueBoost = 2,
			pushbackRoom = CustomNightAnimatronic.CustomNightRooms.DiningAreaWest
		};
		customNightAnimatronics.Add(customNightAnimatronic2);
		global::UnityEngine.Object.Instantiate(customNightCell, cellParent).SetData(customNightAnimatronic2);
		CustomNightAnimatronic customNightAnimatronic3 = new CustomNightAnimatronic
		{
			Id = "Chica",
			Bundle = "animatronics_bareendo",
			Prefab = "BareEndoPrefab",
			PortraitImageName = "alpine_ui_portrait_bareendo",
			InitialAIValue = 0,
			originRoom = CustomNightAnimatronic.CustomNightRooms.Stage,
			EastPath = true,
			ThreeAMValueBoost = 2,
			pushbackRoom = CustomNightAnimatronic.CustomNightRooms.DiningAreaEast
		};
		customNightAnimatronics.Add(customNightAnimatronic3);
		global::UnityEngine.Object.Instantiate(customNightCell, cellParent).SetData(customNightAnimatronic3);
		CustomNightAnimatronic customNightAnimatronic4 = new CustomNightAnimatronic
		{
			Id = "Foxy",
			Bundle = "animatronics_springtrap",
			Prefab = "SpringtrapPrefab",
			PortraitImageName = "alpine_ui_portrait_springtrap",
			InitialAIValue = 0,
			originRoom = CustomNightAnimatronic.CustomNightRooms.PirateCove,
			EastPath = false,
			ThreeAMValueBoost = 4,
			CanGoCloset = false,
			CanGoBackstage = false,
			pushbackRoom = CustomNightAnimatronic.CustomNightRooms.PirateCove
		};
		customNightAnimatronics.Add(customNightAnimatronic4);
		global::UnityEngine.Object.Instantiate(customNightCell, cellParent).SetData(customNightAnimatronic4);
	}
}
