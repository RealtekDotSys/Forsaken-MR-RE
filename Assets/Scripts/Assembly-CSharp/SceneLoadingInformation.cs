public class SceneLoadingInformation
{
	public string bundleName;

	public string assetName;

	public global::System.Action<global::UnityEngine.AsyncOperation> successAction;

	public global::System.Action failureAction;

	public SceneLoadingInformation(string bundle, string asset, global::System.Action<global::UnityEngine.AsyncOperation> success, global::System.Action failure)
	{
		bundleName = bundle;
		assetName = asset;
		successAction = success;
		failureAction = failure;
	}
}
