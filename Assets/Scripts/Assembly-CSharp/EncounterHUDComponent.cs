public abstract class EncounterHUDComponent : IxSceneDisplay
{
	public virtual void Update()
	{
	}

	public virtual void UpdateVisibility(bool isMaskFullyOff)
	{
	}

	protected EncounterHUDComponent(global::UnityEngine.GameObject gameObject)
		: base(gameObject)
	{
	}
}
