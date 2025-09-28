public class TextMeshProUGUILocalizer : global::UnityEngine.MonoBehaviour
{
	private static global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<TextMeshProUGUILocalizer>> _textMeshProUGUILocalizersByLocId;

	[global::UnityEngine.SerializeField]
	private string _localizationId;

	private global::TMPro.TextMeshProUGUI _textMeshProUgui;

	private string _originalString;

	public static void RedoAllLocalizationLookups()
	{
		foreach (string key in _textMeshProUGUILocalizersByLocId.Keys)
		{
			foreach (TextMeshProUGUILocalizer item in _textMeshProUGUILocalizersByLocId[key])
			{
				item.DoLocalizationNow();
			}
		}
	}

	private static bool EnsureGlobalLookupExists(TextMeshProUGUILocalizer textMeshProUguiLocalizer)
	{
		if (!_textMeshProUGUILocalizersByLocId.ContainsKey(textMeshProUguiLocalizer._localizationId))
		{
			global::System.Collections.Generic.List<TextMeshProUGUILocalizer> list = new global::System.Collections.Generic.List<TextMeshProUGUILocalizer>();
			list.Add(textMeshProUguiLocalizer);
			_textMeshProUGUILocalizersByLocId.Add(textMeshProUguiLocalizer._localizationId, list);
			return false;
		}
		if (_textMeshProUGUILocalizersByLocId[textMeshProUguiLocalizer._localizationId].Contains(textMeshProUguiLocalizer))
		{
			return true;
		}
		_textMeshProUGUILocalizersByLocId[textMeshProUguiLocalizer._localizationId].Add(textMeshProUguiLocalizer);
		return false;
	}

	private void EnsureTextMeshProUGUILink()
	{
		if (_textMeshProUgui == null)
		{
			_textMeshProUgui = base.gameObject.GetComponent<global::TMPro.TextMeshProUGUI>();
			_originalString = _textMeshProUgui.text;
		}
		if (!(_textMeshProUgui != null))
		{
			global::UnityEngine.Debug.LogError("TextMeshProUGUILocalizer EnsureTextMeshProUGUILink - Can't find TextMeshProUGUI component on " + base.gameObject.name);
		}
	}

	private void DoLocalizationNow()
	{
		if (!(this == null))
		{
			EnsureTextMeshProUGUILink();
			if (!(_textMeshProUgui == null) && LocalizationDomain.Instance != null)
			{
				LocalizationDomain.Instance.Localization.GetInterfaceAsync(DoLocalizationNowCallback);
			}
		}
	}

	private void DoLocalizationNowCallback(Localization iLocalization)
	{
		if (this == null || _textMeshProUgui == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(iLocalization.GetLocalizedString(_localizationId, _originalString)))
		{
			global::UnityEngine.Debug.LogError("TextMeshProUGUILocalizer DoLocalizationNowCallback - Can't find loc string. localizationId:'" + _localizationId + "' originalString:" + _originalString + " GameObject:" + GetTransformHierarchy(base.transform));
			if (_textMeshProUgui != null)
			{
				_textMeshProUgui.text = _originalString;
			}
		}
		else
		{
			_textMeshProUgui.text = iLocalization.GetLocalizedString(_localizationId, _originalString);
		}
	}

	private static string GetTransformHierarchy(global::UnityEngine.Transform transform)
	{
		if (transform.parent == null)
		{
			return transform.name;
		}
		return transform.parent.name + "/" + transform.name;
	}

	private void Awake()
	{
		if (!EnsureGlobalLookupExists(this))
		{
			DoLocalizationNow();
		}
	}

	static TextMeshProUGUILocalizer()
	{
		_textMeshProUGUILocalizersByLocId = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<TextMeshProUGUILocalizer>>();
	}
}
