public class MoveableEnvironmentObject : global::UnityEngine.MonoBehaviour
{
	private global::UnityEngine.Animator animator;

	[global::UnityEngine.Serialization.FormerlySerializedAs("objectDisabled")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject falsePosition;

	[global::UnityEngine.Serialization.FormerlySerializedAs("objectEnabled")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject truePosition;

	private float cooldown;

	public bool state;

	private void OnEnable()
	{
		state = false;
		animator = GetComponent<global::UnityEngine.Animator>();
	}

	public void Tapped()
	{
		if (!(cooldown > 0f))
		{
			cooldown = 0.2f;
			state = !state;
			global::UnityEngine.Debug.Log("box tapped");
			truePosition.SetActive(state);
			falsePosition.SetActive(!state);
			animator.ResetTrigger("Go");
			animator.SetInteger("Position", state ? 1 : 0);
			animator.SetTrigger("Go");
			global::UnityEngine.Debug.Log("set Position of " + base.gameObject.name + " to " + (state ? 1 : 0));
		}
	}

	private void Update()
	{
		if (cooldown > 0f)
		{
			cooldown -= global::UnityEngine.Time.deltaTime;
		}
	}
}
