public class ScavengingEntityDomain
{
	private EventExposer _eventExposer;

	private ServerDomain _serverDomain;

	private ItemDefinitions _itemDefinitions;

	private ScavengingEntityBuilder _builder;

	private global::System.Collections.Generic.Dictionary<string, ScavengingEntity> _entities;

	private global::System.Collections.Generic.IEnumerable<ScavengingEntitySynchronizeableState> _scavengingEntityServerStates;

	private bool _readyToPopulateEntities;

	private ScavengingEntityLifecycleRegulator _lifecycleRegulator;

	private MasterDataConnector _masterDataConnector;

	private CONFIG_DATA.MapEntities _mapEntityConfig;

	protected bool IsReady => true;

	public ScavengingEntityBuilder Builder => _builder;

	private void EventExposer_ScavengingEntitiesReceivedFromServer(global::System.Collections.Generic.IEnumerable<ScavengingEntitySynchronizeableState> entityServerStates)
	{
		_scavengingEntityServerStates = entityServerStates;
		if (_readyToPopulateEntities)
		{
			ProcessNewEntityServerStates(entityServerStates);
		}
	}

	private void ConfigDataReady(CONFIG_DATA.Root config)
	{
		_mapEntityConfig = config.Entries[0].MapEntities;
		_lifecycleRegulator = new ScavengingEntityLifecycleRegulator(_eventExposer, _entities);
		_lifecycleRegulator.Setup(_serverDomain, _mapEntityConfig);
		OnLoadingFinished();
	}

	public ScavengingEntityDomain(EventExposer eventExposer, MasterDataDomain masterDataDomain)
	{
		_eventExposer = eventExposer;
		_builder = null;
		_lifecycleRegulator = null;
		_entities = new global::System.Collections.Generic.Dictionary<string, ScavengingEntity>();
		_scavengingEntityServerStates = null;
		_readyToPopulateEntities = false;
		masterDataDomain.GetAccessToData.GetConfigDataEntryAsync(ConfigDataReady);
	}

	public void Setup(ServerDomain serverDomain, ItemDefinitions itemDefinitions)
	{
		_serverDomain = serverDomain;
		_itemDefinitions = itemDefinitions;
		_eventExposer.add_ScavengingEntitiesReceivedFromServer(EventExposer_ScavengingEntitiesReceivedFromServer);
		_builder = new ScavengingEntityBuilder();
	}

	private void OnLoadingFinished()
	{
		PopulateEntities();
	}

	private void PopulateEntities()
	{
		_readyToPopulateEntities = true;
		if (_scavengingEntityServerStates != null)
		{
			ProcessNewEntityServerStates(_scavengingEntityServerStates);
		}
	}

	public void Teardown()
	{
		if (_lifecycleRegulator != null)
		{
			_lifecycleRegulator.Teardown();
		}
		_lifecycleRegulator = null;
		_eventExposer.remove_ScavengingEntitiesReceivedFromServer(EventExposer_ScavengingEntitiesReceivedFromServer);
		_scavengingEntityServerStates = null;
		_entities.Clear();
		_masterDataConnector = null;
		_eventExposer = null;
		_builder = null;
		_entities = null;
	}

	public bool TryGetEntity(string key, out ScavengingEntity result)
	{
		return _entities.TryGetValue(key, out result);
	}

	private void ProcessNewEntityServerStates(global::System.Collections.Generic.IEnumerable<ScavengingEntitySynchronizeableState> entityServerStates)
	{
		foreach (string item in CollectServerOnlyEntityKeys())
		{
			_entities.Remove(item);
		}
		foreach (ScavengingEntitySynchronizeableState entityServerState in entityServerStates)
		{
			ProcessServerUpdateForEntityId(entityServerState);
		}
		global::UnityEngine.Debug.Log("new scavenging entities! total: " + _entities.Values.Count);
		RequestNewModels();
	}

	public void RequestNewModels()
	{
		_lifecycleRegulator.DispatchValidEntityModelState();
	}

	private void ProcessServerUpdateForEntityId(ScavengingEntitySynchronizeableState synchronizeableState)
	{
		if (!_entities.ContainsKey(synchronizeableState.entityId))
		{
			_entities[synchronizeableState.entityId] = _builder.CreateEntityFromState(synchronizeableState);
		}
		else
		{
			_entities[synchronizeableState.entityId].SynchronizeableState = synchronizeableState;
		}
	}

	private global::System.Collections.Generic.HashSet<string> CollectServerOnlyEntityKeys()
	{
		global::System.Collections.Generic.HashSet<string> hashSet = new global::System.Collections.Generic.HashSet<string>();
		foreach (string key in _entities.Keys)
		{
			if (_entities[key].SynchronizeOnServer)
			{
				hashSet.Add(key);
			}
		}
		return hashSet;
	}
}
