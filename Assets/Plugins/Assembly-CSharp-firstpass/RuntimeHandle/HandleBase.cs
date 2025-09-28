namespace RuntimeHandle
{
	public abstract class HandleBase : global::UnityEngine.MonoBehaviour
	{
		protected global::RuntimeHandle.RuntimeTransformHandle _parentTransformHandle;

		protected global::UnityEngine.Color _defaultColor;

		protected global::UnityEngine.Material _material;

		protected global::UnityEngine.Vector3 _hitPoint;

		protected bool _isInteracting;

		public float delta;

		public event global::System.Action InteractionStart;

		public event global::System.Action InteractionEnd;

		public event global::System.Action<float> InteractionUpdate;

		protected virtual void InitializeMaterial()
		{
			_material = new global::UnityEngine.Material(global::UnityEngine.Resources.Load("Shaders/HandleShader") as global::UnityEngine.Shader);
			_material.color = _defaultColor;
		}

		public void SetDefaultColor()
		{
			_material.color = _defaultColor;
		}

		public void SetColor(global::UnityEngine.Color p_color)
		{
			_material.color = p_color;
		}

		public virtual void StartInteraction(global::UnityEngine.Vector3 p_hitPoint)
		{
			_hitPoint = p_hitPoint;
			this.InteractionStart?.Invoke();
			_isInteracting = true;
		}

		public virtual bool CanInteract(global::UnityEngine.Vector3 p_hitPoint)
		{
			return true;
		}

		public virtual void Interact(global::UnityEngine.Vector3 p_previousPosition)
		{
			this.InteractionUpdate?.Invoke(delta);
		}

		public virtual void EndInteraction()
		{
			_isInteracting = false;
			this.InteractionEnd?.Invoke();
			delta = 0f;
			SetDefaultColor();
		}

		public static global::UnityEngine.Vector3 GetVectorFromAxes(global::RuntimeHandle.HandleAxes p_axes)
		{
			return p_axes switch
			{
				global::RuntimeHandle.HandleAxes.X => new global::UnityEngine.Vector3(1f, 0f, 0f), 
				global::RuntimeHandle.HandleAxes.Y => new global::UnityEngine.Vector3(0f, 1f, 0f), 
				global::RuntimeHandle.HandleAxes.Z => new global::UnityEngine.Vector3(0f, 0f, 1f), 
				global::RuntimeHandle.HandleAxes.XY => new global::UnityEngine.Vector3(1f, 1f, 0f), 
				global::RuntimeHandle.HandleAxes.XZ => new global::UnityEngine.Vector3(1f, 0f, 1f), 
				global::RuntimeHandle.HandleAxes.YZ => new global::UnityEngine.Vector3(0f, 1f, 1f), 
				_ => new global::UnityEngine.Vector3(1f, 1f, 1f), 
			};
		}
	}
}
