namespace VLB
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.SelectionBase]
	[global::UnityEngine.HelpURL("http://saladgamer.com/vlb-doc/comp-lightbeam/")]
	public class VolumetricLightBeam : global::UnityEngine.MonoBehaviour
	{
		public bool colorFromLight = true;

		public global::VLB.ColorMode colorMode;

		[global::UnityEngine.ColorUsage(true, true)]
		[global::UnityEngine.Serialization.FormerlySerializedAs("colorValue")]
		public global::UnityEngine.Color color = global::VLB.Consts.FlatColor;

		public global::UnityEngine.Gradient colorGradient;

		public bool intensityFromLight = true;

		public bool intensityModeAdvanced;

		[global::UnityEngine.Serialization.FormerlySerializedAs("alphaInside")]
		[global::UnityEngine.Range(0f, 8f)]
		public float intensityInside = 1f;

		[global::UnityEngine.Serialization.FormerlySerializedAs("alphaOutside")]
		[global::UnityEngine.Serialization.FormerlySerializedAs("alpha")]
		[global::UnityEngine.Range(0f, 8f)]
		public float intensityOutside = 1f;

		public global::VLB.BlendingMode blendingMode;

		[global::UnityEngine.Serialization.FormerlySerializedAs("angleFromLight")]
		public bool spotAngleFromLight = true;

		[global::UnityEngine.Range(0.1f, 179.9f)]
		public float spotAngle = 35f;

		[global::UnityEngine.Serialization.FormerlySerializedAs("radiusStart")]
		public float coneRadiusStart = 0.1f;

		public global::VLB.MeshType geomMeshType;

		[global::UnityEngine.Serialization.FormerlySerializedAs("geomSides")]
		public int geomCustomSides = 18;

		public int geomCustomSegments = 5;

		public bool geomCap;

		[global::UnityEngine.Serialization.FormerlySerializedAs("fadeEndFromLight")]
		public bool fallOffEndFromLight = true;

		public global::VLB.AttenuationEquation attenuationEquation = global::VLB.AttenuationEquation.Quadratic;

		[global::UnityEngine.Range(0f, 1f)]
		public float attenuationCustomBlending = 0.5f;

		[global::UnityEngine.Serialization.FormerlySerializedAs("fadeStart")]
		public float fallOffStart;

		[global::UnityEngine.Serialization.FormerlySerializedAs("fadeEnd")]
		public float fallOffEnd = 3f;

		public float depthBlendDistance = 2f;

		public float cameraClippingDistance = 0.5f;

		[global::UnityEngine.Range(0f, 1f)]
		public float glareFrontal = 0.5f;

		[global::UnityEngine.Range(0f, 1f)]
		public float glareBehind = 0.5f;

		[global::System.Obsolete("Use 'glareFrontal' instead")]
		public float boostDistanceInside = 0.5f;

		[global::System.Obsolete("This property has been merged with 'fresnelPow'")]
		public float fresnelPowInside = 6f;

		[global::UnityEngine.Serialization.FormerlySerializedAs("fresnelPowOutside")]
		public float fresnelPow = 8f;

		public bool noiseEnabled;

		[global::UnityEngine.Range(0f, 1f)]
		public float noiseIntensity = 0.5f;

		public bool noiseScaleUseGlobal = true;

		[global::UnityEngine.Range(0.01f, 2f)]
		public float noiseScaleLocal = 0.5f;

		public bool noiseVelocityUseGlobal = true;

		public global::UnityEngine.Vector3 noiseVelocityLocal = global::VLB.Consts.NoiseVelocityDefault;

		public float fadeOutBegin = -150f;

		public float fadeOutEnd = -200f;

		private global::UnityEngine.Plane m_PlaneWS;

		[global::UnityEngine.SerializeField]
		private int pluginVersion = -1;

		[global::UnityEngine.Serialization.FormerlySerializedAs("trackChangesDuringPlaytime")]
		[global::UnityEngine.SerializeField]
		private bool _TrackChangesDuringPlaytime;

		[global::UnityEngine.SerializeField]
		private int _SortingLayerID;

		[global::UnityEngine.SerializeField]
		private int _SortingOrder;

		private global::VLB.BeamGeometry m_BeamGeom;

		private global::UnityEngine.Coroutine m_CoPlaytimeUpdate;

		private global::UnityEngine.Light _CachedLight;

		[global::System.Obsolete("Use 'intensitySimple' or 'intensityInside' instead")]
		public float alphaInside
		{
			get
			{
				return intensityInside;
			}
			set
			{
				intensityInside = value;
			}
		}

		[global::System.Obsolete("Use 'intensitySimple' or 'intensityOutside' instead")]
		public float alphaOutside
		{
			get
			{
				return intensityOutside;
			}
			set
			{
				intensityOutside = value;
			}
		}

		public float intensityGlobal
		{
			get
			{
				return intensityOutside;
			}
			set
			{
				intensityInside = value;
				intensityOutside = value;
			}
		}

		public float coneAngle => global::UnityEngine.Mathf.Atan2(coneRadiusEnd - coneRadiusStart, fallOffEnd) * 57.29578f * 2f;

		public float coneRadiusEnd => fallOffEnd * global::UnityEngine.Mathf.Tan(spotAngle * (global::System.MathF.PI / 180f) * 0.5f);

		public float coneVolume
		{
			get
			{
				float num = coneRadiusStart;
				float num2 = coneRadiusEnd;
				return global::System.MathF.PI / 3f * (num * num + num * num2 + num2 * num2) * fallOffEnd;
			}
		}

		public float coneApexOffsetZ
		{
			get
			{
				float num = coneRadiusStart / coneRadiusEnd;
				if (num != 1f)
				{
					return fallOffEnd * num / (1f - num);
				}
				return float.MaxValue;
			}
		}

		public int geomSides
		{
			get
			{
				if (geomMeshType != global::VLB.MeshType.Custom)
				{
					return global::VLB.Config.Instance.sharedMeshSides;
				}
				return geomCustomSides;
			}
			set
			{
				geomCustomSides = value;
				global::UnityEngine.Debug.LogWarning("The setter VLB.VolumetricLightBeam.geomSides is OBSOLETE and has been renamed to geomCustomSides.");
			}
		}

		public int geomSegments
		{
			get
			{
				if (geomMeshType != global::VLB.MeshType.Custom)
				{
					return global::VLB.Config.Instance.sharedMeshSegments;
				}
				return geomCustomSegments;
			}
			set
			{
				geomCustomSegments = value;
				global::UnityEngine.Debug.LogWarning("The setter VLB.VolumetricLightBeam.geomSegments is OBSOLETE and has been renamed to geomCustomSegments.");
			}
		}

		[global::System.Obsolete("Use 'fallOffEndFromLight' instead")]
		public bool fadeEndFromLight
		{
			get
			{
				return fallOffEndFromLight;
			}
			set
			{
				fallOffEndFromLight = value;
			}
		}

		public float attenuationLerpLinearQuad
		{
			get
			{
				if (attenuationEquation == global::VLB.AttenuationEquation.Linear)
				{
					return 0f;
				}
				if (attenuationEquation == global::VLB.AttenuationEquation.Quadratic)
				{
					return 1f;
				}
				return attenuationCustomBlending;
			}
		}

		[global::System.Obsolete("Use 'fallOffStart' instead")]
		public float fadeStart
		{
			get
			{
				return fallOffStart;
			}
			set
			{
				fallOffStart = value;
			}
		}

		[global::System.Obsolete("Use 'fallOffEnd' instead")]
		public float fadeEnd
		{
			get
			{
				return fallOffEnd;
			}
			set
			{
				fallOffEnd = value;
			}
		}

		public bool isFadeOutEnabled => fadeOutEnd >= 0f;

		public int sortingLayerID
		{
			get
			{
				return _SortingLayerID;
			}
			set
			{
				_SortingLayerID = value;
				if ((bool)m_BeamGeom)
				{
					m_BeamGeom.sortingLayerID = value;
				}
			}
		}

		public string sortingLayerName
		{
			get
			{
				return global::UnityEngine.SortingLayer.IDToName(sortingLayerID);
			}
			set
			{
				sortingLayerID = global::UnityEngine.SortingLayer.NameToID(value);
			}
		}

		public int sortingOrder
		{
			get
			{
				return _SortingOrder;
			}
			set
			{
				_SortingOrder = value;
				if ((bool)m_BeamGeom)
				{
					m_BeamGeom.sortingOrder = value;
				}
			}
		}

		public bool trackChangesDuringPlaytime
		{
			get
			{
				return _TrackChangesDuringPlaytime;
			}
			set
			{
				_TrackChangesDuringPlaytime = value;
				StartPlaytimeUpdateIfNeeded();
			}
		}

		public bool isCurrentlyTrackingChanges => m_CoPlaytimeUpdate != null;

		public bool hasGeometry => m_BeamGeom != null;

		public global::UnityEngine.Bounds bounds
		{
			get
			{
				if (!(m_BeamGeom != null))
				{
					return new global::UnityEngine.Bounds(global::UnityEngine.Vector3.zero, global::UnityEngine.Vector3.zero);
				}
				return m_BeamGeom.meshRenderer.bounds;
			}
		}

		public int blendingModeAsInt => global::UnityEngine.Mathf.Clamp((int)blendingMode, 0, global::System.Enum.GetValues(typeof(global::VLB.BlendingMode)).Length);

		public uint _INTERNAL_InstancedMaterialGroupID { get; protected set; }

		public string meshStats
		{
			get
			{
				global::UnityEngine.Mesh mesh = (m_BeamGeom ? m_BeamGeom.coneMesh : null);
				if ((bool)mesh)
				{
					return $"Cone angle: {coneAngle:0.0} degrees\nMesh: {mesh.vertexCount} vertices, {mesh.triangles.Length / 3} triangles";
				}
				return "no mesh available";
			}
		}

		public int meshVerticesCount
		{
			get
			{
				if (!m_BeamGeom || !m_BeamGeom.coneMesh)
				{
					return 0;
				}
				return m_BeamGeom.coneMesh.vertexCount;
			}
		}

		public int meshTrianglesCount
		{
			get
			{
				if (!m_BeamGeom || !m_BeamGeom.coneMesh)
				{
					return 0;
				}
				return m_BeamGeom.coneMesh.triangles.Length / 3;
			}
		}

		private global::UnityEngine.Light lightSpotAttached
		{
			get
			{
				if (_CachedLight == null)
				{
					_CachedLight = GetComponent<global::UnityEngine.Light>();
				}
				if ((bool)_CachedLight && _CachedLight.type == global::UnityEngine.LightType.Spot)
				{
					return _CachedLight;
				}
				return null;
			}
		}

		public void SetClippingPlane(global::UnityEngine.Plane planeWS)
		{
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.SetClippingPlane(planeWS);
			}
			m_PlaneWS = planeWS;
		}

		public void SetClippingPlaneOff()
		{
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.SetClippingPlaneOff();
			}
			m_PlaneWS = default(global::UnityEngine.Plane);
		}

		public bool IsColliderHiddenByDynamicOccluder(global::UnityEngine.Collider collider)
		{
			if (!m_PlaneWS.IsValid())
			{
				return false;
			}
			return !global::UnityEngine.GeometryUtility.TestPlanesAABB(new global::UnityEngine.Plane[1] { m_PlaneWS }, collider.bounds);
		}

		public float GetInsideBeamFactor(global::UnityEngine.Vector3 posWS)
		{
			return GetInsideBeamFactorFromObjectSpacePos(base.transform.InverseTransformPoint(posWS));
		}

		public float GetInsideBeamFactorFromObjectSpacePos(global::UnityEngine.Vector3 posOS)
		{
			if (posOS.z < 0f)
			{
				return -1f;
			}
			global::UnityEngine.Vector2 normalized = new global::UnityEngine.Vector2(posOS.xy().magnitude, posOS.z + coneApexOffsetZ).normalized;
			return global::UnityEngine.Mathf.Clamp((global::UnityEngine.Mathf.Abs(global::UnityEngine.Mathf.Sin(coneAngle * (global::System.MathF.PI / 180f) / 2f)) - global::UnityEngine.Mathf.Abs(normalized.x)) / 0.1f, -1f, 1f);
		}

		[global::System.Obsolete("Use 'GenerateGeometry()' instead")]
		public void Generate()
		{
			GenerateGeometry();
		}

		public virtual void GenerateGeometry()
		{
			HandleBackwardCompatibility(pluginVersion, 1700);
			pluginVersion = 1700;
			ValidateProperties();
			if (m_BeamGeom == null)
			{
				m_BeamGeom = global::VLB.Utils.NewWithComponent<global::VLB.BeamGeometry>("Beam Geometry");
				m_BeamGeom.Initialize(this);
			}
			m_BeamGeom.RegenerateMesh();
			m_BeamGeom.visible = base.enabled;
		}

		public virtual void UpdateAfterManualPropertyChange()
		{
			ValidateProperties();
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.UpdateMaterialAndBounds();
			}
		}

		private void Start()
		{
			GenerateGeometry();
		}

		private void OnEnable()
		{
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.visible = true;
			}
			StartPlaytimeUpdateIfNeeded();
		}

		private void OnDisable()
		{
			if ((bool)m_BeamGeom)
			{
				m_BeamGeom.visible = false;
			}
			m_CoPlaytimeUpdate = null;
		}

		private void StartPlaytimeUpdateIfNeeded()
		{
			if (global::UnityEngine.Application.isPlaying && trackChangesDuringPlaytime && m_CoPlaytimeUpdate == null)
			{
				m_CoPlaytimeUpdate = StartCoroutine(CoPlaytimeUpdate());
			}
		}

		private global::System.Collections.IEnumerator CoPlaytimeUpdate()
		{
			while (trackChangesDuringPlaytime && base.enabled)
			{
				UpdateAfterManualPropertyChange();
				yield return null;
			}
			m_CoPlaytimeUpdate = null;
		}

		private void OnDestroy()
		{
			DestroyBeam();
		}

		private void DestroyBeam()
		{
			if ((bool)m_BeamGeom)
			{
				global::UnityEngine.Object.DestroyImmediate(m_BeamGeom.gameObject);
			}
			m_BeamGeom = null;
		}

		private void AssignPropertiesFromSpotLight(global::UnityEngine.Light lightSpot)
		{
			if ((bool)lightSpot && lightSpot.type == global::UnityEngine.LightType.Spot)
			{
				if (intensityFromLight)
				{
					intensityModeAdvanced = false;
					intensityGlobal = lightSpot.intensity;
				}
				if (fallOffEndFromLight)
				{
					fallOffEnd = lightSpot.range;
				}
				if (spotAngleFromLight)
				{
					spotAngle = lightSpot.spotAngle;
				}
				if (colorFromLight)
				{
					colorMode = global::VLB.ColorMode.Flat;
					color = lightSpot.color;
				}
			}
		}

		private void ClampProperties()
		{
			intensityInside = global::UnityEngine.Mathf.Clamp(intensityInside, 0f, 8f);
			intensityOutside = global::UnityEngine.Mathf.Clamp(intensityOutside, 0f, 8f);
			attenuationCustomBlending = global::UnityEngine.Mathf.Clamp01(attenuationCustomBlending);
			fallOffEnd = global::UnityEngine.Mathf.Max(0.01f, fallOffEnd);
			fallOffStart = global::UnityEngine.Mathf.Clamp(fallOffStart, 0f, fallOffEnd - 0.01f);
			spotAngle = global::UnityEngine.Mathf.Clamp(spotAngle, 0.1f, 179.9f);
			coneRadiusStart = global::UnityEngine.Mathf.Max(coneRadiusStart, 0f);
			depthBlendDistance = global::UnityEngine.Mathf.Max(depthBlendDistance, 0f);
			cameraClippingDistance = global::UnityEngine.Mathf.Max(cameraClippingDistance, 0f);
			geomCustomSides = global::UnityEngine.Mathf.Clamp(geomCustomSides, 3, 256);
			geomCustomSegments = global::UnityEngine.Mathf.Clamp(geomCustomSegments, 0, 64);
			fresnelPow = global::UnityEngine.Mathf.Max(0f, fresnelPow);
			glareBehind = global::UnityEngine.Mathf.Clamp01(glareBehind);
			glareFrontal = global::UnityEngine.Mathf.Clamp01(glareFrontal);
			noiseIntensity = global::UnityEngine.Mathf.Clamp(noiseIntensity, 0f, 1f);
		}

		private void ValidateProperties()
		{
			AssignPropertiesFromSpotLight(lightSpotAttached);
			ClampProperties();
		}

		private void HandleBackwardCompatibility(int serializedVersion, int newVersion)
		{
			if (serializedVersion != -1 && serializedVersion != newVersion)
			{
				if (serializedVersion < 1301)
				{
					attenuationEquation = global::VLB.AttenuationEquation.Linear;
				}
				if (serializedVersion < 1501)
				{
					geomMeshType = global::VLB.MeshType.Custom;
					geomCustomSegments = 5;
				}
				if (serializedVersion < 1610)
				{
					intensityFromLight = false;
					intensityModeAdvanced = !global::UnityEngine.Mathf.Approximately(intensityInside, intensityOutside);
				}
				global::VLB.Utils.MarkCurrentSceneDirty();
			}
		}
	}
}
