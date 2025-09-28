namespace VLB
{
	public static class GlobalMesh
	{
		private static global::UnityEngine.Mesh ms_Mesh;

		private static bool ms_DoubleSided;

		public static global::UnityEngine.Mesh Get()
		{
			bool useSinglePassShader = global::VLB.Config.Instance.useSinglePassShader;
			if (ms_Mesh == null || ms_DoubleSided != useSinglePassShader)
			{
				Destroy();
				ms_Mesh = global::VLB.MeshGenerator.GenerateConeZ_Radius(1f, 1f, 1f, global::VLB.Config.Instance.sharedMeshSides, global::VLB.Config.Instance.sharedMeshSegments, cap: true, useSinglePassShader);
				ms_Mesh.hideFlags = global::VLB.Consts.ProceduralObjectsHideFlags;
				ms_DoubleSided = useSinglePassShader;
			}
			return ms_Mesh;
		}

		public static void Destroy()
		{
			if (ms_Mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(ms_Mesh);
				ms_Mesh = null;
			}
		}
	}
}
