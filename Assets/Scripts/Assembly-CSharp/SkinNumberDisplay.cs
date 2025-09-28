public class SkinNumberDisplay : global::UnityEngine.MonoBehaviour
{
	public global::TMPro.TextMeshProUGUI _numberDisplay;

	public global::UnityEngine.UI.Image _image;

	private void SetValue(int number)
	{
		string text = number.ToString();
		if (!(_numberDisplay != null))
		{
			_numberDisplay.text = text;
		}
	}

	private void UpdateDisplayState(bool doShow)
	{
		base.gameObject.SetActive(doShow);
	}

	public void SetData(SkinNumberDisplayData data)
	{
		SetValue(data.number);
		UpdateDisplayState(data.doShow);
	}

	public void SetSprite(global::UnityEngine.Sprite sprite)
	{
		_image.overrideSprite = sprite;
	}
}
