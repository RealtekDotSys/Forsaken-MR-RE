namespace DigitalRuby.ThunderAndLightning
{
	[global::System.Serializable]
	public class LightningLightParameters
	{
		[global::UnityEngine.Tooltip("Light render mode - leave as auto unless you have special use cases")]
		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.LightRenderMode RenderMode;

		[global::UnityEngine.Tooltip("Color of the light")]
		public global::UnityEngine.Color LightColor = global::UnityEngine.Color.white;

		[global::UnityEngine.Tooltip("What percent of segments should have a light? For performance you may want to keep this small.")]
		[global::UnityEngine.Range(0f, 1f)]
		public float LightPercent = 1E-06f;

		[global::UnityEngine.Tooltip("What percent of lights created should cast shadows?")]
		[global::UnityEngine.Range(0f, 1f)]
		public float LightShadowPercent;

		[global::UnityEngine.Tooltip("Light intensity")]
		[global::UnityEngine.Range(0f, 8f)]
		public float LightIntensity = 0.5f;

		[global::UnityEngine.Tooltip("Bounce intensity")]
		[global::UnityEngine.Range(0f, 8f)]
		public float BounceIntensity;

		[global::UnityEngine.Tooltip("Shadow strength, 0 means all light, 1 means all shadow")]
		[global::UnityEngine.Range(0f, 1f)]
		public float ShadowStrength = 1f;

		[global::UnityEngine.Tooltip("Shadow bias, 0 - 2")]
		[global::UnityEngine.Range(0f, 2f)]
		public float ShadowBias = 0.05f;

		[global::UnityEngine.Tooltip("Shadow normal bias, 0 - 3")]
		[global::UnityEngine.Range(0f, 3f)]
		public float ShadowNormalBias = 0.4f;

		[global::UnityEngine.Tooltip("The range of each light created")]
		public float LightRange;

		[global::UnityEngine.Tooltip("Only light objects that match this layer mask")]
		public global::UnityEngine.LayerMask CullingMask = -1;

		[global::UnityEngine.Tooltip("Offset from camera position when in orthographic mode")]
		[global::UnityEngine.Range(-1000f, 1000f)]
		public float OrthographicOffset;

		[global::UnityEngine.Tooltip("Increase the duration of light fade in compared to the lightning fade.")]
		[global::UnityEngine.Range(0f, 20f)]
		public float FadeInMultiplier = 1f;

		[global::UnityEngine.Tooltip("Increase the duration of light fully lit compared to the lightning fade.")]
		[global::UnityEngine.Range(0f, 20f)]
		public float FadeFullyLitMultiplier = 1f;

		[global::UnityEngine.Tooltip("Increase the duration of light fade out compared to the lightning fade.")]
		[global::UnityEngine.Range(0f, 20f)]
		public float FadeOutMultiplier = 1f;

		public bool HasLight
		{
			get
			{
				if (LightColor.a > 0f && LightIntensity >= 0.01f && LightPercent >= 1E-07f)
				{
					return LightRange > 0.01f;
				}
				return false;
			}
		}
	}
}
