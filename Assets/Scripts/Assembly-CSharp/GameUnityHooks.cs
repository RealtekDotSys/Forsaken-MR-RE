[global::System.Serializable]
public class GameUnityHooks
{
	[global::UnityEngine.SerializeField]
	private SceneLookupTableAccess _sceneLookupTableAccess;

	[global::UnityEngine.SerializeField]
	private SkyboxSceneConfig _skyboxConfigs;

	[global::UnityEngine.SerializeField]
	private Configs _configs;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _afterTOSAccept;

	[global::UnityEngine.SerializeField]
	private LocalLocalizationKVPs _localLocalizationKVPs;

	[global::UnityEngine.SerializeField]
	private CameraFx _cameraFx;

	public Configs Configs => _configs;

	public CameraFx CameraFx => _cameraFx;

	public global::UnityEngine.GameObject AfterTosAccept => _afterTOSAccept;

	public SceneLookupTableAccess SceneLookupTableAccess => _sceneLookupTableAccess;

	public SkyboxSceneConfig SkyboxConfigs => _skyboxConfigs;

	public LocalLocalizationKVPs LocalLocalizationKVPs => _localLocalizationKVPs;
}
