namespace AmplifyMotion
{
	internal class SkinnedState : global::AmplifyMotion.MotionState
	{
		private global::UnityEngine.SkinnedMeshRenderer m_renderer;

		private int m_boneCount;

		private global::UnityEngine.Transform[] m_boneTransforms;

		private global::AmplifyMotion.MotionState.Matrix3x4[] m_bones;

		private int m_weightCount;

		private int[] m_boneIndices;

		private float[] m_boneWeights;

		private int m_vertexCount;

		private global::UnityEngine.Vector4[] m_baseVertices;

		private global::UnityEngine.Vector3[] m_prevVertices;

		private global::UnityEngine.Vector3[] m_currVertices;

		private int m_gpuBoneTexWidth;

		private int m_gpuBoneTexHeight;

		private int m_gpuVertexTexWidth;

		private int m_gpuVertexTexHeight;

		private global::UnityEngine.Material m_gpuSkinDeformMat;

		private global::UnityEngine.Color[] m_gpuBoneData;

		private global::UnityEngine.Texture2D m_gpuBones;

		private global::UnityEngine.Texture2D m_gpuBoneIndices;

		private global::UnityEngine.Texture2D[] m_gpuBaseVertices;

		private global::UnityEngine.RenderTexture m_gpuPrevVertices;

		private global::UnityEngine.RenderTexture m_gpuCurrVertices;

		private global::UnityEngine.Mesh m_clonedMesh;

		private global::AmplifyMotion.MotionState.Matrix3x4 m_worldToLocalMatrix;

		private global::AmplifyMotion.MotionState.Matrix3x4 m_prevLocalToWorld;

		private global::AmplifyMotion.MotionState.Matrix3x4 m_currLocalToWorld;

		private global::AmplifyMotion.MotionState.MaterialDesc[] m_sharedMaterials;

		private global::System.Threading.ManualResetEvent m_asyncUpdateSignal;

		private bool m_asyncUpdateTriggered;

		private bool m_starting;

		private bool m_wasVisible;

		private bool m_useFallback;

		private bool m_useGPU;

		private static global::System.Collections.Generic.HashSet<AmplifyMotionObjectBase> m_uniqueWarnings = new global::System.Collections.Generic.HashSet<AmplifyMotionObjectBase>();

		public SkinnedState(AmplifyMotionCamera owner, AmplifyMotionObjectBase obj)
			: base(owner, obj)
		{
			m_renderer = m_obj.GetComponent<global::UnityEngine.SkinnedMeshRenderer>();
		}

		private void IssueWarning(string message)
		{
			if (!m_uniqueWarnings.Contains(m_obj))
			{
				global::UnityEngine.Debug.LogWarning(message);
				m_uniqueWarnings.Add(m_obj);
			}
		}

		private void IssueError(string message)
		{
			IssueWarning(message);
			m_error = true;
		}

