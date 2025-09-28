public class ServerData
{
	private readonly global::PlayFab.Json.JsonObject _json;

	public virtual global::System.Collections.Generic.IDictionary<string, object> BaseData => _json;

	public virtual string JSON => _json.ToString();

	public global::System.Collections.Generic.ICollection<string> Keys => _json.Keys;

	public ServerData(global::PlayFab.Json.JsonObject jsonObject)
	{
		if (jsonObject != null)
		{
			_json = jsonObject;
		}
		else
		{
			global::UnityEngine.Debug.LogError("JsonObject is null or invalid!");
		}
	}

	public ServerData()
	{
		_json = new global::PlayFab.Json.JsonObject();
	}

	public virtual int? GetInt(string key)
	{
		if (_json.ContainsKey(key))
		{
			return global::System.Convert.ToInt32(_json[key]);
		}
		return null;
	}

	public virtual string GetString(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		if (_json[key] == null)
		{
			return null;
		}
		return global::System.Convert.ToString(_json[key]);
	}

	public virtual global::System.DateTime? GetDate(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		return global::System.Convert.ToDateTime(_json[key]);
	}

	public virtual double? GetDouble(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		return global::System.Convert.ToDouble(_json[key]);
	}

	public virtual long? GetLong(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		return global::System.Convert.ToInt64(_json[key]);
	}

	public virtual float? GetFloat(string key)
	{
		if (_json.ContainsKey(key))
		{
			return global::System.Convert.ToSingle(_json[key]);
		}
		return null;
	}

	public virtual long? GetNumber(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		return global::System.Convert.ToInt64(_json[key]);
	}

	public virtual bool? GetBoolean(string key)
	{
		if (_json.ContainsKey(key))
		{
			return (bool)_json[key];
		}
		return null;
	}

	public virtual bool ContainsKey(string key)
	{
		if (_json != null)
		{
			return _json.ContainsKey(key);
		}
		return false;
	}

	public virtual global::System.Collections.Generic.List<int> GetIntList(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		if (_json[key] == null)
		{
			return null;
		}
		global::System.Collections.Generic.List<object> list = (global::System.Collections.Generic.List<object>)_json[key];
		global::System.Collections.Generic.List<int> list2 = new global::System.Collections.Generic.List<int>();
		list2.Capacity = list.Count;
		foreach (object item in list)
		{
			list2.Add(global::System.Convert.ToInt32(item));
		}
		return list2;
	}

	public virtual global::System.Collections.Generic.List<long> GetLongList(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		if (_json[key] == null)
		{
			return null;
		}
		global::System.Collections.Generic.List<object> list = (global::System.Collections.Generic.List<object>)_json[key];
		global::System.Collections.Generic.List<long> list2 = new global::System.Collections.Generic.List<long>();
		list2.Capacity = list.Count;
		foreach (object item in list)
		{
			list2.Add(global::System.Convert.ToInt64(item));
		}
		return list2;
	}

	public virtual global::System.Collections.Generic.List<float> GetFloatList(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		if (_json[key] == null)
		{
			return null;
		}
		global::System.Collections.Generic.List<object> list = (global::System.Collections.Generic.List<object>)_json[key];
		global::System.Collections.Generic.List<float> list2 = new global::System.Collections.Generic.List<float>();
		list2.Capacity = list.Count;
		foreach (object item in list)
		{
			list2.Add(global::System.Convert.ToSingle(item));
		}
		return list2;
	}

	public virtual global::System.Collections.Generic.List<double> GetDoubleList(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		if (_json[key] == null)
		{
			return null;
		}
		global::System.Collections.Generic.List<object> list = (global::System.Collections.Generic.List<object>)_json[key];
		global::System.Collections.Generic.List<double> list2 = new global::System.Collections.Generic.List<double>();
		list2.Capacity = list.Count;
		foreach (object item in list)
		{
			list2.Add(global::System.Convert.ToDouble(item));
		}
		return list2;
	}

	public virtual global::System.Collections.Generic.List<string> GetStringList(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		if (_json[key] == null)
		{
			return null;
		}
		global::System.Collections.Generic.List<object> list = (global::System.Collections.Generic.List<object>)_json[key];
		global::System.Collections.Generic.List<string> list2 = new global::System.Collections.Generic.List<string>();
		list2.Capacity = list.Count;
		foreach (object item in list)
		{
			list2.Add((string)item);
		}
		return list2;
	}

	public virtual global::System.Collections.Generic.List<bool> GetBooleanList(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		if (_json[key] == null)
		{
			return null;
		}
		global::System.Collections.Generic.List<object> list = (global::System.Collections.Generic.List<object>)_json[key];
		global::System.Collections.Generic.List<bool> list2 = new global::System.Collections.Generic.List<bool>();
		list2.Capacity = list.Count;
		foreach (object item in list)
		{
			list2.Add((bool)item);
		}
		return list2;
	}

	public virtual ServerData GetServerData(string key)
	{
		if (_json.ContainsKey(key))
		{
			return new ServerData((global::PlayFab.Json.JsonObject)_json[key]);
		}
		return null;
	}

	public virtual global::System.Collections.Generic.List<ServerData> GetServerDataList(string key)
	{
		if (!_json.ContainsKey(key))
		{
			return null;
		}
		if (_json[key] == null)
		{
			return null;
		}
		global::System.Collections.Generic.List<object> list = (global::System.Collections.Generic.List<object>)_json[key];
		global::System.Collections.Generic.List<ServerData> list2 = new global::System.Collections.Generic.List<ServerData>();
		list2.Capacity = list.Count;
		if (list.Count < 1)
		{
			return list2;
		}
		foreach (object item in list)
		{
			list2.Add(new ServerData((global::PlayFab.Json.JsonObject)item));
		}
		return list2;
	}
}
