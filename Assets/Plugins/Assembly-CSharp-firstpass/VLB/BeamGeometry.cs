namespace VLB
{
	[global::UnityEngine.AddComponentMenu("")]
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.HelpURL("http://saladgamer.com/vlb-doc/comp-lightbeam/")]
	public class BeamGeometry : global::UnityEngine.MonoBehaviour
	{
		private global::VLB.VolumetricLightBeam m_Master;

		private global::UnityEngine.Matrix4x4 m_ColorGradientMatrix;

		private global::VLB.MeshType m_CurrentMeshType;

		private global::UnityEngine.Material m_CustomMaterial;

		private global::UnityEngine.Plane m_ClippingPlaneWS;

		public global::UnityEngine.MeshRenderer meshRenderer { get; private set; }

		public global::UnityEngine.MeshFilter meshFilter { get; private set; }

		public global::UnityEngine.Mesh coneMesh { get; private set; }

		public bool visible
		{
			get
			{
				return meshRenderer.enabled;
			}
			set
			{
				meshRenderer.enabled = value;
			}
		}

		public int sortingLayerID
		{
			get
			{
				return meshRenderer.sortingLayerID;
			}
			set
			{
				meshRenderer.sortingLayerID = value;
			}
		}

		public int sortingOrder
		{
			get
			{
				return meshRenderer.sortingOrder;
			}
			set
			{
				meshRenderer.sortingOrder = value;
			}
		}

		public static bool isCustomRenderPipelineSupported => true;

		private bool isNoiseEnabled
		{
			get
			{
				if (m_Master.noiseEnabled && m_Master.noiseIntensity > 0f)
				{
					return global::VLB.Noise3D.isSupported;
				}
				return false;
			}
		}

		private bool isClippingPlaneEnabled => m_ClippingPlaneWS.normal.sqrMagnitude > 0f;

		private bool isDepthBlendEnabled
		{
			get
			{
				if (!global::VLB.GpuInstancing.forceEnableDepthBlend)
				{
					return m_Master.depthBlendDistance > 0f;
				}
				return true;
			}
		}

		private float ComputeFadeOutFactor(global::UnityEngine.Transform camTransform)
		{
			if (m_Master.isFadeOutEnabled)
			{
				float value = global::UnityEngine.Vector3.SqrMagnitude(meshRenderer.bounds.center - camTransform.position);
				return global::UnityEngine.Mathf.InverseLerp(m_Master.fadeOutEnd * m_Master.fadeOutEnd, m_Master.fadeOutBegin * m_Master.fadeOutBegin, value);
			}
			return 1f;
		}

		private global::System.Collections.IEnumerator CoUpdateFadeOut()
		{
			while (m_Master.isFadeOutEnabled)
			{
				ComputeFadeOutFactor();
				yield return null;
			}
			SetFadeOutFactorProp(1f);
		}

		private void ComputeFadeOutFactor()
		{
			global::UnityEngine.Transform fadeOutCameraTransform = global::VLB.Config.Instance.fadeOutCameraTransform;
			if ((bool)fadeOutCameraTransform)
			{
				float num = ComputeFadeOutFactor(fadeOutCameraTransform);
				if (num > 0f)
				{
					meshRenderer.enabled = true;
					SetFadeOutFactorProp(num);
				}
				else
				{
					meshRenderer.enabled = false;
				}
			}
			else
			{
				SetFadeOutFactorProp(1f);
			}
		}

		private void SetFadeOutFactorProp(float value)
		{
			MaterialChangeStart();
			SetMaterialProp("_FadeOutFactor", value);
			MaterialChangeStop();
		}

		private void RestartFadeOutCoroutine()
		{
			StopAllCoroutines();
			if ((bool)m_Master && m_Master.isFadeOutEnabled)
			{
				StartCoroutine(CoUpdateFadeOut());
			}
		}

		private void Start()
		{
			if (!m_Master)
			{
				global::UnityEngine.Object.DestroyImmediate(base.gameObject);
			}
		}

		private void OnDestroy()
		{
			if ((bool)m_CustomMaterial)
			{
				global::UnityEngine.Object.DestroyImmediate(m_CustomMaterial);
				m_CustomMaterial = null;
			}
		}

		private static bool IsUsingCustomRenderPipeline()
		{
			if (global::UnityEngine.Rendering.RenderPipelineManager.currentPipeline == null)
			{
				return global::UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset != null;
			}
			return true;
		}

		private void OnDisable()
		{
			if (IsUsingCustomRenderPipeline())
			{
				global::UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
			}
		}

		private void OnEnable()
		{
			RestartFadeOutCoroutine();
			if (IsUsingCustomRenderPipeline())
			{
				global::UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
			}
		}

		public void Initialize(global::VLB.VolumetricLightBeam master)
		{
			global::UnityEngine.HideFlags proceduralObjectsHideFlags = global::VLB.Consts.ProceduralObjectsHideFlags;
			m_Master = master;
			base.transform.SetParent(master.transform, worldPositionStays: false);
			meshRenderer = base.gameObject.GetOrAddComponent<global::UnityEngine.MeshRenderer>();
			meshRenderer.hideFlags = proceduralObjectsHideFlags;
			meshRenderer.shadowCastingMode = global::UnityEngine.Rendering.ShadowCastingMode.Off;
			meshRenderer.receiveShadows = false;
			meshRenderer.lightProbeUsage = global::UnityEngine.Rendering.LightProbeUsage.Off;
			if (global::VLB.Config.Instance.actualRenderingMode != global::VLB.RenderingMode.GPUInstancing)
			{
				m_CustomMaterial = global::VLB.MaterialManager.NewMaterial(gpuInstanced: false);
				ApplyMaterial();
			}
			if (global::UnityEngine.SortingLayer.IsValid(m_Master.sortingLayerID))
			{
				sortingLayerID = m_Master.sortingLayerID;
			}
			else
			{
				global::UnityEngine.Debug.LogError($"Beam '{(global::VLB.Utils.GetPath(m_Master.transform))}' has an invalid sortingLayerID ({m_Master.sortingLayerID}). Please fix it by setting a valid layer.");
			}
			sortingOrder = m_Master.sortingOrder;
			meshFilter = base.gameObject.GetOrAddComponent<global::UnityEngine.MeshFilter>();
			meshFilter.hideFlags = proceduralObjectsHideFlags;
			base.gameObject.hideFlags = proceduralObjectsHideFlags;
			RestartFadeOutCoroutine();
		}

		public void RegenerateMesh()
		{
			if (global::VLB.Config.Instance.geometryOverrideLayer)
			{
				base.gameObject.layer = global::VLB.Config.Instance.geometryLayerID;
			}
			else
			{
				base.gameObject.layer = m_Master.gameObject.layer;
			}
			base.gameObject.tag = global::VLB.Config.Instance.geometryTag;
			if ((bool)coneMesh && m_CurrentMeshType == global::VLB.MeshType.Custom)
			{
				global::UnityEngine.Object.DestroyImmediate(coneMesh);
			}
			m_CurrentMeshType = m_Master.geomMeshType;
			switch (m_Master.geomMeshType)
			{
			case global::VLB.MeshType.Custom:
				coneMesh = global::VLB.MeshGenerator.GenerateConeZ_Radius(1f, 1f, 1f, m_Master.geomCustomSides, m_Master.geomCustomSegments, m_Master.geomCap, global::VLB.Config.Instance.useSinglePassShader);
				coneMesh.hideFlags = global::VLB.Consts.ProceduralObjectsHideFlags;
				meshFilter.mesh = coneMesh;
				break;
			case global::VLB.MeshType.Shared:
				coneMesh = global::VLB.GlobalMesh.Get();
				meshFilter.sharedMesh = coneMesh;
				break;
			default:
				global::UnityEngine.Debug.LogError("Unsupported MeshType");
				break;
			}
			UpdateMaterialAndBounds();
		}

		private void ComputeLocalMatrix()
		{
			float num = global::UnityEngine.Mathf.Max(m_Master.coneRadiusStart, m_Master.coneRadiusEnd);
			base.transform.localScale = new global::UnityEngine.Vector3(num, num, m_Master.fallOffEnd);
		}

		private bool ApplyMaterial()
		{
			global::VLB.MaterialManager.ColorGradient colorGradient = global::VLB.MaterialManager.ColorGradient.Off;
			if (m_Master.colorMode == global::VLB.ColorMode.Gradient)
			{
				colorGradient = ((global::VLB.Utils.GetFloatPackingPrecision() != global::VLB.Utils.FloatPackingPrecision.High) ? global::VLB.MaterialManager.ColorGradient.MatrixLow : global::VLB.MaterialManager.ColorGradient.MatrixHigh);
			}
			global::VLB.MaterialManager.StaticProperties staticProperties = new global::VLB.MaterialManager.StaticProperties
			{
				blendingMode = (global::VLB.MaterialManager.BlendingMode)m_Master.blendingMode,
				noise3D = (isNoiseEnabled ? global::VLB.MaterialManager.Noise3D.On : global::VLB.MaterialManager.Noise3D.Off),
				depthBlend = (isDepthBlendEnabled ? global::VLB.MaterialManager.DepthBlend.On : global::VLB.MaterialManager.DepthBlend.Off),
				colorGradient = colorGradient,
				clippingPlane = (isClippingPlaneEnabled ? global::VLB.MaterialManager.ClippingPlane.On : global::VLB.MaterialManager.ClippingPlane.Off)
			};
			global::UnityEngine.Material material = null;
			if (global::VLB.Config.Instance.actualRenderingMode != global::VLB.RenderingMode.GPUInstancing)
			{
				material = m_CustomMaterial;
				if ((bool)material)
				{
					staticProperties.ApplyToMaterial(material);
				}
			}
			else
			{
				material = global::VLB.MaterialManager.GetInstancedMaterial(m_Master._INTERNAL_InstancedMaterialGroupID, staticProperties);
			}
			meshRenderer.material = material;
			return material != null;
		}

		private void SetMaterialProp(string name, float value)
		{
			if ((bool)m_CustomMaterial)
			{
				m_CustomMaterial.SetFloat(name, value);
			}
			else
			{
				global::VLB.MaterialManager.materialPropertyBlock.SetFloat(name, value);
			}
		}

		private void SetMaterialProp(string name, global::UnityEngine.Vector4 value)
		{
			if ((bool)m_CustomMaterial)
			{
				m_CustomMaterial.SetVector(name, value);
			}
			else
			{
				global::VLB.MaterialManager.materialPropertyBlock.SetVector(name, value);
			}
		}

		private void SetMaterialProp(string name, global::UnityEngine.Color value)
		{
			if ((bool)m_CustomMaterial)
			{
				m_CustomMaterial.SetColor(name, value);
			}
			else
			{
				global::VLB.MaterialManager.materialPropertyBlock.SetColor(name, value);
			}
		}

		private void SetMaterialProp(string name, global::UnityEngine.Matrix4x4 value)
		{
			if ((bool)m_CustomMaterial)
			{
				m_CustomMaterial.SetMatrix(name, value);
			}
			else
			{
				global::VLB.MaterialManager.materialPropertyBlock.SetMatrix(name, value);
			}
		}

		private void MaterialChangeStart()
		{
			if (m_CustomMaterial == null)
			{
				meshRenderer.GetPropertyBlock(global::VLB.MaterialManager.materialPropertyBlock);
			}
		}

		private void MaterialChangeStop()
		{
			if (m_CustomMaterial == null)
			{
				meshRenderer.SetPropertyBlock(global::VLB.MaterialManager.materialPropertyBlock);
			}
		}

		private void SendMaterialClippingPlaneProp()
		{
			SetMaterialProp("_ClippingPlaneWS", new global::UnityEngine.Vector4(m_ClippingPlaneWS.normal.x, m_ClippingPlaneWS.normal.y, m_ClippingPlaneWS.normal.z, m_ClippingPlaneWS.distance));
		}

		public void UpdateMaterialAndBounds()
		{
			if (ApplyMaterial())
			{
				MaterialChangeStart();
				if (isClippingPlaneEnabled && m_CustomMaterial == null)
				{
					SendMaterialClippingPlaneProp();
				}
				float f = m_Master.coneAngle * (global::System.MathF.PI / 180f) / 2f;
				SetMaterialProp("_ConeSlopeCosSin", new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Cos(f), global::UnityEngine.Mathf.Sin(f)));
				global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Max(m_Master.coneRadiusStart, 0.0001f), global::UnityEngine.Mathf.Max(m_Master.coneRadiusEnd, 0.0001f));
				SetMaterialProp("_ConeRadius", vector);
				float value = global::UnityEngine.Mathf.Sign(m_Master.coneApexOffsetZ) * global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(m_Master.coneApexOffsetZ), 0.0001f);
				SetMaterialProp("_ConeApexOffsetZ", value);
				if (m_Master.colorMode == global::VLB.ColorMode.Flat)
				{
					SetMaterialProp("_ColorFlat", m_Master.color);
				}
				else
				{
					global::VLB.Utils.FloatPackingPrecision floatPackingPrecision = global::VLB.Utils.GetFloatPackingPrecision();
					m_ColorGradientMatrix = m_Master.colorGradient.SampleInMatrix((int)floatPackingPrecision);
				}
				SetMaterialProp("_AlphaInside", m_Master.intensityInside);
				SetMaterialProp("_AlphaOutside", m_Master.intensityOutside);
				SetMaterialProp("_AttenuationLerpLinearQuad", m_Master.attenuationLerpLinearQuad);
				SetMaterialProp("_DistanceFadeStart", m_Master.fallOffStart);
				SetMaterialProp("_DistanceFadeEnd", m_Master.fallOffEnd);
				SetMaterialProp("_DistanceCamClipping", m_Master.cameraClippingDistance);
				SetMaterialProp("_FresnelPow", global::UnityEngine.Mathf.Max(0.001f, m_Master.fresnelPow));
				SetMaterialProp("_GlareBehind", m_Master.glareBehind);
				SetMaterialProp("_GlareFrontal", m_Master.glareFrontal);
				SetMaterialProp("_DrawCap", m_Master.geomCap ? 1 : 0);
				if (isDepthBlendEnabled)
				{
					SetMaterialProp("_DepthBlendDistance", m_Master.depthBlendDistance);
				}
				if (isNoiseEnabled)
				{
					global::VLB.Noise3D.LoadIfNeeded();
					SetMaterialProp("_NoiseLocal", new global::UnityEngine.Vector4(m_Master.noiseVelocityLocal.x, m_Master.noiseVelocityLocal.y, m_Master.noiseVelocityLocal.z, m_Master.noiseScaleLocal));
					SetMaterialProp("_NoiseParam", new global::UnityEngine.Vector3(m_Master.noiseIntensity, m_Master.noiseVelocityUseGlobal ? 1f : 0f, m_Master.noiseScaleUseGlobal ? 1f : 0f));
				}
				MaterialChangeStop();
				ComputeLocalMatrix();
			}
		}

		public void SetClippingPlane(global::UnityEngine.Plane planeWS)
		{
			m_ClippingPlaneWS = planeWS;
			if ((bool)m_CustomMaterial)
			{
				m_CustomMaterial.EnableKeyword("VLB_CLIPPING_PLANE");
				SendMaterialClippingPlaneProp();
			}
			else
			{
				UpdateMaterialAndBounds();
			}
		}

		public void SetClippingPlaneOff()
		{
			m_ClippingPlaneWS = default(global::UnityEngine.Plane);
			if ((bool)m_CustomMaterial)
			{
				m_CustomMaterial.DisableKeyword("VLB_CLIPPING_PLANE");
			}
			else
			{
				UpdateMaterialAndBounds();
			}
		}

		private void OnBeginCameraRendering(global::UnityEngine.Rendering.ScriptableRenderContext context, global::UnityEngine.Camera cam)
		{
			UpdateCameraRelatedProperties(cam);
		}

		private void OnWillRenderObject()
		{
			if (!IsUsingCustomRenderPipeline())
			{
				global::UnityEngine.Camera current = global::UnityEngine.Camera.current;
				if (current != null)
				{
					UpdateCameraRelatedProperties(current);
				}
			}
		}

		private static bool IsEditorCamera(global::UnityEngine.Camera cam)
		{
			return false;
		}

		private void UpdateCameraRelatedProperties(global::UnityEngine.Camera cam)
		{
			if ((bool)cam && (bool)m_Master)
			{
				MaterialChangeStart();
				global::UnityEngine.Vector3 posOS = m_Master.transform.InverseTransformPoint(cam.transform.position);
				global::UnityEngine.Vector3 normalized = base.transform.InverseTransformDirection(cam.transform.forward).normalized;
				float w = (cam.orthographic ? (-1f) : m_Master.GetInsideBeamFactorFromObjectSpacePos(posOS));
				SetMaterialProp("_CameraParams", new global::UnityEngine.Vector4(normalized.x, normalized.y, normalized.z, w));
				if (m_Master.colorMode == global::VLB.ColorMode.Gradient)
				{
					SetMaterialProp("_ColorGradientMatrix", m_ColorGradientMatrix);
				}
				MaterialChangeStop();
				if (m_Master.depthBlendDistance > 0f)
				{
					cam.depthTextureMode |= global::UnityEngine.DepthTextureMode.Depth;
				}
			}
		}
	}
}
