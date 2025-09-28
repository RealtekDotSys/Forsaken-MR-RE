namespace VLB
{
	public static class Consts
	{
		private const string HelpUrlBase = "http://saladgamer.com/vlb-doc/";

		public const string HelpUrlBeam = "http://saladgamer.com/vlb-doc/comp-lightbeam/";

		public const string HelpUrlDustParticles = "http://saladgamer.com/vlb-doc/comp-dustparticles/";

		public const string HelpUrlDynamicOcclusion = "http://saladgamer.com/vlb-doc/comp-dynocclusion/";

		public const string HelpUrlTriggerZone = "http://saladgamer.com/vlb-doc/comp-triggerzone/";

		public const string HelpUrlConfig = "http://saladgamer.com/vlb-doc/config/";

		public static readonly bool ProceduralObjectsVisibleInEditor = true;

		public static readonly global::UnityEngine.Color FlatColor = global::UnityEngine.Color.white;

		public const global::VLB.ColorMode ColorModeDefault = global::VLB.ColorMode.Flat;

		public const float IntensityDefault = 1f;

		public const float IntensityMin = 0f;

		public const float IntensityMax = 8f;

		public const float SpotAngleDefault = 35f;

		public const float SpotAngleMin = 0.1f;

		public const float SpotAngleMax = 179.9f;

		public const float ConeRadiusStart = 0.1f;

		public const global::VLB.MeshType GeomMeshType = global::VLB.MeshType.Shared;

		public const int GeomSidesDefault = 18;

		public const int GeomSidesMin = 3;

		public const int GeomSidesMax = 256;

		public const int GeomSegmentsDefault = 5;

		public const int GeomSegmentsMin = 0;

		public const int GeomSegmentsMax = 64;

		public const bool GeomCap = false;

		public const global::VLB.AttenuationEquation AttenuationEquationDefault = global::VLB.AttenuationEquation.Quadratic;

		public const float AttenuationCustomBlending = 0.5f;

		public const float FallOffStart = 0f;

		public const float FallOffEnd = 3f;

		public const float FallOffDistancesMinThreshold = 0.01f;

		public const float DepthBlendDistance = 2f;

		public const float CameraClippingDistance = 0.5f;

		public const float FresnelPowMaxValue = 10f;

		public const float FresnelPow = 8f;

		public const float GlareFrontal = 0.5f;

		public const float GlareBehind = 0.5f;

		public const float NoiseIntensityMin = 0f;

		public const float NoiseIntensityMax = 1f;

		public const float NoiseIntensityDefault = 0.5f;

		public const float NoiseScaleMin = 0.01f;

		public const float NoiseScaleMax = 2f;

		public const float NoiseScaleDefault = 0.5f;

		public static readonly global::UnityEngine.Vector3 NoiseVelocityDefault = new global::UnityEngine.Vector3(0.07f, 0.18f, 0.05f);

		public const global::VLB.BlendingMode BlendingModeDefault = global::VLB.BlendingMode.Additive;

		public const global::VLB.OccluderDimensions DynOcclusionDimensionsDefault = global::VLB.OccluderDimensions.Occluders3D;

		public static readonly global::UnityEngine.LayerMask DynOcclusionLayerMaskDefault = -1;

		public const bool DynOcclusionConsiderTriggersDefault = false;

		public const float DynOcclusionMinOccluderAreaDefault = 0f;

		public const int DynOcclusionWaitFrameCountDefault = 3;

		public const float DynOcclusionMinSurfaceRatioDefault = 0.5f;

		public const float DynOcclusionMinSurfaceRatioMin = 50f;

		public const float DynOcclusionMinSurfaceRatioMax = 100f;

		public const float DynOcclusionMaxSurfaceDotDefault = 0.25f;

		public const float DynOcclusionMaxSurfaceAngleMin = 45f;

		public const float DynOcclusionMaxSurfaceAngleMax = 90f;

		public const global::VLB.PlaneAlignment DynOcclusionPlaneAlignmentDefault = global::VLB.PlaneAlignment.Surface;

		public const float DynOcclusionPlaneOffsetDefault = 0.1f;

		public const bool ConfigGeometryOverrideLayerDefault = true;

		public const int ConfigGeometryLayerIDDefault = 1;

		public const string ConfigGeometryTagDefault = "Untagged";

		public const string ConfigFadeOutCameraTagDefault = "MainCamera";

		public const global::VLB.RenderQueue ConfigGeometryRenderQueueDefault = global::VLB.RenderQueue.Transparent;

		public const global::VLB.RenderPipeline ConfigGeometryRenderPipelineDefault = global::VLB.RenderPipeline.BuiltIn;

		public const global::VLB.RenderingMode ConfigGeometryRenderingModeDefault = global::VLB.RenderingMode.MultiPass;

		public const int ConfigNoise3DSizeDefault = 64;

		public const int ConfigSharedMeshSides = 24;

		public const int ConfigSharedMeshSegments = 5;

		public static global::UnityEngine.HideFlags ProceduralObjectsHideFlags
		{
			get
			{
				if (!ProceduralObjectsVisibleInEditor)
				{
					return global::UnityEngine.HideFlags.HideAndDontSave;
				}
				return global::UnityEngine.HideFlags.DontSave | global::UnityEngine.HideFlags.NotEditable;
			}
		}
	}
}
