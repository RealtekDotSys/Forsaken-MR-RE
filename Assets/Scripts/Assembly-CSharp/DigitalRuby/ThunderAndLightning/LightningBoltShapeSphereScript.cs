namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltShapeSphereScript : global::DigitalRuby.ThunderAndLightning.LightningBoltPrefabScriptBase
	{
		[global::UnityEngine.Header("Lightning Sphere Properties")]
		[global::UnityEngine.Tooltip("Radius inside the sphere where lightning can emit from")]
		public float InnerRadius = 0.1f;

		[global::UnityEngine.Tooltip("Radius of the sphere")]
		public float Radius = 4f;

		public override void CreateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters parameters)
		{
			global::UnityEngine.Vector3 start = global::UnityEngine.Random.insideUnitSphere * InnerRadius;
			global::UnityEngine.Vector3 end = global::UnityEngine.Random.onUnitSphere * Radius;
			parameters.Start = start;
			parameters.End = end;
			base.CreateLightningBolt(parameters);
		}
	}
}
