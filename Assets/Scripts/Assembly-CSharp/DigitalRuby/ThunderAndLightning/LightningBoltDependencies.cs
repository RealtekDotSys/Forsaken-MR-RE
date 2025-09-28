namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltDependencies
	{
		public global::UnityEngine.GameObject Parent;

		public global::UnityEngine.Material LightningMaterialMesh;

		public global::UnityEngine.Material LightningMaterialMeshNoGlow;

		public global::UnityEngine.ParticleSystem OriginParticleSystem;

		public global::UnityEngine.ParticleSystem DestParticleSystem;

		public global::UnityEngine.Vector3 CameraPos;

		public bool CameraIsOrthographic;

		public global::DigitalRuby.ThunderAndLightning.CameraMode CameraMode;

		public bool UseWorldSpace;

		public float LevelOfDetailDistance;

		public string SortLayerName;

		public int SortOrderInLayer;

		public global::System.Collections.Generic.ICollection<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters> Parameters;

		public global::DigitalRuby.ThunderAndLightning.LightningThreadState ThreadState;

		public global::System.Func<global::System.Collections.IEnumerator, global::UnityEngine.Coroutine> StartCoroutine;

		public global::System.Action<global::UnityEngine.Light> LightAdded;

		public global::System.Action<global::UnityEngine.Light> LightRemoved;

		public global::System.Action<global::DigitalRuby.ThunderAndLightning.LightningBolt> AddActiveBolt;

		public global::System.Action<global::DigitalRuby.ThunderAndLightning.LightningBoltDependencies> ReturnToCache;

		public global::System.Action<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters, global::UnityEngine.Vector3, global::UnityEngine.Vector3> LightningBoltStarted;

		public global::System.Action<global::DigitalRuby.ThunderAndLightning.LightningBoltParameters, global::UnityEngine.Vector3, global::UnityEngine.Vector3> LightningBoltEnded;
	}
}
