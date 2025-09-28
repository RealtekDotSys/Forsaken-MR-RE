[global::UnityEngine.AddComponentMenu("")]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
public sealed class AmplifyMotionPostProcess : global::UnityEngine.MonoBehaviour
{
	private AmplifyMotionEffectBase m_instance;

	public AmplifyMotionEffectBase Instance
	{
		get
		{
			return m_instance;
		}
		set
		{
			m_instance = value;
		}
	}

	private void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (m_instance != null)
		{
			m_instance.PostProcess(source, destination);
		}
	}
}
