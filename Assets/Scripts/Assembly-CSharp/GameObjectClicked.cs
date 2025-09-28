public class GameObjectClicked : IButtonClick
{
	public bool buttonClicked { get; set; }

	public global::UnityEngine.UI.Button button { get; set; }

	public GameObjectClicked(global::UnityEngine.GameObject gameObject)
	{
		buttonClicked = false;
		if (button == null)
		{
			button = gameObject.AddComponent<global::UnityEngine.UI.Button>();
		}
		button.onClick.RemoveListener(ButtonClick);
		button.onClick.AddListener(ButtonClick);
	}

	public void ButtonClick()
	{
		buttonClicked = true;
	}
}
