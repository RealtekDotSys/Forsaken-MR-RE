namespace SRF.UI
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Style Component")]
	public class StyleComponent : global::SRF.SRMonoBehaviour
	{
		private global::SRF.UI.Style _activeStyle;

		private global::SRF.UI.StyleRoot _cachedRoot;

		private global::UnityEngine.UI.Graphic _graphic;

		private bool _hasStarted;

		private global::UnityEngine.UI.Image _image;

		private global::UnityEngine.UI.Selectable _selectable;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("StyleKey")]
		[global::UnityEngine.HideInInspector]
		private string _styleKey;

		public bool IgnoreImage;

		public string StyleKey
		{
			get
			{
				return _styleKey;
			}
			set
			{
				_styleKey = value;
				Refresh(invalidateCache: false);
			}
		}

		private void Start()
		{
			Refresh(invalidateCache: true);
			_hasStarted = true;
		}

		private void OnEnable()
		{
			if (_hasStarted)
			{
				Refresh(invalidateCache: false);
			}
		}

		public void Refresh(bool invalidateCache)
		{
			if (string.IsNullOrEmpty(StyleKey))
			{
				_activeStyle = null;
				return;
			}
			if (_cachedRoot == null || invalidateCache)
			{
				_cachedRoot = GetStyleRoot();
			}
			if (_cachedRoot == null)
			{
				global::UnityEngine.Debug.LogWarning("[StyleComponent] No active StyleRoot object found in parents.", this);
				_activeStyle = null;
				return;
			}
			global::SRF.UI.Style style = _cachedRoot.GetStyle(StyleKey);
			if (style == null)
			{
				global::UnityEngine.Debug.LogWarning("[StyleComponent] Style not found ({0})".Fmt(StyleKey), this);
				_activeStyle = null;
			}
			else
			{
				_activeStyle = style;
				ApplyStyle();
			}
		}

		private global::SRF.UI.StyleRoot GetStyleRoot()
		{
			global::UnityEngine.Transform transform = base.CachedTransform;
			int num = 0;
			global::SRF.UI.StyleRoot componentInParent;
			do
			{
				componentInParent = transform.GetComponentInParent<global::SRF.UI.StyleRoot>();
				if (componentInParent != null)
				{
					transform = componentInParent.transform.parent;
				}
				num++;
				if (num > 100)
				{
					global::UnityEngine.Debug.LogWarning("Breaking Loop");
					break;
				}
			}
			while (componentInParent != null && !componentInParent.enabled && transform != null);
			return componentInParent;
		}

		private void ApplyStyle()
		{
			if (_activeStyle == null)
			{
				return;
			}
			if (_graphic == null)
			{
				_graphic = GetComponent<global::UnityEngine.UI.Graphic>();
			}
			if (_selectable == null)
			{
				_selectable = GetComponent<global::UnityEngine.UI.Selectable>();
			}
			if (_image == null)
			{
				_image = GetComponent<global::UnityEngine.UI.Image>();
			}
			if (!IgnoreImage && _image != null)
			{
				_image.sprite = _activeStyle.Image;
			}
			if (_selectable != null)
			{
				global::UnityEngine.UI.ColorBlock colors = _selectable.colors;
				colors.normalColor = _activeStyle.NormalColor;
				colors.highlightedColor = _activeStyle.HoverColor;
				colors.pressedColor = _activeStyle.ActiveColor;
				colors.disabledColor = _activeStyle.DisabledColor;
				colors.colorMultiplier = 1f;
				_selectable.colors = colors;
				if (_graphic != null)
				{
					_graphic.color = global::UnityEngine.Color.white;
				}
			}
			else if (_graphic != null)
			{
				_graphic.color = _activeStyle.NormalColor;
			}
		}

		private void SRStyleDirty()
		{
			if (!base.CachedGameObject.activeInHierarchy)
			{
				_cachedRoot = null;
			}
			else
			{
				Refresh(invalidateCache: true);
			}
		}
	}
}
