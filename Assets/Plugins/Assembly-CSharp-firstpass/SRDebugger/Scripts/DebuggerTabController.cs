namespace SRDebugger.Scripts
{
	public class DebuggerTabController : global::SRF.SRMonoBehaviourEx
	{
		private global::SRDebugger.UI.Other.SRTab _aboutTabInstance;

		private global::SRDebugger.DefaultTabs? _activeTab;

		private bool _hasStarted;

		public global::SRDebugger.UI.Other.SRTab AboutTab;

		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Other.SRTabController TabController;

		public global::SRDebugger.DefaultTabs? ActiveTab
		{
			get
			{
				string key = TabController.ActiveTab.Key;
				if (string.IsNullOrEmpty(key))
				{
					return null;
				}
				object obj = global::System.Enum.Parse(typeof(global::SRDebugger.DefaultTabs), key);
				if (!global::System.Enum.IsDefined(typeof(global::SRDebugger.DefaultTabs), obj))
				{
					return null;
				}
				return (global::SRDebugger.DefaultTabs)obj;
			}
		}

		protected override void Start()
		{
			base.Start();
			_hasStarted = true;
			global::SRDebugger.UI.Other.SRTab[] array = global::UnityEngine.Resources.LoadAll<global::SRDebugger.UI.Other.SRTab>("SRDebugger/UI/Prefabs/Tabs");
			string[] names = global::System.Enum.GetNames(typeof(global::SRDebugger.DefaultTabs));
			global::SRDebugger.UI.Other.SRTab[] array2 = array;
			foreach (global::SRDebugger.UI.Other.SRTab sRTab in array2)
			{
				if (sRTab.GetComponent(typeof(global::SRDebugger.UI.Other.IEnableTab)) is global::SRDebugger.UI.Other.IEnableTab { IsEnabled: false })
				{
					continue;
				}
				if (global::System.Linq.Enumerable.Contains(names, sRTab.Key))
				{
					object obj = global::System.Enum.Parse(typeof(global::SRDebugger.DefaultTabs), sRTab.Key);
					if (global::System.Enum.IsDefined(typeof(global::SRDebugger.DefaultTabs), obj) && global::SRDebugger.Settings.Instance.DisabledTabs.Contains((global::SRDebugger.DefaultTabs)obj))
					{
						continue;
					}
				}
				TabController.AddTab(SRInstantiate.Instantiate(sRTab));
			}
			if (AboutTab != null)
			{
				_aboutTabInstance = SRInstantiate.Instantiate(AboutTab);
				TabController.AddTab(_aboutTabInstance, visibleInSidebar: false);
			}
			global::SRDebugger.DefaultTabs tab = _activeTab ?? global::SRDebugger.Settings.Instance.DefaultTab;
			if (!OpenTab(tab))
			{
				TabController.ActiveTab = global::System.Linq.Enumerable.FirstOrDefault(TabController.Tabs);
			}
		}

		public bool OpenTab(global::SRDebugger.DefaultTabs tab)
		{
			if (!_hasStarted)
			{
				_activeTab = tab;
				return true;
			}
			string text = tab.ToString();
			foreach (global::SRDebugger.UI.Other.SRTab tab2 in TabController.Tabs)
			{
				if (tab2.Key == text)
				{
					TabController.ActiveTab = tab2;
					return true;
				}
			}
			return false;
		}

		public void ShowAboutTab()
		{
			if (_aboutTabInstance != null)
			{
				TabController.ActiveTab = _aboutTabInstance;
			}
		}
	}
}
