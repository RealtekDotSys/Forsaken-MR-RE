Shader "Forsaken/Animatronic_Carnie" {
	Properties {
		_Grunge01 ("Grunge01", 2D) = "white" {}
		_Base_Albedo ("Base_Albedo", 2D) = "white" {}
		_Base_Normal ("Base_Normal", 2D) = "bump" {}
		_Base_Emission ("Base_Emission", 2D) = "white" {}
		_EmissiveStrength ("Emissive Strength", Float) = 1
		[Header(Paints Textures)] _PaintsAlbedoTex ("Paints Albedo Tex", 2D) = "white" {}
		[Normal] _PaintsNormalTex ("Paints Normal Tex", 2D) = "bump" {}
		_PaintsEmissiveTex ("Paints Emissive Tex", 2D) = "black" {}
		[Header(Paints Grunge)] _PaintsRoughness ("Paints Roughness", Range(0, 10)) = 2.5
		_PaintsDirtRoughness ("Paints Dirt Roughness", Range(0, 1)) = 0.3
		_PaintsDirtAOBalance ("Paints Dirt AO Balance", Range(-1, 1)) = 0
		_PaintsDirtAOContrast ("Paints Dirt AO Contrast", Range(0, 1)) = 0
		_PaintsDirtColor ("Paints Dirt Color", Vector) = (0.5921569,0.5921569,0.5921569,1)
		_PaintsDirtBalance ("Paints Dirt Balance", Range(-1, 1)) = 0.4
		_PaintsDirtContrast ("Paints Dirt Contrast", Range(0, 1)) = 0.2
		_PaintsDirtTiling ("Paints Dirt Tiling", Range(0, 20)) = 4
		_PaintsDirtOffset ("Paints Dirt Offset", Range(0, 1)) = 0
		_PaintsDirtScratchesBalance ("Paints Dirt Scratches Balance", Range(-1, 1)) = 0.4
		_PaintsDirtScratchesContrast ("Paints Dirt Scratches Contrast", Range(0, 1)) = 0.6
		_PaintsScratchesTiling ("Paints Scratches Tiling", Range(0, 20)) = 6
		_PaintsScratchesOffset ("Paints Scratches Offset", Range(0, 1)) = 0
		_PaintsAOMinMax ("Paints AO Min Max", Range(1, 10)) = 2.5
		[Header(Paints Grunge)] _BasePaintsRoughness ("Base Paints Roughness", Range(0, 10)) = 2.5
		[Header(Textures)] _MRAC ("MRAC", 2D) = "white" {}
		_Base_MRAC ("Base_MRAC", 2D) = "white" {}
		[Header(Wear Grunge)] _WearCurvatureBalance ("Wear Curvature Balance", Range(-1, 1)) = 0.2
		_WearCurvatureContrast ("Wear Curvature Contrast", Range(0, 1)) = 0.8
		_WearCurveGrimeBalance ("Wear Curve Grime Balance", Range(-1, 1)) = 0.8
		_WearCurveGrimeContrast ("Wear Curve Grime Contrast", Range(0, 1)) = 1
		_WearCurveGrimeTiling ("Wear Curve Grime Tiling", Range(0, 20)) = 6
		_WearCurveGrimeOffset ("Wear Curve Grime Offset", Range(0, 1)) = 0
		_WearCurveGrimeRotator ("Wear Curve Grime Rotator", Range(0, 360)) = 0
		_WearGrungeBalance ("Wear Grunge Balance", Range(-1, 1)) = 0.65
		_WearGrungeContrast ("Wear Grunge Contrast", Range(0, 1)) = 0.9
		_WearGrungeTiling ("Wear Grunge Tiling", Range(0, 20)) = 6
		_WearGrungeOffset ("Wear Grunge Offset", Range(0, 1)) = 0
		_WearGrungeRotator ("Wear Grunge Rotator", Range(0, 360)) = 0
		_WearScratchesBalance ("Wear Scratches Balance", Range(-1, 1)) = 0.71
		_WearScratchesContrast ("Wear Scratches Contrast", Range(0, 1)) = 0.75
		_WearScratchesTiling ("Wear Scratches Tiling", Range(0, 20)) = 8
		_WearScratchesOffset ("Wear Scratches Offset", Range(0, 1)) = 0
		_WearScratchesRotator ("Wear Scratches Rotator", Range(0, 360)) = 0
		[Header(Cloak)] _PlanePosition ("PlanePosition", Vector) = (0,0,0,0)
		_PlaneNormal ("PlaneNormal", Vector) = (0,1,0,0)
		[HDR] _GlowColor ("Glow Color", Vector) = (0.9215686,0.1411765,0.01568628,0)
		[Header(Vertex Displacement)] [Header()] _VertScale ("Vert Scale", Range(0, 50)) = 2
		_VertDisplace ("Vert Displace", Range(-0.01, 0.01)) = 0.004
		_VertDisplacementMask ("Vert Displacement Mask", 2D) = "white" {}
		_Cutoff ("Mask Clip Value", Float) = 0.5
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}