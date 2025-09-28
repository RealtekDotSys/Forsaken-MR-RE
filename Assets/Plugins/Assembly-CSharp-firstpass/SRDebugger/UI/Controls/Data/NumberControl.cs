namespace SRDebugger.UI.Controls.Data
{
	public class NumberControl : global::SRDebugger.UI.Controls.DataBoundControl
	{
		public struct ValueRange
		{
			public double MaxValue;

			public double MinValue;
		}

		private static readonly global::System.Type[] IntegerTypes = new global::System.Type[6]
		{
			typeof(int),
			typeof(short),
			typeof(byte),
			typeof(sbyte),
			typeof(uint),
			typeof(ushort)
		};

		private static readonly global::System.Type[] DecimalTypes = new global::System.Type[2]
		{
			typeof(float),
			typeof(double)
		};

		public static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::SRDebugger.UI.Controls.Data.NumberControl.ValueRange> ValueRanges = new global::System.Collections.Generic.Dictionary<global::System.Type, global::SRDebugger.UI.Controls.Data.NumberControl.ValueRange>
		{
			{
				typeof(int),
				new global::SRDebugger.UI.Controls.Data.NumberControl.ValueRange
				{
					MaxValue = 2147483647.0,
					MinValue = -2147483648.0
				}
			},
			{
				typeof(short),
				new global::SRDebugger.UI.Controls.Data.NumberControl.ValueRange
				{
					MaxValue = 32767.0,
					MinValue = -32768.0
				}
			},
			{
				typeof(byte),
				new global::SRDebugger.UI.Controls.Data.NumberControl.ValueRange
				{
					MaxValue = 255.0,
					MinValue = 0.0
				}
			},
			{
				typeof(sbyte),
				new global::SRDebugger.UI.Controls.Data.NumberControl.ValueRange
				{
					MaxValue = 127.0,
					MinValue = -128.0
				}
			},
			{
				typeof(uint),
				new global::SRDebugger.UI.Controls.Data.NumberControl.ValueRange
				{
					MaxValue = 4294967295.0,
					MinValue = 0.0
				}
			},
			{
				typeof(ushort),
				new global::SRDebugger.UI.Controls.Data.NumberControl.ValueRange
				{
					MaxValue = 65535.0,
					MinValue = 0.0
				}
			},
			{
				typeof(float),
				new global::SRDebugger.UI.Controls.Data.NumberControl.ValueRange
				{
					MaxValue = 3.4028234663852886E+38,
					MinValue = -3.4028234663852886E+38
				}
			},
			{
				typeof(double),
				new global::SRDebugger.UI.Controls.Data.NumberControl.ValueRange
				{
					MaxValue = double.MaxValue,
					MinValue = double.MinValue
				}
			}
		};

		private string _lastValue;

		private global::System.Type _type;

		public global::UnityEngine.GameObject[] DisableOnReadOnly;

		public global::SRF.UI.SRNumberButton DownNumberButton;

		[global::SRF.RequiredField]
		public global::SRF.UI.SRNumberSpinner NumberSpinner;

		[global::SRF.RequiredField]
		public global::UnityEngine.UI.Text Title;

		public global::SRF.UI.SRNumberButton UpNumberButton;

		protected override void Start()
		{
			base.Start();
			NumberSpinner.onEndEdit.AddListener(OnValueChanged);
		}

		private void OnValueChanged(string newValue)
		{
			try
			{
				object newValue2 = global::System.Convert.ChangeType(newValue, _type);
				UpdateValue(newValue2);
			}
			catch (global::System.Exception)
			{
				NumberSpinner.text = _lastValue;
			}
		}

		protected override void OnBind(string propertyName, global::System.Type t)
		{
			base.OnBind(propertyName, t);
			Title.text = propertyName;
			if (IsIntegerType(t))
			{
				NumberSpinner.contentType = global::UnityEngine.UI.InputField.ContentType.IntegerNumber;
			}
			else
			{
				if (!IsDecimalType(t))
				{
					throw new global::System.ArgumentException("Type must be one of expected types", "t");
				}
				NumberSpinner.contentType = global::UnityEngine.UI.InputField.ContentType.DecimalNumber;
			}
			SROptions.NumberRangeAttribute attribute = base.Property.GetAttribute<SROptions.NumberRangeAttribute>();
			NumberSpinner.MaxValue = GetMaxValue(t);
			NumberSpinner.MinValue = GetMinValue(t);
			if (attribute != null)
			{
				NumberSpinner.MaxValue = global::System.Math.Min(attribute.Max, NumberSpinner.MaxValue);
				NumberSpinner.MinValue = global::System.Math.Max(attribute.Min, NumberSpinner.MinValue);
			}
			SROptions.IncrementAttribute attribute2 = base.Property.GetAttribute<SROptions.IncrementAttribute>();
			if (attribute2 != null)
			{
				if (UpNumberButton != null)
				{
					UpNumberButton.Amount = attribute2.Increment;
				}
				if (DownNumberButton != null)
				{
					DownNumberButton.Amount = 0.0 - attribute2.Increment;
				}
			}
			_type = t;
			NumberSpinner.interactable = !base.IsReadOnly;
			if (DisableOnReadOnly != null)
			{
				global::UnityEngine.GameObject[] disableOnReadOnly = DisableOnReadOnly;
				for (int i = 0; i < disableOnReadOnly.Length; i++)
				{
					disableOnReadOnly[i].SetActive(!base.IsReadOnly);
				}
			}
		}

		protected override void OnValueUpdated(object newValue)
		{
			string text = global::System.Convert.ToDecimal(newValue).ToString();
			if (text != _lastValue)
			{
				NumberSpinner.text = text;
			}
			_lastValue = text;
		}

		public override bool CanBind(global::System.Type type, bool isReadOnly)
		{
			if (!IsDecimalType(type))
			{
				return IsIntegerType(type);
			}
			return true;
		}

		protected static bool IsIntegerType(global::System.Type t)
		{
			for (int i = 0; i < IntegerTypes.Length; i++)
			{
				if (IntegerTypes[i] == t)
				{
					return true;
				}
			}
			return false;
		}

		protected static bool IsDecimalType(global::System.Type t)
		{
			for (int i = 0; i < DecimalTypes.Length; i++)
			{
				if (DecimalTypes[i] == t)
				{
					return true;
				}
			}
			return false;
		}

		protected double GetMaxValue(global::System.Type t)
		{
			if (ValueRanges.TryGetValue(t, out var value))
			{
				return value.MaxValue;
			}
			global::UnityEngine.Debug.LogWarning(global::SRF.SRFStringExtensions.Fmt("[NumberControl] No MaxValue stored for type {0}", t));
			return double.MaxValue;
		}

		protected double GetMinValue(global::System.Type t)
		{
			if (ValueRanges.TryGetValue(t, out var value))
			{
				return value.MinValue;
			}
			global::UnityEngine.Debug.LogWarning(global::SRF.SRFStringExtensions.Fmt("[NumberControl] No MinValue stored for type {0}", t));
			return double.MinValue;
		}
	}
}