		internal override void Initialize()
		{
			if (!m_renderer.sharedMesh.isReadable)
			{
				IssueError("[AmplifyMotion] Read/Write Import Setting disabled in object " + m_obj.name + ". Skipping.");
				return;
			}
			global::UnityEngine.Transform[] bones = m_renderer.bones;
			m_useFallback = bones == null || bones.Length == 0;
			if (!m_useFallback)
			{
				m_useGPU = m_owner.Instance.CanUseGPU;
			}
			base.Initialize();
			m_vertexCount = m_renderer.sharedMesh.vertexCount;
			m_prevVertices = new global::UnityEngine.Vector3[m_vertexCount];
			m_currVertices = new global::UnityEngine.Vector3[m_vertexCount];
			m_clonedMesh = new global::UnityEngine.Mesh();
			if (!m_useFallback)
			{
				if (m_renderer.quality == global::UnityEngine.SkinQuality.Auto)
				{
					m_weightCount = (int)global::UnityEngine.QualitySettings.skinWeights;
				}
				else
				{
					m_weightCount = (int)m_renderer.quality;
				}
				m_boneTransforms = m_renderer.bones;
				m_boneCount = m_renderer.bones.Length;
				m_bones = new global::AmplifyMotion.MotionState.Matrix3x4[m_boneCount];
				global::UnityEngine.Vector4[] baseVertices = new global::UnityEngine.Vector4[m_vertexCount * m_weightCount];
				int[] boneIndices = new int[m_vertexCount * m_weightCount];
				float[] boneWeights = ((m_weightCount > 1) ? new float[m_vertexCount * m_weightCount] : null);
				if (m_weightCount == 1)
				{
					InitializeBone1(baseVertices, boneIndices);
				}
				else if (m_weightCount == 2)
				{
					InitializeBone2(baseVertices, boneIndices, boneWeights);
				}
				else
				{
					InitializeBone4(baseVertices, boneIndices, boneWeights);
				}
				m_baseVertices = baseVertices;
				m_boneIndices = boneIndices;
				m_boneWeights = boneWeights;
				global::UnityEngine.Mesh sharedMesh = m_renderer.sharedMesh;
				m_clonedMesh.vertices = sharedMesh.vertices;
				m_clonedMesh.normals = sharedMesh.vertices;
				m_clonedMesh.uv = sharedMesh.uv;
				m_clonedMesh.subMeshCount = sharedMesh.subMeshCount;
				for (int i = 0; i < sharedMesh.subMeshCount; i++)
				{
					m_clonedMesh.SetTriangles(sharedMesh.GetTriangles(i), i);
				}
				if (m_useGPU)
				{
					if (!InitializeGPUSkinDeform())
					{
						global::UnityEngine.Debug.LogWarning("[AmplifyMotion] Failed initializing GPU skin deform for object " + m_obj.name + ". Falling back to CPU path.");
						m_useGPU = false;
					}
					else
					{
						m_boneIndices = null;
						m_boneWeights = null;
						m_baseVertices = null;
						m_prevVertices = null;
						m_currVertices = null;
					}
				}
				if (!m_useGPU)
				{
					m_asyncUpdateSignal = new global::System.Threading.ManualResetEvent(initialState: false);
					m_asyncUpdateTriggered = false;
				}
			}
			m_sharedMaterials = ProcessSharedMaterials(m_renderer.sharedMaterials);
			m_wasVisible = false;
		}

		internal override void Shutdown()
		{
			if (!m_useFallback && !m_useGPU)
			{
				WaitForAsyncUpdate();
			}
			if (m_useGPU)
			{
				ShutdownGPUSkinDeform();
			}
			if (m_clonedMesh != null)
			{
				global::UnityEngine.Object.Destroy(m_clonedMesh);
				m_clonedMesh = null;
			}
			m_boneTransforms = null;
			m_bones = null;
			m_boneIndices = null;
			m_boneWeights = null;
			m_baseVertices = null;
			m_prevVertices = null;
			m_currVertices = null;
			m_sharedMaterials = null;
		}

