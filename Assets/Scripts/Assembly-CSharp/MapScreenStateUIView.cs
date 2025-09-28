public class MapScreenStateUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private MapScreenButton prefab;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform naturalParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform friendsParent;

	private global::System.Collections.Generic.Dictionary<string, MapScreenButton> buttons = new global::System.Collections.Generic.Dictionary<string, MapScreenButton>();

	private global::System.Collections.Generic.List<string> mapEntitiesWithButtons = new global::System.Collections.Generic.List<string>();

	private void OnEnable()
	{
		MasterDomain.GetDomain().eventExposer.add_MapEntitiesModelsUpdated(EventExposer_MapEntitiesModelsUpdated);
		MasterDomain.GetDomain().ServerDomain.playerDataRequester.GetPlayerData(PlayerDataError);
	}

	private void PlayerDataError(ServerData reponse)
	{
		global::UnityEngine.Debug.LogError("Couldnt get player data heres the error ig: " + reponse.JSON);
	}

	private void MapEntitiesReceived(global::System.Collections.Generic.IEnumerable<MapEntitySynchronizeableState> eh)
	{
		MasterDomain.GetDomain().MapEntityDomain.RequestNewModels();
	}

	private void OnDisable()
	{
		MasterDomain.GetDomain().eventExposer.remove_MapEntitiesModelsUpdated(EventExposer_MapEntitiesModelsUpdated);
		ClearAllButtons();
	}

	private void EventExposer_MapEntitiesModelsUpdated(global::System.Collections.Generic.IEnumerable<MapEntity> entities)
	{
		global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>(buttons.Keys);
		foreach (MapEntity entity in entities)
		{
			list.Remove(entity.EntityId);
			CreateEntityButton(entity);
		}
		foreach (string item in list)
		{
			ClearButton(buttons[item]);
		}
	}

	private void CreateEntityButton(MapEntity entity)
	{
		foreach (string key in buttons.Keys)
		{
			if (key == entity.EntityId)
			{
				return;
			}
		}
		MasterDomain domain = MasterDomain.GetDomain();
		if (entity.SynchronizeableState.history.sourceFeature != "SentFromFriend")
		{
			MapScreenButton mapScreenButton = global::UnityEngine.Object.Instantiate(prefab, naturalParent, worldPositionStays: false);
			mapScreenButton.Setup(entity, domain.GameAssetManagementDomain, domain.ItemDefinitionDomain.ItemDefinitions);
			buttons.Add(entity.EntityId, mapScreenButton);
		}
		else
		{
			MapScreenButton mapScreenButton2 = global::UnityEngine.Object.Instantiate(prefab, friendsParent, worldPositionStays: false);
			mapScreenButton2.Setup(entity, domain.GameAssetManagementDomain, domain.ItemDefinitionDomain.ItemDefinitions);
			buttons.Add(entity.EntityId, mapScreenButton2);
		}
	}

	private void ClearButton(MapScreenButton button)
	{
		RemoveButton(button);
	}

	private void RemoveButton(MapScreenButton button)
	{
		if (button != null)
		{
			buttons.Remove(button.entityData.EntityId);
			button.Teardown();
		}
	}

	private void ClearAllButtons()
	{
		global::System.Collections.Generic.List<MapScreenButton> list = new global::System.Collections.Generic.List<MapScreenButton>();
		foreach (MapScreenButton value in buttons.Values)
		{
			list.Add(value);
		}
		foreach (MapScreenButton item in list)
		{
			RemoveButton(item);
		}
		buttons.Clear();
		list.Clear();
		list = null;
	}
}
