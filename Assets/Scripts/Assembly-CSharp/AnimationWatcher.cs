public class AnimationWatcher
{
	private global::System.Collections.Generic.List<global::UnityEngine.Playables.PlayableDirector> animations;

	public AnimationWatcher(global::UnityEngine.GameObject gameObject)
	{
		animations = global::System.Linq.Enumerable.ToList(gameObject.GetComponentsInChildren<global::UnityEngine.Playables.PlayableDirector>());
		global::UnityEngine.Playables.PlayableDirector component = gameObject.GetComponent<global::UnityEngine.Playables.PlayableDirector>();
		if (!(component == null))
		{
			animations.Add(component);
		}
	}

	public bool AllAnimationsComplete()
	{
		foreach (global::UnityEngine.Playables.PlayableDirector animation in animations)
		{
			if (animation.state == global::UnityEngine.Playables.PlayState.Playing)
			{
				return false;
			}
		}
		return true;
	}
}
