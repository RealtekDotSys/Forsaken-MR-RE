namespace RuntimeHandle
{
	public class PositionAxis : global::RuntimeHandle.HandleBase
	{
		protected global::UnityEngine.Vector3 _startPosition;

		protected global::UnityEngine.Vector3 _axis;

		private global::UnityEngine.Vector3 _interactionOffset;

		private global::UnityEngine.Ray _raxisRay;

		public global::RuntimeHandle.PositionAxis Initialize(global::RuntimeHandle.RuntimeTransformHandle p_runtimeHandle, global::UnityEngine.Vector3 p_axis, global::UnityEngine.Color p_color)
		{
			_parentTransformHandle = p_runtimeHandle;
			_axis = p_axis;
			_defaultColor = p_color;
			InitializeMaterial();
			base.transform.SetParent(p_runtimeHandle.transform, worldPositionStays: false);
			global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject();
			obj.transform.SetParent(base.transform, worldPositionStays: false);
			obj.AddComponent<global::UnityEngine.MeshRenderer>().material = _material;
			obj.AddComponent<global::UnityEngine.MeshFilter>().mesh = global::RuntimeHandle.MeshUtils.CreateCone(2f, 0.02f, 0.02f, 8, 1);
			obj.AddComponent<global::UnityEngine.MeshCollider>().sharedMesh = global::RuntimeHandle.MeshUtils.CreateCone(2f, 0.1f, 0.02f, 8, 1);
			obj.transform.localRotation = global::UnityEngine.Quaternion.FromToRotation(global::UnityEngine.Vector3.up, p_axis);
			global::UnityEngine.GameObject obj2 = new global::UnityEngine.GameObject();
			obj2.transform.SetParent(base.transform, worldPositionStays: false);
			obj2.AddComponent<global::UnityEngine.MeshRenderer>().material = _material;
			obj2.AddComponent<global::UnityEngine.MeshFilter>().mesh = global::RuntimeHandle.MeshUtils.CreateCone(0.4f, 0.2f, 0f, 8, 1);
			obj2.AddComponent<global::UnityEngine.MeshCollider>();
			obj2.transform.localRotation = global::UnityEngine.Quaternion.FromToRotation(global::UnityEngine.Vector3.up, _axis);
			obj2.transform.localPosition = p_axis * 2f;
			return this;
		}

		public override void Interact(global::UnityEngine.Vector3 p_previousPosition)
		{
			global::UnityEngine.Ray other = global::UnityEngine.Camera.main.ScreenPointToRay(global::RuntimeHandle.RuntimeTransformHandle.GetMousePosition());
			float distance = global::RuntimeHandle.HandleMathUtils.ClosestPointOnRay(_raxisRay, other);
			global::UnityEngine.Vector3 vector = _raxisRay.GetPoint(distance) + _interactionOffset - _startPosition;
			global::UnityEngine.Vector3 positionSnap = _parentTransformHandle.positionSnap;
			float magnitude = global::UnityEngine.Vector3.Scale(positionSnap, _axis).magnitude;
			if (magnitude != 0f && _parentTransformHandle.snappingType == global::RuntimeHandle.HandleSnappingType.RELATIVE)
			{
				vector = global::UnityEngine.Mathf.Round(vector.magnitude / magnitude) * magnitude * vector.normalized;
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
			base.StartInteraction(p_hitPoint);
			_startPosition = _parentTransformHandle.target.position;
			global::UnityEngine.Vector3 direction = ((_parentTransformHandle.space == global::RuntimeHandle.HandleSpace.LOCAL) ? (_parentTransformHandle.target.rotation * _axis) : _axis);
			_raxisRay = new global::UnityEngine.Ray(_startPosition, direction);
			global::UnityEngine.Ray other = global::UnityEngine.Camera.main.ScreenPointToRay(global::RuntimeHandle.RuntimeTransformHandle.GetMousePosition());
			float distance = global::RuntimeHandle.HandleMathUtils.ClosestPointOnRay(_raxisRay, other);
			global::UnityEngine.Vector3 point = _raxisRay.GetPoint(distance);
			_interactionOffset = _startPosition - point;
		}
	}
}
