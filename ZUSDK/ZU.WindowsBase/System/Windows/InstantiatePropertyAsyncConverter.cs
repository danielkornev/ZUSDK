using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Threading.Tasks.Schedulers;
using System.Threading;

namespace System.Windows
{
	public class InstantiatePropertyAsyncConverter : IValueConverter
	{
		private TaskScheduler _taskScheduler;
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			Task.Factory.StartNew((context) =>
			{
				var init = value as IInstantiateProperty;
				if (init != null)
				{
					init.InstantiateProperty((parameter as string) ?? PropertyName, culture, (SynchronizationContext)context);
				}
			}, SynchronizationContext.Current, CancellationToken.None, TaskCreationOptions.None, TaskScheduler);
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public string PropertyName { get; set; }

		/// <summary>
		/// MaxConcurrentLevel = 0 mean that TaskScheduler will use up to ProcessorCount threads
		/// so mostly set this property to 1
		/// </summary>
		public int MaxConcurrentLevel { get; set; }

		public bool UseQueue { get; set; }

		public TaskScheduler TaskScheduler
		{
			get
			{
				return LazyInitializer.EnsureInitialized(ref _taskScheduler, () => UseQueue ? (TaskScheduler)new QueuedTaskScheduler(TaskScheduler.Default, MaxConcurrentLevel) : (TaskScheduler)new StackedTaskScheduler(TaskScheduler.Default, MaxConcurrentLevel));
			}
		}
	} // class 
} // namespace
