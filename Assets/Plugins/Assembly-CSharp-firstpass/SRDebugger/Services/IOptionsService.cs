namespace SRDebugger.Services
{
	public interface IOptionsService
	{
		global::System.Collections.Generic.ICollection<global::SRDebugger.Internal.OptionDefinition> Options { get; }

		event global::System.EventHandler OptionsUpdated;

		event global::System.EventHandler<global::System.ComponentModel.PropertyChangedEventArgs> OptionsValueUpdated;

		[global::System.Obsolete("Use IOptionsService.AddContainer instead.")]
		void Scan(object obj);

		void AddContainer(object obj);

		void RemoveContainer(object obj);
	}
}
