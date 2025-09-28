namespace DigitalRuby.ThunderAndLightning
{
	public class WaitForSecondsLightning : global::UnityEngine.CustomYieldInstruction
	{
		private float remaining;

		public override bool keepWaiting
		{
			get
			{
				if (remaining <= 0f)
				{
					return false;
				}
				remaining -= global::DigitalRuby.ThunderAndLightning.LightningBoltScript.DeltaTime;
				return true;
			}
		}

		public WaitForSecondsLightning(float time)
		{
			remaining = time;
		}
	}
}
