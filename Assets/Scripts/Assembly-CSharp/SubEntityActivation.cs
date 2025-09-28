public class SubEntityActivation
{
	public readonly SubEntityData.SubEntityActivationType ActivationType;

	public readonly float Cooldown;

	public readonly SubEntityData.SubEntityDeactivationRequirement DeactivationRequirement;

	public readonly float DeactivationTime;

	public readonly SubEntityData.SubEntityDeactivationType DeactivationType;

	public SubEntityActivation(SUB_ENTITY_DATA.Activation rawSettings)
	{
		ActivationType = (SubEntityData.SubEntityActivationType)global::System.Enum.Parse(typeof(SubEntityData.SubEntityActivationType), rawSettings.ActivationType);
		Cooldown = rawSettings.ActivationCooldown;
		DeactivationRequirement = (SubEntityData.SubEntityDeactivationRequirement)global::System.Enum.Parse(typeof(SubEntityData.SubEntityDeactivationRequirement), rawSettings.DeactivationRequirement);
		DeactivationTime = rawSettings.DeactivationTime;
		DeactivationType = (SubEntityData.SubEntityDeactivationType)global::System.Enum.Parse(typeof(SubEntityData.SubEntityDeactivationType), rawSettings.DeactivationType);
	}
}
