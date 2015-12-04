using System;
using System.Collections.Generic;
using System.Linq;
namespace ZU.Collections.Generic
{
	public interface IPriorityQuadTree<T> : IEnumerable<T>
	{
		void Clear();
		System.Windows.Rect Extent { get; set; }
		System.Collections.Generic.IEnumerator<T> GetEnumerator();
		System.Collections.Generic.IEnumerable<T> GetItemsInside(System.Windows.Rect bounds);
		System.Collections.Generic.IEnumerable<T> GetItemsIntersecting(System.Windows.Rect bounds);
		bool HasItemsInside(System.Windows.Rect bounds);
		bool HasItemsIntersecting(System.Windows.Rect bounds);
		void Insert(T item, System.Windows.Rect bounds, double priority);
		bool Remove(T item);
		bool Remove(T item, System.Windows.Rect bounds);
	} // interface
} // namespace
