public class Container
{
	public class EntityAddedRemovedArgs
	{
		public global::System.Collections.Generic.List<AnimatronicEntity> Entities;
	}

	public class EntitiesClearedArgs
	{
	}

	private global::System.Collections.Generic.List<AnimatronicEntity> _animatronicEntities;

	private global::System.Collections.Generic.List<AnimatronicEntity> _fakeAnimatronicEntities;

	public global::System.Collections.Generic.IEnumerable<AnimatronicEntity> AnimatronicEntities => _animatronicEntities;

	public global::System.Collections.Generic.IEnumerable<AnimatronicEntity> FakeAnimatronicEntities => _fakeAnimatronicEntities;

	public event global::System.Action<Container.EntityAddedRemovedArgs> OnEntityAddedEvent;

	public event global::System.Action<Container.EntityAddedRemovedArgs> OnEntityRemovedEvent;

	public event global::System.Action<Container.EntitiesClearedArgs> OnEntitiesClearedEvent;

	private bool ShouldAddNewEntity(AnimatronicEntity entity)
	{
		if (entity.originData.originState == OriginData.OriginState.Scavenge)
		{
			return true;
		}
		return entity.originData.originState == OriginData.OriginState.MapEntitySystem;
	}

	private AnimatronicEntity GetFirstAnimatronic()
	{
		if (_animatronicEntities.Count >= 1)
		{
			return _animatronicEntities[0];
		}
		return null;
	}

	private AnimatronicEntity GetAnimatronicClosestToAttackPhase(AnimatronicEntity animatronicEntity, int highestPhase)
	{
		AnimatronicEntity result = animatronicEntity;
		foreach (AnimatronicEntity animatronicEntity2 in _animatronicEntities)
		{
			result = ((!IsCloserPhase(animatronicEntity2, highestPhase)) ? animatronicEntity2 : animatronicEntity);
		}
		return result;
	}

	private bool IsCloserPhase(AnimatronicEntity entity, int highestPhase)
	{
		return (int)entity.stateData.animatronicState > highestPhase;
	}

	public global::System.Collections.Generic.List<AnimatronicEntity> GetAllEntities()
	{
		global::System.Collections.Generic.List<AnimatronicEntity> list = new global::System.Collections.Generic.List<AnimatronicEntity>();
		list.AddRange(_animatronicEntities);
		list.AddRange(_fakeAnimatronicEntities);
		return list;
	}

	public AnimatronicEntity GetAnimatronicClosestToPlayer()
	{
		AnimatronicEntity firstAnimatronic = GetFirstAnimatronic();
		return GetAnimatronicClosestToAttackPhase(firstAnimatronic, (int)firstAnimatronic.stateData.animatronicState);
	}

	public string GetFakeID(string id)
	{
		return "fake_" + id + "_" + _fakeAnimatronicEntities.Count;
	}

	public bool IsEmpty()
	{
		return _animatronicEntities.Count == 0;
	}

	public int NumEntities()
	{
		return _animatronicEntities.Count;
	}

	public bool ContainsEntity(string entityId)
	{
		foreach (AnimatronicEntity allEntity in GetAllEntities())
		{
			if (allEntity.entityId == entityId)
			{
				return true;
			}
		}
		return false;
	}

	public AnimatronicEntity GetEntity(string entityId)
	{
		foreach (AnimatronicEntity allEntity in GetAllEntities())
		{
			if (allEntity.entityId == entityId)
			{
				return allEntity;
			}
		}
		return null;
	}

	public AnimatronicEntity GetScavengingEntity()
	{
		foreach (AnimatronicEntity allEntity in GetAllEntities())
		{
			if (allEntity.stateData.animatronicState == StateData.AnimatronicState.Scavenging || allEntity.stateData.animatronicState == StateData.AnimatronicState.ScavengeAppointment)
			{
				return allEntity;
			}
		}
		return null;
	}

