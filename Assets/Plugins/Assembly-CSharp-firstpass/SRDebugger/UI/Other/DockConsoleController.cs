namespace SRDebugger.UI.Other
{
	public class DockConsoleController : global::SRF.SRMonoBehaviourEx, global::UnityEngine.EventSystems.IPointerEnterHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerExitHandler
	{
		public const float NonFocusOpacity = 0.65f;

		private bool _isDirty;

		private bool _isDragging;

		private int _pointersOver;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject BottomHandle;

		[global::SRF.RequiredField]
		public global::UnityEngine.CanvasGroup CanvasGroup;

		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Controls.ConsoleLogControl Console;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject Dropdown;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Image DropdownToggleSprite;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text TextErrors;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text TextInfo;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text TextWarnings;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle ToggleErrors;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle ToggleInfo;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Toggle ToggleWarnings;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject TopBar;

		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject TopHandle;

		public bool IsVisible
		{
			get
			{
				return base.CachedGameObject.activeSelf;
			}
			set
			{
				base.CachedGameObject.SetActive(value);
			}
		}

		protected override void Start()
		{
			base.Start();
			global::SRDebugger.Internal.Service.Console.Updated += ConsoleOnUpdated;
			Refresh();
			RefreshAlpha();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (global::SRDebugger.Internal.Service.Console != null)
			{
				global::SRDebugger.Internal.Service.Console.Updated -= ConsoleOnUpdated;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			_pointersOver = 0;
			_isDragging = false;
			RefreshAlpha();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_pointersOver = 0;
		}

		protected override void Update()
		{
			base.Update();
			if (_isDirty)
			{
				Refresh();
			}
		}

		private void ConsoleOnUpdated(global::SRDebugger.Services.IConsoleService console)
		{
			_isDirty = true;
		}

		public void SetDropdownVisibility(bool visible)
		{
			Dropdown.SetActive(visible);
			DropdownToggleSprite.rectTransform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, visible ? 0f : 180f);
		}

		public void SetAlignmentMode(global::SRDebugger.ConsoleAlignment alignment)
		{
			switch (alignment)
			{
			case global::SRDebugger.ConsoleAlignment.Top:
				TopBar.transform.SetSiblingIndex(0);
				Dropdown.transform.SetSiblingIndex(2);
				TopHandle.SetActive(value: false);
				BottomHandle.SetActive(value: true);
				base.transform.SetSiblingIndex(0);
				DropdownToggleSprite.rectTransform.parent.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 0f);
				break;
			case global::SRDebugger.ConsoleAlignment.Bottom:
				Dropdown.transform.SetSiblingIndex(0);
				TopBar.transform.SetSiblingIndex(2);
				TopHandle.SetActive(value: true);
				BottomHandle.SetActive(value: false);
				base.transform.SetSiblingIndex(1);
				DropdownToggleSprite.rectTransform.parent.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 180f);
				break;
			}
		}

		private void Refresh()
		{
			TextInfo.text = global::SRDebugger.Internal.SRDebuggerUtil.GetNumberString(global::SRDebugger.Internal.Service.Console.InfoCount, 999, "999+");
			TextWarnings.text = global::SRDebugger.Internal.SRDebuggerUtil.GetNumberString(global::SRDebugger.Internal.Service.Console.WarningCount, 999, "999+");
			TextErrors.text = global::SRDebugger.Internal.SRDebuggerUtil.GetNumberString(global::SRDebugger.Internal.Service.Console.ErrorCount, 999, "999+");
			_isDirty = false;
		}

		private void RefreshAlpha()
		{
			if (_isDragging || _pointersOver > 0)
			{
				CanvasGroup.alpha = 1f;
			}
			else
			{
				CanvasGroup.alpha = 0.65f;
			}
		}

		public void ToggleDropdownVisible()
		{
			SetDropdownVisibility(!Dropdown.activeSelf);
		}

		public void MenuButtonPressed()
		{
			SRDebug.Instance.ShowDebugPanel(global::SRDebugger.DefaultTabs.Console);
		}

		public void ClearButtonPressed()
		{
			global::SRDebugger.Internal.Service.Console.Clear();
		}

		public void TogglesUpdated()
		{
			Console.ShowErrors = ToggleErrors.isOn;
			Console.ShowWarnings = ToggleWarnings.isOn;
			Console.ShowInfo = ToggleInfo.isOn;
			SetDropdownVisibility(visible: true);
		}

		public void OnPointerEnter(global::UnityEngine.EventSystems.PointerEventData e)
		{
			_pointersOver = 1;
			RefreshAlpha();
		}

		public void OnPointerExit(global::UnityEngine.EventSystems.PointerEventData e)
		{
			_pointersOver = 0;
			RefreshAlpha();
		}

		public void OnBeginDrag()
		{
			_isDragging = true;
			RefreshAlpha();
		}

		public void OnEndDrag()
		{
			_isDragging = false;
			_pointersOver = 0;
			RefreshAlpha();
		}
	}
}
