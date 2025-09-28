namespace MirzaBeig.Shaders.ImageEffects
{
	[global::System.Serializable]
	[global::UnityEngine.ExecuteInEditMode]
	public class Sharpen : global::MirzaBeig.Shaders.ImageEffects.IEBase
	{
		[global::UnityEngine.Range(-2f, 2f)]
		public float strength = 0.5f;

		[global::UnityEngine.Range(0f, 8f)]
		public float edgeMult = 0.2f;

		private void Awake()
		{
			base.shader = global::UnityEngine.Shader.Find("Hidden/Mirza Beig/Image Effects/Sharpen");
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		private void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
		{
			base.material.SetFloat("_strength", strength);
			base.material.SetFloat("_edgeMult", edgeMult);
			blit(source, destination);
		}
	}
}
