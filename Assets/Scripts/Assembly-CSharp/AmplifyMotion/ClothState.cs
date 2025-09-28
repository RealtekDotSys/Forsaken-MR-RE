namespace AmplifyMotion
{
	internal class ClothState : global::AmplifyMotion.MotionState
	{
		private global::UnityEngine.Cloth m_cloth;

		private global::UnityEngine.Renderer m_renderer;

		private global::AmplifyMotion.MotionState.Matrix3x4 m_prevLocalToWorld;

		private global::AmplifyMotion.MotionState.Matrix3x4 m_currLocalToWorld;

		private int m_targetVertexCount;

		private int[] m_targetRemap;

		private global::UnityEngine.Vector3[] m_prevVertices;

		private global::UnityEngine.Vector3[] m_currVertices;

		private global::UnityEngine.Mesh m_clonedMesh;

		private global::AmplifyMotion.MotionState.MaterialDesc[] m_sharedMaterials;

		private bool m_starting;

		private bool m_wasVisible;

		private static global::System.Collections.Generic.HashSet<AmplifyMotionObjectBase> m_uniqueWarnings = new global::System.Collections.Generic.HashSet<AmplifyMotionObjectBase>();

		public ClothState(AmplifyMotionCamera owner, AmplifyMotionObjectBase obj)
			: base(owner, obj)
		{
			m_cloth = m_obj.GetComponent<global::UnityEngine.Cloth>();
		}

		private void IssueError(string message)
		{
			if (!m_uniqueWarnings.Contains(m_obj))
			{
				global::UnityEngine.Debug.LogWarning(message);
				m_uniqueWarnings.Add(m_obj);
			}
			m_error = true;
		}

		internal override void Initialize()
		{
			if (m_cloth.vertices == null)
			{
				IssueError("[AmplifyMotion] Invalid " + m_cloth.GetType().Name + " vertices in object " + m_obj.name + ". Skipping.");
				return;
			}
			global::UnityEngine.Mesh sharedMesh = m_cloth.gameObject.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().sharedMesh;
			if (sharedMesh == null || sharedMesh.vertices == null || sharedMesh.triangles == null)
			{
				IssueError("[AmplifyMotion] Invalid Mesh on Cloth-enabled object " + m_obj.name);
				return;
			}
			base.Initialize();
			m_renderer = m_cloth.gameObject.GetComponent<global::UnityEngine.Renderer>();
			int vertexCount = sharedMesh.vertexCount;
			global::UnityEngine.Vector3[] vertices = sharedMesh.vertices;
			global::UnityEngine.Vector2[] uv = sharedMesh.uv;
			int[] triangles = sharedMesh.triangles;
			m_targetRemap = new int[vertexCount];
			if (m_cloth.vertices.Length == sharedMesh.vertices.Length)
			{
				for (int i = 0; i < vertexCount; i++)
				{
					m_targetRemap[i] = i;
				}
			}
			else
			{
				global::System.Collections.Generic.Dictionary<global::UnityEngine.Vector3, int> dictionary = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Vector3, int>();
				int num = 0;
				for (int j = 0; j < vertexCount; j++)
				{
					if (dictionary.TryGetValue(vertices[j], out var value))
					{
						m_targetRemap[j] = value;
						continue;
					}
					m_targetRemap[j] = num;
					dictionary.Add(vertices[j], num++);
				}
			}
			m_targetVertexCount = vertexCount;
			m_prevVertices = new global::UnityEngine.Vector3[m_targetVertexCount];
			m_currVertices = new global::UnityEngine.Vector3[m_targetVertexCount];
			m_clonedMesh = new global::UnityEngine.Mesh();
			m_clonedMesh.vertices = vertices;
			m_clonedMesh.normals = vertices;
			m_clonedMesh.uv = uv;
			m_clonedMesh.triangles = triangles;
			m_sharedMaterials = ProcessSharedMaterials(m_renderer.sharedMaterials);
			m_wasVisible = false;
		}

		internal override void Shutdown()
		{
			global::UnityEngine.Object.Destroy(m_clonedMesh);
		}

		internal override void UpdateTransform(global::UnityEngine.Rendering.CommandBuffer updateCB, bool starting)
		{
			if (!m_initialized)
			{
				Initialize();
				return;
			}
			if (!starting && m_wasVisible)
			{
				m_prevLocalToWorld = m_currLocalToWorld;
			}
			bool isVisible = m_renderer.isVisible;
			if (!m_error && (isVisible || starting) && !starting && m_wasVisible)
			{
				global::System.Array.Copy(m_currVertices, m_prevVertices, m_targetVertexCount);
			}
			m_currLocalToWorld = global::UnityEngine.Matrix4x4.TRS(m_transform.position, m_transform.rotation, global::UnityEngine.Vector3.one);
			if (starting || !m_wasVisible)
			{
				m_prevLocalToWorld = m_currLocalToWorld;
			}
			m_starting = starting;
			m_wasVisible = isVisible;
		}

		internal override void RenderVectors(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.CommandBuffer renderCB, float scale, global::AmplifyMotion.Quality quality)
		{
			if (!m_initialized || m_error || !m_renderer.isVisible)
			{
				return;
			}
			bool flag = ((int)m_owner.Instance.CullingMask & (1 << m_obj.gameObject.layer)) != 0;
			int num = (flag ? m_owner.Instance.GenerateObjectId(m_obj.gameObject) : 255);
			global::UnityEngine.Vector3[] vertices = m_cloth.vertices;
			for (int i = 0; i < m_targetVertexCount; i++)
			{
				m_currVertices[i] = vertices[m_targetRemap[i]];
			}
			if (m_starting || !m_wasVisible)
			{
				global::System.Array.Copy(m_currVertices, m_prevVertices, m_targetVertexCount);
			}
			m_clonedMesh.vertices = m_currVertices;
			m_clonedMesh.normals = m_prevVertices;
			float x = (flag ? (m_owner.Instance.CameraMotionMult * scale) : 0f);
			float y = (flag ? (m_owner.Instance.ObjectMotionMult * scale) : 0f);
			renderCB.SetGlobalMatrix("_AM_MATRIX_PREV_M", m_prevLocalToWorld);
			renderCB.SetGlobalMatrix("_AM_MATRIX_CURR_M", m_currLocalToWorld);
			renderCB.SetGlobalVector("_AM_MOTION_PARAMS", new global::UnityEngine.Vector4(x, y, (float)num * 0.003921569f, 0f));
			int num2 = ((quality != global::AmplifyMotion.Quality.Mobile) ? 2 : 0);
			for (int j = 0; j < m_sharedMaterials.Length; j++)
			{
				global::AmplifyMotion.MotionState.MaterialDesc materialDesc = m_sharedMaterials[j];
				int shaderPass = num2 + (materialDesc.coverage ? 1 : 0);
				if (materialDesc.coverage)
				{
					global::UnityEngine.Texture mainTexture = materialDesc.material.mainTexture;
					if (mainTexture != null)
					{
						materialDesc.propertyBlock.SetTexture("_MainTex", mainTexture);
					}
					if (materialDesc.cutoff)
					{
						materialDesc.propertyBlock.SetFloat("_Cutoff", materialDesc.material.GetFloat("_Cutoff"));
					}
				}
				renderCB.DrawMesh(m_clonedMesh, m_currLocalToWorld, m_owner.Instance.ClothVectorsMaterial, j, shaderPass, materialDesc.propertyBlock);
			}
		}
	}
}
