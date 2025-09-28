namespace RuntimeHandle
{
	public class PositionHandle : global::UnityEngine.MonoBehaviour
	{
		protected global::RuntimeHandle.RuntimeTransformHandle _parentTransformHandle;

		protected global::System.Collections.Generic.List<global::RuntimeHandle.PositionAxis> _axes;

		protected global::System.Collections.Generic.List<global::RuntimeHandle.PositionPlane> _planes;

		public global::RuntimeHandle.PositionHandle Initialize(global::RuntimeHandle.RuntimeTransformHandle p_runtimeHandle)
		{
			_parentTransformHandle = p_runtimeHandle;
			base.transform.SetParent(_parentTransformHandle.transform, worldPositionStays: false);
			_axes = new global::System.Collections.Generic.List<global::RuntimeHandle.PositionAxis>();
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.X || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XY || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_axes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.PositionAxis>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.right, global::UnityEngine.Color.red));
			}
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.Y || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XY || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.YZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_axes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.PositionAxis>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.up, global::UnityEngine.Color.green));
			}
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.Z || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.YZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_axes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.PositionAxis>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.forward, global::UnityEngine.Color.blue));
			}
			_planes = new global::System.Collections.Generic.List<global::RuntimeHandle.PositionPlane>();
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XY || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_planes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.PositionPlane>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.right, global::UnityEngine.Vector3.up, -global::UnityEngine.Vector3.forward, new global::UnityEngine.Color(0f, 0f, 1f, 0.2f)));
			}
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.YZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_planes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.PositionPlane>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.up, global::UnityEngine.Vector3.forward, global::UnityEngine.Vector3.right, new global::UnityEngine.Color(1f, 0f, 0f, 0.2f)));
			}
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_planes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.PositionPlane>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.right, global::UnityEngine.Vector3.forward, global::UnityEngine.Vector3.up, new global::UnityEngine.Color(0f, 1f, 0f, 0.2f)));
			}
			return this;
		}

		public void Destroy()
		{
			foreach (global::RuntimeHandle.PositionAxis axis in _axes)
			{
				global::UnityEngine.Object.Destroy(axis.gameObject);
			}
			foreach (global::RuntimeHandle.PositionPlane plane in _planes)
			{
				global::UnityEngine.Object.Destroy(plane.gameObject);
			}
			global::UnityEngine.Object.Destroy(this);
		}
	}
}
