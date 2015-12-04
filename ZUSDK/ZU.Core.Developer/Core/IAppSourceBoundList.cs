using System;
namespace ZU.Core
{
	public interface IAppSourceBoundList
	{
		int CompareTo(object obj);
		ZU.Collections.ObjectModel.IRangeObservableCollection<IEntity> Entities { get; set; }
		bool Equals(object obj);
		int GetHashCode();
		IEntity ListEntity { get; set; }
	} // interface
} // namespace
