public class DialogHandler_Results : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Hookups")]
	private ResultsUIActions resultsUIActions;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Dialog GameObjects")]
	private global::UnityEngine.GameObject dialogRewardsWin;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject dialogRewardsLose;

	[global::UnityEngine.SerializeField]
	private global::PaperPlaneTools.AlertUnityUIAdapter dialogResultsPlayerWin;

	[global::UnityEngine.SerializeField]
	private ResultsPlayerWinUIView resultsPlayerWinView;

	[global::UnityEngine.SerializeField]
	private global::PaperPlaneTools.AlertUnityUIAdapter dialogResultsPlayerLose;

	[global::UnityEngine.SerializeField]
	private global::PaperPlaneTools.AlertUnityUIAdapter dialogResultsTutorialEnd;

	private global::PaperPlaneTools.Alert dialogRewardsAlertWin;

	private global::PaperPlaneTools.Alert dialogRewardsAlertLose;

	private global::PaperPlaneTools.Alert dialogResultsAlertPlayerWin;

	private global::PaperPlaneTools.Alert dialogResultsAlertPlayerLose;

	private global::PaperPlaneTools.Alert dialogResultsAlertTutorialEnd;

	public void ShowRewardsWin()
	{
	}

	public void ShowRewardsLose()
	{
	}

	public void ShowResultsPlayerWin()
	{
		LocalizationDomain.Instance.Localization.GetInterfaceAsync(ShowResultsPlayerWinb__14_0);
	}

	public void ShowResultsPlayerLose()
	{
		dialogResultsAlertPlayerLose.Show();
	}

	public void ShowResultsTutorialEnd()
	{
	}

	private void Awake()
	{
		dialogResultsAlertPlayerWin = new global::PaperPlaneTools.Alert();
		dialogResultsAlertPlayerLose = new global::PaperPlaneTools.Alert();
		dialogResultsAlertPlayerWin.SetAdapter(dialogResultsPlayerWin);
		dialogResultsAlertPlayerLose.SetAdapter(dialogResultsPlayerLose);
		string localizedString = LocalizationDomain.Instance.Localization.GetLocalizedString("ui_results_display_return_button_win", "RETURN");
		dialogResultsAlertPlayerWin.SetPositiveButton((localizedString == null) ? "RETURN" : localizedString, resultsUIActions.HideResultsCanvas);
		string localizedString2 = LocalizationDomain.Instance.Localization.GetLocalizedString("ui_results_display_return_button_loss", "RETURN");
		dialogResultsAlertPlayerLose.SetPositiveButton((localizedString2 == null) ? "RETURN" : localizedString2, resultsUIActions.HideResultsCanvas);
	}

	private void OnDestroy()
	{
		if (dialogRewardsAlertWin != null)
		{
			dialogRewardsAlertWin.Dismiss();
		}
		dialogRewardsAlertWin = null;
		if (dialogRewardsAlertLose != null)
		{
			dialogRewardsAlertLose.Dismiss();
		}
		dialogRewardsAlertLose = null;
		if (dialogResultsAlertPlayerWin != null)
		{
			dialogResultsAlertPlayerWin.Dismiss();
		}
		dialogResultsAlertPlayerWin = null;
		if (dialogResultsAlertPlayerLose != null)
		{
			dialogResultsAlertPlayerLose.Dismiss();
		}
		dialogResultsAlertPlayerLose = null;
		if (dialogResultsAlertTutorialEnd != null)
		{
			dialogResultsAlertTutorialEnd.Dismiss();
		}
		dialogResultsAlertTutorialEnd = null;
	}

	private void ShowResultsPlayerWinb__14_0(Localization localization)
	{
		global::UnityEngine.Debug.Log("Showing win UI");
		resultsPlayerWinView.Show(localization.GetLocalizedString("ui_results_display_return_button_win", "RETURN"), resultsUIActions.HideResultsCanvas);
	}

	private void Awakeb__17_0(Localization localization)
	{
		dialogRewardsAlertWin.SetPositiveButton(localization.GetLocalizedString("ui_results_display_return_button_win", "RETURN"), resultsUIActions.HideResultsCanvas);
	}

	private void Awakeb__17_1(Localization localization)
	{
		dialogRewardsAlertLose.SetPositiveButton(localization.GetLocalizedString("ui_results_display_return_button_loss", "RETURN"), resultsUIActions.HideResultsCanvas);
	}
}
