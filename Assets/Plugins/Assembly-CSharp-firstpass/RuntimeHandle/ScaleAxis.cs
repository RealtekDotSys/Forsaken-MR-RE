namespace RuntimeHandle
{
	public class ScaleAxis : global::RuntimeHandle.HandleBase
	{
		private const float SIZE = 2f;

		private global::UnityEngine.Vector3 _axis;

		private global::UnityEngine.Vector3 _startScale;

		private float _interactionDistance;

		private global::UnityEngine.Ray _raxisRay;

		public global::RuntimeHandle.ScaleAxis Initialize(global::RuntimeHandle.RuntimeTransformHandle p_parentTransformHandle, global::UnityEngine.Vector3 p_axis, global::UnityEngine.Color p_color)
		{
			_parentTransformHandle = p_parentTransformHandle;
			_axis = p_axis;
			_defaultColor = p_color;
			InitializeMaterial();
			base.transform.SetParent(p_parentTransformHandle.transform, worldPositionStays: false);
			global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject();
			obj.transform.SetParent(base.transform, worldPositionStays: false);
			obj.AddComponent<global::UnityEngine.MeshRenderer>().material = _material;
			obj.AddComponent<global::UnityEngine.MeshFilter>().mesh = global::RuntimeHandle.MeshUtils.CreateCone(p_axis.magnitude * 2f, 0.02f, 0.02f, 8, 1);
			obj.AddComponent<global::UnityEngine.MeshCollider>().sharedMesh = global::RuntimeHandle.MeshUtils.CreateCone(p_axis.magnitude * 2f, 0.1f, 0.02f, 8, 1);
			obj.transform.localRotation = global::UnityEngine.Quaternion.FromToRotation(global::UnityEngine.Vector3.up, p_axis);
			global::UnityEngine.GameObject obj2 = new global::UnityEngine.GameObject();
			obj2.transform.SetParent(base.transform, worldPositionStays: false);
			obj2.AddComponent<global::UnityEngine.MeshRenderer>().material = _material;
			obj2.AddComponent<global::UnityEngine.MeshFilter>().mesh = global::RuntimeHandle.MeshUtils.CreateBox(0.25f, 0.25f, 0.25f);
			obj2.AddComponent<global::UnityEngine.MeshCollider>();
			obj2.transform.localRotation = global::UnityEngine.Quaternion.FromToRotation(global::UnityEngine.Vector3.up, p_axis);
			obj2.transform.localPosition = p_axis * 2f;
			return this;
		}

		protected void Update()
		{
			base.transform.GetChild(0).localScale = new global::UnityEngine.Vector3(1f, 1f + delta, 1f);
			base.transform.GetChild(1).localPosition = _axis * (2f * (1f + delta));
		}

		public override void Interact(global::UnityEngine.Vector3 p_previousPosition)
		{
			global::UnityEngine.Ray other = global::UnityEngine.Camera.main.ScreenPointToRay(global::RuntimeHandle.RuntimeTransformHandle.GetMousePosition());
			float distance = global::RuntimeHandle.HandleMathUtils.ClosestPointOnRay(_raxisRay, other);
			global::UnityEngine.Vector3 point = _raxisRay.GetPoint(distance);
			float num = global::UnityEngine.Vector3.Distance(_parentTransformHandle.target.position, point) / _interactionDistance - 1f;
			float num2 = global::UnityEngine.Mathf.Abs(global::UnityEngine.Vector3.Dot(_parentTransformHandle.scaleSnap, _axis));
			if (num2 != 0f)
			{
				if (_parentTransformHandle.snappingType == global::RuntimeHandle.HandleSnappingType.RELATIVE)
				{
					num = global::UnityEngine.Mathf.Round(num / num2) * num2;
				}
				else
				{
					float num3 = global::UnityEngine.Mathf.Abs(global::UnityEngine.Vector3.Dot(_startScale, _axis));
					num = global::UnityEngine.Mathf.Round((num + num3) / num2) * num2 - num3;
				}
			}
			delta = num;
			global::UnityEngine.Vector3 localScale = global::UnityEngine.Vector3.Scale(_startScale, _axis * num + global::UnityEngine.Vector3.one);
			_parentTransformHandle.target.localScale = localScale;
			base.Interact(p_previousPosition);
		}

		public override void StartInteraction(global::UnityEngine.Vector3 p_hitPoint)
		{
			base.StartInteraction(p_hitPoint);
			_startScale = _parentTransformHandle.target.localScale;
			global::UnityEngine.Vector3 direction = ((_parentTransformHandle.space == global::RuntimeHandle.HandleSpace.LOCAL) ? (_parentTransformHandle.target.rotation * _axis) : _axis);
			_raxisRay = new global::UnityEngine.Ray(_parentTransformHandle.target.position, direction);
			global::UnityEngine.Ray other = global::UnityEngine.Camera.main.ScreenPointToRay(global::RuntimeHandle.RuntimeTransformHandle.GetMousePosition());
			float distance = global::RuntimeHandle.HandleMathUtils.ClosestPointOnRay(_raxisRay, other);
			global::UnityEngine.Vector3 point = _raxisRay.GetPoint(distance);
			_interactionDistance = global::UnityEngine.Vector3.Distance(_parentTransformHandle.target.position, point);
		}
	}
}