		private bool InitializeGPUSkinDeform()
		{
			bool result = true;
			try
			{
				m_gpuBoneTexWidth = m_boneCount;
				m_gpuBoneTexHeight = 3;
				m_gpuVertexTexWidth = global::UnityEngine.Mathf.CeilToInt(global::UnityEngine.Mathf.Sqrt(m_vertexCount));
				m_gpuVertexTexHeight = global::UnityEngine.Mathf.CeilToInt((float)m_vertexCount / (float)m_gpuVertexTexWidth);
				m_gpuSkinDeformMat = new global::UnityEngine.Material(global::UnityEngine.Shader.Find("Hidden/Amplify Motion/GPUSkinDeform"))
				{
					hideFlags = global::UnityEngine.HideFlags.DontSave
				};
				m_gpuBones = new global::UnityEngine.Texture2D(m_gpuBoneTexWidth, m_gpuBoneTexHeight, global::UnityEngine.TextureFormat.RGBAFloat, mipChain: false, linear: true);
				m_gpuBones.hideFlags = global::UnityEngine.HideFlags.DontSave;
				m_gpuBones.name = "AM-" + m_obj.name + "-Bones";
				m_gpuBones.filterMode = global::UnityEngine.FilterMode.Point;
				m_gpuBoneData = new global::UnityEngine.Color[m_gpuBoneTexWidth * m_gpuBoneTexHeight];
				UpdateBonesGPU();
				global::UnityEngine.TextureFormat textureFormat = global::UnityEngine.TextureFormat.RHalf;
				textureFormat = ((m_weightCount == 2) ? global::UnityEngine.TextureFormat.RGHalf : textureFormat);
				textureFormat = ((m_weightCount == 4) ? global::UnityEngine.TextureFormat.RGBAHalf : textureFormat);
				m_gpuBoneIndices = new global::UnityEngine.Texture2D(m_gpuVertexTexWidth, m_gpuVertexTexHeight, textureFormat, mipChain: false, linear: true);
				m_gpuBoneIndices.hideFlags = global::UnityEngine.HideFlags.DontSave;
				m_gpuBoneIndices.name = "AM-" + m_obj.name + "-Bones";
				m_gpuBoneIndices.filterMode = global::UnityEngine.FilterMode.Point;
				m_gpuBoneIndices.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
				global::UnityEngine.BoneWeight[] boneWeights = m_renderer.sharedMesh.boneWeights;
				global::UnityEngine.Color[] array = new global::UnityEngine.Color[m_gpuVertexTexWidth * m_gpuVertexTexHeight];
				for (int i = 0; i < m_vertexCount; i++)
				{
					int num = i % m_gpuVertexTexWidth;
					int num2 = i / m_gpuVertexTexWidth * m_gpuVertexTexWidth + num;
					global::UnityEngine.BoneWeight boneWeight = boneWeights[i];
					array[num2] = new global::UnityEngine.Vector4(boneWeight.boneIndex0, boneWeight.boneIndex1, boneWeight.boneIndex2, boneWeight.boneIndex3);
				}
				m_gpuBoneIndices.SetPixels(array);
				m_gpuBoneIndices.Apply();
				m_gpuBaseVertices = new global::UnityEngine.Texture2D[m_weightCount];
				for (int j = 0; j < m_weightCount; j++)
				{
					m_gpuBaseVertices[j] = new global::UnityEngine.Texture2D(m_gpuVertexTexWidth, m_gpuVertexTexHeight, global::UnityEngine.TextureFormat.RGBAFloat, mipChain: false, linear: true);
					m_gpuBaseVertices[j].hideFlags = global::UnityEngine.HideFlags.DontSave;
					m_gpuBaseVertices[j].name = "AM-" + m_obj.name + "-BaseVerts";
					m_gpuBaseVertices[j].filterMode = global::UnityEngine.FilterMode.Point;
				}
				global::System.Collections.Generic.List<global::UnityEngine.Color[]> list = new global::System.Collections.Generic.List<global::UnityEngine.Color[]>(m_weightCount);
				for (int k = 0; k < m_weightCount; k++)
				{
					list.Add(new global::UnityEngine.Color[m_gpuVertexTexWidth * m_gpuVertexTexHeight]);
				}
				for (int l = 0; l < m_vertexCount; l++)
				{
					int num3 = l % m_gpuVertexTexWidth;
					int num4 = l / m_gpuVertexTexWidth * m_gpuVertexTexWidth + num3;
					for (int m = 0; m < m_weightCount; m++)
					{
						list[m][num4] = m_baseVertices[l * m_weightCount + m];
					}
				}
				for (int n = 0; n < m_weightCount; n++)
				{
					m_gpuBaseVertices[n].SetPixels(list[n]);
					m_gpuBaseVertices[n].Apply();
				}
				m_gpuPrevVertices = new global::UnityEngine.RenderTexture(m_gpuVertexTexWidth, m_gpuVertexTexHeight, 0, global::UnityEngine.RenderTextureFormat.ARGBFloat, global::UnityEngine.RenderTextureReadWrite.Linear);
				m_gpuPrevVertices.hideFlags = global::UnityEngine.HideFlags.DontSave;
				m_gpuPrevVertices.name = "AM-" + m_obj.name + "-PrevVerts";
				m_gpuPrevVertices.filterMode = global::UnityEngine.FilterMode.Point;
				m_gpuPrevVertices.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
				m_gpuPrevVertices.Create();
				m_gpuCurrVertices = new global::UnityEngine.RenderTexture(m_gpuVertexTexWidth, m_gpuVertexTexHeight, 0, global::UnityEngine.RenderTextureFormat.ARGBFloat, global::UnityEngine.RenderTextureReadWrite.Linear);
				m_gpuCurrVertices.hideFlags = global::UnityEngine.HideFlags.DontSave;
				m_gpuCurrVertices.name = "AM-" + m_obj.name + "-CurrVerts";
				m_gpuCurrVertices.filterMode = global::UnityEngine.FilterMode.Point;
				m_gpuCurrVertices.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
				m_gpuCurrVertices.Create();
				m_gpuSkinDeformMat.SetTexture("_AM_BONE_TEX", m_gpuBones);
				m_gpuSkinDeformMat.SetTexture("_AM_BONE_INDEX_TEX", m_gpuBoneIndices);
				for (int num5 = 0; num5 < m_weightCount; num5++)
				{
					m_gpuSkinDeformMat.SetTexture("_AM_BASE_VERTEX" + num5 + "_TEX", m_gpuBaseVertices[num5]);
				}
				global::UnityEngine.Vector4 vector = new global::UnityEngine.Vector4(1f / (float)m_gpuBoneTexWidth, 1f / (float)m_gpuBoneTexHeight, m_gpuBoneTexWidth, m_gpuBoneTexHeight);
				global::UnityEngine.Vector4 vector2 = new global::UnityEngine.Vector4(1f / (float)m_gpuVertexTexWidth, 1f / (float)m_gpuVertexTexHeight, m_gpuVertexTexWidth, m_gpuVertexTexHeight);
				m_gpuSkinDeformMat.SetVector("_AM_BONE_TEXEL_SIZE", vector);
				m_gpuSkinDeformMat.SetVector("_AM_BONE_TEXEL_HALFSIZE", vector * 0.5f);
				m_gpuSkinDeformMat.SetVector("_AM_VERTEX_TEXEL_SIZE", vector2);
				m_gpuSkinDeformMat.SetVector("_AM_VERTEX_TEXEL_HALFSIZE", vector2 * 0.5f);
				global::UnityEngine.Vector2[] array2 = new global::UnityEngine.Vector2[m_vertexCount];
				for (int num6 = 0; num6 < m_vertexCount; num6++)
				{
					int num7 = num6 % m_gpuVertexTexWidth;
					int num8 = num6 / m_gpuVertexTexWidth;
					float x = (float)num7 / (float)m_gpuVertexTexWidth + vector2.x * 0.5f;
					float y = (float)num8 / (float)m_gpuVertexTexHeight + vector2.y * 0.5f;
					array2[num6] = new global::UnityEngine.Vector2(x, y);
				}
				m_clonedMesh.uv2 = array2;
			}
			catch (global::System.Exception)
			{
				result = false;
			}
			return result;
		}

