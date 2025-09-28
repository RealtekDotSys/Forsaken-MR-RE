public class ScavengingEntitySynchronizeableState
{
	public string entityId { get; set; }

	public ScavengingEntityType entityClass { get; set; }

	public long spawnTimeStamp { get; set; }

	public long removeTimeStamp { get; set; }

	public long onScreenDuration { get; set; }

	public global::System.Collections.Generic.Dictionary<string, string> parts { get; set; }

	public string environment { get; set; }
}
