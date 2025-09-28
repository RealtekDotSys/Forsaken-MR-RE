public class IntroScreenBehavior : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Animator animator;

	private AudioPlayer audioPlayer;

	public void Activate(global::UnityEngine.UI.Button button)
	{
		animator.SetTrigger("Play");
		button.onClick.AddListener(NextCard);
		audioPlayer = MasterDomain.GetDomain().GameAudioDomain.AudioPlayer;
	}

	private void NextCard()
	{
		animator.SetTrigger("Next");
		if (audioPlayer != null)
		{
			audioPlayer.RaiseGameEventForMode(AudioEventName.CameraIntroCard, AudioMode.Global);
		}
	}
}
