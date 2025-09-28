public class StarDisplay : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> stars;

	public void SetStars(int numStars)
	{
		foreach (global::UnityEngine.GameObject star in stars)
		{
			star.SetActive(stars.IndexOf(star) < numStars);
		}
	}
}
