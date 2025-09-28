public class GameLifecycleProxy : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private GameUnityHooks _unityHooks;

	private global::System.Collections.Generic.List<global::System.Action> _registeredTeardownCallbacks;

	private GameLifecycleHandler _gameLifecycleHandler;

	private static MasterDomain _masterDomain;

	public StartupHandler StartupHandler
	{
		get
		{
			if (_gameLifecycleHandler != null)
			{
				return _gameLifecycleHandler.StartupHandler;
			}
			global::UnityEngine.Debug.Log("GameLifecycleProxy GetStartupHandler - lifecycle handler is just.. gone bro sorry");
			return _gameLifecycleHandler.StartupHandler;
		}
	}

	public ShutdownHandler ShutdownHandler
	{
		get
		{
			if (_gameLifecycleHandler != null)
			{
				return _gameLifecycleHandler.ShutdownHandler;
			}
			global::UnityEngine.Debug.Log("GameLifecycleProxy GetShutdownHandler - lifecycle handler is just.. gone bro sorry");
			return _gameLifecycleHandler.ShutdownHandler;
		}
	}

	public static MasterDomain GetMasterDomain()
	{
		if (_masterDomain != null)
		{
			return _masterDomain;
		}
		global::UnityEngine.Debug.LogError("GameLifecycleProxy GetMasterDomain - Request for MasterDomain before it is ready!");
		return _masterDomain;
	}

	public void ForceShutdown()
	{
		if (_gameLifecycleHandler != null)
		{
			_gameLifecycleHandler.ShutdownHandler.ShutdownGame();
			_gameLifecycleHandler.Teardown();
			_gameLifecycleHandler = null;
		}
	}

	private void Awake()
	{
		_masterDomain = new MasterDomain(new EventExposer(), _unityHooks);
		_registeredTeardownCallbacks = new global::System.Collections.Generic.List<global::System.Action>();
		global::UnityEngine.Debug.Log("Creating startup paramaters");
		StartupParameters startupParameters = new StartupParameters();
		startupParameters.HostMonobehavior = this;
		startupParameters.HasToSBeenAcceptedYet = global::UnityEngine.PlayerPrefs.GetInt("PrivacyPolicyTermsOfServiceAcceptedKey", 0) == 1;
		global::System.Func<MasterDomain> masterDomainGetter = GetMasterDomain;
		startupParameters.MasterDomainGetter = masterDomainGetter;
		startupParameters.GameLifecycleProxy = this;
		startupParameters.RegisteredTeardownCallbacks = _registeredTeardownCallbacks;
		startupParameters.UnityHooks = _unityHooks;
		global::UnityEngine.Debug.Log("Creating shutdown parameters");
		ShutdownParameters shutdownParameters = new ShutdownParameters();
		shutdownParameters.MasterDomainGetter = GetMasterDomain;
		shutdownParameters.RegisteredTeardownCallbacks = _registeredTeardownCallbacks;
		_gameLifecycleHandler = new GameLifecycleHandler(startupParameters, shutdownParameters);
		global::UnityEngine.Debug.Log("Calling startupgame");
		_gameLifecycleHandler.StartupHandler.StartupGame();
	}

	private void OnDestroy()
	{
		ForceShutdown();
	}

	private void OnApplicationQuit()
	{
		if (GetMasterDomain() != null)
		{
			MasterDomain masterDomain = GetMasterDomain();
			if (masterDomain.AnimatronicEntityDomain != null)
			{
				masterDomain.AnimatronicEntityDomain.HandleApplicationQuit();
			}
			masterDomain.AttackSequenceDomain.OnApplicationQuit();
		}
	}

	private void Update()
	{
		if (GetMasterDomain() != null)
		{
			MasterDomain masterDomain = GetMasterDomain();
			if (masterDomain.UpdateHandler != null)
			{
				masterDomain.UpdateHandler.OnUnityFrameUpdate(global::UnityEngine.Time.deltaTime);
			}
		}
	}

	private void OnApplicationPause(bool paused)
	{
		GetMasterDomain().AnimatronicEntityDomain.HandleApplicationPause(paused);
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		MasterDomain masterDomain = GetMasterDomain();
		if (masterDomain != null)
		{
			if (masterDomain.AnimatronicEntityDomain != null)
			{
				masterDomain.AnimatronicEntityDomain.HandleApplicationFocus(hasFocus);
			}
			_ = masterDomain.eventExposer;
			if (masterDomain.eventExposer != null)
			{
				masterDomain.eventExposer.OnApplicationFocus(hasFocus);
			}
		}
	}
}
