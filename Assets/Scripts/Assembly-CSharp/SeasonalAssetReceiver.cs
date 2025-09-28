public class SeasonalAssetReceiver : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private string _seasonalKey;

	public string ReceiverId => _seasonalKey;

	private void Awake()
	{
		Request();
	}

	public void Request()
	{
	}
}
