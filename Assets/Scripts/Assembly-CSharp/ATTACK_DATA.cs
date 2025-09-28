public sealed class ATTACK_DATA
{
	public class Activation
	{
		public float Chance { get; set; }

		public float Modifier { get; set; }
	}

	public class AlgorithmWeights
	{
		public int Nearest { get; set; }

		public int Median { get; set; }

		public int Furthest { get; set; }

		public int Oldest { get; set; }

		public int Newest { get; set; }

		public int Random { get; set; }
	}

	public class AllowedAngle
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class AnimatronicNoiseMeter
	{
		public float ViewingAngle { get; set; }

		public float TimeToJumpScare { get; set; }

		public float TimerDecayPerTick { get; set; }
	}

	public class AngleFromCamera
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class AutoShutdown
	{
		public string ShutdownType { get; set; }

		public int CountTrigger { get; set; }
	}

	public class Base
	{
		public float BaseDrain { get; set; }
	}

	public class Battery
	{
		public ATTACK_DATA.Base Base { get; set; }

		public ATTACK_DATA.Flashlight Flashlight { get; set; }

		public ATTACK_DATA.Shocker Shocker { get; set; }

		public ATTACK_DATA.Haywire Haywire { get; set; }
	}

	public class CameraShake
	{
		public float Magnitude { get; set; }

		public int Roughness { get; set; }

		public float FadeIn { get; set; }

		public float FadeOut { get; set; }
	}

	public class ChangeSpeed
	{
		public float Chance { get; set; }
	}

	public class Charge
	{
		public float Chance { get; set; }

		public float Modifier { get; set; }

		public ATTACK_DATA.Seconds Seconds { get; set; }

		public ATTACK_DATA.ShockWindow ShockWindow { get; set; }

		public float ShockWindowLimit { get; set; }

		public ATTACK_DATA.JumpscareChance JumpscareChance { get; set; }

		public ATTACK_DATA.JumpscareDelayTime JumpscareDelayTime { get; set; }

		public ATTACK_DATA.SkipJumpscareChance SkipJumpscareChance { get; set; }

		public string DeflectionAction { get; set; }

		public bool ForceCircleAfterPause { get; set; }

		public ATTACK_DATA.AutoShutdown AutoShutdown { get; set; }

		public bool DeflectionMustStartDuringCharge { get; set; }

		public ATTACK_DATA.TriggerPercent TriggerPercent { get; set; }

		public int AllowedHalfAngle { get; set; }

		public bool AddToMax { get; set; }

		public bool UseMax { get; set; }

		public ATTACK_DATA.GoToPause GoToPause { get; set; }

		public bool TeleportToCamera { get; set; }

		public ATTACK_DATA.Activation Activation { get; set; }

		public ATTACK_DATA.GoToCircle GoToCircle { get; set; }
	}

	public class ChargeBushingSettings
	{
		public int MaxChargeValue { get; set; }

		public ATTACK_DATA.InitialCharge InitialCharge { get; set; }

		public ATTACK_DATA.TargetChargeMin TargetChargeMin { get; set; }

		public ATTACK_DATA.TargetChargeMax TargetChargeMax { get; set; }

		public ATTACK_DATA.ToleranceAngle ToleranceAngle { get; set; }

		public ATTACK_DATA.FillRatePerSecond FillRatePerSecond { get; set; }

		public ATTACK_DATA.DecayRatePerSecond DecayRatePerSecond { get; set; }

		public ATTACK_DATA.NumLightsPerMeter NumLightsPerMeter { get; set; }

		public ATTACK_DATA.NumBrokenLightsPerMeter NumBrokenLightsPerMeter { get; set; }
	}

	public class Circle
	{
		public ATTACK_DATA.Seconds Seconds { get; set; }

		public ATTACK_DATA.DegreesPerSecond DegreesPerSecond { get; set; }

		public ATTACK_DATA.FootstepSpeedScalar FootstepSpeedScalar { get; set; }

		public ATTACK_DATA.NextPhase NextPhase { get; set; }

		public ATTACK_DATA.ChangeSpeed ChangeSpeed { get; set; }

		public ATTACK_DATA.TriggerPercent TriggerPercent { get; set; }

		public float Chance { get; set; }

		public ATTACK_DATA.Activation Activation { get; set; }

		public int AllowedHalfAngle { get; set; }

		public bool AddToMax { get; set; }

		public bool UseMax { get; set; }

		public ATTACK_DATA.GoToPause GoToPause { get; set; }

		public bool ForceCircleAfterPause { get; set; }

		public bool TeleportToCamera { get; set; }

		public ATTACK_DATA.GoToCircle GoToCircle { get; set; }
	}

	public class CloakDelayTime
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Cooldown
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Count
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class DecayRatePerSecond
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class DegreesPerSecond
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Disruption
	{
		public ATTACK_DATA.Settings Settings { get; set; }

		public ATTACK_DATA.Shake Shake { get; set; }

		public ATTACK_DATA.UITarget UITarget { get; set; }

		public ATTACK_DATA.ScreenObjects ScreenObjects { get; set; }
	}

	public class Distance
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class DistanceFromCamera
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class DropsObjects
	{
		public ATTACK_DATA.Spawn Spawn { get; set; }

		public ATTACK_DATA.OnSuccess OnSuccess { get; set; }

		public ATTACK_DATA.OnFailure OnFailure { get; set; }

		public ATTACK_DATA.UITarget UITarget { get; set; }

		public ATTACK_DATA.Duration Duration { get; set; }
	}

	public class Duration
	{
		public int DropCount { get; set; }

		public string DropDurations { get; set; }
	}

	public class EncounterBurnTime
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Entry
	{
		public string Logical { get; set; }

		public string EncounterType { get; set; }

		public string EnrageType { get; set; }

		public float WaitForCameraTime { get; set; }

		public float OfflineExpireTime { get; set; }

		public ATTACK_DATA.UI UI { get; set; }

		public ATTACK_DATA.IntroScreen IntroScreen { get; set; }

		public ATTACK_DATA.TeleportReposition TeleportReposition { get; set; }

		public ATTACK_DATA.InitialPause InitialPause { get; set; }

		public ATTACK_DATA.Circle Circle { get; set; }

		public ATTACK_DATA.Pause Pause { get; set; }

		public ATTACK_DATA.Glimpse Glimpse { get; set; }

		public ATTACK_DATA.Charge Charge { get; set; }

		public ATTACK_DATA.Jumpscare Jumpscare { get; set; }

		public ATTACK_DATA.Haywire Haywire { get; set; }

		public ATTACK_DATA.Disruption Disruption { get; set; }

		public ATTACK_DATA.Surge Surge { get; set; }

		public ATTACK_DATA.VisibilityAlterEffect VisibilityAlterEffect { get; set; }

		public ATTACK_DATA.DropsObjects DropsObjects { get; set; }

		public ATTACK_DATA.Battery Battery { get; set; }

		public float ShockerCooldownSeconds { get; set; }

		public string StaticProfile { get; set; }

		public ATTACK_DATA.Footsteps Footsteps { get; set; }

		public int NumShocksToDefeat { get; set; }

		public ATTACK_DATA.NoiseMeterSettings NoiseMeterSettings { get; set; }

		public ATTACK_DATA.LookAwayApproach LookAwayApproach { get; set; }

		public ATTACK_DATA.ChargeBushingSettings ChargeBushingSettings { get; set; }

		public ATTACK_DATA.PhantomWalk PhantomWalk { get; set; }

		public ATTACK_DATA.PhantomOverload PhantomOverload { get; set; }

		public ATTACK_DATA.PhantomPause PhantomPause { get; set; }

		public ATTACK_DATA.PhantomSettings PhantomSettings { get; set; }

		public ATTACK_DATA.EncounterTriggerSettings EncounterTriggerSettings { get; set; }

		public ATTACK_DATA.Environment Environment { get; set; }

		public ATTACK_DATA.AttackDataSlash Slash { get; set; }
	}

	public class Environment
	{
		public string Bundle { get; set; }

		public string Asset { get; set; }
	}

	public class ExpireTime
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class EncounterTriggerSettings
	{
		public string PreShutdownAction { get; set; }

		public string ShutdownAction { get; set; }

		public string DamagedAction { get; set; }

		public string SlashedAction { get; set; }
	}

	public class Fallback
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class FillRatePerSecond
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Flashlight
	{
		public float ActivationDrain { get; set; }

		public float ActiveDrain { get; set; }
	}

	public class FlashlightDisableTime
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Footsteps
	{
		public ATTACK_DATA.Walk Walk { get; set; }

		public ATTACK_DATA.Run Run { get; set; }
	}

	public class FootstepSpeedScalar
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Glimpse
	{
		public float Chance { get; set; }

		public ATTACK_DATA.Cooldown Cooldown { get; set; }

		public ATTACK_DATA.Distance Distance { get; set; }

		public ATTACK_DATA.CloakDelayTime CloakDelayTime { get; set; }

		public ATTACK_DATA.ExpireTime ExpireTime { get; set; }

		public ATTACK_DATA.HalfAngleDeadZone HalfAngleDeadZone { get; set; }

		public ATTACK_DATA.HalfAngleTeleport HalfAngleTeleport { get; set; }

		public ATTACK_DATA.PhaseDuration PhaseDuration { get; set; }

		public ATTACK_DATA.RepositionDelay RepositionDelay { get; set; }
	}

	public class GoToCircle
	{
		public float Chance { get; set; }
	}

	public class GoToPause
	{
		public float Chance { get; set; }
	}

	public class HalfAngle
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class HalfAngleDeadZone
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class HalfAngleTeleport
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Haywire
	{
		public ATTACK_DATA.HaywireSettings Settings { get; set; }

		public ATTACK_DATA.Circle Circle { get; set; }

		public ATTACK_DATA.Pause Pause { get; set; }

		public ATTACK_DATA.Charge Charge { get; set; }

		public ATTACK_DATA.Multiwire Multiwire { get; set; }

		public float ShockDrain { get; set; }
	}

	public class HeightOffset
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class HiddenTime
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class InitialCharge
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class InitialPause
	{
		public ATTACK_DATA.Seconds Seconds { get; set; }
	}

	public class IntroScreen
	{
		public string Bundle;

		public string Asset;

		public string Page1Loc;

		public string Page2Loc;

		public string Page3Loc;

		public string Page4Loc;
	}

	public class Jumpscare
	{
		public string VibrationType { get; set; }
	}

	public class JumpscareChance
	{
		public float Modifier { get; set; }

		public float Chance { get; set; }
	}

	public class JumpscareDelayTime
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class LocateTime
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class LookAwayApproach
	{
		public int ApproachSpeed { get; set; }

		public int StareDurationForBlackoutNoChargeSeconds { get; set; }

		public int StareDurationForBlackoutFullChargeSeconds { get; set; }
	}

	public class LookChangeTriggerPercent
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class ContinuousChangeTriggerInterval
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Multiwire
	{
		public ATTACK_DATA.Count Count { get; set; }

		public ATTACK_DATA.HalfAngle HalfAngle { get; set; }

		public ATTACK_DATA.LocateTime LocateTime { get; set; }

		public ATTACK_DATA.HiddenTime HiddenTime { get; set; }
	}

	public class Multislash
	{
		public ATTACK_DATA.Count Count { get; set; }

		public ATTACK_DATA.HalfAngle HalfAngle { get; set; }

		public ATTACK_DATA.LocateTime LocateTime { get; set; }

		public ATTACK_DATA.HiddenTime HiddenTime { get; set; }
	}

	public class NextPhase
	{
		public ATTACK_DATA.Pause Pause { get; set; }

		public ATTACK_DATA.Circle Circle { get; set; }

		public ATTACK_DATA.Charge Charge { get; set; }

		public ATTACK_DATA.Glimpse Glimpse { get; set; }
	}

	public class NoiseMeterSettings
	{
		public ATTACK_DATA.PlayerNoiseMeter PlayerNoiseMeter { get; set; }

		public ATTACK_DATA.AnimatronicNoiseMeter AnimatronicNoiseMeter { get; set; }
	}

	public class NumBrokenLightsPerMeter
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class NumLightsPerMeter
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class OnFailure
	{
		public string FailureAction { get; set; }
	}

	public class OnSuccess
	{
		public string SuccessAction { get; set; }

		public int CollectCount { get; set; }
	}

	public class Pause
	{
		public float Chance { get; set; }

		public ATTACK_DATA.Seconds Seconds { get; set; }

		public bool UseGlimpse { get; set; }

		public ATTACK_DATA.NextPhase NextPhase { get; set; }

		public ATTACK_DATA.Activation Activation { get; set; }

		public ATTACK_DATA.TriggerPercent TriggerPercent { get; set; }

		public int AllowedHalfAngle { get; set; }

		public bool AddToMax { get; set; }

		public bool UseMax { get; set; }

		public ATTACK_DATA.GoToPause GoToPause { get; set; }

		public bool ForceCircleAfterPause { get; set; }

		public bool TeleportToCamera { get; set; }

		public ATTACK_DATA.GoToCircle GoToCircle { get; set; }
	}

	public class PhantomOverload
	{
		public ATTACK_DATA.ReactionTime ReactionTime { get; set; }

		public ATTACK_DATA.FlashlightDisableTime FlashlightDisableTime { get; set; }
	}

	public class PhantomPause
	{
		public ATTACK_DATA.Seconds Seconds { get; set; }
	}

	public class PhantomSettings
	{
		public bool UseGlobalMovement { get; set; }

		public bool AndroidOcclusionEnabled { get; set; }

		public bool AndroidOcclusionBlocksFlashlight { get; set; }
	}

	public class PhantomWalk
	{
		public ATTACK_DATA.EncounterBurnTime EncounterBurnTime { get; set; }

		public ATTACK_DATA.PhaseBurnTime PhaseBurnTime { get; set; }

		public bool ShouldHaywire { get; set; }
	}

	public class PhaseBurnTime
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class PhaseDuration
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class PlayerNoiseMeter
	{
		public float NoiseToJumpScare { get; set; }

		public float NoiseDecayPerTick { get; set; }

		public float RotationScale { get; set; }

		public int RotationExponent { get; set; }

		public int PositionExponentX { get; set; }

		public int PositionExponentY { get; set; }

		public int PositionExponentZ { get; set; }

		public int PositionScaleX { get; set; }

		public int PositionScaleY { get; set; }

		public int PositionScaleZ { get; set; }
	}

	public class ReactionTime
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class RepositionDelay
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<ATTACK_DATA.Entry> Entries { get; set; }
	}

	public class Run
	{
		public float EffectDelay { get; set; }

		public string VibrationType { get; set; }

		public ATTACK_DATA.CameraShake CameraShake { get; set; }
	}

	public class ScreenObjects
	{
		public int AnimationSpeed { get; set; }

		public string AnimationDurations { get; set; }

		public ATTACK_DATA.Cooldown Cooldown { get; set; }
	}

	public class Seconds
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Settings
	{
		public ATTACK_DATA.Seconds Seconds { get; set; }

		public bool ShockCausesAttack { get; set; }

		public bool ShockCausesJumpscare { get; set; }

		public string RequiredMaskState { get; set; }

		public ATTACK_DATA.ShouldLookAway ShouldLookAway { get; set; }

		public float LookTime { get; set; }

		public ATTACK_DATA.Cooldown Cooldown { get; set; }

		public int MaxCount { get; set; }

		public ATTACK_DATA.ShouldLookAt ShouldLookAt { get; set; }

		public ATTACK_DATA.ShouldLookAtThenAway ShouldLookAtThenAway { get; set; }

		public ATTACK_DATA.LookChangeTriggerPercent LookChangeTriggerPercent { get; set; }

		public string Style { get; set; }

		public ATTACK_DATA.Activation Activation { get; set; }

		public float RampTime { get; set; }

		public float CancelTime { get; set; }

		public string CancelAction { get; set; }

		public float BatteryDrainRateBase { get; set; }

		public float BatteryDrainRateCancel { get; set; }

		public float BlinkDuration { get; set; }

		public float MaskLightFadeTime { get; set; }

		public string HapticCue { get; set; }

		public bool WhileCircling { get; set; }

		public bool WhilePaused { get; set; }

		public float YOffset { get; set; }

		public int Distance { get; set; }
	}

	public class HaywireSettings
	{
		public ATTACK_DATA.Seconds Seconds { get; set; }

		public bool ShockCausesAttack { get; set; }

		public bool ShockCausesJumpscare { get; set; }

		public bool CoverEyes { get; set; }

		public string RequiredMaskState { get; set; }

		public ATTACK_DATA.ShouldLookAway ShouldLookAway { get; set; }

		public float LookTime { get; set; }

		public int Cooldown { get; set; }

		public int MaxCount { get; set; }

		public ATTACK_DATA.ShouldLookAt ShouldLookAt { get; set; }

		public ATTACK_DATA.ShouldLookAtThenAway ShouldLookAtThenAway { get; set; }

		public ATTACK_DATA.ShouldLookContinuous ShouldLookContinuous { get; set; }

		public ATTACK_DATA.LookChangeTriggerPercent LookChangeTriggerPercent { get; set; }

		public ATTACK_DATA.ContinuousChangeTriggerInterval ContinuousChangeTriggerInterval { get; set; }

		public string Style { get; set; }

		public ATTACK_DATA.Activation Activation { get; set; }

		public float RampTime { get; set; }

		public float CancelTime { get; set; }

		public string CancelAction { get; set; }

		public float BatteryDrainRateBase { get; set; }

		public float BatteryDrainRateCancel { get; set; }

		public float BlinkDuration { get; set; }

		public float MaskLightFadeTime { get; set; }

		public string HapticCue { get; set; }

		public bool WhileCircling { get; set; }

		public bool WhilePaused { get; set; }

		public float YOffset { get; set; }

		public int Distance { get; set; }
	}

	public class SlashSettings
	{
		public float Seconds { get; set; }

		public int Cooldown { get; set; }

		public int MaxCount { get; set; }
	}

	public class Shake
	{
		public ATTACK_DATA.ShakeMagnitude ShakeMagnitude { get; set; }

		public float ShakeGraceTime { get; set; }
	}

	public class ShakeMagnitude
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class Shocker
	{
		public float ActivationDrain { get; set; }

		public float ActiveDrain { get; set; }
	}

	public class ShockWindow
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class ShouldLookAt
	{
		public float Chance { get; set; }
	}

	public class ShouldLookAtThenAway
	{
		public float Chance { get; set; }

		public float Modifier { get; set; }
	}

	public class ShouldLookAway
	{
		public float Chance { get; set; }
	}

	public class ShouldLookContinuous
	{
		public float Chance { get; set; }
	}

	public class SkipJumpscareChance
	{
		public float Modifier { get; set; }

		public float Chance { get; set; }
	}

	public class AttackDataSlash
	{
		public ATTACK_DATA.SlashSettings Settings { get; set; }

		public ATTACK_DATA.Circle Circle { get; set; }

		public ATTACK_DATA.Pause Pause { get; set; }

		public ATTACK_DATA.Multislash Multislash { get; set; }
	}

	public class Spawn
	{
		public string SpawnType { get; set; }

		public ATTACK_DATA.AlgorithmWeights AlgorithmWeights { get; set; }

		public ATTACK_DATA.Cooldown Cooldown { get; set; }

		public ATTACK_DATA.Fallback Fallback { get; set; }

		public string BlacklistedPhases { get; set; }

		public string TemplateBundleName { get; set; }

		public string TemplateAssetName { get; set; }

		public ATTACK_DATA.AllowedAngle AllowedAngle { get; set; }

		public ATTACK_DATA.Distance Distance { get; set; }

		public ATTACK_DATA.HeightOffset HeightOffset { get; set; }

		public int MaxConcurrentObjects { get; set; }
	}

	public class Surge
	{
		public ATTACK_DATA.Settings Settings { get; set; }
	}

	public class TargetChargeMin
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class TargetChargeMax
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class TeleportReposition
	{
		public ATTACK_DATA.AngleFromCamera AngleFromCamera { get; set; }

		public ATTACK_DATA.DistanceFromCamera DistanceFromCamera { get; set; }
	}

	public class ToleranceAngle
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class TriggerPercent
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class UI
	{
		public bool UseShocker { get; set; }

		public bool UseFlashlight { get; set; }

		public bool UseMask { get; set; }

		public bool ShowRemnant { get; set; }

		public bool ShowCollection { get; set; }

		public bool UsePlayerNoiseMeter { get; set; }

		public bool UseAnimatronicNoiseMeter { get; set; }

		public bool UseContinuousShocker { get; set; }

		public bool UseSwapper { get; set; }

		public bool ShowBillboard { get; set; }
	}

	public class UITarget
	{
		public string UITargetType { get; set; }

		public ATTACK_DATA.UITargetRange UITargetRange { get; set; }

		public float UITargetDuration { get; set; }
	}

	public class UITargetRange
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class VisibilityAlterEffect
	{
		public ATTACK_DATA.Settings Settings { get; set; }
	}

	public class Walk
	{
		public float EffectDelay { get; set; }

		public string VibrationType { get; set; }

		public ATTACK_DATA.CameraShake CameraShake { get; set; }
	}
}
