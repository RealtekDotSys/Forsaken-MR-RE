[global::System.Serializable]
public class ResultStepConfig
{
	public enum StartMode
	{
		Immediate = 0,
		DelayFromPreviousStart = 1,
		DelayFromAllComplete = 2
	}

	public enum Condition
	{
		AlwaysExecute = 0,
		IsNewBestStreak = 1,
		IsNotNewBestStreak = 2,
		HasAtLeastOneReward = 3,
		HasAtLeastTwoRewards = 4,
		HasAtLeastThreeRewards = 5,
		StartingStreakZero = 6,
		StartingStreakAtLeastOne = 7,
		HasAtLeastFourRewards = 8,
		HasAtLeastFiveRewards = 9,
		HasAtLeastSixRewards = 10,
		AdsAllowed = 11,
		AdsNotAllowed = 12,
		HasAtLeastOneCrateReward = 13,
		ShouldDownloadAssetBundles = 14
	}

	public enum Type
	{
		Enable = 0,
		Disable = 1,
		WaitForTap = 2,
		NumberChanger = 3,
		AudioOnly = 4,
		AssetDownload = 5,
		OpenCrate = 6
	}

	[global::UnityEngine.Header("Startup")]
	public ResultStepConfig.Condition executionCondition;

	public ResultStepConfig.StartMode startMode;

	public float delay;

	[global::UnityEngine.Header("Execution - General")]
	public ResultStepConfig.Type executionType;

	public string audioEventName;

	[global::UnityEngine.Header("Execution - Game Object (Enable/Disable)")]
	public global::UnityEngine.GameObject gameObject;

	[global::UnityEngine.Header("Execution - Wait for Tap")]
	public global::UnityEngine.UI.Button button;

	public bool willTimeout;

	public float timeoutTime;

	[global::UnityEngine.Header("Execution - Number Changer")]
	public NumberChanger numberChanger;

	[global::UnityEngine.Header("Execution - Asset Bundles")]
	[global::UnityEngine.HideInInspector]
	public EventExposer masterEventExposer;

	[global::UnityEngine.HideInInspector]
	public GameAssetManagementDomain gameAssetManagementDomain;
}
