public class OrtonDisplayStrings : global::UnityEngine.MonoBehaviour
{
	public static OrtonDisplayStrings Instance;

	private string[] stringsToDisplay;

	public float verticalSpacing = 30f;

	public int fontSize = 20;

	private global::UnityEngine.GUIStyle guiStyle;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		guiStyle = new global::UnityEngine.GUIStyle();
		guiStyle.fontSize = fontSize;
		guiStyle.normal.textColor = global::UnityEngine.Color.white;
	}

	public void Display(string[] strings)
	{
		stringsToDisplay = strings;
	}

	private void OnGUI()
	{
		if (stringsToDisplay != null)
		{
			for (int i = 0; i < stringsToDisplay.Length; i++)
			{
				float num = (float)i * verticalSpacing;
				global::UnityEngine.GUI.Label(new global::UnityEngine.Rect(10f, 10f + num, global::UnityEngine.Screen.width, 30f), stringsToDisplay[i], guiStyle);
			}
		}
	}
}
