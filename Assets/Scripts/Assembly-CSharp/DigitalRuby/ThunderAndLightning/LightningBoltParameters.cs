namespace DigitalRuby.ThunderAndLightning
{
	[global::System.Serializable]
	public sealed class LightningBoltParameters
	{
		private static int randomSeed;

		private static readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters> cache;

		internal int generationWhereForksStop;

		internal int forkednessCalculated;

		internal global::DigitalRuby.ThunderAndLightning.LightningBoltQualitySetting quality;

		internal float delaySeconds;

		internal int maxLights;

		public static float Scale;

		public static readonly global::System.Collections.Generic.Dictionary<int, global::DigitalRuby.ThunderAndLightning.LightningQualityMaximum> QualityMaximums;

		public global::DigitalRuby.ThunderAndLightning.LightningGenerator Generator;

		public global::UnityEngine.Vector3 Start;

		public global::UnityEngine.Vector3 End;

		public global::UnityEngine.Vector3 StartVariance;

		public global::UnityEngine.Vector3 EndVariance;

		public global::System.Action<global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo> CustomTransform;

		private int generations;

		public float LifeTime;

		public float Delay;

		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats DelayRange;

		public float ChaosFactor;

		public float ChaosFactorForks = -1f;

		public float TrunkWidth;

		public float EndWidthMultiplier = 0.5f;

		public float Intensity = 1f;

		public float GlowIntensity;

		public float GlowWidthMultiplier;

		public float Forkedness;

		public int GenerationWhereForksStopSubtractor = 5;

		public global::UnityEngine.Color32 Color = new global::UnityEngine.Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		private global::System.Random random;

		private global::System.Random currentRandom;

		private global::System.Random randomOverride;

		public float FadePercent = 0.15f;

		public float FadeInMultiplier = 1f;

		public float FadeFullyLitMultiplier = 1f;

		public float FadeOutMultiplier = 1f;

		private float growthMultiplier;

		public float ForkLengthMultiplier = 0.6f;

		public float ForkLengthVariance = 0.2f;

		public float ForkEndWidthMultiplier = 1f;

		public global::DigitalRuby.ThunderAndLightning.LightningLightParameters LightParameters;

		public int SmoothingFactor;

		public int Generations
		{
			get
			{
				return generations;
			}
			set
			{
				int b = global::UnityEngine.Mathf.Clamp(value, 1, 8);
				if (quality == global::DigitalRuby.ThunderAndLightning.LightningBoltQualitySetting.UseScript)
				{
					generations = b;
					return;
				}
				int qualityLevel = global::UnityEngine.QualitySettings.GetQualityLevel();
				if (QualityMaximums.TryGetValue(qualityLevel, out var value2))
				{
					generations = global::UnityEngine.Mathf.Min(value2.MaximumGenerations, b);
					return;
				}
				generations = b;
				global::UnityEngine.Debug.LogError("Unable to read lightning quality settings from level " + qualityLevel);
			}
		}

		public global::System.Random Random
		{
			get
			{
				return currentRandom;
			}
			set
			{
				random = value ?? random;
				currentRandom = randomOverride ?? random;
			}
		}

		public global::System.Random RandomOverride
		{
			get
			{
				return randomOverride;
			}
			set
			{
				randomOverride = value;
				currentRandom = randomOverride ?? random;
			}
		}

		public float GrowthMultiplier
		{
			get
			{
				return growthMultiplier;
			}
			set
			{
				growthMultiplier = global::UnityEngine.Mathf.Clamp(value, 0f, 0.999f);
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.Vector3> Points { get; set; }

		static LightningBoltParameters()
		{
			randomSeed = global::System.Environment.TickCount;
			cache = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters>();
			Scale = 1f;
			QualityMaximums = new global::System.Collections.Generic.Dictionary<int, global::DigitalRuby.ThunderAndLightning.LightningQualityMaximum>();
			string[] names = global::UnityEngine.QualitySettings.names;
			for (int i = 0; i < names.Length; i++)
			{
				switch (i)
				{
				case 0:
					QualityMaximums[i] = new global::DigitalRuby.ThunderAndLightning.LightningQualityMaximum
					{
						MaximumGenerations = 3,
						MaximumLightPercent = 0f,
						MaximumShadowPercent = 0f
					};
					break;
				case 1:
					QualityMaximums[i] = new global::DigitalRuby.ThunderAndLightning.LightningQualityMaximum
					{
						MaximumGenerations = 4,
						MaximumLightPercent = 0f,
						MaximumShadowPercent = 0f
					};
					break;
				case 2:
					QualityMaximums[i] = new global::DigitalRuby.ThunderAndLightning.LightningQualityMaximum
					{
						MaximumGenerations = 5,
						MaximumLightPercent = 0.1f,
						MaximumShadowPercent = 0f
					};
					break;
				case 3:
					QualityMaximums[i] = new global::DigitalRuby.ThunderAndLightning.LightningQualityMaximum
					{
						MaximumGenerations = 5,
						MaximumLightPercent = 0.1f,
						MaximumShadowPercent = 0f
					};
					break;
				case 4:
					QualityMaximums[i] = new global::DigitalRuby.ThunderAndLightning.LightningQualityMaximum
					{
						MaximumGenerations = 6,
						MaximumLightPercent = 0.05f,
						MaximumShadowPercent = 0.1f
					};
					break;
				case 5:
					QualityMaximums[i] = new global::DigitalRuby.ThunderAndLightning.LightningQualityMaximum
					{
						MaximumGenerations = 7,
						MaximumLightPercent = 0.025f,
						MaximumShadowPercent = 0.05f
					};
					break;
				default:
					QualityMaximums[i] = new global::DigitalRuby.ThunderAndLightning.LightningQualityMaximum
					{
						MaximumGenerations = 8,
						MaximumLightPercent = 0.025f,
						MaximumShadowPercent = 0.05f
					};
					break;
				}
			}
		}

		public LightningBoltParameters()
		{
			random = (currentRandom = new global::System.Random(randomSeed++));
			Points = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();
		}

		public float ForkMultiplier()
		{
			return (float)Random.NextDouble() * ForkLengthVariance + ForkLengthMultiplier;
		}

		public global::UnityEngine.Vector3 ApplyVariance(global::UnityEngine.Vector3 pos, global::UnityEngine.Vector3 variance)
		{
			return new global::UnityEngine.Vector3(pos.x + ((float)Random.NextDouble() * 2f - 1f) * variance.x, pos.y + ((float)Random.NextDouble() * 2f - 1f) * variance.y, pos.z + ((float)Random.NextDouble() * 2f - 1f) * variance.z);
		}

		public void Reset()
		{
			Start = (End = global::UnityEngine.Vector3.zero);
			Generator = null;
			SmoothingFactor = 0;
			RandomOverride = null;
			CustomTransform = null;
			if (Points != null)
			{
				Points.Clear();
			}
		}

		public static global::DigitalRuby.ThunderAndLightning.LightningBoltParameters GetOrCreateParameters()
		{
			global::DigitalRuby.ThunderAndLightning.LightningBoltParameters result;
			if (cache.Count == 0)
			{
				result = new global::DigitalRuby.ThunderAndLightning.LightningBoltParameters();
			}
			else
			{
				int index = cache.Count - 1;
				result = cache[index];
				cache.RemoveAt(index);
			}
			return result;
		}

		public static void ReturnParametersToCache(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p)
		{
			if (!cache.Contains(p))
			{
				p.Reset();
				cache.Add(p);
			}
		}
	}
}
