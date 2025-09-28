public class CustomNightCell : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Animatronic Image")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image animatronicImage;

	[global::UnityEngine.Header("Number Changer")]
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI valueText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button leftButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button rightButton;

	private int currValue;

	private CustomNightAnimatronic animatronic;

	private void Start()
	{
		leftButton.onClick.AddListener(ValueDown);
		rightButton.onClick.AddListener(ValueUp);
		UpdateText();
	}

	private void UpdateText()
	{
		valueText.text = currValue.ToString();
		if (animatronic != null)
		{
			animatronic.InitialAIValue = currValue;
		}
	}

	public void SetData(CustomNightAnimatronic CNAnimatronic)
	{
		animatronic = CNAnimatronic;
		MasterDomain.GetDomain().GameAssetManagementDomain.IconLookupAccess.GetIcon(IconGroup.Portrait, CNAnimatronic.PortraitImageName, SetImage);
	}

	private void SetImage(global::UnityEngine.Sprite img)
	{
		animatronicImage.sprite = img;
	}

	private void ValueDown()
	{
		currValue = global::UnityEngine.Mathf.Max(0, currValue - 1);
		UpdateText();
	}

	private void ValueUp()
	{
		currValue = global::UnityEngine.Mathf.Min(20, currValue + 1);
		UpdateText();
	}
}
