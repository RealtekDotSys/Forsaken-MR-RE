namespace DigitalRuby.ThunderAndLightning
{
	public class LightningSplineScript : global::DigitalRuby.ThunderAndLightning.LightningBoltPathScriptBase
	{
		public const int MaxSplineGenerations = 5;

		[global::UnityEngine.Header("Lightning Spline Properties")]
		[global::UnityEngine.Tooltip("The distance hint for each spline segment. Set to <= 0 to use the generations to determine how many spline segments to use. If > 0, it will be divided by Generations before being applied. This value is a guideline and is approximate, and not uniform on the spline.")]
		public float DistancePerSegmentHint;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector3> prevSourcePoints = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>(new global::UnityEngine.Vector3[1] { global::UnityEngine.Vector3.zero });

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector3> sourcePoints = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();

		private global::System.Collections.Generic.List<global::UnityEngine.Vector3> savedSplinePoints = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();

		private int previousGenerations = -1;

		private float previousDistancePerSegment = -1f;

		private bool SourceChanged()
		{
			if (sourcePoints.Count != prevSourcePoints.Count)
			{
				return true;
			}
			for (int i = 0; i < sourcePoints.Count; i++)
			{
				if (sourcePoints[i] != prevSourcePoints[i])
				{
					return true;
				}
			}
			return false;
		}

		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			base.Update();
		}

		public override void CreateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters parameters)
		{
			if (LightningPath == null)
			{
				return;
			}
			sourcePoints.Clear();
			try
			{
				foreach (global::UnityEngine.GameObject item in LightningPath)
				{
					if (item != null)
					{
						sourcePoints.Add(item.transform.position);
					}
				}
			}
			catch (global::System.NullReferenceException)
			{
				return;
			}
			if (sourcePoints.Count < 4)
			{
				global::UnityEngine.Debug.LogError("To create spline lightning, you need a lightning path with at least " + 4 + " points.");
				return;
			}
			int generations = (parameters.Generations = global::UnityEngine.Mathf.Clamp(Generations, 1, 5));
			Generations = generations;
			parameters.Points.Clear();
			if (previousGenerations != Generations || previousDistancePerSegment != DistancePerSegmentHint || SourceChanged())
			{
				previousGenerations = Generations;
				previousDistancePerSegment = DistancePerSegmentHint;
				PopulateSpline(parameters.Points, sourcePoints, Generations, DistancePerSegmentHint, Camera);
				prevSourcePoints.Clear();
				prevSourcePoints.AddRange(sourcePoints);
				savedSplinePoints.Clear();
				savedSplinePoints.AddRange(parameters.Points);
			}
			else
			{
				parameters.Points.AddRange(savedSplinePoints);
			}
			parameters.SmoothingFactor = (parameters.Points.Count - 1) / sourcePoints.Count;
			base.CreateLightningBolt(parameters);
		}

		protected override global::DigitalRuby.ThunderAndLightning.LightningBoltParameters OnCreateParameters()
		{
			global::DigitalRuby.ThunderAndLightning.LightningBoltParameters orCreateParameters = global::DigitalRuby.ThunderAndLightning.LightningBoltParameters.GetOrCreateParameters();
			orCreateParameters.Generator = global::DigitalRuby.ThunderAndLightning.LightningGeneratorPath.PathGeneratorInstance;
			return orCreateParameters;
		}

		public void Trigger(global::System.Collections.Generic.List<global::UnityEngine.Vector3> points, bool spline)
		{
			if (points.Count >= 2)
			{
				Generations = global::UnityEngine.Mathf.Clamp(Generations, 1, 5);
				global::DigitalRuby.ThunderAndLightning.LightningBoltParameters lightningBoltParameters = CreateParameters();
				lightningBoltParameters.Points.Clear();
				if (spline && points.Count > 3)
				{
					PopulateSpline(lightningBoltParameters.Points, points, Generations, DistancePerSegmentHint, Camera);
					lightningBoltParameters.SmoothingFactor = (lightningBoltParameters.Points.Count - 1) / points.Count;
				}
				else
				{
					lightningBoltParameters.Points.AddRange(points);
					lightningBoltParameters.SmoothingFactor = 1;
				}
				base.CreateLightningBolt(lightningBoltParameters);
				CreateLightningBoltsNow();
			}
		}

		public static void PopulateSpline(global::System.Collections.Generic.List<global::UnityEngine.Vector3> splinePoints, global::System.Collections.Generic.List<global::UnityEngine.Vector3> sourcePoints, int generations, float distancePerSegmentHit, global::UnityEngine.Camera camera)
		{
			splinePoints.Clear();
			global::DigitalRuby.ThunderAndLightning.PathGenerator.Is2D = camera != null && camera.orthographic;
			if (distancePerSegmentHit > 0f)
			{
				global::DigitalRuby.ThunderAndLightning.PathGenerator.CreateSplineWithSegmentDistance(splinePoints, sourcePoints, distancePerSegmentHit / (float)generations, closePath: false);
			}
			else
			{
				global::DigitalRuby.ThunderAndLightning.PathGenerator.CreateSpline(splinePoints, sourcePoints, sourcePoints.Count * generations * generations, closePath: false);
			}
		}
	}
}
