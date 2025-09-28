namespace DigitalRuby.ThunderAndLightning
{
	public class LightningFieldScript : global::DigitalRuby.ThunderAndLightning.LightningBoltPrefabScriptBase
	{
		[global::UnityEngine.Header("Lightning Field Properties")]
		[global::UnityEngine.Tooltip("The minimum length for a field segment")]
		public float MinimumLength = 0.01f;

		private float minimumLengthSquared;

		[global::UnityEngine.Tooltip("The bounds to put the field in.")]
		public global::UnityEngine.Bounds FieldBounds;

		[global::UnityEngine.Tooltip("Optional light for the lightning field to emit")]
		public global::UnityEngine.Light Light;

		private global::UnityEngine.Vector3 RandomPointInBounds()
		{
			float x = global::UnityEngine.Random.Range(FieldBounds.min.x, FieldBounds.max.x);
			float y = global::UnityEngine.Random.Range(FieldBounds.min.y, FieldBounds.max.y);
			float z = global::UnityEngine.Random.Range(FieldBounds.min.z, FieldBounds.max.z);
			return new global::UnityEngine.Vector3(x, y, z);
		}

		protected override void Start()
		{
			base.Start();
			if (Light != null)
			{
				Light.enabled = false;
			}
		}

		protected override void Update()
		{
			base.Update();
			if (Light != null)
			{
				Light.transform.position = FieldBounds.center;
				Light.intensity = global::UnityEngine.Random.Range(2.8f, 3.2f);
			}
		}

		public override void CreateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters parameters)
		{
			minimumLengthSquared = MinimumLength * MinimumLength;
			for (int i = 0; i < 16; i++)
			{
				parameters.Start = RandomPointInBounds();
				parameters.End = RandomPointInBounds();
				if ((parameters.End - parameters.Start).sqrMagnitude >= minimumLengthSquared)
				{
					break;
				}
			}
			if (Light != null)
			{
				Light.enabled = true;
			}
			base.CreateLightningBolt(parameters);
		}
	}
}
