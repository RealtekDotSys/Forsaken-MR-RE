public class TrophyCell : global::UnityEngine.MonoBehaviour
{
	public class TrophyCellData
	{
		public bool playerOwned;

		public TrophyData serverData;

		public global::System.Action<TrophyCell, bool> SelectFunction;

		public bool isValid;
	}

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject highlightObject;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image image;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button button;

	public TrophyCell.TrophyCellData trophyCellData;

	public void SetData(TrophyCell.TrophyCellData data)
	{
		trophyCellData = data;
		if (trophyCellData.SelectFunction != null)
		{
			button.onClick.AddListener(SetDatab__7_0);
		}
		if (button != null)
		{
			button.interactable = data.isValid && data.playerOwned;
		}
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
		trophyCellData.SelectFunction(this, arg2: false);
	}
}
