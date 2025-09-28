public class OCCreatorObject
{
	public global::UnityEngine.PrimitiveType meshType;

	public string ParentName;

	public global::UnityEngine.Color color;

	public global::UnityEngine.Vector3 position = global::UnityEngine.Vector3.zero;

	public global::UnityEngine.Quaternion rotation = global::UnityEngine.Quaternion.identity;

	public global::UnityEngine.Vector3 scale = global::UnityEngine.Vector3.one;

	public OCCreatorObject(global::UnityEngine.PrimitiveType type, string parent)
	{
		meshType = type;
		if (parent != null)
		{
			ParentName = parent;
		}
	}
}
