public static class VibrationSettings
{
	private const string VIBRATION_ENABLE = "VIBRATION_ENABLE";

	static VibrationSettings()
	{
		if (!global::UnityEngine.PlayerPrefs.HasKey("VIBRATION_ENABLE"))
		{
			VibrationEnable(value: true);
		}
	}

	public static bool VibrationIsEnabled()
	{
		return global::UnityEngine.PlayerPrefs.GetInt("VIBRATION_ENABLE", 1) == 1;
	}

	public static void VibrationEnable(bool value)
	{
		global::UnityEngine.PlayerPrefs.SetInt("VIBRATION_ENABLE", value ? 1 : 0);
		global::UnityEngine.PlayerPrefs.Save();
	}
}
