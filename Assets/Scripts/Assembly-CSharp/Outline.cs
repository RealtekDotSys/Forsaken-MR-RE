public class Outline : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject meshObject;

	private global::UnityEngine.LineRenderer lineRenderer;

	private global::UnityEngine.Mesh mesh;

	private global::UnityEngine.Vector3[] vertices;

	private void Start()
	{
		mesh = meshObject.GetComponent<global::UnityEngine.MeshFilter>().mesh;
		vertices = mesh.vertices;
		lineRenderer = GetComponent<global::UnityEngine.LineRenderer>();
		lineRenderer.positionCount = vertices.Length;
		lineRenderer.SetPositions(vertices);
	}

	private void Update()
	{
		global::UnityEngine.Vector3 vector = global::UnityEngine.Camera.main.WorldToViewportPoint(meshObject.transform.position);
		if (vector.x > 0f && vector.x < 1f && vector.y > 0f && vector.y < 1f)
		{
			lineRenderer.enabled = true;
		}
		else
		{
			lineRenderer.enabled = false;
		}
	}
}
