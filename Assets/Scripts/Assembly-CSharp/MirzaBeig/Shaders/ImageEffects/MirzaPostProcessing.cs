namespace MirzaBeig.Shaders.ImageEffects
{
	[global::System.Serializable]
	[global::UnityEngine.ExecuteInEditMode]
	public class MirzaPostProcessing : global::UnityEngine.MonoBehaviour
	{
		public global::UnityEngine.Material material;

		private void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
		{
			global::UnityEngine.Graphics.Blit(source, destination, material);
		}
	}
}
