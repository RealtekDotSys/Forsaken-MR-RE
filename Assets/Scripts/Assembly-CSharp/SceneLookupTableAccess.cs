public class SceneLookupTableAccess : global::UnityEngine.MonoBehaviour
{
	public delegate void ReturnWorkshopSceneLookupTable(WorkshopSceneLookupTable workshopSceneLookupTable);

	public delegate void ReturnCameraSceneLookupTable(CameraSceneLookupTable cameraSceneLookupTable);

	private WorkshopSceneLookupTable _workshopSceneLookupTable;

	private CameraSceneLookupTable _cameraSceneLookupTable;

	private global::System.Collections.Generic.List<SceneLookupTableAccess.ReturnWorkshopSceneLookupTable> _returnWorkshopSceneLookupTableCallbacks;

	private global::System.Collections.Generic.List<SceneLookupTableAccess.ReturnCameraSceneLookupTable> _returnCameraSceneLookupTableCallbacks;

	private bool foundCam;

	private bool foundWork;

	public void GetWorkshopSceneLookupTableAsync(SceneLookupTableAccess.ReturnWorkshopSceneLookupTable returnWorkshopSceneLookupTableCallback)
	{
		AddWorkshopSceneLookupTableCallback(returnWorkshopSceneLookupTableCallback);
		TryCallWorkshopSceneLookupTableCallbacks();
	}

	public void GetCameraSceneLookupTableAsync(SceneLookupTableAccess.ReturnCameraSceneLookupTable returnCameraSceneLookupTableCallback)
	{
		AddCameraSceneLookupTableCallback(returnCameraSceneLookupTableCallback);
		TryCallCameraSceneLookupTableCallbacks();
	}

	public void UnloadMapSceneLookupTable()
	{
	}

	private void AddWorkshopSceneLookupTableCallback(SceneLookupTableAccess.ReturnWorkshopSceneLookupTable returnWorkshopSceneLookupTableCallback)
	{
		_returnWorkshopSceneLookupTableCallbacks.Add(returnWorkshopSceneLookupTableCallback);
	}

	private void TryCallWorkshopSceneLookupTableCallbacks()
	{
		if (_workshopSceneLookupTable == null || !foundWork)
		{
			return;
		}
		foreach (SceneLookupTableAccess.ReturnWorkshopSceneLookupTable returnWorkshopSceneLookupTableCallback in _returnWorkshopSceneLookupTableCallbacks)
		{
			returnWorkshopSceneLookupTableCallback(_workshopSceneLookupTable);
		}
		_returnWorkshopSceneLookupTableCallbacks.Clear();
	}

	private void AddCameraSceneLookupTableCallback(SceneLookupTableAccess.ReturnCameraSceneLookupTable returnCameraSceneLookupTableCallback)
	{
		_returnCameraSceneLookupTableCallbacks.Remove(returnCameraSceneLookupTableCallback);
		_returnCameraSceneLookupTableCallbacks.Add(returnCameraSceneLookupTableCallback);
	}

	private void TryCallCameraSceneLookupTableCallbacks()
	{
		if (_cameraSceneLookupTable == null || !foundCam)
		{
			return;
		}
		foreach (SceneLookupTableAccess.ReturnCameraSceneLookupTable returnCameraSceneLookupTableCallback in _returnCameraSceneLookupTableCallbacks)
		{
			returnCameraSceneLookupTableCallback(_cameraSceneLookupTable);
		}
		_returnCameraSceneLookupTableCallbacks.Clear();
	}

	private void TryCallSplashSceneLookupTableCallbacks()
	{
	}

	private void Update()
	{
		if (global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Encounter" || global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ScavengingUI")
		{
			if (_cameraSceneLookupTable == null || !foundCam)
			{
				global::UnityEngine.Debug.LogWarning("Grabbed camera scene lookup table");
				_cameraSceneLookupTable = global::UnityEngine.GameObject.Find("CameraSceneLookupTable").GetComponent<CameraSceneLookupTable>();
				foundCam = true;
			}
		}
		else
		{
			foundCam = false;
		}
		if (global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
		{
			if (_workshopSceneLookupTable == null || !foundWork)
			{
				_workshopSceneLookupTable = global::UnityEngine.GameObject.Find("WorkshopSceneLookupTable").GetComponent<WorkshopSceneLookupTable>();
				foundWork = true;
			}
		}
		else
		{
			foundWork = false;
		}
		TryCallWorkshopSceneLookupTableCallbacks();
		TryCallCameraSceneLookupTableCallbacks();
	}

	public SceneLookupTableAccess()
	{
		_returnWorkshopSceneLookupTableCallbacks = new global::System.Collections.Generic.List<SceneLookupTableAccess.ReturnWorkshopSceneLookupTable>();
		_returnCameraSceneLookupTableCallbacks = new global::System.Collections.Generic.List<SceneLookupTableAccess.ReturnCameraSceneLookupTable>();
	}
}
