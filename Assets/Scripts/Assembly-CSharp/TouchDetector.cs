public class TouchDetector
{
	private global::UnityEngine.Vector2 _lastPosition;

	private global::System.Action<global::UnityEngine.Vector2> _onTouchDetected;

	public TouchDetector(global::System.Action<global::UnityEngine.Vector2> onTouchDetected)
	{
		_lastPosition = global::UnityEngine.Vector3.zero;
		_onTouchDetected = onTouchDetected;
	}

	public void Update()
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.IPhonePlayer)
		{
			if (global::UnityEngine.Input.touchCount <= 0)
			{
				return;
			}
			global::UnityEngine.Touch[] touches = global::UnityEngine.Input.touches;
			for (int i = 0; i < touches.Length; i++)
			{
				global::UnityEngine.Touch touch = touches[i];
				_lastPosition = touch.position;
				if (_onTouchDetected != null && touch.phase == global::UnityEngine.TouchPhase.Began)
				{
					_onTouchDetected(_lastPosition);
				}
			}
		}
		else if ((global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.WindowsEditor || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.OSXEditor || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.WindowsPlayer || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.OSXPlayer) && global::UnityEngine.Input.GetMouseButtonDown(0))
		{
			_lastPosition = global::UnityEngine.Input.mousePosition;
			if (_onTouchDetected != null)
			{
				_onTouchDetected(_lastPosition);
			}
		}
	}
}