		private void ShutdownGPUSkinDeform()
		{
			if (m_gpuSkinDeformMat != null)
			{
				global::UnityEngine.Object.DestroyImmediate(m_gpuSkinDeformMat);
				m_gpuSkinDeformMat = null;
			}
			m_gpuBoneData = null;
			if (m_gpuBones != null)
			{
				global::UnityEngine.Object.DestroyImmediate(m_gpuBones);
				m_gpuBones = null;
			}
			if (m_gpuBoneIndices != null)
			{
				global::UnityEngine.Object.DestroyImmediate(m_gpuBoneIndices);
				m_gpuBoneIndices = null;
			}
			if (m_gpuBaseVertices != null)
			{
				for (int i = 0; i < m_gpuBaseVertices.Length; i++)
				{
					global::UnityEngine.Object.DestroyImmediate(m_gpuBaseVertices[i]);
				}
				m_gpuBaseVertices = null;
			}
			if (m_gpuPrevVertices != null)
			{
				global::UnityEngine.RenderTexture.active = null;
				m_gpuPrevVertices.Release();
				global::UnityEngine.Object.DestroyImmediate(m_gpuPrevVertices);
				m_gpuPrevVertices = null;
			}
			if (m_gpuCurrVertices != null)
			{
				global::UnityEngine.RenderTexture.active = null;
				m_gpuCurrVertices.Release();
				global::UnityEngine.Object.DestroyImmediate(m_gpuCurrVertices);
				m_gpuCurrVertices = null;
			}
		}

