public class SlotAssembleButton : global::UnityEngine.MonoBehaviour, ICellInterface<SlotAssembleButtonLoadData>
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button _button;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _imagesParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image _modImage;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image _plushImage;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image _cpuImage;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _lockedSlotParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _emptySlotParent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _activeSlotParent;

	[global::UnityEngine.SerializeField]
	private StarDisplay _starDisplay;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _lockedText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _emptyText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI _streakText;

	private SlotAssembleButtonLoadData _data;

	private SlotState _slotState;

	private EventExposer _eventExposer;

	private void ButtonClicked()
	{
		if (_eventExposer != null)
		{
			_eventExposer.OnWorkshopModifyAssemblyButtonPressed(new AssemblyButtonPressedPayload
			{
				ButtonType = _data.SlotType,
				Index = _data.Index,
				SlotState = _data.SlotState
			});
		}
	}

	private void UpdateDisplayState()
	{
		_lockedSlotParent.SetActive(_data.SlotState == SlotState.Locked);
		_emptySlotParent.SetActive(_data.SlotState == SlotState.Empty);
		_activeSlotParent.SetActive(_data.SlotState == SlotState.Active);
		_modImage.gameObject.SetActive(_data.SlotType == SlotDisplayButtonType.Mod);
		_plushImage.gameObject.SetActive(_data.SlotType == SlotDisplayButtonType.Plushsuit);
		_cpuImage.gameObject.SetActive(_data.SlotType == SlotDisplayButtonType.Cpu);
		_starDisplay.gameObject.SetActive(_data.NumStars > 0);
		_starDisplay.SetStars(_data.NumStars);
	}

	private void UpdateButtonText()
	{
		if (_streakText != null && _data.StreakText != null)
		{
			_streakText.text = _data.StreakText;
		}
		if (_emptyText != null && _data.EmptyText != null)
		{
			_emptyText.text = _data.EmptyText;
		}
		if (_lockedText != null && _data.LockedText != null)
		{
			_lockedText.text = _data.LockedText;
		}
	}

	public void SetData(SlotAssembleButtonLoadData data)
	{
		_data = data;
		UpdateButtonText();
		UpdateDisplayState();
	}

	public void SetSprite(global::UnityEngine.Sprite sprite)
	{
		_imagesParent.SetActive(sprite != null);
		_plushImage.overrideSprite = sprite;
		_cpuImage.overrideSprite = sprite;
		_modImage.overrideSprite = sprite;
	}

	private void Awake()
	{
		MasterDomain domain = MasterDomain.GetDomain();
		if (domain != null)
		{
			_eventExposer = domain.eventExposer;
			_button.onClick.RemoveListener(ButtonClicked);
			_button.onClick.AddListener(ButtonClicked);
		}
	}
}
