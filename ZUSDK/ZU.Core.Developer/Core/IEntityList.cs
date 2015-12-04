using System;
using ZU.Collections.ObjectModel;
namespace ZU.Core
{
	public interface IEntityList
	{
		int CompareTo(object obj);
		IRangeObservableCollection<IEntity> Entities { get; set; }
		bool Equals(object obj);
		int GetHashCode();
		IEntity ListEntity { get; set; }
	} // interface
} // namespace