		private void UpdateBonesGPU()
		{
			for (int i = 0; i < m_boneCount; i++)
			{
				for (int j = 0; j < m_gpuBoneTexHeight; j++)
				{
					m_gpuBoneData[j * m_gpuBoneTexWidth + i] = m_bones[i].GetRow(j);
				}
			}
			m_gpuBones.SetPixels(m_gpuBoneData);
			m_gpuBones.Apply();
		}

		private void UpdateVerticesGPU(global::UnityEngine.Rendering.CommandBuffer updateCB, bool starting)
		{
			if (!starting && m_wasVisible)
			{
				m_gpuPrevVertices.DiscardContents();
				updateCB.Blit(new global::UnityEngine.Rendering.RenderTargetIdentifier(m_gpuCurrVertices), m_gpuPrevVertices);
			}
			updateCB.SetGlobalMatrix("_AM_WORLD_TO_LOCAL_MATRIX", m_worldToLocalMatrix);
			m_gpuCurrVertices.DiscardContents();
			global::UnityEngine.RenderTexture tex = null;
			updateCB.Blit(new global::UnityEngine.Rendering.RenderTargetIdentifier(tex), m_gpuCurrVertices, m_gpuSkinDeformMat, global::UnityEngine.Mathf.Min(m_weightCount - 1, 2));
			if (starting || !m_wasVisible)
			{
				m_gpuPrevVertices.DiscardContents();
				updateCB.Blit(new global::UnityEngine.Rendering.RenderTargetIdentifier(m_gpuCurrVertices), m_gpuPrevVertices);
			}
		}

		private void UpdateBones()
		{
			for (int i = 0; i < m_boneCount; i++)
			{
				m_bones[i] = ((m_boneTransforms[i] != null) ? m_boneTransforms[i].localToWorldMatrix : global::UnityEngine.Matrix4x4.identity);
			}
			m_worldToLocalMatrix = m_transform.worldToLocalMatrix;
			if (m_useGPU)
			{
				UpdateBonesGPU();
			}
		}

		private void UpdateVerticesFallback(bool starting)
		{
			if (!starting && m_wasVisible)
			{
				global::System.Array.Copy(m_currVertices, m_prevVertices, m_vertexCount);
			}
			m_renderer.BakeMesh(m_clonedMesh);
			if (m_clonedMesh.vertexCount == 0 || m_clonedMesh.vertexCount != m_prevVertices.Length)
			{
				IssueError("[AmplifyMotion] Invalid mesh obtained from SkinnedMeshRenderer.BakeMesh in object " + m_obj.name + ". Skipping.");
				return;
			}
			global::System.Array.Copy(m_clonedMesh.vertices, m_currVertices, m_vertexCount);
			if (starting || !m_wasVisible)
			{
				global::System.Array.Copy(m_currVertices, m_prevVertices, m_vertexCount);
			}
		}

		private void AsyncUpdateVertices(bool starting)
		{
			if (!starting && m_wasVisible)
			{
				global::System.Array.Copy(m_currVertices, m_prevVertices, m_vertexCount);
			}
			for (int i = 0; i < m_boneCount; i++)
			{
				m_bones[i] = m_worldToLocalMatrix * m_bones[i];
			}
			if (m_weightCount == 1)
			{
				UpdateVerticesBone1();
			}
			else if (m_weightCount == 2)
			{
				UpdateVerticesBone2();
			}
			else
			{
				UpdateVerticesBone4();
			}
			if (starting || !m_wasVisible)
			{
				global::System.Array.Copy(m_currVertices, m_prevVertices, m_vertexCount);
			}
		}

