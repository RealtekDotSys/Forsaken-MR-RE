namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltPrefabScript : global::DigitalRuby.ThunderAndLightning.LightningBoltPrefabScriptBase
	{
		[global::UnityEngine.Header("Start/end")]
		[global::UnityEngine.Tooltip("The source game object, can be null")]
		public global::UnityEngine.GameObject Source;

		[global::UnityEngine.Tooltip("The destination game object, can be null")]
		public global::UnityEngine.GameObject Destination;

		[global::UnityEngine.Tooltip("X, Y and Z for variance from the start point. Use positive values.")]
		public global::UnityEngine.Vector3 StartVariance;

		[global::UnityEngine.Tooltip("X, Y and Z for variance from the end point. Use positive values.")]
		public global::UnityEngine.Vector3 EndVariance;

		public override void CreateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters parameters)
		{
			parameters.Start = ((Source == null) ? parameters.Start : Source.transform.position);
			parameters.End = ((Destination == null) ? parameters.End : Destination.transform.position);
			parameters.StartVariance = StartVariance;
			parameters.EndVariance = EndVariance;
			base.CreateLightningBolt(parameters);
		}
	}
}
