[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.ImageEffectAllowedInSceneView]
public class UnderwaterEffect : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Material _mat;

	private void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		global::UnityEngine.Graphics.Blit(source, destination, _mat);
	}
}
