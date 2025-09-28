public static class FloatHelper
{
	public static float ApplyModifier(float baseValue, string key, global::System.Collections.Generic.Dictionary<string, float> mods)
	{
		if (mods.ContainsKey(key))
		{
			return baseValue * mods[key];
		}
		return baseValue;
	}
}
