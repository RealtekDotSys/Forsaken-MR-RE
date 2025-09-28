namespace RuntimeHandle
{
	public class MeshUtils
	{
		public static global::UnityEngine.Mesh CreateArc(global::UnityEngine.Vector3 p_center, global::UnityEngine.Vector3 p_startPoint, global::UnityEngine.Vector3 p_axis, float p_radius, float p_angle, int p_segmentCount)
		{
			global::UnityEngine.Mesh mesh = new global::UnityEngine.Mesh();
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[p_segmentCount + 2];
			global::UnityEngine.Vector3 vector = (p_startPoint - p_center).normalized * p_radius;
			for (int i = 0; i <= p_segmentCount; i++)
			{
				global::UnityEngine.Vector3 vector2 = global::UnityEngine.Quaternion.AngleAxis((float)i / (float)p_segmentCount * p_angle * 180f / global::System.MathF.PI, p_axis) * vector;
				array[i] = vector2 + p_center;
			}
			array[p_segmentCount + 1] = p_center;
			global::UnityEngine.Vector3[] array2 = new global::UnityEngine.Vector3[array.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = global::UnityEngine.Vector3.up;
			}
			global::UnityEngine.Vector2[] array3 = new global::UnityEngine.Vector2[array.Length];
			for (int k = 0; k <= p_segmentCount; k++)
			{
				float f = (float)k / (float)p_segmentCount * p_angle;
				array3[k] = new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Cos(f) * 0.5f + 0.5f, global::UnityEngine.Mathf.Sin(f) * 0.5f + 0.5f);
			}
			array3[p_segmentCount + 1] = global::UnityEngine.Vector2.one / 2f;
			int[] array4 = new int[p_segmentCount * 3];
			for (int l = 0; l < p_segmentCount; l++)
			{
				int num = l * 3;
				array4[num] = p_segmentCount + 1;
				array4[num + 1] = l;
				array4[num + 2] = l + 1;
			}
			mesh.vertices = array;
			mesh.normals = array2;
			mesh.uv = array3;
			mesh.triangles = array4;
			mesh.RecalculateBounds();
			return mesh;
		}

		public static global::UnityEngine.Mesh CreateArc(float p_radius, float p_angle, int p_segmentCount)
		{
			global::UnityEngine.Mesh mesh = new global::UnityEngine.Mesh();
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[p_segmentCount + 2];
			for (int i = 0; i <= p_segmentCount; i++)
			{
				float f = (float)i / (float)p_segmentCount * p_angle;
				array[i] = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(f) * p_radius, 0f, global::UnityEngine.Mathf.Sin(f) * p_radius);
			}
			array[p_segmentCount + 1] = global::UnityEngine.Vector3.zero;
			global::UnityEngine.Vector3[] array2 = new global::UnityEngine.Vector3[array.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = global::UnityEngine.Vector3.up;
			}
			global::UnityEngine.Vector2[] array3 = new global::UnityEngine.Vector2[array.Length];
			for (int k = 0; k <= p_segmentCount; k++)
			{
				float f2 = (float)k / (float)p_segmentCount * p_angle;
				array3[k] = new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Cos(f2) * 0.5f + 0.5f, global::UnityEngine.Mathf.Sin(f2) * 0.5f + 0.5f);
			}
			array3[p_segmentCount + 1] = global::UnityEngine.Vector2.one / 2f;
			int[] array4 = new int[p_segmentCount * 3];
			for (int l = 0; l < p_segmentCount; l++)
			{
				int num = l * 3;
				array4[num] = p_segmentCount + 1;
				array4[num + 1] = l;
				array4[num + 2] = l + 1;
			}
			mesh.vertices = array;
			mesh.normals = array2;
			mesh.uv = array3;
			mesh.triangles = array4;
			mesh.RecalculateBounds();
			return mesh;
		}

		public static global::UnityEngine.Mesh CreateGrid(float p_width, float p_height, int p_segmentsX = 1, int p_segmentsY = 1)
		{
			global::UnityEngine.Mesh mesh = new global::UnityEngine.Mesh();
			int num = p_segmentsX + 1;
			int num2 = p_segmentsY + 1;
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[num * num2];
			for (int i = 0; i < num2; i++)
			{
				float z = ((float)i / (float)(num2 - 1) - 0.5f) * p_height;
				for (int j = 0; j < num; j++)
				{
					float x = ((float)j / (float)(num - 1) - 0.5f) * p_width;
					array[j + i * num] = new global::UnityEngine.Vector3(x, 0f, z);
				}
			}
			global::UnityEngine.Vector3[] array2 = new global::UnityEngine.Vector3[array.Length];
			for (int k = 0; k < array2.Length; k++)
			{
				array2[k] = global::UnityEngine.Vector3.up;
			}
			global::UnityEngine.Vector2[] array3 = new global::UnityEngine.Vector2[array.Length];
			for (int l = 0; l < num2; l++)
			{
				for (int m = 0; m < num; m++)
				{
					array3[m + l * num] = new global::UnityEngine.Vector2((float)m / (float)(num - 1), (float)l / (float)(num2 - 1));
				}
			}
			int num3 = (num - 1) * (num2 - 1);
			int[] array4 = new int[num3 * 6];
			int num4 = 0;
			for (int n = 0; n < num3; n++)
			{
				int num5 = n % (num - 1) + n / (num2 - 1) * num;
				array4[num4++] = num5 + num;
				array4[num4++] = num5 + 1;
				array4[num4++] = num5;
				array4[num4++] = num5 + num;
				array4[num4++] = num5 + num + 1;
				array4[num4++] = num5 + 1;
			}
			mesh.vertices = array;
			mesh.normals = array2;
			mesh.uv = array3;
			mesh.triangles = array4;
			mesh.RecalculateBounds();
			return mesh;
		}

		public static global::UnityEngine.Mesh CreateBox(float p_width, float p_height, float p_depth)
		{
			global::UnityEngine.Mesh mesh = new global::UnityEngine.Mesh();
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3((0f - p_depth) * 0.5f, (0f - p_width) * 0.5f, p_height * 0.5f);
			global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(p_depth * 0.5f, (0f - p_width) * 0.5f, p_height * 0.5f);
			global::UnityEngine.Vector3 vector3 = new global::UnityEngine.Vector3(p_depth * 0.5f, (0f - p_width) * 0.5f, (0f - p_height) * 0.5f);
			global::UnityEngine.Vector3 vector4 = new global::UnityEngine.Vector3((0f - p_depth) * 0.5f, (0f - p_width) * 0.5f, (0f - p_height) * 0.5f);
			global::UnityEngine.Vector3 vector5 = new global::UnityEngine.Vector3((0f - p_depth) * 0.5f, p_width * 0.5f, p_height * 0.5f);
			global::UnityEngine.Vector3 vector6 = new global::UnityEngine.Vector3(p_depth * 0.5f, p_width * 0.5f, p_height * 0.5f);
			global::UnityEngine.Vector3 vector7 = new global::UnityEngine.Vector3(p_depth * 0.5f, p_width * 0.5f, (0f - p_height) * 0.5f);
			global::UnityEngine.Vector3 vector8 = new global::UnityEngine.Vector3((0f - p_depth) * 0.5f, p_width * 0.5f, (0f - p_height) * 0.5f);
			global::UnityEngine.Vector3[] vertices = new global::UnityEngine.Vector3[24]
			{
				vector, vector2, vector3, vector4, vector8, vector5, vector, vector4, vector5, vector6,
				vector2, vector, vector7, vector8, vector4, vector3, vector6, vector7, vector3, vector2,
				vector8, vector7, vector6, vector5
			};
			global::UnityEngine.Vector3 up = global::UnityEngine.Vector3.up;
			global::UnityEngine.Vector3 down = global::UnityEngine.Vector3.down;
			global::UnityEngine.Vector3 forward = global::UnityEngine.Vector3.forward;
			global::UnityEngine.Vector3 back = global::UnityEngine.Vector3.back;
			global::UnityEngine.Vector3 left = global::UnityEngine.Vector3.left;
			global::UnityEngine.Vector3 right = global::UnityEngine.Vector3.right;
			global::UnityEngine.Vector3[] normals = new global::UnityEngine.Vector3[24]
			{
				down, down, down, down, left, left, left, left, forward, forward,
				forward, forward, back, back, back, back, right, right, right, right,
				up, up, up, up
			};
			global::UnityEngine.Vector2 vector9 = new global::UnityEngine.Vector2(0f, 0f);
			global::UnityEngine.Vector2 vector10 = new global::UnityEngine.Vector2(1f, 0f);
			global::UnityEngine.Vector2 vector11 = new global::UnityEngine.Vector2(0f, 1f);
			global::UnityEngine.Vector2 vector12 = new global::UnityEngine.Vector2(1f, 1f);
			global::UnityEngine.Vector2[] uv = new global::UnityEngine.Vector2[24]
			{
				vector12, vector11, vector9, vector10, vector12, vector11, vector9, vector10, vector12, vector11,
				vector9, vector10, vector12, vector11, vector9, vector10, vector12, vector11, vector9, vector10,
				vector12, vector11, vector9, vector10
			};
			int[] triangles = new int[36]
			{
				3, 1, 0, 3, 2, 1, 7, 5, 4, 7,
				6, 5, 11, 9, 8, 11, 10, 9, 15, 13,
				12, 15, 14, 13, 19, 17, 16, 19, 18, 17,
				23, 21, 20, 23, 22, 21
			};
			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.uv = uv;
			mesh.triangles = triangles;
			mesh.RecalculateBounds();
			return mesh;
		}

		public static global::UnityEngine.Mesh CreateCone(float p_height, float p_bottomRadius, float p_topRadius, int p_sideCount, int p_heightSegmentCount)
		{
			global::UnityEngine.Mesh mesh = new global::UnityEngine.Mesh();
			int num = p_sideCount + 1;
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[num + num + p_sideCount * p_heightSegmentCount * 2 + 2];
			int i = 0;
			float num2 = global::System.MathF.PI * 2f;
			array[i++] = new global::UnityEngine.Vector3(0f, 0f, 0f);
			for (; i <= p_sideCount; i++)
			{
				float f = (float)i / (float)p_sideCount * num2;
				array[i] = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(f) * p_bottomRadius, 0f, global::UnityEngine.Mathf.Sin(f) * p_bottomRadius);
			}
			array[i++] = new global::UnityEngine.Vector3(0f, p_height, 0f);
			for (; i <= p_sideCount * 2 + 1; i++)
			{
				float f2 = (float)(i - p_sideCount - 1) / (float)p_sideCount * num2;
				array[i] = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(f2) * p_topRadius, p_height, global::UnityEngine.Mathf.Sin(f2) * p_topRadius);
			}
			int num3 = 0;
			while (i <= array.Length - 4)
			{
				float f3 = (float)num3 / (float)p_sideCount * num2;
				array[i] = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(f3) * p_topRadius, p_height, global::UnityEngine.Mathf.Sin(f3) * p_topRadius);
				array[i + 1] = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(f3) * p_bottomRadius, 0f, global::UnityEngine.Mathf.Sin(f3) * p_bottomRadius);
				i += 2;
				num3++;
			}
			array[i] = array[p_sideCount * 2 + 2];
			array[i + 1] = array[p_sideCount * 2 + 3];
			global::UnityEngine.Vector3[] array2 = new global::UnityEngine.Vector3[array.Length];
			i = 0;
			while (i <= p_sideCount)
			{
				array2[i++] = global::UnityEngine.Vector3.down;
			}
			while (i <= p_sideCount * 2 + 1)
			{
				array2[i++] = global::UnityEngine.Vector3.up;
			}
			num3 = 0;
			while (i <= array.Length - 4)
			{
				float f4 = (float)num3 / (float)p_sideCount * num2;
				float x = global::UnityEngine.Mathf.Cos(f4);
				float z = global::UnityEngine.Mathf.Sin(f4);
				array2[i] = new global::UnityEngine.Vector3(x, 0f, z);
				array2[i + 1] = array2[i];
				i += 2;
				num3++;
			}
			array2[i] = array2[p_sideCount * 2 + 2];
			array2[i + 1] = array2[p_sideCount * 2 + 3];
			global::UnityEngine.Vector2[] array3 = new global::UnityEngine.Vector2[array.Length];
			int j = 0;
			array3[j++] = new global::UnityEngine.Vector2(0.5f, 0.5f);
			for (; j <= p_sideCount; j++)
			{
				float f5 = (float)j / (float)p_sideCount * num2;
				array3[j] = new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Cos(f5) * 0.5f + 0.5f, global::UnityEngine.Mathf.Sin(f5) * 0.5f + 0.5f);
			}
			array3[j++] = new global::UnityEngine.Vector2(0.5f, 0.5f);
			for (; j <= p_sideCount * 2 + 1; j++)
			{
				float f6 = (float)j / (float)p_sideCount * num2;
				array3[j] = new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Cos(f6) * 0.5f + 0.5f, global::UnityEngine.Mathf.Sin(f6) * 0.5f + 0.5f);
			}
			int num4 = 0;
			while (j <= array3.Length - 4)
			{
				float x2 = (float)num4 / (float)p_sideCount;
				array3[j] = new global::UnityEngine.Vector3(x2, 1f);
				array3[j + 1] = new global::UnityEngine.Vector3(x2, 0f);
				j += 2;
				num4++;
			}
			array3[j] = new global::UnityEngine.Vector2(1f, 1f);
			array3[j + 1] = new global::UnityEngine.Vector2(1f, 0f);
			int num5 = p_sideCount + p_sideCount + p_sideCount * 2;
			int[] array4 = new int[num5 * 3 + 3];
			int num6 = 0;
			int num7 = 0;
			while (num6 < p_sideCount - 1)
			{
				array4[num7] = 0;
				array4[num7 + 1] = num6 + 1;
				array4[num7 + 2] = num6 + 2;
				num6++;
				num7 += 3;
			}
			array4[num7] = 0;
			array4[num7 + 1] = num6 + 1;
			array4[num7 + 2] = 1;
			num6++;
			num7 += 3;
			while (num6 < p_sideCount * 2)
			{
				array4[num7] = num6 + 2;
				array4[num7 + 1] = num6 + 1;
				array4[num7 + 2] = num;
				num6++;
				num7 += 3;
			}
			array4[num7] = num + 1;
			array4[num7 + 1] = num6 + 1;
			array4[num7 + 2] = num;
			num6++;
			num7 += 3;
			num6++;
			while (num6 <= num5)
			{
				array4[num7] = num6 + 2;
				array4[num7 + 1] = num6 + 1;
				array4[num7 + 2] = num6;
				num6++;
				num7 += 3;
				array4[num7] = num6 + 1;
				array4[num7 + 1] = num6 + 2;
				array4[num7 + 2] = num6;
				num6++;
				num7 += 3;
			}
			mesh.vertices = array;
			mesh.normals = array2;
			mesh.uv = array3;
			mesh.triangles = array4;
			mesh.RecalculateBounds();
			return mesh;
		}

		public static global::UnityEngine.Mesh CreateTube(float p_height, int p_sideCount, float p_bottomRadius, float p_bottomThickness, float p_topRadius, float p_topThickness)
		{
			global::UnityEngine.Mesh mesh = new global::UnityEngine.Mesh();
			int num = p_sideCount * 2 + 2;
			int num2 = p_sideCount * 2 + 2;
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[num * 2 + num2 * 2];
			int i = 0;
			float num3 = global::System.MathF.PI * 2f;
			int num4 = 0;
			for (; i < num; i += 2)
			{
				num4 = ((num4 != p_sideCount) ? num4 : 0);
				float f = (float)num4++ / (float)p_sideCount * num3;
				float num5 = global::UnityEngine.Mathf.Cos(f);
				float num6 = global::UnityEngine.Mathf.Sin(f);
				array[i] = new global::UnityEngine.Vector3(num5 * (p_bottomRadius - p_bottomThickness * 0.5f), 0f, num6 * (p_bottomRadius - p_bottomThickness * 0.5f));
				array[i + 1] = new global::UnityEngine.Vector3(num5 * (p_bottomRadius + p_bottomThickness * 0.5f), 0f, num6 * (p_bottomRadius + p_bottomThickness * 0.5f));
			}
			num4 = 0;
			for (; i < num * 2; i += 2)
			{
				num4 = ((num4 != p_sideCount) ? num4 : 0);
				float f2 = (float)num4++ / (float)p_sideCount * num3;
				float num7 = global::UnityEngine.Mathf.Cos(f2);
				float num8 = global::UnityEngine.Mathf.Sin(f2);
				array[i] = new global::UnityEngine.Vector3(num7 * (p_topRadius - p_topThickness * 0.5f), p_height, num8 * (p_topRadius - p_topThickness * 0.5f));
				array[i + 1] = new global::UnityEngine.Vector3(num7 * (p_topRadius + p_topThickness * 0.5f), p_height, num8 * (p_topRadius + p_topThickness * 0.5f));
			}
			num4 = 0;
			for (; i < num * 2 + num2; i += 2)
			{
				num4 = ((num4 != p_sideCount) ? num4 : 0);
				float f3 = (float)num4++ / (float)p_sideCount * num3;
				float num9 = global::UnityEngine.Mathf.Cos(f3);
				float num10 = global::UnityEngine.Mathf.Sin(f3);
				array[i] = new global::UnityEngine.Vector3(num9 * (p_topRadius + p_topThickness * 0.5f), p_height, num10 * (p_topRadius + p_topThickness * 0.5f));
				array[i + 1] = new global::UnityEngine.Vector3(num9 * (p_bottomRadius + p_bottomThickness * 0.5f), 0f, num10 * (p_bottomRadius + p_bottomThickness * 0.5f));
			}
			num4 = 0;
			for (; i < array.Length; i += 2)
			{
				num4 = ((num4 != p_sideCount) ? num4 : 0);
				float f4 = (float)num4++ / (float)p_sideCount * num3;
				float num11 = global::UnityEngine.Mathf.Cos(f4);
				float num12 = global::UnityEngine.Mathf.Sin(f4);
				array[i] = new global::UnityEngine.Vector3(num11 * (p_topRadius - p_topThickness * 0.5f), p_height, num12 * (p_topRadius - p_topThickness * 0.5f));
				array[i + 1] = new global::UnityEngine.Vector3(num11 * (p_bottomRadius - p_bottomThickness * 0.5f), 0f, num12 * (p_bottomRadius - p_bottomThickness * 0.5f));
			}
			global::UnityEngine.Vector3[] array2 = new global::UnityEngine.Vector3[array.Length];
			i = 0;
			while (i < num)
			{
				array2[i++] = global::UnityEngine.Vector3.down;
			}
			while (i < num * 2)
			{
				array2[i++] = global::UnityEngine.Vector3.up;
			}
			num4 = 0;
			for (; i < num * 2 + num2; i += 2)
			{
				num4 = ((num4 != p_sideCount) ? num4 : 0);
				float f5 = (float)num4++ / (float)p_sideCount * num3;
				array2[i] = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(f5), 0f, global::UnityEngine.Mathf.Sin(f5));
				array2[i + 1] = array2[i];
			}
			num4 = 0;
			for (; i < array.Length; i += 2)
			{
				num4 = ((num4 != p_sideCount) ? num4 : 0);
				float f6 = (float)num4++ / (float)p_sideCount * num3;
				array2[i] = -new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(f6), 0f, global::UnityEngine.Mathf.Sin(f6));
				array2[i + 1] = array2[i];
			}
			global::UnityEngine.Vector2[] array3 = new global::UnityEngine.Vector2[array.Length];
			i = 0;
			num4 = 0;
			while (i < num)
			{
				float y = (float)num4++ / (float)p_sideCount;
				array3[i++] = new global::UnityEngine.Vector2(0f, y);
				array3[i++] = new global::UnityEngine.Vector2(1f, y);
			}
			num4 = 0;
			while (i < num * 2)
			{
				float y2 = (float)num4++ / (float)p_sideCount;
				array3[i++] = new global::UnityEngine.Vector2(0f, y2);
				array3[i++] = new global::UnityEngine.Vector2(1f, y2);
			}
			num4 = 0;
			while (i < num * 2 + num2)
			{
				float x = (float)num4++ / (float)p_sideCount;
				array3[i++] = new global::UnityEngine.Vector2(x, 0f);
				array3[i++] = new global::UnityEngine.Vector2(x, 1f);
			}
			num4 = 0;
			while (i < array.Length)
			{
				float x2 = (float)num4++ / (float)p_sideCount;
				array3[i++] = new global::UnityEngine.Vector2(x2, 0f);
				array3[i++] = new global::UnityEngine.Vector2(x2, 1f);
			}
			int[] array4 = new int[p_sideCount * 4 * 2 * 3];
			int num13 = 0;
			for (num4 = 0; num4 < p_sideCount; num4++)
			{
				int num14 = num4 * 2;
				int num15 = num4 * 2 + 2;
				array4[num13++] = num15 + 1;
				array4[num13++] = num15;
				array4[num13++] = num14;
				array4[num13++] = num14 + 1;
				array4[num13++] = num15 + 1;
				array4[num13++] = num14;
			}
			for (; num4 < p_sideCount * 2; num4++)
			{
				int num16 = num4 * 2 + 2;
				int num17 = num4 * 2 + 4;
				array4[num13++] = num16;
				array4[num13++] = num17;
				array4[num13++] = num17 + 1;
				array4[num13++] = num16;
				array4[num13++] = num17 + 1;
				array4[num13++] = num16 + 1;
			}
			for (; num4 < p_sideCount * 3; num4++)
			{
				int num18 = num4 * 2 + 4;
				int num19 = num4 * 2 + 6;
				array4[num13++] = num18;
				array4[num13++] = num19;
				array4[num13++] = num19 + 1;
				array4[num13++] = num18;
				array4[num13++] = num19 + 1;
				array4[num13++] = num18 + 1;
			}
			for (; num4 < p_sideCount * 4; num4++)
			{
				int num20 = num4 * 2 + 6;
				int num21 = num4 * 2 + 8;
				array4[num13++] = num21 + 1;
				array4[num13++] = num21;
				array4[num13++] = num20;
				array4[num13++] = num20 + 1;
				array4[num13++] = num21 + 1;
				array4[num13++] = num20;
			}
			mesh.vertices = array;
			mesh.normals = array2;
			mesh.uv = array3;
			mesh.triangles = array4;
			mesh.RecalculateBounds();
			return mesh;
		}

		public static global::UnityEngine.Mesh CreateTorus(float p_radius, float p_thickness, int p_radiusSegmentCount, int p_sideCount)
		{
			global::UnityEngine.Mesh mesh = new global::UnityEngine.Mesh();
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[(p_radiusSegmentCount + 1) * (p_sideCount + 1)];
			float num = global::System.MathF.PI * 2f;
			for (int i = 0; i <= p_radiusSegmentCount; i++)
			{
				float num2 = (float)((i != p_radiusSegmentCount) ? i : 0) / (float)p_radiusSegmentCount * num;
				global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(num2) * p_radius, 0f, global::UnityEngine.Mathf.Sin(num2) * p_radius);
				for (int j = 0; j <= p_sideCount; j++)
				{
					int num3 = ((j != p_sideCount) ? j : 0);
					global::UnityEngine.Vector3.Cross(vector, global::UnityEngine.Vector3.up);
					float f = (float)num3 / (float)p_sideCount * num;
					global::UnityEngine.Vector3 vector2 = global::UnityEngine.Quaternion.AngleAxis((0f - num2) * 57.29578f, global::UnityEngine.Vector3.up) * new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Sin(f) * p_thickness, global::UnityEngine.Mathf.Cos(f) * p_thickness);
					array[j + i * (p_sideCount + 1)] = vector + vector2;
				}
			}
			global::UnityEngine.Vector3[] array2 = new global::UnityEngine.Vector3[array.Length];
			for (int k = 0; k <= p_radiusSegmentCount; k++)
			{
				float f2 = (float)((k != p_radiusSegmentCount) ? k : 0) / (float)p_radiusSegmentCount * num;
				global::UnityEngine.Vector3 vector3 = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Cos(f2) * p_radius, 0f, global::UnityEngine.Mathf.Sin(f2) * p_radius);
				for (int l = 0; l <= p_sideCount; l++)
				{
					array2[l + k * (p_sideCount + 1)] = (array[l + k * (p_sideCount + 1)] - vector3).normalized;
				}
			}
			global::UnityEngine.Vector2[] array3 = new global::UnityEngine.Vector2[array.Length];
			for (int m = 0; m <= p_radiusSegmentCount; m++)
			{
				for (int n = 0; n <= p_sideCount; n++)
				{
					array3[n + m * (p_sideCount + 1)] = new global::UnityEngine.Vector2((float)m / (float)p_radiusSegmentCount, (float)n / (float)p_sideCount);
				}
			}
			int[] array4 = new int[array.Length * 2 * 3];
			int num4 = 0;
			for (int num5 = 0; num5 <= p_radiusSegmentCount; num5++)
			{
				for (int num6 = 0; num6 <= p_sideCount - 1; num6++)
				{
					int num7 = num6 + num5 * (p_sideCount + 1);
					int num8 = num6 + ((num5 < p_radiusSegmentCount) ? ((num5 + 1) * (p_sideCount + 1)) : 0);
					if (num4 < array4.Length - 6)
					{
						array4[num4++] = num7;
						array4[num4++] = num8;
						array4[num4++] = num8 + 1;
						array4[num4++] = num7;
						array4[num4++] = num8 + 1;
						array4[num4++] = num7 + 1;
					}
				}
			}
			mesh.vertices = array;
			mesh.normals = array2;
			mesh.uv = array3;
			mesh.triangles = array4;
			mesh.RecalculateBounds();
			return mesh;
		}

		public static global::UnityEngine.Mesh CreateSphere(float p_radius, int p_longitudeCount, int p_lattitudeCount)
		{
			global::UnityEngine.Mesh mesh = new global::UnityEngine.Mesh();
			mesh.Clear();
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[(p_longitudeCount + 1) * p_lattitudeCount + 2];
			float num = global::System.MathF.PI;
			float num2 = num * 2f;
			array[0] = global::UnityEngine.Vector3.up * p_radius;
			for (int i = 0; i < p_lattitudeCount; i++)
			{
				float f = num * (float)(i + 1) / (float)(p_lattitudeCount + 1);
				float num3 = global::UnityEngine.Mathf.Sin(f);
				float y = global::UnityEngine.Mathf.Cos(f);
				for (int j = 0; j <= p_longitudeCount; j++)
				{
					float f2 = num2 * (float)((j != p_longitudeCount) ? j : 0) / (float)p_longitudeCount;
					float num4 = global::UnityEngine.Mathf.Sin(f2);
					float num5 = global::UnityEngine.Mathf.Cos(f2);
					array[j + i * (p_longitudeCount + 1) + 1] = new global::UnityEngine.Vector3(num3 * num5, y, num3 * num4) * p_radius;
				}
			}
			array[^1] = global::UnityEngine.Vector3.up * (0f - p_radius);
			global::UnityEngine.Vector3[] array2 = new global::UnityEngine.Vector3[array.Length];
			for (int k = 0; k < array.Length; k++)
			{
				array2[k] = array[k].normalized;
			}
			global::UnityEngine.Vector2[] array3 = new global::UnityEngine.Vector2[array.Length];
			array3[0] = global::UnityEngine.Vector2.up;
			array3[^1] = global::UnityEngine.Vector2.zero;
			for (int l = 0; l < p_lattitudeCount; l++)
			{
				for (int m = 0; m <= p_longitudeCount; m++)
				{
					array3[m + l * (p_longitudeCount + 1) + 1] = new global::UnityEngine.Vector2((float)m / (float)p_longitudeCount, 1f - (float)(l + 1) / (float)(p_lattitudeCount + 1));
				}
			}
			int[] array4 = new int[array.Length * 2 * 3];
			int num6 = 0;
			for (int n = 0; n < p_longitudeCount; n++)
			{
				array4[num6++] = n + 2;
				array4[num6++] = n + 1;
				array4[num6++] = 0;
			}
			for (int num7 = 0; num7 < p_lattitudeCount - 1; num7++)
			{
				for (int num8 = 0; num8 < p_longitudeCount; num8++)
				{
					int num9 = num8 + num7 * (p_longitudeCount + 1) + 1;
					int num10 = num9 + p_longitudeCount + 1;
					array4[num6++] = num9;
					array4[num6++] = num9 + 1;
					array4[num6++] = num10 + 1;
					array4[num6++] = num9;
					array4[num6++] = num10 + 1;
					array4[num6++] = num10;
				}
			}
			for (int num11 = 0; num11 < p_longitudeCount; num11++)
			{
				array4[num6++] = array.Length - 1;
				array4[num6++] = array.Length - (num11 + 2) - 1;
				array4[num6++] = array.Length - (num11 + 1) - 1;
			}
			mesh.vertices = array;
			mesh.normals = array2;
			mesh.uv = array3;
			mesh.triangles = array4;
			mesh.RecalculateBounds();
			return mesh;
		}
	}
}
