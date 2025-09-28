namespace SRDebugger.UI.Other
{
	public class HandleManager : global::SRF.SRMonoBehaviour
	{
		private bool _hasSet;

		public global::UnityEngine.GameObject BottomHandle;

		public global::UnityEngine.GameObject BottomLeftHandle;

		public global::UnityEngine.GameObject BottomRightHandle;

		public global::SRDebugger.PinAlignment DefaultAlignment;

		public global::UnityEngine.GameObject LeftHandle;

		public global::UnityEngine.GameObject RightHandle;

		public global::UnityEngine.GameObject TopHandle;

		public global::UnityEngine.GameObject TopLeftHandle;

		public global::UnityEngine.GameObject TopRightHandle;

		private void Start()
		{
			if (!_hasSet)
			{
				SetAlignment(DefaultAlignment);
			}
		}

		public void SetAlignment(global::SRDebugger.PinAlignment alignment)
		{
			_hasSet = true;
			switch (alignment)
			{
			case global::SRDebugger.PinAlignment.TopLeft:
			case global::SRDebugger.PinAlignment.TopRight:
				SetActive(BottomHandle, active: true);
				SetActive(TopHandle, active: false);
				SetActive(TopLeftHandle, active: false);
				SetActive(TopRightHandle, active: false);
				break;
			case global::SRDebugger.PinAlignment.BottomLeft:
			case global::SRDebugger.PinAlignment.BottomRight:
				SetActive(BottomHandle, active: false);
				SetActive(TopHandle, active: true);
				SetActive(BottomLeftHandle, active: false);
				SetActive(BottomRightHandle, active: false);
				break;
			}
			switch (alignment)
			{
			case global::SRDebugger.PinAlignment.TopLeft:
			case global::SRDebugger.PinAlignment.BottomLeft:
				SetActive(LeftHandle, active: false);
				SetActive(RightHandle, active: true);
				SetActive(TopLeftHandle, active: false);
				SetActive(BottomLeftHandle, active: false);
				break;
			case global::SRDebugger.PinAlignment.TopRight:
			case global::SRDebugger.PinAlignment.BottomRight:
				SetActive(LeftHandle, active: true);
				SetActive(RightHandle, active: false);
				SetActive(TopRightHandle, active: false);
				SetActive(BottomRightHandle, active: false);
				break;
			}
			switch (alignment)
			{
			case global::SRDebugger.PinAlignment.TopLeft:
				SetActive(BottomLeftHandle, active: false);
				SetActive(BottomRightHandle, active: true);
				break;
			case global::SRDebugger.PinAlignment.TopRight:
				SetActive(BottomLeftHandle, active: true);
				SetActive(BottomRightHandle, active: false);
				break;
			case global::SRDebugger.PinAlignment.BottomLeft:
				SetActive(TopLeftHandle, active: false);
				SetActive(TopRightHandle, active: true);
				break;
			case global::SRDebugger.PinAlignment.BottomRight:
				SetActive(TopLeftHandle, active: true);
				SetActive(TopRightHandle, active: false);
				break;
			}
		}

		private void SetActive(global::UnityEngine.GameObject obj, bool active)
		{
			if (!(obj == null))
			{
				obj.SetActive(active);
			}
		}
	}
}
