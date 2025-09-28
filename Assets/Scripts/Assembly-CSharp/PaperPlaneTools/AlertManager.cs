namespace PaperPlaneTools
{
	public class AlertManager
	{
		private global::PaperPlaneTools.IAlertPlatformAdapter currentAdapter;

		private global::PaperPlaneTools.Alert currentAlert;

		private global::System.Collections.Generic.List<global::PaperPlaneTools.Alert> queue = new global::System.Collections.Generic.List<global::PaperPlaneTools.Alert>();

		private static global::PaperPlaneTools.AlertManager instance;

		public static global::PaperPlaneTools.AlertManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new global::PaperPlaneTools.AlertManager();
				}
				return instance;
			}
		}

		public global::System.Func<global::PaperPlaneTools.IAlertPlatformAdapter> AlertFactory { get; set; }

		private AlertManager()
		{
		}

		public void Show(global::PaperPlaneTools.Alert alert)
		{
			queue.Add(alert);
			ShowNext();
		}

		public void Dismiss(global::PaperPlaneTools.Alert alert)
		{
			if (currentAlert == alert)
			{
				currentAdapter.Dismiss();
				return;
			}
			int num = queue.IndexOf(alert);
			if (num >= 0)
			{
				queue.RemoveAt(num);
				if (alert.OnDismiss != null)
				{
					alert.OnDismiss();
				}
			}
		}

		public void HandleEvent(string eventName, string value)
		{
			if (currentAlert != null)
			{
				currentAdapter.HandleEvent(eventName, value);
			}
		}

		public bool IsShowingAlert()
		{
			if (currentAlert != null)
			{
				return true;
			}
			if (queue != null)
			{
				return queue.Count > 0;
			}
			return false;
		}

		private global::PaperPlaneTools.IAlertPlatformAdapter CreateAdapter()
		{
			if (AlertFactory == null)
			{
				return null;
			}
			return AlertFactory();
		}

		private void OnDismiss()
		{
			currentAdapter = null;
			currentAlert = null;
			ShowNext();
		}

		private void ShowNext()
		{
			if (currentAdapter == null && queue.Count > 0)
			{
				currentAlert = queue[0];
				queue.RemoveAt(0);
				if (currentAlert != null)
				{
					currentAdapter = ((currentAlert.Adapter != null) ? currentAlert.Adapter : CreateAdapter());
					currentAdapter.SetOnDismiss(OnDismiss);
					currentAdapter.Show(currentAlert);
				}
			}
		}
	}
}
