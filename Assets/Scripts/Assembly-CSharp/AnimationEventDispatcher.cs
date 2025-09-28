public class AnimationEventDispatcher
{
	private const int EffectEventIdMin = 1000;

	private const int EffectEventIdMax = 1999;

	private const int GameEventIdMin = 2000;

	private const int GameEventIdMax = 2999;

	private const int SoundEventIdMin = 3000;

	private const int SoundEventIdMax = 3999;

	private AnimationEventListener _animationEventListener;

	public event global::System.Action<global::UnityEngine.AnimationEvent> OnEffectEventReceived;

	public event global::System.Action<global::UnityEngine.AnimationEvent> OnGameEventReceived;

	public event global::System.Action<global::UnityEngine.AnimationEvent> OnSoundEventReceived;

	public void AnimationEventReceived(global::UnityEngine.AnimationEvent animationEvent)
	{
		if (animationEvent.intParameter - 1000 <= 999)
		{
			this.OnEffectEventReceived?.Invoke(animationEvent);
			if (animationEvent.intParameter == 1008)
			{
				this.OnGameEventReceived?.Invoke(animationEvent);
			}
		}
		else if (animationEvent.intParameter - 2000 <= 999)
		{
			this.OnGameEventReceived?.Invoke(animationEvent);
		}
		else if (animationEvent.intParameter - 3000 <= 999)
		{
			this.OnSoundEventReceived?.Invoke(animationEvent);
		}
		else
		{
			global::UnityEngine.Debug.LogError("AnimationEvent Received - Received an animation event with an unknown id " + animationEvent.intParameter);
		}
	}

	public void Setup(AnimationEventListener animationEventListener)
	{
		_animationEventListener = animationEventListener;
		_animationEventListener.OnAnimationEventReceived += AnimationEventReceived;
	}

	public void Teardown()
	{
		_animationEventListener.OnAnimationEventReceived -= AnimationEventReceived;
		_animationEventListener = null;
	}
}
