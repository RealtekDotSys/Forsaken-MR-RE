public class DiscordTabHandler
{
	private DiscordTabData _discordTabData;

	public DiscordTabHandler(DiscordTabData discordTabData)
	{
		_discordTabData = discordTabData;
	}

	private void GetIcon()
	{
		CoroutineHelper.StartCoroutine(DownloadImage("https://cdn.discordapp.com/avatars/" + _discordTabData.controller.DiscordUserId + "/" + _discordTabData.controller.DiscordAvatarId + ".png"));
		_discordTabData.usernameText.text = _discordTabData.controller.DiscordUsername;
	}

	private global::System.Collections.IEnumerator DownloadImage(string MediaUrl)
	{
		global::UnityEngine.Networking.UnityWebRequest request = global::UnityEngine.Networking.UnityWebRequestTexture.GetTexture(MediaUrl);
		yield return request.SendWebRequest();
		if (request.isNetworkError || request.isHttpError)
		{
			global::UnityEngine.Debug.Log(request.error);
		}
		else
		{
			_discordTabData.profileRawImage.texture = ((global::UnityEngine.Networking.DownloadHandlerTexture)request.downloadHandler).texture;
		}
	}

	public void PopulateDiscordTab()
	{
		GetIcon();
	}

	public void OnDestroy()
	{
	}
}
