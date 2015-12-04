//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Text;

//namespace System.Collections.ObjectModel
//{
//	/// <summary>
//	/// From: http://peteohanlon.wordpress.com/2008/10/22/bulk-loading-in-observablecollection/
//	/// </summary>
//	/// <typeparam name="T"></typeparam>
//	public class RangeObservableCollection<T> : ObservableCollection<T>, ZU.System.Collections.ObjectModel.IRangeObservableCollection<T>
//	{
//		private bool _suppressNotification = false;

//		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
//		{
//			if (!_suppressNotification)
//				base.OnCollectionChanged(e);
//		}

//		public void NotifyRefreshCollection()
//		{
//			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
//		}

//		public virtual void AddRange(IEnumerable<T> list)
//		{
//			if (list == null)
//				throw new ArgumentNullException("list");

//			_suppressNotification = true;

//			foreach (T item in list)
//			{
//				Add(item);
//			}
//			_suppressNotification = false;
//			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

//			// let's try this... raise "Add" event with a list of new items; old items = null
//			//OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list.ToList()));
//		}
//	} // class
//} // namespace
