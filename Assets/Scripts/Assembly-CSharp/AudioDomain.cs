public class AudioDomain
{
	private enum Game_Mode
	{
		None = 0,
		Map = 1,
		Encounter = 2,
		Reward = 3,
		Camera = 4,
		Remnant = 5,
		Ad = 6,
		Store = 7,
		Inbox = 8,
		Scavenging = 9
	}

	private const string WwiseInitializerResourcesPath = "Audio/WwiseInitializer";

	private global::UnityEngine.GameObject _initializerPrefab;

	private readonly global::UnityEngine.Transform _domainParent;

	private global::FMODUnity.StudioListener _listener;

	private AudioPlayer player;

	public SoundBankLoader SoundBankLoader;

	public bool IsReady => true;

	public AnimatronicAudioManager MakeNewEmitter(string name)
	{
		global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject(name);
		gameObject.transform.parent = _listener.transform;
		gameObject.transform.localPosition = global::UnityEngine.Vector3.zero;
		gameObject.transform.localEulerAngles = global::UnityEngine.Vector3.zero;
		return gameObject.AddComponent<AnimatronicAudioManager>();
	}

	public void SetEmittersListener(AnimatronicAudioManager emitter)
	{
	}

	public void SetListenerParent(global::UnityEngine.Transform parent)
	{
		_listener.transform.parent = parent;
		_listener.transform.localPosition = global::UnityEngine.Vector3.zero;
		_listener.transform.localEulerAngles = global::UnityEngine.Vector3.zero;
	}

	public global::UnityEngine.Transform GetListenerParent()
	{
		return _listener.transform.parent;
	}

	public void SetState(string group, string state)
	{
		if (state == "MaskOn")
		{
			global::FMODUnity.RuntimeManager.PlayOneShot("event:/MaskState");
			global::FMODUnity.RuntimeManager.StudioSystem.setParameterByName(group, 0f);
		}
		if (state == "MaskOff")
		{
			global::FMODUnity.RuntimeManager.StudioSystem.setParameterByName(group, 1f);
		}
		if (group == "Game_Mode")
		{
			AudioDomain.Game_Mode game_Mode = (AudioDomain.Game_Mode)global::System.Enum.Parse(typeof(AudioDomain.Game_Mode), state);
			int num = (int)game_Mode;
			global::UnityEngine.Debug.Log("Setting Game_Mode to " + state + " index " + num);
			global::FMODUnity.RuntimeManager.StudioSystem.setParameterByName(group, (float)game_Mode);
		}
	}

	public AudioDomain(global::UnityEngine.Transform parent, AudioPlayer _player)
	{
		_domainParent = parent;
		player = _player;
	}

	public void Setup()
	{
		global::UnityEngine.Transform transform = new global::UnityEngine.GameObject("AudioDomain").transform;
		transform.parent = _domainParent;
		_initializerPrefab = global::UnityEngine.Object.Instantiate(new global::UnityEngine.GameObject("Initializer"), transform);
		global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("AkAudioListener");
		_listener = gameObject.AddComponent<global::FMODUnity.StudioListener>();
		global::FMODUnity.RuntimeManager.AddListener(_listener);
		SetListenerParent(_domainParent);
	}

	public void AssetCacheReady(AssetCache cache)
	{
		SoundBankLoader = new SoundBankLoader(cache);
		player.ReceivedSoundBankLoader();
	}

	public void Teardown()
	{
		_listener = null;
	}
}
