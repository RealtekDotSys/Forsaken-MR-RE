namespace RuntimeHandle
{
	public class RotationAxis : global::RuntimeHandle.HandleBase
	{
		private global::UnityEngine.Mesh _arcMesh;

		private global::UnityEngine.Material _arcMaterial;

		private global::UnityEngine.Vector3 _axis;

		private global::UnityEngine.Vector3 _rotatedAxis;

		private global::UnityEngine.Plane _axisPlane;

		private global::UnityEngine.Vector3 _tangent;

		private global::UnityEngine.Vector3 _biTangent;

		private global::UnityEngine.Quaternion _startRotation;

		public global::RuntimeHandle.RotationAxis Initialize(global::RuntimeHandle.RuntimeTransformHandle p_runtimeHandle, global::UnityEngine.Vector3 p_axis, global::UnityEngine.Color p_color)
		{
			_parentTransformHandle = p_runtimeHandle;
			_axis = p_axis;
			_defaultColor = p_color;
			InitializeMaterial();
			base.transform.SetParent(p_runtimeHandle.transform, worldPositionStays: false);
			global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject();
			obj.transform.SetParent(base.transform, worldPositionStays: false);
			obj.AddComponent<global::UnityEngine.MeshRenderer>().material = _material;
			obj.AddComponent<global::UnityEngine.MeshFilter>().mesh = global::RuntimeHandle.MeshUtils.CreateTorus(2f, 0.02f, 32, 6);
			obj.AddComponent<global::UnityEngine.MeshCollider>().sharedMesh = global::RuntimeHandle.MeshUtils.CreateTorus(2f, 0.1f, 32, 6);
			obj.transform.localRotation = global::UnityEngine.Quaternion.FromToRotation(global::UnityEngine.Vector3.up, _axis);
			return this;
		}

		protected override void InitializeMaterial()
		{
			_material = new global::UnityEngine.Material(global::UnityEngine.Resources.Load("Shaders/AdvancedHandleShader") as global::UnityEngine.Shader);
			_material.color = _defaultColor;
		}

		public void Update()
		{
			_material.SetVector("_CameraPosition", _parentTransformHandle.handleCamera.transform.position);
			_material.SetFloat("_CameraDistance", (_parentTransformHandle.handleCamera.transform.position - _parentTransformHandle.transform.position).magnitude);
		}

		public override void Interact(global::UnityEngine.Vector3 p_previousPosition)
		{
			global::UnityEngine.Ray ray = global::UnityEngine.Camera.main.ScreenPointToRay(global::RuntimeHandle.RuntimeTransformHandle.GetMousePosition());
			if (!_axisPlane.Raycast(ray, out var enter))
			{
				base.Interact(p_previousPosition);
				return;
			}
			global::UnityEngine.Vector3 normalized = (ray.GetPoint(enter) - _parentTransformHandle.target.position).normalized;
			float num = global::UnityEngine.Mathf.Atan2(x: global::UnityEngine.Vector3.Dot(normalized, _tangent), y: global::UnityEngine.Vector3.Dot(normalized, _biTangent));
			float num2 = num * 57.29578f;
			if (_parentTransformHandle.rotationSnap != 0f)
			{
				num2 = global::UnityEngine.Mathf.Round(num2 / _parentTransformHandle.rotationSnap) * _parentTransformHandle.rotationSnap;
				num = num2 * (global::System.MathF.PI / 180f);
			}
			if (_parentTransformHandle.space == global::RuntimeHandle.HandleSpace.LOCAL)
			{
				_parentTransformHandle.target.localRotation = _startRotation * global::UnityEngine.Quaternion.AngleAxis(num2, _axis);
			}
			else
			{
				global::UnityEngine.Vector3 axis = global::UnityEngine.Quaternion.Inverse(_startRotation) * _axis;
				_parentTransformHandle.target.rotation = _startRotation * global::UnityEngine.Quaternion.AngleAxis(num2, axis);
			}
			_arcMesh = global::RuntimeHandle.MeshUtils.CreateArc(base.transform.position, _hitPoint, _rotatedAxis, 2f, num, global::UnityEngine.Mathf.Abs(global::UnityEngine.Mathf.CeilToInt(num2)) + 1);
			DrawArc();
			base.Interact(p_previousPosition);
		}

		public override bool CanInteract(global::UnityEngine.Vector3 p_hitPoint)
		{
			float magnitude = (_parentTransformHandle.transform.position - _parentTransformHandle.handleCamera.transform.position).magnitude;
			return (p_hitPoint - _parentTransformHandle.handleCamera.transform.position).magnitude <= magnitude;
		}

		public override void StartInteraction(global::UnityEngine.Vector3 p_hitPoint)
		{
			if (CanInteract(p_hitPoint))
			{
				base.StartInteraction(p_hitPoint);
				_startRotation = ((_parentTransformHandle.space == global::RuntimeHandle.HandleSpace.LOCAL) ? _parentTransformHandle.target.localRotation : _parentTransformHandle.target.rotation);
				_arcMaterial = new global::UnityEngine.Material(global::UnityEngine.Shader.Find("sHTiF/HandleShader"));
				_arcMaterial.color = new global::UnityEngine.Color(1f, 1f, 0f, 0.4f);
				_arcMaterial.renderQueue = 5000;
				if (_parentTransformHandle.space == global::RuntimeHandle.HandleSpace.LOCAL)
				{
					_rotatedAxis = _startRotation * _axis;
				}
				else
				{
					_rotatedAxis = _axis;
				}
				_axisPlane = new global::UnityEngine.Plane(_rotatedAxis, _parentTransformHandle.target.position);
				global::UnityEngine.Ray ray = global::UnityEngine.Camera.main.ScreenPointToRay(global::RuntimeHandle.RuntimeTransformHandle.GetMousePosition());
				float enter;
				global::UnityEngine.Vector3 vector = ((!_axisPlane.Raycast(ray, out enter)) ? _axisPlane.ClosestPointOnPlane(p_hitPoint) : ray.GetPoint(enter));
				_tangent = (vector - _parentTransformHandle.target.position).normalized;
				_biTangent = global::UnityEngine.Vector3.Cross(_rotatedAxis, _tangent);
			}
		}

		public override void EndInteraction()
		{
			base.EndInteraction();
			delta = 0f;
		}

		private void DrawArc()
		{
			global::UnityEngine.Graphics.DrawMesh(_arcMesh, global::UnityEngine.Matrix4x4.identity, _arcMaterial, 0);
		}
	}
}
