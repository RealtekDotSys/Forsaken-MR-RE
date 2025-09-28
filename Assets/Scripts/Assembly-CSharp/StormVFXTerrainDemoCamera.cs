public class StormVFXTerrainDemoCamera : global::UnityEngine.MonoBehaviour
{
	public float moveSpeed = 5f;

	public float height = 2f;

	[global::UnityEngine.Space]
	public float acceleration = 10f;

	public float deceleration = 5f;

	private global::UnityEngine.Vector3 velocity;

	private void Start()
	{
	}

	private void Update()
	{
		global::UnityEngine.Vector2 vector = default(global::UnityEngine.Vector2);
		vector.x = global::UnityEngine.Input.GetAxisRaw("Horizontal");
		vector.y = global::UnityEngine.Input.GetAxisRaw("Vertical");
		bool num = vector != global::UnityEngine.Vector2.zero;
		global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
		global::UnityEngine.RaycastHit hitInfo;
		bool num2 = global::UnityEngine.Physics.Raycast(base.transform.position, global::UnityEngine.Vector3.down, out hitInfo);
		_ = global::UnityEngine.Vector3.zero;
		if (num2)
		{
			zero.y = hitInfo.point.y + height - base.transform.position.y;
		}
		if (num)
		{
			zero += base.transform.right * vector.x;
			zero += base.transform.forward * vector.y;
			zero.Normalize();
			zero *= moveSpeed;
			velocity = global::UnityEngine.Vector3.MoveTowards(velocity, zero, global::UnityEngine.Time.deltaTime * acceleration);
		}
		else
		{
			velocity = global::UnityEngine.Vector3.MoveTowards(velocity, zero, global::UnityEngine.Time.deltaTime * deceleration);
		}
		global::UnityEngine.Vector3 vector2 = velocity * global::UnityEngine.Time.deltaTime;
		base.transform.position += vector2;
	}
}
