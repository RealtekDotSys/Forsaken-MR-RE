public interface IButtonClick
{
	global::UnityEngine.UI.Button button { get; set; }

	bool buttonClicked { get; set; }

	void ButtonClick();
}
