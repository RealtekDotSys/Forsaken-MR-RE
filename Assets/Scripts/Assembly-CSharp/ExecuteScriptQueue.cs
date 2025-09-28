public class ExecuteScriptQueue : IExecuteScriptRequest
{
	private readonly global::System.Collections.Generic.Queue<ExecuteScriptParameters> _queue;

	private readonly IExecuteScriptRequest _scriptRequest;

	private bool _executing;

	private ExecuteScriptParameters _currentParameters;

	private readonly ExecuteScriptParameters _queueParameters;

	public ExecuteScriptQueue(IExecuteScriptRequest request)
	{
		_queue = new global::System.Collections.Generic.Queue<ExecuteScriptParameters>();
		_queueParameters = new ExecuteScriptParameters();
		if (request != null)
		{
			_scriptRequest = request;
			_queueParameters.resultCallback = OnRequestResult;
			_queueParameters.errorCallback = OnRequestError;
		}
		else
		{
			new global::System.NullReferenceException("request");
		}
	}

	public void ExecuteParameters(ExecuteScriptParameters parameters)
	{
		global::UnityEngine.Debug.LogError("execute in queue executor");
		_queue.Enqueue(parameters);
		if (!_executing)
		{
			ExecuteNextRequest();
		}
	}

	private void OnRequestResult(ExecuteScriptResult executeScriptResult)
	{
		_currentParameters.resultCallback(executeScriptResult);
		HandleQueue();
	}

	private void OnRequestError(IllumixErrorData illumixErrorData)
	{
		_currentParameters.errorCallback(illumixErrorData);
		HandleQueue();
	}

	private void HandleQueue()
	{
		if (_queue.Count >= 1)
		{
			ExecuteNextRequest();
		}
		else
		{
			_executing = false;
		}
	}

	private void ExecuteNextRequest()
	{
		_currentParameters = _queue.Dequeue();
		_executing = true;
		_queueParameters.functionName = _currentParameters.functionName;
		_queueParameters.functionParameter = _currentParameters.functionParameter;
		_queueParameters.authContext = _currentParameters.authContext;
		_queueParameters.generatePlayStreamEvent = _currentParameters.generatePlayStreamEvent;
		global::UnityEngine.Debug.LogError("Executing to limiter execute");
		_scriptRequest.ExecuteParameters(_queueParameters);
	}
}
