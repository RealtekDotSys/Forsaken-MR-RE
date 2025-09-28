namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltPathScript : global::DigitalRuby.ThunderAndLightning.LightningBoltPathScriptBase
	{
		[global::UnityEngine.Tooltip("How fast the lightning moves through the points or objects. 1 is normal speed, 0.01 is slower, so the lightning will move slowly between the points or objects.")]
		[global::UnityEngine.Range(0.01f, 1f)]
		public float Speed = 1f;

		[global::DigitalRuby.ThunderAndLightning.SingleLineClamp("When each new point is moved to, this can provide a random value to make the movement to the next point appear more staggered or random. Leave as 1 and 1 to have constant speed. Use a higher maximum to create more randomness.", 1.0, 500.0)]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats SpeedIntervalRange = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 1f,
			Maximum = 1f
		};

		[global::UnityEngine.Tooltip("Repeat when the path completes?")]
		public bool Repeat = true;

		private float nextInterval = 1f;

		private int nextIndex;

		private global::UnityEngine.Vector3? lastPoint;

		public override void CreateLightningBolt(global::DigitalRuby.ThunderAndLightning.LightningBoltParameters parameters)
		{
			global::UnityEngine.Vector3? vector = null;
			global::System.Collections.Generic.List<global::UnityEngine.GameObject> list = GetCurrentPathObjects();
			if (list.Count < 2)
			{
				return;
			}
			if (nextIndex >= list.Count)
			{
				if (!Repeat)
				{
					return;
				}
				if (list[list.Count - 1] == list[0])
				{
					nextIndex = 1;
				}
				else
				{
					nextIndex = 0;
					lastPoint = null;
				}
			}
			try
			{
				if (!lastPoint.HasValue)
				{
					lastPoint = list[nextIndex++].transform.position;
				}
				vector = list[nextIndex].transform.position;
				if (lastPoint.HasValue && vector.HasValue)
				{
					parameters.Start = lastPoint.Value;
					parameters.End = vector.Value;
					base.CreateLightningBolt(parameters);
					if ((nextInterval -= Speed) <= 0f)
					{
						float num = global::UnityEngine.Random.Range(SpeedIntervalRange.Minimum, SpeedIntervalRange.Maximum);
						nextInterval = num + nextInterval;
						lastPoint = vector;
						nextIndex++;
					}
				}
			}
			catch (global::System.NullReferenceException)
			{
			}
		}

		public void Reset()
		{
			lastPoint = null;
			nextIndex = 0;
			nextInterval = 1f;
		}
	}
}
