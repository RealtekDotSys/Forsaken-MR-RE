public class MapEntityServerStateParser
{
	public MapEntitySynchronizeableState Parse(ServerData data)
	{
		return new MapEntitySynchronizeableState
		{
			entityId = data.GetString("EntityId"),
			entityClass = (MapEntityType)global::System.Enum.Parse(typeof(MapEntityType), data.GetString("EntityClass")),
			spawnTimeStamp = data.GetLong("SpawnTime").Value,
			removeTimeStamp = data.GetLong("RemoveTime").Value,
			revealedBy = ParseRevealedState(data),
			history = GenerateMapEntityHistory(data.GetServerData("History")),
			parts = GenerateMapEntityParts(data.GetServerData("AnimatronicParts")),
			legacyEssence = data.GetInt("Essence").Value,
			onScreenDuration = data.GetLong("OnScreenDuration").Value,
			aggression = data.GetInt("Aggression").Value,
			perception = data.GetInt("Perception").Value
		};
	}

	private RevealedBy ParseRevealedState(ServerData data)
	{
		if (data.GetBoolean("RevealedFlag") == false)
		{
			return RevealedBy.None;
		}
		if (data.GetString("RevealedBy") == null)
		{
			return RevealedBy.Legacy;
		}
		return (RevealedBy)global::System.Enum.Parse(typeof(RevealedBy), data.GetString("RevealedBy"));
	}

	private MapEntityHistory GenerateMapEntityHistory(ServerData data)
	{
		MapEntityHistory mapEntityHistory = new MapEntityHistory();
		if (data.GetString("SourceFeature") != null)
		{
			data.GetString("SourceFeature");
		}
		mapEntityHistory.owner = data.GetString("Owner");
		mapEntityHistory.ownerDisplayName = data.GetString("OwnerDisplayName");
		mapEntityHistory.sourceFeature = data.GetString("SourceFeature");
		return mapEntityHistory;
	}

	private global::System.Collections.Generic.Dictionary<string, string> GenerateMapEntityParts(ServerData data)
	{
		global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
		foreach (string key in data.Keys)
		{
			dictionary.Add(key, data.GetString(key));
		}
		return dictionary;
	}

	private global::System.Collections.Generic.Dictionary<string, float> GenerateRemnantSpawnWeights(ServerData data)
	{
		return null;
	}
}
