public class EyeColorData
{
	public bool HasOverride;

	public global::UnityEngine.Color Color;

	public float Intensity;

	public EyeColorData(bool hasOverride, global::UnityEngine.Color color, float intensity)
	{
		HasOverride = hasOverride;
		Color = color;
		Intensity = intensity;
	}
}
