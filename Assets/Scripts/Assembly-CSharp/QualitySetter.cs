public class QualitySetter : global::UnityEngine.MonoBehaviour
{
	public enum QualityLevel
	{
		None = 0,
		Low = 1,
		Medium = 2,
		High = 3,
		VeryHigh = 4
	}

	private static int _initialResolutionWidth;

	private static int _initialResolutionHeight;

	private static readonly int DEFAULT_RESOLUTION = 7;

	private static readonly int DEFAULT_FPS = 30;

	private int _currentResolutionWidth;

	private int _currentResolutionHeight;

	public int resolutionMultiplier = 10;

	public int targetFrameRate = 60;

	private bool shouldCheckResolutionChanged;

	public static QualitySetter Instance;

	// To prevent log spamming on resolution change
	private bool _hasLoggedResolutionChange = false;

	private void Awake()
	{
		Instance = this;
		_ = global::UnityEngine.SystemInfo.systemMemorySize;
		_ = 2047;
	}

	private void Start()
	{
		_initialResolutionWidth = global::UnityEngine.Display.main.systemWidth;
		_initialResolutionHeight = global::UnityEngine.Display.main.systemHeight;
		SetRatio(0.6f, 1f);
	}

	private void InitSavedValues()
	{
		int num = DEFAULT_RESOLUTION;
		if (global::UnityEngine.PlayerPrefs.HasKey("ResolutionMultiplier"))
		{
			num = global::UnityEngine.PlayerPrefs.GetInt("ResolutionMultiplier");
			if (num < 3)
			{
				num = 3;
			}
		}
		SetResolutionMultiplier(num);
		int fPS = DEFAULT_FPS;
		if (global::UnityEngine.PlayerPrefs.HasKey("TargetFrameRate"))
		{
			fPS = global::UnityEngine.PlayerPrefs.GetInt("TargetFrameRate");
		}
		SetFPS(fPS);
	}

	private void SetRatio(float w, float h)
	{
		if ((float)(_initialResolutionWidth / _initialResolutionHeight) > w / h)
		{
			_initialResolutionWidth = (int)((float)_initialResolutionHeight * (w / h));
		}
		else
		{
			_initialResolutionHeight = (int)((float)_initialResolutionWidth * (h / w));
		}
		InitSavedValues();
	}

	public void SetResolutionMultiplier(int multiplier)
	{
		resolutionMultiplier = multiplier;
		global::UnityEngine.PlayerPrefs.SetInt("ResolutionMultiplier", multiplier);
		global::UnityEngine.PlayerPrefs.Save();
		float num = (float)multiplier / 10f;
		global::UnityEngine.Debug.Log("Setting resolution multiplier " + multiplier + " reduced to: " + num);
		global::UnityEngine.Debug.Log("Initial resolution is " + _initialResolutionWidth + "x" + _initialResolutionHeight);
		_currentResolutionWidth = (int)((float)_initialResolutionWidth * num);
		_currentResolutionHeight = (int)((float)_initialResolutionHeight * num);
		SetResolution();
	}

	private void SetResolution()
	{
		shouldCheckResolutionChanged = false;
		global::UnityEngine.Screen.SetResolution(_currentResolutionWidth, _currentResolutionHeight, global::UnityEngine.FullScreenMode.FullScreenWindow);
		shouldCheckResolutionChanged = true;
		_hasLoggedResolutionChange = false; // Reset log flag after setting resolution
	}

	public void SetFPS(int fps)
	{
		targetFrameRate = fps;
		global::UnityEngine.PlayerPrefs.SetInt("TargetFrameRate", fps);
		global::UnityEngine.PlayerPrefs.Save();
	}

	private void Update()
	{
		global::UnityEngine.Application.targetFrameRate = -1;
		if (shouldCheckResolutionChanged && (_currentResolutionWidth != global::UnityEngine.Screen.width || _currentResolutionHeight != global::UnityEngine.Screen.height))
		{
			if (!_hasLoggedResolutionChange)
			{
				OnScreenSizeChanged();
				_hasLoggedResolutionChange = true;
			}
		}
		else
		{
			// If resolution matches, allow logging again on next change
			_hasLoggedResolutionChange = false;
		}
	}

	private void OnScreenSizeChanged()
	{
		global::UnityEngine.Debug.LogError("ResolutionChangeException: Resolution fucking changed help");
		_initialResolutionWidth = global::UnityEngine.Display.main.systemWidth;
		_initialResolutionHeight = global::UnityEngine.Display.main.systemHeight;
		SetRatio(0.6f, 1f);
	}
}
