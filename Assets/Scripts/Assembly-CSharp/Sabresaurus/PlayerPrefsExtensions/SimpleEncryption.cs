namespace Sabresaurus.PlayerPrefsExtensions
{
	public static class SimpleEncryption
	{
		private static string key = ":{j%6j?E:t#}G10mM%9hp5S=%}2,Y26C";

		private static global::System.Security.Cryptography.RijndaelManaged provider = null;

		private static void SetupProvider()
		{
			provider = new global::System.Security.Cryptography.RijndaelManaged();
			provider.Key = global::System.Text.Encoding.ASCII.GetBytes(key);
			provider.Mode = global::System.Security.Cryptography.CipherMode.ECB;
		}

		public static string EncryptString(string sourceString)
		{
			if (provider == null)
			{
				SetupProvider();
			}
			global::System.Security.Cryptography.ICryptoTransform cryptoTransform = provider.CreateEncryptor();
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(sourceString);
			return global::System.Convert.ToBase64String(cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length));
		}

		public static string DecryptString(string sourceString)
		{
			if (provider == null)
			{
				SetupProvider();
			}
			global::System.Security.Cryptography.ICryptoTransform cryptoTransform = provider.CreateDecryptor();
			byte[] array = global::System.Convert.FromBase64String(sourceString);
			byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			return global::System.Text.Encoding.UTF8.GetString(bytes);
		}

		public static string EncryptFloat(float value)
		{
			return EncryptString(global::System.Convert.ToBase64String(global::System.BitConverter.GetBytes(value)));
		}

		public static string EncryptInt(int value)
		{
			return EncryptString(global::System.Convert.ToBase64String(global::System.BitConverter.GetBytes(value)));
		}

		public static float DecryptFloat(string sourceString)
		{
			return global::System.BitConverter.ToSingle(global::System.Convert.FromBase64String(DecryptString(sourceString)), 0);
		}

		public static int DecryptInt(string sourceString)
		{
			return global::System.BitConverter.ToInt32(global::System.Convert.FromBase64String(DecryptString(sourceString)), 0);
		}
	}
}
