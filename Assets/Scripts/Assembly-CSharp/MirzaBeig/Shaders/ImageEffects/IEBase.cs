namespace MirzaBeig.Shaders.ImageEffects
{
	[global::System.Serializable]
	[global::UnityEngine.ExecuteInEditMode]
	public class IEBase : global::UnityEngine.MonoBehaviour
	{
		private global::UnityEngine.Material _material;

		private global::UnityEngine.Camera _camera;

		protected global::UnityEngine.Material material
		{
			get
			{
				if (!_material)
				{
					_material = new global::UnityEngine.Material(shader);
					_material.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
				}
				return _material;
			}
		}

		protected global::UnityEngine.Shader shader { get; set; }

		protected global::UnityEngine.Camera camera
		{
			get
			{
				if (!_camera)
				{
					_camera = GetComponent<global::UnityEngine.Camera>();
				}
				return _camera;
			}
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		private void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
		{
		}

		protected void blit(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
		{
			global::UnityEngine.Graphics.Blit(source, destination, material);
		}

		private void OnDisable()
		{
			if ((bool)_material)
			{
				global::UnityEngine.Object.DestroyImmediate(_material);
			}
		}
	}
}
