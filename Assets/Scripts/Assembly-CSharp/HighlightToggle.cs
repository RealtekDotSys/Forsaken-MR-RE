public class HighlightToggle : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject selectedObject;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject unselectedObject;

	[global::UnityEngine.SerializeField]
	private bool autoSelectOnButtonClick = true;

	[global::UnityEngine.SerializeField]
	private bool unselectSiblingsOnSelected;

	private void SetSelected(bool value)
	{
		if (selectedObject != null)
		{
			selectedObject.SetActive(value);
		}
		if (!(unselectedObject == null))
		{
			unselectedObject.SetActive(!value);
		}
	}

	private void UnselectSiblings()
	{
		HighlightToggle[] componentsInChildren = base.transform.parent.GetComponentsInChildren<HighlightToggle>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetHighlight(value: false);
		}
	}

	public void SetHighlight(bool value)
	{
		SetSelected(value);
	}

	public void SetHighlightAndOtherCellsHighlightState(bool value)
	{
		if (unselectSiblingsOnSelected)
		{
			UnselectSiblings();
		}
		SetSelected(value);
	}

	private void Awake()
	{
		if (autoSelectOnButtonClick && base.gameObject.GetComponent<global::UnityEngine.UI.Button>() != null)
		{
			global::UnityEngine.Debug.Log("found button for highlight " + base.gameObject.name);
			base.gameObject.GetComponent<global::UnityEngine.UI.Button>().onClick.AddListener(ButtonPress);
		}
	}

	private void ButtonPress()
	{
		global::UnityEngine.Debug.Log("clicked highlight");
		SetHighlightAndOtherCellsHighlightState(value: true);
	}
}
