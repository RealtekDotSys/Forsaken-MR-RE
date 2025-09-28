public class ForsakenAnimationEventListener : global::UnityEngine.MonoBehaviour
{
	private AnimatronicModelConfig modelConfig;

	public void RaiseAnimationEvent(global::UnityEngine.AnimationEvent animationEvent)
	{
		switch (animationEvent.intParameter)
		{
		case 1003:
			modelConfig = GetComponentInParent<AnimatronicModelConfig>();
			modelConfig.AnimatronicMaterialController.SetEyeGlow(eyeGlowEnabled: true);
			break;
		case 1004:
			modelConfig = GetComponentInParent<AnimatronicModelConfig>();
			modelConfig.AnimatronicMaterialController.SetEyeGlow(eyeGlowEnabled: false);
			break;
		case 3000:
			_ = animationEvent.stringParameter;
			break;
		}
	}
}
