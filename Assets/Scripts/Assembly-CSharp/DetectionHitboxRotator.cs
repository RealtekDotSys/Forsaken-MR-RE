public class DetectionHitboxRotator : global::UnityEngine.MonoBehaviour
{
	private global::UnityEngine.Transform animatronicTransform;

	private bool isAnimatronic3DNull;

	private void Start()
	{
		StartCoroutine(FindAnimatronic());
	}

	private global::System.Collections.IEnumerator FindAnimatronic()
	{
		while (global::UnityEngine.GameObject.Find("Animatronic3D") == null)
		{
			yield return null;
		}
		animatronicTransform = global::UnityEngine.GameObject.Find("Animatronic3D").transform;
		isAnimatronic3DNull = false;
		yield return null;
	}

	private void Update()
	{
		if (!isAnimatronic3DNull)
		{
			base.gameObject.transform.LookAt(animatronicTransform, global::UnityEngine.Vector3.up);
			base.transform.eulerAngles = new global::UnityEngine.Vector3(0f, base.transform.eulerAngles.y, 0f);
		}
	}
}
