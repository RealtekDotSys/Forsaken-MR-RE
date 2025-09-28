public class SettingsCanvasSwapper : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Canvases")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject SettingsMasterParent;

	private global::UnityEngine.UI.Button settingsButton;

	private void Update()
	{
		if (settingsButton == null)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find("TopBarSettingsButton");
			if (gameObject != null)
			{
				settingsButton = gameObject.GetComponent<global::UnityEngine.UI.Button>();
				settingsButton.onClick.RemoveListener(SettingsButtonPressed);
				settingsButton.onClick.AddListener(SettingsButtonPressed);
			}
		}
	}

	private void SettingsButtonPressed()
	{
		SettingsMasterParent.SetActive(value: true);
	}

	public void SettingsClosed()
	{
		SettingsMasterParent.SetActive(value: false);
	}
}
