namespace SRDebugger.Internal
{
	public class BugReportScreenshotUtil
	{
		public static byte[] ScreenshotData;

		public static global::System.Collections.IEnumerator ScreenshotCaptureCo()
		{
			if (ScreenshotData != null)
			{
				global::UnityEngine.Debug.LogWarning("[SRDebugger] Warning, overriding existing screenshot data.");
			}
			yield return new global::UnityEngine.WaitForEndOfFrame();
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(global::UnityEngine.Screen.width, global::UnityEngine.Screen.height, global::UnityEngine.TextureFormat.RGB24, mipChain: false);
			texture2D.ReadPixels(new global::UnityEngine.Rect(0f, 0f, global::UnityEngine.Screen.width, global::UnityEngine.Screen.height), 0, 0);
			texture2D.Apply();
			ScreenshotData = global::UnityEngine.ImageConversion.EncodeToPNG(texture2D);
			global::UnityEngine.Object.Destroy(texture2D);
		}
	}
}
