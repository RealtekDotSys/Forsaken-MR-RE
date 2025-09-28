namespace AmplifyMotion
{
	internal class SolidState : global::AmplifyMotion.MotionState
	{
		private global::UnityEngine.MeshRenderer m_meshRenderer;

		private global::AmplifyMotion.MotionState.Matrix3x4 m_prevLocalToWorld;

		private global::AmplifyMotion.MotionState.Matrix3x4 m_currLocalToWorld;

		private global::UnityEngine.Mesh m_mesh;

		private global::AmplifyMotion.MotionState.MaterialDesc[] m_sharedMaterials;

		public bool m_moved;

		private bool m_wasVisible;

		private static global::System.Collections.Generic.HashSet<AmplifyMotionObjectBase> m_uniqueWarnings = new global::System.Collections.Generic.HashSet<AmplifyMotionObjectBase>();

		public SolidState(AmplifyMotionCamera owner, AmplifyMotionObjectBase obj)
			: base(owner, obj)
		{
			m_meshRenderer = m_obj.GetComponent<global::UnityEngine.MeshRenderer>();
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
			global::UnityEngine.MeshFilter component = m_obj.GetComponent<global::UnityEngine.MeshFilter>();
			if (component == null || component.sharedMesh == null)
			{
				IssueError("[AmplifyMotion] Invalid MeshFilter/Mesh in object " + m_obj.name + ". Skipping.");
				return;
			}
			base.Initialize();
			m_mesh = component.sharedMesh;
			m_sharedMaterials = ProcessSharedMaterials(m_meshRenderer.sharedMaterials);
			m_wasVisible = false;
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
			m_currLocalToWorld = m_transform.localToWorldMatrix;
			m_moved = true;
			if (!m_owner.Overlay)
			{
				m_moved = starting || global::AmplifyMotion.MotionState.MatrixChanged(m_currLocalToWorld, m_prevLocalToWorld);
			}
			if (starting || !m_wasVisible)
			{
				m_prevLocalToWorld = m_currLocalToWorld;
			}
			m_wasVisible = m_meshRenderer.isVisible;
		}

		internal override void RenderVectors(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.CommandBuffer renderCB, float scale, global::AmplifyMotion.Quality quality)
		{
			if (!m_initialized || m_error || !m_meshRenderer.isVisible)
			{
				return;
			}
			bool flag = ((int)m_owner.Instance.CullingMask & (1 << m_obj.gameObject.layer)) != 0;
			if (flag && (!flag || !m_moved))
			{
				return;
			}
			int num = (flag ? m_owner.Instance.GenerateObjectId(m_obj.gameObject) : 255);
			float x = (flag ? (m_owner.Instance.CameraMotionMult * scale) : 0f);
			float y = (flag ? (m_owner.Instance.ObjectMotionMult * scale) : 0f);
			renderCB.SetGlobalMatrix("_AM_MATRIX_PREV_M", m_prevLocalToWorld);
			renderCB.SetGlobalMatrix("_AM_MATRIX_CURR_M", m_currLocalToWorld);
			renderCB.SetGlobalVector("_AM_MOTION_PARAMS", new global::UnityEngine.Vector4(x, y, (float)num * 0.003921569f, 0f));
			int num2 = ((quality != global::AmplifyMotion.Quality.Mobile) ? 2 : 0);
			for (int i = 0; i < m_sharedMaterials.Length; i++)
			{
				global::AmplifyMotion.MotionState.MaterialDesc materialDesc = m_sharedMaterials[i];
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
				renderCB.DrawMesh(m_mesh, m_transform.localToWorldMatrix, m_owner.Instance.SolidVectorsMaterial, i, shaderPass, materialDesc.propertyBlock);
			}
		}
	}
}
