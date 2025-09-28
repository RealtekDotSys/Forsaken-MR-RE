public class EncounterEnvironment : global::UnityEngine.MonoBehaviour
{
	public global::System.Collections.Generic.List<global::UnityEngine.Transform> droppablePositions = new global::System.Collections.Generic.List<global::UnityEngine.Transform>();

	public global::System.Collections.Generic.List<global::UnityEngine.Light> haywireLights = new global::System.Collections.Generic.List<global::UnityEngine.Light>();

	public void SetLightColorMode(global::UnityEngine.Color lightColor)
	{
		foreach (global::UnityEngine.Light haywireLight in haywireLights)
		{
			haywireLight.color = lightColor;
		}
	}
}
