namespace VLB
{
	public static class Utils
	{
		public enum FloatPackingPrecision
		{
			High = 64,
			Low = 8,
			Undef = 0
		}

		private static global::VLB.Utils.FloatPackingPrecision ms_FloatPackingPrecision;

		private const int kFloatPackingHighMinShaderLevel = 35;

		public static string GetPath(global::UnityEngine.Transform current)
		{
			if (current.parent == null)
			{
				return "/" + current.name;
			}
			return GetPath(current.parent) + "/" + current.name;
		}

		public static T NewWithComponent<T>(string name) where T : global::UnityEngine.Component
		{
			return new global::UnityEngine.GameObject(name, typeof(T)).GetComponent<T>();
		}

		public static T GetOrAddComponent<T>(this global::UnityEngine.GameObject self) where T : global::UnityEngine.Component
		{
			T val = self.GetComponent<T>();
			if (val == null)
			{
				val = self.AddComponent<T>();
			}
			return val;
		}

		public static T GetOrAddComponent<T>(this global::UnityEngine.MonoBehaviour self) where T : global::UnityEngine.Component
		{
			return self.gameObject.GetOrAddComponent<T>();
		}

		public static bool HasFlag(this global::System.Enum mask, global::System.Enum flags)
		{
			return ((int)(object)mask & (int)(object)flags) == (int)(object)flags;
		}

		public static global::UnityEngine.Vector2 xy(this global::UnityEngine.Vector3 aVector)
		{
			return new global::UnityEngine.Vector2(aVector.x, aVector.y);
		}

		public static global::UnityEngine.Vector2 xz(this global::UnityEngine.Vector3 aVector)
		{
			return new global::UnityEngine.Vector2(aVector.x, aVector.z);
		}

		public static global::UnityEngine.Vector2 yz(this global::UnityEngine.Vector3 aVector)
		{
			return new global::UnityEngine.Vector2(aVector.y, aVector.z);
		}

		public static global::UnityEngine.Vector2 yx(this global::UnityEngine.Vector3 aVector)
		{
			return new global::UnityEngine.Vector2(aVector.y, aVector.x);
		}

		public static global::UnityEngine.Vector2 zx(this global::UnityEngine.Vector3 aVector)
		{
			return new global::UnityEngine.Vector2(aVector.z, aVector.x);
		}

		public static global::UnityEngine.Vector2 zy(this global::UnityEngine.Vector3 aVector)
		{
			return new global::UnityEngine.Vector2(aVector.z, aVector.y);
		}

		public static float GetVolumeCubic(this global::UnityEngine.Bounds self)
		{
			return self.size.x * self.size.y * self.size.z;
		}

		public static float GetMaxArea2D(this global::UnityEngine.Bounds self)
		{
			return global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Max(self.size.x * self.size.y, self.size.y * self.size.z), self.size.x * self.size.z);
		}

		public static global::UnityEngine.Color Opaque(this global::UnityEngine.Color self)
		{
			return new global::UnityEngine.Color(self.r, self.g, self.b, 1f);
		}

		public static void GizmosDrawPlane(global::UnityEngine.Vector3 normal, global::UnityEngine.Vector3 position, global::UnityEngine.Color color, float size = 1f)
		{
			global::UnityEngine.Vector3 vector = global::UnityEngine.Vector3.Cross(normal, (global::UnityEngine.Mathf.Abs(global::UnityEngine.Vector3.Dot(normal, global::UnityEngine.Vector3.forward)) < 0.999f) ? global::UnityEngine.Vector3.forward : global::UnityEngine.Vector3.up).normalized * size;
			global::UnityEngine.Vector3 vector2 = position + vector;
			global::UnityEngine.Vector3 vector3 = position - vector;
			vector = global::UnityEngine.Quaternion.AngleAxis(90f, normal) * vector;
			global::UnityEngine.Vector3 vector4 = position + vector;
			global::UnityEngine.Vector3 vector5 = position - vector;
			global::UnityEngine.Gizmos.matrix = global::UnityEngine.Matrix4x4.identity;
			global::UnityEngine.Gizmos.color = color;
			global::UnityEngine.Gizmos.DrawLine(vector2, vector3);
			global::UnityEngine.Gizmos.DrawLine(vector4, vector5);
			global::UnityEngine.Gizmos.DrawLine(vector2, vector4);
			global::UnityEngine.Gizmos.DrawLine(vector4, vector3);
			global::UnityEngine.Gizmos.DrawLine(vector3, vector5);
			global::UnityEngine.Gizmos.DrawLine(vector5, vector2);
		}

