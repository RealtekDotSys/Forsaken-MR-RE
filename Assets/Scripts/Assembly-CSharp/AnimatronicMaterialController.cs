[global::UnityEngine.ExecuteInEditMode]
public class AnimatronicMaterialController : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject revealPlane;

	public global::UnityEngine.GameObject eyeRevealPlane;

	public global::UnityEngine.Material[] bodyMaterials;

	public global::UnityEngine.Material[] eyeMaterials;

	public global::UnityEngine.Material[] minWearAndTearMaterials;

	public global::UnityEngine.Material[] maxWearAndTearMaterials;

	public float eyeGlowMultiplier;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Range(0f, 1f)]
	private float _eyeGlowStrength;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Range(0f, 1f)]
	private float _eyeOverrideSwitch;

	[global::UnityEngine.Range(0f, 100f)]
	public float wearAndTearPercentage;

	[global::UnityEngine.Range(0f, 1f)]
	public float offset;

	private readonly global::System.Collections.Generic.List<string> _parameterNames;

	private bool _shouldPerformRepairInterpolation;

	private bool _hasEyeColorOverride;

	private global::UnityEngine.Color _eyeColorOverride = new global::UnityEngine.Color(1f, 0f, 0f, 1f);

	private global::UnityEngine.SkinnedMeshRenderer _renderer;

	private global::UnityEngine.MaterialPropertyBlock _propertyBlock;

	private global::UnityEngine.Material[] _materials;

	private bool DontLateUpdate;

	private void OnEnable()
	{
		_renderer = GetComponent<global::UnityEngine.SkinnedMeshRenderer>();
		_materials = _renderer.sharedMaterials;
		_propertyBlock = new global::UnityEngine.MaterialPropertyBlock();
		_renderer.SetPropertyBlock(_propertyBlock);
	}

	private void LateUpdate()
	{
		if (!DontLateUpdate)
		{
			if (_shouldPerformRepairInterpolation)
			{
				PerformRepairInterpolation();
				return;
			}
			SetBodyMaterialProperties();
			SetEyeMaterialProperties();
		}
	}

	public void LaterUpdater()
	{
		DontLateUpdate = true;
		if (_shouldPerformRepairInterpolation)
		{
			PerformRepairInterpolation();
			return;
		}
		SetBodyMaterialProperties();
		SetEyeMaterialProperties();
	}

	private void SetBodyMaterialProperties()
	{
		float t = wearAndTearPercentage / 100f;
		_propertyBlock.Clear();
		global::UnityEngine.Material[] array = bodyMaterials;
		foreach (global::UnityEngine.Material material in array)
		{
			int num = global::System.Array.IndexOf(bodyMaterials, material);
			if (num < minWearAndTearMaterials.Length)
			{
				foreach (string parameterName in _parameterNames)
				{
					if (parameterName.ToLower().Contains("offset"))
					{
						_propertyBlock.SetFloat(parameterName, offset);
					}
					else
					{
						_propertyBlock.SetFloat(parameterName, global::UnityEngine.Mathf.Lerp(minWearAndTearMaterials[num].GetFloat(parameterName), maxWearAndTearMaterials[num].GetFloat(parameterName), t));
					}
				}
			}
			_propertyBlock.SetVector("_PlanePosition", revealPlane.transform.position);
			_propertyBlock.SetVector("_PlaneNormal", revealPlane.transform.forward);
			_renderer.SetPropertyBlock(_propertyBlock, GetMaterialIndex(material));
		}
	}

	private void SetEyeMaterialProperties()
	{
		float value = wearAndTearPercentage / 100f * 0.75f;
		float value2 = wearAndTearPercentage / 100f * 0.25f;
		global::UnityEngine.Vector3 localScale = revealPlane.transform.localScale;
		float value3 = localScale.x * eyeGlowMultiplier * _eyeGlowStrength;
		float y = localScale.y;
		_propertyBlock.Clear();
		global::UnityEngine.Material[] array = eyeMaterials;
		foreach (global::UnityEngine.Material material in array)
		{
			_propertyBlock.SetFloat("_GrungeDirtBalance", value);
			_propertyBlock.SetFloat("_GrungeSmearBalance", value2);
			_propertyBlock.SetFloat("_GlowAmount", value3);
			_propertyBlock.SetFloat("_GlowFlicker", y);
			_propertyBlock.SetVector("_PlanePosition", eyeRevealPlane.transform.position);
			_propertyBlock.SetVector("_PlaneNormal", eyeRevealPlane.transform.forward);
			_propertyBlock.SetColor("_ExterminateColor", _eyeColorOverride);
			_propertyBlock.SetFloat("_ExterminateSwitch", _eyeOverrideSwitch);
			_renderer.SetPropertyBlock(_propertyBlock, GetMaterialIndex(material));
		}
	}

	private void PerformRepairInterpolation()
	{
		if (wearAndTearPercentage > 0f)
		{
			wearAndTearPercentage += -1f;
			SetBodyMaterialProperties();
			SetEyeMaterialProperties();
		}
		else
		{
			wearAndTearPercentage = 0f;
			_shouldPerformRepairInterpolation = false;
		}
	}

	private int GetMaterialIndex(global::UnityEngine.Material material)
	{
		return global::System.Array.IndexOf(_materials, material);
	}

	public bool IsGlowing()
	{
		return _eyeGlowStrength > 0f;
	}

	public bool IsColorOverriden()
	{
		if (global::UnityEngine.Mathf.Approximately(_eyeOverrideSwitch, 1f))
		{
			return _hasEyeColorOverride;
		}
		return false;
	}

	public void StartEyeColorOverride(global::UnityEngine.Color color)
	{
		_eyeColorOverride = color;
		_hasEyeColorOverride = true;
		_eyeOverrideSwitch = 1f;
	}

	public void EndEyeColorOverride()
	{
		_hasEyeColorOverride = false;
		_eyeOverrideSwitch = 0f;
	}

	public global::UnityEngine.Color GetEyeColorOverride()
	{
		float r = _eyeColorOverride.r;
		float g = _eyeColorOverride.g;
		float b = _eyeColorOverride.b;
		float a = _eyeColorOverride.a;
		return new global::UnityEngine.Color(r, g, b, a);
	}

	public void SetEyeGlowIntensity(float intensity)
	{
		eyeGlowMultiplier = ((intensity < 0f) ? 1f : intensity);
	}

	public void SetEyeGlow(bool eyeGlowEnabled)
	{
		_eyeGlowStrength = (eyeGlowEnabled ? 1f : 0f);
	}

	public void TriggerRepairInterpolation()
	{
		_shouldPerformRepairInterpolation = true;
	}

	public AnimatronicMaterialController()
	{
		eyeGlowMultiplier = 1f;
		_parameterNames = new global::System.Collections.Generic.List<string>
		{
			"_PaintsRoughness", "_PaintsDirtBalance", "_PaintsDirtScratchesBalance", "_PaintsDirtScratchesContrast", "_PaintsScratchesOffset", "_WearCurvatureBalance", "_WearCurveGrimeTiling", "_WearGrungeBalance", "_WearGrungeTiling", "_WearGrungeOffset",
			"_WearScratchesBalance", "_MetalOilRoughness", "_MetalOilSmearBalance", "_MetalOilScratchesTiling", "_VertScale", "_VertDisplace"
		};
	}
}
