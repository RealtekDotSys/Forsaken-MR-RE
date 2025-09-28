namespace VLB
{
	public static class MaterialManager
	{
		public enum BlendingMode
		{
			Additive = 0,
			SoftAdditive = 1,
			TraditionalTransparency = 2,
			Count = 3
		}

		public enum Noise3D
		{
			Off = 0,
			On = 1,
			Count = 2
		}

		public enum DepthBlend
		{
			Off = 0,
			On = 1,
			Count = 2
		}

		public enum ColorGradient
		{
			Off = 0,
			MatrixLow = 1,
			MatrixHigh = 2,
			Count = 3
		}

		public enum ClippingPlane
		{
			Off = 0,
			On = 1,
			Count = 2
		}

		public class StaticProperties
		{
			public global::VLB.MaterialManager.BlendingMode blendingMode;

			public global::VLB.MaterialManager.Noise3D noise3D;

			public global::VLB.MaterialManager.DepthBlend depthBlend;

			public global::VLB.MaterialManager.ColorGradient colorGradient;

			public global::VLB.MaterialManager.ClippingPlane clippingPlane;

			public int materialID => (int)((int)((int)((int)((int)blendingMode * 2 + noise3D) * 2 + depthBlend) * 3 + colorGradient) * 2 + clippingPlane);

			public void ApplyToMaterial(global::UnityEngine.Material mat)
			{
				mat.SetKeywordEnabled("VLB_ALPHA_AS_BLACK", BlendingMode_AlphaAsBlack[(int)blendingMode]);
				mat.SetInt("_BlendSrcFactor", (int)BlendingMode_SrcFactor[(int)blendingMode]);
				mat.SetInt("_BlendDstFactor", (int)BlendingMode_DstFactor[(int)blendingMode]);
				mat.SetKeywordEnabled("VLB_COLOR_GRADIENT_MATRIX_LOW", colorGradient == global::VLB.MaterialManager.ColorGradient.MatrixLow);
				mat.SetKeywordEnabled("VLB_COLOR_GRADIENT_MATRIX_HIGH", colorGradient == global::VLB.MaterialManager.ColorGradient.MatrixHigh);
				mat.SetKeywordEnabled("VLB_DEPTH_BLEND", depthBlend == global::VLB.MaterialManager.DepthBlend.On);
				mat.SetKeywordEnabled("VLB_NOISE_3D", noise3D == global::VLB.MaterialManager.Noise3D.On);
				mat.SetKeywordEnabled("VLB_CLIPPING_PLANE", clippingPlane == global::VLB.MaterialManager.ClippingPlane.On);
			}
		}

		private class MaterialsGroup
		{
			public global::UnityEngine.Material[] materials = new global::UnityEngine.Material[kStaticPropertiesCount];
		}

		public static global::UnityEngine.MaterialPropertyBlock materialPropertyBlock = new global::UnityEngine.MaterialPropertyBlock();

		private static readonly global::UnityEngine.Rendering.BlendMode[] BlendingMode_SrcFactor = new global::UnityEngine.Rendering.BlendMode[3]
		{
			global::UnityEngine.Rendering.BlendMode.One,
			global::UnityEngine.Rendering.BlendMode.OneMinusDstColor,
			global::UnityEngine.Rendering.BlendMode.SrcAlpha
		};

		private static readonly global::UnityEngine.Rendering.BlendMode[] BlendingMode_DstFactor = new global::UnityEngine.Rendering.BlendMode[3]
		{
			global::UnityEngine.Rendering.BlendMode.One,
			global::UnityEngine.Rendering.BlendMode.One,
			global::UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha
		};

		private static readonly bool[] BlendingMode_AlphaAsBlack = new bool[3] { true, true, false };

		private static int kStaticPropertiesCount = 72;

		private static global::System.Collections.Hashtable ms_MaterialsGroup = new global::System.Collections.Hashtable(1);

		public static global::UnityEngine.Material NewMaterial(bool gpuInstanced)
		{
			global::UnityEngine.Shader beamShader = global::VLB.Config.Instance.beamShader;
			if (!beamShader)
			{
				global::UnityEngine.Debug.LogError("Invalid Beam Shader set in VLB Config");
				return null;
			}
			global::UnityEngine.Material obj = new global::UnityEngine.Material(beamShader)
			{
				hideFlags = global::VLB.Consts.ProceduralObjectsHideFlags,
				renderQueue = global::VLB.Config.Instance.geometryRenderQueue
			};
			global::VLB.GpuInstancing.SetMaterialProperties(obj, gpuInstanced);
			return obj;
		}

		public static global::UnityEngine.Material GetInstancedMaterial(uint groupID, global::VLB.MaterialManager.StaticProperties staticProps)
		{
			global::VLB.MaterialManager.MaterialsGroup materialsGroup = (global::VLB.MaterialManager.MaterialsGroup)ms_MaterialsGroup[groupID];
			if (materialsGroup == null)
			{
				materialsGroup = new global::VLB.MaterialManager.MaterialsGroup();
				ms_MaterialsGroup[groupID] = materialsGroup;
			}
			int materialID = staticProps.materialID;
			global::UnityEngine.Material material = materialsGroup.materials[materialID];
			if (material == null)
			{
				material = NewMaterial(gpuInstanced: true);
				if ((bool)material)
				{
					materialsGroup.materials[materialID] = material;
					staticProps.ApplyToMaterial(material);
				}
			}
			return material;
		}
	}
}
