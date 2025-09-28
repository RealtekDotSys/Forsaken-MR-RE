internal class AudioSourceWatcher
{
	private global::UnityEngine.AudioSource[] audioSources;

	public AudioSourceWatcher(global::UnityEngine.GameObject gameObject)
	{
		audioSources = gameObject.GetComponentsInChildren<global::UnityEngine.AudioSource>();
	}

	public bool AllAudioSourcesFinished()
	{
		int num = 0;
		do
		{
			if (num >= audioSources.Length)
			{
				return true;
			}
			num++;
		}
		while (!audioSources[0].isPlaying);
		return false;
	}
}
