public class SubEntityJumpscare
{
	public readonly SubEntityData.SubEntityJumpscareSource Source;

	public readonly float TimeToJumpscare;

	public readonly bool PauseWhileDeactivationRequirementActive;

	public readonly global::System.Collections.Generic.List<SubEntityJumpscareEffect> JumpscareEffects = new global::System.Collections.Generic.List<SubEntityJumpscareEffect>();

	public readonly float EffectSeconds;

	public SubEntityJumpscare(SUB_ENTITY_DATA.Jumpscare rawSettings)
	{
		Source = (SubEntityData.SubEntityJumpscareSource)global::System.Enum.Parse(typeof(SubEntityData.SubEntityJumpscareSource), rawSettings.JumpscareSource);
		TimeToJumpscare = rawSettings.TimeToJumpscare;
		PauseWhileDeactivationRequirementActive = rawSettings.PauseWhileDeactivationRequirementActive;
		if (rawSettings.JumpscareEffects != null)
		{
			foreach (SUB_ENTITY_DATA.JumpscareEffect jumpscareEffect in rawSettings.JumpscareEffects)
			{
				JumpscareEffects.Add(new SubEntityJumpscareEffect(jumpscareEffect));
			}
		}
		EffectSeconds = rawSettings.JumpscareEffectSeconds;
	}
}
