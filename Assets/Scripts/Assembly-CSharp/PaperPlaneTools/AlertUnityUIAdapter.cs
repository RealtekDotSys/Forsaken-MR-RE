namespace PaperPlaneTools
{
	public class AlertUnityUIAdapter : global::UnityEngine.MonoBehaviour, global::PaperPlaneTools.IAlertPlatformAdapter
	{
		public global::TMPro.TextMeshProUGUI titleTextTMP;

		public global::TMPro.TextMeshProUGUI messageTextTMP;

		[global::UnityEngine.Tooltip("Text component to set alert title. If null title isn't presented.")]
		public global::UnityEngine.UI.Text titleText;

		[global::UnityEngine.Tooltip("Text component to set alert message. If null message isn't presented.")]
		public global::UnityEngine.UI.Text messageText;

		[global::UnityEngine.Tooltip("Button component to set alert positive button text and callback. If null no button is presented.")]
		public global::UnityEngine.UI.Button positiveButton;

		[global::UnityEngine.Tooltip("Button component to set alert neutral button text and callback. If null no button is presented.")]
		public global::UnityEngine.UI.Button neutralButton;

		[global::UnityEngine.Tooltip("Button component to set alert negative button text and callback. If null no button is presented.")]
		public global::UnityEngine.UI.Button negativeButton;

		[global::UnityEngine.Tooltip("Backgroud panel which dismisses the dialog when clicked. If null, dialog doesn't dismiss when click/tap background.")]
		public global::UnityEngine.GameObject dismissPanel;

		private global::System.Action onDismiss;

		void global::PaperPlaneTools.IAlertPlatformAdapter.SetOnDismiss(global::System.Action action)
		{
			onDismiss = action;
		}

		void global::PaperPlaneTools.IAlertPlatformAdapter.Dismiss()
		{
			base.gameObject.SetActive(value: false);
			if (onDismiss != null)
			{
				onDismiss();
			}
		}

		void global::PaperPlaneTools.IAlertPlatformAdapter.Show(global::PaperPlaneTools.Alert alert)
		{
			base.transform.SetAsLastSibling();
			global::UnityEngine.Debug.LogError(alert.Title + " - " + alert.Message);
			if (titleTextTMP == null)
			{
				if (titleText != null)
				{
					titleText.gameObject.SetActive(alert.Title != null);
					titleText.text = ((alert.Title != null) ? alert.Title : "");
				}
			}
			else
			{
				titleTextTMP.gameObject.SetActive(alert.Title != null);
				titleTextTMP.text = ((alert.Title != null) ? alert.Title : "");
			}
			if (messageTextTMP == null)
			{
				if (messageText != null)
				{
					messageText.gameObject.SetActive(alert.Message != null);
					messageText.text = ((alert.Message != null) ? alert.Message : "");
				}
			}
			else
			{
				messageTextTMP.gameObject.SetActive(alert.Message != null);
				messageTextTMP.text = ((alert.Message != null) ? alert.Message : "");
			}
			SetButton(positiveButton, alert.PositiveButton);
			SetButton(neutralButton, alert.NeutralButton);
			SetButton(negativeButton, alert.NegativeButton);
			if (dismissPanel != null)
			{
				global::UnityEngine.EventSystems.EventTrigger eventTrigger = dismissPanel.GetComponent<global::UnityEngine.EventSystems.EventTrigger>();
				if (eventTrigger == null)
				{
					eventTrigger = dismissPanel.AddComponent(typeof(global::UnityEngine.EventSystems.EventTrigger)) as global::UnityEngine.EventSystems.EventTrigger;
				}
				if (eventTrigger != null)
				{
					global::System.Collections.Generic.List<global::UnityEngine.EventSystems.EventTrigger.Entry> list = (eventTrigger.triggers = new global::System.Collections.Generic.List<global::UnityEngine.EventSystems.EventTrigger.Entry>());
					list.RemoveAll((global::UnityEngine.EventSystems.EventTrigger.Entry foo) => true);
					global::UnityEngine.EventSystems.EventTrigger.Entry entry = new global::UnityEngine.EventSystems.EventTrigger.Entry();
					entry.eventID = global::UnityEngine.EventSystems.EventTriggerType.PointerClick;
					entry.callback.AddListener(delegate
					{
						((global::PaperPlaneTools.IAlertPlatformAdapter)this).Dismiss();
					});
					list.Add(entry);
				}
			}
			base.gameObject.SetActive(value: true);
		}

		void global::PaperPlaneTools.IAlertPlatformAdapter.HandleEvent(string name, string value)
		{
		}

		private void SetButton(global::UnityEngine.UI.Button uiButton, global::PaperPlaneTools.AlertButton alertButton)
		{
			if (!(uiButton != null))
			{
				return;
			}
			uiButton.gameObject.SetActive(alertButton != null);
			global::UnityEngine.Component[] componentsInChildren = uiButton.GetComponentsInChildren(typeof(global::UnityEngine.UI.Text), includeInactive: true);
			global::UnityEngine.Component[] componentsInChildren2 = uiButton.GetComponentsInChildren(typeof(global::TMPro.TextMeshProUGUI), includeInactive: true);
			if (componentsInChildren2 != null && componentsInChildren2.Length != 0)
			{
				(componentsInChildren2[0] as global::TMPro.TextMeshProUGUI).text = ((alertButton != null && alertButton.Title != null) ? alertButton.Title : "");
			}
			if (componentsInChildren != null && componentsInChildren.Length != 0)
			{
				(componentsInChildren[0] as global::UnityEngine.UI.Text).text = ((alertButton != null && alertButton.Title != null) ? alertButton.Title : "");
			}
			uiButton.onClick.RemoveAllListeners();
			uiButton.onClick.AddListener(delegate
			{
				((global::PaperPlaneTools.IAlertPlatformAdapter)this).Dismiss();
			});
			if (alertButton != null && alertButton.Handler != null)
			{
				uiButton.onClick.AddListener(delegate
				{
					alertButton.Handler();
				});
			}
		}
	}
}
