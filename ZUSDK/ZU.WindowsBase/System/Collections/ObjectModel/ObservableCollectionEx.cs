using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.ObjectModel
{
	[Serializable]
	public class ObservableCollectionEx<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged, IDisposable
	{
		private const string _countString = "Count";

		private const string _indexerName = "Item[]";

		[NonSerialized]
		private readonly static NotifyCollectionChangedEventHandler _emptyDelegate;

		[NonSerialized]
		private ObservableCollectionEx<T>.ReentryMonitor _monitor;

		[NonSerialized]
		private ObservableCollectionEx<T>.NotificationInfo _notifyInfo;

		[NonSerialized]
		private bool _disableReentry;

		[NonSerialized]
		private Action FireCountAndIndexerChanged;

		[NonSerialized]
		private Action FireIndexerChanged;

		static ObservableCollectionEx()
		{
			ObservableCollectionEx<T>._emptyDelegate = (object param0, NotifyCollectionChangedEventArgs param1) =>
			{
			};
		}

		public ObservableCollectionEx()
		{
			this._monitor = new ObservableCollectionEx<T>.ReentryMonitor();
			this.FireCountAndIndexerChanged = () =>
			{
			};
			this.FireIndexerChanged = () =>
			{
			};
			this.CollectionChanged = ObservableCollectionEx<T>._emptyDelegate;

		}

		public ObservableCollectionEx(List<T> list)
			: base((list != null ? new List<T>(list.Count) : list))
		{
			this._monitor = new ObservableCollectionEx<T>.ReentryMonitor();
			this.FireCountAndIndexerChanged = () =>
			{
			};
			this.FireIndexerChanged = () =>
			{
			};
			this.CollectionChanged = ObservableCollectionEx<T>._emptyDelegate;

			foreach (T item in list)
			{
				base.Items.Add(item);
			}
		}

		public ObservableCollectionEx(IEnumerable<T> collection)
			: base()
		{
			this._monitor = new ObservableCollectionEx<T>.ReentryMonitor();
			this.FireCountAndIndexerChanged = () =>
			{
			};
			this.FireIndexerChanged = () =>
			{
			};
			this.CollectionChanged = ObservableCollectionEx<T>._emptyDelegate;

			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (T t in collection)
			{
				base.Items.Add(t);
			}
		}

		public ObservableCollectionEx(ObservableCollectionEx<T> parent, bool notify)
			: base(parent.Items)
		{
			this._monitor = new ObservableCollectionEx<T>.ReentryMonitor();
			this.FireCountAndIndexerChanged = () =>
			{
			};
			this.FireIndexerChanged = () =>
			{
			};
			this.CollectionChanged = ObservableCollectionEx<T>._emptyDelegate;

			this._notifyInfo = new ObservableCollectionEx<T>.NotificationInfo()
			{
				RootCollection = parent
			};
			if (notify)
			{
				this.CollectionChanged = this._notifyInfo.Initialize();
			}
		}

		protected IDisposable BlockReentrancy()
		{
			return this._monitor.Enter();
		}

		protected void CheckReentrancy()
		{
			if (this._disableReentry && this._monitor.IsNotifying)
			{
				throw new InvalidOperationException("ObservableCollectionEx Reentrancy Not Allowed");
			}
		}

		protected override void ClearItems()
		{
			this.CheckReentrancy();
			base.ClearItems();
			this.FireCountAndIndexerChanged();
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public ObservableCollectionEx<T> DelayNotifications()
		{
			ObservableCollectionEx<T> rootCollection;
			if (this._notifyInfo == null)
			{
				rootCollection = this;
			}
			else
			{
				rootCollection = this._notifyInfo.RootCollection;
			}
			return new ObservableCollectionEx<T>(rootCollection, true);
		}

		public ObservableCollectionEx<T> DisableNotifications()
		{
			ObservableCollectionEx<T> rootCollection;
			if (this._notifyInfo == null)
			{
				rootCollection = this;
			}
			else
			{
				rootCollection = this._notifyInfo.RootCollection;
			}
			return new ObservableCollectionEx<T>(rootCollection, false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool reason)
		{
			if (this._notifyInfo != null && this._notifyInfo.HasEventArgs)
			{
				if (this._notifyInfo.RootCollection.PropertyChanged != null)
				{
					if (this._notifyInfo.IsCountChanged)
					{
						this._notifyInfo.RootCollection.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
					}
					this._notifyInfo.RootCollection.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
				}
				using (IDisposable disposable = this._notifyInfo.RootCollection.BlockReentrancy())
				{
					NotifyCollectionChangedEventArgs args = this._notifyInfo.EventArgs;
					Delegate[] invocationList = this._notifyInfo.RootCollection.CollectionChanged.GetInvocationList();
					for (int i = 0; i < (int)invocationList.Length; i++)
					{
						Delegate delegateItem = invocationList[i];
						try
						{
							object[] rootCollection = new object[] { this._notifyInfo.RootCollection, args };
							delegateItem.DynamicInvoke(rootCollection);
						}
						catch (TargetInvocationException targetInvocationException)
						{
							if (!(targetInvocationException.InnerException is NotSupportedException) || !(delegateItem.Target is ICollectionView))
							{
								throw;
							}
							else
							{
								(delegateItem.Target as ICollectionView).Refresh();
							}
						}
					}
				}
				this.CollectionChanged = this._notifyInfo.Initialize();
			}
		}

		protected override void InsertItem(int index, T item)
		{
			this.CheckReentrancy();
			base.InsertItem(index, item);
			this.FireCountAndIndexerChanged();
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object)item, index));
		}

		public void Move(int oldIndex, int newIndex)
		{
			this.MoveItem(oldIndex, newIndex);
		}

		protected virtual void MoveItem(int oldIndex, int newIndex)
		{
			this.CheckReentrancy();
			T removedItem = base[oldIndex];
			base.RemoveItem(oldIndex);
			base.InsertItem(newIndex, removedItem);
			this.FireIndexerChanged();
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, (object)removedItem, newIndex, oldIndex));
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			using (IDisposable disposable = this.BlockReentrancy())
			{
				Delegate[] invocationList = this.CollectionChanged.GetInvocationList();
				for (int i = 0; i < (int)invocationList.Length; i++)
				{
					Delegate delegateItem = invocationList[i];
					try
					{
						object[] objArray = new object[] { this, args };
						delegateItem.DynamicInvoke(objArray);
					}
					catch (TargetInvocationException targetInvocationException)
					{
						if (!(delegateItem.Target is ICollectionView))
						{
							throw targetInvocationException;
						}
						else
						{
							(delegateItem.Target as ICollectionView).Refresh();
						}
					}
				}
			}
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			this.PropertyChanged(this, e);
		}

		protected override void RemoveItem(int index)
		{
			this.CheckReentrancy();
			T removedItem = base[index];
			base.RemoveItem(index);
			this.FireCountAndIndexerChanged();
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object)removedItem, index));
		}

		protected override void SetItem(int index, T item)
		{
			this.CheckReentrancy();
			T originalItem = base[index];
			base.SetItem(index, item);
			this.FireIndexerChanged();
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, (object)originalItem, (object)item, index));
		}

		protected virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		protected virtual event PropertyChangedEventHandler PropertyChanged;

		event NotifyCollectionChangedEventHandler System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged
		{
			add
			{
				if (this._notifyInfo != null)
				{
					this._notifyInfo.RootCollection.CollectionChanged += value;
					return;
				}
				if (1 == (int)this.CollectionChanged.GetInvocationList().Length)
				{
					this.CollectionChanged -= ObservableCollectionEx<T>._emptyDelegate;
				}
				this.CollectionChanged += value;
				this._disableReentry = (int)this.CollectionChanged.GetInvocationList().Length > 1;
			}
			remove
			{
				if (this._notifyInfo != null)
				{
					this._notifyInfo.RootCollection.CollectionChanged -= value;
					return;
				}
				this.CollectionChanged -= value;
				if (this.CollectionChanged == null || (int)this.CollectionChanged.GetInvocationList().Length == 0)
				{
					this.CollectionChanged += ObservableCollectionEx<T>._emptyDelegate;
				}
				this._disableReentry = (int)this.CollectionChanged.GetInvocationList().Length > 1;
			}
		}

		event PropertyChangedEventHandler System.ComponentModel.INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				if (this._notifyInfo != null)
				{
					this._notifyInfo.RootCollection.PropertyChanged += value;
					return;
				}
				if (this.PropertyChanged == null)
				{
					this.FireCountAndIndexerChanged = () =>
					{
						this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
						this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
					};
					this.FireIndexerChanged = () => this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
				}
				this.PropertyChanged += value;
			}
			remove
			{
				if (this._notifyInfo != null)
				{
					this._notifyInfo.RootCollection.PropertyChanged -= value;
				}
				else
				{
					this.PropertyChanged -= value;
					if (this.PropertyChanged == null)
					{
						this.FireCountAndIndexerChanged = () =>
						{
						};
						this.FireIndexerChanged = () =>
						{
						};
						return;
					}
				}
			}
		}

		private class NotificationInfo
		{
			private NotifyCollectionChangedAction? _action;

			private IList _newItems;

			private IList _oldItems;

			private int _newIndex;

			private int _oldIndex;

			public NotifyCollectionChangedEventArgs EventArgs
			{
				get
				{
					NotifyCollectionChangedAction? nullable = this._action;
					NotifyCollectionChangedAction valueOrDefault = nullable.GetValueOrDefault();
					if (nullable.HasValue)
					{
						switch (valueOrDefault)
						{
							case NotifyCollectionChangedAction.Add:
								{
									return new NotifyCollectionChangedEventArgs(this._action.Value, this._newItems);
								}
							case NotifyCollectionChangedAction.Remove:
								{
									return new NotifyCollectionChangedEventArgs(this._action.Value, this._oldItems);
								}
							case NotifyCollectionChangedAction.Replace:
								{
									return new NotifyCollectionChangedEventArgs(this._action.Value, this._newItems, this._oldItems);
								}
							case NotifyCollectionChangedAction.Move:
								{
									return new NotifyCollectionChangedEventArgs(this._action.Value, this._oldItems[0], this._newIndex, this._oldIndex);
								}
							case NotifyCollectionChangedAction.Reset:
								{
									return new NotifyCollectionChangedEventArgs(this._action.Value);
								}
						}
					}
					return null;
				}
			}

			public bool HasEventArgs
			{
				get
				{
					return this._action.HasValue;
				}
			}

			public bool IsCountChanged
			{
				get;
				private set;
			}

			public ObservableCollectionEx<T> RootCollection
			{
				get;
				set;
			}

			public NotificationInfo()
			{
			}

			private void AssertActionType(NotifyCollectionChangedEventArgs e)
			{
				NotifyCollectionChangedAction action = e.Action;
				NotifyCollectionChangedAction? nullable = this._action;
				if ((action != nullable.GetValueOrDefault() ? true : !nullable.HasValue))
				{
					throw new InvalidOperationException(string.Format("Attempting to perform {0} during {1}. Mixed actions on the same delayed interface are not allowed.", e.Action, this._action));
				}
			}

			public NotifyCollectionChangedEventHandler Initialize()
			{
				this._action = null;
				this._newItems = null;
				this._oldItems = null;
				return (object sender, NotifyCollectionChangedEventArgs args) =>
				{
					ObservableCollectionEx<T> wrapper = sender as ObservableCollectionEx<T>;
					this._action = new NotifyCollectionChangedAction?(args.Action);
					NotifyCollectionChangedAction? nullable = this._action;
					NotifyCollectionChangedAction valueOrDefault = nullable.GetValueOrDefault();
					if (nullable.HasValue)
					{
						switch (valueOrDefault)
						{
							case NotifyCollectionChangedAction.Add:
								{
									this._newItems = new List<T>();
									this.IsCountChanged = true;
									wrapper.CollectionChanged = (object s, NotifyCollectionChangedEventArgs e) =>
									{
										this.AssertActionType(e);
										foreach (T item in e.NewItems)
										{
											this._newItems.Add(item);
										}
									};
									wrapper.CollectionChanged(sender, args);
									return;
								}
							case NotifyCollectionChangedAction.Remove:
								{
									this._oldItems = new List<T>();
									this.IsCountChanged = true;
									wrapper.CollectionChanged = (object s, NotifyCollectionChangedEventArgs e) =>
									{
										this.AssertActionType(e);
										foreach (T item in e.OldItems)
										{
											this._oldItems.Add(item);
										}
									};
									wrapper.CollectionChanged(sender, args);
									return;
								}
							case NotifyCollectionChangedAction.Replace:
								{
									this._newItems = new List<T>();
									this._oldItems = new List<T>();
									wrapper.CollectionChanged = (object s, NotifyCollectionChangedEventArgs e) =>
									{
										this.AssertActionType(e);
										foreach (T item in e.NewItems)
										{
											this._newItems.Add(item);
										}
										foreach (T item in e.OldItems)
										{
											this._oldItems.Add(item);
										}
									};
									wrapper.CollectionChanged(sender, args);
									return;
								}
							case NotifyCollectionChangedAction.Move:
								{
									this._newIndex = args.NewStartingIndex;
									this._newItems = args.NewItems;
									this._oldIndex = args.OldStartingIndex;
									this._oldItems = args.OldItems;
									wrapper.CollectionChanged = this.InternalCollectionChanged;
									return;
								}
							case NotifyCollectionChangedAction.Reset:
								{
									this.IsCountChanged = true;
									wrapper.CollectionChanged = (object s, NotifyCollectionChangedEventArgs e) => this.AssertActionType(e);
									break;
								}
							default:
								{
									return;
								}
						}
					}
				};
			}

			private void InternalCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
			{
				throw new InvalidOperationException("Due to design of NotifyCollectionChangedEventArgs combination of multiple Move operations is not possible");
			}
		}

		[Serializable]
		private class ReentryMonitor : IDisposable
		{
			private int _referenceCount;

			public bool IsNotifying
			{
				get
				{
					return this._referenceCount != 0;
				}
			}

			public ReentryMonitor()
			{
			}

			public void Dispose()
			{
				ObservableCollectionEx<T>.ReentryMonitor reentryMonitor = this;
				reentryMonitor._referenceCount = reentryMonitor._referenceCount - 1;
			}

			public IDisposable Enter()
			{
				ObservableCollectionEx<T>.ReentryMonitor reentryMonitor = this;
				reentryMonitor._referenceCount = reentryMonitor._referenceCount + 1;
				return this;
			}
		}
	}
}