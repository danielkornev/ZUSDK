using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ZU.Collections.ObjectModel
{
	public interface IRangeObservableCollection<T> : IList<T>
	{
		void Clear();
		void AddRange(System.Collections.Generic.IEnumerable<T> list);
		void NotifyRefreshCollection();
		event NotifyCollectionChangedEventHandler CollectionChanged;
	} // interface
} // namespace
