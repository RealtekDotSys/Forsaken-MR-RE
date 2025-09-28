namespace SRDebugger.UI
{
	public class MobileMenuController : global::SRF.SRMonoBehaviourEx
	{
		private global::UnityEngine.UI.Button _closeButton;

		[global::UnityEngine.SerializeField]
		private float _maxMenuWidth = 185f;

		[global::UnityEngine.SerializeField]
		private float _peekAmount = 45f;

		private float _targetXPosition;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform Content;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform Menu;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Button OpenButton;

		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Other.SRTabController TabController;

		public float PeekAmount => _peekAmount;

		public float MaxMenuWidth => _maxMenuWidth;

		protected override void OnEnable()
		{
			base.OnEnable();
			global::UnityEngine.RectTransform rectTransform = Menu.parent as global::UnityEngine.RectTransform;
			Menu.GetComponent<global::UnityEngine.UI.LayoutElement>().ignoreLayout = true;
			Menu.pivot = new global::UnityEngine.Vector2(1f, 1f);
			Menu.offsetMin = new global::UnityEngine.Vector2(1f, 0f);
			Menu.offsetMax = new global::UnityEngine.Vector2(1f, 1f);
			Menu.SetSizeWithCurrentAnchors(global::UnityEngine.RectTransform.Axis.Horizontal, global::UnityEngine.Mathf.Clamp(rectTransform.rect.width - PeekAmount, 0f, MaxMenuWidth));
			Menu.SetSizeWithCurrentAnchors(global::UnityEngine.RectTransform.Axis.Vertical, rectTransform.rect.height);
			Menu.anchoredPosition = new global::UnityEngine.Vector2(0f, 0f);
			if (_closeButton == null)
			{
				CreateCloseButton();
			}
			OpenButton.gameObject.SetActive(value: true);
			TabController.ActiveTabChanged += TabControllerOnActiveTabChanged;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Menu.GetComponent<global::UnityEngine.UI.LayoutElement>().ignoreLayout = false;
			Content.anchoredPosition = new global::UnityEngine.Vector2(0f, 0f);
			_closeButton.gameObject.SetActive(value: false);
			OpenButton.gameObject.SetActive(value: false);
			TabController.ActiveTabChanged -= TabControllerOnActiveTabChanged;
		}

		private void CreateCloseButton()
		{
			global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject("SR_CloseButtonCanvas", typeof(global::UnityEngine.RectTransform));
			obj.transform.SetParent(Content, worldPositionStays: false);
			global::UnityEngine.Canvas canvas = obj.AddComponent<global::UnityEngine.Canvas>();
			obj.AddComponent<global::UnityEngine.UI.GraphicRaycaster>();
			global::UnityEngine.RectTransform componentOrAdd = global::SRF.SRFGameObjectExtensions.GetComponentOrAdd<global::UnityEngine.RectTransform>(obj);
			canvas.overrideSorting = true;
			canvas.sortingOrder = 122;
			obj.AddComponent<global::UnityEngine.UI.LayoutElement>().ignoreLayout = true;
			SetRectSize(componentOrAdd);
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("SR_CloseButton", typeof(global::UnityEngine.RectTransform));
			gameObject.transform.SetParent(componentOrAdd, worldPositionStays: false);
			global::UnityEngine.RectTransform component = gameObject.GetComponent<global::UnityEngine.RectTransform>();
			SetRectSize(component);
			gameObject.AddComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
			_closeButton = gameObject.AddComponent<global::UnityEngine.UI.Button>();
			_closeButton.transition = global::UnityEngine.UI.Selectable.Transition.None;
			_closeButton.onClick.AddListener(CloseButtonClicked);
			_closeButton.gameObject.SetActive(value: false);
		}

		private void SetRectSize(global::UnityEngine.RectTransform rect)
		{
			rect.anchorMin = new global::UnityEngine.Vector2(0f, 0f);
			rect.anchorMax = new global::UnityEngine.Vector2(1f, 1f);
			rect.SetSizeWithCurrentAnchors(global::UnityEngine.RectTransform.Axis.Horizontal, Content.rect.width);
			rect.SetSizeWithCurrentAnchors(global::UnityEngine.RectTransform.Axis.Vertical, Content.rect.height);
		}

		private void CloseButtonClicked()
		{
			Close();
		}

		protected override void Update()
		{
			base.Update();
			float x = Content.anchoredPosition.x;
			if (global::UnityEngine.Mathf.Abs(_targetXPosition - x) < 2.5f)
			{
				Content.anchoredPosition = new global::UnityEngine.Vector2(_targetXPosition, Content.anchoredPosition.y);
			}
			else
			{
				Content.anchoredPosition = new global::UnityEngine.Vector2(SRMath.SpringLerp(x, _targetXPosition, 15f, global::UnityEngine.Time.unscaledDeltaTime), Content.anchoredPosition.y);
			}
		}

		private void TabControllerOnActiveTabChanged(global::SRDebugger.UI.Other.SRTabController srTabController, global::SRDebugger.UI.Other.SRTab srTab)
		{
			Close();
		}

		[global::UnityEngine.ContextMenu("Open")]
		public void Open()
		{
			_targetXPosition = Menu.rect.width;
			_closeButton.gameObject.SetActive(value: true);
		}

		[global::UnityEngine.ContextMenu("Close")]
		public void Close()
		{
			_targetXPosition = 0f;
			_closeButton.gameObject.SetActive(value: false);
		}
	}
}
