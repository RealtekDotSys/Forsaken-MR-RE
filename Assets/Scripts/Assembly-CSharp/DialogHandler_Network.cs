public class DialogHandler_Network : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Dialog GameObjects")]
	private global::UnityEngine.GameObject genericDialog;

	private MasterDomain _masterDomain;

	private global::PaperPlaneTools.Alert genericAlert;

	private void EventExposer_GenericDialogRequestRecieved(GenericDialogData data)
	{
		ShowGenericDialog(data);
	}

	private void AddSubscriptions()
	{
		_masterDomain.eventExposer.add_NetworkDialogRequestReceived(EventExposer_GenericDialogRequestRecieved);
		_masterDomain.eventExposer.add_NetworkDialogRequestRemoved(CancelAlert);
	}

	private void RemoveSubcriptions()
	{
		if (_masterDomain.eventExposer != null)
		{
			_masterDomain.eventExposer.remove_NetworkDialogRequestReceived(EventExposer_GenericDialogRequestRecieved);
			_masterDomain.eventExposer.remove_NetworkDialogRequestRemoved(CancelAlert);
		}
	}

	public void ShowGenericDialog(string title, string message, string positiveButtonText, global::System.Action positiveButtonAction, string neutralButtonText, global::System.Action neutralButtonAction, string negativeButtonText, global::System.Action negativeButtonAction)
	{
		if (title != null)
		{
			genericAlert.SetTitle(title);
		}
		if (message != null)
		{
			genericAlert.SetMessage(message);
		}
		if (positiveButtonText == null || positiveButtonAction == null)
		{
			genericAlert.ClearPositiveButton();
		}
		else
		{
			genericAlert.SetPositiveButton(positiveButtonText, positiveButtonAction);
		}
		if (neutralButtonText == null || neutralButtonAction == null)
		{
			genericAlert.ClearNeutralButton();
		}
		else
		{
			genericAlert.SetNeutralButton(neutralButtonText, neutralButtonAction);
		}
		if (negativeButtonText == null)
		{
			genericAlert.ClearNegativeButton();
		}
		else
		{
			genericAlert.SetNegativeButton(negativeButtonText, negativeButtonAction);
		}
		AlertUtilities.ShowAlertWithAndroidBackButtonAction(genericAlert, ShowGenericDialogb__6_0);
	}

	public void ShowGenericDialog(GenericDialogData obj)
	{
		ShowGenericDialog(obj.title, obj.message, obj.positiveButtonText, obj.positiveButtonAction, obj.neutralButtonText, obj.neutralButtonAction, obj.negativeButtonText, obj.negativeButtonAction);
	}

	private void SetupGenericDialog()
	{
		genericAlert = new global::PaperPlaneTools.Alert();
		genericAlert.SetAdapter(genericDialog.GetComponent<global::PaperPlaneTools.IAlertPlatformAdapter>());
	}

	private void CancelAlert()
	{
		genericAlert.Dismiss();
	}

	private void Awake()
	{
		SetupGenericDialog();
		_masterDomain = MasterDomain.GetDomain();
		AddSubscriptions();
	}

	private void OnDestroy()
	{
		if (genericAlert != null)
		{
			genericAlert.Dismiss();
		}
		genericAlert = null;
		RemoveSubcriptions();
	}

	private void ShowGenericDialogb__6_0()
	{
		if (genericAlert.NegativeButton.Handler != null)
		{
			genericAlert.NegativeButton.Handler();
		}
		genericAlert.Dismiss();
	}
}
