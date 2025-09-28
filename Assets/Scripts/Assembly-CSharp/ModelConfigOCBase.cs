public class ModelConfigOCBase : global::UnityEngine.MonoBehaviour
{
	private enum Mode
	{
		Position = 1,
		Rotation = 2,
		Scale = 3
	}

	[global::UnityEngine.Header("Material")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Material baseMaterial;

	[global::UnityEngine.HideInInspector]
	public global::UnityEngine.GameObject selectedObject;

	[global::UnityEngine.Header("Bone Objects")]
	public global::System.Collections.Generic.List<RigBone> bones;

	[global::UnityEngine.Header("UI Parents")]
	public global::UnityEngine.GameObject DeselectedUI;

	public global::UnityEngine.GameObject SelectedUI;

	[global::UnityEngine.Header("Relevant UI Elements")]
	public global::UnityEngine.UI.Button positionButton;

	public global::UnityEngine.UI.Button rotationButton;

	public global::UnityEngine.UI.Button scaleButton;

	public global::TMPro.TextMeshProUGUI hideMeshButtonText;

	public global::UnityEngine.UI.Slider XSlider;

	public global::UnityEngine.UI.Slider YSlider;

	public global::UnityEngine.UI.Slider ZSlider;

	public global::UnityEngine.UI.Slider RotationSlider;

	public global::TMPro.TMP_Dropdown BoneDropdown;

	public global::UnityEngine.Transform cameraMover;

	public global::UnityEngine.GameObject hideableMesh;

	public SelectTransformGizmo selectionHandler;

	public global::System.Collections.Generic.Dictionary<global::UnityEngine.GameObject, OCCreatorObject> objects = new global::System.Collections.Generic.Dictionary<global::UnityEngine.GameObject, OCCreatorObject>();

	private ModelConfigOCBase.Mode currentMode = ModelConfigOCBase.Mode.Position;

	private void Awake()
	{
		global::System.Collections.Generic.List<global::TMPro.TMP_Dropdown.OptionData> list = new global::System.Collections.Generic.List<global::TMPro.TMP_Dropdown.OptionData>();
		foreach (RigBone bone in bones)
		{
			list.Add(new global::TMPro.TMP_Dropdown.OptionData(bone.BoneName));
		}
		BoneDropdown.AddOptions(list);
	}

	public void AddShape(string type)
	{
		AddShape(type, BoneDropdown.options[BoneDropdown.value].text);
	}

	public void AddShape(string type, string parent)
	{
		if (type == "Cube")
		{
			AddShape(global::UnityEngine.PrimitiveType.Cube, parent);
		}
	}

	private void AddShape(global::UnityEngine.PrimitiveType type, string parent)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.CreatePrimitive(type);
		gameObject.GetComponent<global::UnityEngine.MeshRenderer>().material = baseMaterial;
		if (parent != null)
		{
			gameObject.transform.SetParent(GetBone(parent).transform);
		}
		gameObject.transform.localPosition = global::UnityEngine.Vector3.zero;
		gameObject.transform.localRotation = global::UnityEngine.Quaternion.identity;
		gameObject.transform.localScale = new global::UnityEngine.Vector3(0.3f, 0.3f, 0.3f);
		gameObject.layer = 8;
		gameObject.tag = "Selectable";
		OCCreatorObject value = new OCCreatorObject(type, parent);
		objects.Add(gameObject, value);
		SelectObject(gameObject);
	}

	private global::UnityEngine.GameObject GetBone(string name)
	{
		foreach (RigBone bone in bones)
		{
			if (bone.BoneName == name)
			{
				return bone.Bone;
			}
		}
		return null;
	}

	public void RemoveObject(global::UnityEngine.GameObject go)
	{
		objects.Remove(go);
		global::UnityEngine.Object.Destroy(go);
		DeselectObjects();
	}

	public void RemoveObject()
	{
		if (selectedObject != null)
		{
			objects.Remove(selectedObject);
			global::UnityEngine.Object.Destroy(selectedObject);
			selectedObject = null;
			DeselectObjects();
		}
	}

	public void SelectObject(global::UnityEngine.GameObject go)
	{
		if (objects.ContainsKey(go))
		{
			SelectedUI.SetActive(value: true);
			DeselectedUI.SetActive(value: false);
			selectedObject = go;
			SetHighlight(go, highlight: true);
			selectionHandler.ForceSelection(go.transform);
		}
	}

	public void DeselectObjects()
	{
		SelectedUI.SetActive(value: false);
		DeselectedUI.SetActive(value: true);
		selectionHandler.ForceDeselect();
		if (selectedObject != null)
		{
			SetHighlight(selectedObject, highlight: false);
		}
		selectedObject = null;
	}

	public void SetColor(global::UnityEngine.Color setColor)
	{
		if (selectedObject != null)
		{
			objects[selectedObject].color = setColor;
		}
	}

	public void SetHighlight(global::UnityEngine.GameObject go, bool highlight)
	{
	}

	public void SetPosition(string axis, float value)
	{
		switch (axis)
		{
		case "X":
			cameraMover.position = new global::UnityEngine.Vector3(value, cameraMover.position.y, cameraMover.position.z);
			break;
		case "Y":
			cameraMover.position = new global::UnityEngine.Vector3(cameraMover.position.x, value, cameraMover.position.z);
			break;
		case "Z":
			cameraMover.position = new global::UnityEngine.Vector3(cameraMover.position.x, cameraMover.position.y, value);
			break;
		}
	}

	public void SetRotation(float value)
	{
		base.transform.localEulerAngles = new global::UnityEngine.Vector3(0f, value, 0f);
	}

	public static float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
	{
		float num = from - fromMin;
		float num2 = fromMax - fromMin;
		float num3 = num / num2;
		return (toMax - toMin) * num3 + toMin;
	}

	private void UpdateModeButtons(ModelConfigOCBase.Mode mode)
	{
		switch (mode)
		{
		case ModelConfigOCBase.Mode.Position:
			positionButton.interactable = false;
			rotationButton.interactable = true;
			scaleButton.interactable = true;
			break;
		case ModelConfigOCBase.Mode.Rotation:
			positionButton.interactable = true;
			rotationButton.interactable = false;
			scaleButton.interactable = true;
			break;
		case ModelConfigOCBase.Mode.Scale:
			positionButton.interactable = true;
			rotationButton.interactable = true;
			scaleButton.interactable = false;
			break;
		}
	}

	public void PositionButtonPressed()
	{
		UpdateModeButtons(ModelConfigOCBase.Mode.Position);
		selectionHandler.SetHandleType(global::RuntimeHandle.HandleType.POSITION);
	}

	public void RotationButtonPressed()
	{
		UpdateModeButtons(ModelConfigOCBase.Mode.Rotation);
		selectionHandler.SetHandleType(global::RuntimeHandle.HandleType.ROTATION);
	}

	public void ScaleButtonPressed()
	{
		UpdateModeButtons(ModelConfigOCBase.Mode.Scale);
		selectionHandler.SetHandleType(global::RuntimeHandle.HandleType.SCALE);
	}

	public void XSliderChanged()
	{
		switch (currentMode)
		{
		case ModelConfigOCBase.Mode.Position:
			SetPosition("X", XSlider.value);
			break;
		case ModelConfigOCBase.Mode.Rotation:
		case ModelConfigOCBase.Mode.Scale:
			break;
		}
	}

	public void YSliderChanged()
	{
		switch (currentMode)
		{
		case ModelConfigOCBase.Mode.Position:
			SetPosition("Y", YSlider.value);
			break;
		case ModelConfigOCBase.Mode.Rotation:
		case ModelConfigOCBase.Mode.Scale:
			break;
		}
	}

	public void ZSliderChanged()
	{
		switch (currentMode)
		{
		case ModelConfigOCBase.Mode.Position:
			SetPosition("Z", ZSlider.value);
			break;
		case ModelConfigOCBase.Mode.Rotation:
		case ModelConfigOCBase.Mode.Scale:
			break;
		}
	}

	public void RotationSliderChanged()
	{
		SetRotation(RotationSlider.value);
	}

	private void UpdateSliderValues(global::UnityEngine.GameObject go)
	{
		if (go == null)
		{
			XSlider.value = 0f;
			YSlider.value = 0f;
			ZSlider.value = 0f;
		}
		else if (currentMode == ModelConfigOCBase.Mode.Position)
		{
			XSlider.value = go.transform.localPosition.x;
			YSlider.value = go.transform.localPosition.y;
			ZSlider.value = go.transform.localPosition.z;
		}
	}

	public void HideHideableMesh()
	{
		if (hideableMesh.activeSelf)
		{
			hideableMesh.SetActive(value: false);
			hideMeshButtonText.text = "Unhide\nMesh";
		}
		else
		{
			hideableMesh.SetActive(value: true);
			hideMeshButtonText.text = "Hide\nMesh";
		}
	}
}
