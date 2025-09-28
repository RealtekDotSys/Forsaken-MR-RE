namespace VLB
{
	public static class Noise3D
	{
		private static bool ms_IsSupportedChecked;

		private static bool ms_IsSupported;

		private static global::UnityEngine.Texture3D ms_NoiseTexture;

		private const global::UnityEngine.HideFlags kHideFlags = global::UnityEngine.HideFlags.HideAndDontSave;

		private const int kMinShaderLevel = 35;

		public static bool isSupported
		{
			get
			{
				if (!ms_IsSupportedChecked)
				{
					ms_IsSupported = global::UnityEngine.SystemInfo.graphicsShaderLevel >= 35;
					if (!ms_IsSupported)
					{
						global::UnityEngine.Debug.LogWarning(isNotSupportedString);
					}
					ms_IsSupportedChecked = true;
				}
				return ms_IsSupported;
			}
		}

		public static bool isProperlyLoaded => ms_NoiseTexture != null;

		public static string isNotSupportedString => $"3D Noise requires higher shader capabilities (Shader Model 3.5 / OpenGL ES 3.0), which are not available on the current platform: graphicsShaderLevel (current/required) = {(global::UnityEngine.SystemInfo.graphicsShaderLevel)} / {35}";

		[global::UnityEngine.RuntimeInitializeOnLoadMethod]
		private static void OnStartUp()
		{
			LoadIfNeeded();
		}

		public static void LoadIfNeeded()
		{
			if (!isSupported)
			{
				return;
			}
			if (ms_NoiseTexture == null)
			{
				ms_NoiseTexture = LoadTexture3D(global::VLB.Config.Instance.noise3DData, global::VLB.Config.Instance.noise3DSize);
				if ((bool)ms_NoiseTexture)
				{
					ms_NoiseTexture.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
				}
			}
			global::UnityEngine.Shader.SetGlobalTexture("_VLB_NoiseTex3D", ms_NoiseTexture);
			global::UnityEngine.Shader.SetGlobalVector("_VLB_NoiseGlobal", global::VLB.Config.Instance.globalNoiseParam);
		}

		private static global::UnityEngine.Texture3D LoadTexture3D(global::UnityEngine.TextAsset textData, int size)
		{
			if (textData == null)
			{
				global::UnityEngine.Debug.LogErrorFormat("Fail to open Noise 3D Data");
				return null;
			}
			byte[] bytes = textData.bytes;
			int num = global::UnityEngine.Mathf.Max(0, size * size * size);
			if (bytes.Length != num)
			{
				global::UnityEngine.Debug.LogErrorFormat("Noise 3D Data file has not the proper size {0}x{0}x{0}", size);
				return null;
			}
			global::UnityEngine.Texture3D texture3D = new global::UnityEngine.Texture3D(size, size, size, global::UnityEngine.TextureFormat.Alpha8, mipChain: false);
			global::UnityEngine.Color[] array = new global::UnityEngine.Color[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new global::UnityEngine.Color32(0, 0, 0, bytes[i]);
			}
			texture3D.SetPixels(array);
			texture3D.Apply();
			return texture3D;
		}
	}
}
