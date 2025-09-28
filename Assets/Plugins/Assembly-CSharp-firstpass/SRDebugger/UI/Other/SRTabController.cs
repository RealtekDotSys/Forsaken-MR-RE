namespace SRDebugger.UI.Other
{
	public class SRTabController : global::SRF.SRMonoBehaviourEx
	{
		private readonly global::SRF.SRList<global::SRDebugger.UI.Other.SRTab> _tabs = new global::SRF.SRList<global::SRDebugger.UI.Other.SRTab>();

		private global::SRDebugger.UI.Other.SRTab _activeTab;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform TabButtonContainer;

		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Controls.SRTabButton TabButtonPrefab;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform TabContentsContainer;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform TabHeaderContentContainer;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text TabHeaderText;

		public global::SRDebugger.UI.Other.SRTab ActiveTab
		{
			get
			{
				return _activeTab;
			}
			set
			{
				MakeActive(value);
			}
		}

		public global::System.Collections.Generic.IList<global::SRDebugger.UI.Other.SRTab> Tabs => _tabs.AsReadOnly();

		public event global::System.Action<global::SRDebugger.UI.Other.SRTabController, global::SRDebugger.UI.Other.SRTab> ActiveTabChanged;

		public void AddTab(global::SRDebugger.UI.Other.SRTab tab, bool visibleInSidebar = true)
		{
			tab.CachedTransform.SetParent(TabContentsContainer, worldPositionStays: false);
			tab.CachedGameObject.SetActive(value: false);
			if (visibleInSidebar)
			{
				global::SRDebugger.UI.Controls.SRTabButton sRTabButton = SRInstantiate.Instantiate(TabButtonPrefab);
				sRTabButton.CachedTransform.SetParent(TabButtonContainer, worldPositionStays: false);
				sRTabButton.TitleText.text = tab.Title.ToUpper();
				if (tab.IconExtraContent != null)
				{
					SRInstantiate.Instantiate(tab.IconExtraContent).SetParent(sRTabButton.ExtraContentContainer, worldPositionStays: false);
				}
				sRTabButton.IconStyleComponent.StyleKey = tab.IconStyleKey;
				sRTabButton.IsActive = false;
				sRTabButton.Button.onClick.AddListener(delegate
				{
					MakeActive(tab);
				});
				tab.TabButton = sRTabButton;
			}
			_tabs.Add(tab);
			SortTabs();
			if (_tabs.Count == 1)
			{
				ActiveTab = tab;
			}
		}

		private void MakeActive(global::SRDebugger.UI.Other.SRTab tab)
		{
			if (!_tabs.Contains(tab))
			{
				throw new global::System.ArgumentException("tab is not a member of this tab controller", "tab");
			}
			if (_activeTab != null)
			{
				_activeTab.CachedGameObject.SetActive(value: false);
				if (_activeTab.TabButton != null)
				{
					_activeTab.TabButton.IsActive = false;
				}
				if (_activeTab.HeaderExtraContent != null)
				{
					_activeTab.HeaderExtraContent.gameObject.SetActive(value: false);
				}
			}
			_activeTab = tab;
			if (_activeTab != null)
			{
				_activeTab.CachedGameObject.SetActive(value: true);
				TabHeaderText.text = _activeTab.LongTitle;
				if (_activeTab.TabButton != null)
				{
					_activeTab.TabButton.IsActive = true;
				}
				if (_activeTab.HeaderExtraContent != null)
				{
					_activeTab.HeaderExtraContent.SetParent(TabHeaderContentContainer, worldPositionStays: false);
					_activeTab.HeaderExtraContent.gameObject.SetActive(value: true);
				}
			}
			if (this.ActiveTabChanged != null)
			{
				this.ActiveTabChanged(this, _activeTab);
			}
		}

		private void SortTabs()
		{
			_tabs.Sort((global::SRDebugger.UI.Other.SRTab t1, global::SRDebugger.UI.Other.SRTab t2) => t1.SortIndex.CompareTo(t2.SortIndex));
			for (int num = 0; num < _tabs.Count; num++)
			{
				if (_tabs[num].TabButton != null)
				{
					_tabs[num].TabButton.CachedTransform.SetSiblingIndex(num);
				}
			}
		}
	}
}
