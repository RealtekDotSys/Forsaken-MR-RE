namespace RuntimeHandle
{
	public class ScaleGlobal : global::RuntimeHandle.HandleBase
	{
		protected global::UnityEngine.Vector3 _axis;

		protected global::UnityEngine.Vector3 _startScale;

		public global::RuntimeHandle.ScaleGlobal Initialize(global::RuntimeHandle.RuntimeTransformHandle p_parentTransformHandle, global::UnityEngine.Vector3 p_axis, global::UnityEngine.Color p_color)
		{
			_parentTransformHandle = p_parentTransformHandle;
			_axis = p_axis;
			_defaultColor = p_color;
			InitializeMaterial();
			base.transform.SetParent(p_parentTransformHandle.transform, worldPositionStays: false);
			global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject();
			obj.transform.SetParent(base.transform, worldPositionStays: false);
			obj.AddComponent<global::UnityEngine.MeshRenderer>().material = _material;
			obj.AddComponent<global::UnityEngine.MeshFilter>().mesh = global::RuntimeHandle.MeshUtils.CreateBox(0.35f, 0.35f, 0.35f);
			obj.AddComponent<global::UnityEngine.MeshCollider>();
			return this;
		}

		public override void Interact(global::UnityEngine.Vector3 p_previousPosition)
		{
			global::UnityEngine.Vector3 vector = global::RuntimeHandle.RuntimeTransformHandle.GetMousePosition() - p_previousPosition;
			float num = (vector.x + vector.y) * global::UnityEngine.Time.deltaTime * 2f;
			delta += num;
			_parentTransformHandle.target.localScale = _startScale + global::UnityEngine.Vector3.Scale(_startScale, _axis) * delta;
			base.Interact(p_previousPosition);
		}

		public override void StartInteraction(global::UnityEngine.Vector3 p_hitPoint)
		{
			base.StartInteraction(p_hitPoint);
			_startScale = _parentTransformHandle.target.localScale;
		}
	}
}
