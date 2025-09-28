namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IPinEntryService))]
	public class PinEntryServiceImpl : global::SRF.Service.SRServiceBase<global::SRDebugger.Services.IPinEntryService>, global::SRDebugger.Services.IPinEntryService
	{
		private global::SRDebugger.Services.PinEntryCompleteCallback _callback;

		private bool _isVisible;

		private global::SRDebugger.UI.Controls.PinEntryControl _pinControl;

		private global::System.Collections.Generic.List<int> _requiredPin = new global::System.Collections.Generic.List<int>(4);

		public bool IsShowingKeypad => _isVisible;

		public void ShowPinEntry(global::System.Collections.Generic.IList<int> requiredPin, string message, global::SRDebugger.Services.PinEntryCompleteCallback callback, bool allowCancel = true)
		{
			if (_isVisible)
			{
				throw new global::System.InvalidOperationException("Pin entry is already in progress");
			}
			VerifyPin(requiredPin);
			if (_pinControl == null)
			{
				Load();
			}
			if (_pinControl == null)
			{
				global::UnityEngine.Debug.LogWarning("[PinEntry] Pin entry failed loading, executing callback with fail result");
				callback(validPinEntered: false);
				return;
			}
			_pinControl.Clear();
			_pinControl.PromptText.text = message;
			_pinControl.CanCancel = allowCancel;
			_callback = callback;
			_requiredPin.Clear();
			_requiredPin.AddRange(requiredPin);
			_pinControl.Show();
			_isVisible = true;
			global::SRDebugger.Internal.SRDebuggerUtil.EnsureEventSystemExists();
		}

		[global::System.Obsolete]
		public void ShowPinEntry(global::System.Collections.Generic.IList<int> requiredPin, string message, global::SRDebugger.Services.PinEntryCompleteCallback callback, bool blockInput, bool allowCancel)
		{
			ShowPinEntry(requiredPin, message, callback, allowCancel);
		}

		protected override void Awake()
		{
			base.Awake();
			base.CachedTransform.SetParent(global::SRF.Hierarchy.Get("SRDebugger"));
		}

		private void Load()
		{
			global::SRDebugger.UI.Controls.PinEntryControl pinEntryControl = global::UnityEngine.Resources.Load<global::SRDebugger.UI.Controls.PinEntryControl>("SRDebugger/UI/Prefabs/PinEntry");
			if (pinEntryControl == null)
			{
				global::UnityEngine.Debug.LogError("[PinEntry] Unable to load pin entry prefab");
				return;
			}
			_pinControl = SRInstantiate.Instantiate(pinEntryControl);
			_pinControl.CachedTransform.SetParent(base.CachedTransform, worldPositionStays: false);
			_pinControl.Hide();
			_pinControl.Complete += PinControlOnComplete;
		}

		private void PinControlOnComplete(global::System.Collections.Generic.IList<int> result, bool didCancel)
		{
			bool flag = global::System.Linq.Enumerable.SequenceEqual(_requiredPin, result);
			if (!didCancel && !flag)
			{
				_pinControl.Clear();
				_pinControl.PlayInvalidCodeAnimation();
				return;
			}
			_isVisible = false;
			_pinControl.Hide();
			if (didCancel)
			{
				_callback(validPinEntered: false);
			}
			else
			{
				_callback(flag);
			}
		}

		private void VerifyPin(global::System.Collections.Generic.IList<int> pin)
		{
			if (pin.Count != 4)
			{
				throw new global::System.ArgumentException("Pin list must have 4 elements");
			}
			for (int i = 0; i < pin.Count; i++)
			{
				if (pin[i] < 0 || pin[i] > 9)
				{
					throw new global::System.ArgumentException("Pin numbers must be >= 0 && <= 9");
				}
			}
		}
	}
}
