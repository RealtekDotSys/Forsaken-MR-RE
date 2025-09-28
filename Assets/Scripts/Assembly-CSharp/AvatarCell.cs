public class AvatarCell : global::UnityEngine.MonoBehaviour, ICellInterface<AvatarCellDataPack>
{
	private sealed class _003C_003Ec__DisplayClass2_0
	{
		public AvatarCellDataPack data;

		internal void _003CSetData_003Eb__0()
		{
			data.SelectCell(data.id);
		}
	}

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Hookups")]
	private global::UnityEngine.UI.Image avatarImage;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button button;

	public void SetData(AvatarCellDataPack data)
	{
		AvatarCell._003C_003Ec__DisplayClass2_0 _003C_003Ec__DisplayClass2_ = new AvatarCell._003C_003Ec__DisplayClass2_0();
		_003C_003Ec__DisplayClass2_.data = data;
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(_003C_003Ec__DisplayClass2_._003CSetData_003Eb__0);
	}

	public void SetSprite(global::UnityEngine.Sprite sprite)
	{
		avatarImage.sprite = sprite;
	}
}
