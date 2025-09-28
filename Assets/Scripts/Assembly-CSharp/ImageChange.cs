public class ImageChange : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Sprite[] sprite;

	public global::UnityEngine.UI.Image ImageComponent;

	public void ChangeImage()
	{
		if (sprite.Length >= 2)
		{
			ImageComponent.sprite = sprite[1];
		}
	}
}
