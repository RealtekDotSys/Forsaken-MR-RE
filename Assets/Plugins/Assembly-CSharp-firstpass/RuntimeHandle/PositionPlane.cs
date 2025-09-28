namespace RuntimeHandle
{
	public class PositionPlane : global::RuntimeHandle.HandleBase
	{
		protected global::UnityEngine.Vector3 _startPosition;

		protected global::UnityEngine.Vector3 _axis1;

		protected global::UnityEngine.Vector3 _axis2;

		protected global::UnityEngine.Vector3 _perp;

		protected global::UnityEngine.Plane _plane;

		protected global::UnityEngine.Vector3 _interactionOffset;

		protected global::UnityEngine.GameObject _handle;

		public global::RuntimeHandle.PositionPlane Initialize(global::RuntimeHandle.RuntimeTransformHandle p_runtimeHandle, global::UnityEngine.Vector3 p_axis1, global::UnityEngine.Vector3 p_axis2, global::UnityEngine.Vector3 p_perp, global::UnityEngine.Color p_color)
		{
			_parentTransformHandle = p_runtimeHandle;
			_defaultColor = p_color;
			_axis1 = p_axis1;
			_axis2 = p_axis2;
			_perp = p_perp;
			InitializeMaterial();
			base.transform.SetParent(p_runtimeHandle.transform, worldPositionStays: false);
			_handle = new global::UnityEngine.GameObject();
			_handle.transform.SetParent(base.transform, worldPositionStays: false);
			_handle.AddComponent<global::UnityEngine.MeshRenderer>().material = _material;
			_handle.AddComponent<global::UnityEngine.MeshFilter>().mesh = global::RuntimeHandle.MeshUtils.CreateBox(0.02f, 0.5f, 0.5f);
			_handle.AddComponent<global::UnityEngine.MeshCollider>();
			_handle.transform.localRotation = global::UnityEngine.Quaternion.FromToRotation(global::UnityEngine.Vector3.up, _perp);
			_handle.transform.localPosition = (_axis1 + _axis2) * 0.25f;
			return this;
		}

		public override void Interact(global::UnityEngine.Vector3 p_previousPosition)
		{
			global::UnityEngine.Ray ray = global::UnityEngine.Camera.main.ScreenPointToRay(global::RuntimeHandle.RuntimeTransformHandle.GetMousePosition());
			float enter = 0f;
			_plane.Raycast(ray, out enter);
			global::UnityEngine.Vector3 vector = ray.GetPoint(enter) + _interactionOffset - _startPosition;
			global::UnityEngine.Vector3 b = _axis1 + _axis2;
			global::UnityEngine.Vector3 positionSnap = _parentTransformHandle.positionSnap;
			float magnitude = global::UnityEngine.Vector3.Scale(positionSnap, b).magnitude;
			if (magnitude != 0f && _parentTransformHandle.snappingType == global::RuntimeHandle.HandleSnappingType.RELATIVE)
			{
				if (positionSnap.x != 0f)
				{
					vector.x = global::UnityEngine.Mathf.Round(vector.x / positionSnap.x) * positionSnap.x;
				}
				if (positionSnap.y != 0f)
				{
					vector.y = global::UnityEngine.Mathf.Round(vector.y / positionSnap.y) * positionSnap.y;
				}
				if (positionSnap.z != 0f)
				{
					vector.z = global::UnityEngine.Mathf.Round(vector.z / positionSnap.z) * positionSnap.z;
				}
			}
			global::UnityEngine.Vector3 position = _startPosition + vector;
			if (magnitude != 0f && _parentTransformHandle.snappingType == global::RuntimeHandle.HandleSnappingType.ABSOLUTE)
			{
				if (positionSnap.x != 0f)
				{
					position.x = global::UnityEngine.Mathf.Round(position.x / positionSnap.x) * positionSnap.x;
				}
				if (positionSnap.y != 0f)
				{
					position.y = global::UnityEngine.Mathf.Round(position.y / positionSnap.y) * positionSnap.y;
				}
				if (positionSnap.x != 0f)
				{
					position.z = global::UnityEngine.Mathf.Round(position.z / positionSnap.z) * positionSnap.z;
				}
			}
			_parentTransformHandle.target.position = position;
			base.Interact(p_previousPosition);
		}

		public override void StartInteraction(global::UnityEngine.Vector3 p_hitPoint)
		{
			global::UnityEngine.Vector3 inNormal = ((_parentTransformHandle.space == global::RuntimeHandle.HandleSpace.LOCAL) ? (_parentTransformHandle.target.rotation * _perp) : _perp);
			_plane = new global::UnityEngine.Plane(inNormal, _parentTransformHandle.target.position);
			global::UnityEngine.Ray ray = global::UnityEngine.Camera.main.ScreenPointToRay(global::RuntimeHandle.RuntimeTransformHandle.GetMousePosition());
			float enter = 0f;
			_plane.Raycast(ray, out enter);
			global::UnityEngine.Vector3 point = ray.GetPoint(enter);
			_startPosition = _parentTransformHandle.target.position;
			_interactionOffset = _startPosition - point;
		}

		private void Update()
		{
			global::UnityEngine.Vector3 vector = _axis1;
			global::UnityEngine.Vector3 to = ((_parentTransformHandle.space == global::RuntimeHandle.HandleSpace.LOCAL) ? (_parentTransformHandle.target.rotation * vector) : vector);
			if (global::UnityEngine.Vector3.Angle(_parentTransformHandle.handleCamera.transform.forward, to) < 90f)
			{
				vector = -vector;
			}
			global::UnityEngine.Vector3 vector2 = _axis2;
			global::UnityEngine.Vector3 to2 = ((_parentTransformHandle.space == global::RuntimeHandle.HandleSpace.LOCAL) ? (_parentTransformHandle.target.rotation * vector2) : vector2);
			if (global::UnityEngine.Vector3.Angle(_parentTransformHandle.handleCamera.transform.forward, to2) < 90f)
			{
				vector2 = -vector2;
			}
			_handle.transform.localPosition = (vector + vector2) * 0.25f;
		}
	}
}
