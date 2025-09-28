public class CameraFx : global::UnityEngine.MonoBehaviour
{
	public DisruptionFxController disruptionFxController;

	private void Update()
	{
		if (global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 2)
		{
			if (disruptionFxController == null)
			{
				global::UnityEngine.Debug.LogWarning("CameraFx has found disruption controller");
				disruptionFxController = global::UnityEngine.GameObject.Find("UI_Camera").GetComponent<DisruptionFxController>();
			}
		}
		else
		{
			disruptionFxController = null;
		}
	}
}
