namespace RuntimeHandle
{
	public class ScaleHandle : global::UnityEngine.MonoBehaviour
	{
		protected global::RuntimeHandle.RuntimeTransformHandle _parentTransformHandle;

		protected global::System.Collections.Generic.List<global::RuntimeHandle.ScaleAxis> _axes;

		protected global::RuntimeHandle.ScaleGlobal _globalAxis;

		public global::RuntimeHandle.ScaleHandle Initialize(global::RuntimeHandle.RuntimeTransformHandle p_parentTransformHandle)
		{
			_parentTransformHandle = p_parentTransformHandle;
			base.transform.SetParent(_parentTransformHandle.transform, worldPositionStays: false);
			_axes = new global::System.Collections.Generic.List<global::RuntimeHandle.ScaleAxis>();
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.X || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XY || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_axes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.ScaleAxis>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.right, global::UnityEngine.Color.red));
			}
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.Y || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XY || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.YZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_axes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.ScaleAxis>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.up, global::UnityEngine.Color.green));
			}
			if (_parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.Z || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.YZ || _parentTransformHandle.axes == global::RuntimeHandle.HandleAxes.XYZ)
			{
				_axes.Add(new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.ScaleAxis>().Initialize(_parentTransformHandle, global::UnityEngine.Vector3.forward, global::UnityEngine.Color.blue));
			}
			if (_parentTransformHandle.axes != global::RuntimeHandle.HandleAxes.X && _parentTransformHandle.axes != global::RuntimeHandle.HandleAxes.Y && _parentTransformHandle.axes != global::RuntimeHandle.HandleAxes.Z)
			{
				_globalAxis = new global::UnityEngine.GameObject().AddComponent<global::RuntimeHandle.ScaleGlobal>().Initialize(_parentTransformHandle, global::RuntimeHandle.HandleBase.GetVectorFromAxes(_parentTransformHandle.axes), global::UnityEngine.Color.white);
				_globalAxis.InteractionStart += OnGlobalInteractionStart;
				_globalAxis.InteractionUpdate += OnGlobalInteractionUpdate;
				_globalAxis.InteractionEnd += OnGlobalInteractionEnd;
			}
			return this;
		}

		private void OnGlobalInteractionStart()
		{
			foreach (global::RuntimeHandle.ScaleAxis axis in _axes)
			{
				axis.SetColor(global::UnityEngine.Color.yellow);
			}
		}

		private void OnGlobalInteractionUpdate(float p_delta)
		{
			foreach (global::RuntimeHandle.ScaleAxis axis in _axes)
			{
				axis.delta = p_delta;
			}
		}

		private void OnGlobalInteractionEnd()
		{
			foreach (global::RuntimeHandle.ScaleAxis axis in _axes)
			{
				axis.SetDefaultColor();
				axis.delta = 0f;
			}
		}

		public void Destroy()
		{
			foreach (global::RuntimeHandle.ScaleAxis axis in _axes)
			{
				global::UnityEngine.Object.Destroy(axis.gameObject);
			}
			if ((bool)_globalAxis)
			{
				global::UnityEngine.Object.Destroy(_globalAxis.gameObject);
			}
			global::UnityEngine.Object.Destroy(this);
		}
	}
}
