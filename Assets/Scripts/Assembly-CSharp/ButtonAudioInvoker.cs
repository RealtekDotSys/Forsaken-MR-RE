public class ButtonAudioInvoker : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private string eventName;

	private AudioPlayer _audioPlayer;

	private void InvokeAudio()
	{
		_audioPlayer.RaiseGameEventForModeByName(eventName, AudioMode.Global);
	}

	public void RetrieveAndInvokeAudio()
	{
		if (_audioPlayer != null)
		{
			InvokeAudio();
			return;
		}
		_audioPlayer = MasterDomain.GetDomain().GameAudioDomain.AudioPlayer;
		InvokeAudio();
	}
}