		private void InitializeBone1(global::UnityEngine.Vector4[] baseVertices, int[] boneIndices)
		{
			global::UnityEngine.Vector3[] vertices = m_renderer.sharedMesh.vertices;
			global::UnityEngine.Matrix4x4[] bindposes = m_renderer.sharedMesh.bindposes;
			global::UnityEngine.BoneWeight[] boneWeights = m_renderer.sharedMesh.boneWeights;
			for (int i = 0; i < m_vertexCount; i++)
			{
				int num = i * m_weightCount;
				global::UnityEngine.Vector3 vector = bindposes[boneIndices[num] = boneWeights[i].boneIndex0].MultiplyPoint3x4(vertices[i]);
				baseVertices[num] = new global::UnityEngine.Vector4(vector.x, vector.y, vector.z, 1f);
			}
		}

		private void InitializeBone2(global::UnityEngine.Vector4[] baseVertices, int[] boneIndices, float[] boneWeights)
		{
			global::UnityEngine.Vector3[] vertices = m_renderer.sharedMesh.vertices;
			global::UnityEngine.Matrix4x4[] bindposes = m_renderer.sharedMesh.bindposes;
			global::UnityEngine.BoneWeight[] boneWeights2 = m_renderer.sharedMesh.boneWeights;
			for (int i = 0; i < m_vertexCount; i++)
			{
				int num = i * m_weightCount;
				int num2 = num + 1;
				global::UnityEngine.BoneWeight boneWeight = boneWeights2[i];
				int num3 = (boneIndices[num] = boneWeight.boneIndex0);
				int num4 = (boneIndices[num2] = boneWeight.boneIndex1);
				float weight = boneWeight.weight0;
				float weight2 = boneWeight.weight1;
				float num5 = 1f / (weight + weight2);
				weight = (boneWeights[num] = weight * num5);
				weight2 = (boneWeights[num2] = weight2 * num5);
				global::UnityEngine.Vector3 vector = weight * bindposes[num3].MultiplyPoint3x4(vertices[i]);
				global::UnityEngine.Vector3 vector2 = weight2 * bindposes[num4].MultiplyPoint3x4(vertices[i]);
				baseVertices[num] = new global::UnityEngine.Vector4(vector.x, vector.y, vector.z, weight);
				baseVertices[num2] = new global::UnityEngine.Vector4(vector2.x, vector2.y, vector2.z, weight2);
			}
		}

		private void InitializeBone4(global::UnityEngine.Vector4[] baseVertices, int[] boneIndices, float[] boneWeights)
		{
			global::UnityEngine.Vector3[] vertices = m_renderer.sharedMesh.vertices;
			global::UnityEngine.Matrix4x4[] bindposes = m_renderer.sharedMesh.bindposes;
			global::UnityEngine.BoneWeight[] boneWeights2 = m_renderer.sharedMesh.boneWeights;
			for (int i = 0; i < m_vertexCount; i++)
			{
				int num = i * m_weightCount;
				int num2 = num + 1;
				int num3 = num + 2;
				int num4 = num + 3;
				global::UnityEngine.BoneWeight boneWeight = boneWeights2[i];
				int num5 = (boneIndices[num] = boneWeight.boneIndex0);
				int num6 = (boneIndices[num2] = boneWeight.boneIndex1);
				int num7 = (boneIndices[num3] = boneWeight.boneIndex2);
				int num8 = (boneIndices[num4] = boneWeight.boneIndex3);
				float num9 = (boneWeights[num] = boneWeight.weight0);
				float num10 = (boneWeights[num2] = boneWeight.weight1);
				float num11 = (boneWeights[num3] = boneWeight.weight2);
				float num12 = (boneWeights[num4] = boneWeight.weight3);
				global::UnityEngine.Vector3 vector = num9 * bindposes[num5].MultiplyPoint3x4(vertices[i]);
				global::UnityEngine.Vector3 vector2 = num10 * bindposes[num6].MultiplyPoint3x4(vertices[i]);
				global::UnityEngine.Vector3 vector3 = num11 * bindposes[num7].MultiplyPoint3x4(vertices[i]);
				global::UnityEngine.Vector3 vector4 = num12 * bindposes[num8].MultiplyPoint3x4(vertices[i]);
				baseVertices[num] = new global::UnityEngine.Vector4(vector.x, vector.y, vector.z, num9);
				baseVertices[num2] = new global::UnityEngine.Vector4(vector2.x, vector2.y, vector2.z, num10);
				baseVertices[num3] = new global::UnityEngine.Vector4(vector3.x, vector3.y, vector3.z, num11);
				baseVertices[num4] = new global::UnityEngine.Vector4(vector4.x, vector4.y, vector4.z, num12);
			}
		}

