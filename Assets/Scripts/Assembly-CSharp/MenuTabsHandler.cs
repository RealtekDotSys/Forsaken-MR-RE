public class MenuTabsHandler
{
	private global::System.Collections.Generic.List<TabParentKeyValue> tabParents;

	private Tab currentTab;

	private global::System.Action<Tab> DidSwitchTabs;

	private global::System.Action<Tab> WillSwitchTabs;

	private bool IsCurrentTab(Tab tab)
	{
		foreach (TabParentKeyValue tabParent in tabParents)
		{
			if (tabParent.tab == tab)
			{
				return tabParent.parent.activeSelf;
			}
		}
		return false;
	}

	public MenuTabsHandler(global::System.Collections.Generic.List<TabParentKeyValue> tabParents, global::System.Action<Tab> switchedTabs, global::System.Action<Tab> willSwitchTabs)
	{
		this.tabParents = tabParents;
		DidSwitchTabs = switchedTabs;
		WillSwitchTabs = willSwitchTabs;
	}

	public void ShowTab(Tab tab)
	{
		if (IsCurrentTab(tab))
		{
			return;
		}
		WillSwitchTabs(tab);
		foreach (TabParentKeyValue tabParent in tabParents)
		{
			tabParent.parent.SetActive(tabParent.tab == tab);
		}
		DidSwitchTabs(tab);
		if (tab == Tab.leaderboard)
		{
		}
	}
}
