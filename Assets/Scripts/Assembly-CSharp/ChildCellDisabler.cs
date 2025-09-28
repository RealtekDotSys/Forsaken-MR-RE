public class ChildCellDisabler : global::UnityEngine.MonoBehaviour
{
	private const string ClassName = "ChildCellDisabler";

	[global::UnityEngine.SerializeField]
	private float _disableTime;

	private global::System.Collections.Generic.List<IDisableableChildCell> _children;

	private bool _isDisabling;

	public void OnChildAdded(IDisableableChildCell child)
	{
		_children.Add(child);
		child.UpdateEnableState(!_isDisabling);
	}

	public void OnChildRemoved(IDisableableChildCell child)
	{
		_children.Remove(child);
	}

	public void Invoke()
	{
		if (_isDisabling)
		{
			global::UnityEngine.Debug.LogError("ChildCellDisabler Invoke - Attempt to invoke childCellDisabler that is already in a disabled state");
		}
		_isDisabling = true;
		foreach (IDisableableChildCell child in _children)
		{
			child.UpdateEnableState(state: false);
		}
		StartCoroutine(WaitThenReenable(_disableTime));
	}

	private global::System.Collections.IEnumerator WaitThenReenable(float delay)
	{
		yield return new global::UnityEngine.WaitForSeconds(delay);
		foreach (IDisableableChildCell child in _children)
		{
			child.UpdateEnableState(state: true);
		}
		_isDisabling = false;
	}

	public ChildCellDisabler()
	{
		_children = new global::System.Collections.Generic.List<IDisableableChildCell>();
	}
}
