public class StableTransformDebug : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform playerTransform;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform myTransform;

	private void Update()
	{
		myTransform.localEulerAngles = new global::UnityEngine.Vector3(0f, playerTransform.localEulerAngles.y, 0f);
		myTransform.position = playerTransform.position;
	}
}
