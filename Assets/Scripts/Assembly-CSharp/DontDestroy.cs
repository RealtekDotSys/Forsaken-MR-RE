public class DontDestroy : global::UnityEngine.MonoBehaviour
{
	private void Start()
	{
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}
}
