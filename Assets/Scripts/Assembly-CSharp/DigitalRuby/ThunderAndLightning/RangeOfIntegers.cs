namespace DigitalRuby.ThunderAndLightning
{
	[global::System.Serializable]
	public struct RangeOfIntegers
	{
		[global::UnityEngine.Tooltip("Minimum value (inclusive)")]
		public int Minimum;

		[global::UnityEngine.Tooltip("Maximum value (inclusive)")]
		public int Maximum;

		public int Random()
		{
			return global::UnityEngine.Random.Range(Minimum, Maximum + 1);
		}

		public int Random(global::System.Random r)
		{
			return r.Next(Minimum, Maximum + 1);
		}
	}
}
