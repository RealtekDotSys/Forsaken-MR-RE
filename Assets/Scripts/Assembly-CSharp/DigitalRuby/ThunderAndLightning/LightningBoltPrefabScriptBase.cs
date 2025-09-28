namespace DigitalRuby.ThunderAndLightning
{
	public abstract class LightningBoltPrefabScriptBase : global::DigitalRuby.ThunderAndLightning.LightningBoltScript
	{
		private readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters> batchParameters = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters>();

		private readonly global::System.Random random = new global::System.Random();

		[global::UnityEngine.Header("Lightning Spawn Properties")]
		[global::DigitalRuby.ThunderAndLightning.SingleLineClamp("How long to wait before creating another round of lightning bolts in seconds", 0.001, double.MaxValue)]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats IntervalRange = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 0.05f,
			Maximum = 0.1f
		};

		[global::DigitalRuby.ThunderAndLightning.SingleLineClamp("How many lightning bolts to emit for each interval", 0.0, 100.0)]
		public global::DigitalRuby.ThunderAndLightning.RangeOfIntegers CountRange = new global::DigitalRuby.ThunderAndLightning.RangeOfIntegers
		{
			Minimum = 1,
			Maximum = 1
		};

		[global::UnityEngine.Tooltip("Reduces the probability that additional bolts from CountRange will actually happen (0 - 1).")]
		[global::UnityEngine.Range(0f, 1f)]
		public float CountProbabilityModifier = 1f;

		[global::DigitalRuby.ThunderAndLightning.SingleLineClamp("Delay in seconds (range) before each additional lightning bolt in count range is emitted", 0.0, 30.0)]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats DelayRange = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 0f,
			Maximum = 0f
		};

		[global::DigitalRuby.ThunderAndLightning.SingleLineClamp("For each bolt emitted, how long should it stay in seconds", 0.01, 10.0)]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats DurationRange = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 0.06f,
			Maximum = 0.12f
		};

		[global::UnityEngine.Header("Lightning Appearance Properties")]
		[global::DigitalRuby.ThunderAndLightning.SingleLineClamp("The trunk width range in unity units (x = min, y = max)", 0.0001, 100.0)]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats TrunkWidthRange = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 0.1f,
			Maximum = 0.2f
		};

		[global::UnityEngine.Tooltip("How long (in seconds) this game object should live before destroying itself. Leave as 0 for infinite.")]
		[global::UnityEngine.Range(0f, 1000f)]
		public float LifeTime;

		[global::UnityEngine.Tooltip("Generations (1 - 8, higher makes more detailed but more expensive lightning)")]
		[global::UnityEngine.Range(1f, 8f)]
		public int Generations = 6;

		[global::UnityEngine.Tooltip("The chaos factor that determines how far the lightning main trunk can spread out, higher numbers spread out more. 0 - 1.")]
		[global::UnityEngine.Range(0f, 1f)]
		public float ChaosFactor = 0.075f;

		[global::UnityEngine.Tooltip("The chaos factor that determines how far the forks of the lightning can spread out, higher numbers spread out more. 0 - 1.")]
		[global::UnityEngine.Range(0f, 1f)]
		public float ChaosFactorForks = 0.095f;

		[global::UnityEngine.Tooltip("Intensity of the lightning")]
		[global::UnityEngine.Range(0f, 10f)]
		public float Intensity = 1f;

		[global::UnityEngine.Tooltip("The intensity of the glow")]
		[global::UnityEngine.Range(0f, 10f)]
		public float GlowIntensity = 0.1f;

		[global::UnityEngine.Tooltip("The width multiplier for the glow, 0 - 64")]
		[global::UnityEngine.Range(0f, 64f)]
		public float GlowWidthMultiplier = 4f;

		[global::UnityEngine.Tooltip("What percent of time the lightning should fade in and out. For example, 0.15 fades in 15% of the time and fades out 15% of the time, with full visibility 70% of the time.")]
		[global::UnityEngine.Range(0f, 0.5f)]
		public float FadePercent = 0.15f;

		[global::UnityEngine.Tooltip("Modify the duration of lightning fade in.")]
		[global::UnityEngine.Range(0f, 1f)]
		public float FadeInMultiplier = 1f;

		[global::UnityEngine.Tooltip("Modify the duration of fully lit lightning.")]
		[global::UnityEngine.Range(0f, 1f)]
		public float FadeFullyLitMultiplier = 1f;

		[global::UnityEngine.Tooltip("Modify the duration of lightning fade out.")]
		[global::UnityEngine.Range(0f, 1f)]
		public float FadeOutMultiplier = 1f;

		[global::UnityEngine.Tooltip("0 - 1, how slowly the lightning should grow. 0 for instant, 1 for slow.")]
		[global::UnityEngine.Range(0f, 1f)]
		public float GrowthMultiplier;

		[global::UnityEngine.Tooltip("How much smaller the lightning should get as it goes towards the end of the bolt. For example, 0.5 will make the end 50% the width of the start.")]
		[global::UnityEngine.Range(0f, 10f)]
		public float EndWidthMultiplier = 0.5f;

		[global::UnityEngine.Tooltip("How forked should the lightning be? (0 - 1, 0 for none, 1 for lots of forks)")]
		[global::UnityEngine.Range(0f, 1f)]
		public float Forkedness = 0.25f;

		[global::UnityEngine.Range(0f, 10f)]
		[global::UnityEngine.Tooltip("Minimum distance multiplier for forks")]
		public float ForkLengthMultiplier = 0.6f;

		[global::UnityEngine.Range(0f, 10f)]
		[global::UnityEngine.Tooltip("Fork distance multiplier variance. Random range of 0 to n that is added to Fork Length Multiplier.")]
		public float ForkLengthVariance = 0.2f;

		[global::UnityEngine.Tooltip("Forks have their EndWidthMultiplier multiplied by this value")]
		[global::UnityEngine.Range(0f, 10f)]
		public float ForkEndWidthMultiplier = 1f;

		[global::UnityEngine.Header("Lightning Light Properties")]
		[global::UnityEngine.Tooltip("Light parameters")]
		public global::DigitalRuby.ThunderAndLightning.LightningLightParameters LightParameters;

		[global::UnityEngine.Tooltip("Maximum number of lights that can be created per batch of lightning")]
		[global::UnityEngine.Range(0f, 64f)]
		public int MaximumLightsPerBatch = 8;

		[global::UnityEngine.Header("Lightning Trigger Type")]
		[global::UnityEngine.Tooltip("Manual or automatic mode. Manual requires that you call the Trigger method in script. Automatic uses the interval to create lightning continuously.")]
		public bool ManualMode;

		[global::UnityEngine.Tooltip("Turns lightning into automatic mode for this number of seconds, then puts it into manual mode.")]
		[global::UnityEngine.Range(0f, 120f)]
		public float AutomaticModeSeconds;

		[global::UnityEngine.Header("Lightning custom transform handler")]
		[global::UnityEngine.Tooltip("Custom handler to modify the transform of each lightning bolt, useful if it will be alive longer than a few frames and needs to scale and rotate based on the position of other objects.")]
		public global::DigitalRuby.ThunderAndLightning.LightningCustomTransformDelegate CustomTransformHandler;

		private float nextLightningTimestamp;

		private float lifeTimeRemaining;

		public global::System.Random RandomOverride { get; set; }

		private void CalculateNextLightningTimestamp(float offset)
		{
			nextLightningTimestamp = ((IntervalRange.Minimum == IntervalRange.Maximum) ? IntervalRange.Minimum : (offset + IntervalRange.Random()));
		}

		private void CustomTransform(global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo state)
		{
			if (CustomTransformHandler != null)
			{
				CustomTransformHandler.Invoke(state);
			}
		}

		private void CallLightning()
		{
			CallLightning(null, null);
		}

		private void CallLightning(global::UnityEngine.Vector3? start, global::UnityEngine.Vector3? end)
		{
			global::System.Random r = RandomOverride ?? random;
			int num = CountRange.Random(r);
			for (int i = 0; i < num; i++)
			{
				global::DigitalRuby.ThunderAndLightning.LightningBoltParameters lightningBoltParameters = CreateParameters();
				if (CountProbabilityModifier >= 0.9999f || i == 0 || (float)lightningBoltParameters.Random.NextDouble() <= CountProbabilityModifier)
				{
					lightningBoltParameters.CustomTransform = ((CustomTransformHandler == null) ? null : new global::System.Action<global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo>(CustomTransform));
					CreateLightningBolt(lightningBoltParameters);
					if (start.HasValue)
					{
						lightningBoltParameters.Start = start.Value;
					}
					if (end.HasValue)
					{
						lightningBoltParameters.End = end.Value;
					}
				}
				else
				{
					global::DigitalRuby.ThunderAndLightning.LightningBoltParameters.ReturnParametersToCache(lightningBoltParameters);
				}
			}
			CreateLightningBoltsNow();
		}

		protected void CreateLightningBoltsNow()
		{
			int maximumLightsPerBatch = global::DigitalRuby.ThunderAndLightning.LightningBolt.MaximumLightsPerBatch;
			global::DigitalRuby.ThunderAndLightning.LightningBolt.MaximumLightsPerBatch = MaximumLightsPerBatch;
			CreateLightningBolts(batchParameters);
			global::DigitalRuby.ThunderAndLightning.LightningBolt.MaximumLightsPerBatch = maximumLightsPerBatch;
			batchParameters.Clear();
		}

		protected override void PopulateParameters(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p)
		{
			base.PopulateParameters(p);
			p.RandomOverride = RandomOverride;
			float lifeTime = DurationRange.Random(p.Random);
			float trunkWidth = TrunkWidthRange.Random(p.Random);
			p.Generations = Generations;
			p.LifeTime = lifeTime;
			p.ChaosFactor = ChaosFactor;
			p.ChaosFactorForks = ChaosFactorForks;
			p.TrunkWidth = trunkWidth;
			p.Intensity = Intensity;
			p.GlowIntensity = GlowIntensity;
			p.GlowWidthMultiplier = GlowWidthMultiplier;
			p.Forkedness = Forkedness;
			p.ForkLengthMultiplier = ForkLengthMultiplier;
			p.ForkLengthVariance = ForkLengthVariance;
			p.FadePercent = FadePercent;
			p.FadeInMultiplier = FadeInMultiplier;
			p.FadeOutMultiplier = FadeOutMultiplier;
			p.FadeFullyLitMultiplier = FadeFullyLitMultiplier;
			p.GrowthMultiplier = GrowthMultiplier;
			p.EndWidthMultiplier = EndWidthMultiplier;
			p.ForkEndWidthMultiplier = ForkEndWidthMultiplier;
			p.DelayRange = DelayRange;
			p.LightParameters = LightParameters;
		}

		protected override void Start()
		{
			base.Start();
			CalculateNextLightningTimestamp(0f);
			lifeTimeRemaining = ((LifeTime <= 0f) ? float.MaxValue : LifeTime);
		}

		protected override void Update()
		{
			base.Update();
			if ((lifeTimeRemaining -= global::DigitalRuby.ThunderAndLightning.LightningBoltScript.DeltaTime) < 0f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			if ((nextLightningTimestamp -= global::DigitalRuby.ThunderAndLightning.LightningBoltScript.DeltaTime) <= 0f)
			{
				CalculateNextLightningTimestamp(nextLightningTimestamp);
				if (!ManualMode)
				{
					CallLightning();
				}
			}
			if (AutomaticModeSeconds > 0f)
			{
				AutomaticModeSeconds = global::UnityEngine.Mathf.Max(0f, AutomaticModeSeconds - global::DigitalRuby.ThunderAndLightning.LightningBoltScript.DeltaTime);
				ManualMode = AutomaticModeSeconds == 0f;
			}
		}

		public override void CreateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p)
		{
			batchParameters.Add(p);
		}

		public void Trigger()
		{
			Trigger(-1f);
		}

		public void Trigger(float seconds)
		{
			CallLightning();
			if (seconds >= 0f)
			{
				AutomaticModeSeconds = global::UnityEngine.Mathf.Max(0f, seconds);
			}
		}

		public void Trigger(global::UnityEngine.Vector3? start, global::UnityEngine.Vector3? end)
		{
			CallLightning(start, end);
		}
	}
}
