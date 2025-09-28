public static class SRDebugUtil
{
	public const int LineBufferCount = 512;

	public static bool IsFixedUpdate { get; set; }

	[global::System.Diagnostics.DebuggerNonUserCode]
	[global::System.Diagnostics.DebuggerStepThrough]
	public static void AssertNotNull(object value, string message = null, global::UnityEngine.MonoBehaviour instance = null)
	{
		if (!global::System.Collections.Generic.EqualityComparer<object>.Default.Equals(value, null))
		{
			return;
		}
		message = ((message != null) ? global::SRF.SRFStringExtensions.Fmt("NotNullAssert Failed: {0}", message) : "Assert Failed");
		global::UnityEngine.Debug.LogError(message, instance);
		if (instance != null)
		{
			instance.enabled = false;
		}
		throw new global::System.NullReferenceException(message);
	}

	[global::System.Diagnostics.DebuggerNonUserCode]
	[global::System.Diagnostics.DebuggerStepThrough]
	public static void Assert(bool condition, string message = null, global::UnityEngine.MonoBehaviour instance = null)
	{
		if (condition)
		{
			return;
		}
		message = ((message != null) ? global::SRF.SRFStringExtensions.Fmt("Assert Failed: {0}", message) : "Assert Failed");
		global::UnityEngine.Debug.LogError(message, instance);
		throw new global::System.Exception(message);
	}

	[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
	[global::System.Diagnostics.DebuggerNonUserCode]
	[global::System.Diagnostics.DebuggerStepThrough]
	public static void EditorAssertNotNull(object value, string message = null, global::UnityEngine.MonoBehaviour instance = null)
	{
		AssertNotNull(value, message, instance);
	}

	[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
	[global::System.Diagnostics.DebuggerNonUserCode]
	[global::System.Diagnostics.DebuggerStepThrough]
	public static void EditorAssert(bool condition, string message = null, global::UnityEngine.MonoBehaviour instance = null)
	{
		Assert(condition, message, instance);
	}
}
