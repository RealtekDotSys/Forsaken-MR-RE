[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.ExecuteInEditMode]
public class ModifiedGlitchShader : global::UnityEngine.MonoBehaviour
{
	public float ShearStrength = 0.12f;

	public bool InvertScreen;

	public bool InvertGrayScreen;

	private global::UnityEngine.Material _material;

	private float TimeCount;

	private float HorizontalResolution = 480f;

	private float VerticalResolution = 640f;

	private void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		_material = new global::UnityEngine.Material(global::UnityEngine.Shader.Find("Custom/Mobile Modified Glitch Shader"));
		_material.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
		_material.SetFloat("_ResHorizontal", HorizontalResolution);
		_material.SetFloat("_ResVertical", VerticalResolution);
		_material.SetFloat("_Timer", TimeCount);
		_material.SetFloat("_ShearStrength", ShearStrength);
		_material.SetFloat("_ScanlineGlitchEnabled", (ShearStrength == 0f) ? 0f : 1f);
		_material.SetFloat("_InvertScreen", InvertScreen ? 1f : 0f);
		_material.SetFloat("_InvertGrayScreen", InvertGrayScreen ? 1f : 0f);
		global::UnityEngine.Graphics.Blit(source, destination, _material);
		TimeCount += global::UnityEngine.Time.deltaTime * 0.5f;
		if (TimeCount > 10f)
		{
			TimeCount = 0f;
		}
	}

	private void OnDisable()
	{
		global::UnityEngine.Object.DestroyImmediate(_material);
	}

	public ModifiedGlitchShader()
	{
		HorizontalResolution = global::UnityEngine.Screen.width;
		VerticalResolution = global::UnityEngine.Screen.height;
	}
}
