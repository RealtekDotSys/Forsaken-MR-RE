public static class ServerTime
{
	private static long serverOffset;

	private static bool initialized;

	public static void SetServerTime(long serverTime)
	{
		serverOffset = serverTime - global::System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		initialized = true;
	}

	public static long GetCurrentTime()
	{
		if (initialized)
		{
			return serverOffset + global::System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		}
		return global::System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
	}

	public static bool IsInitialized()
	{
		return initialized;
	}
}
