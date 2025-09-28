[global::UnityEngine.AddComponentMenu("")]
public class AmplifyMotionObjectBase : global::UnityEngine.MonoBehaviour
{
	public enum MinMaxCurveState
	{
		Scalar = 0,
		Curve = 1,
		TwoCurves = 2,
		TwoScalars = 3
	}

	internal static bool ApplyToChildren = true;

	[global::UnityEngine.SerializeField]
	private bool m_applyToChildren = ApplyToChildren;

	private global::AmplifyMotion.ObjectType m_type;

	private global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, global::AmplifyMotion.MotionState> m_states = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, global::AmplifyMotion.MotionState>();

	private bool m_fixedStep;

	private int m_objectId;

	private global::UnityEngine.Vector3 m_lastPosition = global::UnityEngine.Vector3.zero;

	private int m_resetAtFrame = -1;

	internal bool FixedStep => m_fixedStep;

	internal int ObjectId => m_objectId;

	public global::AmplifyMotion.ObjectType Type => m_type;

	internal void RegisterCamera(AmplifyMotionCamera camera)
	{
		global::UnityEngine.Camera component = camera.GetComponent<global::UnityEngine.Camera>();
		if ((component.cullingMask & (1 << base.gameObject.layer)) != 0 && !m_states.ContainsKey(component))
		{
			global::AmplifyMotion.MotionState motionState = null;
			motionState = m_type switch
			{
				global::AmplifyMotion.ObjectType.Solid => new global::AmplifyMotion.SolidState(camera, this), 
				global::AmplifyMotion.ObjectType.Skinned => new global::AmplifyMotion.SkinnedState(camera, this), 
				global::AmplifyMotion.ObjectType.Cloth => new global::AmplifyMotion.ClothState(camera, this), 
				global::AmplifyMotion.ObjectType.Particle => new global::AmplifyMotion.ParticleState(camera, this), 
				_ => throw new global::System.Exception("[AmplifyMotion] Invalid object type."), 
			};
			camera.RegisterObject(this);
			m_states.Add(component, motionState);
		}
	}

	internal void UnregisterCamera(AmplifyMotionCamera camera)
	{
		global::UnityEngine.Camera component = camera.GetComponent<global::UnityEngine.Camera>();
		if (m_states.TryGetValue(component, out var value))
		{
			camera.UnregisterObject(this);
			if (m_states.TryGetValue(component, out value))
			{
				value.Shutdown();
			}
			m_states.Remove(component);
		}
	}

	private bool InitializeType()
	{
		global::UnityEngine.Renderer component = GetComponent<global::UnityEngine.Renderer>();
		if (AmplifyMotionEffectBase.CanRegister(base.gameObject, autoReg: false))
		{
			if (GetComponent<global::UnityEngine.ParticleSystem>() != null)
			{
				m_type = global::AmplifyMotion.ObjectType.Particle;
				AmplifyMotionEffectBase.RegisterObject(this);
			}
			else if (component != null)
			{
				if (component.GetType() == typeof(global::UnityEngine.MeshRenderer))
				{
					m_type = global::AmplifyMotion.ObjectType.Solid;
				}
				else if (component.GetType() == typeof(global::UnityEngine.SkinnedMeshRenderer))
				{
					if (GetComponent<global::UnityEngine.Cloth>() != null)
					{
						m_type = global::AmplifyMotion.ObjectType.Cloth;
					}
					else
					{
						m_type = global::AmplifyMotion.ObjectType.Skinned;
					}
				}
				AmplifyMotionEffectBase.RegisterObject(this);
			}
		}
		return component != null;
	}

	private void OnEnable()
	{
		bool flag = InitializeType();
		if (flag)
		{
			if (m_type == global::AmplifyMotion.ObjectType.Cloth)
			{
				m_fixedStep = false;
			}
			else if (m_type == global::AmplifyMotion.ObjectType.Solid)
			{
				global::UnityEngine.Rigidbody component = GetComponent<global::UnityEngine.Rigidbody>();
				if (component != null && component.interpolation == global::UnityEngine.RigidbodyInterpolation.None && !component.isKinematic)
				{
					m_fixedStep = true;
				}
			}
		}
		if (m_applyToChildren)
		{
			foreach (global::UnityEngine.Transform item in base.gameObject.transform)
			{
				AmplifyMotionEffectBase.RegisterRecursivelyS(item.gameObject);
			}
		}
		if (!flag)
		{
			base.enabled = false;
		}
	}

	private void OnDisable()
	{
		AmplifyMotionEffectBase.UnregisterObject(this);
	}

	private void TryInitializeStates()
	{
		global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, global::AmplifyMotion.MotionState>.Enumerator enumerator = m_states.GetEnumerator();
		while (enumerator.MoveNext())
		{
			global::AmplifyMotion.MotionState value = enumerator.Current.Value;
			if (value.Owner.Initialized && !value.Error && !value.Initialized)
			{
				value.Initialize();
			}
		}
	}

	private void Start()
	{
		if (AmplifyMotionEffectBase.Instance != null)
		{
			TryInitializeStates();
		}
		m_lastPosition = base.transform.position;
	}

	private void Update()
	{
		if (AmplifyMotionEffectBase.Instance != null)
		{
			TryInitializeStates();
		}
	}

	private static void RecursiveResetMotionAtFrame(global::UnityEngine.Transform transform, AmplifyMotionObjectBase obj, int frame)
	{
		if (obj != null)
		{
			obj.m_resetAtFrame = frame;
		}
		foreach (global::UnityEngine.Transform item in transform)
		{
			RecursiveResetMotionAtFrame(item, item.GetComponent<AmplifyMotionObjectBase>(), frame);
		}
	}

	public void ResetMotionNow()
	{
		RecursiveResetMotionAtFrame(base.transform, this, global::UnityEngine.Time.frameCount);
	}

	public void ResetMotionAtFrame(int frame)
	{
		RecursiveResetMotionAtFrame(base.transform, this, frame);
	}

	private void CheckTeleportReset(AmplifyMotionEffectBase inst)
	{
		if (global::UnityEngine.Vector3.SqrMagnitude(base.transform.position - m_lastPosition) > inst.MinResetDeltaDistSqr)
		{
			RecursiveResetMotionAtFrame(base.transform, this, global::UnityEngine.Time.frameCount + inst.ResetFrameDelay);
		}
	}

	internal void OnUpdateTransform(AmplifyMotionEffectBase inst, global::UnityEngine.Camera camera, global::UnityEngine.Rendering.CommandBuffer updateCB, bool starting)
	{
		if (m_states.TryGetValue(camera, out var value) && !value.Error)
		{
			CheckTeleportReset(inst);
			bool flag = m_resetAtFrame > 0 && global::UnityEngine.Time.frameCount >= m_resetAtFrame;
			value.UpdateTransform(updateCB, starting || flag);
		}
		m_lastPosition = base.transform.position;
	}

	internal void OnRenderVectors(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.CommandBuffer renderCB, float scale, global::AmplifyMotion.Quality quality)
	{
		if (m_states.TryGetValue(camera, out var value) && !value.Error)
		{
			value.RenderVectors(camera, renderCB, scale, quality);
			if (m_resetAtFrame > 0 && global::UnityEngine.Time.frameCount >= m_resetAtFrame)
			{
				m_resetAtFrame = -1;
			}
		}
	}
}
