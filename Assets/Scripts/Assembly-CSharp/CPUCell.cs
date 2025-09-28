public class CPUCell : global::UnityEngine.MonoBehaviour
{
	public class CPUCellData
	{
		public bool playerOwned;

		public CPUData serverData;

		public global::System.Action<CPUCell, bool> SelectFunction;

		public bool isValid;
	}

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI userNameText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject highlightObject;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image image;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button button;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject unavailableObject;

	public CPUCell.CPUCellData cpuCellData;

	public void SetData(CPUCell.CPUCellData data)
	{
		cpuCellData = data;
		if (cpuCellData.SelectFunction != null)
		{
			button.onClick.AddListener(SetDatab__7_0);
		}
		if (button != null)
		{
			button.interactable = data.isValid && data.playerOwned;
		}
		userNameText.gameObject.SetActive(cpuCellData.playerOwned);
		userNameText.text = MasterDomain.GetDomain().LocalizationDomain.Localization.GetLocalizedString(data.serverData.AnimatronicName, data.serverData.AnimatronicName);
		unavailableObject.SetActive(!data.isValid);
	}

	public string GetIconName()
	{
		global::UnityEngine.Sprite overrideSprite = image.overrideSprite;
		if (overrideSprite == null)
		{
			return "";
		}
		return overrideSprite.name;
	}

	public void SetIcon(global::UnityEngine.Sprite icon)
	{
		if (!(image.overrideSprite == icon))
		{
			image.overrideSprite = icon;
		}
	}

	internal void SetSelected(bool value)
	{
		if (!(highlightObject == null))
		{
			highlightObject.SetActive(value);
		}
	}

	private void SetDatab__7_0()
	{
		cpuCellData.SelectFunction(this, arg2: false);
	}
}
