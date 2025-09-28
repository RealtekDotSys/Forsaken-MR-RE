public class ScavengingEntityServerStateParser
{
	public ScavengingEntitySynchronizeableState Parse(ServerData data)
	{
		return new ScavengingEntitySynchronizeableState
		{
			entityId = data.GetString("EntityId"),
			entityClass = (ScavengingEntityType)global::System.Enum.Parse(typeof(ScavengingEntityType), data.GetString("EntityClass")),
			spawnTimeStamp = data.GetLong("SpawnTime").Value,
			removeTimeStamp = data.GetLong("RemoveTime").Value,
			onScreenDuration = data.GetLong("OnScreenDuration").Value,
			parts = GenerateScavengingEntityParts(data.GetServerData("AnimatronicParts")),
			environment = data.GetString("ScavengingLocation")
		};
	}

	private global::System.Collections.Generic.Dictionary<string, string> GenerateScavengingEntityParts(ServerData data)
	{
		global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
		foreach (string key in data.Keys)
		{
			dictionary.Add(key, data.GetString(key));
		}
		return dictionary;
	}
}
