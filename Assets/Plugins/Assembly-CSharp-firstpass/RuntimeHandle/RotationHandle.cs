namespace RuntimeHandle
{
	public class RotationHandle : global::UnityEngine.MonoBehaviour
	{
		protected global::RuntimeHandle.RuntimeTransformHandle _parentTransformHandle;

		protected global::System.Collections.Generic.List<global::RuntimeHandle.RotationAxis> _axes;

		public global::RuntimeHandle.RotationHandle Initialize(global::RuntimeHandle.RuntimeTransformHandle p_parentTransformHandle)
		{
			_parentTransformHandle = p_parentTransformHandle;
			base.transform.SetParent(_parentTransformHandle.transform, worldPositionStays: false);
			_axes = new global::System.Collections.Generic.List<global::RuntimeHandle.RotationAxis>();
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.X || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XY || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_axes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.RotationAxis>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.right, global::UnityEngine.Color.red));
			}
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.Y || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XY || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.YZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_axes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.RotationAxis>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.up, global::UnityEngine.Color.green));
			}
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.Z || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.YZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_axes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.RotationAxis>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.forward, global::UnityEngine.Color.blue));
			}
			return this;
		}

		public void Destroy()
		{
			foreach (global::RuntimeHandle.RotationAxis axis in _axes)
			{
				global::UnityEngine.Object.Destroy(axis.gameObject);
			}
			global::UnityEngine.Object.Destroy(this);
		}
	}
}
