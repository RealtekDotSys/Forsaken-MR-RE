namespace Discord
{
	public class OverlayManager
	{
		internal struct FFIEvents
		{
			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void ToggleHandler(global::System.IntPtr ptr, bool locked);

			internal global::Discord.OverlayManager.FFIEvents.ToggleHandler OnToggle;
		}

		internal struct FFIMethods
		{
			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void IsEnabledMethod(global::System.IntPtr methodsPtr, ref bool enabled);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void IsLockedMethod(global::System.IntPtr methodsPtr, ref bool locked);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void SetLockedCallback(global::System.IntPtr ptr, global::Discord.Result result);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void SetLockedMethod(global::System.IntPtr methodsPtr, bool locked, global::System.IntPtr callbackData, global::Discord.OverlayManager.FFIMethods.SetLockedCallback callback);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void OpenActivityInviteCallback(global::System.IntPtr ptr, global::Discord.Result result);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void OpenActivityInviteMethod(global::System.IntPtr methodsPtr, global::Discord.ActivityActionType type, global::System.IntPtr callbackData, global::Discord.OverlayManager.FFIMethods.OpenActivityInviteCallback callback);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void OpenGuildInviteCallback(global::System.IntPtr ptr, global::Discord.Result result);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void OpenGuildInviteMethod(global::System.IntPtr methodsPtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.LPStr)] string code, global::System.IntPtr callbackData, global::Discord.OverlayManager.FFIMethods.OpenGuildInviteCallback callback);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void OpenVoiceSettingsCallback(global::System.IntPtr ptr, global::Discord.Result result);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void OpenVoiceSettingsMethod(global::System.IntPtr methodsPtr, global::System.IntPtr callbackData, global::Discord.OverlayManager.FFIMethods.OpenVoiceSettingsCallback callback);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate global::Discord.Result InitDrawingDxgiMethod(global::System.IntPtr methodsPtr, global::System.IntPtr swapchain, bool useMessageForwarding);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void OnPresentMethod(global::System.IntPtr methodsPtr);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void ForwardMessageMethod(global::System.IntPtr methodsPtr, global::System.IntPtr message);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void KeyEventMethod(global::System.IntPtr methodsPtr, bool down, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.LPStr)] string keyCode, global::Discord.KeyVariant variant);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void CharEventMethod(global::System.IntPtr methodsPtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.LPStr)] string character);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void MouseButtonEventMethod(global::System.IntPtr methodsPtr, byte down, int clickCount, global::Discord.MouseButton which, int x, int y);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void MouseMotionEventMethod(global::System.IntPtr methodsPtr, int x, int y);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void ImeCommitTextMethod(global::System.IntPtr methodsPtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.LPStr)] string text);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void ImeSetCompositionMethod(global::System.IntPtr methodsPtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.LPStr)] string text, ref global::Discord.ImeUnderline underlines, int from, int to);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void ImeCancelCompositionMethod(global::System.IntPtr methodsPtr);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void SetImeCompositionRangeCallbackCallback(global::System.IntPtr ptr, int from, int to, ref global::Discord.Rect bounds);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void SetImeCompositionRangeCallbackMethod(global::System.IntPtr methodsPtr, global::System.IntPtr callbackData, global::Discord.OverlayManager.FFIMethods.SetImeCompositionRangeCallbackCallback callback);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void SetImeSelectionBoundsCallbackCallback(global::System.IntPtr ptr, global::Discord.Rect anchor, global::Discord.Rect focus, bool isAnchorFirst);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate void SetImeSelectionBoundsCallbackMethod(global::System.IntPtr methodsPtr, global::System.IntPtr callbackData, global::Discord.OverlayManager.FFIMethods.SetImeSelectionBoundsCallbackCallback callback);

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Winapi)]
			internal delegate bool IsPointInsideClickZoneMethod(global::System.IntPtr methodsPtr, int x, int y);

			internal global::Discord.OverlayManager.FFIMethods.IsEnabledMethod IsEnabled;

			internal global::Discord.OverlayManager.FFIMethods.IsLockedMethod IsLocked;

			internal global::Discord.OverlayManager.FFIMethods.SetLockedMethod SetLocked;

			internal global::Discord.OverlayManager.FFIMethods.OpenActivityInviteMethod OpenActivityInvite;

			internal global::Discord.OverlayManager.FFIMethods.OpenGuildInviteMethod OpenGuildInvite;

			internal global::Discord.OverlayManager.FFIMethods.OpenVoiceSettingsMethod OpenVoiceSettings;

			internal global::Discord.OverlayManager.FFIMethods.InitDrawingDxgiMethod InitDrawingDxgi;

			internal global::Discord.OverlayManager.FFIMethods.OnPresentMethod OnPresent;

			internal global::Discord.OverlayManager.FFIMethods.ForwardMessageMethod ForwardMessage;

			internal global::Discord.OverlayManager.FFIMethods.KeyEventMethod KeyEvent;

			internal global::Discord.OverlayManager.FFIMethods.CharEventMethod CharEvent;

			internal global::Discord.OverlayManager.FFIMethods.MouseButtonEventMethod MouseButtonEvent;

			internal global::Discord.OverlayManager.FFIMethods.MouseMotionEventMethod MouseMotionEvent;

			internal global::Discord.OverlayManager.FFIMethods.ImeCommitTextMethod ImeCommitText;

			internal global::Discord.OverlayManager.FFIMethods.ImeSetCompositionMethod ImeSetComposition;

			internal global::Discord.OverlayManager.FFIMethods.ImeCancelCompositionMethod ImeCancelComposition;

			internal global::Discord.OverlayManager.FFIMethods.SetImeCompositionRangeCallbackMethod SetImeCompositionRangeCallback;

			internal global::Discord.OverlayManager.FFIMethods.SetImeSelectionBoundsCallbackMethod SetImeSelectionBoundsCallback;

			internal global::Discord.OverlayManager.FFIMethods.IsPointInsideClickZoneMethod IsPointInsideClickZone;
		}

		public delegate void SetLockedHandler(global::Discord.Result result);

		public delegate void OpenActivityInviteHandler(global::Discord.Result result);

		public delegate void OpenGuildInviteHandler(global::Discord.Result result);

		public delegate void OpenVoiceSettingsHandler(global::Discord.Result result);

		public delegate void SetImeCompositionRangeCallbackHandler(int from, int to, ref global::Discord.Rect bounds);

		public delegate void SetImeSelectionBoundsCallbackHandler(global::Discord.Rect anchor, global::Discord.Rect focus, bool isAnchorFirst);

		public delegate void ToggleHandler(bool locked);

		private global::System.IntPtr MethodsPtr;

		private object MethodsStructure;

		private global::Discord.OverlayManager.FFIMethods Methods
		{
			get
			{
				if (MethodsStructure == null)
				{
					MethodsStructure = global::System.Runtime.InteropServices.Marshal.PtrToStructure(MethodsPtr, typeof(global::Discord.OverlayManager.FFIMethods));
				}
				return (global::Discord.OverlayManager.FFIMethods)MethodsStructure;
			}
		}

		public event global::Discord.OverlayManager.ToggleHandler OnToggle;

		internal OverlayManager(global::System.IntPtr ptr, global::System.IntPtr eventsPtr, ref global::Discord.OverlayManager.FFIEvents events)
		{
			if (eventsPtr == global::System.IntPtr.Zero)
			{
				throw new global::Discord.ResultException(global::Discord.Result.InternalError);
			}
			InitEvents(eventsPtr, ref events);
			MethodsPtr = ptr;
			if (MethodsPtr == global::System.IntPtr.Zero)
			{
				throw new global::Discord.ResultException(global::Discord.Result.InternalError);
			}
		}

		private void InitEvents(global::System.IntPtr eventsPtr, ref global::Discord.OverlayManager.FFIEvents events)
		{
			events.OnToggle = OnToggleImpl;
			global::System.Runtime.InteropServices.Marshal.StructureToPtr(events, eventsPtr, fDeleteOld: false);
		}

		public bool IsEnabled()
		{
			bool enabled = false;
			Methods.IsEnabled(MethodsPtr, ref enabled);
			return enabled;
		}

		public bool IsLocked()
		{
			bool locked = false;
			Methods.IsLocked(MethodsPtr, ref locked);
			return locked;
		}

		[global::Discord.MonoPInvokeCallback]
		private static void SetLockedCallbackImpl(global::System.IntPtr ptr, global::Discord.Result result)
		{
			global::System.Runtime.InteropServices.GCHandle gCHandle = global::System.Runtime.InteropServices.GCHandle.FromIntPtr(ptr);
			global::Discord.OverlayManager.SetLockedHandler obj = (global::Discord.OverlayManager.SetLockedHandler)gCHandle.Target;
			gCHandle.Free();
			obj(result);
		}

		public void SetLocked(bool locked, global::Discord.OverlayManager.SetLockedHandler callback)
		{
			global::System.Runtime.InteropServices.GCHandle value = global::System.Runtime.InteropServices.GCHandle.Alloc(callback);
			Methods.SetLocked(MethodsPtr, locked, global::System.Runtime.InteropServices.GCHandle.ToIntPtr(value), SetLockedCallbackImpl);
		}

		[global::Discord.MonoPInvokeCallback]
		private static void OpenActivityInviteCallbackImpl(global::System.IntPtr ptr, global::Discord.Result result)
		{
			global::System.Runtime.InteropServices.GCHandle gCHandle = global::System.Runtime.InteropServices.GCHandle.FromIntPtr(ptr);
			global::Discord.OverlayManager.OpenActivityInviteHandler obj = (global::Discord.OverlayManager.OpenActivityInviteHandler)gCHandle.Target;
			gCHandle.Free();
			obj(result);
		}

		public void OpenActivityInvite(global::Discord.ActivityActionType type, global::Discord.OverlayManager.OpenActivityInviteHandler callback)
		{
			global::System.Runtime.InteropServices.GCHandle value = global::System.Runtime.InteropServices.GCHandle.Alloc(callback);
			Methods.OpenActivityInvite(MethodsPtr, type, global::System.Runtime.InteropServices.GCHandle.ToIntPtr(value), OpenActivityInviteCallbackImpl);
		}

		[global::Discord.MonoPInvokeCallback]
		private static void OpenGuildInviteCallbackImpl(global::System.IntPtr ptr, global::Discord.Result result)
		{
			global::System.Runtime.InteropServices.GCHandle gCHandle = global::System.Runtime.InteropServices.GCHandle.FromIntPtr(ptr);
			global::Discord.OverlayManager.OpenGuildInviteHandler obj = (global::Discord.OverlayManager.OpenGuildInviteHandler)gCHandle.Target;
			gCHandle.Free();
			obj(result);
		}

		public void OpenGuildInvite(string code, global::Discord.OverlayManager.OpenGuildInviteHandler callback)
		{
			global::System.Runtime.InteropServices.GCHandle value = global::System.Runtime.InteropServices.GCHandle.Alloc(callback);
			Methods.OpenGuildInvite(MethodsPtr, code, global::System.Runtime.InteropServices.GCHandle.ToIntPtr(value), OpenGuildInviteCallbackImpl);
		}

		[global::Discord.MonoPInvokeCallback]
		private static void OpenVoiceSettingsCallbackImpl(global::System.IntPtr ptr, global::Discord.Result result)
		{
			global::System.Runtime.InteropServices.GCHandle gCHandle = global::System.Runtime.InteropServices.GCHandle.FromIntPtr(ptr);
			global::Discord.OverlayManager.OpenVoiceSettingsHandler obj = (global::Discord.OverlayManager.OpenVoiceSettingsHandler)gCHandle.Target;
			gCHandle.Free();
			obj(result);
		}

		public void OpenVoiceSettings(global::Discord.OverlayManager.OpenVoiceSettingsHandler callback)
		{
			global::System.Runtime.InteropServices.GCHandle value = global::System.Runtime.InteropServices.GCHandle.Alloc(callback);
			Methods.OpenVoiceSettings(MethodsPtr, global::System.Runtime.InteropServices.GCHandle.ToIntPtr(value), OpenVoiceSettingsCallbackImpl);
		}

		public void InitDrawingDxgi(global::System.IntPtr swapchain, bool useMessageForwarding)
		{
			global::Discord.Result result = Methods.InitDrawingDxgi(MethodsPtr, swapchain, useMessageForwarding);
			if (result != global::Discord.Result.Ok)
			{
				throw new global::Discord.ResultException(result);
			}
		}

		public void OnPresent()
		{
			Methods.OnPresent(MethodsPtr);
		}

		public void ForwardMessage(global::System.IntPtr message)
		{
			Methods.ForwardMessage(MethodsPtr, message);
		}

		public void KeyEvent(bool down, string keyCode, global::Discord.KeyVariant variant)
		{
			Methods.KeyEvent(MethodsPtr, down, keyCode, variant);
		}

		public void CharEvent(string character)
		{
			Methods.CharEvent(MethodsPtr, character);
		}

		public void MouseButtonEvent(byte down, int clickCount, global::Discord.MouseButton which, int x, int y)
		{
			Methods.MouseButtonEvent(MethodsPtr, down, clickCount, which, x, y);
		}

		public void MouseMotionEvent(int x, int y)
		{
			Methods.MouseMotionEvent(MethodsPtr, x, y);
		}

		public void ImeCommitText(string text)
		{
			Methods.ImeCommitText(MethodsPtr, text);
		}

		public void ImeSetComposition(string text, global::Discord.ImeUnderline underlines, int from, int to)
		{
			Methods.ImeSetComposition(MethodsPtr, text, ref underlines, from, to);
		}

		public void ImeCancelComposition()
		{
			Methods.ImeCancelComposition(MethodsPtr);
		}

		[global::Discord.MonoPInvokeCallback]
		private static void SetImeCompositionRangeCallbackCallbackImpl(global::System.IntPtr ptr, int from, int to, ref global::Discord.Rect bounds)
		{
			global::System.Runtime.InteropServices.GCHandle gCHandle = global::System.Runtime.InteropServices.GCHandle.FromIntPtr(ptr);
			global::Discord.OverlayManager.SetImeCompositionRangeCallbackHandler obj = (global::Discord.OverlayManager.SetImeCompositionRangeCallbackHandler)gCHandle.Target;
			gCHandle.Free();
			obj(from, to, ref bounds);
		}

		public void SetImeCompositionRangeCallback(global::Discord.OverlayManager.SetImeCompositionRangeCallbackHandler callback)
		{
			global::System.Runtime.InteropServices.GCHandle value = global::System.Runtime.InteropServices.GCHandle.Alloc(callback);
			Methods.SetImeCompositionRangeCallback(MethodsPtr, global::System.Runtime.InteropServices.GCHandle.ToIntPtr(value), SetImeCompositionRangeCallbackCallbackImpl);
		}

		[global::Discord.MonoPInvokeCallback]
		private static void SetImeSelectionBoundsCallbackCallbackImpl(global::System.IntPtr ptr, global::Discord.Rect anchor, global::Discord.Rect focus, bool isAnchorFirst)
		{
			global::System.Runtime.InteropServices.GCHandle gCHandle = global::System.Runtime.InteropServices.GCHandle.FromIntPtr(ptr);
			global::Discord.OverlayManager.SetImeSelectionBoundsCallbackHandler obj = (global::Discord.OverlayManager.SetImeSelectionBoundsCallbackHandler)gCHandle.Target;
			gCHandle.Free();
			obj(anchor, focus, isAnchorFirst);
		}

		public void SetImeSelectionBoundsCallback(global::Discord.OverlayManager.SetImeSelectionBoundsCallbackHandler callback)
		{
			global::System.Runtime.InteropServices.GCHandle value = global::System.Runtime.InteropServices.GCHandle.Alloc(callback);
			Methods.SetImeSelectionBoundsCallback(MethodsPtr, global::System.Runtime.InteropServices.GCHandle.ToIntPtr(value), SetImeSelectionBoundsCallbackCallbackImpl);
		}

		public bool IsPointInsideClickZone(int x, int y)
		{
			return Methods.IsPointInsideClickZone(MethodsPtr, x, y);
		}

		[global::Discord.MonoPInvokeCallback]
		private static void OnToggleImpl(global::System.IntPtr ptr, bool locked)
		{
			global::Discord.Discord discord = (global::Discord.Discord)global::System.Runtime.InteropServices.GCHandle.FromIntPtr(ptr).Target;
			if (discord.OverlayManagerInstance.OnToggle != null)
			{
				discord.OverlayManagerInstance.OnToggle(locked);
			}
		}
	}
}
