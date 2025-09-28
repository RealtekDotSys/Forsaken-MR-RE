namespace SRF.UI
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.UI.CanvasScaler))]
	[global::UnityEngine.AddComponentMenu("SRF/UI/Retina Scaler")]
	public class SRRetinaScaler : global::SRF.SRMonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		private float _retinaScale = 2f;

		[global::UnityEngine.SerializeField]
		private int _thresholdDpi = 250;

		[global::UnityEngine.SerializeField]
		private bool _disablePixelPerfect;

		public int ThresholdDpi => _thresholdDpi;

		public float RetinaScale => _retinaScale;

		private void Start()
		{
			float dpi = global::UnityEngine.Screen.dpi;
			if (!(dpi <= 0f) && dpi > (float)ThresholdDpi)
			{
				global::UnityEngine.UI.CanvasScaler component = GetComponent<global::UnityEngine.UI.CanvasScaler>();
				component.uiScaleMode = global::UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize;
				component.scaleFactor *= RetinaScale;
				if (_disablePixelPerfect)
				{
					GetComponent<global::UnityEngine.Canvas>().pixelPerfect = false;
				}
			}
		}
	}
}
