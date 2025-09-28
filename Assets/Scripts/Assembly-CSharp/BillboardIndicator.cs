public class BillboardIndicator : global::UnityEngine.MonoBehaviour
{
	private global::UnityEngine.Animator _animator;

	private static readonly int FlashId = global::UnityEngine.Animator.StringToHash("Flashing");

	private static readonly int JitterId = global::UnityEngine.Animator.StringToHash("Jitter");

	private static readonly int SteadyId = global::UnityEngine.Animator.StringToHash("Steady");

	private void OnEnable()
	{
		_animator = GetComponent<global::UnityEngine.Animator>();
	}

	public void PlayAnimation(TextAnim anim)
	{
		if (!(_animator == null))
		{
			switch (anim)
			{
			case TextAnim.Steady:
				_animator.SetTrigger(SteadyId);
				break;
			case TextAnim.Flashing:
				_animator.SetTrigger(FlashId);
				break;
			case TextAnim.Jitter:
				_animator.SetTrigger(JitterId);
				break;
			}
		}
	}
}
