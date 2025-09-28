namespace DigitalRuby.ThunderAndLightning
{
	public class MeshHelper
	{
		private global::UnityEngine.Mesh mesh;

		private int[] triangles;

		private global::UnityEngine.Vector3[] vertices;

		private global::UnityEngine.Vector3[] normals;

		private float[] normalizedAreaWeights;

		public global::UnityEngine.Mesh Mesh => mesh;

		public int[] Triangles => triangles;

		public global::UnityEngine.Vector3[] Vertices => vertices;

		public global::UnityEngine.Vector3[] Normals => normals;

		public MeshHelper(global::UnityEngine.Mesh mesh)
		{
			this.mesh = mesh;
			triangles = mesh.triangles;
			vertices = mesh.vertices;
			normals = mesh.normals;
			CalculateNormalizedAreaWeights();
		}

		public void GenerateRandomPoint(ref global::UnityEngine.RaycastHit hit, out int triangleIndex)
		{
			triangleIndex = SelectRandomTriangle();
			GetRaycastFromTriangleIndex(triangleIndex, ref hit);
		}

		public void GetRaycastFromTriangleIndex(int triangleIndex, ref global::UnityEngine.RaycastHit hit)
		{
			global::UnityEngine.Vector3 barycentricCoordinate = GenerateRandomBarycentricCoordinates();
			global::UnityEngine.Vector3 vector = vertices[triangles[triangleIndex]];
			global::UnityEngine.Vector3 vector2 = vertices[triangles[triangleIndex + 1]];
			global::UnityEngine.Vector3 vector3 = vertices[triangles[triangleIndex + 2]];
			hit.barycentricCoordinate = barycentricCoordinate;
			hit.point = vector * barycentricCoordinate.x + vector2 * barycentricCoordinate.y + vector3 * barycentricCoordinate.z;
			if (normals == null)
			{
				hit.normal = global::UnityEngine.Vector3.Cross(vector3 - vector2, vector - vector2).normalized;
				return;
			}
			vector = normals[triangles[triangleIndex]];
			vector2 = normals[triangles[triangleIndex + 1]];
			vector3 = normals[triangles[triangleIndex + 2]];
			hit.normal = vector * barycentricCoordinate.x + vector2 * barycentricCoordinate.y + vector3 * barycentricCoordinate.z;
		}

		private float[] CalculateSurfaceAreas(out float totalSurfaceArea)
		{
			int num = 0;
			totalSurfaceArea = 0f;
			float[] array = new float[triangles.Length / 3];
			for (int i = 0; i < triangles.Length; i += 3)
			{
				global::UnityEngine.Vector3 vector = vertices[triangles[i]];
				global::UnityEngine.Vector3 vector2 = vertices[triangles[i + 1]];
				global::UnityEngine.Vector3 vector3 = vertices[triangles[i + 2]];
				float sqrMagnitude = (vector - vector2).sqrMagnitude;
				float sqrMagnitude2 = (vector - vector3).sqrMagnitude;
				float sqrMagnitude3 = (vector2 - vector3).sqrMagnitude;
				float num2 = global::DigitalRuby.ThunderAndLightning.PathGenerator.SquareRoot((2f * sqrMagnitude * sqrMagnitude2 + 2f * sqrMagnitude2 * sqrMagnitude3 + 2f * sqrMagnitude3 * sqrMagnitude - sqrMagnitude * sqrMagnitude - sqrMagnitude2 * sqrMagnitude2 - sqrMagnitude3 * sqrMagnitude3) / 16f);
				array[num++] = num2;
				totalSurfaceArea += num2;
			}
			return array;
		}

		private void CalculateNormalizedAreaWeights()
		{
			normalizedAreaWeights = CalculateSurfaceAreas(out var totalSurfaceArea);
			if (normalizedAreaWeights.Length != 0)
			{
				float num = 0f;
				for (int i = 0; i < normalizedAreaWeights.Length; i++)
				{
					float num2 = normalizedAreaWeights[i] / totalSurfaceArea;
					normalizedAreaWeights[i] = num;
					num += num2;
				}
			}
		}

		private int SelectRandomTriangle()
		{
			float value = global::UnityEngine.Random.value;
			int num = 0;
			int num2 = normalizedAreaWeights.Length - 1;
			while (num < num2)
			{
				int num3 = (num + num2) / 2;
				if (normalizedAreaWeights[num3] < value)
				{
					num = num3 + 1;
				}
				else
				{
					num2 = num3;
				}
			}
			return num * 3;
		}

		private global::UnityEngine.Vector3 GenerateRandomBarycentricCoordinates()
		{
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(global::UnityEngine.Mathf.Epsilon, 1f), global::UnityEngine.Random.Range(global::UnityEngine.Mathf.Epsilon, 1f), global::UnityEngine.Random.Range(global::UnityEngine.Mathf.Epsilon, 1f));
			return vector / (vector.x + vector.y + vector.z);
		}
	}
}
