namespace SRDebugger.UI.Controls
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.CanvasRenderer))]
	public class ProfilerGraphControl : global::UnityEngine.UI.Graphic
	{
		public enum VerticalAlignments
		{
			Top = 0,
			Bottom = 1
		}

		public global::SRDebugger.UI.Controls.ProfilerGraphControl.VerticalAlignments VerticalAlignment = global::SRDebugger.UI.Controls.ProfilerGraphControl.VerticalAlignments.Bottom;

		private static readonly float[] ScaleSteps = new float[9]
		{
			0.005f,
			1f / 160f,
			1f / 120f,
			0.01f,
			1f / 60f,
			1f / 30f,
			0.05f,
			1f / 12f,
			1f / 6f
		};

		public bool FloatingScale;

		public bool TargetFpsUseApplication;

		public bool DrawAxes = true;

		public int TargetFps = 60;

		public bool Clip = true;

		public const float DataPointMargin = 2f;

		public const float DataPointVerticalMargin = 2f;

		public const float DataPointWidth = 4f;

		public int VerticalPadding = 10;

		public const int LineCount = 3;

		public global::UnityEngine.Color[] LineColours = new global::UnityEngine.Color[0];

		private global::SRDebugger.Services.IProfilerService _profilerService;

		private global::SRDebugger.UI.Controls.ProfilerGraphAxisLabel[] _axisLabels;

		private global::UnityEngine.Rect _clipBounds;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Vector3> _meshVertices = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Color32> _meshVertexColors = new global::System.Collections.Generic.List<global::UnityEngine.Color32>();

		private readonly global::System.Collections.Generic.List<int> _meshTriangles = new global::System.Collections.Generic.List<int>();

		protected override void Awake()
		{
			base.Awake();
			_profilerService = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.IProfilerService>();
		}

		protected override void Start()
		{
			base.Start();
		}

		protected void Update()
		{
			SetVerticesDirty();
		}

		[global::System.Obsolete]
		protected override void OnPopulateMesh(global::UnityEngine.Mesh m)
		{
			_meshVertices.Clear();
			_meshVertexColors.Clear();
			_meshTriangles.Clear();
			float width = base.rectTransform.rect.width;
			float height = base.rectTransform.rect.height;
			_clipBounds = new global::UnityEngine.Rect(0f, 0f, width, height);
			int num = TargetFps;
			if (global::UnityEngine.Application.isPlaying && TargetFpsUseApplication && global::UnityEngine.Application.targetFrameRate > 0)
			{
				num = global::UnityEngine.Application.targetFrameRate;
			}
			float num2 = 1f / (float)num;
			int num3 = -1;
			float num4 = (FloatingScale ? CalculateMaxFrameTime() : (1f / (float)num));
			if (FloatingScale)
			{
				for (int i = 0; i < ScaleSteps.Length; i++)
				{
					float num5 = ScaleSteps[i];
					if (num4 < num5 * 1.1f)
					{
						num2 = num5;
						num3 = i;
						break;
					}
				}
				if (num3 < 0)
				{
					num3 = ScaleSteps.Length - 1;
					num2 = ScaleSteps[num3];
				}
			}
			else
			{
				for (int j = 0; j < ScaleSteps.Length; j++)
				{
					float num6 = ScaleSteps[j];
					if (num4 > num6)
					{
						num3 = j;
					}
				}
			}
			float num7 = (height - (float)(VerticalPadding * 2)) / num2;
			int num8 = CalculateVisibleDataPointCount();
			int frameBufferCurrentSize = GetFrameBufferCurrentSize();
			for (int k = 0; k < frameBufferCurrentSize && k < num8; k++)
			{
				global::SRDebugger.Services.ProfilerFrame frame = GetFrame(frameBufferCurrentSize - k - 1);
				float xPosition = width - 4f * (float)k - 4f - width / 2f;
				DrawDataPoint(xPosition, num7, frame);
			}
			if (DrawAxes)
			{
				if (!FloatingScale)
				{
					DrawAxis(num2, num2 * num7, GetAxisLabel(0));
				}
				int num9 = 2;
				int num10 = 0;
				if (!FloatingScale)
				{
					num10++;
				}
				int num11 = num3;
				while (num11 >= 0 && num10 < num9)
				{
					DrawAxis(ScaleSteps[num11], ScaleSteps[num11] * num7, GetAxisLabel(num10));
					num10++;
					num11--;
				}
			}
			m.Clear();
			m.SetVertices(_meshVertices);
			m.SetColors(_meshVertexColors);
			m.SetTriangles(_meshTriangles, 0);
		}

		protected void DrawDataPoint(float xPosition, float verticalScale, global::SRDebugger.Services.ProfilerFrame frame)
		{
			float x = global::UnityEngine.Mathf.Min(_clipBounds.width / 2f, xPosition + 4f - 2f);
			float num = 0f;
			for (int i = 0; i < 3; i++)
			{
				int num2 = i;
				float num3 = 0f;
				switch (i)
				{
				case 0:
					num3 = (float)frame.UpdateTime;
					break;
				case 1:
					num3 = (float)frame.RenderTime;
					break;
				case 2:
					num3 = (float)frame.OtherTime;
					break;
				}
				num3 *= verticalScale;
				if (!global::SRF.SRFFloatExtensions.ApproxZero(num3) && !(num3 - 4f < 0f))
				{
					float num4 = num + 2f - base.rectTransform.rect.height / 2f;
					if (VerticalAlignment == global::SRDebugger.UI.Controls.ProfilerGraphControl.VerticalAlignments.Top)
					{
						num4 = base.rectTransform.rect.height / 2f - num - 2f;
					}
					float y = num4 + num3 - 2f;
					if (VerticalAlignment == global::SRDebugger.UI.Controls.ProfilerGraphControl.VerticalAlignments.Top)
					{
						y = num4 - num3 + 2f;
					}
					global::UnityEngine.Color c = LineColours[num2];
					AddRect(new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Max((0f - _clipBounds.width) / 2f, xPosition), num4), new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Max((0f - _clipBounds.width) / 2f, xPosition), y), new global::UnityEngine.Vector3(x, y), new global::UnityEngine.Vector3(x, num4), c);
					num += num3;
				}
			}
		}

		protected void DrawAxis(float frameTime, float yPosition, global::SRDebugger.UI.Controls.ProfilerGraphAxisLabel label)
		{
			float num = (0f - base.rectTransform.rect.width) * 0.5f;
			float x = 0f - num;
			float y = yPosition - base.rectTransform.rect.height * 0.5f + 0.5f;
			float y2 = yPosition - base.rectTransform.rect.height * 0.5f - 0.5f;
			AddRect(c: new global::UnityEngine.Color(1f, 1f, 1f, 0.4f), tl: new global::UnityEngine.Vector3(num, y2), tr: new global::UnityEngine.Vector3(num, y), bl: new global::UnityEngine.Vector3(x, y), br: new global::UnityEngine.Vector3(x, y2));
			if (label != null)
			{
				label.SetValue(frameTime, yPosition);
			}
		}

		protected void AddRect(global::UnityEngine.Vector3 tl, global::UnityEngine.Vector3 tr, global::UnityEngine.Vector3 bl, global::UnityEngine.Vector3 br, global::UnityEngine.Color c)
		{
			_meshVertices.Add(tl);
			_meshVertices.Add(tr);
			_meshVertices.Add(bl);
			_meshVertices.Add(br);
			_meshTriangles.Add(_meshVertices.Count - 4);
			_meshTriangles.Add(_meshVertices.Count - 3);
			_meshTriangles.Add(_meshVertices.Count - 1);
			_meshTriangles.Add(_meshVertices.Count - 2);
			_meshTriangles.Add(_meshVertices.Count - 1);
			_meshTriangles.Add(_meshVertices.Count - 3);
			_meshVertexColors.Add(c);
			_meshVertexColors.Add(c);
			_meshVertexColors.Add(c);
			_meshVertexColors.Add(c);
		}

		protected global::SRDebugger.Services.ProfilerFrame GetFrame(int i)
		{
			return _profilerService.FrameBuffer[i];
		}

		protected int CalculateVisibleDataPointCount()
		{
			return global::UnityEngine.Mathf.RoundToInt(base.rectTransform.rect.width / 4f);
		}

		protected int GetFrameBufferCurrentSize()
		{
			return _profilerService.FrameBuffer.Count;
		}

		protected int GetFrameBufferMaxSize()
		{
			return _profilerService.FrameBuffer.Capacity;
		}

		protected float CalculateMaxFrameTime()
		{
			int frameBufferCurrentSize = GetFrameBufferCurrentSize();
			int num = global::UnityEngine.Mathf.Min(CalculateVisibleDataPointCount(), frameBufferCurrentSize);
			double num2 = 0.0;
			for (int i = 0; i < num; i++)
			{
				int i2 = frameBufferCurrentSize - i - 1;
				global::SRDebugger.Services.ProfilerFrame frame = GetFrame(i2);
				if (frame.FrameTime > num2)
				{
					num2 = frame.FrameTime;
				}
			}
			return (float)num2;
		}

		private global::SRDebugger.UI.Controls.ProfilerGraphAxisLabel GetAxisLabel(int index)
		{
			if (_axisLabels == null || !global::UnityEngine.Application.isPlaying)
			{
				_axisLabels = GetComponentsInChildren<global::SRDebugger.UI.Controls.ProfilerGraphAxisLabel>();
			}
			if (_axisLabels.Length > index)
			{
				return _axisLabels[index];
			}
			global::UnityEngine.Debug.LogWarning("[SRDebugger.Profiler] Not enough axis labels in pool");
			return null;
		}
	}
}
