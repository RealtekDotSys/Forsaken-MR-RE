public class ComponentContainer
{
	public static readonly global::System.Type[] Texts;

	public static readonly global::System.Type[] Buttons;

	public static readonly global::System.Type[] Images;

	public static readonly global::System.Type[] TextsButtons;

	public static readonly global::System.Type[] TextsImages;

	public static readonly global::System.Type[] ButtonsImages;

	public static readonly global::System.Type[] TextsButtonsImages;

	public static readonly global::System.Type[] AnimatorsTextsImages;

	private global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.LinkedList<global::UnityEngine.Component>>> _components;

	private void CacheComponent(global::UnityEngine.Component component)
	{
		CacheComponentWithType(component, component.GetType());
	}

	private void CacheComponentWithType(global::UnityEngine.Component component, global::System.Type t)
	{
		if (!_components.TryGetValue(t, out var value))
		{
			_components.Add(t, new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.LinkedList<global::UnityEngine.Component>>());
		}
		value = _components[t];
		if (!value.TryGetValue(component.name, out var value2))
		{
			value.Add(component.name, new global::System.Collections.Generic.LinkedList<global::UnityEngine.Component>());
		}
		value2 = value[component.name];
		value2.AddLast(component);
	}

	private T GetCachedComponent<T>(string searchName, global::System.Type componentType, bool suppressError = false) where T : global::UnityEngine.Component
	{
		global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.LinkedList<global::UnityEngine.Component>> value = null;
		if (!_components.TryGetValue(componentType, out value))
		{
			return null;
		}
		if (!value.TryGetValue(searchName, out var value2))
		{
			if (!suppressError)
			{
				string text = $"Could not find component {searchName} of type {componentType}";
				global::UnityEngine.Debug.LogError("ComponentContainer GetCachedComponent - " + text);
			}
			return null;
		}
		if (value2.First.Value == null)
		{
			return null;
		}
		return (T)value2.First.Value;
	}

	public void CacheComponentsWithInheritance(global::UnityEngine.GameObject gameObject, global::System.Type[] onlyCacheTypes)
	{
		foreach (global::System.Type type in onlyCacheTypes)
		{
			global::UnityEngine.Component[] componentsInChildren = gameObject.GetComponentsInChildren(type, includeInactive: true);
			foreach (global::UnityEngine.Component component in componentsInChildren)
			{
				CacheComponentWithType(component, type);
			}
		}
	}

	public void CacheComponents(global::UnityEngine.GameObject gameObject, global::System.Type[] onlyCacheTypes)
	{
		foreach (global::System.Type type in onlyCacheTypes)
		{
			global::UnityEngine.Component[] componentsInChildren = gameObject.GetComponentsInChildren(type, includeInactive: true);
			foreach (global::UnityEngine.Component component in componentsInChildren)
			{
				CacheComponent(component);
			}
		}
	}

	public bool TryGetComponent<T>(string searchName, out T returnComponent, bool suppressError = false) where T : global::UnityEngine.Component
	{
		returnComponent = TryGetComponent<T>(searchName, suppressError);
		return returnComponent != null;
	}

	public T TryGetComponent<T>(string searchName, bool suppressError = false) where T : global::UnityEngine.Component
	{
		return GetCachedComponent<T>(searchName, typeof(T), suppressError);
	}

	public void TearDown()
	{
		if (_components != null)
		{
			_components.Clear();
		}
		_components = null;
	}

	public ComponentContainer()
	{
		_components = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.LinkedList<global::UnityEngine.Component>>>();
	}

	static ComponentContainer()
	{
		Texts = new global::System.Type[1] { typeof(global::TMPro.TextMeshProUGUI) };
		Buttons = new global::System.Type[1] { typeof(global::UnityEngine.UI.Button) };
		Images = new global::System.Type[1] { typeof(global::UnityEngine.UI.Image) };
		TextsButtons = new global::System.Type[2]
		{
			typeof(global::TMPro.TextMeshProUGUI),
			typeof(global::UnityEngine.UI.Button)
		};
		TextsImages = new global::System.Type[2]
		{
			typeof(global::TMPro.TextMeshProUGUI),
			typeof(global::UnityEngine.UI.Image)
		};
		ButtonsImages = new global::System.Type[2]
		{
			typeof(global::UnityEngine.UI.Button),
			typeof(global::UnityEngine.UI.Image)
		};
		TextsButtonsImages = new global::System.Type[3]
		{
			typeof(global::TMPro.TextMeshProUGUI),
			typeof(global::UnityEngine.UI.Button),
			typeof(global::UnityEngine.UI.Image)
		};
		AnimatorsTextsImages = new global::System.Type[3]
		{
			typeof(global::UnityEngine.Animator),
			typeof(global::TMPro.TextMeshProUGUI),
			typeof(global::UnityEngine.UI.Image)
		};
	}
}
