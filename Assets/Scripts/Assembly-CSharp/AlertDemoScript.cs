public class AlertDemoScript : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject alertNativeWindow;

	public void OnButtonShowNativeWindow()
	{
		new global::PaperPlaneTools.Alert("Hello", "Hello, world").SetPositiveButton("OK", delegate
		{
			global::UnityEngine.Debug.Log("Ok handler");
		}).Show();
	}

	public void OnButtonShowUIWindow()
	{
		if (alertNativeWindow == null)
		{
			global::UnityEngine.Debug.Log("Alert Native Window property is the inspector");
		}
		else
		{
			new global::PaperPlaneTools.Alert("Hello", "Hello, world").SetPositiveButton("OK").SetNeutralButton("Cancel").SetAdapter(alertNativeWindow.GetComponent<global::PaperPlaneTools.IAlertPlatformAdapter>())
				.Show();
		}
	}

	public void OnButtonQueueTest()
	{
		new global::PaperPlaneTools.Alert("Hello", "#1 in queue").SetPositiveButton("OK").Show();
		new global::PaperPlaneTools.Alert("Hello", "#2 in queue").SetPositiveButton("OK").Show();
	}
}
