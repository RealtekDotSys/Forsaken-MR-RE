public class GenericCreationRequest
{
	private global::System.Action<GenericCreationRequest> OnRequestComplete;

	private bool _spawnComplete;

	private bool _cancelled;

	public string Bundle { get; }

	public string Asset { get; }

	public global::UnityEngine.GameObject SpawnedObject { get; set; }

	public bool IsComplete { get; set; }

	public void add_OnRequestComplete(global::System.Action<GenericCreationRequest> request)
	{
		OnRequestComplete = (global::System.Action<GenericCreationRequest>)global::System.Delegate.Combine(OnRequestComplete, request);
	}

	public void remove_OnRequestComplete(global::System.Action<GenericCreationRequest> request)
	{
		OnRequestComplete = (global::System.Action<GenericCreationRequest>)global::System.Delegate.Remove(OnRequestComplete, request);
	}

	public GenericCreationRequest(string bundle, string asset)
	{
		Bundle = bundle;
		Asset = asset;
		SpawnedObject = null;
		IsComplete = false;
		MasterDomain.GetDomain().eventExposer.add_GameDisplayChange(SceneChanged);
	}

	private void SceneChanged(GameDisplayData data)
	{
		global::UnityEngine.Debug.LogError("Scene changed while instantiating object " + Bundle + "_" + Asset);
		CancelRequest();
	}

	public void SetObjectCreationSuccess(global::UnityEngine.GameObject spawnedObj)
	{
		_spawnComplete = true;
		SpawnedObject = spawnedObj;
		TryToNotifyComplete();
	}

	public void SetObjectCreationFailure()
	{
		_spawnComplete = true;
		SpawnedObject = null;
		TryToNotifyComplete();
	}

	public void CancelRequest()
	{
		global::UnityEngine.Debug.Log("Generic instantiate req cancelled");
		_cancelled = true;
	}

	public bool IsCancelled()
	{
		return _cancelled;
	}

	private void TryToNotifyComplete()
	{
		if (!_spawnComplete)
		{
			global::UnityEngine.Debug.LogError("Not Complete.");
			return;
		}
		IsComplete = true;
		if (_cancelled)
		{
			MasterDomain.GetDomain().GameAssetManagementDomain.ReleaseGenericSpawn(SpawnedObject);
			global::UnityEngine.Object.Destroy(SpawnedObject);
			SpawnedObject = null;
		}
		if (OnRequestComplete != null)
		{
			OnRequestComplete(this);
		}
		OnRequestComplete = null;
		MasterDomain.GetDomain().eventExposer.remove_GameDisplayChange(SceneChanged);
	}
}
