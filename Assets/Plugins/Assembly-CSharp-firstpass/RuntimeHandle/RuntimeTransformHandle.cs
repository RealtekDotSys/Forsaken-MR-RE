namespace RuntimeHandle
{
	public class RuntimeTransformHandle : global::UnityEngine.MonoBehaviour
	{
		public global::RuntimeHandle.HandleAxes axes = global::RuntimeHandle.HandleAxes.XYZ;

		public global::RuntimeHandle.HandleSpace space = global::RuntimeHandle.HandleSpace.LOCAL;

		public global::RuntimeHandle.HandleType type;

		public global::RuntimeHandle.HandleSnappingType snappingType = global::RuntimeHandle.HandleSnappingType.RELATIVE;

		public global::UnityEngine.Vector3 positionSnap = global::UnityEngine.Vector3.zero;

		public float rotationSnap;

		public global::UnityEngine.Vector3 scaleSnap = global::UnityEngine.Vector3.zero;

		public bool autoScale;

		public float autoScaleFactor = 1f;

		public global::UnityEngine.Camera handleCamera;

		private global::UnityEngine.Vector3 _previousMousePosition;

		private global::RuntimeHandle.HandleBase _previousAxis;

		private global::RuntimeHandle.HandleBase _draggingHandle;

		private global::RuntimeHandle.HandleType _previousType;

		private global::RuntimeHandle.HandleAxes _previousAxes;

		private global::RuntimeHandle.PositionHandle _positionHandle;

		private global::RuntimeHandle.RotationHandle _rotationHandle;

		private global::RuntimeHandle.ScaleHandle _scaleHandle;

		public global::UnityEngine.Transform target;

		public global::UnityEngine.Events.UnityEvent startedDraggingHandle = new global::UnityEngine.Events.UnityEvent();

		public global::UnityEngine.Events.UnityEvent isDraggingHandle = new global::UnityEngine.Events.UnityEvent();

		public global::UnityEngine.Events.UnityEvent endedDraggingHandle = new global::UnityEngine.Events.UnityEvent();

		[global::UnityEngine.SerializeField]
		private bool disableWhenNoTarget;

		private void Start()
		{
			if (handleCamera == null)
			{
				handleCamera = global::UnityEngine.Camera.main;
			}
			_previousType = type;
			if (target == null)
			{
				target = base.transform;
			}
			if (disableWhenNoTarget && target == base.transform)
			{
				base.gameObject.SetActive(value: false);
			}
			CreateHandles();
		}

		private void CreateHandles()
		{
			switch (type)
			{
			case global::RuntimeHandle.HandleType.POSITION:
				_positionHandle = base.gameObject.AddComponent<global::RuntimeHandle.PositionHandle>().Initialize(this);
				break;
			case global::RuntimeHandle.HandleType.ROTATION:
				_rotationHandle = base.gameObject.AddComponent<global::RuntimeHandle.RotationHandle>().Initialize(this);
				break;
			case global::RuntimeHandle.HandleType.SCALE:
				_scaleHandle = base.gameObject.AddComponent<global::RuntimeHandle.ScaleHandle>().Initialize(this);
				break;
			}
		}

		private void Clear()
		{
			_draggingHandle = null;
			if ((bool)_positionHandle)
			{
				_positionHandle.Destroy();
			}
			if ((bool)_rotationHandle)
			{
				_rotationHandle.Destroy();
			}
			if ((bool)_scaleHandle)
			{
				_scaleHandle.Destroy();
			}
		}

		private void Update()
		{
			if (autoScale)
			{
				base.transform.localScale = global::UnityEngine.Vector3.one * (global::UnityEngine.Vector3.Distance(handleCamera.transform.position, base.transform.position) * autoScaleFactor) / 15f;
			}
			if (_previousType != type || _previousAxes != axes)
			{
				Clear();
				CreateHandles();
				_previousType = type;
				_previousAxes = axes;
			}
			global::RuntimeHandle.HandleBase p_handle = null;
			global::UnityEngine.Vector3 p_hitPoint = global::UnityEngine.Vector3.zero;
			GetHandle(ref p_handle, ref p_hitPoint);
			HandleOverEffect(p_handle, p_hitPoint);
			if (PointerIsDown() && _draggingHandle != null)
			{
				_draggingHandle.Interact(_previousMousePosition);
				isDraggingHandle.Invoke();
			}
			if (GetPointerDown() && p_handle != null)
			{
				_draggingHandle = p_handle;
				_draggingHandle.StartInteraction(p_hitPoint);
				startedDraggingHandle.Invoke();
			}
			if (GetPointerUp() && _draggingHandle != null)
			{
				_draggingHandle.EndInteraction();
				_draggingHandle = null;
				endedDraggingHandle.Invoke();
			}
			_previousMousePosition = GetMousePosition();
			base.transform.position = target.transform.position;
			if (space == global::RuntimeHandle.HandleSpace.LOCAL || type == global::RuntimeHandle.HandleType.SCALE)
			{
				base.transform.rotation = target.transform.rotation;
			}
			else
			{
				base.transform.rotation = global::UnityEngine.Quaternion.identity;
			}
		}

		public static bool GetPointerDown()
		{
			return global::UnityEngine.Input.GetMouseButtonDown(0);
		}

		public static bool PointerIsDown()
		{
			return global::UnityEngine.Input.GetMouseButton(0);
		}

		public static bool GetPointerUp()
		{
			return global::UnityEngine.Input.GetMouseButtonUp(0);
		}

		public static global::UnityEngine.Vector3 GetMousePosition()
		{
			return global::UnityEngine.Input.mousePosition;
		}

		private void HandleOverEffect(global::RuntimeHandle.HandleBase p_axis, global::UnityEngine.Vector3 p_hitPoint)
		{
			if (_draggingHandle == null && _previousAxis != null && (_previousAxis != p_axis || !_previousAxis.CanInteract(p_hitPoint)))
			{
				_previousAxis.SetDefaultColor();
			}
			if (p_axis != null && _draggingHandle == null && p_axis.CanInteract(p_hitPoint))
			{
				p_axis.SetColor(global::UnityEngine.Color.yellow);
			}
			_previousAxis = p_axis;
		}

		private void GetHandle(ref global::RuntimeHandle.HandleBase p_handle, ref global::UnityEngine.Vector3 p_hitPoint)
		{
			global::UnityEngine.RaycastHit[] array = global::UnityEngine.Physics.RaycastAll(global::UnityEngine.Camera.main.ScreenPointToRay(GetMousePosition()));
			if (array.Length == 0)
			{
				return;
			}
			global::UnityEngine.RaycastHit[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				global::UnityEngine.RaycastHit raycastHit = array2[i];
				p_handle = raycastHit.collider.gameObject.GetComponentInParent<global::RuntimeHandle.HandleBase>();
				if (p_handle != null)
				{
					p_hitPoint = raycastHit.point;
					break;
				}
			}
		}

		public static global::RuntimeHandle.RuntimeTransformHandle Create(global::UnityEngine.Transform p_target, global::RuntimeHandle.HandleType p_handleType)
		{
			global::RuntimeHandle.RuntimeTransformHandle runtimeTransformHandle = new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.RuntimeTransformHandle>();
			runtimeTransformHandle.target = p_target;
			runtimeTransformHandle.type = p_handleType;
			return runtimeTransformHandle;
		}

		public void SetTarget(global::UnityEngine.Transform newTarget)
		{
			target = newTarget;
		}

		public void SetTarget(global::UnityEngine.GameObject newTarget)
		{
			target = newTarget.transform;
			if (target == null)
			{
				target = base.transform;
			}
			if (disableWhenNoTarget && target == base.transform)
			{
				base.gameObject.SetActive(value: false);
			}
			else if (disableWhenNoTarget && target != base.transform)
			{
				base.gameObject.SetActive(value: true);
			}
		}

		public void SetHandleMode(int mode)
		{
			SetHandleMode((global::RuntimeHandle.HandleType)mode);
		}

		public void SetHandleMode(global::RuntimeHandle.HandleType mode)
		{
			type = mode;
		}

		public void EnableXAxis(bool enable)
		{
			if (enable)
			{
				axes |= global::RuntimeHandle.HandleAxes.X;
			}
			else
			{
				axes &= (global::RuntimeHandle.HandleAxes)(-5);
			}
		}

		public void EnableYAxis(bool enable)
		{
			if (enable)
			{
				axes |= global::RuntimeHandle.HandleAxes.Y;
			}
			else
			{
				axes &= (global::RuntimeHandle.HandleAxes)(-3);
			}
		}

		public void EnableZAxis(bool enable)
		{
			if (enable)
			{
				axes |= global::RuntimeHandle.HandleAxes.Z;
			}
			else
			{
				axes &= (global::RuntimeHandle.HandleAxes)(-2);
			}
		}

		public void SetAxis(global::RuntimeHandle.HandleAxes newAxes)
		{
			axes = newAxes;
		}
	}
}
