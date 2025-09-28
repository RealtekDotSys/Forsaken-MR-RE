namespace PaperPlaneTools
{
	public class AlertMonoBehaviour : global::UnityEngine.MonoBehaviour
	{
		private void Start()
		{
		}

		public void AlertAndroid_OnButtonClick(string buttonType)
		{
			global::PaperPlaneTools.AlertManager.Instance.HandleEvent("AlertAndroid_OnButtonClick", buttonType);
		}

		public void AlertAndroid_OnCancel(string nothing)
		{
			global::PaperPlaneTools.AlertManager.Instance.HandleEvent("AlertAndroid_OnCancel", nothing);
		}

		public void AlertAndroid_OnDismiss(string nothing)
		{
			global::PaperPlaneTools.AlertManager.Instance.HandleEvent("AlertAndroid_OnDismiss", nothing);
		}

		private void AlertIOS_OnButtonClick(string tag)
		{
			global::PaperPlaneTools.AlertManager.Instance.HandleEvent("AlertIOS_OnButtonClick", tag);
		}

		private void AlertIOS_OnDismiss(string nothing)
		{
			global::PaperPlaneTools.AlertManager.Instance.HandleEvent("AlertIOS_OnDismiss", nothing);
		}
	}
}
