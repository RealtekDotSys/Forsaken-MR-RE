namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBolt
	{
		public class LineRendererMesh
		{
			private const int defaultListCapacity = 2048;

			private static readonly global::UnityEngine.Vector2 uv1 = new global::UnityEngine.Vector2(0f, 0f);

			private static readonly global::UnityEngine.Vector2 uv2 = new global::UnityEngine.Vector2(1f, 0f);

			private static readonly global::UnityEngine.Vector2 uv3 = new global::UnityEngine.Vector2(0f, 1f);

			private static readonly global::UnityEngine.Vector2 uv4 = new global::UnityEngine.Vector2(1f, 1f);

			private readonly global::System.Collections.Generic.List<int> indices = new global::System.Collections.Generic.List<int>(2048);

			private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector3> vertices = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>(2048);

			private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector4> lineDirs = new global::System.Collections.Generic.List<global::UnityEngine.Vector4>(2048);

			private readonly global::System.Collections.Generic.List<global::UnityEngine.Color32> colors = new global::System.Collections.Generic.List<global::UnityEngine.Color32>(2048);

			private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector3> ends = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>(2048);

			private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector4> texCoordsAndGlowModifiers = new global::System.Collections.Generic.List<global::UnityEngine.Vector4>(2048);

			private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector4> fadeLifetimes = new global::System.Collections.Generic.List<global::UnityEngine.Vector4>(2048);

			private const int boundsPadder = 1000000000;

			private int currentBoundsMinX = 1147483647;

			private int currentBoundsMinY = 1147483647;

			private int currentBoundsMinZ = 1147483647;

			private int currentBoundsMaxX = -1147483648;

			private int currentBoundsMaxY = -1147483648;

			private int currentBoundsMaxZ = -1147483648;

			private global::UnityEngine.Mesh mesh;

			private global::UnityEngine.MeshFilter meshFilter;

			private global::UnityEngine.MeshRenderer meshRenderer;

			public global::UnityEngine.GameObject GameObject { get; private set; }

			public global::UnityEngine.Material Material
			{
				get
				{
					return meshRenderer.sharedMaterial;
				}
				set
				{
					meshRenderer.sharedMaterial = value;
				}
			}

			public global::UnityEngine.MeshRenderer MeshRenderer => meshRenderer;

			public int Tag { get; set; }

			public global::System.Action<global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo> CustomTransform { get; set; }

			public global::UnityEngine.Transform Transform { get; private set; }

			public bool Empty => vertices.Count == 0;

			public LineRendererMesh()
			{
				GameObject = new global::UnityEngine.GameObject("LightningBoltMeshRenderer");
				GameObject.SetActive(value: false);
				mesh = new global::UnityEngine.Mesh
				{
					name = "ProceduralLightningMesh"
				};
				mesh.MarkDynamic();
				meshFilter = GameObject.AddComponent<global::UnityEngine.MeshFilter>();
				meshFilter.sharedMesh = mesh;
				meshRenderer = GameObject.AddComponent<global::UnityEngine.MeshRenderer>();
				meshRenderer.shadowCastingMode = global::UnityEngine.Rendering.ShadowCastingMode.Off;
				meshRenderer.reflectionProbeUsage = global::UnityEngine.Rendering.ReflectionProbeUsage.Off;
				meshRenderer.lightProbeUsage = global::UnityEngine.Rendering.LightProbeUsage.Off;
				meshRenderer.receiveShadows = false;
				Transform = GameObject.GetComponent<global::UnityEngine.Transform>();
			}

			public void PopulateMesh()
			{
				if (vertices.Count == 0)
				{
					mesh.Clear();
				}
				else
				{
					PopulateMeshInternal();
				}
			}

			public bool PrepareForLines(int lineCount)
			{
				int num = lineCount * 4;
				if (vertices.Count + num > 64999)
				{
					return false;
				}
				return true;
			}

			public void BeginLine(global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 end, float radius, global::UnityEngine.Color32 color, float colorIntensity, global::UnityEngine.Vector4 fadeLifeTime, float glowWidthModifier, float glowIntensity)
			{
				global::UnityEngine.Vector4 dir = end - start;
				dir.w = radius;
				AppendLineInternal(ref start, ref end, ref dir, ref dir, ref dir, color, colorIntensity, ref fadeLifeTime, glowWidthModifier, glowIntensity);
			}

			public void AppendLine(global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 end, float radius, global::UnityEngine.Color32 color, float colorIntensity, global::UnityEngine.Vector4 fadeLifeTime, float glowWidthModifier, float glowIntensity)
			{
				global::UnityEngine.Vector4 dir = end - start;
				dir.w = radius;
				global::UnityEngine.Vector4 dirPrev = lineDirs[lineDirs.Count - 3];
				global::UnityEngine.Vector4 dirPrev2 = lineDirs[lineDirs.Count - 1];
				AppendLineInternal(ref start, ref end, ref dir, ref dirPrev, ref dirPrev2, color, colorIntensity, ref fadeLifeTime, glowWidthModifier, glowIntensity);
			}

			public void Reset()
			{
				CustomTransform = null;
				Tag++;
				GameObject.SetActive(value: false);
				mesh.Clear();
				indices.Clear();
				vertices.Clear();
				colors.Clear();
				lineDirs.Clear();
				ends.Clear();
				texCoordsAndGlowModifiers.Clear();
				fadeLifetimes.Clear();
				currentBoundsMaxX = (currentBoundsMaxY = (currentBoundsMaxZ = -1147483648));
				currentBoundsMinX = (currentBoundsMinY = (currentBoundsMinZ = 1147483647));
			}

			private void PopulateMeshInternal()
			{
				GameObject.SetActive(value: true);
				mesh.SetVertices(vertices);
				mesh.SetTangents(lineDirs);
				mesh.SetColors(colors);
				mesh.SetUVs(0, texCoordsAndGlowModifiers);
				mesh.SetUVs(1, fadeLifetimes);
				mesh.SetNormals(ends);
				mesh.SetTriangles(indices, 0);
				global::UnityEngine.Bounds bounds = default(global::UnityEngine.Bounds);
				global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(currentBoundsMinX - 2, currentBoundsMinY - 2, currentBoundsMinZ - 2);
				global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(currentBoundsMaxX + 2, currentBoundsMaxY + 2, currentBoundsMaxZ + 2);
				bounds.center = (vector2 + vector) * 0.5f;
				bounds.size = (vector2 - vector) * 1.2f;
				mesh.bounds = bounds;
			}

			private void UpdateBounds(ref global::UnityEngine.Vector3 point1, ref global::UnityEngine.Vector3 point2)
			{
				int num = (int)point1.x - (int)point2.x;
				num &= num >> 31;
				int num2 = (int)point2.x + num;
				int num3 = (int)point1.x - num;
				num = currentBoundsMinX - num2;
				num &= num >> 31;
				currentBoundsMinX = num2 + num;
				num = currentBoundsMaxX - num3;
				num &= num >> 31;
				currentBoundsMaxX -= num;
				int num4 = (int)point1.y - (int)point2.y;
				num4 &= num4 >> 31;
				int num5 = (int)point2.y + num4;
				int num6 = (int)point1.y - num4;
				num4 = currentBoundsMinY - num5;
				num4 &= num4 >> 31;
				currentBoundsMinY = num5 + num4;
				num4 = currentBoundsMaxY - num6;
				num4 &= num4 >> 31;
				currentBoundsMaxY -= num4;
				int num7 = (int)point1.z - (int)point2.z;
				num7 &= num7 >> 31;
				int num8 = (int)point2.z + num7;
				int num9 = (int)point1.z - num7;
				num7 = currentBoundsMinZ - num8;
				num7 &= num7 >> 31;
				currentBoundsMinZ = num8 + num7;
				num7 = currentBoundsMaxZ - num9;
				num7 &= num7 >> 31;
				currentBoundsMaxZ -= num7;
			}

			private void AddIndices()
			{
				int count = vertices.Count;
				indices.Add(count++);
				indices.Add(count++);
				indices.Add(count);
				indices.Add(count--);
				indices.Add(count);
				indices.Add(count += 2);
			}

			private void AppendLineInternal(ref global::UnityEngine.Vector3 start, ref global::UnityEngine.Vector3 end, ref global::UnityEngine.Vector4 dir, ref global::UnityEngine.Vector4 dirPrev1, ref global::UnityEngine.Vector4 dirPrev2, global::UnityEngine.Color32 color, float colorIntensity, ref global::UnityEngine.Vector4 fadeLifeTime, float glowWidthModifier, float glowIntensity)
			{
				AddIndices();
				color.a = (byte)global::UnityEngine.Mathf.Lerp(0f, 255f, colorIntensity * 0.1f);
				global::UnityEngine.Vector4 item = new global::UnityEngine.Vector4(uv1.x, uv1.y, glowWidthModifier, glowIntensity);
				vertices.Add(start);
				lineDirs.Add(dirPrev1);
				colors.Add(color);
				ends.Add(dir);
				vertices.Add(end);
				lineDirs.Add(dir);
				colors.Add(color);
				ends.Add(dir);
				dir.w = 0f - dir.w;
				vertices.Add(start);
				lineDirs.Add(dirPrev2);
				colors.Add(color);
				ends.Add(dir);
				vertices.Add(end);
				lineDirs.Add(dir);
				colors.Add(color);
				ends.Add(dir);
				texCoordsAndGlowModifiers.Add(item);
				item.x = uv2.x;
				item.y = uv2.y;
				texCoordsAndGlowModifiers.Add(item);
				item.x = uv3.x;
				item.y = uv3.y;
				texCoordsAndGlowModifiers.Add(item);
				item.x = uv4.x;
				item.y = uv4.y;
				texCoordsAndGlowModifiers.Add(item);
				fadeLifetimes.Add(fadeLifeTime);
				fadeLifetimes.Add(fadeLifeTime);
				fadeLifetimes.Add(fadeLifeTime);
				fadeLifetimes.Add(fadeLifeTime);
				UpdateBounds(ref start, ref end);
			}
		}

		public static int MaximumLightCount = 128;

		public static int MaximumLightsPerBatch = 8;

		private global::System.DateTime startTimeOffset;

		private global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies dependencies;

		private float elapsedTime;

		private float lifeTime;

		private float maxLifeTime;

		private bool hasLight;

		private float timeSinceLevelLoad;

		private readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup> segmentGroups = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup>();

		private readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup> segmentGroupsWithLight = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup>();

		private readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh> activeLineRenderers = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh>();

		private static int lightCount;

		private static readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh> lineRendererCache = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh>();

		private static readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup> groupCache = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup>();

		private static readonly global::System.Collections.Generic.List<global::UnityEngine.Light> lightCache = new global::System.Collections.Generic.List<global::UnityEngine.Light>();

		public float MinimumDelay { get; private set; }

		public bool HasGlow { get; private set; }

		public bool IsActive => elapsedTime < lifeTime;

		public global::DigitalRuby.ThunderAndLightning.CameraMode CameraMode { get; private set; }

		public void SetupLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies dependencies)
		{
			if (dependencies == null || dependencies.Parameters.Count == 0)
			{
				global::UnityEngine.Debug.LogError("Lightning bolt dependencies must not be null");
				return;
			}
			if (this.dependencies != null)
			{
				global::UnityEngine.Debug.LogError("This lightning bolt is already in use!");
				return;
			}
			this.dependencies = dependencies;
			CameraMode = dependencies.CameraMode;
			timeSinceLevelLoad = global::DigitalRuby.ThunderAndLightning.LightningBoltScript.TimeSinceStart;
			CheckForGlow(dependencies.Parameters);
			MinimumDelay = float.MaxValue;
			if (dependencies.ThreadState != null)
			{
				startTimeOffset = global::System.DateTime.UtcNow;
				dependencies.ThreadState.AddActionForBackgroundThread(ProcessAllLightningParameters);
			}
			else
			{
				ProcessAllLightningParameters();
			}
		}

		public bool Update()
		{
			elapsedTime += global::DigitalRuby.ThunderAndLightning.LightningBoltScript.DeltaTime;
			if (elapsedTime > maxLifeTime)
			{
				return false;
			}
			if (hasLight)
			{
				UpdateLights();
			}
			return true;
		}

		public void Cleanup()
		{
			foreach (global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup item in segmentGroupsWithLight)
			{
				foreach (global::UnityEngine.Light light in item.Lights)
				{
					CleanupLight(light);
				}
				item.Lights.Clear();
			}
			lock (groupCache)
			{
				foreach (global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup segmentGroup in segmentGroups)
				{
					groupCache.Add(segmentGroup);
				}
			}
			hasLight = false;
			elapsedTime = 0f;
			lifeTime = 0f;
			maxLifeTime = 0f;
			if (dependencies != null)
			{
				dependencies.ReturnToCache(dependencies);
				dependencies = null;
			}
			foreach (global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh activeLineRenderer in activeLineRenderers)
			{
				if (activeLineRenderer != null)
				{
					activeLineRenderer.Reset();
					lineRendererCache.Add(activeLineRenderer);
				}
			}
			segmentGroups.Clear();
			segmentGroupsWithLight.Clear();
			activeLineRenderers.Clear();
		}

		public global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup AddGroup()
		{
			global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup lightningBoltSegmentGroup;
			lock (groupCache)
			{
				if (groupCache.Count == 0)
				{
					lightningBoltSegmentGroup = new global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup();
				}
				else
				{
					int index = groupCache.Count - 1;
					lightningBoltSegmentGroup = groupCache[index];
					lightningBoltSegmentGroup.Reset();
					groupCache.RemoveAt(index);
				}
			}
			segmentGroups.Add(lightningBoltSegmentGroup);
			return lightningBoltSegmentGroup;
		}

		public static void ClearCache()
		{
			foreach (global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh item in lineRendererCache)
			{
				if (item != null)
				{
					global::UnityEngine.Object.Destroy(item.GameObject);
				}
			}
			foreach (global::UnityEngine.Light item2 in lightCache)
			{
				if (item2 != null)
				{
					global::UnityEngine.Object.Destroy(item2.gameObject);
				}
			}
			lineRendererCache.Clear();
			lightCache.Clear();
			lock (groupCache)
			{
				groupCache.Clear();
			}
		}

		private void CleanupLight(global::UnityEngine.Light l)
		{
			if (l != null)
			{
				dependencies.LightRemoved(l);
				lightCache.Add(l);
				l.gameObject.SetActive(value: false);
				lightCount--;
			}
		}

		private void EnableLineRenderer(global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh lineRenderer, int tag)
		{
			if (lineRenderer != null && lineRenderer.GameObject != null && lineRenderer.Tag == tag && IsActive)
			{
				lineRenderer.PopulateMesh();
			}
		}

		private global::System.Collections.IEnumerator EnableLastRendererCoRoutine()
		{
			global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh lineRenderer = activeLineRenderers[activeLineRenderers.Count - 1];
			int tag = ++lineRenderer.Tag;
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(MinimumDelay);
			EnableLineRenderer(lineRenderer, tag);
		}

		private global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh GetOrCreateLineRenderer()
		{
			global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh lineRendererMesh;
			do
			{
				if (lineRendererCache.Count == 0)
				{
					lineRendererMesh = new global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh();
					break;
				}
				int index = lineRendererCache.Count - 1;
				lineRendererMesh = lineRendererCache[index];
				lineRendererCache.RemoveAt(index);
			}
			while (lineRendererMesh == null || lineRendererMesh.Transform == null);
			lineRendererMesh.Transform.parent = null;
			lineRendererMesh.Transform.rotation = global::UnityEngine.Quaternion.identity;
			lineRendererMesh.Transform.localScale = global::UnityEngine.Vector3.one;
			lineRendererMesh.Transform.parent = dependencies.Parent.transform;
			lineRendererMesh.GameObject.layer = dependencies.Parent.layer;
			if (dependencies.UseWorldSpace)
			{
				lineRendererMesh.GameObject.transform.position = global::UnityEngine.Vector3.zero;
			}
			else
			{
				lineRendererMesh.GameObject.transform.localPosition = global::UnityEngine.Vector3.zero;
			}
			lineRendererMesh.Material = (HasGlow ? dependencies.LightningMaterialMesh : dependencies.LightningMaterialMeshNoGlow);
			if (!string.IsNullOrEmpty(dependencies.SortLayerName))
			{
				lineRendererMesh.MeshRenderer.sortingLayerName = dependencies.SortLayerName;
				lineRendererMesh.MeshRenderer.sortingOrder = dependencies.SortOrderInLayer;
			}
			else
			{
				lineRendererMesh.MeshRenderer.sortingLayerName = null;
				lineRendererMesh.MeshRenderer.sortingOrder = 0;
			}
			activeLineRenderers.Add(lineRendererMesh);
			return lineRendererMesh;
		}

		private void RenderGroup(global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup group, global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p)
		{
			if (group.SegmentCount == 0)
			{
				return;
			}
			float num = ((dependencies.ThreadState == null) ? 0f : ((float)(global::System.DateTime.UtcNow - startTimeOffset).TotalSeconds));
			float num2 = timeSinceLevelLoad + group.Delay + num;
			global::UnityEngine.Vector4 fadeLifeTime = new global::UnityEngine.Vector4(num2, num2 + group.PeakStart, num2 + group.PeakEnd, num2 + group.LifeTime);
			float num3 = group.LineWidth * 0.5f * global::DigitalRuby.ThunderAndLightning.LightningBoltParameters.Scale;
			int num4 = group.Segments.Count - group.StartIndex;
			float num5 = (num3 - num3 * group.EndWidthMultiplier) / (float)num4;
			float num6;
			if (p.GrowthMultiplier > 0f)
			{
				num6 = group.LifeTime / (float)num4 * p.GrowthMultiplier;
				num = 0f;
			}
			else
			{
				num6 = 0f;
				num = 0f;
			}
			global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh currentLineRenderer = ((activeLineRenderers.Count == 0) ? GetOrCreateLineRenderer() : activeLineRenderers[activeLineRenderers.Count - 1]);
			if (!currentLineRenderer.PrepareForLines(num4))
			{
				if (currentLineRenderer.CustomTransform != null)
				{
					return;
				}
				if (dependencies.ThreadState != null)
				{
					dependencies.ThreadState.AddActionForMainThread(delegate
					{
						EnableCurrentLineRenderer();
						currentLineRenderer = GetOrCreateLineRenderer();
					}, waitForAction: true);
				}
				else
				{
					EnableCurrentLineRenderer();
					currentLineRenderer = GetOrCreateLineRenderer();
				}
			}
			currentLineRenderer.BeginLine(group.Segments[group.StartIndex].Start, group.Segments[group.StartIndex].End, num3, group.Color, p.Intensity, fadeLifeTime, p.GlowWidthMultiplier, p.GlowIntensity);
			for (int num7 = group.StartIndex + 1; num7 < group.Segments.Count; num7++)
			{
				num3 -= num5;
				if (p.GrowthMultiplier < 1f)
				{
					num += num6;
					fadeLifeTime = new global::UnityEngine.Vector4(num2 + num, num2 + group.PeakStart + num, num2 + group.PeakEnd, num2 + group.LifeTime);
				}
				currentLineRenderer.AppendLine(group.Segments[num7].Start, group.Segments[num7].End, num3, group.Color, p.Intensity, fadeLifeTime, p.GlowWidthMultiplier, p.GlowIntensity);
			}
		}

		private static global::System.Collections.IEnumerator NotifyBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies dependencies, global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p, global::UnityEngine.Transform transform, global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 end)
		{
			float delaySeconds = p.delaySeconds;
			float lifeTime = p.LifeTime;
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(delaySeconds);
			if (dependencies.LightningBoltStarted != null)
			{
				dependencies.LightningBoltStarted(p, start, end);
			}
			global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo state = ((p.CustomTransform == null) ? null : global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo.GetOrCreateStateInfo());
			if (state != null)
			{
				state.Parameters = p;
				state.BoltStartPosition = start;
				state.BoltEndPosition = end;
				state.State = global::DigitalRuby.ThunderAndLightning.LightningCustomTransformState.Started;
				state.Transform = transform;
				p.CustomTransform(state);
				state.State = global::DigitalRuby.ThunderAndLightning.LightningCustomTransformState.Executing;
			}
			if (p.CustomTransform == null)
			{
				yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(lifeTime);
			}
			else
			{
				while (lifeTime > 0f)
				{
					p.CustomTransform(state);
					lifeTime -= global::DigitalRuby.ThunderAndLightning.LightningBoltScript.DeltaTime;
					yield return null;
				}
			}
			if (p.CustomTransform != null)
			{
				state.State = global::DigitalRuby.ThunderAndLightning.LightningCustomTransformState.Ended;
				p.CustomTransform(state);
				global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo.ReturnStateInfoToCache(state);
			}
			if (dependencies.LightningBoltEnded != null)
			{
				dependencies.LightningBoltEnded(p, start, end);
			}
			global::DigitalRuby.ThunderAndLightning.LightningBoltParameters.ReturnParametersToCache(p);
		}

		private void ProcessParameters(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters p, global::DigitalRuby.ThunderAndLightning.RangeOfFloats delay, global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies depends)
		{
			MinimumDelay = global::UnityEngine.Mathf.Min(delay.Minimum, MinimumDelay);
			p.delaySeconds = delay.Random(p.Random);
			if (depends.LevelOfDetailDistance > global::UnityEngine.Mathf.Epsilon)
			{
				float num;
				if (p.Points.Count > 1)
				{
					num = global::UnityEngine.Vector3.Distance(depends.CameraPos, p.Points[0]);
					num = global::UnityEngine.Mathf.Min(global::UnityEngine.Vector3.Distance(depends.CameraPos, p.Points[p.Points.Count - 1]));
				}
				else
				{
					num = global::UnityEngine.Vector3.Distance(depends.CameraPos, p.Start);
					num = global::UnityEngine.Mathf.Min(global::UnityEngine.Vector3.Distance(depends.CameraPos, p.End));
				}
				int num2 = global::UnityEngine.Mathf.Min(8, (int)(num / depends.LevelOfDetailDistance));
				p.Generations = global::UnityEngine.Mathf.Max(1, p.Generations - num2);
				p.GenerationWhereForksStopSubtractor = global::UnityEngine.Mathf.Clamp(p.GenerationWhereForksStopSubtractor - num2, 0, 8);
			}
			p.generationWhereForksStop = p.Generations - p.GenerationWhereForksStopSubtractor;
			lifeTime = global::UnityEngine.Mathf.Max(p.LifeTime + p.delaySeconds, lifeTime);
			maxLifeTime = global::UnityEngine.Mathf.Max(lifeTime, maxLifeTime);
			p.forkednessCalculated = (int)global::UnityEngine.Mathf.Ceil(p.Forkedness * (float)p.Generations);
			if (p.Generations > 0)
			{
				p.Generator = p.Generator ?? global::DigitalRuby.ThunderAndLightning.LightningGenerator.GeneratorInstance;
				p.Generator.GenerateLightningBolt(this, p, out var start, out var end);
				p.Start = start;
				p.End = end;
			}
		}

		private void ProcessAllLightningParameters()
		{
			int maxLights = MaximumLightsPerBatch / dependencies.Parameters.Count;
			global::DigitalRuby.ThunderAndLightning.RangeOfFloats delay = default(global::DigitalRuby.ThunderAndLightning.RangeOfFloats);
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>(dependencies.Parameters.Count + 1);
			int num = 0;
			foreach (global::DigitalRuby.ThunderAndLightning.LightningBoltParameters parameter in dependencies.Parameters)
			{
				delay.Minimum = parameter.DelayRange.Minimum + parameter.Delay;
				delay.Maximum = parameter.DelayRange.Maximum + parameter.Delay;
				parameter.maxLights = maxLights;
				list.Add(segmentGroups.Count);
				ProcessParameters(parameter, delay, dependencies);
			}
			list.Add(segmentGroups.Count);
			global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies dependenciesRef = dependencies;
			foreach (global::DigitalRuby.ThunderAndLightning.LightningBoltParameters parameters in dependenciesRef.Parameters)
			{
				global::UnityEngine.Transform transform = RenderLightningBolt(parameters.quality, parameters.Generations, list[num], list[++num], parameters);
				if (dependenciesRef.ThreadState != null)
				{
					dependenciesRef.ThreadState.AddActionForMainThread(delegate
					{
						dependenciesRef.StartCoroutine(NotifyBolt(dependenciesRef, parameters, transform, parameters.Start, parameters.End));
					});
				}
				else
				{
					dependenciesRef.StartCoroutine(NotifyBolt(dependenciesRef, parameters, transform, parameters.Start, parameters.End));
				}
			}
			if (dependencies.ThreadState != null)
			{
				dependencies.ThreadState.AddActionForMainThread(EnableCurrentLineRendererFromThread);
				return;
			}
			EnableCurrentLineRenderer();
			dependencies.AddActiveBolt(this);
		}

		private void EnableCurrentLineRendererFromThread()
		{
			EnableCurrentLineRenderer();
			dependencies.ThreadState = null;
			dependencies.AddActiveBolt(this);
		}

		private void EnableCurrentLineRenderer()
		{
			if (activeLineRenderers.Count != 0)
			{
				if (MinimumDelay <= 0f)
				{
					EnableLineRenderer(activeLineRenderers[activeLineRenderers.Count - 1], activeLineRenderers[activeLineRenderers.Count - 1].Tag);
				}
				else
				{
					dependencies.StartCoroutine(EnableLastRendererCoRoutine());
				}
			}
		}

		private void RenderParticleSystems(global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 end, float trunkWidth, float lifeTime, float delaySeconds)
		{
			if (trunkWidth > 0f)
			{
				if (dependencies.OriginParticleSystem != null)
				{
					dependencies.StartCoroutine(GenerateParticleCoRoutine(dependencies.OriginParticleSystem, start, delaySeconds));
				}
				if (dependencies.DestParticleSystem != null)
				{
					dependencies.StartCoroutine(GenerateParticleCoRoutine(dependencies.DestParticleSystem, end, delaySeconds + lifeTime * 0.8f));
				}
			}
		}

		private global::UnityEngine.Transform RenderLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltQualitySetting quality, int generations, int startGroupIndex, int endGroupIndex, global::DigitalRuby.ThunderAndLightning.LightningBoltParameters parameters)
		{
			if (segmentGroups.Count == 0 || startGroupIndex >= segmentGroups.Count || endGroupIndex > segmentGroups.Count)
			{
				return null;
			}
			global::UnityEngine.Transform result = null;
			global::DigitalRuby.ThunderAndLightning.LightningLightParameters lp = parameters.LightParameters;
			if (lp != null)
			{
				if (hasLight |= lp.HasLight)
				{
					lp.LightPercent = global::UnityEngine.Mathf.Clamp(lp.LightPercent, global::UnityEngine.Mathf.Epsilon, 1f);
					lp.LightShadowPercent = global::UnityEngine.Mathf.Clamp(lp.LightShadowPercent, 0f, 1f);
				}
				else
				{
					lp = null;
				}
			}
			global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup lightningBoltSegmentGroup = segmentGroups[startGroupIndex];
			global::UnityEngine.Vector3 start = lightningBoltSegmentGroup.Segments[lightningBoltSegmentGroup.StartIndex].Start;
			global::UnityEngine.Vector3 end = lightningBoltSegmentGroup.Segments[lightningBoltSegmentGroup.StartIndex + lightningBoltSegmentGroup.SegmentCount - 1].End;
			parameters.FadePercent = global::UnityEngine.Mathf.Clamp(parameters.FadePercent, 0f, 0.5f);
			if (parameters.CustomTransform != null)
			{
				global::DigitalRuby.ThunderAndLightning.LightningBolt.LineRendererMesh currentLineRenderer = ((activeLineRenderers.Count == 0 || !activeLineRenderers[activeLineRenderers.Count - 1].Empty) ? null : activeLineRenderers[activeLineRenderers.Count - 1]);
				if (currentLineRenderer == null)
				{
					if (dependencies.ThreadState != null)
					{
						dependencies.ThreadState.AddActionForMainThread(delegate
						{
							EnableCurrentLineRenderer();
							currentLineRenderer = GetOrCreateLineRenderer();
						}, waitForAction: true);
					}
					else
					{
						EnableCurrentLineRenderer();
						currentLineRenderer = GetOrCreateLineRenderer();
					}
				}
				if (currentLineRenderer == null)
				{
					return null;
				}
				currentLineRenderer.CustomTransform = parameters.CustomTransform;
				result = currentLineRenderer.Transform;
			}
			for (int num = startGroupIndex; num < endGroupIndex; num++)
			{
				global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup lightningBoltSegmentGroup2 = segmentGroups[num];
				lightningBoltSegmentGroup2.Delay = parameters.delaySeconds;
				lightningBoltSegmentGroup2.LifeTime = parameters.LifeTime;
				lightningBoltSegmentGroup2.PeakStart = lightningBoltSegmentGroup2.LifeTime * parameters.FadePercent;
				lightningBoltSegmentGroup2.PeakEnd = lightningBoltSegmentGroup2.LifeTime - lightningBoltSegmentGroup2.PeakStart;
				float num2 = lightningBoltSegmentGroup2.PeakEnd - lightningBoltSegmentGroup2.PeakStart;
				float num3 = lightningBoltSegmentGroup2.LifeTime - lightningBoltSegmentGroup2.PeakEnd;
				lightningBoltSegmentGroup2.PeakStart *= parameters.FadeInMultiplier;
				lightningBoltSegmentGroup2.PeakEnd = lightningBoltSegmentGroup2.PeakStart + num2 * parameters.FadeFullyLitMultiplier;
				lightningBoltSegmentGroup2.LifeTime = lightningBoltSegmentGroup2.PeakEnd + num3 * parameters.FadeOutMultiplier;
				lightningBoltSegmentGroup2.LightParameters = lp;
				RenderGroup(lightningBoltSegmentGroup2, parameters);
			}
			if (dependencies.ThreadState != null)
			{
				dependencies.ThreadState.AddActionForMainThread(delegate
				{
					RenderParticleSystems(start, end, parameters.TrunkWidth, parameters.LifeTime, parameters.delaySeconds);
					if (lp != null)
					{
						CreateLightsForGroup(segmentGroups[startGroupIndex], lp, quality, parameters.maxLights);
					}
				});
			}
			else
			{
				RenderParticleSystems(start, end, parameters.TrunkWidth, parameters.LifeTime, parameters.delaySeconds);
				if (lp != null)
				{
					CreateLightsForGroup(segmentGroups[startGroupIndex], lp, quality, parameters.maxLights);
				}
			}
			return result;
		}

		private void CreateLightsForGroup(global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup group, global::DigitalRuby.ThunderAndLightning.LightningLightParameters lp, global::DigitalRuby.ThunderAndLightning.LightningBoltQualitySetting quality, int maxLights)
		{
			if (lightCount == MaximumLightCount || maxLights <= 0)
			{
				return;
			}
			float num = (lifeTime - group.PeakEnd) * lp.FadeOutMultiplier;
			float num2 = (group.PeakEnd - group.PeakStart) * lp.FadeFullyLitMultiplier;
			float num3 = group.PeakStart * lp.FadeInMultiplier + num2 + num;
			maxLifeTime = global::UnityEngine.Mathf.Max(maxLifeTime, group.Delay + num3);
			segmentGroupsWithLight.Add(group);
			int segmentCount = group.SegmentCount;
			float num4;
			float num5;
			if (quality == global::DigitalRuby.ThunderAndLightning.LightningBoltQualitySetting.LimitToQualitySetting)
			{
				int qualityLevel = global::UnityEngine.QualitySettings.GetQualityLevel();
				if (global::DigitalRuby.ThunderAndLightning.LightningBoltParameters.QualityMaximums.TryGetValue(qualityLevel, out var value))
				{
					num4 = global::UnityEngine.Mathf.Min(lp.LightPercent, value.MaximumLightPercent);
					num5 = global::UnityEngine.Mathf.Min(lp.LightShadowPercent, value.MaximumShadowPercent);
				}
				else
				{
					global::UnityEngine.Debug.LogError("Unable to read lightning quality for level " + qualityLevel);
					num4 = lp.LightPercent;
					num5 = lp.LightShadowPercent;
				}
			}
			else
			{
				num4 = lp.LightPercent;
				num5 = lp.LightShadowPercent;
			}
			maxLights = global::UnityEngine.Mathf.Max(1, global::UnityEngine.Mathf.Min(maxLights, (int)((float)segmentCount * num4)));
			int num6 = global::UnityEngine.Mathf.Max(1, segmentCount / maxLights);
			int num7 = maxLights - (int)((float)maxLights * num5);
			int nthShadowCounter = num7;
			for (int i = group.StartIndex + (int)((float)num6 * 0.5f); i < group.Segments.Count && !AddLightToGroup(group, lp, i, num6, num7, ref maxLights, ref nthShadowCounter); i += num6)
			{
			}
		}

		private bool AddLightToGroup(global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup group, global::DigitalRuby.ThunderAndLightning.LightningLightParameters lp, int segmentIndex, int nthLight, int nthShadows, ref int maxLights, ref int nthShadowCounter)
		{
			global::UnityEngine.Light orCreateLight = GetOrCreateLight(lp);
			group.Lights.Add(orCreateLight);
			global::UnityEngine.Vector3 vector = (group.Segments[segmentIndex].Start + group.Segments[segmentIndex].End) * 0.5f;
			if (dependencies.CameraIsOrthographic)
			{
				if (dependencies.CameraMode == global::DigitalRuby.ThunderAndLightning.CameraMode.OrthographicXZ)
				{
					vector.y = dependencies.CameraPos.y + lp.OrthographicOffset;
				}
				else
				{
					vector.z = dependencies.CameraPos.z + lp.OrthographicOffset;
				}
			}
			if (dependencies.UseWorldSpace)
			{
				orCreateLight.gameObject.transform.position = vector;
			}
			else
			{
				orCreateLight.gameObject.transform.localPosition = vector;
			}
			if (lp.LightShadowPercent == 0f || ++nthShadowCounter < nthShadows)
			{
				orCreateLight.shadows = global::UnityEngine.LightShadows.None;
			}
			else
			{
				orCreateLight.shadows = global::UnityEngine.LightShadows.Soft;
				nthShadowCounter = 0;
			}
			if (++lightCount != MaximumLightCount)
			{
				return --maxLights == 0;
			}
			return true;
		}

		private global::UnityEngine.Light GetOrCreateLight(global::DigitalRuby.ThunderAndLightning.LightningLightParameters lp)
		{
			global::UnityEngine.Light light;
			do
			{
				if (lightCache.Count == 0)
				{
					light = new global::UnityEngine.GameObject("LightningBoltLight").AddComponent<global::UnityEngine.Light>();
					light.type = global::UnityEngine.LightType.Point;
					break;
				}
				light = lightCache[lightCache.Count - 1];
				lightCache.RemoveAt(lightCache.Count - 1);
			}
			while (light == null);
			light.bounceIntensity = lp.BounceIntensity;
			light.shadowNormalBias = lp.ShadowNormalBias;
			light.color = lp.LightColor;
			light.renderMode = lp.RenderMode;
			light.range = lp.LightRange;
			light.shadowStrength = lp.ShadowStrength;
			light.shadowBias = lp.ShadowBias;
			light.intensity = 0f;
			light.gameObject.transform.parent = dependencies.Parent.transform;
			light.gameObject.SetActive(value: true);
			dependencies.LightAdded(light);
			return light;
		}

		private void UpdateLight(global::DigitalRuby.ThunderAndLightning.LightningLightParameters lp, global::System.Collections.Generic.IEnumerable<global::UnityEngine.Light> lights, float delay, float peakStart, float peakEnd, float lifeTime)
		{
			if (elapsedTime < delay)
			{
				return;
			}
			float num = (lifeTime - peakEnd) * lp.FadeOutMultiplier;
			float num2 = (peakEnd - peakStart) * lp.FadeFullyLitMultiplier;
			peakStart *= lp.FadeInMultiplier;
			peakEnd = peakStart + num2;
			lifeTime = peakEnd + num;
			float num3 = elapsedTime - delay;
			if (num3 >= peakStart)
			{
				if (num3 <= peakEnd)
				{
					foreach (global::UnityEngine.Light light in lights)
					{
						light.intensity = lp.LightIntensity;
					}
					return;
				}
				float t = (num3 - peakEnd) / (lifeTime - peakEnd);
				{
					foreach (global::UnityEngine.Light light2 in lights)
					{
						light2.intensity = global::UnityEngine.Mathf.Lerp(lp.LightIntensity, 0f, t);
					}
					return;
				}
			}
			float t2 = num3 / peakStart;
			foreach (global::UnityEngine.Light light3 in lights)
			{
				light3.intensity = global::UnityEngine.Mathf.Lerp(0f, lp.LightIntensity, t2);
			}
		}

		private void UpdateLights()
		{
			foreach (global::DigitalRuby.ThunderAndLightning.LightningBoltSegmentGroup item in segmentGroupsWithLight)
			{
				UpdateLight(item.LightParameters, item.Lights, item.Delay, item.PeakStart, item.PeakEnd, item.LifeTime);
			}
		}

		private global::System.Collections.IEnumerator GenerateParticleCoRoutine(global::UnityEngine.ParticleSystem p, global::UnityEngine.Vector3 pos, float delay)
		{
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(delay);
			p.transform.position = pos;
			if (p.emission.burstCount > 0)
			{
				global::UnityEngine.ParticleSystem.Burst[] array = new global::UnityEngine.ParticleSystem.Burst[p.emission.burstCount];
				p.emission.GetBursts(array);
				int count = global::UnityEngine.Random.Range(array[0].minCount, array[0].maxCount + 1);
				p.Emit(count);
			}
			else
			{
				global::UnityEngine.ParticleSystem.MinMaxCurve rateOverTime = p.emission.rateOverTime;
				int count = (int)((rateOverTime.constantMax - rateOverTime.constantMin) * 0.5f);
				count = global::UnityEngine.Random.Range(count, count * 2);
				p.Emit(count);
			}
		}

		private void CheckForGlow(global::System.Collections.Generic.IEnumerable<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters> parameters)
		{
			foreach (global::DigitalRuby.ThunderAndLightning.LightningBoltParameters parameter in parameters)
			{
				HasGlow = parameter.GlowIntensity >= global::UnityEngine.Mathf.Epsilon && parameter.GlowWidthMultiplier >= global::UnityEngine.Mathf.Epsilon;
				if (HasGlow)
				{
					break;
				}
			}
		}
	}
}
