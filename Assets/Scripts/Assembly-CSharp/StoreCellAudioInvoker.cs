public class StoreCellAudioInvoker : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private StoreCell cell;

	private AudioPlayer _audioPlayer;

	private void InvokeAudio()
	{
		_audioPlayer.RaiseGameEventForModeByName(cell.GetAudioEventName(), AudioMode.Global);
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
