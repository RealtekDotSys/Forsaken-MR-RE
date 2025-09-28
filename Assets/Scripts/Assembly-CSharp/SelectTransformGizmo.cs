public class SelectTransformGizmo : global::UnityEngine.MonoBehaviour
{
	private global::UnityEngine.Transform highlight;

	private global::UnityEngine.Transform selection;

	private global::UnityEngine.RaycastHit raycastHit;

	private global::UnityEngine.RaycastHit raycastHitHandle;

	private global::UnityEngine.GameObject runtimeTransformGameObj;

	private global::RuntimeHandle.RuntimeTransformHandle runtimeTransformHandle;

	private int runtimeTransformLayer = 9;

	private int runtimeTransformLayerMask;

	public ModelConfigOCBase ocCreator;

	private void Start()
	{
		runtimeTransformGameObj = new global::UnityEngine.GameObject();
		runtimeTransformHandle = runtimeTransformGameObj.AddComponent<global::RuntimeHandle.RuntimeTransformHandle>();
		runtimeTransformGameObj.layer = runtimeTransformLayer;
		runtimeTransformLayerMask = 1 << runtimeTransformLayer;
		runtimeTransformHandle.type = global::RuntimeHandle.HandleType.POSITION;
		runtimeTransformHandle.space = global::RuntimeHandle.HandleSpace.LOCAL;
		runtimeTransformHandle.autoScale = true;
		runtimeTransformHandle.autoScaleFactor = 1f;
		runtimeTransformGameObj.SetActive(value: false);
	}

	private void Update()
	{
		if (highlight != null)
		{
			highlight = null;
		}
		global::UnityEngine.Ray ray = global::UnityEngine.Camera.main.ScreenPointToRay(global::UnityEngine.Input.mousePosition);
		if (global::UnityEngine.Physics.Raycast(ray, out raycastHit, 1000f))
		{
			highlight = raycastHit.transform;
			if (!highlight.CompareTag("Selectable") || !(highlight != selection))
			{
				highlight = null;
			}
		}
		if (!global::UnityEngine.Input.GetMouseButtonDown(0) || global::UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		ApplyLayerToChildren(runtimeTransformGameObj);
		if (global::UnityEngine.Physics.Raycast(ray, out raycastHit))
		{
			if (!global::UnityEngine.Physics.Raycast(ray, out raycastHitHandle, float.PositiveInfinity, runtimeTransformLayerMask))
			{
				if ((bool)highlight)
				{
					_ = selection != null;
					selection = raycastHit.transform;
					runtimeTransformHandle.target = selection;
					runtimeTransformGameObj.SetActive(value: true);
					ocCreator.SelectObject(selection.gameObject);
					highlight = null;
				}
				else if ((bool)selection)
				{
					selection = null;
					runtimeTransformGameObj.SetActive(value: false);
					ocCreator.DeselectObjects();
				}
			}
		}
		else if ((bool)selection)
		{
			selection = null;
			runtimeTransformGameObj.SetActive(value: false);
			ocCreator.DeselectObjects();
		}
	}

	public void SetHandleType(global::RuntimeHandle.HandleType type)
	{
		runtimeTransformHandle.type = type;
	}

	public void ForceSelection(global::UnityEngine.Transform selected)
	{
		selection = selected;
		runtimeTransformHandle.target = selected;
		runtimeTransformGameObj.SetActive(value: true);
	}

	public void ForceDeselect()
	{
		selection = null;
		runtimeTransformGameObj.SetActive(value: false);
	}

	private void ApplyLayerToChildren(global::UnityEngine.GameObject parentGameObj)
	{
		foreach (global::UnityEngine.Transform item in parentGameObj.transform)
		{
			int layer = runtimeTransformLayer;
			item.gameObject.layer = layer;
			foreach (global::UnityEngine.Transform item2 in item)
			{
				item2.gameObject.layer = layer;
				foreach (global::UnityEngine.Transform item3 in item2)
				{
					item3.gameObject.layer = layer;
					foreach (global::UnityEngine.Transform item4 in item3)
					{
						item4.gameObject.layer = layer;
						foreach (global::UnityEngine.Transform item5 in item4)
						{
							item5.gameObject.layer = layer;
						}
					}
				}
			}
		}
	}
}
