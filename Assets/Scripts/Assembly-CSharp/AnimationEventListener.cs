public class AnimationEventListener : global::UnityEngine.MonoBehaviour
{
	public event global::System.Action<global::UnityEngine.AnimationEvent> OnAnimationEventReceived;

	public void RaiseAnimationEvent(global::UnityEngine.AnimationEvent animationEvent)
	{
		this.OnAnimationEventReceived?.Invoke(animationEvent);
	}
}
