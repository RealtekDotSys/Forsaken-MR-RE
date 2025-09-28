namespace VLB_Samples
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.UI.Text))]
	public class FeaturesNotSupportedMessage : global::UnityEngine.MonoBehaviour
	{
		private void Start()
		{
			GetComponent<global::UnityEngine.UI.Text>().text = (global::VLB.Noise3D.isSupported ? "" : global::VLB.Noise3D.isNotSupportedString);
		}
	}
}
