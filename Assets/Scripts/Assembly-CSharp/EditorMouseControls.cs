public class EditorMouseControls : global::UnityEngine.MonoBehaviour
{
	public float speed = 3.5f;

	private float X;

	private float Y;

	private void Update()
	{
		if (global::UnityEngine.Input.GetMouseButton(0))
		{
			base.transform.Rotate(new global::UnityEngine.Vector3(global::UnityEngine.Input.GetAxis("Mouse Y") * speed, (0f - global::UnityEngine.Input.GetAxis("Mouse X")) * speed, 0f));
			X = base.transform.rotation.eulerAngles.x;
			Y = base.transform.rotation.eulerAngles.y;
			base.transform.rotation = global::UnityEngine.Quaternion.Euler(X, Y, 0f);
		}
	}
}
