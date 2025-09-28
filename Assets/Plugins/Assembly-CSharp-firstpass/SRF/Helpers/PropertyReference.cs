namespace SRF.Helpers
{
	public class PropertyReference
	{
		private readonly global::System.Reflection.PropertyInfo _property;

		private readonly object _target;

		public string PropertyName => _property.Name;

		public global::System.Type PropertyType => _property.PropertyType;

		public bool CanRead => _property.GetGetMethod() != null;

		public bool CanWrite => _property.GetSetMethod() != null;

		public PropertyReference(object target, global::System.Reflection.PropertyInfo property)
		{
			SRDebugUtil.AssertNotNull(target);
			_target = target;
			_property = property;
		}

		public object GetValue()
		{
			if (_property.CanRead)
			{
				return global::SRF.Helpers.SRReflection.GetPropertyValue(_target, _property);
			}
			return null;
		}

		public void SetValue(object value)
		{
			if (_property.CanWrite)
			{
				global::SRF.Helpers.SRReflection.SetPropertyValue(_target, _property, value);
				return;
			}
			throw new global::System.InvalidOperationException("Can not write to property");
		}

		public T GetAttribute<T>() where T : global::System.Attribute
		{
			return global::System.Linq.Enumerable.FirstOrDefault(_property.GetCustomAttributes(typeof(T), inherit: true)) as T;
		}
	}
}
