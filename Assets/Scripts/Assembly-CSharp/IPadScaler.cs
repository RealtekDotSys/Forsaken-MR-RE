public class IPadScaler : global::UnityEngine.MonoBehaviour
{
	private void Awake()
	{
		if (global::UnityEngine.SystemInfo.deviceModel.ToLower().Contains("ipad"))
		{
			base.gameObject.GetComponent<global::UnityEngine.UI.CanvasScaler>().matchWidthOrHeight = 0.4f;
		}
	}
}
