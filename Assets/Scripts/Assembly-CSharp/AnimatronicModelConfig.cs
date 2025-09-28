public class AnimatronicModelConfig : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Model Transforms")]
	public ModelTransforms ModelTransforms;

	[global::UnityEngine.Header("Colliders")]
	public global::UnityEngine.BoxCollider AABBCollider;

	[global::UnityEngine.Header("Effect Connections")]
	public AnimatronicMaterialController AnimatronicMaterialController;

	public LightningFxController ShockLightningEffect;

	public EyeGlowLightController EyeGlowLightController;

	public PhantomFxController PhantomFxController;

	[global::UnityEngine.Header("Component Connections")]
	public AnimationEventListener AnimationEventListener;

	public global::UnityEngine.Animator Animator;

	public AnimatronicAudioManager AnimatronicAudioManager;

	public FieldOfView FieldOfView;

	[global::UnityEngine.Header("Animatronic Settings")]
	public AdditionalOffsets AdditionalOffsets;

	public CloakSettings CloakSettings;

	public MovementSettings MovementSettings;
}
