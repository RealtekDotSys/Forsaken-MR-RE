namespace DigitalRuby.ThunderAndLightning
{
	public class LightningLightsabreScript : global::DigitalRuby.ThunderAndLightning.LightningBoltPrefabScript
	{
		[global::UnityEngine.Header("Lightsabre Properties")]
		[global::UnityEngine.Tooltip("Height of the blade")]
		public float BladeHeight = 19f;

		[global::UnityEngine.Tooltip("How long it takes to turn the lightsabre on and off")]
		public float ActivationTime = 0.5f;

		[global::UnityEngine.Tooltip("Sound to play when the lightsabre turns on")]
		public global::UnityEngine.AudioSource StartSound;

		[global::UnityEngine.Tooltip("Sound to play when the lightsabre turns off")]
		public global::UnityEngine.AudioSource StopSound;

		[global::UnityEngine.Tooltip("Sound to play when the lightsabre stays on")]
		public global::UnityEngine.AudioSource ConstantSound;

		private int state;

		private global::UnityEngine.Vector3 bladeStart;

		private global::UnityEngine.Vector3 bladeDir;

		private float bladeTime;

		private float bladeIntensity;

		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			if (state == 2 || state == 3)
			{
				bladeTime += global::DigitalRuby.ThunderAndLightning.LightningBoltScript.DeltaTime;
				float num = global::UnityEngine.Mathf.Lerp(0.01f, 1f, bladeTime / ActivationTime);
				global::UnityEngine.Vector3 position = bladeStart + bladeDir * num * BladeHeight;
				Destination.transform.position = position;
				GlowIntensity = bladeIntensity * ((state == 3) ? num : (1f - num));
				if (bladeTime >= ActivationTime)
				{
					GlowIntensity = bladeIntensity;
					bladeTime = 0f;
					if (state == 2)
					{
						ManualMode = true;
						state = 0;
					}
					else
					{
						state = 1;
					}
				}
			}
			base.Update();
		}

		public bool TurnOn(bool value)
		{
			if (state == 2 || state == 3 || (state == 1 && value) || (state == 0 && !value))
			{
				return false;
			}
			bladeStart = Destination.transform.position;
			ManualMode = false;
			bladeIntensity = GlowIntensity;
			if (value)
			{
				bladeDir = (Camera.orthographic ? base.transform.up : base.transform.forward);
				state = 3;
				StartSound.Play();
				StopSound.Stop();
				ConstantSound.Play();
			}
			else
			{
				bladeDir = -(Camera.orthographic ? base.transform.up : base.transform.forward);
				state = 2;
				StartSound.Stop();
				StopSound.Play();
				ConstantSound.Stop();
			}
			return true;
		}

		public void TurnOnGUI(bool value)
		{
			TurnOn(value);
		}
	}
}
