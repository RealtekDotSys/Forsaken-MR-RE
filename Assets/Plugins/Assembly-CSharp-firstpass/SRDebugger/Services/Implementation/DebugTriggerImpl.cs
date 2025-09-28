namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IDebugTriggerService))]
	public class DebugTriggerImpl : global::SRF.Service.SRServiceBase<global::SRDebugger.Services.IDebugTriggerService>, global::SRDebugger.Services.IDebugTriggerService
	{
		private global::SRDebugger.PinAlignment _position;

		private global::SRDebugger.UI.Other.TriggerRoot _trigger;

		public bool IsEnabled
		{
			get
			{
				if (_trigger != null)
				{
					return _trigger.CachedGameObject.activeSelf;
				}
				return false;
			}
			set
			{
				if (value && _trigger == null)
				{
					CreateTrigger();
				}
				if (_trigger != null)
				{
					_trigger.CachedGameObject.SetActive(value);
				}
			}
		}

		public global::SRDebugger.PinAlignment Position
		{
			get
			{
				return _position;
			}
			set
			{
				if (_trigger != null)
				{
					SetTriggerPosition(_trigger.TriggerTransform, value);
				}
				_position = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			global::UnityEngine.Object.DontDestroyOnLoad(base.CachedGameObject);
			base.CachedTransform.SetParent(global::SRF.Hierarchy.Get("SRDebugger"), worldPositionStays: true);
			base.name = "Trigger";
		}

		private void CreateTrigger()
		{
			global::SRDebugger.UI.Other.TriggerRoot triggerRoot = global::UnityEngine.Resources.Load<global::SRDebugger.UI.Other.TriggerRoot>("SRDebugger/UI/Prefabs/Trigger");
			if (triggerRoot == null)
			{
				global::UnityEngine.Debug.LogError("[SRDebugger] Error loading trigger prefab");
				return;
			}
			_trigger = SRInstantiate.Instantiate(triggerRoot);
			_trigger.CachedTransform.SetParent(base.CachedTransform, worldPositionStays: true);
			SetTriggerPosition(_trigger.TriggerTransform, _position);
			switch (global::SRDebugger.Settings.Instance.TriggerBehaviour)
			{
			case global::SRDebugger.Settings.TriggerBehaviours.TripleTap:
				_trigger.TripleTapButton.onClick.AddListener(OnTriggerButtonClick);
				_trigger.TapHoldButton.gameObject.SetActive(value: false);
				break;
			case global::SRDebugger.Settings.TriggerBehaviours.TapAndHold:
				_trigger.TapHoldButton.onLongPress.AddListener(OnTriggerButtonClick);
				_trigger.TripleTapButton.gameObject.SetActive(value: false);
				break;
			case global::SRDebugger.Settings.TriggerBehaviours.DoubleTap:
				_trigger.TripleTapButton.RequiredTapCount = 2;
				_trigger.TripleTapButton.onClick.AddListener(OnTriggerButtonClick);
				_trigger.TapHoldButton.gameObject.SetActive(value: false);
				break;
			default:
				throw new global::System.Exception("Unhandled TriggerBehaviour");
			}
			global::SRDebugger.Internal.SRDebuggerUtil.EnsureEventSystemExists();
			global::UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnActiveSceneChanged;
		}

		protected override void OnDestroy()
		{
			global::UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= OnActiveSceneChanged;
			base.OnDestroy();
		}

		private static void OnActiveSceneChanged(global::UnityEngine.SceneManagement.Scene s1, global::UnityEngine.SceneManagement.Scene s2)
		{
			global::SRDebugger.Internal.SRDebuggerUtil.EnsureEventSystemExists();
		}

		private void OnTriggerButtonClick()
		{
			SRDebug.Instance.ShowDebugPanel();
		}

		private static void SetTriggerPosition(global::UnityEngine.RectTransform t, global::SRDebugger.PinAlignment position)
		{
			float x = 0f;
			float y = 0f;
			float x2 = 0f;
			float y2 = 0f;
			switch (position)
			{
			case global::SRDebugger.PinAlignment.TopLeft:
			case global::SRDebugger.PinAlignment.TopRight:
			case global::SRDebugger.PinAlignment.TopCenter:
				y = 1f;
				y2 = 1f;
				break;
			case global::SRDebugger.PinAlignment.BottomLeft:
			case global::SRDebugger.PinAlignment.BottomRight:
			case global::SRDebugger.PinAlignment.BottomCenter:
				y = 0f;
				y2 = 0f;
				break;
			case global::SRDebugger.PinAlignment.CenterLeft:
			case global::SRDebugger.PinAlignment.CenterRight:
				y = 0.5f;
				y2 = 0.5f;
				break;
			}
			switch (position)
			{
			case global::SRDebugger.PinAlignment.TopLeft:
			case global::SRDebugger.PinAlignment.BottomLeft:
			case global::SRDebugger.PinAlignment.CenterLeft:
				x = 0f;
				x2 = 0f;
				break;
			case global::SRDebugger.PinAlignment.TopRight:
			case global::SRDebugger.PinAlignment.BottomRight:
			case global::SRDebugger.PinAlignment.CenterRight:
				x = 1f;
				x2 = 1f;
				break;
			case global::SRDebugger.PinAlignment.TopCenter:
			case global::SRDebugger.PinAlignment.BottomCenter:
				x = 0.5f;
				x2 = 0.5f;
				break;
			}
			t.pivot = new global::UnityEngine.Vector2(x, y);
			global::UnityEngine.Vector2 anchorMax = (t.anchorMin = new global::UnityEngine.Vector2(x2, y2));
			t.anchorMax = anchorMax;
		}
	}
}
