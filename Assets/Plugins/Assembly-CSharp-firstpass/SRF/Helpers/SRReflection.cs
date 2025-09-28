namespace SRF.Helpers
{
	public static class SRReflection
	{
		public static void SetPropertyValue(object obj, global::System.Reflection.PropertyInfo p, object value)
		{
			p.GetSetMethod().Invoke(obj, new object[1] { value });
		}

		public static object GetPropertyValue(object obj, global::System.Reflection.PropertyInfo p)
		{
			return p.GetGetMethod().Invoke(obj, null);
		}

		public static T GetAttribute<T>(global::System.Reflection.MemberInfo t) where T : global::System.Attribute
		{
			return global::System.Attribute.GetCustomAttribute(t, typeof(T)) as T;
		}
	}
}
