public class SurgeLightController : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject surgeLight;

	[global::UnityEngine.SerializeField]
	private float surgeLightStrength;

	private global::UnityEngine.Light _light;

	private bool _lightIsNotNull;

	private void Start()
	{
		_light = surgeLight.GetComponent<global::UnityEngine.Light>();
		_lightIsNotNull = _light != null;
	}

	private void Update()
	{
		if (_lightIsNotNull)
		{
			if (surgeLightStrength > 0f)
			{
				surgeLight.SetActive(value: true);
				_light.intensity = surgeLightStrength;
			}
			else
			{
				surgeLight.SetActive(value: false);
			}
		}
	}

	public void SetSurgeLightStrength(float value)
	{
		surgeLightStrength = global::UnityEngine.Mathf.Clamp(value, 0f, 1f);
	}
}
