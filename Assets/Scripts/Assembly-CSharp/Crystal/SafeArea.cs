namespace Crystal
{
	public class SafeArea : global::UnityEngine.MonoBehaviour
	{
		public enum SimDevice
		{
			None = 0,
			iPhoneX = 1,
			iPhoneXsMax = 2,
			Pixel3XL_LSL = 3,
			Pixel3XL_LSR = 4
		}

		public static global::Crystal.SafeArea.SimDevice Sim;

		private global::UnityEngine.Rect[] NSA_iPhoneX = new global::UnityEngine.Rect[2]
		{
			new global::UnityEngine.Rect(0f, 0.04187192f, 1f, 0.9039409f),
			new global::UnityEngine.Rect(0.054187194f, 0.056f, 0.89162564f, 0.944f)
		};

		private global::UnityEngine.Rect[] NSA_iPhoneXsMax = new global::UnityEngine.Rect[2]
		{
			new global::UnityEngine.Rect(0f, 0.03794643f, 1f, 0.9129464f),
			new global::UnityEngine.Rect(0.04910714f, 7f / 138f, 101f / 112f, 131f / 138f)
		};

		private global::UnityEngine.Rect[] NSA_Pixel3XL_LSL = new global::UnityEngine.Rect[2]
		{
			new global::UnityEngine.Rect(0f, 0f, 1f, 0.94222975f),
			new global::UnityEngine.Rect(0f, 0f, 0.94222975f, 1f)
		};

		private global::UnityEngine.Rect[] NSA_Pixel3XL_LSR = new global::UnityEngine.Rect[2]
		{
			new global::UnityEngine.Rect(0f, 0f, 1f, 0.94222975f),
			new global::UnityEngine.Rect(0.05777027f, 0f, 0.94222975f, 1f)
		};

		private global::UnityEngine.RectTransform Panel;

		private global::UnityEngine.Rect LastSafeArea = new global::UnityEngine.Rect(0f, 0f, 0f, 0f);

		private global::UnityEngine.Vector2Int LastScreenSize = new global::UnityEngine.Vector2Int(0, 0);

		private global::UnityEngine.ScreenOrientation LastOrientation = global::UnityEngine.ScreenOrientation.AutoRotation;

		[global::UnityEngine.SerializeField]
		private bool ConformX = true;

		[global::UnityEngine.SerializeField]
		private bool ConformY = true;

		[global::UnityEngine.SerializeField]
		private bool Logging;

		private void Awake()
		{
			Panel = GetComponent<global::UnityEngine.RectTransform>();
			if (Panel == null)
			{
				global::UnityEngine.Debug.LogError("Cannot apply safe area - no RectTransform found on " + base.name);
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			Refresh();
		}

		private void Update()
		{
			Refresh();
		}

		private void Refresh()
		{
			global::UnityEngine.Rect safeArea = GetSafeArea();
			if (safeArea != LastSafeArea || global::UnityEngine.Screen.width != LastScreenSize.x || global::UnityEngine.Screen.height != LastScreenSize.y || global::UnityEngine.Screen.orientation != LastOrientation)
			{
				LastScreenSize.x = global::UnityEngine.Screen.width;
				LastScreenSize.y = global::UnityEngine.Screen.height;
				LastOrientation = global::UnityEngine.Screen.orientation;
				ApplySafeArea(safeArea);
			}
		}

		private global::UnityEngine.Rect GetSafeArea()
		{
			global::UnityEngine.Rect result = global::UnityEngine.Screen.safeArea;
			if (global::UnityEngine.Application.isEditor && Sim != global::Crystal.SafeArea.SimDevice.None)
			{
				global::UnityEngine.Rect rect = new global::UnityEngine.Rect(0f, 0f, global::UnityEngine.Screen.width, global::UnityEngine.Screen.height);
				switch (Sim)
				{
				case global::Crystal.SafeArea.SimDevice.iPhoneX:
					rect = ((global::UnityEngine.Screen.height <= global::UnityEngine.Screen.width) ? NSA_iPhoneX[1] : NSA_iPhoneX[0]);
					break;
				case global::Crystal.SafeArea.SimDevice.iPhoneXsMax:
					rect = ((global::UnityEngine.Screen.height <= global::UnityEngine.Screen.width) ? NSA_iPhoneXsMax[1] : NSA_iPhoneXsMax[0]);
					break;
				case global::Crystal.SafeArea.SimDevice.Pixel3XL_LSL:
					rect = ((global::UnityEngine.Screen.height <= global::UnityEngine.Screen.width) ? NSA_Pixel3XL_LSL[1] : NSA_Pixel3XL_LSL[0]);
					break;
				case global::Crystal.SafeArea.SimDevice.Pixel3XL_LSR:
					rect = ((global::UnityEngine.Screen.height <= global::UnityEngine.Screen.width) ? NSA_Pixel3XL_LSR[1] : NSA_Pixel3XL_LSR[0]);
					break;
				}
				result = new global::UnityEngine.Rect((float)global::UnityEngine.Screen.width * rect.x, (float)global::UnityEngine.Screen.height * rect.y, (float)global::UnityEngine.Screen.width * rect.width, (float)global::UnityEngine.Screen.height * rect.height);
			}
			return result;
		}

		private void ApplySafeArea(global::UnityEngine.Rect r)
		{
			LastSafeArea = r;
			if (!ConformX)
			{
				r.x = 0f;
				r.width = global::UnityEngine.Screen.width;
			}
			if (!ConformY)
			{
				r.y = 0f;
				r.height = global::UnityEngine.Screen.height;
			}
			if (global::UnityEngine.Screen.width > 0 && global::UnityEngine.Screen.height > 0)
			{
				global::UnityEngine.Vector2 position = r.position;
				global::UnityEngine.Vector2 anchorMax = r.position + r.size;
				position.x /= global::UnityEngine.Screen.width;
				position.y /= global::UnityEngine.Screen.height;
				anchorMax.x /= global::UnityEngine.Screen.width;
				anchorMax.y /= global::UnityEngine.Screen.height;
				if (position.x >= 0f && position.y >= 0f && anchorMax.x >= 0f && anchorMax.y >= 0f)
				{
					Panel.anchorMin = position;
					Panel.anchorMax = anchorMax;
				}
			}
			if (Logging)
			{
				global::UnityEngine.Debug.LogFormat("New safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}", base.name, r.x, r.y, r.width, r.height, global::UnityEngine.Screen.width, global::UnityEngine.Screen.height);
			}
		}
	}
}
