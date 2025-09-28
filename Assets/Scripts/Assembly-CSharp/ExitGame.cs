public class ExitGame : global::UnityEngine.MonoBehaviour
{
	private float heldTime;

	private bool isExiting;

	private void Update()
	{
		if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.Escape))
		{
			heldTime += global::UnityEngine.Time.deltaTime;
			if (heldTime >= 3f && !isExiting)
			{
				isExiting = true;
				global::UnityEngine.Debug.Log("Exiting game...");
				ExitTheGame();
			}
		}
		else
		{
			heldTime = 0f;
			isExiting = false;
		}
	}

	private void ExitTheGame()
	{
		global::UnityEngine.Application.Quit();
	}
}
