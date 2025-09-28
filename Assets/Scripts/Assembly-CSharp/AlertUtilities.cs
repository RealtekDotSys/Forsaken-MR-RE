public static class AlertUtilities
{
	public static void ShowAlertWithAndroidBackButtonAction(global::PaperPlaneTools.Alert alert, global::System.Action backButtonAction)
	{
		int backButtonActionId = 0;
		global::System.Action oldOnDismiss = alert.OnDismiss;
		alert.SetOnDismiss(delegate
		{
			if (backButtonActionId >= 1)
			{
				backButtonActionId = 0;
			}
			alert.SetOnDismiss(oldOnDismiss);
			oldOnDismiss?.Invoke();
		});
		alert.Show();
	}

	public static void ShowAlertWithAndroidBackButtonPause(global::PaperPlaneTools.Alert alert)
	{
		int pauseId = 0;
		global::System.Action oldOnDismiss = alert.OnDismiss;
		alert.SetOnDismiss(delegate
		{
			if (pauseId >= 1)
			{
				pauseId = 0;
			}
			alert.SetOnDismiss(oldOnDismiss);
			oldOnDismiss?.Invoke();
		});
		alert.Show();
	}
}
