namespace SRF.Service
{
	[global::UnityEngine.AddComponentMenu("SRF/Service/Service Manager")]
	public class SRServiceManager : global::SRF.Components.SRAutoSingleton<global::SRF.Service.SRServiceManager>
	{
		[global::System.Serializable]
		private class Service
		{
			public object Object;

			public global::System.Type Type;
		}

		[global::System.Serializable]
		private class ServiceStub
		{
			public global::System.Func<object> Constructor;

			public global::System.Type InterfaceType;

			public global::System.Func<global::System.Type> Selector;

			public global::System.Type Type;

			public override string ToString()
			{
				string text = InterfaceType.Name + " (";
				if (Type != null)
				{
					text = text + "Type: " + Type;
				}
				else if (Selector != null)
				{
					text = text + "Selector: " + Selector;
				}
				else if (Constructor != null)
				{
					text = text + "Constructor: " + Constructor;
				}
				return text + ")";
			}
		}

		public const bool EnableLogging = false;

		public static int LoadingCount;

		private readonly global::SRF.SRList<global::SRF.Service.SRServiceManager.Service> _services = new global::SRF.SRList<global::SRF.Service.SRServiceManager.Service>();

		private global::System.Collections.Generic.List<global::SRF.Service.SRServiceManager.ServiceStub> _serviceStubs;

		private static bool _hasQuit;

		public static bool IsLoading => LoadingCount > 0;

		public static T GetService<T>() where T : class
		{
			T val = GetServiceInternal(typeof(T)) as T;
			if (val == null && !_hasQuit)
			{
				global::UnityEngine.Debug.LogWarning("Service {0} not found. (HasQuit: {1})".Fmt(typeof(T).Name, _hasQuit));
			}
			return val;
		}

		public static object GetService(global::System.Type t)
		{
			object serviceInternal = GetServiceInternal(t);
			if (serviceInternal == null && !_hasQuit)
			{
				global::UnityEngine.Debug.LogWarning("Service {0} not found. (HasQuit: {1})".Fmt(t.Name, _hasQuit));
			}
			return serviceInternal;
		}

		private static object GetServiceInternal(global::System.Type t)
		{
			if (_hasQuit || !global::UnityEngine.Application.isPlaying)
			{
				return null;
			}
			global::SRF.SRList<global::SRF.Service.SRServiceManager.Service> services = global::SRF.Components.SRAutoSingleton<global::SRF.Service.SRServiceManager>.Instance._services;
			for (int i = 0; i < services.Count; i++)
			{
				global::SRF.Service.SRServiceManager.Service service = services[i];
				if (t.IsAssignableFrom(service.Type))
				{
					if (service.Object == null)
					{
						UnRegisterService(t);
						break;
					}
					return service.Object;
				}
			}
			return global::SRF.Components.SRAutoSingleton<global::SRF.Service.SRServiceManager>.Instance.AutoCreateService(t);
		}

		public static bool HasService<T>() where T : class
		{
			return HasService(typeof(T));
		}

		public static bool HasService(global::System.Type t)
		{
			if (_hasQuit || !global::UnityEngine.Application.isPlaying)
			{
				return false;
			}
			global::SRF.SRList<global::SRF.Service.SRServiceManager.Service> services = global::SRF.Components.SRAutoSingleton<global::SRF.Service.SRServiceManager>.Instance._services;
			for (int i = 0; i < services.Count; i++)
			{
				global::SRF.Service.SRServiceManager.Service service = services[i];
				if (t.IsAssignableFrom(service.Type))
				{
					return service.Object != null;
				}
			}
			return false;
		}

		public static void RegisterService<T>(object service) where T : class
		{
			RegisterService(typeof(T), service);
		}

