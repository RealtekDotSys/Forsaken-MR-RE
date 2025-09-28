public class ProgressUpdaterDebug : global::UnityEngine.MonoBehaviour
{
	public static global::TMPro.TextMeshProUGUI text;

	private void Awake()
	{
		text = base.gameObject.GetComponent<global::TMPro.TextMeshProUGUI>();
	}
}
