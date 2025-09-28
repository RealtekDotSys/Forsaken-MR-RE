public class PlayerAnimatronicInventory
{
	private global::System.Collections.Generic.Dictionary<string, int> inventory;

	public PlayerAnimatronicInventory()
	{
		inventory = new global::System.Collections.Generic.Dictionary<string, int>();
	}

	public void AddItem(string name, int amount)
	{
		if (inventory.ContainsKey(name))
		{
			inventory[name] += amount;
		}
		else
		{
			inventory.Add(name, amount);
		}
	}

	public global::System.Collections.Generic.Dictionary<string, int> GetInventory()
	{
		return inventory;
	}
}
