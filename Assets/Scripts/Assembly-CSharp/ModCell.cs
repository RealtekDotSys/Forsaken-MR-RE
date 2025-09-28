public class ModCell : global::UnityEngine.MonoBehaviour, ICellInterface<ModSelectionCellData>
{
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI nameText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI descriptionText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI sellText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image image;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button modCell;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button equipModButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button unequipModButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button sellButton;

	[global::UnityEngine.SerializeField]
	private StarDisplay stars;

	[global::UnityEngine.SerializeField]
	private HighlightToggle highlightToggle;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject inUseParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject availableParent;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI quantityText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject descriptionPanel;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject categoryInUseDescription;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI categoryTextAvailable;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI categoryTextUnavailable;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI categoryTextEquipped;

	private ModSelectionCellData modCellPayload;

	public GatherModsForEquipping.ModContext modContext
	{
		get
		{
			if (modCellPayload != null)
			{
				return modCellPayload.context;
			}
			return null;
		}
	}

	private void SetText()
	{
		nameText.text = modCellPayload.context.Mod.Name;
		descriptionText.text = modCellPayload.context.Mod.Description;
		sellText.text = modCellPayload.context.Mod.PartsBuyback.ToString("n0");
		quantityText.text = modCellPayload.context.quantity.ToString("n0");
		categoryTextAvailable.text = modCellPayload.context.Mod.CategoryStringLocalized;
		categoryTextUnavailable.text = modCellPayload.context.Mod.CategoryStringLocalized;
		categoryTextEquipped.text = modCellPayload.context.Mod.CategoryStringLocalized;
	}

	private void SelectThisModCell()
	{
		if (modCellPayload != null)
		{
			modCellPayload.SelectModCell(this);
		}
	}

	private void DisplaySellThisDialog()
	{
		if (modCellPayload != null)
		{
			modCellPayload.DisplaySellDialog(this);
		}
	}

	private void AddCallbacks()
	{
		equipModButton.onClick.RemoveListener(SelectThisModCell);
		equipModButton.onClick.AddListener(SelectThisModCell);
		unequipModButton.onClick.RemoveListener(SelectThisModCell);
		unequipModButton.onClick.AddListener(SelectThisModCell);
		sellButton.onClick.RemoveListener(DisplaySellThisDialog);
		sellButton.onClick.AddListener(DisplaySellThisDialog);
	}

	private void UpdateCellState()
	{
		sellButton.gameObject.SetActive(modCellPayload.context.modSellable);
		sellButton.interactable = !modCellPayload.context.isEquipped;
		categoryInUseDescription.SetActive(!modCellPayload.context.modEquippable);
		availableParent.SetActive(modCellPayload.context.modEquippable);
		inUseParent.SetActive(!modCellPayload.context.modEquippable);
	}

	public void SetSelected(bool value)
	{
		highlightToggle.SetHighlightAndOtherCellsHighlightState(value);
	}

	public void SetData(ModSelectionCellData payload)
	{
		modCellPayload = payload;
		AddCallbacks();
		SetText();
		UpdateCellState();
		stars.SetStars(modCellPayload.context.Mod.Stars);
	}

	public void SetSprite(global::UnityEngine.Sprite icon)
	{
		image.overrideSprite = icon;
	}
}
