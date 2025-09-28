public class BatteryColorHandlerData
{
	public SurgeMechanicUIHandler surgeMechanicUIHandler;

	public global::UnityEngine.UI.Slider batterySlider;

	public global::UnityEngine.UI.Image batteryColor;

	public global::UnityEngine.UI.Image batteryBG;

	public global::TMPro.TextMeshProUGUI batteryText;

	public global::System.Collections.Generic.List<BatteryColorToValue> batteryColorToValues;

	public global::UnityEngine.Color batterySurgeColor;

	public float batterySurgeBlinkDuration;

	public BatteryColorHandlerData(SurgeMechanicUIHandler surgeMechanicUIHandler, global::UnityEngine.UI.Slider batterySlider, global::UnityEngine.UI.Image batteryColor, global::UnityEngine.UI.Image batteryBG, global::TMPro.TextMeshProUGUI batteryText, global::System.Collections.Generic.List<BatteryColorToValue> batteryColorToValues, global::UnityEngine.Color batterySurgeColor)
	{
		this.surgeMechanicUIHandler = surgeMechanicUIHandler;
		this.batterySlider = batterySlider;
		this.batteryColor = batteryColor;
		this.batteryBG = batteryBG;
		this.batteryText = batteryText;
		this.batteryColorToValues = batteryColorToValues;
		this.batterySurgeColor = batterySurgeColor;
	}
}
