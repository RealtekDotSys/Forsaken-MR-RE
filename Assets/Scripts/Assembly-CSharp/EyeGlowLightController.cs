[global::UnityEngine.ExecuteInEditMode]
public class EyeGlowLightController : global::UnityEngine.MonoBehaviour
{
	public global::System.Collections.Generic.List<global::UnityEngine.Transform> eyeGlows;

	public global::System.Collections.Generic.List<global::UnityEngine.Transform> eyelidBones;

	public global::System.Collections.Generic.List<global::VLB.VolumetricLightBeam> eyeGlowLights;

	public float eyelidOpenValue;

	public float eyelidClosedValue;

	public global::UnityEngine.GameObject revealPlane;

	public float intensityMultiplier = 5f;

	public global::UnityEngine.Light eyeGlowPointLight;

	public global::UnityEngine.GameObject eyeGlowGrp;

	public global::UnityEngine.Color normalEyeGlowColor = global::UnityEngine.Color.black;

	private float _currentEyelidRotation;

	private global::UnityEngine.Color _currentColor;

	private void Start()
	{
		_currentColor = normalEyeGlowColor;
	}

	private void Update()
	{
		float num = 0f;
		if (eyeGlows.Count > 0)
		{
			for (int i = 0; i < eyeGlows.Count; i++)
			{
				float num2 = 1f;
				if (eyelidBones.Count > i)
				{
					_currentEyelidRotation = eyelidBones[i].localRotation.x;
					num2 = global::UnityEngine.Mathf.Clamp(global::UnityEngine.Mathf.Lerp(1f, 0f, global::UnityEngine.Mathf.InverseLerp(eyelidOpenValue, eyelidClosedValue, _currentEyelidRotation)), 0f, 1f);
				}
				global::UnityEngine.Vector3 localScale = revealPlane.transform.localScale;
				float num3 = global::UnityEngine.Mathf.Clamp(localScale.y, 0f, 50f);
				float num4 = localScale.x * intensityMultiplier;
				float num5 = num2 * (num4 * (global::UnityEngine.Time.deltaTime * num3 + 1f));
				if (eyeGlowLights.Count > i)
				{
					eyeGlowLights[i].intensityInside = num5;
					eyeGlowLights[i].color = _currentColor;
				}
				eyeGlowPointLight.color = _currentColor;
				num += num5;
			}
		}
		eyeGlowPointLight.intensity = global::UnityEngine.Mathf.Clamp(num, 0f, 3f);
	}

	public void StartEyeColorOverride(global::UnityEngine.Color color)
	{
		_currentColor = color;
	}

	public void EndEyeColorOverride()
	{
		_currentColor = normalEyeGlowColor;
	}

	public void SetEyeGlow(bool eyeGlowEnabled)
	{
		eyeGlowGrp.SetActive(eyeGlowEnabled);
	}
}
