using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace System.Collections.ObjectModel
{
	public class ThreadSafeObservableCollection<T> : ObservableCollection<T>
	{

		Dispatcher _dispatcher;
		ReaderWriterLock _lock;

		public ThreadSafeObservableCollection()
		{
			_dispatcher = Dispatcher.CurrentDispatcher;
			if (_dispatcher != Application.Current.Dispatcher)
				_dispatcher = Application.Current.Dispatcher;
			_lock = new ReaderWriterLock();
		}

		protected override void ClearItems()
		{
			if (_dispatcher.CheckAccess())
			{
				LockCookie c = _lock.UpgradeToWriterLock(-1);
				base.ClearItems();
				_lock.DowngradeFromWriterLock(ref c);
			}
			else
			{
				_dispatcher.Invoke(DispatcherPriority.Input, (SendOrPostCallback)delegate { Clear(); }, null);
			}
		}
		protected override void InsertItem(int index, T item)
		{
			if (_dispatcher.CheckAccess())
			{
				if (index > this.Count)
					return;
				LockCookie c = _lock.UpgradeToWriterLock(-1);
				base.InsertItem(index, item);
				_lock.DowngradeFromWriterLock(ref c);
			}
			else
			{
				object[] e = new object[] { index, item };
				_dispatcher.Invoke(DispatcherPriority.Input, (SendOrPostCallback)delegate { InsertItemImpl(e); }, e);
			}

		}
		void InsertItemImpl(object[] e)
		{
			if (_dispatcher.CheckAccess())
			{
				InsertItem((int)e[0], (T)e[1]);
			}
			else
			{
				_dispatcher.Invoke(DispatcherPriority.Input, (SendOrPostCallback)delegate { InsertItemImpl(e); });
			}
		}
		protected override void MoveItem(int oldIndex, int newIndex)
		{
			if (_dispatcher.CheckAccess())
			{
				if (oldIndex >= this.Count | newIndex >= this.Count | oldIndex == newIndex)
					return;
				LockCookie c = _lock.UpgradeToWriterLock(-1);
				base.MoveItem(oldIndex, newIndex);
				_lock.DowngradeFromWriterLock(ref c);
			}
			else
			{
				object[] e = new object[] { oldIndex, newIndex };
				_dispatcher.Invoke(DispatcherPriority.Input, (SendOrPostCallback)delegate { MoveItemImpl(e); }, e);
			}
		}
		void MoveItemImpl(object[] e)
		{
			if (_dispatcher.CheckAccess())
			{
				MoveItem((int)e[0], (int)e[1]);
			}
			else
			{
				_dispatcher.Invoke(DispatcherPriority.Input, (SendOrPostCallback)delegate { MoveItemImpl(e); });
			}
		}
		protected override void RemoveItem(int index)
		{

			if (_dispatcher.CheckAccess())
			{
				if (index >= this.Count)
					return;
				LockCookie c = _lock.UpgradeToWriterLock(-1);
				base.RemoveItem(index);
				_lock.DowngradeFromWriterLock(ref c);
			}
			else
			{
				_dispatcher.Invoke(DispatcherPriority.Input, (SendOrPostCallback)delegate { RemoveItem(index); }, index);
			}
		}
		protected override void SetItem(int index, T item)
		{
			if (_dispatcher.CheckAccess())
			{
				LockCookie c = _lock.UpgradeToWriterLock(-1);
				base.SetItem(index, item);
				_lock.DowngradeFromWriterLock(ref c);
			}
			else
			{
				object[] e = new object[] { index, item };
				_dispatcher.Invoke(DispatcherPriority.Input, (SendOrPostCallback)delegate { SetItemImpl(e); }, e);
			}
		}
		void SetItemImpl(object[] e)
		{
			if (_dispatcher.CheckAccess())
			{
				SetItem((int)e[0], (T)e[1]);
			}
			else
			{
				_dispatcher.Invoke(DispatcherPriority.Input, (SendOrPostCallback)delegate { SetItemImpl(e); });
			}
		}

		public override event NotifyCollectionChangedEventHandler CollectionChanged;

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (this.CollectionChanged != null)
			{
				using (IDisposable locker = BlockReentrancy())
				{
					foreach (Delegate invoker in CollectionChanged.GetInvocationList())
					{
						NotifyCollectionChangedEventHandler handler = (NotifyCollectionChangedEventHandler)invoker;
						DispatcherObject dispatcherInvoker = invoker.Target as DispatcherObject;
						ISynchronizeInvoke syncInvoker = invoker.Target as ISynchronizeInvoke;
						if (dispatcherInvoker != null)
						{
							dispatcherInvoker.Dispatcher.Invoke(DispatcherPriority.DataBind,
								(NotifyCollectionChangedEventHandler)delegate(object s, NotifyCollectionChangedEventArgs ex)
								{
									handler(s, ex);
								}, this, e);
						}
						else if (syncInvoker != null)
						{
							syncInvoker.Invoke(invoker, new object[] { this, e });
						}
						else
						{
							handler(this, e);
						}
					}
				}
			}
		}


		public T[] ToSyncArray()
		{
			_lock.AcquireReaderLock(-1);
			T[] _sync = new T[this.Count];
			this.CopyTo(_sync, 0);
			_lock.ReleaseReaderLock();
			return _sync;
		}
	}
}
