public class MapEntityLifecycleRegulator
{
	private EventExposer _eventExposer;

	private ServerDomain _serverDomain;

	private global::System.Collections.Generic.Dictionary<string, MapEntity> _allEntitiesRef;

	private float _syncTimer;

	private global::System.Collections.Generic.Dictionary<string, MapEntity> _tempValidEntities;

	private global::System.Collections.Generic.List<string> _entityIdsToRetire;

	private global::System.Collections.Generic.List<string> _retiredEntityIdsForServer;

	private global::System.Collections.Generic.List<string> _scannedEntityIdsForServer;

	private CONFIG_DATA.MapEntities _mapEntityConfig;

	public void DispatchValidEntityModelState()
	{
		_tempValidEntities.Clear();
		global::UnityEngine.Debug.Log("entities count: " + _allEntitiesRef.Keys.Count);
		foreach (string key in _allEntitiesRef.Keys)
		{
			MapEntity entity = _allEntitiesRef[key];
			if (IsCurrentTimeValidForEntity(entity))
			{
				global::UnityEngine.Debug.Log("adding temp entity");
				_tempValidEntities[key] = _allEntitiesRef[key];
			}
			else
			{
				global::UnityEngine.Debug.Log("time aint valid");
			}
		}
		global::UnityEngine.Debug.Log("updating temp valid entities");
		_eventExposer.OnMapEntitiesModelsUpdated(_tempValidEntities.Values);
	}

	public void ClearEntityModelState()
	{
		global::UnityEngine.Debug.Log("Cleared model states");
		_eventExposer.OnMapEntitiesModelsUpdated(new global::System.Collections.Generic.List<MapEntity>());
	}

	public void AddClientsideScan(MapEntity entity)
	{
		if (entity.SynchronizeOnServer && !_retiredEntityIdsForServer.Contains(entity.SynchronizeableState.entityId))
		{
			_scannedEntityIdsForServer.Add(entity.SynchronizeableState.entityId);
		}
	}

	private void EventExposer_UnityFrameUpdate(float dt)
	{
		_syncTimer += dt;
		MasterDomain.GetDomain();
		if (CountEntitiesActivatedBetween(global::UnityEngine.Time.time - dt, global::UnityEngine.Time.time) > 0 || TryFlushExpiredEntities())
		{
			DispatchValidEntityModelState();
		}
		if (!((double)_mapEntityConfig.Server.ClientBatchedRetireEventPeriodSeconds > (double)_syncTimer))
		{
			DispatchExpiredEntityUpdateToServer();
			_syncTimer = 0f;
		}
	}

	public MapEntityLifecycleRegulator(EventExposer eventExposer, global::System.Collections.Generic.Dictionary<string, MapEntity> entities)
	{
		_eventExposer = eventExposer;
		_allEntitiesRef = entities;
		global::UnityEngine.Debug.LogWarning("should be THIS MANY ENTITIES: " + entities.Values.Count);
		_tempValidEntities = new global::System.Collections.Generic.Dictionary<string, MapEntity>();
		_entityIdsToRetire = new global::System.Collections.Generic.List<string>();
		_retiredEntityIdsForServer = new global::System.Collections.Generic.List<string>();
		_scannedEntityIdsForServer = new global::System.Collections.Generic.List<string>();
	}

	public void Setup(ServerDomain serverDomain, CONFIG_DATA.MapEntities mapEntityConfig)
	{
		_serverDomain = serverDomain;
		_syncTimer = 0f;
		_eventExposer.add_UnityFrameUpdate(EventExposer_UnityFrameUpdate);
		_mapEntityConfig = mapEntityConfig;
	}

	public void Teardown()
	{
		_eventExposer.remove_UnityFrameUpdate(EventExposer_UnityFrameUpdate);
		_eventExposer = null;
		_allEntitiesRef = null;
		_tempValidEntities = null;
		_retiredEntityIdsForServer = null;
	}

	private int CountEntitiesActivatedBetween(float previousTime, float currentTime)
	{
		int num = 0;
		foreach (string key in _allEntitiesRef.Keys)
		{
			MapEntity mapEntity = _allEntitiesRef[key];
			if (mapEntity.LocalSpawnTime > previousTime && mapEntity.LocalSpawnTime <= currentTime)
			{
				num++;
			}
		}
		return num;
	}

	private bool TryFlushExpiredEntities()
	{
		foreach (string key in _allEntitiesRef.Keys)
		{
			if (!IsEntityExpired(_allEntitiesRef[key]))
			{
				continue;
			}
			_entityIdsToRetire.Add(key);
			if (_allEntitiesRef[key].SynchronizeOnServer)
			{
				_retiredEntityIdsForServer.Add(key);
				if (_scannedEntityIdsForServer.Contains(key))
				{
					_scannedEntityIdsForServer.Remove(key);
				}
			}
		}
		if (_entityIdsToRetire.Count < 1)
		{
			return false;
		}
		foreach (string item in _entityIdsToRetire)
		{
			if (_allEntitiesRef.ContainsKey(item))
			{
				global::UnityEngine.Debug.Log("retiring.");
				_allEntitiesRef.Remove(item);
			}
		}
		return true;
	}

	private void DispatchExpiredEntityUpdateToServer()
	{
		if (_retiredEntityIdsForServer.Count >= 1)
		{
			_serverDomain.mapEntityScanAndRetireEntitiesRequester.ScanAndRetireEntities(_scannedEntityIdsForServer, _retiredEntityIdsForServer);
		}
		_retiredEntityIdsForServer.Clear();
	}

	private void FlushAllServerEntities()
	{
		global::UnityEngine.Debug.Log("FLUSHING");
		foreach (string key in _allEntitiesRef.Keys)
		{
			if (_allEntitiesRef[key].SynchronizeOnServer)
			{
				_allEntitiesRef.Remove(key);
				_retiredEntityIdsForServer.Add(key);
			}
		}
	}

	private bool IsEntityExpired(MapEntity entity)
	{
		return global::UnityEngine.Time.time >= entity.LocalRemoveTime;
	}

	private bool IsCurrentTimeValidForEntity(MapEntity entity)
	{
		if (global::UnityEngine.Time.time < entity.LocalSpawnTime)
		{
			return false;
		}
		return global::UnityEngine.Time.time < entity.LocalRemoveTime;
	}
}
