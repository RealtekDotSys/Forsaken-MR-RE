public class CPUInventory
{
	public global::System.Collections.Generic.Dictionary<string, int> entries { get; set; }

	public CPUInventory()
	{
		entries = new global::System.Collections.Generic.Dictionary<string, int>();
	}

	public void Add(string key, int amount)
	{
		entries.Add(key, amount);
	}
}
