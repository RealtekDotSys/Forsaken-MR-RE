public class SellModDisplayHandler : global::UnityEngine.MonoBehaviour, ICellInterface<ModCell>
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public SellModDisplayHandler _003C_003E4__this;

		public ModCell modCell;

		internal void _003CSetData_003Eb__0(Localization localization)
		{
			_003C_003E4__this.messageText.text = localization.GetLocalizedString(modCell.modContext.Mod.Name, modCell.modContext.Mod.Name);
			_003C_003E4__this.titleText.text = localization.GetLocalizedString("ui_workshop_modify_dialogs_sellprefix", "ui_workshop_modify_dialogs_sellprefix") + modCell.modContext.Mod.PartsBuyback + localization.GetLocalizedString("ui_workshop_modify_dialogs_sellpostfix", "ui_workshop_modify_dialogs_sellpostfix");
		}
	}

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI titleText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI messageText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image icon;

	[global::UnityEngine.SerializeField]
	private StarDisplay stars;

	private const string SELL_PREFIX_TEXT = "ui_workshop_modify_dialogs_sellprefix";

	private const string SELL_POSTFIX_TEXT = "ui_workshop_modify_dialogs_sellpostfix";

	public void SetData(ModCell modCell)
	{
		SellModDisplayHandler._003C_003Ec__DisplayClass6_0 _003C_003Ec__DisplayClass6_ = new SellModDisplayHandler._003C_003Ec__DisplayClass6_0();
		_003C_003Ec__DisplayClass6_._003C_003E4__this = this;
		_003C_003Ec__DisplayClass6_.modCell = modCell;
		_003C_003Ec__DisplayClass6_._003CSetData_003Eb__0(LocalizationDomain.Instance.Localization);
		stars.SetStars(modCell.modContext.Mod.Stars);
	}

	public void SetSprite(global::UnityEngine.Sprite sprite)
	{
		if (!(icon == null))
		{
			icon.overrideSprite = sprite;
		}
	}
}
