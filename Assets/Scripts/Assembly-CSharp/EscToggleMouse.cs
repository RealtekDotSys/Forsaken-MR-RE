public class EscToggleMouse : global::UnityEngine.MonoBehaviour
{
	private bool toggled;

	private void Update()
	{
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape))
		{
			bool flag = (toggled = !toggled);
			global::UnityEngine.Cursor.lockState = ((!flag) ? global::UnityEngine.CursorLockMode.Locked : global::UnityEngine.CursorLockMode.None);
			global::UnityEngine.Cursor.visible = flag;
		}
	}
}
