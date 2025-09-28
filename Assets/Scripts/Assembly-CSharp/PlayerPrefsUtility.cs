public static class PlayerPrefsUtility
{
	public const string KEY_PREFIX = "ENC-";

	public const string VALUE_FLOAT_PREFIX = "0";

	public const string VALUE_INT_PREFIX = "1";

	public const string VALUE_STRING_PREFIX = "2";

	public static bool IsEncryptedKey(string key)
	{
		if (key.StartsWith("ENC-"))
		{
			return true;
		}
		return false;
	}

	public static string DecryptKey(string encryptedKey)
	{
		if (encryptedKey.StartsWith("ENC-"))
		{
			return global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.DecryptString(encryptedKey.Substring("ENC-".Length));
		}
		throw new global::System.InvalidOperationException("Could not decrypt item, no match found in known encrypted key prefixes");
	}

	public static void SetEncryptedFloat(string key, float value)
	{
		string text = global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.EncryptString(key);
		string text2 = global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.EncryptFloat(value);
		global::UnityEngine.PlayerPrefs.SetString("ENC-" + text, "0" + text2);
	}

	public static void SetEncryptedInt(string key, int value)
	{
		string text = global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.EncryptString(key);
		string text2 = global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.EncryptInt(value);
		global::UnityEngine.PlayerPrefs.SetString("ENC-" + text, "1" + text2);
	}

	public static void SetEncryptedString(string key, string value)
	{
		string text = global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.EncryptString(key);
		string text2 = global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.EncryptString(value);
		global::UnityEngine.PlayerPrefs.SetString("ENC-" + text, "2" + text2);
	}

	public static object GetEncryptedValue(string encryptedKey, string encryptedValue)
	{
		if (encryptedValue.StartsWith("0"))
		{
			return GetEncryptedFloat(global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.DecryptString(encryptedKey.Substring("ENC-".Length)));
		}
		if (encryptedValue.StartsWith("1"))
		{
			return GetEncryptedInt(global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.DecryptString(encryptedKey.Substring("ENC-".Length)));
		}
		if (encryptedValue.StartsWith("2"))
		{
			return GetEncryptedString(global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.DecryptString(encryptedKey.Substring("ENC-".Length)));
		}
		throw new global::System.InvalidOperationException("Could not decrypt item, no match found in known encrypted key prefixes");
	}

	public static float GetEncryptedFloat(string key, float defaultValue = 0f)
	{
		string text = global::UnityEngine.PlayerPrefs.GetString("ENC-" + global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.EncryptString(key));
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Remove(0, 1);
			return global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.DecryptFloat(text);
		}
		return defaultValue;
	}

	public static int GetEncryptedInt(string key, int defaultValue = 0)
	{
		string text = global::UnityEngine.PlayerPrefs.GetString("ENC-" + global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.EncryptString(key));
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Remove(0, 1);
			return global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.DecryptInt(text);
		}
		return defaultValue;
	}

	public static string GetEncryptedString(string key, string defaultValue = "")
	{
		string text = global::UnityEngine.PlayerPrefs.GetString("ENC-" + global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.EncryptString(key));
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Remove(0, 1);
			return global::Sabresaurus.PlayerPrefsExtensions.SimpleEncryption.DecryptString(text);
		}
		return defaultValue;
	}

	public static void SetBool(string key, bool value)
	{
		if (value)
		{
			global::UnityEngine.PlayerPrefs.SetInt(key, 1);
		}
		else
		{
			global::UnityEngine.PlayerPrefs.SetInt(key, 0);
		}
	}

	public static bool GetBool(string key, bool defaultValue = false)
	{
		if (global::UnityEngine.PlayerPrefs.HasKey(key))
		{
			if (global::UnityEngine.PlayerPrefs.GetInt(key) != 0)
			{
				return true;
			}
			return false;
		}
		return defaultValue;
	}

	public static void SetEnum(string key, global::System.Enum value)
	{
		global::UnityEngine.PlayerPrefs.SetString(key, value.ToString());
	}

	public static T GetEnum<T>(string key, T defaultValue = default(T)) where T : struct
	{
		string value = global::UnityEngine.PlayerPrefs.GetString(key);
		if (!string.IsNullOrEmpty(value))
		{
			return (T)global::System.Enum.Parse(typeof(T), value);
		}
		return defaultValue;
	}

	public static object GetEnum(string key, global::System.Type enumType, object defaultValue)
	{
		string value = global::UnityEngine.PlayerPrefs.GetString(key);
		if (!string.IsNullOrEmpty(value))
		{
			return global::System.Enum.Parse(enumType, value);
		}
		return defaultValue;
	}

	public static void SetDateTime(string key, global::System.DateTime value)
	{
		global::UnityEngine.PlayerPrefs.SetString(key, value.ToString("o", global::System.Globalization.CultureInfo.InvariantCulture));
	}

	public static global::System.DateTime GetDateTime(string key, global::System.DateTime defaultValue = default(global::System.DateTime))
	{
		string text = global::UnityEngine.PlayerPrefs.GetString(key);
		if (!string.IsNullOrEmpty(text))
		{
			return global::System.DateTime.Parse(text, global::System.Globalization.CultureInfo.InvariantCulture, global::System.Globalization.DateTimeStyles.RoundtripKind);
		}
		return defaultValue;
	}

	public static void SetTimeSpan(string key, global::System.TimeSpan value)
	{
		global::UnityEngine.PlayerPrefs.SetString(key, value.ToString());
	}

	public static global::System.TimeSpan GetTimeSpan(string key, global::System.TimeSpan defaultValue = default(global::System.TimeSpan))
	{
		string text = global::UnityEngine.PlayerPrefs.GetString(key);
		if (!string.IsNullOrEmpty(text))
		{
			return global::System.TimeSpan.Parse(text);
		}
		return defaultValue;
	}
}
