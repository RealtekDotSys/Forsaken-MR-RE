public class SkinCell : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI userNameText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject highlightObject;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image image;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button button;

	public SkinCellData skinCellData;

	private void SelectCell()
	{
		if (skinCellData != null)
		{
			skinCellData.SelectFunction(this);
		}
	}

	private void UpdateButton()
	{
		button.onClick.RemoveListener(SelectCell);
		button.onClick.AddListener(SelectCell);
		button.interactable = skinCellData.PlayerOwned;
	}

	private void UpdateText()
	{
		string animatronicName = skinCellData.PlushSuitData.AnimatronicName;
		userNameText.gameObject.SetActive(skinCellData.PlayerOwned);
		userNameText.text = animatronicName;
	}

	public void SetData(SkinCellData cellData)
	{
		skinCellData = cellData;
		UpdateText();
		UpdateButton();
	}

	public void SetSprite(global::UnityEngine.Sprite sprite)
	{
		if (!(image.overrideSprite == sprite))
		{
			image.overrideSprite = sprite;
		}
	}

	public void SetSelected(bool v)
	{
		if (!(highlightObject == null))
		{
			highlightObject.SetActive(v);
		}
	}

	public void SetSelectable(bool value)
	{
		button.interactable = value;
	}
}
