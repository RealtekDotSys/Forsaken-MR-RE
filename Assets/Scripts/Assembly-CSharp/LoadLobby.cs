public class LoadLobby : global::UnityEngine.MonoBehaviour
{
	public void LoadLobbyScene()
	{
		MasterDomain.GetDomain().TheGameDomain.gameDisplayChanger.RequestDisplayChange(GameDisplayData.DisplayType.map);
	}

	public void LoadModelViewer()
	{
		global::UnityEngine.SceneManagement.SceneManager.LoadScene(3);
	}

	public void LoadCustomNight()
	{
		global::UnityEngine.SceneManagement.SceneManager.LoadScene(6);
	}
}
