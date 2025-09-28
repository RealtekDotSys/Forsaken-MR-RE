public class ExitAttackSequenceDisplay : IxDisplay
{
	private EventExposer _eventExposer;

	public ExitAttackSequenceDisplay(PrefabInstance instance)
		: base(instance)
	{
	}

	protected override void CacheAndPopulateComponents()
	{
		global::System.Type[] onlyCacheTypes = new global::System.Type[2]
		{
			typeof(global::TMPro.TextMeshProUGUI),
			typeof(global::UnityEngine.UI.Button)
		};
		_components.CacheComponents(_root, onlyCacheTypes);
	}

	public void Setup(EventExposer eventExposer)
	{
		_eventExposer = eventExposer;
		eventExposer.add_AttackSequenceEnded(CloseOnEncounterEnd);
	}

	private void CloseOnEncounterEnd()
	{
		ForceClose();
	}

	public override void Teardown()
	{
		_eventExposer.remove_AttackSequenceEnded(CloseOnEncounterEnd);
		_eventExposer = null;
		base.Teardown();
	}
}
