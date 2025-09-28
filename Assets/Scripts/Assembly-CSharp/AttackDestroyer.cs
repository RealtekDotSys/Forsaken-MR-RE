public class AttackDestroyer
{
	private AttackSpawner _spawner;

	private Animatronic3DDomain _animatronic3DDomain;

	private bool addedCallback;

	public event global::System.Action<AttackAnimatronicDestroyedPayload> OnAnimatronicAboutToBeDestroyed;

	private void AttackAnimatronicSpawned(AttackAnimatronic animatronic)
	{
		global::UnityEngine.Debug.LogError("adding ready to cleanup");
		animatronic.OnReadyForCleanup += AttackAnimatronicReadyForCleanup;
	}

	private void AttackAnimatronicReadyForCleanup(AttackAnimatronic animatronic)
	{
		global::UnityEngine.Debug.LogError("Cleanup request");
		if (animatronic != null && animatronic.Entity != null)
		{
			animatronic.OnReadyForCleanup -= AttackAnimatronicReadyForCleanup;
			AttackAnimatronicDestroyedPayload attackAnimatronicDestroyedPayload = new AttackAnimatronicDestroyedPayload();
			attackAnimatronicDestroyedPayload.EntityId = animatronic.EntityId;
			this.OnAnimatronicAboutToBeDestroyed?.Invoke(attackAnimatronicDestroyedPayload);
			global::UnityEngine.Debug.LogError("releasing animatronic 3d");
			_animatronic3DDomain.ReleaseAnimatronic3D(animatronic.Model);
			animatronic.Teardown();
		}
	}

	public void Setup(AttackSpawner spawner, Animatronic3DDomain animatronic3DDomain)
	{
		_spawner = spawner;
		_animatronic3DDomain = animatronic3DDomain;
		if (!addedCallback)
		{
			_spawner.OnAnimatronicSpawned += AttackAnimatronicSpawned;
			addedCallback = true;
		}
	}

	public void Teardown()
	{
		if (_spawner != null)
		{
			_spawner.OnAnimatronicSpawned -= AttackAnimatronicSpawned;
		}
		_spawner = null;
	}
}
