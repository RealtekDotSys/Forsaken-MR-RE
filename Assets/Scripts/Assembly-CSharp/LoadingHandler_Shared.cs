public class LoadingHandler_Shared : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Loading Screen Objects")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject loadingScreen;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Slider loadingSlider;

	private MasterDomain domain;

	private bool loading;

	private global::UnityEngine.AsyncOperation currOperation;

	private void Awake()
	{
		domain = MasterDomain.GetDomain();
		domain.eventExposer.add_SceneLoading(SceneLoading);
	}

	private void SceneLoading(global::UnityEngine.AsyncOperation operation)
	{
		if (currOperation == null && operation != null)
		{
			currOperation = operation;
			operation.completed += OnSceneCompleted;
			loadingScreen.SetActive(value: true);
			StartCoroutine("UpdateLoadBar");
		}
	}

	private void OnSceneCompleted(global::UnityEngine.AsyncOperation operation)
	{
		operation.completed -= OnSceneCompleted;
		currOperation = null;
		loadingScreen.SetActive(value: false);
	}

	private global::System.Collections.IEnumerator UpdateLoadBar()
	{
		while (currOperation != null && !currOperation.isDone)
		{
			loadingSlider.value = currOperation.progress;
			yield return null;
		}
	}
}
