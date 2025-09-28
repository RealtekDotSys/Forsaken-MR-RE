namespace SRDebugger.UI.Other
{
	public class BugReportSheetController : global::SRF.SRMonoBehaviourEx
	{
		[global::SRF.RequiredField]
		public global::UnityEngine.GameObject ButtonContainer;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text ButtonText;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Button CancelButton;

		public global::System.Action CancelPressed;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.InputField DescriptionField;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.InputField EmailField;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Slider ProgressBar;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text ResultMessageText;

		public global::System.Action ScreenshotComplete;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Button SubmitButton;

		public global::System.Action<bool, string> SubmitComplete;

		public global::System.Action TakingScreenshot;

		public bool IsCancelButtonEnabled
		{
			get
			{
				return CancelButton.gameObject.activeSelf;
			}
			set
			{
				CancelButton.gameObject.SetActive(value);
			}
		}

		protected override void Start()
		{
			base.Start();
			SetLoadingSpinnerVisible(visible: false);
			ClearErrorMessage();
			ClearForm();
		}

		public void Submit()
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
			ProgressBar.value = 0f;
			ClearErrorMessage();
			SetLoadingSpinnerVisible(visible: true);
			SetFormEnabled(e: false);
			if (!string.IsNullOrEmpty(EmailField.text))
			{
				SetDefaultEmailFieldContents(EmailField.text);
			}
			StartCoroutine(SubmitCo());
		}

		public void Cancel()
		{
			if (CancelPressed != null)
			{
				CancelPressed();
			}
		}

		private global::System.Collections.IEnumerator SubmitCo()
		{
			if (global::SRDebugger.Internal.BugReportScreenshotUtil.ScreenshotData == null)
			{
				if (TakingScreenshot != null)
				{
					TakingScreenshot();
				}
				yield return new global::UnityEngine.WaitForEndOfFrame();
				yield return StartCoroutine(global::SRDebugger.Internal.BugReportScreenshotUtil.ScreenshotCaptureCo());
				if (ScreenshotComplete != null)
				{
					ScreenshotComplete();
				}
			}
			global::SRDebugger.Services.IBugReportService service = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IBugReportService>();
			global::SRDebugger.Services.BugReport report = new global::SRDebugger.Services.BugReport
			{
				Email = EmailField.text,
				UserDescription = DescriptionField.text,
				ConsoleLog = global::System.Linq.Enumerable.ToList(global::SRDebugger.Internal.Service.Console.AllEntries),
				SystemInformation = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.ISystemInformationService>().CreateReport(),
				ScreenshotData = global::SRDebugger.Internal.BugReportScreenshotUtil.ScreenshotData
			};
			global::SRDebugger.Internal.BugReportScreenshotUtil.ScreenshotData = null;
			service.SendBugReport(report, OnBugReportComplete, OnBugReportProgress);
		}

		private void OnBugReportProgress(float progress)
		{
			ProgressBar.value = progress;
		}

		private void OnBugReportComplete(bool didSucceed, string errorMessage)
		{
			if (!didSucceed)
			{
				ShowErrorMessage("Error sending bug report", errorMessage);
			}
			else
			{
				ClearForm();
				ShowErrorMessage("Bug report submitted successfully");
			}
			SetLoadingSpinnerVisible(visible: false);
			SetFormEnabled(e: true);
			if (SubmitComplete != null)
			{
				SubmitComplete(didSucceed, errorMessage);
			}
		}

		protected void SetLoadingSpinnerVisible(bool visible)
		{
			ProgressBar.gameObject.SetActive(visible);
			ButtonContainer.SetActive(!visible);
		}

		protected void ClearForm()
		{
			EmailField.text = GetDefaultEmailFieldContents();
			DescriptionField.text = "";
		}

		protected void ShowErrorMessage(string userMessage, string serverMessage = null)
		{
			string text = userMessage;
			if (!string.IsNullOrEmpty(serverMessage))
			{
				text += global::SRF.SRFStringExtensions.Fmt(" (<b>{0}</b>)", serverMessage);
			}
			ResultMessageText.text = text;
			ResultMessageText.gameObject.SetActive(value: true);
		}

		protected void ClearErrorMessage()
		{
			ResultMessageText.text = "";
			ResultMessageText.gameObject.SetActive(value: false);
		}

		protected void SetFormEnabled(bool e)
		{
			SubmitButton.interactable = e;
			CancelButton.interactable = e;
			EmailField.interactable = e;
			DescriptionField.interactable = e;
		}

		private string GetDefaultEmailFieldContents()
		{
			return global::UnityEngine.PlayerPrefs.GetString("SRDEBUGGER_BUG_REPORT_LAST_EMAIL", "");
		}

		private void SetDefaultEmailFieldContents(string value)
		{
			global::UnityEngine.PlayerPrefs.SetString("SRDEBUGGER_BUG_REPORT_LAST_EMAIL", value);
			global::UnityEngine.PlayerPrefs.Save();
		}
	}
}
