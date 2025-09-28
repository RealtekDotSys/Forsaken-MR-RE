public class SetIconSize : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Vector2 size = new global::UnityEngine.Vector2(5f, 5f);

	private global::UnityEngine.SpriteRenderer _sprite;

	private void Start()
	{
		_sprite = GetComponent<global::UnityEngine.SpriteRenderer>();
	}

	private void Update()
	{
		if (!(_sprite.size == size))
		{
			_sprite.size = size;
		}
	}
}
