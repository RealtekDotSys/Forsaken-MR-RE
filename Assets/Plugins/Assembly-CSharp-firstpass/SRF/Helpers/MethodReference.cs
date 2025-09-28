namespace SRF.Helpers
{
	public class MethodReference
	{
		private global::System.Reflection.MethodInfo _method;

		private object _target;

		public string MethodName => _method.Name;

		public MethodReference(object target, global::System.Reflection.MethodInfo method)
		{
			SRDebugUtil.AssertNotNull(target);
			_target = target;
			_method = method;
		}

		public object Invoke(object[] parameters)
		{
			return _method.Invoke(_target, parameters);
		}
	}
}
