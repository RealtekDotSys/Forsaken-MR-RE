public class PlushSuitSelectorCategoryCellDisplay : IxItemCellDisplay<SkinCategoryData>
{
	private sealed class _003C_003Ec__DisplayClass17_0
	{
		public string id;

		public PlushSuitSelectorCategoryCellDisplay _003C_003E4__this;

		public global::System.Action<global::UnityEngine.Sprite> _003C_003E9__1;

		internal void _003CSetIcon_003Eb__0(IconLookup iconLookup)
		{
			iconLookup.GetIcon(IconGroup.PlushSuit, id, _003C_003E9__1);
		}

		internal void _003CSetIcon_003Eb__1(global::UnityEngine.Sprite sprite)
		{
			_003C_003E4__this._iconImage.sprite = sprite;
		}
	}

	private global::UnityEngine.UI.Image _iconImage;

	private global::UnityEngine.UI.Image _selectedImage;

	private global::UnityEngine.UI.Image _skinIcon;

	private global::UnityEngine.UI.Image _lockedImage;

	private global::UnityEngine.UI.Button _button;

	private global::TMPro.TextMeshProUGUI _nameText;

	private global::TMPro.TextMeshProUGUI _skinNumText;

	private global::TMPro.TextMeshProUGUI _lockedLevelText;

	private int _currentIndex;

	private int _numOwned;

	private IPlushSuitSelectionRules _rules;

	public global::System.Action<PlushSuitSelectorCategoryCellDisplay> CategoryCellSelected;

	protected override void PopulateComponents()
	{
		global::System.Type[] onlyCacheTypes = new global::System.Type[3]
		{
			typeof(global::UnityEngine.UI.Image),
			typeof(global::UnityEngine.UI.Button),
			typeof(global::TMPro.TextMeshProUGUI)
		};
		_components.CacheComponents(_root, onlyCacheTypes);
		_iconImage = _components.TryGetComponent<global::UnityEngine.UI.Image>("SuitImage");
		_selectedImage = _components.TryGetComponent<global::UnityEngine.UI.Image>("SelectedStroke");
		_skinIcon = _components.TryGetComponent<global::UnityEngine.UI.Image>("SkinIcon");
		_button = _components.TryGetComponent<global::UnityEngine.UI.Button>("SkinButton");
		_nameText = _components.TryGetComponent<global::TMPro.TextMeshProUGUI>("NameText");
		_skinNumText = _components.TryGetComponent<global::TMPro.TextMeshProUGUI>("SkinNumText");
		_lockedImage = _components.TryGetComponent<global::UnityEngine.UI.Image>("LockedPlushsuitIcon");
		_lockedLevelText = _components.TryGetComponent<global::TMPro.TextMeshProUGUI>("LockedLevelText");
		_button.onClick.AddListener(OnButtonClicked);
	}

	public override void UpdateData()
	{
	}

	public void InitializeContents(IPlushSuitSelectionRules rules, global::System.Action<PlushSuitSelectorCategoryCellDisplay> callback)
	{
		_rules = rules;
		CategoryCellSelected = callback;
		_currentIndex = -1;
		InitializeOwnershipData();
		SetDisplayData();
	}

	private void InitializeOwnershipData()
	{
		foreach (OwnedPlushSuitData datum in _dataItem.Data)
		{
			if (datum.Owned && _rules.IsValid(datum.Data.Id))
			{
				_numOwned++;
				if (_currentIndex == -1)
				{
					_currentIndex = _dataItem.Data.IndexOf(datum);
				}
			}
		}
		if (_currentIndex == -1)
		{
			_currentIndex = 0;
		}
	}

	private void SetDisplayData()
	{
		SetIcon();
		_skinNumText.text = _numOwned.ToString();
		bool flag = _dataItem.Data[_currentIndex].Owned && _rules.IsValid(_dataItem.Data[_currentIndex].Data.Id);
		_skinIcon.gameObject.SetActive(flag);
		OwnedPlushSuitData ownedPlushSuitData = _dataItem.Data[_currentIndex];
		_nameText.text = MasterDomain.GetDomain().LocalizationDomain.Localization.GetLocalizedString(ownedPlushSuitData.Data.AnimatronicName, ownedPlushSuitData.Data.AnimatronicName);
		if (ownedPlushSuitData.Data.UnavailableDisplay == PlushSuitData.UnavailableDisplayType.Hide)
		{
			_root.SetActive(value: false);
			_lockedImage.gameObject.SetActive(flag);
			_lockedLevelText.gameObject.SetActive(value: false);
		}
		else if (ownedPlushSuitData.Data.UnavailableDisplay == PlushSuitData.UnavailableDisplayType.Silhouette)
		{
			_lockedImage.gameObject.SetActive(value: false);
			_lockedLevelText.gameObject.SetActive(value: false);
		}
		else if (ownedPlushSuitData.Data.UnavailableDisplay == PlushSuitData.UnavailableDisplayType.UnlockLevel)
		{
			_lockedImage.gameObject.SetActive(!flag);
			_lockedLevelText.gameObject.SetActive(!flag);
			_lockedLevelText.text = $"{ownedPlushSuitData.LockedLevelText} {ownedPlushSuitData.Data.UnlockedLevel}";
		}
	}

	private void SetIcon()
	{
		global::UnityEngine.Debug.Log("IS SUIT " + _dataItem.Data[_currentIndex].Data.Id + " ON A DIFFERENT SLOT? - " + !_rules.IsValid(_dataItem.Data[_currentIndex].Data.Id));
		PlushSuitSelectorCategoryCellDisplay._003C_003Ec__DisplayClass17_0 _003C_003Ec__DisplayClass17_ = new PlushSuitSelectorCategoryCellDisplay._003C_003Ec__DisplayClass17_0();
		_003C_003Ec__DisplayClass17_._003C_003E4__this = this;
		_003C_003Ec__DisplayClass17_._003C_003E9__1 = _003C_003Ec__DisplayClass17_._003CSetIcon_003Eb__1;
		if (_dataItem.Data[_currentIndex].Owned && _rules.IsValid(_dataItem.Data[_currentIndex].Data.Id))
		{
			_003C_003Ec__DisplayClass17_.id = _dataItem.Data[_currentIndex].Data.PlushSuitIconName;
		}
		else
		{
			_003C_003Ec__DisplayClass17_.id = _dataItem.Data[_currentIndex].Data.PlushSuitSilhouetteIcon;
		}
		MasterDomain.GetDomain().GameAssetManagementDomain.IconLookupAccess.GetInterfaceAsync(_003C_003Ec__DisplayClass17_._003CSetIcon_003Eb__0);
		_button.interactable = _dataItem.Data[_currentIndex].Owned && _rules.IsValid(_dataItem.Data[_currentIndex].Data.Id);
	}

	public void SetSelected(bool selected)
	{
		_selectedImage.gameObject.SetActive(selected);
	}

	public void OnButtonClicked()
	{
		if (CategoryCellSelected != null)
		{
			CategoryCellSelected(this);
		}
	}

	public string GetAnimatronicId()
	{
		if (_dataItem.Data[_currentIndex].Owned && _currentIndex < _dataItem.Data.Count)
		{
			return _dataItem.Data[_currentIndex].Data.Id;
		}
		return "";
	}

	public SkinCategoryData GetData()
	{
		return _dataItem;
	}

	public int GetNumOwned()
	{
		return _numOwned;
	}

	public int GetCurrentIndex()
	{
		return _currentIndex;
	}

	public void UpdateCategoryIcon(string skinId)
	{
		foreach (OwnedPlushSuitData datum in _dataItem.Data)
		{
			if (datum.Data.Id == skinId)
			{
				_currentIndex = _dataItem.Data.IndexOf(datum);
				SetDisplayData();
				break;
			}
		}
	}

	public override void TearDown()
	{
		global::UnityEngine.Debug.LogError("category item cell torn down!");
		CategoryCellSelected = null;
		base.TearDown();
	}

	public PlushSuitSelectorCategoryCellDisplay()
	{
		_currentIndex = 0;
	}
}
