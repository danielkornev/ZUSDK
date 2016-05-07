using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ZU.Collections.ObjectModel
{
	/// <summary>
	/// This class supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public interface IRangeObservableCollection<T> : IList<T>
	{
		void Clear();
		void AddRange(System.Collections.Generic.IEnumerable<T> list);
		void NotifyRefreshCollection();
		event NotifyCollectionChangedEventHandler CollectionChanged;
	} // interface
} // namespace
