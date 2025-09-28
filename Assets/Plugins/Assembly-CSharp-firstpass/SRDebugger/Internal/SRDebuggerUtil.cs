namespace SRDebugger.Internal
{
	public static class SRDebuggerUtil
	{
		public static bool IsMobilePlatform
		{
			get
			{
				if (global::UnityEngine.Application.isMobilePlatform)
				{
					return true;
				}
				global::UnityEngine.RuntimePlatform platform = global::UnityEngine.Application.platform;
				if ((uint)(platform - 18) <= 2u)
				{
					return true;
				}
				return false;
			}
		}

		public static bool EnsureEventSystemExists()
		{
			if (!global::SRDebugger.Settings.Instance.EnableEventSystemGeneration)
			{
				return false;
			}
			if (global::UnityEngine.EventSystems.EventSystem.current != null)
			{
				return false;
			}
			global::UnityEngine.EventSystems.EventSystem eventSystem = global::UnityEngine.Object.FindObjectOfType<global::UnityEngine.EventSystems.EventSystem>();
			if (eventSystem != null && eventSystem.gameObject.activeSelf && eventSystem.enabled)
			{
				return false;
			}
			global::UnityEngine.Debug.LogWarning("[SRDebugger] No EventSystem found in scene - creating a default one.");
			CreateDefaultEventSystem();
			return true;
		}

		public static void CreateDefaultEventSystem()
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("EventSystem");
			gameObject.AddComponent<global::UnityEngine.EventSystems.EventSystem>();
			gameObject.AddComponent<global::UnityEngine.EventSystems.StandaloneInputModule>();
		}

		public static global::System.Collections.Generic.ICollection<global::SRDebugger.Internal.OptionDefinition> ScanForOptions(object obj)
		{
			global::System.Collections.Generic.List<global::SRDebugger.Internal.OptionDefinition> list = new global::System.Collections.Generic.List<global::SRDebugger.Internal.OptionDefinition>();
			global::System.Reflection.MemberInfo[] members = obj.GetType().GetMembers(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.InvokeMethod | global::System.Reflection.BindingFlags.GetProperty | global::System.Reflection.BindingFlags.SetProperty);
			foreach (global::System.Reflection.MemberInfo memberInfo in members)
			{
				global::System.ComponentModel.CategoryAttribute attribute = global::SRF.Helpers.SRReflection.GetAttribute<global::System.ComponentModel.CategoryAttribute>(memberInfo);
				string category = ((attribute == null) ? "Default" : attribute.Category);
				int sortPriority = global::SRF.Helpers.SRReflection.GetAttribute<SROptions.SortAttribute>(memberInfo)?.SortPriority ?? 0;
				SROptions.DisplayNameAttribute attribute2 = global::SRF.Helpers.SRReflection.GetAttribute<SROptions.DisplayNameAttribute>(memberInfo);
				string name = ((attribute2 == null) ? memberInfo.Name : attribute2.Name);
				if (memberInfo is global::System.Reflection.PropertyInfo)
				{
					global::System.Reflection.PropertyInfo propertyInfo = memberInfo as global::System.Reflection.PropertyInfo;
					if (!(propertyInfo.GetGetMethod() == null) && (propertyInfo.GetGetMethod().Attributes & global::System.Reflection.MethodAttributes.Static) == 0)
					{
						list.Add(new global::SRDebugger.Internal.OptionDefinition(name, category, sortPriority, new global::SRF.Helpers.PropertyReference(obj, propertyInfo)));
					}
				}
				else if (memberInfo is global::System.Reflection.MethodInfo)
				{
					global::System.Reflection.MethodInfo methodInfo = memberInfo as global::System.Reflection.MethodInfo;
					if (!methodInfo.IsStatic && !(methodInfo.ReturnType != typeof(void)) && methodInfo.GetParameters().Length == 0)
					{
						list.Add(new global::SRDebugger.Internal.OptionDefinition(name, category, sortPriority, new global::SRF.Helpers.MethodReference(obj, methodInfo)));
					}
				}
			}
			return list;
		}

		public static string GetNumberString(int value, int max, string exceedsMaxString)
		{
			if (value >= max)
			{
				return exceedsMaxString;
			}
			return value.ToString();
		}

		public static void ConfigureCanvas(global::UnityEngine.Canvas canvas)
		{
			if (global::SRDebugger.Settings.Instance.UseDebugCamera)
			{
				canvas.worldCamera = global::SRDebugger.Internal.Service.DebugCamera.Camera;
				canvas.renderMode = global::UnityEngine.RenderMode.ScreenSpaceCamera;
			}
		}
	}
}
