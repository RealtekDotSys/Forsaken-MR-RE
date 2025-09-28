namespace SRF
{
	public static class Coroutines
	{
		public static global::System.Collections.IEnumerator WaitForSecondsRealTime(float time)
		{
			float endTime = global::UnityEngine.Time.realtimeSinceStartup + time;
			while (global::UnityEngine.Time.realtimeSinceStartup < endTime)
			{
				yield return null;
			}
		}
	}
}
