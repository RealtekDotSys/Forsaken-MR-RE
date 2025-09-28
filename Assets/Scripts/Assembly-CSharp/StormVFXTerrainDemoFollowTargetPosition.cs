public class StormVFXTerrainDemoFollowTargetPosition : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform target;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void LateUpdate()
	{
		base.transform.position = target.position;
	}
}
