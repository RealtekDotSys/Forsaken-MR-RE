namespace DigitalRuby.ThunderAndLightning
{
	public abstract class LightningBoltPathScriptBase : global::DigitalRuby.ThunderAndLightning.LightningBoltPrefabScriptBase
	{
		[global::UnityEngine.Header("Lightning Path Properties")]
		[global::UnityEngine.Tooltip("The game objects to follow for the lightning path")]
		public global::System.Collections.Generic.List<global::UnityEngine.GameObject> LightningPath;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.GameObject> currentPathObjects = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

		protected global::System.Collections.Generic.List<global::UnityEngine.GameObject> GetCurrentPathObjects()
		{
			currentPathObjects.Clear();
			if (LightningPath != null)
			{
				foreach (global::UnityEngine.GameObject item in LightningPath)
				{
					if (item != null && item.activeInHierarchy)
					{
						currentPathObjects.Add(item);
					}
				}
			}
			return currentPathObjects;
		}

		protected override global::DigitalRuby.ThunderAndLightning.LightningBoltParameters OnCreateParameters()
		{
			global::DigitalRuby.ThunderAndLightning.LightningBoltParameters lightningBoltParameters = base.OnCreateParameters();
			lightningBoltParameters.Generator = global::DigitalRuby.ThunderAndLightning.LightningGenerator.GeneratorInstance;
			return lightningBoltParameters;
		}
	}
}
