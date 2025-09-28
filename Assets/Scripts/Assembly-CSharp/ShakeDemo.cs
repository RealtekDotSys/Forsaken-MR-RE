public class ShakeDemo : global::UnityEngine.MonoBehaviour
{
	private delegate float Slider(float val, string prefix, float min, float max, int pad);

	private global::UnityEngine.Vector3 posInf = new global::UnityEngine.Vector3(0.25f, 0.25f, 0.25f);

	private global::UnityEngine.Vector3 rotInf = new global::UnityEngine.Vector3(1f, 1f, 1f);

	private float magn = 1f;

	private float rough = 1f;

	private float fadeIn = 0.1f;

	private float fadeOut = 2f;

	private bool modValues;

	private bool showList;

	private global::EZCameraShake.CameraShakeInstance shake;

	private void OnGUI()
	{
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.R))
		{
			global::UnityEngine.SceneManagement.SceneManager.LoadScene(global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		}
		ShakeDemo.Slider slider = delegate(float val, string prefix, float min, float max, int pad)
		{
			global::UnityEngine.GUILayout.BeginHorizontal();
			global::UnityEngine.GUILayout.Label(prefix, global::UnityEngine.GUILayout.MaxWidth(pad));
			val = global::UnityEngine.GUILayout.HorizontalSlider(val, min, max);
			global::UnityEngine.GUILayout.Label(val.ToString("F2"), global::UnityEngine.GUILayout.MaxWidth(50f));
			global::UnityEngine.GUILayout.EndHorizontal();
			return val;
		};
		global::UnityEngine.GUI.Box(new global::UnityEngine.Rect(10f, 10f, 250f, global::UnityEngine.Screen.height - 15), "Make-A-Shake");
		global::UnityEngine.GUILayout.BeginArea(new global::UnityEngine.Rect(29f, 40f, 215f, global::UnityEngine.Screen.height - 40));
		global::UnityEngine.GUILayout.Label("--Position Infleunce--");
		posInf.x = slider(posInf.x, "X", 0f, 4f, 25);
		posInf.y = slider(posInf.y, "Y", 0f, 4f, 25);
		posInf.z = slider(posInf.z, "Z", 0f, 4f, 25);
		global::UnityEngine.GUILayout.Label("--Rotation Infleunce--");
		rotInf.x = slider(rotInf.x, "X", 0f, 4f, 25);
		rotInf.y = slider(rotInf.y, "Y", 0f, 4f, 25);
		rotInf.z = slider(rotInf.z, "Z", 0f, 4f, 25);
		global::UnityEngine.GUILayout.Label("--Other Properties--");
		magn = slider(magn, "Magnitude:", 0f, 10f, 75);
		rough = slider(rough, "Roughness:", 0f, 20f, 75);
		fadeIn = slider(fadeIn, "Fade In:", 0f, 10f, 75);
		fadeOut = slider(fadeOut, "Fade Out:", 0f, 10f, 75);
		global::UnityEngine.GUILayout.Label("--Saved Shake Instance--");
		global::UnityEngine.GUILayout.Label("You can save shake instances and modify their properties at runtime.");
		if (shake == null && global::UnityEngine.GUILayout.Button("Create Shake Instance"))
		{
			shake = global::EZCameraShake.CameraShaker.Instance.StartShake(magn, rough, fadeIn);
			shake.DeleteOnInactive = false;
		}
		if (shake != null)
		{
			if (global::UnityEngine.GUILayout.Button("Delete Shake Instance"))
			{
				shake.DeleteOnInactive = true;
				shake.StartFadeOut(fadeOut);
				shake = null;
			}
			if (shake != null)
			{
				global::UnityEngine.GUILayout.BeginHorizontal();
				if (global::UnityEngine.GUILayout.Button("Fade Out"))
				{
					shake.StartFadeOut(fadeOut);
				}
				if (global::UnityEngine.GUILayout.Button("Fade In"))
				{
					shake.StartFadeIn(fadeIn);
				}
				global::UnityEngine.GUILayout.EndHorizontal();
				modValues = global::UnityEngine.GUILayout.Toggle(modValues, "Modify Instance Values");
				if (modValues)
				{
					shake.ScaleMagnitude = magn;
					shake.ScaleRoughness = rough;
					shake.PositionInfluence = posInf;
					shake.RotationInfluence = rotInf;
				}
			}
		}
		global::UnityEngine.GUILayout.Label("--Shake Once--");
		global::UnityEngine.GUILayout.Label("You can simply have a shake that automatically starts and stops too.");
		if (global::UnityEngine.GUILayout.Button("Shake!"))
		{
			global::EZCameraShake.CameraShakeInstance cameraShakeInstance = global::EZCameraShake.CameraShaker.Instance.ShakeOnce(magn, rough, fadeIn, fadeOut);
			cameraShakeInstance.PositionInfluence = posInf;
			cameraShakeInstance.RotationInfluence = rotInf;
		}
		global::UnityEngine.GUILayout.EndArea();
		global::UnityEngine.GUI.Box(new global::UnityEngine.Rect(height: showList ? (120f + (float)global::EZCameraShake.CameraShaker.Instance.ShakeInstances.Count * 130f) : 120f, x: global::UnityEngine.Screen.width - 310, y: 10f, width: 300f), "Shake Instance List");
		global::UnityEngine.GUILayout.BeginArea(new global::UnityEngine.Rect(global::UnityEngine.Screen.width - 285, 40f, 255f, global::UnityEngine.Screen.height - 40));
		global::UnityEngine.GUILayout.Label("All shake instances are saved and stacked as long as they are active.");
		showList = global::UnityEngine.GUILayout.Toggle(showList, "Show List");
		if (showList)
		{
			int num = 1;
			foreach (global::EZCameraShake.CameraShakeInstance shakeInstance in global::EZCameraShake.CameraShaker.Instance.ShakeInstances)
			{
				string text = shakeInstance.CurrentState.ToString();
				global::UnityEngine.GUILayout.Label("#" + num + ": Magnitude: " + shakeInstance.Magnitude.ToString("F2") + ", Roughness: " + shakeInstance.Roughness.ToString("F2"));
				global::UnityEngine.Vector3 positionInfluence = shakeInstance.PositionInfluence;
				global::UnityEngine.GUILayout.Label("      Position Influence: " + positionInfluence.ToString());
				positionInfluence = shakeInstance.RotationInfluence;
				global::UnityEngine.GUILayout.Label("      Rotation Influence: " + positionInfluence.ToString());
				global::UnityEngine.GUILayout.Label("      State: " + text);
				global::UnityEngine.GUILayout.Label("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
				num++;
			}
		}
		global::UnityEngine.GUILayout.EndArea();
	}
}
