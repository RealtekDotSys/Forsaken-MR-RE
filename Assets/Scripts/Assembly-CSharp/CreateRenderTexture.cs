[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
public class CreateRenderTexture : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Material iosMaterial;

	public global::UnityEngine.Material androidMaterial;

	private global::UnityEngine.Camera cam;

	private global::UnityEngine.MeshRenderer mr;

	private void Start()
	{
		cam = GetComponent<global::UnityEngine.Camera>();
		float num = cam.fieldOfView * 0.5f * 0.01745329f * 26f;
		CreateQuad(num * cam.aspect, num, 13f);
		SetupCameraRenderTexture();
	}

	private void SetupCameraRenderTexture()
	{
		cam = GetComponent<global::UnityEngine.Camera>();
		cam.targetTexture = new global::UnityEngine.RenderTexture(((global::UnityEngine.Screen.width < 0) ? (global::UnityEngine.Screen.width + 1) : global::UnityEngine.Screen.width) >> 1, ((global::UnityEngine.Screen.height < 0) ? (global::UnityEngine.Screen.height + 1) : global::UnityEngine.Screen.height) >> 1, 24);
		global::UnityEngine.RenderTexture.active = cam.targetTexture;
		mr.material.mainTexture = cam.targetTexture;
	}

	private void CreateQuad(float width, float height, float distance)
	{
		global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject();
		gameObject.transform.parent = base.transform.parent;
		gameObject.transform.localEulerAngles = global::UnityEngine.Vector3.zero;
		gameObject.transform.localScale = global::UnityEngine.Vector3.one * 1.1f;
		gameObject.layer = global::UnityEngine.LayerMask.NameToLayer("PostProcessing");
		gameObject.transform.localPosition = global::UnityEngine.Vector3.zero;
		gameObject.name = "Camera_Background_RenderTexture";
		mr = gameObject.AddComponent<global::UnityEngine.MeshRenderer>();
		mr.material = androidMaterial;
		mr.material.renderQueue = 9998;
		global::UnityEngine.Mesh mesh = new global::UnityEngine.Mesh();
		gameObject.AddComponent<global::UnityEngine.MeshFilter>().mesh = mesh;
		mesh.vertices = new global::UnityEngine.Vector3[4]
		{
			new global::UnityEngine.Vector3(width * -0.5f, height * -0.5f, distance),
			new global::UnityEngine.Vector3(width * 0.5f, height * -0.5f, distance),
			new global::UnityEngine.Vector3(width * -0.5f, height * 0.5f, distance),
			new global::UnityEngine.Vector3(width * 0.5f, height * 0.5f, distance)
		};
		mesh.triangles = new int[6] { 0, 2, 1, 2, 3, 1 };
		mesh.normals = new global::UnityEngine.Vector3[4]
		{
			-global::UnityEngine.Vector3.forward,
			-global::UnityEngine.Vector3.forward,
			-global::UnityEngine.Vector3.forward,
			-global::UnityEngine.Vector3.forward
		};
		mesh.uv = new global::UnityEngine.Vector2[4]
		{
			new global::UnityEngine.Vector2(0f, 0f),
			new global::UnityEngine.Vector2(1f, 0f),
			new global::UnityEngine.Vector2(0f, 1f),
			new global::UnityEngine.Vector2(1f, 1f)
		};
	}
}
