public class TopBarUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Top Panel")]
	private global::UnityEngine.GameObject topBarParent;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI fazTokenText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI nextLevelText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI currentLevelText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Slider levelProgressSlider;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI partsNumText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image playerAvatarImage;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Event Currency")]
	private global::UnityEngine.UI.Image EventCurrencyIconImage;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject EventCurrencyContainer;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI eventCurrencyNumText;

	private MasterDomain _masterDomain;

	private TopBarDisplayHandler _topBarDisplayHandler;

	private TopBarDisplayHandler.TopBarDisplayData _topBarDisplayData;

	private void BuildData()
	{
		TopBarDisplayHandler.TopBarDisplayData topBarDisplayData = new TopBarDisplayHandler.TopBarDisplayData();
		topBarDisplayData.masterDomain = _masterDomain;
		topBarDisplayData.fazTokenText = fazTokenText;
		topBarDisplayData.partsNumText = partsNumText;
		topBarDisplayData.eventCurrencyNumText = eventCurrencyNumText;
		topBarDisplayData.topBarParent = topBarParent;
		topBarDisplayData.gameAssetManagementDomain = _masterDomain.GameAssetManagementDomain;
		topBarDisplayData.playerAvatarImage = playerAvatarImage;
		topBarDisplayData.avatarIconLookup = _masterDomain.PlayerAvatarDomain.AvatarIconHandler;
		topBarDisplayData.eventCurrencyIconImage = EventCurrencyIconImage;
		topBarDisplayData.eventCurrencyContainer = EventCurrencyContainer;
		topBarDisplayData.currentLevelText = currentLevelText;
		topBarDisplayData.nextLevelText = nextLevelText;
		topBarDisplayData.levelProgressSlider = levelProgressSlider;
		_topBarDisplayData = topBarDisplayData;
	}

	private void Awake()
	{
		_masterDomain = MasterDomain.GetDomain();
		BuildData();
		_topBarDisplayHandler = new TopBarDisplayHandler(_topBarDisplayData);
	}

	private void Update()
	{
		if (_masterDomain != null && _masterDomain.TheGameDomain != null && !(global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LoadingScene"))
		{
			_topBarDisplayHandler.Update();
		}
	}

	private void OnDestroy()
	{
		_topBarDisplayHandler.OnDestroy();
	}
}
