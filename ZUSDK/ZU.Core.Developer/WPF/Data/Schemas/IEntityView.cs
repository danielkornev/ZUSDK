using System;
using System.ComponentModel;
using ZU.ComponentModel;
namespace ZU.WPF.Data.Schemas
{
	public interface IEntityView : INotifyPropertyChanged, IInstantiateProperty, IComparable
	{
		string ActionName { get; set; }
		int CompareTo(object obj);
		ZU.Core.IEntity Entity { get; set; }
		bool Equals(object obj);
		int GetHashCode();
		void InstantiateProperty(string propertyName, System.Globalization.CultureInfo culture, System.Threading.SynchronizationContext callbackExecutionContext);
		bool IsSystem { get; set; }
		string Kind { get; }
		object Thumbnail { get; }
		DateTime TLChange { get; set; }
	} // interface
} // namespace
