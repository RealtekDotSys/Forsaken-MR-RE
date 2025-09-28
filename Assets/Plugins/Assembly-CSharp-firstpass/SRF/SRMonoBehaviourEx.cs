namespace SRF
{
	public abstract class SRMonoBehaviourEx : global::SRF.SRMonoBehaviour
	{
		private struct FieldInfo
		{
			public bool AutoCreate;

			public bool AutoSet;

			public global::System.Reflection.FieldInfo Field;

			public bool Import;

			public global::System.Type ImportType;
		}

		private static global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.IList<global::SRF.SRMonoBehaviourEx.FieldInfo>> _checkedFields;

		private static void CheckFields(global::SRF.SRMonoBehaviourEx instance, bool justSet = false)
		{
			if (_checkedFields == null)
			{
				_checkedFields = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.IList<global::SRF.SRMonoBehaviourEx.FieldInfo>>();
			}
			global::System.Type type = instance.GetType();
			if (!_checkedFields.TryGetValue(instance.GetType(), out var value))
			{
				value = ScanType(type);
				_checkedFields.Add(type, value);
			}
			PopulateObject(value, instance, justSet);
		}

		private static void PopulateObject(global::System.Collections.Generic.IList<global::SRF.SRMonoBehaviourEx.FieldInfo> cache, global::SRF.SRMonoBehaviourEx instance, bool justSet)
		{
			for (int i = 0; i < cache.Count; i++)
			{
				global::SRF.SRMonoBehaviourEx.FieldInfo fieldInfo = cache[i];
				if (!global::System.Collections.Generic.EqualityComparer<object>.Default.Equals(fieldInfo.Field.GetValue(instance), null))
				{
					continue;
				}
				if (fieldInfo.Import)
				{
					global::System.Type type = fieldInfo.ImportType ?? fieldInfo.Field.FieldType;
					object service = global::SRF.Service.SRServiceManager.GetService(type);
					if (service == null)
					{
						global::UnityEngine.Debug.LogWarning("Field {0} import failed (Type {1})".Fmt(fieldInfo.Field.Name, type));
					}
					else
					{
						fieldInfo.Field.SetValue(instance, service);
					}
					continue;
				}
				if (fieldInfo.AutoSet)
				{
					global::UnityEngine.Component component = instance.GetComponent(fieldInfo.Field.FieldType);
					if (!global::System.Collections.Generic.EqualityComparer<object>.Default.Equals(component, null))
					{
						fieldInfo.Field.SetValue(instance, component);
						continue;
					}
				}
				if (justSet)
				{
					continue;
				}
				if (fieldInfo.AutoCreate)
				{
					global::UnityEngine.Component value = instance.CachedGameObject.AddComponent(fieldInfo.Field.FieldType);
					fieldInfo.Field.SetValue(instance, value);
				}
				throw new global::UnityEngine.UnassignedReferenceException("Field {0} is unassigned, but marked with RequiredFieldAttribute".Fmt(fieldInfo.Field.Name));
			}
		}

		private static global::System.Collections.Generic.List<global::SRF.SRMonoBehaviourEx.FieldInfo> ScanType(global::System.Type t)
		{
			global::System.Collections.Generic.List<global::SRF.SRMonoBehaviourEx.FieldInfo> list = new global::System.Collections.Generic.List<global::SRF.SRMonoBehaviourEx.FieldInfo>();
			global::SRF.RequiredFieldAttribute attribute = global::SRF.Helpers.SRReflection.GetAttribute<global::SRF.RequiredFieldAttribute>(t);
			global::System.Reflection.FieldInfo[] fields = t.GetFields(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
			foreach (global::System.Reflection.FieldInfo fieldInfo in fields)
			{
				global::SRF.RequiredFieldAttribute attribute2 = global::SRF.Helpers.SRReflection.GetAttribute<global::SRF.RequiredFieldAttribute>(fieldInfo);
				global::SRF.ImportAttribute attribute3 = global::SRF.Helpers.SRReflection.GetAttribute<global::SRF.ImportAttribute>(fieldInfo);
				if (attribute != null || attribute2 != null || attribute3 != null)
				{
					global::SRF.SRMonoBehaviourEx.FieldInfo item = new global::SRF.SRMonoBehaviourEx.FieldInfo
					{
						Field = fieldInfo
					};
					if (attribute3 != null)
					{
						item.Import = true;
						item.ImportType = attribute3.Service;
					}
					else if (attribute2 != null)
					{
						item.AutoSet = attribute2.AutoSearch;
						item.AutoCreate = attribute2.AutoCreate;
					}
					else
					{
						item.AutoSet = attribute.AutoSearch;
						item.AutoCreate = attribute.AutoCreate;
					}
					list.Add(item);
				}
			}
			return list;
		}

		protected virtual void Awake()
		{
			CheckFields(this);
		}

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void FixedUpdate()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnDestroy()
		{
		}
	}
}
