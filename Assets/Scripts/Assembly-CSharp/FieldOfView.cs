public class FieldOfView : global::UnityEngine.MonoBehaviour
{
	public float radius;

	[global::UnityEngine.Range(0f, 360f)]
	public float angle;

	public global::UnityEngine.LayerMask targetMask;

	public global::UnityEngine.LayerMask obstructionMask;

	public bool canSeePlayer { get; private set; }

	private void Start()
	{
		StartCoroutine(FOVRoutine());
	}

	private global::System.Collections.IEnumerator FOVRoutine()
	{
		global::UnityEngine.WaitForSeconds wait = new global::UnityEngine.WaitForSeconds(0.2f);
		while (true)
		{
			yield return wait;
			FieldOfViewCheck();
		}
	}

	private void FieldOfViewCheck()
	{
		global::UnityEngine.Collider[] array = global::UnityEngine.Physics.OverlapSphere(base.transform.position, radius, targetMask);
		if (array.Length != 0)
		{
			global::UnityEngine.Transform transform = array[0].transform;
			global::UnityEngine.Vector3 normalized = (transform.position - base.transform.position).normalized;
			if (global::UnityEngine.Vector3.Angle(base.transform.forward, normalized) < angle / 2f)
			{
				float maxDistance = global::UnityEngine.Vector3.Distance(base.transform.position, transform.position);
				if (!global::UnityEngine.Physics.Raycast(base.transform.position, normalized, maxDistance, obstructionMask))
				{
					canSeePlayer = true;
				}
				else
				{
					canSeePlayer = false;
				}
			}
			else
			{
				canSeePlayer = false;
			}
		}
		else if (canSeePlayer)
		{
			canSeePlayer = false;
		}
	}
}
