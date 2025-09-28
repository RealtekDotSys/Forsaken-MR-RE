public class WinLose_Rotate : global::UnityEngine.MonoBehaviour
{
	public float speed = 5f;

	private void Update()
	{
		base.transform.Rotate(0f, 0f, speed);
	}
}