		private static void RegisterService(global::System.Type t, object service)
		{
			if (_hasQuit)
			{
				return;
			}
			if (HasService(t))
			{
				if (GetServiceInternal(t) == service)
				{
					return;
				}
				throw new global::System.Exception("Service already registered for type " + t.Name);
			}
			UnRegisterService(t);
			if (!t.IsInstanceOfType(service))
			{
				throw new global::System.ArgumentException("service {0} must be assignable from type {1}".Fmt(service.GetType(), t));
			}
			global::SRF.Components.SRAutoSingleton<global::SRF.Service.SRServiceManager>.Instance._services.Add(new global::SRF.Service.SRServiceManager.Service
			{
				Object = service,
				Type = t
			});
		}

		public static void UnRegisterService<T>() where T : class
		{
			UnRegisterService(typeof(T));
		}

		private static void UnRegisterService(global::System.Type t)
		{
			if (_hasQuit || !global::SRF.Components.SRAutoSingleton<global::SRF.Service.SRServiceManager>.HasInstance || !HasService(t))
			{
				return;
			}
			global::SRF.SRList<global::SRF.Service.SRServiceManager.Service> services = global::SRF.Components.SRAutoSingleton<global::SRF.Service.SRServiceManager>.Instance._services;
			for (int num = services.Count - 1; num >= 0; num--)
			{
				if (services[num].Type == t)
				{
					services.RemoveAt(num);
				}
			}
		}

		protected override void Awake()
		{
			_hasQuit = false;
			base.Awake();
			global::UnityEngine.Object.DontDestroyOnLoad(base.CachedGameObject);
			base.CachedGameObject.hideFlags = global::UnityEngine.HideFlags.NotEditable;
		}

		protected void UpdateStubs()
		{
			if (_serviceStubs != null)
			{
				return;
			}
			_serviceStubs = new global::System.Collections.Generic.List<global::SRF.Service.SRServiceManager.ServiceStub>();
			global::System.Collections.Generic.List<global::System.Type> list = new global::System.Collections.Generic.List<global::System.Type>();
			global::System.Reflection.Assembly assembly = typeof(global::SRF.Service.SRServiceManager).Assembly;
			try
			{
				list.AddRange(assembly.GetExportedTypes());
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogError("[SRServiceManager] Error loading assembly {0}".Fmt(assembly.FullName), this);
				global::UnityEngine.Debug.LogException(exception);
			}
			foreach (global::System.Type item in list)
			{
				ScanType(item);
			}
		}

		protected object AutoCreateService(global::System.Type t)
		{
			UpdateStubs();
			foreach (global::SRF.Service.SRServiceManager.ServiceStub serviceStub in _serviceStubs)
			{
				if (serviceStub.InterfaceType != t)
				{
					continue;
				}
				object obj = null;
				if (serviceStub.Constructor != null)
				{
					obj = serviceStub.Constructor();
				}
				else
				{
					global::System.Type type = serviceStub.Type;
					if (type == null)
					{
						type = serviceStub.Selector();
					}
					obj = DefaultServiceConstructor(t, type);
				}
				if (!HasService(t))
				{
					RegisterService(t, obj);
				}
				return obj;
			}
			return null;
		}

		protected void OnApplicationQuit()
		{
			_hasQuit = true;
		}

		private static object DefaultServiceConstructor(global::System.Type serviceIntType, global::System.Type implType)
		{
			if (typeof(global::UnityEngine.MonoBehaviour).IsAssignableFrom(implType))
			{
				return new global::UnityEngine.GameObject("_S_" + serviceIntType.Name).AddComponent(implType);
			}
			if (typeof(global::UnityEngine.ScriptableObject).IsAssignableFrom(implType))
			{
				return global::UnityEngine.ScriptableObject.CreateInstance(implType);
			}
			return global::System.Activator.CreateInstance(implType);
		}

		private void ScanType(global::System.Type type)
		{
			global::SRF.Service.ServiceAttribute attribute = global::SRF.Helpers.SRReflection.GetAttribute<global::SRF.Service.ServiceAttribute>(type);
			if (attribute != null)
			{
				_serviceStubs.Add(new global::SRF.Service.SRServiceManager.ServiceStub
				{
					Type = type,
					InterfaceType = attribute.ServiceType
				});
			}
			ScanTypeForConstructors(type, _serviceStubs);
			ScanTypeForSelectors(type, _serviceStubs);
		}

