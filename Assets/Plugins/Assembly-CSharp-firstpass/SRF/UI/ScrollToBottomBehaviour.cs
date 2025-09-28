namespace SRF.UI
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Scroll To Bottom Behaviour")]
	public class ScrollToBottomBehaviour : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.ScrollRect _scrollRect;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.CanvasGroup _canvasGroup;

		public void Start()
		{
			if (_scrollRect == null)
			{
				global::UnityEngine.Debug.LogError("[ScrollToBottomBehaviour] ScrollRect not set");
				return;
			}
			if (_canvasGroup == null)
			{
				global::UnityEngine.Debug.LogError("[ScrollToBottomBehaviour] CanvasGroup not set");
				return;
			}
			_scrollRect.onValueChanged.AddListener(OnScrollRectValueChanged);
			Refresh();
		}

		private void OnEnable()
		{
			Refresh();
		}

		public void Trigger()
		{
			_scrollRect.normalizedPosition = new global::UnityEngine.Vector2(0f, 0f);
		}

		private void OnScrollRectValueChanged(global::UnityEngine.Vector2 position)
		{
			Refresh();
		}

		private void Refresh()
		{
			if (!(_scrollRect == null))
			{
				if (_scrollRect.normalizedPosition.y < 0.001f)
				{
					SetVisible(truth: false);
				}
				else
				{
					SetVisible(truth: true);
				}
			}
		}

		private void SetVisible(bool truth)
		{
			if (truth)
			{
				_canvasGroup.alpha = 1f;
				_canvasGroup.interactable = true;
				_canvasGroup.blocksRaycasts = true;
			}
			else
			{
				_canvasGroup.alpha = 0f;
				_canvasGroup.interactable = false;
				_canvasGroup.blocksRaycasts = false;
			}
		}
	}
}
