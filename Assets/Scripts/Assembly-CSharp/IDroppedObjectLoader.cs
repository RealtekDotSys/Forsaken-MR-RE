public interface IDroppedObjectLoader
{
	void Load(global::UnityEngine.Transform parent, global::System.Action<global::System.Collections.Generic.List<DroppedObject>> onComplete);

	void Unload();
}
