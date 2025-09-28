namespace AmplifyMotion
{
	internal class ParticleState : global::AmplifyMotion.MotionState
	{
		protected class Particle
		{
			public int refCount;

			public global::AmplifyMotion.MotionState.Matrix3x4 prevLocalToWorld;

			public global::AmplifyMotion.MotionState.Matrix3x4 currLocalToWorld;
		}

		public global::UnityEngine.ParticleSystem m_particleSystem;

		public global::UnityEngine.ParticleSystemRenderer m_renderer;

		private global::UnityEngine.Mesh m_mesh;

		private global::UnityEngine.ParticleSystem.RotationOverLifetimeModule rotationOverLifetime;

		private global::UnityEngine.ParticleSystem.RotationBySpeedModule rotationBySpeed;

		private global::UnityEngine.ParticleSystem.Particle[] m_particles;

		private global::System.Collections.Generic.Dictionary<uint, global::AmplifyMotion.ParticleState.Particle> m_particleDict;

		private global::System.Collections.Generic.List<uint> m_listToRemove;

		private global::System.Collections.Generic.Stack<global::AmplifyMotion.ParticleState.Particle> m_particleStack;

		private int m_capacity;

		private global::AmplifyMotion.MotionState.MaterialDesc[] m_sharedMaterials;

		private bool m_moved;

		private bool m_wasVisible;

		private static global::System.Collections.Generic.HashSet<AmplifyMotionObjectBase> m_uniqueWarnings = new global::System.Collections.Generic.HashSet<AmplifyMotionObjectBase>();

		public ParticleState(AmplifyMotionCamera owner, AmplifyMotionObjectBase obj)
			: base(owner, obj)
		{
			m_particleSystem = m_obj.GetComponent<global::UnityEngine.ParticleSystem>();
			m_renderer = m_particleSystem.GetComponent<global::UnityEngine.ParticleSystemRenderer>();
			rotationOverLifetime = m_particleSystem.rotationOverLifetime;
			rotationBySpeed = m_particleSystem.rotationBySpeed;
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

		private global::UnityEngine.Mesh CreateBillboardMesh()
		{
			int[] triangles = new int[6] { 0, 1, 2, 2, 3, 0 };
			global::UnityEngine.Vector3[] vertices = new global::UnityEngine.Vector3[4]
			{
				new global::UnityEngine.Vector3(-0.5f, -0.5f, 0f),
				new global::UnityEngine.Vector3(0.5f, -0.5f, 0f),
				new global::UnityEngine.Vector3(0.5f, 0.5f, 0f),
				new global::UnityEngine.Vector3(-0.5f, 0.5f, 0f)
			};
			global::UnityEngine.Vector2[] uv = new global::UnityEngine.Vector2[4]
			{
				new global::UnityEngine.Vector2(0f, 0f),
				new global::UnityEngine.Vector2(1f, 0f),
				new global::UnityEngine.Vector2(1f, 1f),
				new global::UnityEngine.Vector2(0f, 1f)
			};
			return new global::UnityEngine.Mesh
			{
				vertices = vertices,
				uv = uv,
				triangles = triangles
			};
		}

		private global::UnityEngine.Mesh CreateStretchedBillboardMesh()
		{
			int[] triangles = new int[6] { 0, 1, 2, 2, 3, 0 };
			global::UnityEngine.Vector3[] vertices = new global::UnityEngine.Vector3[4]
			{
				new global::UnityEngine.Vector3(0f, -0.5f, -1f),
				new global::UnityEngine.Vector3(0f, -0.5f, 0f),
				new global::UnityEngine.Vector3(0f, 0.5f, 0f),
				new global::UnityEngine.Vector3(0f, 0.5f, -1f)
			};
			global::UnityEngine.Vector2[] uv = new global::UnityEngine.Vector2[4]
			{
				new global::UnityEngine.Vector2(1f, 1f),
				new global::UnityEngine.Vector2(0f, 1f),
				new global::UnityEngine.Vector2(0f, 0f),
				new global::UnityEngine.Vector2(1f, 0f)
			};
			return new global::UnityEngine.Mesh
			{
				vertices = vertices,
				uv = uv,
				triangles = triangles
			};
		}

		internal override void Initialize()
		{
			if (m_renderer == null)
			{
				IssueError("[AmplifyMotion] Missing/Invalid Particle Renderer in object " + m_obj.name + ". Skipping.");
				return;
			}
			base.Initialize();
			if (m_renderer.renderMode == global::UnityEngine.ParticleSystemRenderMode.Mesh)
			{
				m_mesh = m_renderer.mesh;
			}
			else if (m_renderer.renderMode == global::UnityEngine.ParticleSystemRenderMode.Stretch)
			{
				m_mesh = CreateStretchedBillboardMesh();
			}
			else
			{
				m_mesh = CreateBillboardMesh();
			}
			m_sharedMaterials = ProcessSharedMaterials(m_renderer.sharedMaterials);
			m_capacity = m_particleSystem.main.maxParticles;
			m_particleDict = new global::System.Collections.Generic.Dictionary<uint, global::AmplifyMotion.ParticleState.Particle>(m_capacity);
			m_particles = new global::UnityEngine.ParticleSystem.Particle[m_capacity];
			m_listToRemove = new global::System.Collections.Generic.List<uint>(m_capacity);
			m_particleStack = new global::System.Collections.Generic.Stack<global::AmplifyMotion.ParticleState.Particle>(m_capacity);
			for (int i = 0; i < m_capacity; i++)
			{
				m_particleStack.Push(new global::AmplifyMotion.ParticleState.Particle());
			}
			m_wasVisible = false;
		}

		private void RemoveDeadParticles()
		{
			m_listToRemove.Clear();
			global::System.Collections.Generic.Dictionary<uint, global::AmplifyMotion.ParticleState.Particle>.Enumerator enumerator = m_particleDict.GetEnumerator();
			while (enumerator.MoveNext())
			{
				global::System.Collections.Generic.KeyValuePair<uint, global::AmplifyMotion.ParticleState.Particle> current = enumerator.Current;
				if (current.Value.refCount <= 0)
				{
					m_particleStack.Push(current.Value);
					if (!m_listToRemove.Contains(current.Key))
					{
						m_listToRemove.Add(current.Key);
					}
				}
				else
				{
					current.Value.refCount = 0;
				}
			}
			for (int i = 0; i < m_listToRemove.Count; i++)
			{
				m_particleDict.Remove(m_listToRemove[i]);
			}
		}

		internal override void UpdateTransform(global::UnityEngine.Rendering.CommandBuffer updateCB, bool starting)
		{
			int maxParticles = m_particleSystem.main.maxParticles;
			if (!m_initialized || m_capacity != maxParticles)
			{
				Initialize();
				return;
			}
			if (!starting && m_wasVisible)
			{
				global::System.Collections.Generic.Dictionary<uint, global::AmplifyMotion.ParticleState.Particle>.Enumerator enumerator = m_particleDict.GetEnumerator();
				while (enumerator.MoveNext())
				{
					global::AmplifyMotion.ParticleState.Particle value = enumerator.Current.Value;
					value.prevLocalToWorld = value.currLocalToWorld;
				}
			}
			m_moved = true;
			int particles = m_particleSystem.GetParticles(m_particles);
			global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.Matrix4x4.TRS(m_transform.position, m_transform.rotation, global::UnityEngine.Vector3.one);
			bool flag = (rotationOverLifetime.enabled && rotationOverLifetime.separateAxes) || (rotationBySpeed.enabled && rotationBySpeed.separateAxes);
			for (int i = 0; i < particles; i++)
			{
				uint randomSeed = m_particles[i].randomSeed;
				bool flag2 = false;
				if (!m_particleDict.TryGetValue(randomSeed, out var value2) && m_particleStack.Count > 0)
				{
					value2 = (m_particleDict[randomSeed] = m_particleStack.Pop());
					flag2 = true;
				}
				if (value2 == null)
				{
					continue;
				}
				float currentSize = m_particles[i].GetCurrentSize(m_particleSystem);
				global::UnityEngine.Vector3 s = new global::UnityEngine.Vector3(currentSize, currentSize, currentSize);
				global::UnityEngine.Matrix4x4 matrix4x3;
				if (m_renderer.renderMode == global::UnityEngine.ParticleSystemRenderMode.Mesh)
				{
					global::UnityEngine.Matrix4x4 matrix4x2 = global::UnityEngine.Matrix4x4.TRS(q: (!flag) ? global::UnityEngine.Quaternion.AngleAxis(m_particles[i].rotation, m_particles[i].axisOfRotation) : global::UnityEngine.Quaternion.Euler(m_particles[i].rotation3D), pos: m_particles[i].position, s: s);
					matrix4x3 = ((m_particleSystem.main.simulationSpace != global::UnityEngine.ParticleSystemSimulationSpace.World) ? (matrix4x * matrix4x2) : matrix4x2);
				}
				else if (m_renderer.renderMode == global::UnityEngine.ParticleSystemRenderMode.Billboard)
				{
					if (m_particleSystem.main.simulationSpace == global::UnityEngine.ParticleSystemSimulationSpace.Local)
					{
						m_particles[i].position = matrix4x.MultiplyPoint(m_particles[i].position);
					}
					global::UnityEngine.Quaternion quaternion = ((!flag) ? global::UnityEngine.Quaternion.AngleAxis(m_particles[i].rotation, global::UnityEngine.Vector3.back) : global::UnityEngine.Quaternion.Euler(0f - m_particles[i].rotation3D.x, 0f - m_particles[i].rotation3D.y, m_particles[i].rotation3D.z));
					matrix4x3 = global::UnityEngine.Matrix4x4.TRS(m_particles[i].position, m_owner.Transform.rotation * quaternion, s);
				}
				else
				{
					matrix4x3 = global::UnityEngine.Matrix4x4.identity;
				}
				value2.refCount = 1;
				value2.currLocalToWorld = matrix4x3;
				if (flag2)
				{
					value2.prevLocalToWorld = value2.currLocalToWorld;
				}
			}
			if (starting || !m_wasVisible)
			{
				global::System.Collections.Generic.Dictionary<uint, global::AmplifyMotion.ParticleState.Particle>.Enumerator enumerator2 = m_particleDict.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					global::AmplifyMotion.ParticleState.Particle value3 = enumerator2.Current.Value;
					value3.prevLocalToWorld = value3.currLocalToWorld;
				}
			}
			RemoveDeadParticles();
			m_wasVisible = m_renderer.isVisible;
		}

		internal override void RenderVectors(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.CommandBuffer renderCB, float scale, global::AmplifyMotion.Quality quality)
		{
			if (!m_initialized || m_error || !m_renderer.isVisible)
			{
				return;
			}
			bool flag = ((int)m_owner.Instance.CullingMask & (1 << m_obj.gameObject.layer)) != 0;
			if (flag && (!flag || !m_moved))
			{
				return;
			}
			int num = (flag ? m_owner.Instance.GenerateObjectId(m_obj.gameObject) : 255);
			renderCB.SetGlobalFloat("_AM_OBJECT_ID", (float)num * 0.003921569f);
			renderCB.SetGlobalFloat("_AM_MOTION_SCALE", flag ? scale : 0f);
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
				global::System.Collections.Generic.Dictionary<uint, global::AmplifyMotion.ParticleState.Particle>.Enumerator enumerator = m_particleDict.GetEnumerator();
				while (enumerator.MoveNext())
				{
					global::System.Collections.Generic.KeyValuePair<uint, global::AmplifyMotion.ParticleState.Particle> current = enumerator.Current;
					global::UnityEngine.Matrix4x4 value = m_owner.PrevViewProjMatrixRT * (global::UnityEngine.Matrix4x4)current.Value.prevLocalToWorld;
					renderCB.SetGlobalMatrix("_AM_MATRIX_PREV_MVP", value);
					renderCB.DrawMesh(m_mesh, current.Value.currLocalToWorld, m_owner.Instance.SolidVectorsMaterial, i, shaderPass, materialDesc.propertyBlock);
				}
			}
		}
	}
}
