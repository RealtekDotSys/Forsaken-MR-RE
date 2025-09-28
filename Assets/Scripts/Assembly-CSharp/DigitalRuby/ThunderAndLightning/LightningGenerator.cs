namespace DigitalRuby.ThunderAndLightning
{
	public class LightningGenerator
	{
		public static readonly global::DigitalRuby.ThunderAndLightning.LightningGenerator GeneratorInstance = new global::DigitalRuby.ThunderAndLightning.LightningGenerator();

		private void GetPerpendicularVector(ref global::UnityEngine.Vector3 directionNormalized, out global::UnityEngine.Vector3 side)
		{
			if (directionNormalized == global::UnityEngine.Vector3.zero)
			{
				side = global::UnityEngine.Vector3.right;
				return;
			}
			float x = directionNormalized.x;
			float y = directionNormalized.y;
			float z = directionNormalized.z;
			float num = global::UnityEngine.Mathf.Abs(x);
			float num2 = global::UnityEngine.Mathf.Abs(y);
			float num3 = global::UnityEngine.Mathf.Abs(z);
			float num4;
			float num5;
			float num6;
			if (num >= num2 && num2 >= num3)
			{
				num4 = 1f;
				num5 = 1f;
				num6 = (0f - (y * num4 + z * num5)) / x;
			}
			else if (num2 >= num3)
			{
				num6 = 1f;
				num5 = 1f;
				num4 = (0f - (x * num6 + z * num5)) / y;
			}
			else
			{
				num6 = 1f;
				num4 = 1f;
				num5 = (0f - (x * num6 + y * num4)) / z;
			}
			side = new global::UnityEngine.Vector3(num6, num4, num5).normalized;
		}

		protected virtual void OnGenerateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBolt bolt, global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 end, global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p)
		{
			GenerateLightningBoltStandard(bolt, start, end, p.Generations, p.Generations, 0f, p);
		}

		public bool ShouldCreateFork(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p, int generation, int totalGenerations)
		{
			if (generation > p.generationWhereForksStop && generation >= totalGenerations - p.forkednessCalculated)
			{
				return (float)p.Random.NextDouble() < p.Forkedness;
			}
			return false;
		}

		public void CreateFork(global::DigitalRuby.ThunderAndLightning.LightningBolt bolt, global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p, int generation, int totalGenerations, global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 midPoint)
		{
			if (ShouldCreateFork(p, generation, totalGenerations))
			{
				global::UnityEngine.Vector3 vector = (midPoint - start) * p.ForkMultiplier();
				global::UnityEngine.Vector3 end = midPoint + vector;
				GenerateLightningBoltStandard(bolt, midPoint, end, generation, totalGenerations, 0f, p);
			}
		}

		public void GenerateLightningBoltStandard(global::DigitalRuby.ThunderAndLightning.LightningBolt bolt, global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 end, int generation, int totalGenerations, float offsetAmount, global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p)
		{
			if (generation < 1)
			{
				return;
			}
			global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup lightningBoltSegmentGroup = bolt.AddGroup();
			lightningBoltSegmentGroup.Segments.Add(new global::DigitalRuby.ThunderAndLightning.LightningBoltSegment
			{
				Start = start,
				End = end
			});
			float num = (float)generation / (float)totalGenerations;
			num *= num;
			lightningBoltSegmentGroup.LineWidth = p.TrunkWidth * num;
			lightningBoltSegmentGroup.Generation = generation;
			lightningBoltSegmentGroup.Color = p.Color;
			lightningBoltSegmentGroup.Color.a = (byte)(255f * num);
			lightningBoltSegmentGroup.EndWidthMultiplier = p.EndWidthMultiplier * p.ForkEndWidthMultiplier;
			if (offsetAmount <= 0f)
			{
				offsetAmount = (end - start).magnitude * ((generation == totalGenerations) ? p.ChaosFactor : p.ChaosFactorForks);
			}
			while (generation-- > 0)
			{
				int startIndex = lightningBoltSegmentGroup.StartIndex;
				lightningBoltSegmentGroup.StartIndex = lightningBoltSegmentGroup.Segments.Count;
				for (int i = startIndex; i < lightningBoltSegmentGroup.StartIndex; i++)
				{
					start = lightningBoltSegmentGroup.Segments[i].Start;
					end = lightningBoltSegmentGroup.Segments[i].End;
					global::UnityEngine.Vector3 vector = (start + end) * 0.5f;
					RandomVector(bolt, ref start, ref end, offsetAmount, p.Random, out var result);
					vector += result;
					lightningBoltSegmentGroup.Segments.Add(new global::DigitalRuby.ThunderAndLightning.LightningBoltSegment
					{
						Start = start,
						End = vector
					});
					lightningBoltSegmentGroup.Segments.Add(new global::DigitalRuby.ThunderAndLightning.LightningBoltSegment
					{
						Start = vector,
						End = end
					});
					CreateFork(bolt, p, generation, totalGenerations, start, vector);
				}
				offsetAmount *= 0.5f;
			}
		}

		public global::UnityEngine.Vector3 RandomDirection3D(global::System.Random r)
		{
			float num = 2f * (float)r.NextDouble() - 1f;
			global::UnityEngine.Vector3 result = RandomDirection2D(r) * global::UnityEngine.Mathf.Sqrt(1f - num * num);
			result.z = num;
			return result;
		}

		public global::UnityEngine.Vector3 RandomDirection2D(global::System.Random r)
		{
			float f = (float)r.NextDouble() * 2f * global::System.MathF.PI;
			return new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(f), global::UnityEngine.Mathf.Sin(f), 0f);
		}

		public global::UnityEngine.Vector3 RandomDirection2DXZ(global::System.Random r)
		{
			float f = (float)r.NextDouble() * 2f * global::System.MathF.PI;
			return new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(f), 0f, global::UnityEngine.Mathf.Sin(f));
		}

		public void RandomVector(global::DigitalRuby.ThunderAndLightning.LightningBolt bolt, ref global::UnityEngine.Vector3 start, ref global::UnityEngine.Vector3 end, float offsetAmount, global::System.Random random, out global::UnityEngine.Vector3 result)
		{
			if (bolt.CameraMode == global::DigitalRuby.ThunderAndLightning.CameraMode.Perspective)
			{
				global::UnityEngine.Vector3 directionNormalized = (end - start).normalized;
				global::UnityEngine.Vector3 side = global::UnityEngine.Vector3.Cross(start, end);
				if (side == global::UnityEngine.Vector3.zero)
				{
					GetPerpendicularVector(ref directionNormalized, out side);
				}
				else
				{
					side.Normalize();
				}
				float num = ((float)random.NextDouble() + 0.1f) * offsetAmount;
				float num2 = (float)random.NextDouble() * global::System.MathF.PI;
				directionNormalized *= (float)global::System.Math.Sin(num2);
				global::UnityEngine.Quaternion quaternion = default(global::UnityEngine.Quaternion);
				quaternion.x = directionNormalized.x;
				quaternion.y = directionNormalized.y;
				quaternion.z = directionNormalized.z;
				quaternion.w = (float)global::System.Math.Cos(num2);
				result = quaternion * side * num;
			}
			else if (bolt.CameraMode == global::DigitalRuby.ThunderAndLightning.CameraMode.OrthographicXY)
			{
				end.z = start.z;
				global::UnityEngine.Vector3 normalized = (end - start).normalized;
				global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(0f - normalized.y, normalized.x, 0f);
				float num3 = (float)random.NextDouble() * offsetAmount * 2f - offsetAmount;
				result = vector * num3;
			}
			else
			{
				end.y = start.y;
				global::UnityEngine.Vector3 normalized2 = (end - start).normalized;
				global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(0f - normalized2.z, 0f, normalized2.x);
				float num4 = (float)random.NextDouble() * offsetAmount * 2f - offsetAmount;
				result = vector2 * num4;
			}
		}

		public void GenerateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBolt bolt, global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p)
		{
			GenerateLightningBolt(bolt, p, out var _, out var _);
		}

		public void GenerateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBolt bolt, global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p, out global::UnityEngine.Vector3 start, out global::UnityEngine.Vector3 end)
		{
			start = p.ApplyVariance(p.Start, p.StartVariance);
			end = p.ApplyVariance(p.End, p.EndVariance);
			OnGenerateLightningBolt(bolt, start, end, p);
		}
	}
}
