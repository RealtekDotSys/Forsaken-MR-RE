public class LogEventHandler
{
	private sealed class _003C_003Ec__DisplayClass19_0
	{
		public LogEventHandler _003C_003E4__this;

		public LogEventRequest request;

		public global::System.Action<ServerResponse> successCallback;

		public global::System.Action<ServerData> errorCallback;

		internal void _003CSend_003Eb__0(ServerResponse serverResponse)
		{
			_003C_003E4__this.ProcessSuccessCallback(request, serverResponse, successCallback);
		}

		internal void _003CSend_003Eb__1(ServerData serverData)
		{
			_003C_003E4__this.ProcessErrorCallback(request, serverData, errorCallback);
		}
	}

	private const float _minDelayBetweenCalls = 0.25f;

	private global::System.Collections.Generic.List<float> _timeRecord;

	private bool _generatePlayStreamEvent;

	private readonly global::System.Collections.Generic.HashSet<EventResponseHandler> eventResponseSet;

	private readonly IExecuteScriptRequest _executeScriptRequest;

	private readonly EventExposer _eventExposer;

	private global::System.Collections.Generic.Queue<EventRequestExecutor> _unhandledPendingRequests;

	private global::System.Collections.Generic.Queue<EventRequestExecutor> _passivePendingRequests;

	private global::System.Collections.Generic.List<EventRequestExecutor> _inflightRequestList;

	private global::System.Collections.Generic.HashSet<string> _alwaysGeneratePlayStreamEventList;

	private float _lastRequestExecutorTimestamp;

	private global::System.Collections.Generic.List<string> _passiveRequestList;

	public global::System.Collections.Generic.List<EventRequestExecutor> GetInflightRequestList()
	{
		return _inflightRequestList;
	}

	public global::System.Collections.Generic.Queue<EventRequestExecutor> GetPassivePendingRequests()
	{
		return _passivePendingRequests;
	}

	public LogEventHandler(global::System.Collections.Generic.HashSet<EventResponseHandler> responseSet, EventExposer eventExposer, IExecuteScriptRequest executeScriptRequest)
	{
		_timeRecord = new global::System.Collections.Generic.List<float>();
		_unhandledPendingRequests = new global::System.Collections.Generic.Queue<EventRequestExecutor>();
		_passivePendingRequests = new global::System.Collections.Generic.Queue<EventRequestExecutor>();
		_inflightRequestList = new global::System.Collections.Generic.List<EventRequestExecutor>();
		_alwaysGeneratePlayStreamEventList = new global::System.Collections.Generic.HashSet<string> { "LOG_ACCOUNT_ALTER_DETAILS" };
		eventResponseSet = responseSet;
		_executeScriptRequest = executeScriptRequest;
		_eventExposer = eventExposer;
	}

	private void OnGeneratePlayStreamEvent(bool obj)
	{
		_generatePlayStreamEvent = obj;
	}

	private void OnPassiveRequestListReceived(global::System.Collections.Generic.List<string> list)
	{
		_passiveRequestList = list;
	}

	public void ProcessScriptData(ServerData scriptData)
	{
		ProcessServerData(scriptData);
	}

	public void Send(LogEventRequest request, global::System.Action<ServerResponse> successCallback, global::System.Action<ServerData> errorCallback)
	{
		LogEventHandler._003C_003Ec__DisplayClass19_0 _003C_003Ec__DisplayClass19_ = new LogEventHandler._003C_003Ec__DisplayClass19_0();
		_003C_003Ec__DisplayClass19_.request = request;
		_003C_003Ec__DisplayClass19_._003C_003E4__this = this;
		_003C_003Ec__DisplayClass19_.successCallback = successCallback;
		_003C_003Ec__DisplayClass19_.errorCallback = errorCallback;
		EventRequestExecutor.RequestType type = (IsInPassiveRequestList(request) ? EventRequestExecutor.RequestType.Unhandled : EventRequestExecutor.RequestType.Passive);
		_generatePlayStreamEvent = ShouldAlwaysGeneratePlayStreamEvent(request.GetEventKey());
		FireOrQueueRequestExecutor(new EventRequestExecutor(request, ProcessServerData, _003C_003Ec__DisplayClass19_._003CSend_003Eb__0, _003C_003Ec__DisplayClass19_._003CSend_003Eb__1, type, _eventExposer, _generatePlayStreamEvent, _executeScriptRequest));
	}

	private bool ShouldAlwaysGeneratePlayStreamEvent(string eventKey)
	{
		if (string.IsNullOrEmpty(eventKey))
		{
			return false;
		}
		if (_alwaysGeneratePlayStreamEventList != null)
		{
			return _alwaysGeneratePlayStreamEventList.Contains(eventKey);
		}
		return false;
	}

	private bool IsInPassiveRequestList(LogEventRequest request)
	{
		object value = null;
		if (_passiveRequestList != null)
		{
			request.JSONData.TryGetValue("eventKey", out value);
			if (value != null)
			{
				return _passiveRequestList.Contains(value.ToString());
			}
		}
		return false;
	}

	private void FireOrQueueRequestExecutor(EventRequestExecutor executor)
	{
		global::UnityEngine.Debug.LogError("FireOrQueueRequestExecutor engaged for request: " + executor.Request.GetEventKey());
		if (CanFireExecutorNow(executor))
		{
			global::UnityEngine.Debug.LogError("firing now.");
			FireExecutorRequest(executor);
		}
		else if (executor.requestType == EventRequestExecutor.RequestType.Passive && _passivePendingRequests != null)
		{
			global::UnityEngine.Debug.LogError("queuing to passive");
			_passivePendingRequests.Enqueue(executor);
		}
		else if (_unhandledPendingRequests == null)
		{
			global::UnityEngine.Debug.LogError("unhandled requests is null");
		}
		else
		{
			global::UnityEngine.Debug.LogError("queuing to unhandled");
			_unhandledPendingRequests.Enqueue(executor);
		}
	}

	private bool CanFireExecutorNow(EventRequestExecutor executor)
	{
		if (executor.requestType == EventRequestExecutor.RequestType.Passive)
		{
			return CanFirePassiveRequestNow();
		}
		return CanFireUnhandledRequestNow();
	}

	private bool CanFireUnhandledRequestNow()
	{
		if (!ShouldExecutorWaitForDelay())
		{
			return !AreUnhandledExecutorsPending();
		}
		return false;
	}

	private bool CanFirePassiveRequestNow()
	{
		if (!ShouldExecutorWaitForDelay() && !IsAnExecutorInFlight())
		{
			return !AreAnyExecutorsPending();
		}
		return false;
	}

	public bool ShouldExecutorWaitForDelay()
	{
		return _lastRequestExecutorTimestamp + 0.25f > global::UnityEngine.Time.time;
	}

	public bool AreUnhandledExecutorsPending()
	{
		return _unhandledPendingRequests.Count > 0;
	}

	public bool IsAnExecutorInFlight()
	{
		return _inflightRequestList.Count > 0;
	}

	private bool AreAnyExecutorsPending()
	{
		if (_unhandledPendingRequests.Count > 0)
		{
			return true;
		}
		return _passivePendingRequests.Count > 0;
	}

	private void FireExecutorRequest(EventRequestExecutor executor)
	{
		_lastRequestExecutorTimestamp = global::UnityEngine.Time.time;
		_inflightRequestList.Add(executor);
		AddToTimeRecord(_lastRequestExecutorTimestamp);
		global::UnityEngine.Debug.LogError("Firing Executor Request: " + executor.Request.GetEventKey());
		executor.Send();
	}

	private void AddToTimeRecord(float time)
	{
		_timeRecord.Add(time);
		if (_timeRecord.Count >= 21)
		{
			_timeRecord.RemoveRange(0, 10);
		}
	}

	private void FirePendingRequest()
	{
		if (CanFireQueuedUnhandledEvent() && _unhandledPendingRequests != null)
		{
			FireExecutorRequest(_unhandledPendingRequests.Dequeue());
		}
		else if (CanFireQueuedPassiveEvent() && _passivePendingRequests != null)
		{
			FireExecutorRequest(_passivePendingRequests.Dequeue());
		}
	}

	public bool CanFireQueuedUnhandledEvent()
	{
		if (AreUnhandledExecutorsPending())
		{
			return !ShouldExecutorWaitForDelay();
		}
		return false;
	}

	public bool CanFireQueuedPassiveEvent()
	{
		if (ArePassiveExecutorsPending() && !IsAnExecutorInFlight())
		{
			return !ShouldExecutorWaitForDelay();
		}
		return false;
	}

	public bool ArePassiveExecutorsPending()
	{
		return _passivePendingRequests.Count > 0;
	}

	private void FireQueuedRequest(EventRequestExecutor executor)
	{
		FireExecutorRequest(executor);
	}

	private void ProcessServerData(ServerData scriptData)
	{
		foreach (EventResponseHandler item in eventResponseSet)
		{
			item.TryHandleResponse(scriptData);
		}
	}

	private void ProcessSuccessCallback(LogEventRequest request, ServerResponse serverResponse, global::System.Action<ServerResponse> successCallback)
	{
		RemoveRequestExecutor(request);
		if (!serverResponse.HasErrors)
		{
			successCallback?.Invoke(serverResponse);
		}
	}

	private void RemoveRequestExecutor(LogEventRequest request)
	{
		int num = -1;
		foreach (EventRequestExecutor inflightRequest in _inflightRequestList)
		{
			if (inflightRequest.Request == request)
			{
				num = _inflightRequestList.IndexOf(inflightRequest);
			}
		}
		if (num != -1)
		{
			_inflightRequestList.RemoveAt(num);
		}
	}

	private void ProcessErrorCallback(LogEventRequest request, ServerData serverData, global::System.Action<ServerData> errorCallback)
	{
		RemoveRequestExecutor(request);
		errorCallback?.Invoke(serverData);
	}

	public void Update()
	{
		FirePendingRequest();
	}
}