	public void AddEntity(AnimatronicEntity entity)
	{
		global::UnityEngine.Debug.LogError("CONTAINER IS MAKING SCAVENGING ENTITIES");
		if (ShouldAddNewEntity(entity))
		{
			_animatronicEntities.Add(entity);
			if (this.OnEntityAddedEvent != null)
			{
				global::System.Collections.Generic.List<AnimatronicEntity> list = new global::System.Collections.Generic.List<AnimatronicEntity>();
				list.Add(entity);
				Container.EntityAddedRemovedArgs entityAddedRemovedArgs = new Container.EntityAddedRemovedArgs();
				entityAddedRemovedArgs.Entities = list;
				this.OnEntityAddedEvent(entityAddedRemovedArgs);
			}
		}
	}

	public void AddEntities(global::System.Collections.Generic.List<AnimatronicEntity> entities)
	{
		global::UnityEngine.Debug.LogError("CONTAINER IS MAKING ENTITIES");
		global::System.Collections.Generic.List<AnimatronicEntity> list = new global::System.Collections.Generic.List<AnimatronicEntity>();
		foreach (AnimatronicEntity entity in entities)
		{
			if (ShouldAddNewEntity(entity))
			{
				_animatronicEntities.Add(entity);
				list.Add(entity);
			}
		}
		if (this.OnEntityAddedEvent == null)
		{
			global::UnityEngine.Debug.LogError("ON ENTITY ADDED EVENT FOR CONTAINER IS NULL.");
			return;
		}
		global::UnityEngine.Debug.LogError("CALLING ON ENTITY ADDED EVENT");
		Container.EntityAddedRemovedArgs entityAddedRemovedArgs = new Container.EntityAddedRemovedArgs();
		entityAddedRemovedArgs.Entities = list;
		this.OnEntityAddedEvent(entityAddedRemovedArgs);
	}

	public void AddFakeEntity(AnimatronicEntity entity)
	{
		_fakeAnimatronicEntities.Add(entity);
		if (this.OnEntityAddedEvent != null)
		{
			global::System.Collections.Generic.List<AnimatronicEntity> list = new global::System.Collections.Generic.List<AnimatronicEntity>();
			list.Add(entity);
			Container.EntityAddedRemovedArgs entityAddedRemovedArgs = new Container.EntityAddedRemovedArgs();
			entityAddedRemovedArgs.Entities = list;
			this.OnEntityAddedEvent(entityAddedRemovedArgs);
		}
	}

	public void RemoveEntity(AnimatronicEntity entity)
	{
		if (_animatronicEntities.Contains(entity))
		{
			_animatronicEntities.Remove(entity);
			if (this.OnEntityRemovedEvent != null)
			{
				global::System.Collections.Generic.List<AnimatronicEntity> list = new global::System.Collections.Generic.List<AnimatronicEntity>();
				list.Add(entity);
				Container.EntityAddedRemovedArgs entityAddedRemovedArgs = new Container.EntityAddedRemovedArgs();
				entityAddedRemovedArgs.Entities = list;
				this.OnEntityRemovedEvent(entityAddedRemovedArgs);
			}
		}
		if (_fakeAnimatronicEntities.Contains(entity))
		{
			_fakeAnimatronicEntities.Remove(entity);
		}
	}

	public void Clear()
	{
		_animatronicEntities.Clear();
		_fakeAnimatronicEntities.Clear();
		this.OnEntitiesClearedEvent?.Invoke(new Container.EntitiesClearedArgs());
	}

	public void RegisterEntityAdded(global::System.Action<Container.EntityAddedRemovedArgs> callback)
	{
		this.OnEntityAddedEvent = callback;
	}

	public void RegisterEntityRemoved(global::System.Action<Container.EntityAddedRemovedArgs> callback)
	{
		this.OnEntityRemovedEvent = callback;
	}

	public void RegisterEntitiesCleared(global::System.Action<Container.EntitiesClearedArgs> callback)
	{
		this.OnEntitiesClearedEvent = callback;
	}

	public Container()
	{
		_animatronicEntities = new global::System.Collections.Generic.List<AnimatronicEntity>();
		_fakeAnimatronicEntities = new global::System.Collections.Generic.List<AnimatronicEntity>();
	}
}
