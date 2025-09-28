public class ScavengingSelectStateUIView : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private ScavengingSelectButton prefab;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform prefabParent;

	private global::System.Collections.Generic.Dictionary<string, ScavengingSelectButton> buttons = new global::System.Collections.Generic.Dictionary<string, ScavengingSelectButton>();

	private global::System.Collections.Generic.List<string> scavengingEntitiesWithButtons = new global::System.Collections.Generic.List<string>();

	private void OnEnable()
	{
		MasterDomain.GetDomain().eventExposer.add_ScavengingEntitiesModelsUpdated(EventExposer_ScavengingEntitiesModelsUpdated);
		MasterDomain.GetDomain().ServerDomain.playerDataRequester.GetPlayerData(PlayerDataError);
	}

	private void PlayerDataError(ServerData reponse)
	{
		global::UnityEngine.Debug.LogError("Couldnt get player data heres the error ig: " + reponse.JSON);
	}

	private void OnDisable()
	{
		MasterDomain.GetDomain().eventExposer.remove_ScavengingEntitiesModelsUpdated(EventExposer_ScavengingEntitiesModelsUpdated);
		ClearAllButtons();
	}

	private void EventExposer_ScavengingEntitiesModelsUpdated(global::System.Collections.Generic.IEnumerable<ScavengingEntity> entities)
	{
		global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>(buttons.Keys);
		foreach (ScavengingEntity entity in entities)
		{
			list.Remove(entity.EntityId);
			CreateEntityButton(entity);
		}
		foreach (string item in list)
		{
			ClearButton(buttons[item]);
		}
	}

	private void CreateEntityButton(ScavengingEntity entity)
	{
		foreach (string key in buttons.Keys)
		{
			if (key == entity.EntityId)
			{
				return;
			}
		}
		MasterDomain domain = MasterDomain.GetDomain();
		ScavengingSelectButton scavengingSelectButton = global::UnityEngine.Object.Instantiate(prefab, prefabParent, worldPositionStays: false);
		scavengingSelectButton.Setup(entity, domain.GameAssetManagementDomain, domain.ItemDefinitionDomain.ItemDefinitions);
		buttons.Add(entity.EntityId, scavengingSelectButton);
	}

	private void ClearButton(ScavengingSelectButton button)
	{
		RemoveButton(button);
	}

	private void RemoveButton(ScavengingSelectButton button)
	{
		if (button != null)
		{
			buttons.Remove(button.entityData.EntityId);
			button.Teardown();
		}
	}

	private void ClearAllButtons()
	{
		global::System.Collections.Generic.List<ScavengingSelectButton> list = new global::System.Collections.Generic.List<ScavengingSelectButton>();
		foreach (ScavengingSelectButton value in buttons.Values)
		{
			list.Add(value);
		}
		foreach (ScavengingSelectButton item in list)
		{
			RemoveButton(item);
		}
		buttons.Clear();
		list.Clear();
		list = null;
	}
}
