public class LoadingAnimatronics : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject[] animatronics;

	private global::UnityEngine.GameObject activeAnimatronic;

	private void Start()
	{
		int num = (int)global::UnityEngine.Random.Range(0f, animatronics.Length);
		activeAnimatronic = animatronics[num];
		activeAnimatronic.SetActive(value: true);
	}
}