		private void UpdateVerticesBone1()
		{
			for (int i = 0; i < m_vertexCount; i++)
			{
				global::AmplifyMotion.MotionState.MulPoint3x4_XYZ(ref m_currVertices[i], ref m_bones[m_boneIndices[i]], m_baseVertices[i]);
			}
		}

		private void UpdateVerticesBone2()
		{
			global::UnityEngine.Vector3 result = global::UnityEngine.Vector3.zero;
			for (int i = 0; i < m_vertexCount; i++)
			{
				int num = i * 2;
				int num2 = num + 1;
				int num3 = m_boneIndices[num];
				int num4 = m_boneIndices[num2];
				float num5 = m_boneWeights[num2];
				global::AmplifyMotion.MotionState.MulPoint3x4_XYZW(ref result, ref m_bones[num3], m_baseVertices[num]);
				if (num5 != 0f)
				{
					global::AmplifyMotion.MotionState.MulAddPoint3x4_XYZW(ref result, ref m_bones[num4], m_baseVertices[num2]);
				}
				m_currVertices[i] = result;
			}
		}

		private void UpdateVerticesBone4()
		{
			global::UnityEngine.Vector3 result = global::UnityEngine.Vector3.zero;
			for (int i = 0; i < m_vertexCount; i++)
			{
				int num = i * 4;
				int num2 = num + 1;
				int num3 = num + 2;
				int num4 = num + 3;
				int num5 = m_boneIndices[num];
				int num6 = m_boneIndices[num2];
				int num7 = m_boneIndices[num3];
				int num8 = m_boneIndices[num4];
				float num9 = m_boneWeights[num2];
				float num10 = m_boneWeights[num3];
				float num11 = m_boneWeights[num4];
				global::AmplifyMotion.MotionState.MulPoint3x4_XYZW(ref result, ref m_bones[num5], m_baseVertices[num]);
				if (num9 != 0f)
				{
					global::AmplifyMotion.MotionState.MulAddPoint3x4_XYZW(ref result, ref m_bones[num6], m_baseVertices[num2]);
				}
				if (num10 != 0f)
				{
					global::AmplifyMotion.MotionState.MulAddPoint3x4_XYZW(ref result, ref m_bones[num7], m_baseVertices[num3]);
				}
				if (num11 != 0f)
				{
					global::AmplifyMotion.MotionState.MulAddPoint3x4_XYZW(ref result, ref m_bones[num8], m_baseVertices[num4]);
				}
				m_currVertices[i] = result;
			}
		}

