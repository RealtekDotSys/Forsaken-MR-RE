[global::System.Serializable]
public class OriginData
{
	[global::System.Serializable]
	public enum OriginState
	{
		NoState = -1,
		RandomSpawn = 0,
		Lure = 1,
		EssenceEncounter = 2,
		Scavenge = 3,
		Sent = 4,
		Tutorial = 5,
		MapEntitySystem = 6,
		COUNT = 7
	}

	public OriginData.OriginState originState { get; set; }

	public OriginData()
	{
		originState = OriginData.OriginState.RandomSpawn;
	}

	public OriginData(OriginData originData)
	{
		originState = originData.originState;
	}

	public OriginData(OriginData.OriginState originState)
	{
		this.originState = originState;
	}

	public override string ToString()
	{
		return originState.ToString();
	}
}
