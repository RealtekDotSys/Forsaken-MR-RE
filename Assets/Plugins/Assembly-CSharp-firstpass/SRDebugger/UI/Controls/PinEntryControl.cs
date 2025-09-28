namespace SRDebugger.UI.Controls
{
	public class PinEntryControl : global::SRF.SRMonoBehaviourEx
	{
		private bool _isVisible = true;

		private global::System.Collections.Generic.List<int> _numbers = new global::System.Collections.Generic.List<int>(4);

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Image Background;

		public bool CanCancel = true;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Button CancelButton;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text CancelButtonText;

		[global::SRF.RequiredField]
		public global::UnityEngine.CanvasGroup CanvasGroup;

		[global::SRF.RequiredField]
		public global::UnityEngine.Animator DotAnimator;

		public global::UnityEngine.UI.Button[] NumberButtons;

		public global::UnityEngine.UI.Toggle[] NumberDots;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text PromptText;

		public event global::SRDebugger.UI.Controls.PinEntryControlCallback Complete;

		protected override void Awake()
		{
			base.Awake();
			for (int i = 0; i < NumberButtons.Length; i++)
			{
				int number = i;
				NumberButtons[i].onClick.AddListener(delegate
				{
					PushNumber(number);
				});
			}
			CancelButton.onClick.AddListener(CancelButtonPressed);
			RefreshState();
		}

		protected override void Update()
		{
			base.Update();
			if (!_isVisible)
			{
				return;
			}
			if (_numbers.Count > 0 && (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Backspace) || global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Delete)))
			{
				global::SRF.SRFIListExtensions.PopLast(_numbers);
				RefreshState();
			}
			string inputString = global::UnityEngine.Input.inputString;
			for (int i = 0; i < inputString.Length; i++)
			{
				if (char.IsNumber(inputString, i))
				{
					int num = (int)char.GetNumericValue(inputString, i);
					if (num <= 9 && num >= 0)
					{
						PushNumber(num);
					}
				}
			}
		}

		public void Show()
		{
			CanvasGroup.alpha = 1f;
			global::UnityEngine.CanvasGroup canvasGroup = CanvasGroup;
			bool blocksRaycasts = (CanvasGroup.interactable = true);
			canvasGroup.blocksRaycasts = blocksRaycasts;
			_isVisible = true;
		}

		public void Hide()
		{
			CanvasGroup.alpha = 0f;
			global::UnityEngine.CanvasGroup canvasGroup = CanvasGroup;
			bool blocksRaycasts = (CanvasGroup.interactable = false);
			canvasGroup.blocksRaycasts = blocksRaycasts;
			_isVisible = false;
		}

		public void Clear()
		{
			_numbers.Clear();
			RefreshState();
		}

		public void PlayInvalidCodeAnimation()
		{
			DotAnimator.SetTrigger("Invalid");
		}

		protected void OnComplete()
		{
			if (this.Complete != null)
			{
				this.Complete(new global::System.Collections.ObjectModel.ReadOnlyCollection<int>(_numbers), didCancel: false);
			}
		}

		protected void OnCancel()
		{
			if (this.Complete != null)
			{
				this.Complete(new int[0], didCancel: true);
			}
		}

		private void CancelButtonPressed()
		{
			if (_numbers.Count > 0)
			{
				global::SRF.SRFIListExtensions.PopLast(_numbers);
			}
			else
			{
				OnCancel();
			}
			RefreshState();
		}

		public void PushNumber(int number)
		{
			if (_numbers.Count >= 4)
			{
				global::UnityEngine.Debug.LogWarning("[PinEntry] Expected 4 numbers");
				return;
			}
			_numbers.Add(number);
			if (_numbers.Count >= 4)
			{
				OnComplete();
			}
			RefreshState();
		}

		private void RefreshState()
		{
			for (int i = 0; i < NumberDots.Length; i++)
			{
				NumberDots[i].isOn = i < _numbers.Count;
			}
			if (_numbers.Count > 0)
			{
				CancelButtonText.text = "Delete";
			}
			else
			{
				CancelButtonText.text = (CanCancel ? "Cancel" : "");
			}
		}
	}
}
