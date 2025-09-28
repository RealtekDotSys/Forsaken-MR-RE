[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Camera/Scanlines Effect")]
public class ScanlinesEffect : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Shader shader;

	private global::UnityEngine.Material _material;

	[global::UnityEngine.Range(0f, 10f)]
	public float lineWidth = 2f;

	[global::UnityEngine.Range(0f, 1f)]
	public float hardness = 0.9f;

	[global::UnityEngine.Range(0f, 1f)]
	public float displacementSpeed = 0.1f;

	protected global::UnityEngine.Material material
	{
		get
		{
			if (_material == null)
			{
				_material = new global::UnityEngine.Material(shader);
				_material.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			}
			return _material;
		}
	}

	private void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!(shader == null))
		{
			material.SetFloat("_LineWidth", lineWidth);
			material.SetFloat("_Hardness", hardness);
			material.SetFloat("_Speed", displacementSpeed);
			global::UnityEngine.Graphics.Blit(source, destination, material, 0);
		}
	}

	private void OnDisable()
	{
		if ((bool)_material)
		{
			global::UnityEngine.Object.DestroyImmediate(_material);
		}
	}
}
