public class LoadingBarController : global::UnityEngine.MonoBehaviour
{
	public global::TMPro.TextMeshProUGUI percentageText;

	public global::UnityEngine.UI.Slider slider;

	private int Progress;

	private void Awake()
	{
		percentageText = GetComponentInChildren<global::TMPro.TextMeshProUGUI>();
		slider = GetComponentInChildren<global::UnityEngine.UI.Slider>();
		Progress = 0;
		slider.value = 0f;
		slider.minValue = 0f;
		slider.maxValue = 100f;
		percentageText.text = "0%";
	}

	public void AddSliderValue(int fullCount)
	{
		Progress = (int)global::UnityEngine.Mathf.Round(global::UnityEngine.Mathf.Min(100f, (float)Progress + 100f / (float)fullCount));
		slider.value = Progress;
		percentageText.text = Progress + "%";
	}

	public void Complete()
	{
		Progress = 100;
		slider.value = Progress;
		percentageText.text = Progress + "%";
	}
}
