public struct EntityDisplayData
{
	public MapEntity entity;

	public EventExposer eventExposer;

	public MapEntityInteractionMutex interactionMutex;

	public global::System.Action<MapEntity> onConfirmStandardAction;

	public global::System.Action<MapEntity> onDismiss;

	public global::System.Action<MapEntity> onScanned;

	public global::System.Action<MapEntity> onForceDeleteEntity;
}
