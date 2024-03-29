﻿using System;
using System.Collections;
using System.Collections.Generic;
using ZU.Collections.Generic;
namespace ZU.Semantic.Spatial.Collections
{
	/// <summary>
	/// This interface supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public interface ISpatialRangeObservableCollection<T> : ISpatialItemsSource, ICollection<T>
	{
		new System.Windows.Rect Extent { get; }
		event EventHandler ExtentChanged;
		System.Collections.Generic.IEnumerable<int> Query(System.Windows.Rect bounds);
		event EventHandler QueryInvalidated;
		double Scale { get; set; }
		IPriorityQuadTree<Tuple<int, ZU.Core.IEntity, System.Windows.Rect>> SpatialIndex { get; set; }
	} // interface
} // namespace
