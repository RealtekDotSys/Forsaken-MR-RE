public class CustomAnimationEventScript : global::UnityEngine.MonoBehaviour
{
	public event global::System.Action<string> AnimEvent;

	public event global::System.Action<int> AnimComplete;

	public void OnAnimEvent(string message)
	{
		this.AnimEvent?.Invoke(message);
	}

	public void OnAnimComplete(int step)
	{
		this.AnimComplete?.Invoke(step);
	}
}
