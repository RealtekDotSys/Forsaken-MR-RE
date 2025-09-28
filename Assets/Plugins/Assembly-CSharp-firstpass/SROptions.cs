public class SROptions
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property)]
	public sealed class NumberRangeAttribute : global::System.Attribute
	{
		public readonly double Max;

		public readonly double Min;

		public NumberRangeAttribute(double min, double max)
		{
			Min = min;
			Max = max;
		}
	}

	[global::System.AttributeUsage(global::System.AttributeTargets.Property)]
	public sealed class IncrementAttribute : global::System.Attribute
	{
		public readonly double Increment;

		public IncrementAttribute(double increment)
		{
			Increment = increment;
		}
	}

	[global::System.AttributeUsage(global::System.AttributeTargets.Method | global::System.AttributeTargets.Property)]
	public sealed class SortAttribute : global::System.Attribute
	{
		public readonly int SortPriority;

		public SortAttribute(int priority)
		{
			SortPriority = priority;
		}
	}

	[global::System.AttributeUsage(global::System.AttributeTargets.Method | global::System.AttributeTargets.Property)]
	public sealed class DisplayNameAttribute : global::System.Attribute
	{
		public readonly string Name;

		public DisplayNameAttribute(string name)
		{
			Name = name;
		}
	}

	private static readonly SROptions _current = new SROptions();

	public static SROptions Current => _current;

	public event SROptionsPropertyChanged PropertyChanged;

	public void OnPropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, propertyName);
		}
	}
}
