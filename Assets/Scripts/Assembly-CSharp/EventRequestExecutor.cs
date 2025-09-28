public class EventRequestExecutor
{
	public enum RequestType
	{
		Passive = 0,
		Unhandled = 1
	}

	private const string ClassName = "EventRequestExecutor";

	private readonly global::System.Action<ServerData> _processDataCallback;

	private readonly global::System.Action<ServerResponse> _successCallback;

	private readonly global::System.Action<ServerData> _errorCallback;

	private readonly EventExposer _eventExposer;

	private readonly bool _generatePlayStreamEvent;

	public readonly IExecuteScriptRequest ExecuteScriptRequest;

	private int retries;

	private LogEventRequest _logEventRequest;

	private string _eventKey;

	public readonly EventRequestExecutor.RequestType requestType;

	private const int _outOfOrderLogThreshold = 5;

	public LogEventRequest Request => _logEventRequest;

	public EventRequestExecutor(LogEventRequest logEventRequest, global::System.Action<ServerData> processDataCallback, global::System.Action<ServerResponse> successCallback, global::System.Action<ServerData> errorCallback, EventRequestExecutor.RequestType type, EventExposer eventExposer, bool generatePlayStreamEvent, IExecuteScriptRequest executeScriptRequest)
	{
		_logEventRequest = logEventRequest;
		_processDataCallback = processDataCallback;
		_successCallback = successCallback;
		_errorCallback = errorCallback;
		requestType = type;
		ExecuteScriptRequest = executeScriptRequest;
		_eventExposer = eventExposer;
		_generatePlayStreamEvent = generatePlayStreamEvent;
	}

	public void Send()
	{
		ServerDomain.ExecuteCloudScriptCount++;
		ExecuteFunctionWithParameters(GetEventKey(), GenerateParameters());
	}

	private global::PlayFab.Json.JsonObject GenerateParameters()
	{
		_logEventRequest.SetEventAttribute("executeCloudScriptCount", ServerDomain.ExecuteCloudScriptCount);
		_logEventRequest.SetEventAttribute("currentSessionToken", ServerDomain.CurrentSessionToken);
		return _logEventRequest.JSONData;
	}

	private string GetEventKey()
	{
		_eventKey = _logEventRequest.GetEventKey();
		return _logEventRequest.GetEventKey();
	}

	private void ExecuteFunctionWithParameters(string name, global::PlayFab.Json.JsonObject parameters)
	{
		ExecuteScriptParameters executeScriptParameters = new ExecuteScriptParameters();
		executeScriptParameters.functionName = name;
		executeScriptParameters.functionParameter = parameters;
		executeScriptParameters.authContext = ServerDomain.IllumixAuthContext;
		executeScriptParameters.errorCallback = ProcessIllumixErrors;
		executeScriptParameters.resultCallback = ProcessIllumixResponse;
		executeScriptParameters.generatePlayStreamEvent = _generatePlayStreamEvent;
		global::UnityEngine.Debug.LogError("executing func with params");
		ExecuteScriptRequest.ExecuteParameters(executeScriptParameters);
	}

	private void ProcessIllumixErrors(IllumixErrorData errorData)
	{
		global::PlayFab.Json.JsonObject jsonObject = new global::PlayFab.Json.JsonObject();
		jsonObject.Add("error", errorData.errorCode);
		jsonObject.Add("errorMessage", errorData.errorMessage);
		HandleErrors(new ServerData(jsonObject));
	}

	private void ProcessIllumixResponse(ExecuteScriptResult responseData)
	{
		ServerData serverData;
		if (responseData.HasError())
		{
			global::PlayFab.Json.JsonObject jsonObject = new global::PlayFab.Json.JsonObject();
			jsonObject.Add("Error", responseData.error.ToJson());
			serverData = new ServerData(new global::PlayFab.Json.JsonObject { { "scriptError", jsonObject } });
		}
		else
		{
			serverData = new ServerData(responseData.functionResult);
			CheckCloudScriptExecutionOrderIllumix(serverData);
		}
		ProcessIllumixResponse(new ServerResponse(serverData));
	}

	private void CheckCloudScriptExecutionOrderIllumix(ServerData data)
	{
	}

	private void ProcessIllumixResponse(ServerResponse response)
	{
		if (response.HasErrors)
		{
			HandleErrors(response.Errors);
		}
		if (_successCallback != null)
		{
			_successCallback(response);
		}
		if (response.ScriptData != null && _processDataCallback != null)
		{
			_processDataCallback(response.ScriptData);
		}
	}

	private void ProcessResponse(ServerData serverResponse)
	{
		ServerResponse serverResponse2 = new ServerResponse(serverResponse);
		if (serverResponse2.HasErrors)
		{
			HandleErrors(serverResponse2.Errors);
			return;
		}
		if (_successCallback != null)
		{
			_successCallback(serverResponse2);
		}
		if (serverResponse2.ScriptData != null && _processDataCallback != null)
		{
			_processDataCallback(serverResponse2.ScriptData);
		}
	}

	private void HandleErrors(ServerData serverData)
	{
		if (serverData.ContainsKey("error"))
		{
			GenericDialogData genericDialogData = new GenericDialogData
			{
				title = "CLOUD SCRIPT ERROR",
				message = serverData.GetString("error"),
				neutralButtonText = "CLOSE"
			};
			MasterDomain.GetDomain().eventExposer.GenericDialogRequest(genericDialogData);
			global::UnityEngine.Debug.LogError("Server Error: " + serverData.GetString("error"));
			if (serverData.GetString("error") == "timeout")
			{
				HandleTimeout();
				return;
			}
		}
		if (serverData.ContainsKey("Error"))
		{
			GenericDialogData genericDialogData2 = new GenericDialogData
			{
				title = "CLOUD SCRIPT ERROR",
				message = serverData.GetString("Error"),
				neutralButtonText = "CLOSE"
			};
			MasterDomain.GetDomain().eventExposer.GenericDialogRequest(genericDialogData2);
			global::UnityEngine.Debug.LogError("Server Error: " + serverData.GetString("Error"));
			if (serverData.GetString("Error") == null)
			{
				global::UnityEngine.Debug.LogError("Server Error: " + serverData.GetServerData("Error").GetString("Error"));
				if (serverData.GetServerData("Error").GetString("Error") == "timeout")
				{
					HandleTimeout();
				}
				return;
			}
			if (serverData.GetString("Error") == "timeout")
			{
				HandleTimeout();
				return;
			}
		}
		if (_errorCallback != null)
		{
			_errorCallback(serverData);
		}
	}

	private void HandleTimeout()
	{
		if (retries > 2)
		{
			global::UnityEngine.Debug.LogError("EventRequestExecutor ProcessResponse - HIT MAXIMUM Retries!");
			if (_errorCallback != null)
			{
				_errorCallback(null);
			}
		}
		else
		{
			retries++;
			Send();
		}
	}

	private void HandleRateLimitExceeded()
	{
	}
}
