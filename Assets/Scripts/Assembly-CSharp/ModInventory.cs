public class ModInventory
{
	public global::System.Collections.Generic.Dictionary<ModData, int> entries { get; set; }

	public ModInventory()
	{
		entries = new global::System.Collections.Generic.Dictionary<ModData, int>();
	}

	public bool ContainsMod(ModData modData)
	{
		foreach (ModData key in entries.Keys)
		{
			if (key == modData)
			{
				return true;
			}
		}
		return false;
	}

	public void AddMod(ModData key, int amount)
	{
		entries.Add(key, amount);
	}

	public void RemoveMod(ModData key, int amount)
	{
		ModData modData = null;
		foreach (ModData key2 in entries.Keys)
		{
			if (key2.Id == key.Id)
			{
				modData = key2;
			}
		}
		if (modData != null)
		{
			if (entries[modData] > amount)
			{
				entries[modData] -= amount;
			}
			else
			{
				entries.Remove(modData);
			}
		}
	}

	public global::System.Collections.Generic.Dictionary<ModData, int> GetMods()
	{
		global::System.Collections.Generic.Dictionary<ModData, int> dictionary = new global::System.Collections.Generic.Dictionary<ModData, int>();
		foreach (ModData key in entries.Keys)
		{
			dictionary.Add(key, entries[key]);
		}
		return dictionary;
	}

	public global::System.Collections.Generic.Dictionary<ModData, int> GetRandomItems()
	{
		global::System.Collections.Generic.Dictionary<ModData, int> dictionary = new global::System.Collections.Generic.Dictionary<ModData, int>();
		global::System.Collections.Generic.List<ModData> list = new global::System.Collections.Generic.List<ModData>();
		foreach (ModData key in entries.Keys)
		{
			list.Add(key);
		}
		global::System.Random rng = new global::System.Random();
		global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.OrderBy(list, (ModData _) => rng.Next()));
		foreach (ModData item in list)
		{
			dictionary.Add(item, entries[item]);
		}
		return dictionary;
	}

	public void UpdateFromLookup(ItemDefinitions itemDefinitions)
	{
		foreach (ModData key in entries.Keys)
		{
			ModData modById = itemDefinitions.GetModById(key.Id);
			key.Copy(modById);
		}
	}
}
