public sealed class STATIC_DATA
{
	public class Angle
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class AngleRange
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class BaseAngle
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class BaseEffects
	{
		public double Shear { get; set; }

		public double Audio { get; set; }
	}

	public class Charge
	{
		public STATIC_DATA.BaseAngle BaseAngle { get; set; }

		public STATIC_DATA.BaseEffects BaseEffects { get; set; }

		public STATIC_DATA.Angle Angle { get; set; }

		public STATIC_DATA.Effects Effects { get; set; }

		public STATIC_DATA.FlashlightAngle FlashlightAngle { get; set; }

		public STATIC_DATA.FlashlightEffects FlashlightEffects { get; set; }
	}

	public class ChargeStaticSettings
	{
		public STATIC_DATA.AngleRange AngleRange { get; set; }

		public STATIC_DATA.ShaderSettings ShaderSettings { get; set; }

		public STATIC_DATA.FlashlightAngleRange FlashlightAngleRange { get; set; }

		public STATIC_DATA.FlashlightAdditive FlashlightAdditive { get; set; }
	}

	public class Circle
	{
		public STATIC_DATA.BaseAngle BaseAngle { get; set; }

		public STATIC_DATA.BaseEffects BaseEffects { get; set; }

		public STATIC_DATA.Angle Angle { get; set; }

		public STATIC_DATA.Effects Effects { get; set; }

		public STATIC_DATA.FlashlightAngle FlashlightAngle { get; set; }

		public STATIC_DATA.FlashlightEffects FlashlightEffects { get; set; }
	}

	public class CircleStaticSettings
	{
		public STATIC_DATA.AngleRange AngleRange { get; set; }

		public STATIC_DATA.ShaderSettings ShaderSettings { get; set; }

		public STATIC_DATA.FlashlightAngleRange FlashlightAngleRange { get; set; }

		public STATIC_DATA.FlashlightAdditive FlashlightAdditive { get; set; }
	}

	public class Duration
	{
		public double Min { get; set; }

		public double Max { get; set; }
	}

	public class Effects
	{
		public double Shear { get; set; }

		public double Audio { get; set; }
	}

	public class Entry
	{
		public string Profile { get; set; }

		public STATIC_DATA.Circle Circle { get; set; }

		public STATIC_DATA.Pause Pause { get; set; }

		public STATIC_DATA.Charge Charge { get; set; }

		public STATIC_DATA.WalkFootsteps WalkFootsteps { get; set; }

		public STATIC_DATA.RunFootsteps RunFootsteps { get; set; }

		public STATIC_DATA.CircleStaticSettings CircleStaticSettings { get; set; }

		public STATIC_DATA.PauseStaticSettings PauseStaticSettings { get; set; }

		public STATIC_DATA.ChargeStaticSettings ChargeStaticSettings { get; set; }

		public STATIC_DATA.WalkFootstepAdditive WalkFootstepAdditive { get; set; }

		public STATIC_DATA.RunFootstepAdditive RunFootstepAdditive { get; set; }

		public STATIC_DATA.PhantomWalk PhantomWalk { get; set; }

		public STATIC_DATA.PhantomWalkStaticSettings PhantomWalkStaticSettings { get; set; }
	}

	public class FadeSettings
	{
		public double FadeIn { get; set; }

		public double FadeOut { get; set; }
	}

	public class FlashlightAdditive
	{
		public bool IsPositional { get; set; }

		public STATIC_DATA.ShaderSettings ShaderSettings { get; set; }
	}

	public class FlashlightAngle
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class FlashlightAngleRange
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class FlashlightEffects
	{
		public double Shear { get; set; }

		public double Audio { get; set; }
	}

	public class Pause
	{
		public STATIC_DATA.BaseAngle BaseAngle { get; set; }

		public STATIC_DATA.BaseEffects BaseEffects { get; set; }

		public STATIC_DATA.Angle Angle { get; set; }

		public STATIC_DATA.Effects Effects { get; set; }

		public STATIC_DATA.FlashlightAngle FlashlightAngle { get; set; }

		public STATIC_DATA.FlashlightEffects FlashlightEffects { get; set; }
	}

	public class PauseStaticSettings
	{
		public STATIC_DATA.AngleRange AngleRange { get; set; }

		public STATIC_DATA.ShaderSettings ShaderSettings { get; set; }

		public STATIC_DATA.FlashlightAngleRange FlashlightAngleRange { get; set; }

		public STATIC_DATA.FlashlightAdditive FlashlightAdditive { get; set; }
	}

	public class PhantomWalk
	{
		public STATIC_DATA.BaseAngle BaseAngle { get; set; }

		public STATIC_DATA.BaseEffects BaseEffects { get; set; }

		public STATIC_DATA.Angle Angle { get; set; }

		public STATIC_DATA.Effects Effects { get; set; }

		public STATIC_DATA.FlashlightAngle FlashlightAngle { get; set; }

		public STATIC_DATA.FlashlightEffects FlashlightEffects { get; set; }
	}

	public class PhantomWalkStaticSettings
	{
		public STATIC_DATA.AngleRange AngleRange { get; set; }

		public STATIC_DATA.ShaderSettings ShaderSettings { get; set; }

		public STATIC_DATA.FlashlightAngleRange FlashlightAngleRange { get; set; }

		public STATIC_DATA.FlashlightAdditive FlashlightAdditive { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<STATIC_DATA.Entry> Entries { get; set; }
	}

	public class RunFootstepAdditive
	{
		public bool IsPositional { get; set; }

		public STATIC_DATA.FadeSettings FadeSettings { get; set; }

		public STATIC_DATA.Duration Duration { get; set; }

		public STATIC_DATA.ShaderSettings ShaderSettings { get; set; }
	}

	public class RunFootsteps
	{
		public bool IsPositional { get; set; }

		public STATIC_DATA.FadeSettings FadeSettings { get; set; }

		public STATIC_DATA.Duration Duration { get; set; }

		public STATIC_DATA.Effects Effects { get; set; }
	}

	public class ShaderSettings
	{
		public double Shear { get; set; }

		public double Audio { get; set; }
	}

	public class WalkFootstepAdditive
	{
		public STATIC_DATA.FadeSettings FadeSettings { get; set; }

		public STATIC_DATA.Duration Duration { get; set; }

		public STATIC_DATA.ShaderSettings ShaderSettings { get; set; }
	}

	public class WalkFootsteps
	{
		public STATIC_DATA.FadeSettings FadeSettings { get; set; }

		public STATIC_DATA.Duration Duration { get; set; }

		public STATIC_DATA.Effects Effects { get; set; }
	}
}
