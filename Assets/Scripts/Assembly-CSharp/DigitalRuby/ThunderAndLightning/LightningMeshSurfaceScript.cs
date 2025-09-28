namespace DigitalRuby.ThunderAndLightning
{
	public class LightningMeshSurfaceScript : global::DigitalRuby.ThunderAndLightning.LightningBoltPrefabScriptBase
	{
		[global::UnityEngine.Header("Lightning Mesh Properties")]
		[global::UnityEngine.Tooltip("The mesh filter. You must assign a mesh filter in order to create lightning on the mesh.")]
		public global::UnityEngine.MeshFilter MeshFilter;

		[global::UnityEngine.Tooltip("The mesh collider. This is used to get random points on the mesh.")]
		public global::UnityEngine.Collider MeshCollider;

		[global::DigitalRuby.ThunderAndLightning.SingleLine("Random range that the point will offset from the mesh, using the normal of the chosen point to offset")]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats MeshOffsetRange = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 0.5f,
			Maximum = 1f
		};

		[global::UnityEngine.Header("Lightning Path Properties")]
		[global::DigitalRuby.ThunderAndLightning.SingleLine("Range for points in the lightning path")]
		public global::DigitalRuby.ThunderAndLightning.RangeOfIntegers PathLengthCount = new global::DigitalRuby.ThunderAndLightning.RangeOfIntegers
		{
			Minimum = 3,
			Maximum = 6
		};

		[global::DigitalRuby.ThunderAndLightning.SingleLine("Range for minimum distance between points in the lightning path")]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats MinimumPathDistanceRange = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 0.5f,
			Maximum = 1f
		};

		[global::UnityEngine.Tooltip("The maximum distance between mesh points. When walking the mesh, if a point is greater than this, the path direction is reversed. This tries to avoid paths crossing between mesh points that are not actually physically touching.")]
		public float MaximumPathDistance = 2f;

		private float maximumPathDistanceSquared;

		[global::UnityEngine.Tooltip("Whether to use spline interpolation between the path points. Paths must be at least 4 points long to be splined.")]
		public bool Spline;

		[global::UnityEngine.Tooltip("For spline. the distance hint for each spline segment. Set to <= 0 to use the generations to determine how many spline segments to use. If > 0, it will be divided by Generations before being applied. This value is a guideline and is approximate, and not uniform on the spline.")]
		public float DistancePerSegmentHint;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector3> sourcePoints = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();

		private global::UnityEngine.Mesh previousMesh;

		private global::DigitalRuby.ThunderAndLightning.MeshHelper meshHelper;

		private void CheckMesh()
		{
			if (MeshFilter == null || MeshFilter.sharedMesh == null)
			{
				meshHelper = null;
			}
			else if (MeshFilter.sharedMesh != previousMesh)
			{
				previousMesh = MeshFilter.sharedMesh;
				meshHelper = new global::DigitalRuby.ThunderAndLightning.MeshHelper(previousMesh);
			}
		}

		protected override global::DigitalRuby.ThunderAndLightning.LightningBoltParameters OnCreateParameters()
		{
			global::DigitalRuby.ThunderAndLightning.LightningBoltParameters lightningBoltParameters = base.OnCreateParameters();
			lightningBoltParameters.Generator = global::DigitalRuby.ThunderAndLightning.LightningGeneratorPath.PathGeneratorInstance;
			return lightningBoltParameters;
		}

		protected virtual void PopulateSourcePoints(global::System.Collections.Generic.List<global::UnityEngine.Vector3> points)
		{
			if (meshHelper != null)
			{
				CreateRandomLightningPath(sourcePoints);
			}
		}

		public void CreateRandomLightningPath(global::System.Collections.Generic.List<global::UnityEngine.Vector3> points)
		{
			if (meshHelper == null)
			{
				return;
			}
			global::UnityEngine.RaycastHit hit = default(global::UnityEngine.RaycastHit);
			maximumPathDistanceSquared = MaximumPathDistance * MaximumPathDistance;
			meshHelper.GenerateRandomPoint(ref hit, out var triangleIndex);
			hit.distance = global::UnityEngine.Random.Range(MeshOffsetRange.Minimum, MeshOffsetRange.Maximum);
			global::UnityEngine.Vector3 vector = hit.point + hit.normal * hit.distance;
			float num = global::UnityEngine.Random.Range(MinimumPathDistanceRange.Minimum, MinimumPathDistanceRange.Maximum);
			num *= num;
			sourcePoints.Add(MeshFilter.transform.TransformPoint(vector));
			int num2 = ((global::UnityEngine.Random.Range(0, 1) == 1) ? 3 : (-3));
			int num3 = global::UnityEngine.Random.Range(PathLengthCount.Minimum, PathLengthCount.Maximum);
			while (num3 != 0)
			{
				triangleIndex += num2;
				if (triangleIndex >= 0 && triangleIndex < meshHelper.Triangles.Length)
				{
					meshHelper.GetRaycastFromTriangleIndex(triangleIndex, ref hit);
					hit.distance = global::UnityEngine.Random.Range(MeshOffsetRange.Minimum, MeshOffsetRange.Maximum);
					global::UnityEngine.Vector3 vector2 = hit.point + hit.normal * hit.distance;
					float sqrMagnitude = (vector2 - vector).sqrMagnitude;
					if (sqrMagnitude > maximumPathDistanceSquared)
					{
						break;
					}
					if (sqrMagnitude >= num)
					{
						vector = vector2;
						sourcePoints.Add(MeshFilter.transform.TransformPoint(vector2));
						num3--;
						num = global::UnityEngine.Random.Range(MinimumPathDistanceRange.Minimum, MinimumPathDistanceRange.Maximum);
						num *= num;
					}
				}
				else
				{
					num2 = -num2;
					triangleIndex += num2;
					num3--;
				}
			}
		}

		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			CheckMesh();
			base.Update();
		}

		public override void CreateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters parameters)
		{
			if (meshHelper == null)
			{
				return;
			}
			int generations = (parameters.Generations = global::UnityEngine.Mathf.Clamp(Generations, 1, 5));
			Generations = generations;
			sourcePoints.Clear();
			PopulateSourcePoints(sourcePoints);
			if (sourcePoints.Count > 1)
			{
				parameters.Points.Clear();
				if (Spline && sourcePoints.Count > 3)
				{
					global::DigitalRuby.ThunderAndLightning.LightningSplineScript.PopulateSpline(parameters.Points, sourcePoints, Generations, DistancePerSegmentHint, Camera);
					parameters.SmoothingFactor = (parameters.Points.Count - 1) / sourcePoints.Count;
				}
				else
				{
					parameters.Points.AddRange(sourcePoints);
					parameters.SmoothingFactor = 1;
				}
				base.CreateLightningBolt(parameters);
			}
		}
	}
}
