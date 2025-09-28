public class StaticSource
{
	public AngleStrength BaseAngleStrength;

	public StaticEffectSettings BaseEffects;

	public AngleStrength AngleStrength;

	public StaticEffectSettings Effects;

	public AngleStrength FlashlightAngleStrength;

	public StaticEffectSettings FlashlightEffects;

	public readonly global::System.Collections.Generic.List<AdditiveSource> AdditiveSources;

	public float ShearModifier;

	public float Angle;

	public StaticSource()
	{
		AdditiveSources = new global::System.Collections.Generic.List<AdditiveSource>();
	}
}
