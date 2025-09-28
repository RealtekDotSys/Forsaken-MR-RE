public class DroppedObject : global::UnityEngine.MonoBehaviour
{
	public string audioPrefix;

	public global::UnityEngine.Transform modelRoot;

	public global::UnityEngine.Animator animator;

	public float rimlightBias;

	public readonly SimpleTimer expirationTimer;

	public float SpawnTime;

	public DroppedObject()
	{
		expirationTimer = new SimpleTimer();
	}
}
