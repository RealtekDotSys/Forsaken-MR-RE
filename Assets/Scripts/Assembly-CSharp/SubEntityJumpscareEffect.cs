public class SubEntityJumpscareEffect
{
	public readonly SubEntityData.SubEntityJumpscareEffectType Type;

	public SubEntityJumpscareEffect(SUB_ENTITY_DATA.JumpscareEffect rawSettings)
	{
		Type = (SubEntityData.SubEntityJumpscareEffectType)global::System.Enum.Parse(typeof(SubEntityData.SubEntityJumpscareEffectType), rawSettings.JumpscareEffectType);
	}
}
