namespace DigitalRuby.ThunderAndLightning
{
	[global::System.Serializable]
	public struct RangeOfFloats
	{
		[global::UnityEngine.Tooltip("Minimum value (inclusive)")]
		public float Minimum;

		[global::UnityEngine.Tooltip("Maximum value (inclusive)")]
		public float Maximum;

		public float Random()
		{
			return global::UnityEngine.Random.Range(Minimum, Maximum);
		}

		public float Random(global::System.Random r)
		{
			return Minimum + (float)r.NextDouble() * (Maximum - Minimum);
		}
	}
}
