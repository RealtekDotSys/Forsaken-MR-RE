public class BatteryMaskLightHandler
{
	private BatteryMaskLightHandlerData _data;

	private SurgeLightController _surgeLightController;

	private float _surgeLightStrength;

	private SurgeLightController SurgeLightController
	{
		get
		{
			if (_surgeLightController == null)
			{
				_surgeLightController = global::UnityEngine.Object.FindObjectOfType<SurgeLightController>();
				return _surgeLightController;
			}
			return _surgeLightController;
		}
	}

	public BatteryMaskLightHandler(BatteryMaskLightHandlerData batteryMaskLightHandlerData)
	{
		_data = batteryMaskLightHandlerData;
	}

	public void Update()
	{
		if (_data.surgeMechanicUIHandler.BlinkState)
		{
			float a = _surgeLightStrength + global::UnityEngine.Time.deltaTime / _data.surgeMechanicUIHandler.BatterySurgeMaskLightFadeTime;
			_surgeLightStrength = global::UnityEngine.Mathf.Min(a, 1f);
		}
		else
		{
			float a = _surgeLightStrength - global::UnityEngine.Time.deltaTime / _data.surgeMechanicUIHandler.BatterySurgeMaskLightFadeTime;
			_surgeLightStrength = global::UnityEngine.Mathf.Max(a, 0f);
		}
		if (!(SurgeLightController == null))
		{
			SurgeLightController.SetSurgeLightStrength(_surgeLightStrength);
		}
	}
}