		private static void ScanTypeForSelectors(global::System.Type t, global::System.Collections.Generic.List<global::SRF.Service.SRServiceManager.ServiceStub> stubs)
		{
			global::System.Reflection.MethodInfo[] staticMethods = GetStaticMethods(t);
			foreach (global::System.Reflection.MethodInfo methodInfo in staticMethods)
			{
				global::SRF.Service.ServiceSelectorAttribute attrib = global::SRF.Helpers.SRReflection.GetAttribute<global::SRF.Service.ServiceSelectorAttribute>(methodInfo);
				if (attrib == null)
				{
					continue;
				}
				if (methodInfo.ReturnType != typeof(global::System.Type))
				{
					global::UnityEngine.Debug.LogError("ServiceSelector must have return type of Type ({0}.{1}())".Fmt(t.Name, methodInfo.Name));
					continue;
				}
				if (methodInfo.GetParameters().Length != 0)
				{
					global::UnityEngine.Debug.LogError("ServiceSelector must have no parameters ({0}.{1}())".Fmt(t.Name, methodInfo.Name));
					continue;
				}
				global::SRF.Service.SRServiceManager.ServiceStub serviceStub = global::System.Linq.Enumerable.FirstOrDefault(stubs, (global::SRF.Service.SRServiceManager.ServiceStub p) => p.InterfaceType == attrib.ServiceType);
				if (serviceStub == null)
				{
					serviceStub = new global::SRF.Service.SRServiceManager.ServiceStub
					{
						InterfaceType = attrib.ServiceType
					};
					stubs.Add(serviceStub);
				}
				serviceStub.Selector = (global::System.Func<global::System.Type>)global::System.Delegate.CreateDelegate(typeof(global::System.Func<global::System.Type>), methodInfo);
			}
		}

		private static void ScanTypeForConstructors(global::System.Type t, global::System.Collections.Generic.List<global::SRF.Service.SRServiceManager.ServiceStub> stubs)
		{
			global::System.Reflection.MethodInfo[] staticMethods = GetStaticMethods(t);
			foreach (global::System.Reflection.MethodInfo methodInfo in staticMethods)
			{
				global::SRF.Service.ServiceConstructorAttribute attrib = global::SRF.Helpers.SRReflection.GetAttribute<global::SRF.Service.ServiceConstructorAttribute>(methodInfo);
				if (attrib == null)
				{
					continue;
				}
				if (methodInfo.ReturnType != attrib.ServiceType)
				{
					global::UnityEngine.Debug.LogError("ServiceConstructor must have return type of {2} ({0}.{1}())".Fmt(t.Name, methodInfo.Name, attrib.ServiceType));
					continue;
				}
				if (methodInfo.GetParameters().Length != 0)
				{
					global::UnityEngine.Debug.LogError("ServiceConstructor must have no parameters ({0}.{1}())".Fmt(t.Name, methodInfo.Name));
					continue;
				}
				global::SRF.Service.SRServiceManager.ServiceStub serviceStub = global::System.Linq.Enumerable.FirstOrDefault(stubs, (global::SRF.Service.SRServiceManager.ServiceStub p) => p.InterfaceType == attrib.ServiceType);
				if (serviceStub == null)
				{
					serviceStub = new global::SRF.Service.SRServiceManager.ServiceStub
					{
						InterfaceType = attrib.ServiceType
					};
					stubs.Add(serviceStub);
				}
				global::System.Reflection.MethodInfo m = methodInfo;
				serviceStub.Constructor = () => m.Invoke(null, null);
			}
		}

		private static global::System.Reflection.MethodInfo[] GetStaticMethods(global::System.Type t)
		{
			return t.GetMethods(global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
		}
	}
}
