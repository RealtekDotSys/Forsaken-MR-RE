public class ReportController : global::UnityEngine.MonoBehaviour
{
	public static ReportController Instance;

	public global::UnityEngine.Transform ReportParent;

	public Report ReportPrefab;

	private string reportData;

	private string cachedMapEntities;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		reportData = null;
		StartCoroutine(ConvertMapEntities());
	}

	private global::System.Collections.IEnumerator ConvertMapEntities()
	{
		yield return new global::UnityEngine.WaitUntil(() => reportData != null);
		global::UnityEngine.Debug.Log(reportData);
		yield return null;
	}

	public void SetReportData(string data)
	{
		reportData = data;
	}
}