		public static global::UnityEngine.Plane TranslateCustom(this global::UnityEngine.Plane plane, global::UnityEngine.Vector3 translation)
		{
			plane.distance += global::UnityEngine.Vector3.Dot(translation.normalized, plane.normal) * translation.magnitude;
			return plane;
		}

		public static bool IsValid(this global::UnityEngine.Plane plane)
		{
			return plane.normal.sqrMagnitude > 0.5f;
		}

		public static void SetKeywordEnabled(this global::UnityEngine.Material mat, string name, bool enabled)
		{
			if (enabled)
			{
				mat.EnableKeyword(name);
			}
			else
			{
				mat.DisableKeyword(name);
			}
		}

		public static void SetShaderKeywordEnabled(string name, bool enabled)
		{
			if (enabled)
			{
				global::UnityEngine.Shader.EnableKeyword(name);
			}
			else
			{
				global::UnityEngine.Shader.DisableKeyword(name);
			}
		}

		public static global::UnityEngine.Matrix4x4 SampleInMatrix(this global::UnityEngine.Gradient self, int floatPackingPrecision)
		{
			global::UnityEngine.Matrix4x4 result = default(global::UnityEngine.Matrix4x4);
			for (int i = 0; i < 16; i++)
			{
				global::UnityEngine.Color color = self.Evaluate(global::UnityEngine.Mathf.Clamp01((float)i / 15f));
				result[i] = color.PackToFloat(floatPackingPrecision);
			}
			return result;
		}

		public static global::UnityEngine.Color[] SampleInArray(this global::UnityEngine.Gradient self, int samplesCount)
		{
			global::UnityEngine.Color[] array = new global::UnityEngine.Color[samplesCount];
			for (int i = 0; i < samplesCount; i++)
			{
				array[i] = self.Evaluate(global::UnityEngine.Mathf.Clamp01((float)i / (float)(samplesCount - 1)));
			}
			return array;
		}

		private static global::UnityEngine.Vector4 Vector4_Floor(global::UnityEngine.Vector4 vec)
		{
			return new global::UnityEngine.Vector4(global::UnityEngine.Mathf.Floor(vec.x), global::UnityEngine.Mathf.Floor(vec.y), global::UnityEngine.Mathf.Floor(vec.z), global::UnityEngine.Mathf.Floor(vec.w));
		}

		public static float PackToFloat(this global::UnityEngine.Color color, int floatPackingPrecision)
		{
			global::UnityEngine.Vector4 vector = Vector4_Floor(color * (floatPackingPrecision - 1));
			return 0f + vector.x * (float)floatPackingPrecision * (float)floatPackingPrecision * (float)floatPackingPrecision + vector.y * (float)floatPackingPrecision * (float)floatPackingPrecision + vector.z * (float)floatPackingPrecision + vector.w;
		}

		public static global::VLB.Utils.FloatPackingPrecision GetFloatPackingPrecision()
		{
			if (ms_FloatPackingPrecision == global::VLB.Utils.FloatPackingPrecision.Undef)
			{
				ms_FloatPackingPrecision = ((global::UnityEngine.SystemInfo.graphicsShaderLevel >= 35) ? global::VLB.Utils.FloatPackingPrecision.High : global::VLB.Utils.FloatPackingPrecision.Low);
			}
			return ms_FloatPackingPrecision;
		}

		public static void MarkCurrentSceneDirty()
		{
		}

		public static void MarkObjectDirty(global::UnityEngine.Object obj)
		{
		}
	}
}
