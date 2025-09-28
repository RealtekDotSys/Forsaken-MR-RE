public class PrefabInstance
{
	public global::UnityEngine.GameObject Root;

	public ComponentContainer ComponentContainer;

	private global::System.Action RequestClose;

	public void add_RequestClose(global::System.Action value)
	{
		RequestClose = (global::System.Action)global::System.Delegate.Combine(RequestClose, value);
	}

	public void remove_RequestClose(global::System.Action value)
	{
		RequestClose = (global::System.Action)global::System.Delegate.Remove(RequestClose, value);
	}

	public void DispatchCloseEvent()
	{
		global::UnityEngine.Debug.LogWarning("Dispatch close event called");
		if (RequestClose == null)
		{
			global::UnityEngine.Debug.LogWarning("Dispatch close event is NULL MOTHER FUCKER");
			Clear();
		}
		else
		{
			RequestClose();
		}
	}

	public void Clear()
	{
		if (Root != null)
		{
			global::UnityEngine.Object.Destroy(Root);
		}
		Root = null;
		if (ComponentContainer != null)
		{
			ComponentContainer.TearDown();
		}
		ComponentContainer = null;
	}
}