		internal override void AsyncUpdate()
		{
			try
			{
				AsyncUpdateVertices(m_starting);
			}
			catch (global::System.Exception ex)
			{
				IssueError("[AmplifyMotion] Failed on SkinnedMeshRenderer data. Please contact support.\n" + ex.Message);
			}
			finally
			{
				m_asyncUpdateSignal.Set();
			}
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
			if (!m_error && (isVisible || starting))
			{
				UpdateBones();
				m_starting = !m_wasVisible || starting;
				if (!m_useFallback)
				{
					if (!m_useGPU)
					{
						m_asyncUpdateSignal.Reset();
						m_asyncUpdateTriggered = true;
						m_owner.Instance.WorkerPool.EnqueueAsyncUpdate(this);
					}
					else
					{
						UpdateVerticesGPU(updateCB, m_starting);
					}
				}
				else
				{
					UpdateVerticesFallback(m_starting);
				}
			}
			if (!m_useFallback)
			{
				m_currLocalToWorld = m_transform.localToWorldMatrix;
			}
			else
			{
				m_currLocalToWorld = m_transform.localToWorldMatrix;
			}
			if (starting || !m_wasVisible)
			{
				m_prevLocalToWorld = m_currLocalToWorld;
			}
			m_wasVisible = isVisible;
		}

		private void WaitForAsyncUpdate()
		{
			if (m_asyncUpdateTriggered)
			{
				if (!m_asyncUpdateSignal.WaitOne(100))
				{
					global::UnityEngine.Debug.LogWarning("[AmplifyMotion] Aborted abnormally long Async Skin deform operation. Not a critical error but might indicate a problem. Please contact support.");
				}
				else
				{
					m_asyncUpdateTriggered = false;
				}
			}
		}

		internal override void RenderVectors(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.CommandBuffer renderCB, float scale, global::AmplifyMotion.Quality quality)
		{
			if (!m_initialized || m_error || !m_renderer.isVisible)
			{
				return;
			}
			if (!m_useFallback && !m_useGPU)
			{
				WaitForAsyncUpdate();
			}
			if (!m_useGPU)
			{
				if (!m_useFallback)
				{
					m_clonedMesh.vertices = m_currVertices;
				}
				m_clonedMesh.normals = m_prevVertices;
			}
			bool num = ((int)m_owner.Instance.CullingMask & (1 << m_obj.gameObject.layer)) != 0;
			int num2 = (num ? m_owner.Instance.GenerateObjectId(m_obj.gameObject) : 255);
			float x = (num ? (m_owner.Instance.CameraMotionMult * scale) : 0f);
			float y = (num ? (m_owner.Instance.ObjectMotionMult * scale) : 0f);
			renderCB.SetGlobalMatrix("_AM_MATRIX_PREV_M", m_prevLocalToWorld);
			renderCB.SetGlobalMatrix("_AM_MATRIX_CURR_M", m_currLocalToWorld);
			renderCB.SetGlobalVector("_AM_MOTION_PARAMS", new global::UnityEngine.Vector4(x, y, (float)num2 * 0.003921569f, 0f));
			if (m_useGPU)
			{
				global::UnityEngine.Vector4 vector = new global::UnityEngine.Vector4(1f / (float)m_gpuVertexTexWidth, 1f / (float)m_gpuVertexTexHeight, m_gpuVertexTexWidth, m_gpuVertexTexHeight);
				renderCB.SetGlobalVector("_AM_VERTEX_TEXEL_SIZE", vector);
				renderCB.SetGlobalVector("_AM_VERTEX_TEXEL_HALFSIZE", vector * 0.5f);
				renderCB.SetGlobalTexture("_AM_PREV_VERTEX_TEX", m_gpuPrevVertices);
				renderCB.SetGlobalTexture("_AM_CURR_VERTEX_TEX", m_gpuCurrVertices);
			}
			int num3 = (m_useGPU ? 4 : 0);
			int num4 = ((quality != global::AmplifyMotion.Quality.Mobile) ? 2 : 0);
			int num5 = num3 + num4;
			for (int i = 0; i < m_sharedMaterials.Length; i++)
			{
				global::AmplifyMotion.MotionState.MaterialDesc materialDesc = m_sharedMaterials[i];
				int shaderPass = num5 + (materialDesc.coverage ? 1 : 0);
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
				renderCB.DrawMesh(m_clonedMesh, m_currLocalToWorld, m_owner.Instance.SkinnedVectorsMaterial, i, shaderPass, materialDesc.propertyBlock);
			}
		}
	}
}
