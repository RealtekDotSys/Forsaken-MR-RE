namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.IOptionsService))]
	public class OptionsServiceImpl : global::SRDebugger.Services.IOptionsService
	{
		private readonly global::System.Collections.Generic.Dictionary<object, global::System.Collections.Generic.ICollection<global::SRDebugger.Internal.OptionDefinition>> _optionContainerLookup = new global::System.Collections.Generic.Dictionary<object, global::System.Collections.Generic.ICollection<global::SRDebugger.Internal.OptionDefinition>>();

		private readonly global::System.Collections.Generic.List<global::SRDebugger.Internal.OptionDefinition> _options = new global::System.Collections.Generic.List<global::SRDebugger.Internal.OptionDefinition>();

		private readonly global::System.Collections.Generic.IList<global::SRDebugger.Internal.OptionDefinition> _optionsReadonly;

		public global::System.Collections.Generic.ICollection<global::SRDebugger.Internal.OptionDefinition> Options => _optionsReadonly;

		public event global::System.EventHandler OptionsUpdated;

		public event global::System.EventHandler<global::System.ComponentModel.PropertyChangedEventArgs> OptionsValueUpdated;

		public OptionsServiceImpl()
		{
			_optionsReadonly = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::SRDebugger.Internal.OptionDefinition>(_options);
			Scan(SROptions.Current);
			SROptions.Current.PropertyChanged += OnSROptionsPropertyChanged;
		}

		public void Scan(object obj)
		{
			AddContainer(obj);
		}

		public void AddContainer(object obj)
		{
			if (_optionContainerLookup.ContainsKey(obj))
			{
				throw new global::System.Exception("An object should only be added once.");
			}
			global::System.Collections.Generic.ICollection<global::SRDebugger.Internal.OptionDefinition> collection = global::SRDebugger.Internal.SRDebuggerUtil.ScanForOptions(obj);
			_optionContainerLookup.Add(obj, collection);
			if (collection.Count > 0)
			{
				_options.AddRange(collection);
				OnOptionsUpdated();
				if (obj is global::System.ComponentModel.INotifyPropertyChanged notifyPropertyChanged)
				{
					notifyPropertyChanged.PropertyChanged += OnPropertyChanged;
				}
			}
		}

		public void RemoveContainer(object obj)
		{
			if (!_optionContainerLookup.ContainsKey(obj))
			{
				return;
			}
			global::System.Collections.Generic.ICollection<global::SRDebugger.Internal.OptionDefinition> collection = _optionContainerLookup[obj];
			_optionContainerLookup.Remove(obj);
			foreach (global::SRDebugger.Internal.OptionDefinition item in collection)
			{
				_options.Remove(item);
			}
			if (obj is global::System.ComponentModel.INotifyPropertyChanged notifyPropertyChanged)
			{
				notifyPropertyChanged.PropertyChanged -= OnPropertyChanged;
			}
			OnOptionsUpdated();
		}

		private void OnPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (this.OptionsValueUpdated != null)
			{
				this.OptionsValueUpdated(this, propertyChangedEventArgs);
			}
		}

		private void OnSROptionsPropertyChanged(object sender, string propertyName)
		{
			OnPropertyChanged(sender, new global::System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}

		private void OnOptionsUpdated()
		{
			if (this.OptionsUpdated != null)
			{
				this.OptionsUpdated(this, global::System.EventArgs.Empty);
			}
		}
	}
}
