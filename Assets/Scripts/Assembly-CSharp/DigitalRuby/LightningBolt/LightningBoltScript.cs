namespace DigitalRuby.LightningBolt
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.LineRenderer))]
	public class LightningBoltScript : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.Tooltip("The game object where the lightning will emit from. If null, StartPosition is used.")]
		public global::UnityEngine.GameObject StartObject;

		[global::UnityEngine.Tooltip("The start position where the lightning will emit from. This is in world space if StartObject is null, otherwise this is offset from StartObject position.")]
		public global::UnityEngine.Vector3 StartPosition;

		[global::UnityEngine.Tooltip("The game object where the lightning will end at. If null, EndPosition is used.")]
		public global::UnityEngine.GameObject EndObject;

		[global::UnityEngine.Tooltip("The end position where the lightning will end at. This is in world space if EndObject is null, otherwise this is offset from EndObject position.")]
		public global::UnityEngine.Vector3 EndPosition;

		[global::UnityEngine.Range(0f, 8f)]
		[global::UnityEngine.Tooltip("How manu generations? Higher numbers create more line segments.")]
		public int Generations = 6;

		[global::UnityEngine.Range(0.01f, 1f)]
		[global::UnityEngine.Tooltip("How long each bolt should last before creating a new bolt. In ManualMode, the bolt will simply disappear after this amount of seconds.")]
		public float Duration = 0.05f;

		private float timer;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.Tooltip("How chaotic should the lightning be? (0-1)")]
		public float ChaosFactor = 0.15f;

		[global::UnityEngine.Tooltip("In manual mode, the trigger method must be called to create a bolt")]
		public bool ManualMode;

		[global::UnityEngine.Range(1f, 64f)]
		[global::UnityEngine.Tooltip("The number of rows in the texture. Used for animation.")]
		public int Rows = 1;

		[global::UnityEngine.Range(1f, 64f)]
		[global::UnityEngine.Tooltip("The number of columns in the texture. Used for animation.")]
		public int Columns = 1;

		[global::UnityEngine.Tooltip("The animation mode for the lightning")]
		public global::DigitalRuby.LightningBolt.LightningBoltAnimationMode AnimationMode = global::DigitalRuby.LightningBolt.LightningBoltAnimationMode.PingPong;

		[global::System.NonSerialized]
		[global::UnityEngine.HideInInspector]
		public global::System.Random RandomGenerator = new global::System.Random();

		private global::UnityEngine.LineRenderer lineRenderer;

		private global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Vector3, global::UnityEngine.Vector3>> segments = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Vector3, global::UnityEngine.Vector3>>();

		private int startIndex;

		private global::UnityEngine.Vector2 size;

		private global::UnityEngine.Vector2[] offsets;

		private int animationOffsetIndex;

		private int animationPingPongDirection = 1;

		private bool orthographic;

		public global::UnityEngine.Vector3 Offset = global::UnityEngine.Vector3.zero;

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

		private void GenerateLightningBolt(global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 end, int generation, int totalGenerations, float offsetAmount)
		{
			if (generation < 0 || generation > 8)
			{
				return;
			}
			if (orthographic)
			{
				start.z = (end.z = global::UnityEngine.Mathf.Min(start.z, end.z));
			}
			segments.Add(new global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Vector3, global::UnityEngine.Vector3>(start, end));
			if (generation == 0)
			{
				return;
			}
			if (offsetAmount <= 0f)
			{
				offsetAmount = (end - start).magnitude * ChaosFactor;
			}
			while (generation-- > 0)
			{
				int num = startIndex;
				startIndex = segments.Count;
				for (int i = num; i < startIndex; i++)
				{
					start = segments[i].Key;
					end = segments[i].Value;
					global::UnityEngine.Vector3 vector = (start + end) * 0.5f;
					RandomVector(ref start, ref end, offsetAmount, out var result);
					vector += result;
					segments.Add(new global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Vector3, global::UnityEngine.Vector3>(start, vector));
					segments.Add(new global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Vector3, global::UnityEngine.Vector3>(vector, end));
				}
				offsetAmount *= 0.5f;
			}
		}

		public void RandomVector(ref global::UnityEngine.Vector3 start, ref global::UnityEngine.Vector3 end, float offsetAmount, out global::UnityEngine.Vector3 result)
		{
			if (orthographic)
			{
				global::UnityEngine.Vector3 normalized = (end - start).normalized;
				global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(0f - normalized.y, normalized.x, normalized.z);
				float num = (float)RandomGenerator.NextDouble() * offsetAmount * 2f - offsetAmount;
				result = vector * num;
			}
			else
			{
				global::UnityEngine.Vector3 directionNormalized = (end - start).normalized;
				GetPerpendicularVector(ref directionNormalized, out var side);
				float num2 = ((float)RandomGenerator.NextDouble() + 0.1f) * offsetAmount;
				float angle = (float)RandomGenerator.NextDouble() * 360f;
				result = global::UnityEngine.Quaternion.AngleAxis(angle, directionNormalized) * side * num2;
			}
		}

		private void SelectOffsetFromAnimationMode()
		{
			if (AnimationMode == global::DigitalRuby.LightningBolt.LightningBoltAnimationMode.None)
			{
				lineRenderer.material.mainTextureOffset = offsets[0];
				return;
			}
			int num;
			if (AnimationMode == global::DigitalRuby.LightningBolt.LightningBoltAnimationMode.PingPong)
			{
				num = animationOffsetIndex;
				animationOffsetIndex += animationPingPongDirection;
				if (animationOffsetIndex >= offsets.Length)
				{
					animationOffsetIndex = offsets.Length - 2;
					animationPingPongDirection = -1;
				}
				else if (animationOffsetIndex < 0)
				{
					animationOffsetIndex = 1;
					animationPingPongDirection = 1;
				}
			}
			else if (AnimationMode == global::DigitalRuby.LightningBolt.LightningBoltAnimationMode.Loop)
			{
				num = animationOffsetIndex++;
				if (animationOffsetIndex >= offsets.Length)
				{
					animationOffsetIndex = 0;
				}
			}
			else
			{
				num = RandomGenerator.Next(0, offsets.Length);
			}
			if (num >= 0 && num < offsets.Length)
			{
				lineRenderer.material.mainTextureOffset = offsets[num];
			}
			else
			{
				lineRenderer.material.mainTextureOffset = offsets[0];
			}
		}

		private void UpdateLineRenderer()
		{
			int num = segments.Count - startIndex + 1;
			lineRenderer.positionCount = num;
			if (num >= 1)
			{
				int num2 = 0;
				lineRenderer.SetPosition(num2++, segments[startIndex].Key);
				for (int i = startIndex; i < segments.Count; i++)
				{
					lineRenderer.SetPosition(num2++, segments[i].Value);
				}
				segments.Clear();
				SelectOffsetFromAnimationMode();
			}
		}

		private void Start()
		{
			orthographic = global::UnityEngine.Camera.main != null && global::UnityEngine.Camera.main.orthographic;
			lineRenderer = GetComponent<global::UnityEngine.LineRenderer>();
			lineRenderer.positionCount = 0;
			UpdateFromMaterialChange();
		}

		private void Update()
		{
			orthographic = global::UnityEngine.Camera.main != null && global::UnityEngine.Camera.main.orthographic;
			if (timer <= 0f)
			{
				if (ManualMode)
				{
					timer = Duration;
					lineRenderer.positionCount = 0;
				}
				else
				{
					Trigger();
				}
			}
			timer -= global::UnityEngine.Time.deltaTime;
		}

		public void Trigger()
		{
			timer = Duration + global::UnityEngine.Mathf.Min(0f, timer);
			global::UnityEngine.Vector3 vector = ((!(StartObject == null)) ? (StartObject.transform.position + StartPosition) : StartPosition);
			global::UnityEngine.Vector3 vector2 = ((!(EndObject == null)) ? (EndObject.transform.position + EndPosition) : EndPosition);
			startIndex = 0;
			GenerateLightningBolt(vector + Offset, vector2 + Offset, Generations, Generations, 0f);
			UpdateLineRenderer();
		}

		public void UpdateFromMaterialChange()
		{
			size = new global::UnityEngine.Vector2(1f / (float)Columns, 1f / (float)Rows);
			lineRenderer.material.mainTextureScale = size;
			offsets = new global::UnityEngine.Vector2[Rows * Columns];
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					offsets[j + i * Columns] = new global::UnityEngine.Vector2((float)j / (float)Columns, (float)i / (float)Rows);
				}
			}
		}
	}
}
