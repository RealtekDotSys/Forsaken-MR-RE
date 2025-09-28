public class IconGroupData
{
	public string BundleName;

	private static readonly string ClassName;

	private AssetCache _assetCache;

	private global::System.Collections.Generic.Dictionary<string, global::UnityEngine.Sprite> _spriteLookup;

	private global::System.Collections.Generic.Dictionary<string, global::UnityEngine.U2D.SpriteAtlas> _spriteAtlasLookup;

	private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::System.Action<global::UnityEngine.Sprite>>> _queuedSuccessCallbacks;

	private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::System.Action>> _queuedFailureCallbacks;

	public IconGroupData()
	{
		_spriteLookup = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.Sprite>();
		_spriteAtlasLookup = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.U2D.SpriteAtlas>();
		_queuedSuccessCallbacks = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::System.Action<global::UnityEngine.Sprite>>>();
		_queuedFailureCallbacks = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::System.Action>>();
	}

	public void Teardown()
	{
		ReleaseAssets();
		_spriteLookup.Clear();
		_spriteLookup = null;
		_spriteAtlasLookup.Clear();
		_spriteAtlasLookup = null;
		_queuedSuccessCallbacks.Clear();
		_queuedSuccessCallbacks = null;
		_queuedFailureCallbacks.Clear();
		_queuedFailureCallbacks = null;
		_assetCache = null;
	}

	public void SetAssetCache(AssetCache assetCache)
	{
		_assetCache = assetCache;
	}

	public void GetIcon(string assetName, global::System.Action<global::UnityEngine.Sprite> onSuccess, global::System.Action onFailure)
	{
		if (_spriteLookup.ContainsKey(assetName))
		{
			onSuccess?.Invoke(_spriteLookup[assetName]);
		}
		else if (_queuedSuccessCallbacks.ContainsKey(assetName))
		{
			AddCallbacksToLoadingAsset(assetName, onSuccess, onFailure);
		}
		else
		{
			LoadSpriteAsset(assetName, onSuccess, onFailure);
		}
	}

	public void GetIconFromAtlas(string assetName, string atlasName, global::System.Action<global::UnityEngine.Sprite> onSuccess, global::System.Action onFailure)
	{
		if (_spriteLookup.ContainsKey(assetName))
		{
			global::UnityEngine.Sprite sprite = _spriteLookup[assetName];
			if (sprite == null)
			{
				onFailure?.Invoke();
			}
			else
			{
				onSuccess?.Invoke(sprite);
			}
		}
		else if (_spriteAtlasLookup.ContainsKey(atlasName))
		{
			global::UnityEngine.Sprite sprite = _spriteAtlasLookup[atlasName].GetSprite(assetName);
			if (sprite == null)
			{
				onFailure?.Invoke();
				return;
			}
			_spriteLookup.Add(assetName, sprite);
			onSuccess?.Invoke(sprite);
		}
		else if (_queuedSuccessCallbacks.ContainsKey(assetName))
		{
			AddCallbacksToLoadingAsset(assetName, onSuccess, onFailure);
		}
		else
		{
			LoadSpriteAtlasAsset(atlasName, assetName, onSuccess, onFailure);
		}
	}

	public void ReleaseAssets()
	{
		foreach (global::UnityEngine.Sprite value in _spriteLookup.Values)
		{
			if (value != null)
			{
				_assetCache.ReleaseAsset(value);
			}
		}
		_spriteLookup.Clear();
		foreach (global::UnityEngine.U2D.SpriteAtlas value2 in _spriteAtlasLookup.Values)
		{
			if (value2 != null)
			{
				_assetCache.ReleaseAsset(value2);
			}
		}
		_spriteAtlasLookup.Clear();
	}

	private void AddCallbacksToLoadingAsset(string assetName, global::System.Action<global::UnityEngine.Sprite> onSuccess, global::System.Action onFailure)
	{
		_queuedSuccessCallbacks[assetName].Add(onSuccess);
		_queuedFailureCallbacks[assetName].Add(onFailure);
	}

	private void LoadSpriteAsset(string assetName, global::System.Action<global::UnityEngine.Sprite> onSuccess, global::System.Action onFailure)
	{
		_queuedSuccessCallbacks.Add(assetName, new global::System.Collections.Generic.List<global::System.Action<global::UnityEngine.Sprite>>());
		_queuedFailureCallbacks.Add(assetName, new global::System.Collections.Generic.List<global::System.Action>());
		AddCallbacksToLoadingAsset(assetName, onSuccess, onFailure);
		_assetCache.LoadAsset(BundleName, assetName, delegate(global::UnityEngine.Sprite sprite)
		{
			SpriteLoadSuccess(assetName, sprite);
		}, delegate
		{
			AssetLoadFailure(assetName);
		});
	}

	private void LoadSpriteAtlasAsset(string atlasName, string assetName, global::System.Action<global::UnityEngine.Sprite> onSuccess, global::System.Action onFailure)
	{
		_queuedSuccessCallbacks.Add(assetName, new global::System.Collections.Generic.List<global::System.Action<global::UnityEngine.Sprite>>());
		_queuedFailureCallbacks.Add(assetName, new global::System.Collections.Generic.List<global::System.Action>());
		AddCallbacksToLoadingAsset(assetName, onSuccess, onFailure);
		_assetCache.LoadAsset(BundleName, atlasName, delegate(global::UnityEngine.U2D.SpriteAtlas sprite)
		{
			SpriteAtlasLoadSuccess(atlasName, assetName, sprite);
		}, delegate
		{
			AssetLoadFailure(assetName);
		});
	}

	public void SpriteLoadSuccess(string assetName, global::UnityEngine.Sprite sprite)
	{
		if (sprite == null)
		{
			global::UnityEngine.Debug.LogError("IconGroupData SpriteAtlasLoadSuccess - Invalid 'sprite' for '" + assetName + "'!");
			UpdateObserversAssetFailure(assetName);
		}
		else
		{
			_spriteLookup.Add(assetName, sprite);
			UpdateObserversAssetSuccess(assetName, sprite);
		}
	}

	public void SpriteAtlasLoadSuccess(string atlasName, string spriteAssetName, global::UnityEngine.U2D.SpriteAtlas spriteAtlas)
	{
		string text;
		if (spriteAtlas == null)
		{
			text = "Invalid 'spriteAtlas' for '" + atlasName + ":" + spriteAssetName + "'!";
			global::UnityEngine.Debug.LogError("IconGroupData SpriteAtlasLoadSuccess - " + text);
			UpdateObserversAssetFailure(spriteAssetName);
			return;
		}
		if (!_spriteAtlasLookup.ContainsKey(atlasName))
		{
			_spriteAtlasLookup.Add(atlasName, spriteAtlas);
		}
		global::UnityEngine.Sprite sprite = spriteAtlas.GetSprite(spriteAssetName);
		if (sprite != null)
		{
			_spriteLookup.Add(spriteAssetName, sprite);
			UpdateObserversAssetSuccess(spriteAssetName, sprite);
			return;
		}
		text = "SpriteAtlas '" + spriteAtlas.name + "' does not contain Sprite '" + spriteAssetName + "'";
		global::UnityEngine.Debug.LogError("IconGroupData SpriteAtlasLoadSuccess - " + text);
		UpdateObserversAssetFailure(spriteAssetName);
	}

	public void AssetLoadFailure(string assetName)
	{
		UpdateObserversAssetFailure(assetName);
	}

	private void UpdateObserversAssetSuccess(string assetName, global::UnityEngine.Sprite sprite)
	{
		if (_queuedSuccessCallbacks == null)
		{
			ClearCallbacks(assetName);
			return;
		}
		foreach (global::System.Action<global::UnityEngine.Sprite> item in _queuedSuccessCallbacks[assetName])
		{
			item(sprite);
		}
		ClearCallbacks(assetName);
	}

	private void UpdateObserversAssetFailure(string assetName)
	{
		if (_queuedFailureCallbacks == null)
		{
			ClearCallbacks(assetName);
			return;
		}
		foreach (global::System.Action item in _queuedFailureCallbacks[assetName])
		{
			item();
		}
		ClearCallbacks(assetName);
	}

	private void ClearCallbacks(string assetName)
	{
		_queuedSuccessCallbacks[assetName].Clear();
		_queuedSuccessCallbacks.Remove(assetName);
		_queuedFailureCallbacks[assetName].Clear();
		_queuedFailureCallbacks.Remove(assetName);
	}
}
