Shader "Illumix/Distortion" {
	Properties {
		[Header(Textures)] _Alpha ("Alpha ", 2D) = "white" {}
		[Normal] _Normal ("Normal", 2D) = "bump" {}
		_NormalIntensity ("Normal Intensity", Range(0, 1)) = 0
		_Grunge02 ("Grunge02 ", 2D) = "white" {}
		[Header(Floats)] _Speed ("Speed", Range(-10, 10)) = 0
		_DistortionAmount ("Distortion Amount", Range(0, 1)) = 0.0730508
		[IntRange] _DistortionTiling ("Distortion Tiling", Range(1, 20)) = 4
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
	//CustomEditor "ASEMaterialInspector"
}