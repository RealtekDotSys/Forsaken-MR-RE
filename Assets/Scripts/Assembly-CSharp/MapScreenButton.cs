public class MapScreenButton : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image portraitImage;

	public MapEntity entityData;

	public void Setup(MapEntity entity, GameAssetManagementDomain assetManagement, ItemDefinitions itemDefinitions)
	{
		entityData = entity;
		PlushSuitData plushSuitById = itemDefinitions.GetPlushSuitById(entityData.PlushSuitId);
		if (plushSuitById != null)
		{
			assetManagement.IconLookupAccess.GetIcon(IconGroup.Portrait, plushSuitById.PortraitIconName, IconRetrieved);
		}
	}

	public void Teardown()
	{
		entityData = null;
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	public void ButtonPress()
	{
		if (entityData.InteractionController == null)
		{
			global::UnityEngine.Debug.LogError("MAP ENTITY HAS NO INTERACTION CONTROLLER");
		}
		else
		{
			entityData.InteractionController.OnInteract(entityData);
		}
	}

	private void IconRetrieved(global::UnityEngine.Sprite sprite)
	{
		portraitImage.overrideSprite = sprite;
		base.gameObject.SetActive(value: true);
	}
}
