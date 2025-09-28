public class ScavengingEntityBuilder
{
	public ScavengingEntity CreateEntityFromState(ScavengingEntitySynchronizeableState synchronizeableState)
	{
		ScavengingEntity scavengingEntity = new ScavengingEntity(synchronizeableState, synchronizeOnServer: true);
		scavengingEntity.LocalSpawnTime = global::UnityEngine.Time.time;
		scavengingEntity.LocalRemoveTime = global::UnityEngine.Time.time + (float)synchronizeableState.onScreenDuration;
		global::UnityEngine.Debug.Log(scavengingEntity.LocalSpawnTime + " - " + scavengingEntity.LocalRemoveTime + " - " + synchronizeableState.onScreenDuration);
		return scavengingEntity;
	}

	public void UpdateExistingEntityState(ScavengingEntity entity, ScavengingEntitySynchronizeableState synchronizeableState)
	{
		entity.SynchronizeableState = synchronizeableState;
	}
}
